using System;
using System.Globalization;
using System.Security.Permissions;
using System.Text;
using System.Web.Configuration;
using System.Web.Security.Cryptography;

namespace System.Web.Security
{
	// Token: 0x020005CE RID: 1486
	public sealed class AnonymousIdentificationModule : IHttpModule
	{
		// Token: 0x06004B50 RID: 19280 RVA: 0x000030B5 File Offset: 0x000012B5
		[SecurityPermission(SecurityAction.Demand, Unrestricted = true)]
		public AnonymousIdentificationModule()
		{
		}

		// Token: 0x14000126 RID: 294
		// (add) Token: 0x06004B51 RID: 19281 RVA: 0x000FF3A1 File Offset: 0x000FD5A1
		// (remove) Token: 0x06004B52 RID: 19282 RVA: 0x000FF3BA File Offset: 0x000FD5BA
		public event AnonymousIdentificationEventHandler Creating
		{
			add
			{
				this._CreateNewIdEventHandler = (AnonymousIdentificationEventHandler)Delegate.Combine(this._CreateNewIdEventHandler, value);
			}
			remove
			{
				this._CreateNewIdEventHandler = (AnonymousIdentificationEventHandler)Delegate.Remove(this._CreateNewIdEventHandler, value);
			}
		}

		// Token: 0x06004B53 RID: 19283 RVA: 0x000FF3D4 File Offset: 0x000FD5D4
		public static void ClearAnonymousIdentifier()
		{
			if (!AnonymousIdentificationModule.s_Initialized)
			{
				AnonymousIdentificationModule.Initialize();
			}
			HttpContext httpContext = HttpContext.Current;
			if (httpContext == null)
			{
				return;
			}
			if (!AnonymousIdentificationModule.s_Enabled || !httpContext.Request.IsAuthenticated)
			{
				throw new NotSupportedException(SR.GetString("Anonymous_ClearAnonymousIdentifierNotSupported"));
			}
			bool flag = false;
			if (httpContext.CookielessHelper.GetCookieValue('A') != null)
			{
				httpContext.CookielessHelper.SetCookieValue('A', null);
				flag = true;
			}
			if (!CookielessHelperClass.UseCookieless(httpContext, false, AnonymousIdentificationModule.s_CookieMode) || httpContext.Request.Browser.Cookies)
			{
				string value = string.Empty;
				if (httpContext.Request.Browser["supportsEmptyStringInCookieValue"] == "false")
				{
					value = "NoCookie";
				}
				HttpCookie httpCookie = new HttpCookie(AnonymousIdentificationModule.s_CookieName, value);
				httpCookie.HttpOnly = true;
				httpCookie.Path = AnonymousIdentificationModule.s_CookiePath;
				httpCookie.Secure = AnonymousIdentificationModule.s_RequireSSL;
				if (AnonymousIdentificationModule.s_Domain != null)
				{
					httpCookie.Domain = AnonymousIdentificationModule.s_Domain;
				}
				httpCookie.Expires = new DateTime(1999, 10, 12);
				httpContext.Response.Cookies.RemoveCookie(AnonymousIdentificationModule.s_CookieName);
				httpContext.Response.Cookies.Add(httpCookie);
			}
			if (flag)
			{
				httpContext.Response.Redirect(httpContext.Request.RawUrl, false);
			}
		}

		// Token: 0x06004B54 RID: 19284 RVA: 0x00006164 File Offset: 0x00004364
		public void Dispose()
		{
		}

		// Token: 0x06004B55 RID: 19285 RVA: 0x000FF51B File Offset: 0x000FD71B
		public void Init(HttpApplication app)
		{
			if (!AnonymousIdentificationModule.s_Initialized)
			{
				AnonymousIdentificationModule.Initialize();
			}
			if (AnonymousIdentificationModule.s_Enabled)
			{
				app.PostAuthenticateRequest += this.OnEnter;
			}
		}

