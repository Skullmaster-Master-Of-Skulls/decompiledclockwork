using System;
using System.Collections.Specialized;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;
using System.Web.Compilation;
using System.Web.Configuration;
using System.Web.Management;
using System.Web.Security.Cryptography;
using System.Web.Util;

namespace System.Web.Security
{
	// Token: 0x020005DD RID: 1501
	public sealed class FormsAuthentication
	{
		// Token: 0x06004BCA RID: 19402 RVA: 0x00102288 File Offset: 0x00100488
		[Obsolete("The recommended alternative is to use the Membership APIs, such as Membership.CreateUser. For more information, see http://go.microsoft.com/fwlink/?LinkId=252463.")]
		public static string HashPasswordForStoringInConfigFile(string password, string passwordFormat)
		{
			if (password == null)
			{
				throw new ArgumentNullException("password");
			}
			if (passwordFormat == null)
			{
				throw new ArgumentNullException("passwordFormat");
			}
			HashAlgorithm hashAlgorithm;
			if (StringUtil.EqualsIgnoreCase(passwordFormat, "sha1"))
			{
				hashAlgorithm = CryptoAlgorithms.CreateSHA1();
			}
			else if (StringUtil.EqualsIgnoreCase(passwordFormat, "md5"))
			{
				hashAlgorithm = CryptoAlgorithms.CreateMD5();
			}
			else if (StringUtil.EqualsIgnoreCase(passwordFormat, "sha256"))
			{
				hashAlgorithm = CryptoAlgorithms.CreateSHA256();
			}
			else if (StringUtil.EqualsIgnoreCase(passwordFormat, "sha384"))
			{
				hashAlgorithm = CryptoAlgorithms.CreateSHA384();
			}
			else
			{
				if (!StringUtil.EqualsIgnoreCase(passwordFormat, "sha512"))
				{
					throw new ArgumentException(SR.GetString("InvalidArgumentValue", new object[]
					{
						"passwordFormat"
					}));
				}
				hashAlgorithm = CryptoAlgorithms.CreateSHA512();
			}
			string result;
			using (hashAlgorithm)
			{
				result = CryptoUtil.BinaryToHex(hashAlgorithm.ComputeHash(Encoding.UTF8.GetBytes(password)));
			}
			return result;
		}

		// Token: 0x06004BCB RID: 19403 RVA: 0x00102370 File Offset: 0x00100570
		public static void Initialize()
		{
			if (FormsAuthentication._Initialized)
			{
				return;
			}
			object lockObject = FormsAuthentication._lockObject;
			lock (lockObject)
			{
				if (!FormsAuthentication._Initialized)
				{
					AuthenticationSection authentication = RuntimeConfig.GetAppConfig().Authentication;
					authentication.ValidateAuthenticationMode();
					FormsAuthentication._FormsName = authentication.Forms.Name;
					FormsAuthentication._RequireSSL = authentication.Forms.RequireSSL;
					FormsAuthentication._SlidingExpiration = authentication.Forms.SlidingExpiration;
					if (FormsAuthentication._FormsName == null)
					{
						FormsAuthentication._FormsName = ".ASPXAUTH";
					}
					FormsAuthentication._Protection = authentication.Forms.Protection;
					FormsAuthentication._Timeout = (int)authentication.Forms.Timeout.TotalMinutes;
					FormsAuthentication._FormsCookiePath = authentication.Forms.Path;
					FormsAuthentication._LoginUrl = authentication.Forms.LoginUrl;
					if (FormsAuthentication._LoginUrl == null)
					{
						FormsAuthentication._LoginUrl = "login.aspx";
					}
					FormsAuthentication._DefaultUrl = authentication.Forms.DefaultUrl;
					if (FormsAuthentication._DefaultUrl == null)
					{
						FormsAuthentication._DefaultUrl = "default.aspx";
					}
					FormsAuthentication._CookieMode = authentication.Forms.Cookieless;
					FormsAuthentication._CookieDomain = authentication.Forms.Domain;
					FormsAuthentication._EnableCrossAppRedirects = authentication.Forms.EnableCrossAppRedirects;
					FormsAuthentication._TicketCompatibilityMode = authentication.Forms.TicketCompatibilityMode;
					FormsAuthentication._cookieSameSite = authentication.Forms.CookieSameSite;
					FormsAuthentication._Initialized = true;
				}
			}
		}

