using System;
using System.Globalization;
using System.Security.Cryptography;
using System.Web.Configuration;
using System.Web.Hosting;
using System.Web.Security;
using System.Web.Util;

namespace System.Web.SessionState
{
	// Token: 0x02000128 RID: 296
	public class SessionIDManager : ISessionIDManager
	{
		// Token: 0x17000583 RID: 1411
		// (get) Token: 0x060011B8 RID: 4536 RVA: 0x00031B32 File Offset: 0x0002FD32
		public static int SessionIDMaxLength
		{
			get
			{
				return 80;
			}
		}

		// Token: 0x060011B9 RID: 4537 RVA: 0x00031B38 File Offset: 0x0002FD38
		private void OneTimeInit()
		{
			SessionStateSection sessionState = RuntimeConfig.GetAppConfig().SessionState;
			SessionIDManager.s_appPath = HostingEnvironment.ApplicationVirtualPathObject.VirtualPathString;
			SessionIDManager.s_iSessionId = SessionIDManager.s_appPath.Length;
			SessionIDManager.s_config = sessionState;
		}

		// Token: 0x17000584 RID: 1412
		// (get) Token: 0x060011BA RID: 4538 RVA: 0x00031B74 File Offset: 0x0002FD74
		private static SessionStateSection Config
		{
			get
			{
				if (SessionIDManager.s_config == null)
				{
					throw new HttpException(SR.GetString("SessionIDManager_uninit"));
				}
				return SessionIDManager.s_config;
			}
		}

		// Token: 0x060011BB RID: 4539 RVA: 0x00031B94 File Offset: 0x0002FD94
		public void Initialize()
		{
			if (SessionIDManager.s_config == null)
			{
				SessionIDManager.s_lock.AcquireWriterLock();
				try
				{
					if (SessionIDManager.s_config == null)
					{
						this.OneTimeInit();
					}
				}
				finally
				{
					SessionIDManager.s_lock.ReleaseWriterLock();
				}
			}
			this._isInherited = !(base.GetType() == typeof(SessionIDManager));
		}

		// Token: 0x060011BC RID: 4540 RVA: 0x00031BFC File Offset: 0x0002FDFC
		internal void GetCookielessSessionID(HttpContext context, bool allowRedirect, out bool cookieless)
		{
			HttpRequest request = context.Request;
			cookieless = CookielessHelperClass.UseCookieless(context, allowRedirect, SessionIDManager.Config.Cookieless);
			context.Items["AspCookielessBoolSession"] = cookieless;
			if (cookieless)
			{
				string text = context.CookielessHelper.GetCookieValue('S');
				if (text == null)
				{
					text = string.Empty;
				}
				text = this.Decode(text);
				if (!this.ValidateInternal(text, false))
				{
					return;
				}
				context.Items.Add("AspCookielessSession", text);
			}
		}

		// Token: 0x060011BD RID: 4541 RVA: 0x00031C7C File Offset: 0x0002FE7C
		private static HttpCookie CreateSessionCookie(string id)
		{
			return new HttpCookie(SessionIDManager.Config.CookieName, id)
			{
				Path = "/",
				SameSite = SessionIDManager.Config.CookieSameSite,
				HttpOnly = true
			};
		}

		// Token: 0x060011BE RID: 4542 RVA: 0x00031CC0 File Offset: 0x0002FEC0
		internal static bool CheckIdLength(string id, bool throwOnFail)
		{
			bool result = true;
			if (id.Length > 80)
			{
				if (throwOnFail)
				{
					throw new HttpException(SR.GetString("Session_id_too_long", new object[]
					{
						80.ToString(CultureInfo.InvariantCulture),
						id
					}));
				}
				result = false;
			}
			return result;
		}

		// Token: 0x060011BF RID: 4543 RVA: 0x00031D0B File Offset: 0x0002FF0B
		private bool ValidateInternal(string id, bool throwOnIdCheck)
		{
			return SessionIDManager.CheckIdLength(id, throwOnIdCheck) && this.Validate(id);
		}

		// Token: 0x060011C0 RID: 4544 RVA: 0x00031D1F File Offset: 0x0002FF1F
		public virtual bool Validate(string id)
		{
			return SessionId.IsLegit(id);
		}

		// Token: 0x060011C1 RID: 4545 RVA: 0x00031D27 File Offset: 0x0002FF27
		public virtual string Encode(string id)
		{
			if (this._isInherited)
			{
				return HttpUtility.UrlEncode(id);
			}
			return id;
		}

		// Token: 0x060011C2 RID: 4546 RVA: 0x00031D39 File Offset: 0x0002FF39
		public virtual string Decode(string id)
		{
			if (this._isInherited)
			{
				return HttpUtility.UrlDecode(id);
			}
			return id.ToLower(CultureInfo.InvariantCulture);
		}

		// Token: 0x060011C3 RID: 4547 RVA: 0x00031D58 File Offset: 0x0002FF58
		internal bool UseCookieless(HttpContext context)
		{
			if (SessionIDManager.Config.Cookieless == HttpCookieMode.UseCookies)
			{
				return false;
			}
			object obj = context.Items["AspCookielessBoolSession"];
			return (bool)obj;
		}