		// Token: 0x06004B56 RID: 19286 RVA: 0x000FF544 File Offset: 0x000FD744
		private void OnEnter(object source, EventArgs eventArgs)
		{
			if (!AnonymousIdentificationModule.s_Initialized)
			{
				AnonymousIdentificationModule.Initialize();
			}
			if (!AnonymousIdentificationModule.s_Enabled)
			{
				return;
			}
			bool flag = false;
			string text = null;
			HttpApplication httpApplication = (HttpApplication)source;
			HttpContext context = httpApplication.Context;
			bool isAuthenticated = context.Request.IsAuthenticated;
			bool flag2;
			if (isAuthenticated)
			{
				flag2 = CookielessHelperClass.UseCookieless(context, false, AnonymousIdentificationModule.s_CookieMode);
			}
			else
			{
				flag2 = CookielessHelperClass.UseCookieless(context, true, AnonymousIdentificationModule.s_CookieMode);
			}
			if (AnonymousIdentificationModule.s_RequireSSL && !context.Request.IsSecureConnection && !flag2)
			{
				HttpCookie httpCookie = context.Request.Cookies[AnonymousIdentificationModule.s_CookieName];
				if (httpCookie != null)
				{
					httpCookie = new HttpCookie(AnonymousIdentificationModule.s_CookieName, string.Empty);
					httpCookie.HttpOnly = true;
					httpCookie.Path = AnonymousIdentificationModule.s_CookiePath;
					httpCookie.Secure = AnonymousIdentificationModule.s_RequireSSL;
					if (AnonymousIdentificationModule.s_Domain != null)
					{
						httpCookie.Domain = AnonymousIdentificationModule.s_Domain;
					}
					httpCookie.Expires = new DateTime(1999, 10, 12);
					if (context.Request.Browser["supportsEmptyStringInCookieValue"] == "false")
					{
						httpCookie.Value = "NoCookie";
					}
					context.Response.Cookies.Add(httpCookie);
				}
				return;
			}
			if (!flag2)
			{
				HttpCookie httpCookie = context.Request.Cookies[AnonymousIdentificationModule.s_CookieName];
				if (httpCookie != null)
				{
					text = httpCookie.Value;
					httpCookie.Path = AnonymousIdentificationModule.s_CookiePath;
					if (AnonymousIdentificationModule.s_Domain != null)
					{
						httpCookie.Domain = AnonymousIdentificationModule.s_Domain;
					}
				}
			}
			else
			{
				text = context.CookielessHelper.GetCookieValue('A');
			}
			AnonymousIdData decodedValue = AnonymousIdentificationModule.GetDecodedValue(text);
			if (decodedValue != null && decodedValue.AnonymousId != null)
			{
				context.Request.AnonymousID = decodedValue.AnonymousId;
			}
			if (isAuthenticated)
			{
				return;
			}
			if (context.Request.AnonymousID == null)
			{
				if (this._CreateNewIdEventHandler != null)
				{
					AnonymousIdentificationEventArgs anonymousIdentificationEventArgs = new AnonymousIdentificationEventArgs(context);
					this._CreateNewIdEventHandler(this, anonymousIdentificationEventArgs);
					context.Request.AnonymousID = anonymousIdentificationEventArgs.AnonymousID;
				}
				if (string.IsNullOrEmpty(context.Request.AnonymousID))
				{
					context.Request.AnonymousID = Guid.NewGuid().ToString("D", CultureInfo.InvariantCulture);
				}
				else if (context.Request.AnonymousID.Length > 128)
				{
					throw new HttpException(SR.GetString("Anonymous_id_too_long"));
				}
				if (AnonymousIdentificationModule.s_RequireSSL && !context.Request.IsSecureConnection && !flag2)
				{
					return;
				}
				flag = true;
			}
			DateTime utcNow = DateTime.UtcNow;
			if (!flag && AnonymousIdentificationModule.s_SlidingExpiration)
			{
				if (decodedValue == null || decodedValue.ExpireDate < utcNow)
				{
					flag = true;
				}
				else
				{
					double totalSeconds = (decodedValue.ExpireDate - utcNow).TotalSeconds;
					if (totalSeconds < (double)(AnonymousIdentificationModule.s_CookieTimeout * 60 / 2))
					{
						flag = true;
					}
				}
			}
			if (flag)
			{
				DateTime dateTime = utcNow.AddMinutes((double)AnonymousIdentificationModule.s_CookieTimeout);
				text = AnonymousIdentificationModule.GetEncodedValue(new AnonymousIdData(context.Request.AnonymousID, dateTime));
				if (text.Length > 512)
				{
					throw new HttpException(SR.GetString("Anonymous_id_too_long_2"));
				}
				if (!flag2)
				{
					HttpCookie httpCookie = new HttpCookie(AnonymousIdentificationModule.s_CookieName, text);
					httpCookie.HttpOnly = true;
					httpCookie.Expires = dateTime;
					httpCookie.Path = AnonymousIdentificationModule.s_CookiePath;
					httpCookie.Secure = AnonymousIdentificationModule.s_RequireSSL;
					if (AnonymousIdentificationModule.s_Domain != null)
					{
						httpCookie.Domain = AnonymousIdentificationModule.s_Domain;
					}
					context.Response.Cookies.Add(httpCookie);
					return;
				}
				context.CookielessHelper.SetCookieValue('A', text);
				context.Response.Redirect(context.Request.RawUrl);
			}
		}

		// Token: 0x17001631 RID: 5681
		// (get) Token: 0x06004B57 RID: 19287 RVA: 0x000FF8D4 File Offset: 0x000FDAD4
		public static bool Enabled
		{
			get
			{
				if (!AnonymousIdentificationModule.s_Initialized)
				{
					AnonymousIdentificationModule.Initialize();
				}
				return AnonymousIdentificationModule.s_Enabled;
			}
		}