		// Token: 0x06004BCC RID: 19404 RVA: 0x001024EC File Offset: 0x001006EC
		public static FormsAuthenticationTicket Decrypt(string encryptedTicket)
		{
			if (string.IsNullOrEmpty(encryptedTicket) || encryptedTicket.Length > 4096)
			{
				throw new ArgumentException(SR.GetString("InvalidArgumentValue", new object[]
				{
					"encryptedTicket"
				}));
			}
			FormsAuthentication.Initialize();
			byte[] array = null;
			if (encryptedTicket.Length % 2 == 0)
			{
				try
				{
					array = CryptoUtil.HexToBinary(encryptedTicket);
				}
				catch
				{
				}
			}
			if (array == null)
			{
				array = HttpServerUtility.UrlTokenDecode(encryptedTicket);
			}
			if (array == null || array.Length < 1)
			{
				throw new ArgumentException(SR.GetString("InvalidArgumentValue", new object[]
				{
					"encryptedTicket"
				}));
			}
			int num;
			if (AspNetCryptoServiceProvider.Instance.IsDefaultProvider)
			{
				ICryptoService cryptoService = AspNetCryptoServiceProvider.Instance.GetCryptoService(Purpose.FormsAuthentication_Ticket, CryptoServiceOptions.None);
				byte[] array2 = cryptoService.Unprotect(array);
				num = array2.Length;
				array = array2;
			}
			else
			{
				if (FormsAuthentication._Protection == FormsProtectionEnum.All || FormsAuthentication._Protection == FormsProtectionEnum.Encryption)
				{
					array = MachineKeySection.EncryptOrDecryptData(false, array, null, 0, array.Length, false, false, IVType.Random);
					if (array == null)
					{
						return null;
					}
				}
				num = array.Length;
				if (FormsAuthentication._Protection == FormsProtectionEnum.All || FormsAuthentication._Protection == FormsProtectionEnum.Validation)
				{
					if (!MachineKeySection.VerifyHashedData(array))
					{
						return null;
					}
					num -= MachineKeySection.HashSize;
				}
			}
			if (!AppSettings.UseLegacyFormsAuthenticationTicketCompatibility)
			{
				return FormsAuthenticationTicketSerializer.Deserialize(array, num);
			}
			int num2 = (num > 4096) ? 4096 : num;
			StringBuilder stringBuilder = new StringBuilder(num2);
			StringBuilder stringBuilder2 = new StringBuilder(num2);
			StringBuilder stringBuilder3 = new StringBuilder(num2);
			byte[] array3 = new byte[4];
			long[] array4 = new long[2];
			int num3 = UnsafeNativeMethods.CookieAuthParseTicket(array, num, stringBuilder, num2, stringBuilder2, num2, stringBuilder3, num2, array3, array4);
			if (num3 != 0)
			{
				return null;
			}
			DateTime issueDate = DateTime.FromFileTime(array4[0]);
			DateTime expiration = DateTime.FromFileTime(array4[1]);
			return new FormsAuthenticationTicket((int)array3[0], stringBuilder.ToString(), issueDate, expiration, array3[1] > 0, stringBuilder2.ToString(), stringBuilder3.ToString());
		}

		// Token: 0x06004BCD RID: 19405 RVA: 0x001026AC File Offset: 0x001008AC
		public static string Encrypt(FormsAuthenticationTicket ticket)
		{
			return FormsAuthentication.Encrypt(ticket, true);
		}

		// Token: 0x06004BCE RID: 19406 RVA: 0x001026B8 File Offset: 0x001008B8
		internal static string Encrypt(FormsAuthenticationTicket ticket, bool hexEncodedTicket)
		{
			if (ticket == null)
			{
				throw new ArgumentNullException("ticket");
			}
			FormsAuthentication.Initialize();
			byte[] array = FormsAuthentication.MakeTicketIntoBinaryBlob(ticket);
			if (array == null)
			{
				return null;
			}
			if (AspNetCryptoServiceProvider.Instance.IsDefaultProvider)
			{
				ICryptoService cryptoService = AspNetCryptoServiceProvider.Instance.GetCryptoService(Purpose.FormsAuthentication_Ticket, CryptoServiceOptions.None);
				byte[] array2 = cryptoService.Protect(array);
				array = array2;
			}
			else
			{
				if (FormsAuthentication._Protection == FormsProtectionEnum.All || FormsAuthentication._Protection == FormsProtectionEnum.Validation)
				{
					byte[] array3 = MachineKeySection.HashData(array, null, 0, array.Length);
					if (array3 == null)
					{
						return null;
					}
					byte[] array4 = new byte[array3.Length + array.Length];
					Buffer.BlockCopy(array, 0, array4, 0, array.Length);
					Buffer.BlockCopy(array3, 0, array4, array.Length, array3.Length);
					array = array4;
				}
				if (FormsAuthentication._Protection == FormsProtectionEnum.All || FormsAuthentication._Protection == FormsProtectionEnum.Encryption)
				{
					array = MachineKeySection.EncryptOrDecryptData(true, array, null, 0, array.Length, false, false, IVType.Random);
				}
			}
			if (!hexEncodedTicket)
			{
				return HttpServerUtility.UrlTokenEncode(array);
			}
			return CryptoUtil.BinaryToHex(array);
		}