		// Token: 0x060011C4 RID: 4548 RVA: 0x00031D8B File Offset: 0x0002FF8B
		private void CheckInitializeRequestCalled(HttpContext context)
		{
			if (context.Items["AspSessionIDManagerInitializeRequestCalled"] == null)
			{
				throw new HttpException(SR.GetString("SessionIDManager_InitializeRequest_not_called"));
			}
		}

		// Token: 0x060011C5 RID: 4549 RVA: 0x00031DB0 File Offset: 0x0002FFB0
		public bool InitializeRequest(HttpContext context, bool suppressAutoDetectRedirect, out bool supportSessionIDReissue)
		{
			if (context.Items["AspSessionIDManagerInitializeRequestCalled"] != null)
			{
				supportSessionIDReissue = this.UseCookieless(context);
				return false;
			}
			context.Items["AspSessionIDManagerInitializeRequestCalled"] = true;
			if (SessionIDManager.Config.Cookieless == HttpCookieMode.UseCookies)
			{
				supportSessionIDReissue = false;
				return false;
			}
			bool flag;
			this.GetCookielessSessionID(context, !suppressAutoDetectRedirect, out flag);
			supportSessionIDReissue = flag;
			return context.Response.IsRequestBeingRedirected;
		}

		// Token: 0x060011C6 RID: 4550 RVA: 0x00031E20 File Offset: 0x00030020
		public string GetSessionID(HttpContext context)
		{
			string text = null;
			this.CheckInitializeRequestCalled(context);
			if (this.UseCookieless(context))
			{
				text = (string)context.Items["AspCookielessSession"];
			}
			else
			{
				HttpCookie httpCookie = context.Request.Cookies[SessionIDManager.Config.CookieName];
				if (httpCookie != null && httpCookie.Value != null)
				{
					text = this.Decode(httpCookie.Value);
					if (text != null && !this.ValidateInternal(text, false))
					{
						text = null;
					}
				}
			}
			return text;
		}

		// Token: 0x060011C7 RID: 4551 RVA: 0x00031E9A File Offset: 0x0003009A
		public virtual string CreateSessionID(HttpContext context)
		{
			return SessionId.Create(ref this._randgen);
		}

		// Token: 0x060011C8 RID: 4552 RVA: 0x00031EA8 File Offset: 0x000300A8
		public void SaveSessionID(HttpContext context, string id, out bool redirected, out bool cookieAdded)
		{
			redirected = false;
			cookieAdded = false;
			this.CheckInitializeRequestCalled(context);
			if (context.Response.HeadersWritten)
			{
				throw new HttpException(SR.GetString("Cant_save_session_id_because_response_was_flushed"));
			}
			if (!this.ValidateInternal(id, true))
			{
				throw new HttpException(SR.GetString("Cant_save_session_id_because_id_is_invalid", new object[]
				{
					id
				}));
			}
			string text = this.Encode(id);
			if (!this.UseCookieless(context))
			{
				HttpCookie cookie = SessionIDManager.CreateSessionCookie(text);
				context.Response.Cookies.Add(cookie);
				cookieAdded = true;
				return;
			}
			context.CookielessHelper.SetCookieValue('S', text);
			HttpRequest request = context.Request;
			string text2 = request.Path;
			string queryStringText = request.QueryStringText;
			if (!string.IsNullOrEmpty(queryStringText))
			{
				text2 = text2 + "?" + queryStringText;
			}
			context.Response.Redirect(text2, false);
			context.ApplicationInstance.CompleteRequest();
			redirected = true;
		}

		// Token: 0x060011C9 RID: 4553 RVA: 0x00031F88 File Offset: 0x00030188
		public void RemoveSessionID(HttpContext context)
		{
			context.Response.Cookies.RemoveCookie(SessionIDManager.Config.CookieName);
		}

		// Token: 0x04001402 RID: 5122
		private const int COOKIELESS_SESSION_LENGTH = 26;

		// Token: 0x04001403 RID: 5123
		internal const string COOKIELESS_SESSION_KEY = "AspCookielessSession";

		// Token: 0x04001404 RID: 5124
		internal const string COOKIELESS_BOOL_SESSION_KEY = "AspCookielessBoolSession";

		// Token: 0x04001405 RID: 5125
		internal const string ASP_SESSIONID_MANAGER_INITIALIZEREQUEST_CALLED_KEY = "AspSessionIDManagerInitializeRequestCalled";

		// Token: 0x04001406 RID: 5126
		private static string s_appPath;

		// Token: 0x04001407 RID: 5127
		private static int s_iSessionId;

		// Token: 0x04001408 RID: 5128
		internal const HttpCookieMode COOKIEMODE_DEFAULT = HttpCookieMode.UseCookies;

		// Token: 0x04001409 RID: 5129
		internal const string SESSION_COOKIE_DEFAULT = "ASP.NET_SessionId";

		// Token: 0x0400140A RID: 5130
		internal const int SESSION_ID_LENGTH_LIMIT = 80;

		// Token: 0x0400140B RID: 5131
		private static ReadWriteSpinLock s_lock;

		// Token: 0x0400140C RID: 5132
		private static SessionStateSection s_config;

		// Token: 0x0400140D RID: 5133
		private bool _isInherited;

		// Token: 0x0400140E RID: 5134
		private RandomNumberGenerator _randgen;
	}
}