		// Token: 0x06004B58 RID: 19288 RVA: 0x000FF8E8 File Offset: 0x000FDAE8
		private static void Initialize()
		{
			if (AnonymousIdentificationModule.s_Initialized)
			{
				return;
			}
			object obj = AnonymousIdentificationModule.s_InitLock;
			lock (obj)
			{
				if (!AnonymousIdentificationModule.s_Initialized)
				{
					AnonymousIdentificationSection anonymousIdentification = RuntimeConfig.GetAppConfig().AnonymousIdentification;
					AnonymousIdentificationModule.s_Enabled = anonymousIdentification.Enabled;
					AnonymousIdentificationModule.s_CookieName = anonymousIdentification.CookieName;
					AnonymousIdentificationModule.s_CookiePath = anonymousIdentification.CookiePath;
					AnonymousIdentificationModule.s_CookieTimeout = (int)anonymousIdentification.CookieTimeout.TotalMinutes;
					AnonymousIdentificationModule.s_RequireSSL = anonymousIdentification.CookieRequireSSL;
					AnonymousIdentificationModule.s_SlidingExpiration = anonymousIdentification.CookieSlidingExpiration;
					AnonymousIdentificationModule.s_Protection = anonymousIdentification.CookieProtection;
					AnonymousIdentificationModule.s_CookieMode = anonymousIdentification.Cookieless;
					AnonymousIdentificationModule.s_Domain = anonymousIdentification.Domain;
					AnonymousIdentificationModule.s_Modifier = Encoding.UTF8.GetBytes("AnonymousIdentification");
					if (AnonymousIdentificationModule.s_CookieTimeout < 1)
					{
						AnonymousIdentificationModule.s_CookieTimeout = 1;
					}
					if (AnonymousIdentificationModule.s_CookieTimeout > 1051200)
					{
						AnonymousIdentificationModule.s_CookieTimeout = 1051200;
					}
					AnonymousIdentificationModule.s_Initialized = true;
				}
			}
		}

		// Token: 0x06004B59 RID: 19289 RVA: 0x000FF9EC File Offset: 0x000FDBEC
		private static string GetEncodedValue(AnonymousIdData data)
		{
			if (data == null)
			{
				return null;
			}
			byte[] bytes = Encoding.UTF8.GetBytes(data.AnonymousId);
			byte[] bytes2 = BitConverter.GetBytes(bytes.Length);
			byte[] bytes3 = BitConverter.GetBytes(data.ExpireDate.ToFileTimeUtc());
			byte[] array = new byte[12 + bytes.Length];
			Buffer.BlockCopy(bytes3, 0, array, 0, 8);
			Buffer.BlockCopy(bytes2, 0, array, 8, 4);
			Buffer.BlockCopy(bytes, 0, array, 12, bytes.Length);
			return CookieProtectionHelper.Encode(AnonymousIdentificationModule.s_Protection, array, Purpose.AnonymousIdentificationModule_Ticket);
		}

		// Token: 0x06004B5A RID: 19290 RVA: 0x000FFA68 File Offset: 0x000FDC68
		private static AnonymousIdData GetDecodedValue(string data)
		{
			if (data == null || data.Length < 1 || data.Length > 512)
			{
				return null;
			}
			try
			{
				byte[] array = CookieProtectionHelper.Decode(AnonymousIdentificationModule.s_Protection, data, Purpose.AnonymousIdentificationModule_Ticket);
				if (array == null || array.Length < 13)
				{
					return null;
				}
				DateTime dateTime = DateTime.FromFileTimeUtc(BitConverter.ToInt64(array, 0));
				if (dateTime < DateTime.UtcNow)
				{
					return null;
				}
				int num = BitConverter.ToInt32(array, 8);
				if (num < 0 || num > array.Length - 12)
				{
					return null;
				}
				string @string = Encoding.UTF8.GetString(array, 12, num);
				if (@string.Length > 128)
				{
					return null;
				}
				return new AnonymousIdData(@string, dateTime);
			}
			catch
			{
			}
			return null;
		}

		// Token: 0x04002895 RID: 10389
		private const int MAX_ENCODED_COOKIE_STRING = 512;

		// Token: 0x04002896 RID: 10390
		private const int MAX_ID_LENGTH = 128;

		// Token: 0x04002897 RID: 10391
		private AnonymousIdentificationEventHandler _CreateNewIdEventHandler;

		// Token: 0x04002898 RID: 10392
		private static bool s_Initialized = false;

		// Token: 0x04002899 RID: 10393
		private static bool s_Enabled = false;

		// Token: 0x0400289A RID: 10394
		private static string s_CookieName = ".ASPXANONYMOUS";

		// Token: 0x0400289B RID: 10395
		private static string s_CookiePath = "/";

		// Token: 0x0400289C RID: 10396
		private static int s_CookieTimeout = 100000;

		// Token: 0x0400289D RID: 10397
		private static bool s_RequireSSL = false;

		// Token: 0x0400289E RID: 10398
		private static string s_Domain = null;

		// Token: 0x0400289F RID: 10399
		private static bool s_SlidingExpiration = true;

		// Token: 0x040028A0 RID: 10400
		private static byte[] s_Modifier = null;

		// Token: 0x040028A1 RID: 10401
		private static object s_InitLock = new object();

		// Token: 0x040028A2 RID: 10402
		private static HttpCookieMode s_CookieMode = HttpCookieMode.UseDeviceProfile;

		// Token: 0x040028A3 RID: 10403
		private static CookieProtection s_Protection = CookieProtection.None;
	}
}