		// Token: 0x06004BCF RID: 19407 RVA: 0x0010278C File Offset: 0x0010098C
		[Obsolete("The recommended alternative is to use the Membership APIs, such as Membership.ValidateUser. For more information, see http://go.microsoft.com/fwlink/?LinkId=252463.")]
		public static bool Authenticate(string name, string password)
		{
			bool flag = FormsAuthentication.InternalAuthenticate(name, password);
			if (flag)
			{
				PerfCounters.IncrementCounter(AppPerfCounter.FORMS_AUTH_SUCCESS);
				WebBaseEvent.RaiseSystemEvent(null, 4001, name);
			}
			else
			{
				PerfCounters.IncrementCounter(AppPerfCounter.FORMS_AUTH_FAIL);
				WebBaseEvent.RaiseSystemEvent(null, 4005, name);
			}
			return flag;
		}

		// Token: 0x06004BD0 RID: 19408 RVA: 0x001027D0 File Offset: 0x001009D0
		private static bool InternalAuthenticate(string name, string password)
		{
			if (name == null || password == null)
			{
				return false;
			}
			FormsAuthentication.Initialize();
			AuthenticationSection authentication = RuntimeConfig.GetAppConfig().Authentication;
			authentication.ValidateAuthenticationMode();
			FormsAuthenticationUserCollection users = authentication.Forms.Credentials.Users;
			if (users == null)
			{
				return false;
			}
			FormsAuthenticationUser formsAuthenticationUser = users[name.ToLower(CultureInfo.InvariantCulture)];
			if (formsAuthenticationUser == null)
			{
				return false;
			}
			string password2 = formsAuthenticationUser.Password;
			if (password2 == null)
			{
				return false;
			}
			string strA;
			switch (authentication.Forms.Credentials.PasswordFormat)
			{
			case FormsAuthPasswordFormat.Clear:
				strA = password;
				break;
			case FormsAuthPasswordFormat.SHA1:
				strA = FormsAuthentication.HashPasswordForStoringInConfigFile(password, "sha1");
				break;
			case FormsAuthPasswordFormat.MD5:
				strA = FormsAuthentication.HashPasswordForStoringInConfigFile(password, "md5");
				break;
			case FormsAuthPasswordFormat.SHA256:
				strA = FormsAuthentication.HashPasswordForStoringInConfigFile(password, "sha256");
				break;
			case FormsAuthPasswordFormat.SHA384:
				strA = FormsAuthentication.HashPasswordForStoringInConfigFile(password, "sha384");
				break;
			case FormsAuthPasswordFormat.SHA512:
				strA = FormsAuthentication.HashPasswordForStoringInConfigFile(password, "sha512");
				break;
			default:
				return false;
			}
			return string.Compare(strA, password2, (authentication.Forms.Credentials.PasswordFormat != FormsAuthPasswordFormat.Clear) ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal) == 0;
		}

		// Token: 0x06004BD1 RID: 19409 RVA: 0x001028DC File Offset: 0x00100ADC
		public static void SignOut()
		{
			FormsAuthentication.Initialize();
			HttpContext httpContext = HttpContext.Current;
			bool flag = httpContext.CookielessHelper.DoesCookieValueExistInOriginal('F');
			httpContext.CookielessHelper.SetCookieValue('F', null);
			if (!CookielessHelperClass.UseCookieless(httpContext, false, FormsAuthentication.CookieMode) || httpContext.Request.Browser.Cookies)
			{
				string value = string.Empty;
				if (httpContext.Request.Browser["supportsEmptyStringInCookieValue"] == "false")
				{
					value = "NoCookie";
				}
				HttpCookie httpCookie = new HttpCookie(FormsAuthentication.FormsCookieName, value);
				httpCookie.HttpOnly = true;
				httpCookie.Path = FormsAuthentication._FormsCookiePath;
				httpCookie.Expires = new DateTime(1999, 10, 12);
				httpCookie.Secure = FormsAuthentication._RequireSSL;
				if (FormsAuthentication._CookieDomain != null)
				{
					httpCookie.Domain = FormsAuthentication._CookieDomain;
				}
				httpCookie.SameSite = FormsAuthentication._cookieSameSite;
				httpContext.Response.Cookies.RemoveCookie(FormsAuthentication.FormsCookieName);
				httpContext.Response.Cookies.Add(httpCookie);
			}
			if (flag)
			{
				httpContext.Response.Redirect(FormsAuthentication.GetLoginPage(null), false);
			}
		}

		// Token: 0x06004BD2 RID: 19410 RVA: 0x001029F5 File Offset: 0x00100BF5
		public static void SetAuthCookie(string userName, bool createPersistentCookie)
		{
			FormsAuthentication.Initialize();
			FormsAuthentication.SetAuthCookie(userName, createPersistentCookie, FormsAuthentication.FormsCookiePath);
		}

		// Token: 0x06004BD3 RID: 19411 RVA: 0x00102A08 File Offset: 0x00100C08
		public static void SetAuthCookie(string userName, bool createPersistentCookie, string strCookiePath)
		{
			FormsAuthentication.Initialize();
			HttpContext httpContext = HttpContext.Current;
			if (!httpContext.Request.IsSecureConnection && FormsAuthentication.RequireSSL)
			{
				throw new HttpException(SR.GetString("Connection_not_secure_creating_secure_cookie"));
			}
			bool flag = CookielessHelperClass.UseCookieless(httpContext, false, FormsAuthentication.CookieMode);
			HttpCookie authCookie = FormsAuthentication.GetAuthCookie(userName, createPersistentCookie, flag ? "/" : strCookiePath, !flag);
			if (!flag)
			{
				HttpContext.Current.Response.Cookies.Add(authCookie);
				httpContext.CookielessHelper.SetCookieValue('F', null);
				return;
			}
			httpContext.CookielessHelper.SetCookieValue('F', authCookie.Value);
		}

		// Token: 0x06004BD4 RID: 19412 RVA: 0x00102AA2 File Offset: 0x00100CA2
		public static HttpCookie GetAuthCookie(string userName, bool createPersistentCookie)
		{
			FormsAuthentication.Initialize();
			return FormsAuthentication.GetAuthCookie(userName, createPersistentCookie, FormsAuthentication.FormsCookiePath);
		}

		// Token: 0x06004BD5 RID: 19413 RVA: 0x00102AB5 File Offset: 0x00100CB5
		public static HttpCookie GetAuthCookie(string userName, bool createPersistentCookie, string strCookiePath)
		{
			return FormsAuthentication.GetAuthCookie(userName, createPersistentCookie, strCookiePath, true);
		}

		// Token: 0x06004BD6 RID: 19414 RVA: 0x00102AC0 File Offset: 0x00100CC0
		private static HttpCookie GetAuthCookie(string userName, bool createPersistentCookie, string strCookiePath, bool hexEncodedTicket)
		{
			FormsAuthentication.Initialize();
			if (userName == null)
			{
				userName = string.Empty;
			}
			if (strCookiePath == null || strCookiePath.Length < 1)
			{
				strCookiePath = FormsAuthentication.FormsCookiePath;
			}
			DateTime utcNow = DateTime.UtcNow;
			DateTime expirationUtc = utcNow.AddMinutes((double)FormsAuthentication._Timeout);
			FormsAuthenticationTicket formsAuthenticationTicket = FormsAuthenticationTicket.FromUtc(2, userName, utcNow, expirationUtc, createPersistentCookie, string.Empty, strCookiePath);
			string text = FormsAuthentication.Encrypt(formsAuthenticationTicket, hexEncodedTicket);
			if (text == null || text.Length < 1)
			{
				throw new HttpException(SR.GetString("Unable_to_encrypt_cookie_ticket"));
			}
			HttpCookie httpCookie = new HttpCookie(FormsAuthentication.FormsCookieName, text);
			httpCookie.HttpOnly = true;
			httpCookie.Path = strCookiePath;
			httpCookie.Secure = FormsAuthentication._RequireSSL;
			if (FormsAuthentication._CookieDomain != null)
			{
				httpCookie.Domain = FormsAuthentication._CookieDomain;
			}
			if (formsAuthenticationTicket.IsPersistent)
			{
				httpCookie.Expires = formsAuthenticationTicket.Expiration;
			}
			httpCookie.SameSite = FormsAuthentication._cookieSameSite;
			return httpCookie;
		}

		// Token: 0x06004BD7 RID: 19415 RVA: 0x00102B98 File Offset: 0x00100D98
		internal static string GetReturnUrl(bool useDefaultIfAbsent)
		{
			FormsAuthentication.Initialize();
			HttpContext httpContext = HttpContext.Current;
			string text = httpContext.Request.QueryString[FormsAuthentication.ReturnUrlVar];
			if (text == null)
			{
				text = httpContext.Request.Form[FormsAuthentication.ReturnUrlVar];
				if (!string.IsNullOrEmpty(text) && !text.Contains("/") && text.Contains("%"))
				{
					text = HttpUtility.UrlDecode(text);
				}
			}
			if (!string.IsNullOrEmpty(text) && !FormsAuthentication.EnableCrossAppRedirects && !UrlPath.IsPathOnSameServer(text, httpContext.Request.Url))
			{
				text = null;
			}
			if (!string.IsNullOrEmpty(text) && CrossSiteScriptingValidation.IsDangerousUrl(text))
			{
				throw new HttpException(SR.GetString("Invalid_redirect_return_url"));
			}
			if (text != null || !useDefaultIfAbsent)
			{
				return text;
			}
			return FormsAuthentication.DefaultUrl;
		}

		// Token: 0x06004BD8 RID: 19416 RVA: 0x00102C5B File Offset: 0x00100E5B
		public static string GetRedirectUrl(string userName, bool createPersistentCookie)
		{
			if (userName == null)
			{
				return null;
			}
			return FormsAuthentication.GetReturnUrl(true);
		}

		// Token: 0x06004BD9 RID: 19417 RVA: 0x00102C68 File Offset: 0x00100E68
		public static void RedirectFromLoginPage(string userName, bool createPersistentCookie)
		{
			FormsAuthentication.Initialize();
			FormsAuthentication.RedirectFromLoginPage(userName, createPersistentCookie, FormsAuthentication.FormsCookiePath);
		}

		// Token: 0x06004BDA RID: 19418 RVA: 0x00102C7C File Offset: 0x00100E7C
		public static void RedirectFromLoginPage(string userName, bool createPersistentCookie, string strCookiePath)
		{
			FormsAuthentication.Initialize();
			if (userName == null)
			{
				return;
			}
			HttpContext httpContext = HttpContext.Current;
			string text = FormsAuthentication.GetReturnUrl(true);
			if (FormsAuthentication.CookiesSupported || FormsAuthentication.IsPathWithinAppRoot(httpContext, text))
			{
				FormsAuthentication.SetAuthCookie(userName, createPersistentCookie, strCookiePath);
				text = FormsAuthentication.RemoveQueryStringVariableFromUrl(text, FormsAuthentication.FormsCookieName);
				if (!FormsAuthentication.CookiesSupported)
				{
					int num = text.IndexOf("://", StringComparison.Ordinal);
					if (num > 0)
					{
						num = text.IndexOf('/', num + 3);
						if (num > 0)
						{
							text = text.Substring(num);
						}
					}
				}
			}
			else
			{
				if (!FormsAuthentication.EnableCrossAppRedirects)
				{
					throw new HttpException(SR.GetString("Can_not_issue_cookie_or_redirect"));
				}
				HttpCookie authCookie = FormsAuthentication.GetAuthCookie(userName, createPersistentCookie, strCookiePath);
				text = FormsAuthentication.RemoveQueryStringVariableFromUrl(text, authCookie.Name);
				if (text.IndexOf('?') > 0)
				{
					text = string.Concat(new string[]
					{
						text,
						"&",
						authCookie.Name,
						"=",
						authCookie.Value
					});
				}
				else
				{
					text = string.Concat(new string[]
					{
						text,
						"?",
						authCookie.Name,
						"=",
						authCookie.Value
					});
				}
			}
			httpContext.Response.Redirect(text, false);
		}

		// Token: 0x06004BDB RID: 19419 RVA: 0x00102DB4 File Offset: 0x00100FB4
		public static FormsAuthenticationTicket RenewTicketIfOld(FormsAuthenticationTicket tOld)
		{
			if (tOld == null)
			{
				return null;
			}
			DateTime utcNow = DateTime.UtcNow;
			TimeSpan t = utcNow - tOld.IssueDateUtc;
			TimeSpan t2 = tOld.ExpirationUtc - utcNow;
			if (t2 > t)
			{
				return tOld;
			}
			TimeSpan t3 = tOld.ExpirationUtc - tOld.IssueDateUtc;
			DateTime expirationUtc = utcNow + t3;
			return FormsAuthenticationTicket.FromUtc(tOld.Version, tOld.Name, utcNow, expirationUtc, tOld.IsPersistent, tOld.UserData, tOld.CookiePath);
		}

		// Token: 0x06004BDC RID: 19420 RVA: 0x00102E38 File Offset: 0x00101038
		public static void EnableFormsAuthentication(NameValueCollection configurationData)
		{
			BuildManager.ThrowIfPreAppStartNotRunning();
			configurationData = (configurationData ?? new NameValueCollection());
			AuthenticationConfig.Mode = AuthenticationMode.Forms;
			FormsAuthentication.Initialize();
			string text = configurationData["defaultUrl"];
			if (!string.IsNullOrEmpty(text))
			{
				FormsAuthentication._DefaultUrl = text;
			}
			string text2 = configurationData["loginUrl"];
			if (!string.IsNullOrEmpty(text2))
			{
				FormsAuthentication._LoginUrl = text2;
			}
		}

		// Token: 0x1700164D RID: 5709
		// (get) Token: 0x06004BDD RID: 19421 RVA: 0x00102E95 File Offset: 0x00101095
		public static bool IsEnabled
		{
			get
			{
				return AuthenticationConfig.Mode == AuthenticationMode.Forms;
			}
		}

		// Token: 0x1700164E RID: 5710
		// (get) Token: 0x06004BDE RID: 19422 RVA: 0x00102E9F File Offset: 0x0010109F
		public static string FormsCookieName
		{
			get
			{
				FormsAuthentication.Initialize();
				return FormsAuthentication._FormsName;
			}
		}

		// Token: 0x1700164F RID: 5711
		// (get) Token: 0x06004BDF RID: 19423 RVA: 0x00102EAB File Offset: 0x001010AB
		public static string FormsCookiePath
		{
			get
			{
				FormsAuthentication.Initialize();
				return FormsAuthentication._FormsCookiePath;
			}
		}

		// Token: 0x17001650 RID: 5712
		// (get) Token: 0x06004BE0 RID: 19424 RVA: 0x00102EB7 File Offset: 0x001010B7
		public static bool RequireSSL
		{
			get
			{
				FormsAuthentication.Initialize();
				return FormsAuthentication._RequireSSL;
			}
		}

		// Token: 0x17001651 RID: 5713
		// (get) Token: 0x06004BE1 RID: 19425 RVA: 0x00102EC3 File Offset: 0x001010C3
		public static TimeSpan Timeout
		{
			get
			{
				FormsAuthentication.Initialize();
				return new TimeSpan(0, FormsAuthentication._Timeout, 0);
			}
		}

		// Token: 0x17001652 RID: 5714
		// (get) Token: 0x06004BE2 RID: 19426 RVA: 0x00102ED6 File Offset: 0x001010D6
		public static bool SlidingExpiration
		{
			get
			{
				FormsAuthentication.Initialize();
				return FormsAuthentication._SlidingExpiration;
			}
		}

		// Token: 0x17001653 RID: 5715
		// (get) Token: 0x06004BE3 RID: 19427 RVA: 0x00102EE2 File Offset: 0x001010E2
		public static HttpCookieMode CookieMode
		{
			get
			{
				FormsAuthentication.Initialize();
				return FormsAuthentication._CookieMode;
			}
		}

		// Token: 0x17001654 RID: 5716
		// (get) Token: 0x06004BE4 RID: 19428 RVA: 0x00102EEE File Offset: 0x001010EE
		public static string CookieDomain
		{
			get
			{
				FormsAuthentication.Initialize();
				return FormsAuthentication._CookieDomain;
			}
		}

		// Token: 0x17001655 RID: 5717
		// (get) Token: 0x06004BE5 RID: 19429 RVA: 0x00102EFA File Offset: 0x001010FA
		public static bool EnableCrossAppRedirects
		{
			get
			{
				FormsAuthentication.Initialize();
				return FormsAuthentication._EnableCrossAppRedirects;
			}
		}

		// Token: 0x17001656 RID: 5718
		// (get) Token: 0x06004BE6 RID: 19430 RVA: 0x00102F06 File Offset: 0x00101106
		public static TicketCompatibilityMode TicketCompatibilityMode
		{
			get
			{
				FormsAuthentication.Initialize();
				return FormsAuthentication._TicketCompatibilityMode;
			}
		}

		// Token: 0x17001657 RID: 5719
		// (get) Token: 0x06004BE7 RID: 19431 RVA: 0x00102F12 File Offset: 0x00101112
		public static SameSiteMode CookieSameSite
		{
			get
			{
				FormsAuthentication.Initialize();
				return FormsAuthentication._cookieSameSite;
			}
		}

		// Token: 0x17001658 RID: 5720
		// (get) Token: 0x06004BE8 RID: 19432 RVA: 0x00102F20 File Offset: 0x00101120
		public static bool CookiesSupported
		{
			get
			{
				HttpContext httpContext = HttpContext.Current;
				return httpContext == null || !CookielessHelperClass.UseCookieless(httpContext, false, FormsAuthentication.CookieMode);
			}
		}

		// Token: 0x17001659 RID: 5721
		// (get) Token: 0x06004BE9 RID: 19433 RVA: 0x00102F48 File Offset: 0x00101148
		public static string LoginUrl
		{
			get
			{
				FormsAuthentication.Initialize();
				HttpContext httpContext = HttpContext.Current;
				if (httpContext != null)
				{
					return AuthenticationConfig.GetCompleteLoginUrl(httpContext, FormsAuthentication._LoginUrl);
				}
				if (FormsAuthentication._LoginUrl.Length == 0 || (FormsAuthentication._LoginUrl[0] != '/' && FormsAuthentication._LoginUrl.IndexOf("//", StringComparison.Ordinal) < 0))
				{
					return "/" + FormsAuthentication._LoginUrl;
				}
				return FormsAuthentication._LoginUrl;
			}
		}

		// Token: 0x1700165A RID: 5722
		// (get) Token: 0x06004BEA RID: 19434 RVA: 0x00102FB4 File Offset: 0x001011B4
		public static string DefaultUrl
		{
			get
			{
				FormsAuthentication.Initialize();
				HttpContext httpContext = HttpContext.Current;
				if (httpContext != null)
				{
					return AuthenticationConfig.GetCompleteLoginUrl(httpContext, FormsAuthentication._DefaultUrl);
				}
				if (FormsAuthentication._DefaultUrl.Length == 0 || (FormsAuthentication._DefaultUrl[0] != '/' && FormsAuthentication._DefaultUrl.IndexOf("//", StringComparison.Ordinal) < 0))
				{
					return "/" + FormsAuthentication._DefaultUrl;
				}
				return FormsAuthentication._DefaultUrl;
			}
		}

		// Token: 0x1700165B RID: 5723
		// (get) Token: 0x06004BEB RID: 19435 RVA: 0x0010301E File Offset: 0x0010121E
		internal static string ReturnUrlVar
		{
			get
			{
				if (!string.IsNullOrEmpty(AppSettings.FormsAuthReturnUrlVar))
				{
					return AppSettings.FormsAuthReturnUrlVar;
				}
				return "ReturnUrl";
			}
		}

		// Token: 0x06004BEC RID: 19436 RVA: 0x00103037 File Offset: 0x00101237
		internal static string GetLoginPage(string extraQueryString)
		{
			return FormsAuthentication.GetLoginPage(extraQueryString, false);
		}

		// Token: 0x06004BED RID: 19437 RVA: 0x00103040 File Offset: 0x00101240
		internal static string GetLoginPage(string extraQueryString, bool reuseReturnUrl)
		{
			HttpContext httpContext = HttpContext.Current;
			string text = FormsAuthentication.LoginUrl;
			if (text.IndexOf('?') >= 0)
			{
				text = FormsAuthentication.RemoveQueryStringVariableFromUrl(text, FormsAuthentication.ReturnUrlVar);
			}
			int num = text.IndexOf('?');
			if (num < 0)
			{
				text += "?";
			}
			else if (num < text.Length - 1)
			{
				text += "&";
			}
			string text2 = null;
			if (reuseReturnUrl)
			{
				text2 = HttpUtility.UrlEncode(FormsAuthentication.GetReturnUrl(false), httpContext.Request.QueryStringEncoding);
			}
			if (text2 == null)
			{
				text2 = HttpUtility.UrlEncode(httpContext.Request.RawUrl, httpContext.Request.ContentEncoding);
			}
			text = text + FormsAuthentication.ReturnUrlVar + "=" + text2;
			if (!string.IsNullOrEmpty(extraQueryString))
			{
				text = text + "&" + extraQueryString;
			}
			return text;
		}

		// Token: 0x06004BEE RID: 19438 RVA: 0x00103105 File Offset: 0x00101305
		public static void RedirectToLoginPage()
		{
			FormsAuthentication.RedirectToLoginPage(null);
		}

		// Token: 0x06004BEF RID: 19439 RVA: 0x00103110 File Offset: 0x00101310
		public static void RedirectToLoginPage(string extraQueryString)
		{
			HttpContext httpContext = HttpContext.Current;
			string loginPage = FormsAuthentication.GetLoginPage(extraQueryString);
			httpContext.Response.Redirect(loginPage, false);
		}

		// Token: 0x06004BF0 RID: 19440 RVA: 0x00103138 File Offset: 0x00101338
		private static byte[] MakeTicketIntoBinaryBlob(FormsAuthenticationTicket ticket)
		{
			if (ticket.Name == null || ticket.UserData == null || ticket.CookiePath == null)
			{
				return null;
			}
			if (!AppSettings.UseLegacyFormsAuthenticationTicketCompatibility)
			{
				return FormsAuthenticationTicketSerializer.Serialize(ticket);
			}
			byte[] array = new byte[4096];
			byte[] array2 = new byte[4];
			long[] array3 = new long[2];
			byte[] array4 = new byte[3];
			bool flag = FormsAuthentication._Protection == FormsProtectionEnum.All || FormsAuthentication._Protection == FormsProtectionEnum.Encryption;
			bool flag2 = !flag || MachineKeySection.CompatMode == MachineKeyCompatibilityMode.Framework20SP1;
			if (flag2)
			{
				byte[] array5 = new byte[8];
				RNGCryptoServiceProvider rngcryptoServiceProvider = new RNGCryptoServiceProvider();
				rngcryptoServiceProvider.GetBytes(array5);
				Buffer.BlockCopy(array5, 0, array, 0, 8);
			}
			array2[0] = (byte)ticket.Version;
			array2[1] = (ticket.IsPersistent ? 1 : 0);
			array3[0] = ticket.IssueDate.ToFileTime();
			array3[1] = ticket.Expiration.ToFileTime();
			int num = UnsafeNativeMethods.CookieAuthConstructTicket(array, array.Length, ticket.Name, ticket.UserData, ticket.CookiePath, array2, array3);
			if (num < 0)
			{
				return null;
			}
			byte[] array6 = new byte[num];
			Buffer.BlockCopy(array, 0, array6, 0, num);
			return array6;
		}

		// Token: 0x06004BF1 RID: 19441 RVA: 0x00103254 File Offset: 0x00101454
		internal static string RemoveQueryStringVariableFromUrl(string strUrl, string QSVar)
		{
			int num = strUrl.IndexOf('?');
			if (num < 0)
			{
				return strUrl;
			}
			string text = "&";
			string text2 = "?";
			string token = text + QSVar + "=";
			FormsAuthentication.RemoveQSVar(ref strUrl, num, token, text, text.Length);
			token = text2 + QSVar + "=";
			FormsAuthentication.RemoveQSVar(ref strUrl, num, token, text, text2.Length);
			text = HttpUtility.UrlEncode("&");
			text2 = HttpUtility.UrlEncode("?");
			token = text + HttpUtility.UrlEncode(QSVar + "=");
			FormsAuthentication.RemoveQSVar(ref strUrl, num, token, text, text.Length);
			token = text2 + HttpUtility.UrlEncode(QSVar + "=");
			FormsAuthentication.RemoveQSVar(ref strUrl, num, token, text, text2.Length);
			return strUrl;
		}

		// Token: 0x06004BF2 RID: 19442 RVA: 0x0010331C File Offset: 0x0010151C
		private static void RemoveQSVar(ref string strUrl, int posQ, string token, string sep, int lenAtStartToLeave)
		{
			for (int i = strUrl.LastIndexOf(token, StringComparison.Ordinal); i >= posQ; i = strUrl.LastIndexOf(token, StringComparison.Ordinal))
			{
				int num = strUrl.IndexOf(sep, i + token.Length, StringComparison.Ordinal) + sep.Length;
				if (num < sep.Length || num >= strUrl.Length)
				{
					strUrl = strUrl.Substring(0, i);
				}
				else
				{
					strUrl = strUrl.Substring(0, i + lenAtStartToLeave) + strUrl.Substring(num);
				}
			}
		}

		// Token: 0x06004BF3 RID: 19443 RVA: 0x00103398 File Offset: 0x00101598
		private static bool IsPathWithinAppRoot(HttpContext context, string path)
		{
			Uri uri;
			if (!Uri.TryCreate(path, UriKind.Absolute, out uri))
			{
				return HttpRuntime.IsPathWithinAppRoot(path);
			}
			return (uri.IsLoopback || string.Equals(context.Request.Url.Host, uri.Host, StringComparison.OrdinalIgnoreCase)) && HttpRuntime.IsPathWithinAppRoot(uri.AbsolutePath);
		}

		// Token: 0x040028D1 RID: 10449
		private const int MAX_TICKET_LENGTH = 4096;

		// Token: 0x040028D2 RID: 10450
		private static object _lockObject = new object();

		// Token: 0x040028D3 RID: 10451
		private const string CONFIG_DEFAULT_COOKIE = ".ASPXAUTH";

		// Token: 0x040028D4 RID: 10452
		private static bool _Initialized;

		// Token: 0x040028D5 RID: 10453
		private static string _FormsName;

		// Token: 0x040028D6 RID: 10454
		private static FormsProtectionEnum _Protection;

		// Token: 0x040028D7 RID: 10455
		private static int _Timeout;

		// Token: 0x040028D8 RID: 10456
		private static string _FormsCookiePath;

		// Token: 0x040028D9 RID: 10457
		private static bool _RequireSSL;

		// Token: 0x040028DA RID: 10458
		private static bool _SlidingExpiration;

		// Token: 0x040028DB RID: 10459
		private static string _LoginUrl;

		// Token: 0x040028DC RID: 10460
		private static string _DefaultUrl;

		// Token: 0x040028DD RID: 10461
		private static HttpCookieMode _CookieMode;

		// Token: 0x040028DE RID: 10462
		private static string _CookieDomain = null;

		// Token: 0x040028DF RID: 10463
		private static bool _EnableCrossAppRedirects;

		// Token: 0x040028E0 RID: 10464
		private static TicketCompatibilityMode _TicketCompatibilityMode;

		// Token: 0x040028E1 RID: 10465
		private static SameSiteMode _cookieSameSite;
	}
}
