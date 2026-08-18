using System;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Security.Principal;
using System.Text;
using System.Web.Util;

namespace System.Web.Security
{
	// Token: 0x020005EE RID: 1518
	[Obsolete("This type is obsolete. The Passport authentication product is no longer supported and has been superseded by Live ID.")]
	public sealed class PassportIdentity : IIdentity, IDisposable
	{
		// Token: 0x1700168B RID: 5771
		// (get) Token: 0x06004C7B RID: 19579 RVA: 0x00105360 File Offset: 0x00103560
		internal bool WWWAuthHeaderSet
		{
			get
			{
				return this._WWWAuthHeaderSet;
			}
		}

		// Token: 0x06004C7C RID: 19580 RVA: 0x00105368 File Offset: 0x00103568
		[SecurityPermission(SecurityAction.Demand, UnmanagedCode = true)]
		public PassportIdentity()
		{
			HttpContext httpContext = HttpContext.Current;
			if (PassportIdentity._iPassportVer == 0)
			{
				PassportIdentity._iPassportVer = UnsafeNativeMethods.PassportVersion();
			}
			if (PassportIdentity._iPassportVer < 3)
			{
				string szQueryStrT = httpContext.Request.QueryString["t"];
				string szQueryStrP = httpContext.Request.QueryString["p"];
				HttpCookie httpCookie = httpContext.Request.Cookies["MSPAuth"];
				HttpCookie httpCookie2 = httpContext.Request.Cookies["MSPProf"];
				HttpCookie httpCookie3 = httpContext.Request.Cookies["MSPProfC"];
				string text = (httpCookie != null && httpCookie.Value != null) ? httpCookie.Value : string.Empty;
				string text2 = (httpCookie2 != null && httpCookie2.Value != null) ? httpCookie2.Value : string.Empty;
				string text3 = (httpCookie3 != null && httpCookie3.Value != null) ? httpCookie3.Value : string.Empty;
				StringBuilder stringBuilder = new StringBuilder(1028);
				StringBuilder stringBuilder2 = new StringBuilder(1028);
				text = HttpUtility.UrlDecode(text);
				text2 = HttpUtility.UrlDecode(text2);
				text3 = HttpUtility.UrlDecode(text3);
				int errorCode = UnsafeNativeMethods.PassportCreate(szQueryStrT, szQueryStrP, text, text2, text3, stringBuilder, stringBuilder2, 1024, ref this._iPassport);
				if (this._iPassport == IntPtr.Zero)
				{
					throw new COMException(SR.GetString("Could_not_create_passport_identity"), errorCode);
				}
				string text4 = PassportIdentity.UrlEncodeCookie(stringBuilder.ToString());
				string text5 = PassportIdentity.UrlEncodeCookie(stringBuilder2.ToString());
				if (text4.Length > 1)
				{
					httpContext.Response.AppendHeader("Set-Cookie", text4);
				}
				if (text5.Length > 1)
				{
					httpContext.Response.AppendHeader("Set-Cookie", text5);
				}
			}
			else
			{
				string szRequestLine = string.Concat(new string[]
				{
					httpContext.Request.HttpMethod,
					" ",
					httpContext.Request.RawUrl,
					" ",
					httpContext.Request.ServerVariables["SERVER_PROTOCOL"],
					"\r\n"
				});
				StringBuilder stringBuilder3 = new StringBuilder(4092);
				int errorCode2 = UnsafeNativeMethods.PassportCreateHttpRaw(szRequestLine, httpContext.Request.ServerVariables["ALL_RAW"], httpContext.Request.IsSecureConnection ? 1 : 0, stringBuilder3, 4090, ref this._iPassport);
				if (this._iPassport == IntPtr.Zero)
				{
					throw new COMException(SR.GetString("Could_not_create_passport_identity"), errorCode2);
				}
				string strResponseHeaders = stringBuilder3.ToString();
				this.SetHeaders(httpContext, strResponseHeaders);
			}
			this._Authenticated = this.GetIsAuthenticated(-1, -1, -1);
			if (!this._Authenticated)
			{
				this._Name = string.Empty;
			}
		}

		// Token: 0x06004C7D RID: 19581 RVA: 0x0010562C File Offset: 0x0010382C
		private void SetHeaders(HttpContext context, string strResponseHeaders)
		{
			int num;
			for (int i = 0; i < strResponseHeaders.Length; i = num + 2)
			{
				num = strResponseHeaders.IndexOf('\r', i);
				if (num < 0)
				{
					num = strResponseHeaders.Length;
				}
				string text = strResponseHeaders.Substring(i, num - i);
				int num2 = text.IndexOf(':');
				if (num2 > 0)
				{
					string name = text.Substring(0, num2);
					string value = text.Substring(num2 + 1);
					context.Response.AppendHeader(name, value);
				}
			}
		}

		// Token: 0x06004C7E RID: 19582 RVA: 0x0010569C File Offset: 0x0010389C
		~PassportIdentity()
		{
			UnsafeNativeMethods.PassportDestroy(this._iPassport);
			this._iPassport = IntPtr.Zero;
		}

		// Token: 0x06004C7F RID: 19583 RVA: 0x001056D8 File Offset: 0x001038D8
		private static string UrlEncodeCookie(string strIn)
		{
			if (strIn == null || strIn.Length < 1)
			{
				return string.Empty;
			}
			int num = strIn.IndexOf('=');
			if (num < 0)
			{
				return HttpUtility.AspCompatUrlEncode(strIn);
			}
			num++;
			int num2 = strIn.IndexOf(';', num);
			if (num2 < 0)
			{
				return HttpUtility.AspCompatUrlEncode(strIn);
			}
			string str = strIn.Substring(0, num);
			string s = strIn.Substring(num, num2 - num);
			string str2 = strIn.Substring(num2, strIn.Length - num2);
			return str + HttpUtility.AspCompatUrlEncode(s) + str2;
		}

		// Token: 0x1700168C RID: 5772
		// (get) Token: 0x06004C80 RID: 19584 RVA: 0x00105758 File Offset: 0x00103958
		public string Name
		{
			get
			{
				if (this._Name == null)
				{
					if (PassportIdentity._iPassportVer >= 3)
					{
						this._Name = this.HexPUID;
					}
					else if (this.HasProfile("core"))
					{
						this._Name = int.Parse(this["MemberIDHigh"], CultureInfo.InvariantCulture).ToString("X8", CultureInfo.InvariantCulture) + int.Parse(this["MemberIDLow"], CultureInfo.InvariantCulture).ToString("X8", CultureInfo.InvariantCulture);
					}
					else
					{
						this._Name = string.Empty;
					}
				}
				return this._Name;
			}
		}

		// Token: 0x1700168D RID: 5773
		// (get) Token: 0x06004C81 RID: 19585 RVA: 0x001057FF File Offset: 0x001039FF
		public string AuthenticationType
		{
			get
			{
				return "Passport";
			}
		}

		// Token: 0x1700168E RID: 5774
		// (get) Token: 0x06004C82 RID: 19586 RVA: 0x00105806 File Offset: 0x00103A06
		public bool IsAuthenticated
		{
			get
			{
				return this._Authenticated;
			}
		}

		// Token: 0x1700168F RID: 5775
		public string this[string strProfileName]
		{
			get
			{
				object profileObject = this.GetProfileObject(strProfileName);
				if (profileObject == null)
				{
					return string.Empty;
				}
				if (profileObject is string)
				{
					return (string)profileObject;
				}
				return profileObject.ToString();
			}
		}

		// Token: 0x06004C84 RID: 19588 RVA: 0x00105843 File Offset: 0x00103A43
		public bool GetIsAuthenticated(int iTimeWindow, bool bForceLogin, bool bCheckSecure)
		{
			return this.GetIsAuthenticated(iTimeWindow, bForceLogin ? 1 : 0, bCheckSecure ? 10 : 0);
		}

		// Token: 0x06004C85 RID: 19589 RVA: 0x0010585C File Offset: 0x00103A5C
		public bool GetIsAuthenticated(int iTimeWindow, int iForceLogin, int iCheckSecure)
		{
			int num = UnsafeNativeMethods.PassportIsAuthenticated(this._iPassport, iTimeWindow, iForceLogin, iCheckSecure);
			if (num < 0)
			{
				throw new COMException(SR.GetString("Passport_method_failed"), num);
			}
			return num == 0;
		}

		// Token: 0x06004C86 RID: 19590 RVA: 0x00105894 File Offset: 0x00103A94
		public object GetProfileObject(string strProfileName)
		{
			object result = new object();
			int num = UnsafeNativeMethods.PassportGetProfile(this._iPassport, strProfileName, out result);
			if (num < 0)
			{
				throw new COMException(SR.GetString("Passport_method_failed"), num);
			}
			return result;
		}

		// Token: 0x17001690 RID: 5776
		// (get) Token: 0x06004C87 RID: 19591 RVA: 0x001058CC File Offset: 0x00103ACC
		public int Error
		{
			get
			{
				return UnsafeNativeMethods.PassportGetError(this._iPassport);
			}
		}

		// Token: 0x17001691 RID: 5777
		// (get) Token: 0x06004C88 RID: 19592 RVA: 0x001058DC File Offset: 0x00103ADC
		public bool GetFromNetworkServer
		{
			get
			{
				int num = UnsafeNativeMethods.PassportGetFromNetworkServer(this._iPassport);
				if (num < 0)
				{
					throw new COMException(SR.GetString("Passport_method_failed"), num);
				}
				return num == 0;
			}
		}

		// Token: 0x06004C89 RID: 19593 RVA: 0x00105910 File Offset: 0x00103B10
		public string GetDomainFromMemberName(string strMemberName)
		{
			StringBuilder stringBuilder = new StringBuilder(1028);
			int num = UnsafeNativeMethods.PassportDomainFromMemberName(this._iPassport, strMemberName, stringBuilder, 1024);
			if (num < 0)
			{
				throw new COMException(SR.GetString("Passport_method_failed"), num);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06004C8A RID: 19594 RVA: 0x00105958 File Offset: 0x00103B58
		public bool HasProfile(string strProfile)
		{
			int num = UnsafeNativeMethods.PassportHasProfile(this._iPassport, strProfile);
			if (num < 0)
			{
				throw new COMException(SR.GetString("Passport_method_failed"), num);
			}
			return num == 0;
		}

		// Token: 0x06004C8B RID: 19595 RVA: 0x0010598C File Offset: 0x00103B8C
		public bool HasFlag(int iFlagMask)
		{
			int num = UnsafeNativeMethods.PassportHasFlag(this._iPassport, iFlagMask);
			if (num < 0)
			{
				throw new COMException(SR.GetString("Passport_method_failed"), num);
			}
			return num == 0;
		}

		// Token: 0x06004C8C RID: 19596 RVA: 0x001059C0 File Offset: 0x00103BC0
		public bool HaveConsent(bool bNeedFullConsent, bool bNeedBirthdate)
		{
			int num = UnsafeNativeMethods.PassportHasConsent(this._iPassport, bNeedFullConsent ? 1 : 0, bNeedBirthdate ? 1 : 0);
			if (num < 0)
			{
				throw new COMException(SR.GetString("Passport_method_failed"), num);
			}
			return num == 0;
		}

		// Token: 0x06004C8D RID: 19597 RVA: 0x00105A00 File Offset: 0x00103C00
		public object GetOption(string strOpt)
		{
			object result = new object();
			int num = UnsafeNativeMethods.PassportGetOption(this._iPassport, strOpt, out result);
			if (num < 0)
			{
				throw new COMException(SR.GetString("Passport_method_failed"), num);
			}
			return result;
		}

		// Token: 0x06004C8E RID: 19598 RVA: 0x00105A38 File Offset: 0x00103C38
		public void SetOption(string strOpt, object vOpt)
		{
			int num = UnsafeNativeMethods.PassportSetOption(this._iPassport, strOpt, vOpt);
			if (num < 0)
			{
				throw new COMException(SR.GetString("Passport_method_failed"), num);
			}
		}

		// Token: 0x06004C8F RID: 19599 RVA: 0x00105A68 File Offset: 0x00103C68
		public string LogoutURL()
		{
			return this.LogoutURL(null, null, -1, null, -1);
		}

		// Token: 0x06004C90 RID: 19600 RVA: 0x00105A78 File Offset: 0x00103C78
		public string LogoutURL(string szReturnURL, string szCOBrandArgs, int iLangID, string strDomain, int iUseSecureAuth)
		{
			StringBuilder stringBuilder = new StringBuilder(4096);
			int num = UnsafeNativeMethods.PassportLogoutURL(this._iPassport, szReturnURL, szCOBrandArgs, iLangID, strDomain, iUseSecureAuth, stringBuilder, 4096);
			if (num < 0)
			{
				throw new COMException(SR.GetString("Passport_method_failed"), num);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x17001692 RID: 5778
		// (get) Token: 0x06004C91 RID: 19601 RVA: 0x00105AC4 File Offset: 0x00103CC4
		public bool HasSavedPassword
		{
			get
			{
				int num = UnsafeNativeMethods.PassportGetHasSavedPassword(this._iPassport);
				if (num < 0)
				{
					throw new COMException(SR.GetString("Passport_method_failed"), num);
				}
				return num == 0;
			}
		}

		// Token: 0x17001693 RID: 5779
		// (get) Token: 0x06004C92 RID: 19602 RVA: 0x00105AF8 File Offset: 0x00103CF8
		public bool HasTicket
		{
			get
			{
				int num = UnsafeNativeMethods.PassportHasTicket(this._iPassport);
				if (num < 0)
				{
					throw new COMException(SR.GetString("Passport_method_failed"), num);
				}
				return num == 0;
			}
		}

		// Token: 0x17001694 RID: 5780
		// (get) Token: 0x06004C93 RID: 19603 RVA: 0x00105B2C File Offset: 0x00103D2C
		public int TicketAge
		{
			get
			{
				int num = UnsafeNativeMethods.PassportGetTicketAge(this._iPassport);
				if (num < 0)
				{
					throw new COMException(SR.GetString("Passport_method_failed"), num);
				}
				return num;
			}
		}

		// Token: 0x17001695 RID: 5781
		// (get) Token: 0x06004C94 RID: 19604 RVA: 0x00105B5C File Offset: 0x00103D5C
		public int TimeSinceSignIn
		{
			get
			{
				int num = UnsafeNativeMethods.PassportGetTimeSinceSignIn(this._iPassport);
				if (num < 0)
				{
					throw new COMException(SR.GetString("Passport_method_failed"), num);
				}
				return num;
			}
		}

		// Token: 0x06004C95 RID: 19605 RVA: 0x00105B8C File Offset: 0x00103D8C
		public string LogoTag()
		{
			return this.LogoTag(null, -1, -1, null, -1, -1, null, -1, -1);
		}

		// Token: 0x06004C96 RID: 19606 RVA: 0x00105BA8 File Offset: 0x00103DA8
		public string LogoTag(string strReturnUrl)
		{
			return this.LogoTag(strReturnUrl, -1, -1, null, -1, -1, null, -1, -1);
		}

		// Token: 0x06004C97 RID: 19607 RVA: 0x00105BC4 File Offset: 0x00103DC4
		public string LogoTag2()
		{
			return this.LogoTag2(null, -1, -1, null, -1, -1, null, -1, -1);
		}

		// Token: 0x06004C98 RID: 19608 RVA: 0x00105BE0 File Offset: 0x00103DE0
		public string LogoTag2(string strReturnUrl)
		{
			return this.LogoTag2(strReturnUrl, -1, -1, null, -1, -1, null, -1, -1);
		}

		// Token: 0x06004C99 RID: 19609 RVA: 0x00105BFC File Offset: 0x00103DFC
		public string LogoTag(string strReturnUrl, int iTimeWindow, bool fForceLogin, string strCoBrandedArgs, int iLangID, bool fSecure, string strNameSpace, int iKPP, bool bUseSecureAuth)
		{
			return this.LogoTag(strReturnUrl, iTimeWindow, fForceLogin ? 1 : 0, strCoBrandedArgs, iLangID, fSecure ? 1 : 0, strNameSpace, iKPP, bUseSecureAuth ? 10 : 0);
		}

		// Token: 0x06004C9A RID: 19610 RVA: 0x00105C34 File Offset: 0x00103E34
		public string LogoTag(string strReturnUrl, int iTimeWindow, int iForceLogin, string strCoBrandedArgs, int iLangID, int iSecure, string strNameSpace, int iKPP, int iUseSecureAuth)
		{
			StringBuilder stringBuilder = new StringBuilder(4092);
			int num = UnsafeNativeMethods.PassportLogoTag(this._iPassport, strReturnUrl, iTimeWindow, iForceLogin, strCoBrandedArgs, iLangID, iSecure, strNameSpace, iKPP, iUseSecureAuth, stringBuilder, 4090);
			if (num < 0)
			{
				throw new COMException(SR.GetString("Passport_method_failed"), num);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06004C9B RID: 19611 RVA: 0x00105C88 File Offset: 0x00103E88
		public string LogoTag2(string strReturnUrl, int iTimeWindow, bool fForceLogin, string strCoBrandedArgs, int iLangID, bool fSecure, string strNameSpace, int iKPP, bool bUseSecureAuth)
		{
			return this.LogoTag2(strReturnUrl, iTimeWindow, fForceLogin ? 1 : 0, strCoBrandedArgs, iLangID, fSecure ? 1 : 0, strNameSpace, iKPP, bUseSecureAuth ? 10 : 0);
		}

		// Token: 0x06004C9C RID: 19612 RVA: 0x00105CC0 File Offset: 0x00103EC0
		public string LogoTag2(string strReturnUrl, int iTimeWindow, int iForceLogin, string strCoBrandedArgs, int iLangID, int iSecure, string strNameSpace, int iKPP, int iUseSecureAuth)
		{
			StringBuilder stringBuilder = new StringBuilder(4092);
			int num = UnsafeNativeMethods.PassportLogoTag2(this._iPassport, strReturnUrl, iTimeWindow, iForceLogin, strCoBrandedArgs, iLangID, iSecure, strNameSpace, iKPP, iUseSecureAuth, stringBuilder, 4090);
			if (num < 0)
			{
				throw new COMException(SR.GetString("Passport_method_failed"), num);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06004C9D RID: 19613 RVA: 0x00105D14 File Offset: 0x00103F14
		public string AuthUrl()
		{
			return this.AuthUrl(null, -1, -1, null, -1, null, -1, -1);
		}

		// Token: 0x06004C9E RID: 19614 RVA: 0x00105D30 File Offset: 0x00103F30
		public string AuthUrl(string strReturnUrl)
		{
			return this.AuthUrl(strReturnUrl, -1, -1, null, -1, null, -1, -1);
		}

		// Token: 0x06004C9F RID: 19615 RVA: 0x00105D4C File Offset: 0x00103F4C
		public string AuthUrl2()
		{
			return this.AuthUrl2(null, -1, -1, null, -1, null, -1, -1);
		}

		// Token: 0x06004CA0 RID: 19616 RVA: 0x00105D68 File Offset: 0x00103F68
		public string AuthUrl2(string strReturnUrl)
		{
			return this.AuthUrl2(strReturnUrl, -1, -1, null, -1, null, -1, -1);
		}

		// Token: 0x06004CA1 RID: 19617 RVA: 0x00105D84 File Offset: 0x00103F84
		public string AuthUrl(string strReturnUrl, int iTimeWindow, bool fForceLogin, string strCoBrandedArgs, int iLangID, string strNameSpace, int iKPP, bool bUseSecureAuth)
		{
			StringBuilder stringBuilder = new StringBuilder(4092);
			int num = UnsafeNativeMethods.PassportAuthURL(this._iPassport, strReturnUrl, iTimeWindow, fForceLogin ? 1 : 0, strCoBrandedArgs, iLangID, strNameSpace, iKPP, bUseSecureAuth ? 10 : 0, stringBuilder, 4090);
			if (num < 0)
			{
				throw new COMException(SR.GetString("Passport_method_failed"), num);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06004CA2 RID: 19618 RVA: 0x00105DE4 File Offset: 0x00103FE4
		public string AuthUrl2(string strReturnUrl, int iTimeWindow, bool fForceLogin, string strCoBrandedArgs, int iLangID, string strNameSpace, int iKPP, bool bUseSecureAuth)
		{
			StringBuilder stringBuilder = new StringBuilder(4092);
			int num = UnsafeNativeMethods.PassportAuthURL2(this._iPassport, strReturnUrl, iTimeWindow, fForceLogin ? 1 : 0, strCoBrandedArgs, iLangID, strNameSpace, iKPP, bUseSecureAuth ? 10 : 0, stringBuilder, 4090);
			if (num < 0)
			{
				throw new COMException(SR.GetString("Passport_method_failed"), num);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06004CA3 RID: 19619 RVA: 0x00105E44 File Offset: 0x00104044
		public string AuthUrl(string strReturnUrl, int iTimeWindow, int iForceLogin, string strCoBrandedArgs, int iLangID, string strNameSpace, int iKPP, int iUseSecureAuth)
		{
			StringBuilder stringBuilder = new StringBuilder(4092);
			int num = UnsafeNativeMethods.PassportAuthURL(this._iPassport, strReturnUrl, iTimeWindow, iForceLogin, strCoBrandedArgs, iLangID, strNameSpace, iKPP, iUseSecureAuth, stringBuilder, 4090);
			if (num < 0)
			{
				throw new COMException(SR.GetString("Passport_method_failed"), num);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06004CA4 RID: 19620 RVA: 0x00105E98 File Offset: 0x00104098
		public string AuthUrl2(string strReturnUrl, int iTimeWindow, int iForceLogin, string strCoBrandedArgs, int iLangID, string strNameSpace, int iKPP, int iUseSecureAuth)
		{
			StringBuilder stringBuilder = new StringBuilder(4092);
			int num = UnsafeNativeMethods.PassportAuthURL2(this._iPassport, strReturnUrl, iTimeWindow, iForceLogin, strCoBrandedArgs, iLangID, strNameSpace, iKPP, iUseSecureAuth, stringBuilder, 4090);
			if (num < 0)
			{
				throw new COMException(SR.GetString("Passport_method_failed"), num);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06004CA5 RID: 19621 RVA: 0x00105EEC File Offset: 0x001040EC
		public int LoginUser(string szRetURL, int iTimeWindow, bool fForceLogin, string szCOBrandArgs, int iLangID, string strNameSpace, int iKPP, bool fUseSecureAuth, object oExtraParams)
		{
			return this.LoginUser(szRetURL, iTimeWindow, fForceLogin ? 1 : 0, szCOBrandArgs, iLangID, strNameSpace, iKPP, fUseSecureAuth ? 10 : 0, oExtraParams);
		}

		// Token: 0x06004CA6 RID: 19622 RVA: 0x00105F1C File Offset: 0x0010411C
		public int LoginUser(string szRetURL, int iTimeWindow, int fForceLogin, string szCOBrandArgs, int iLangID, string strNameSpace, int iKPP, int iUseSecureAuth, object oExtraParams)
		{
			string text = this.GetLoginChallenge(szRetURL, iTimeWindow, fForceLogin, szCOBrandArgs, iLangID, strNameSpace, iKPP, iUseSecureAuth, oExtraParams);
			if (text == null || text.Length < 1)
			{
				return -1;
			}
			HttpContext httpContext = HttpContext.Current;
			this.SetHeaders(httpContext, text);
			this._WWWAuthHeaderSet = true;
			text = httpContext.Request.Headers["Accept-Auth"];
			if (text != null && text.Length > 0 && text.IndexOf("Passport", StringComparison.Ordinal) >= 0)
			{
				httpContext.Response.StatusCode = 401;
				httpContext.Response.End();
				return 0;
			}
			text = this.AuthUrl(szRetURL, iTimeWindow, fForceLogin, szCOBrandArgs, iLangID, strNameSpace, iKPP, iUseSecureAuth);
			if (!string.IsNullOrEmpty(text))
			{
				httpContext.Response.Redirect(text, false);
				return 0;
			}
			return -1;
		}

		// Token: 0x06004CA7 RID: 19623 RVA: 0x00105FDC File Offset: 0x001041DC
		public int LoginUser()
		{
			return this.LoginUser(null, -1, -1, null, -1, null, -1, -1, null);
		}

		// Token: 0x06004CA8 RID: 19624 RVA: 0x00105FF8 File Offset: 0x001041F8
		public int LoginUser(string strReturnUrl)
		{
			return this.LoginUser(strReturnUrl, -1, -1, null, -1, null, -1, -1, null);
		}

		// Token: 0x06004CA9 RID: 19625 RVA: 0x00106014 File Offset: 0x00104214
		public string GetLoginChallenge()
		{
			return this.GetLoginChallenge(null, -1, -1, null, -1, null, -1, -1, null);
		}

		// Token: 0x06004CAA RID: 19626 RVA: 0x00106030 File Offset: 0x00104230
		public string GetLoginChallenge(string strReturnUrl)
		{
			return this.GetLoginChallenge(strReturnUrl, -1, -1, null, -1, null, -1, -1, null);
		}

		// Token: 0x06004CAB RID: 19627 RVA: 0x0010604C File Offset: 0x0010424C
		public string GetLoginChallenge(string szRetURL, int iTimeWindow, int fForceLogin, string szCOBrandArgs, int iLangID, string strNameSpace, int iKPP, int iUseSecureAuth, object oExtraParams)
		{
			StringBuilder stringBuilder = new StringBuilder(4092);
			int num = UnsafeNativeMethods.PassportGetLoginChallenge(this._iPassport, szRetURL, iTimeWindow, fForceLogin, szCOBrandArgs, iLangID, strNameSpace, iKPP, iUseSecureAuth, oExtraParams, stringBuilder, 4090);
			if (num < 0)
			{
				throw new COMException(SR.GetString("Passport_method_failed"), num);
			}
			string text = stringBuilder.ToString();
			if (text != null && !StringUtil.StringStartsWith(text, "WWW-Authenticate"))
			{
				text = "WWW-Authenticate: " + text;
			}
			return text;
		}

		// Token: 0x06004CAC RID: 19628 RVA: 0x001060C0 File Offset: 0x001042C0
		public string GetDomainAttribute(string strAttribute, int iLCID, string strDomain)
		{
			StringBuilder stringBuilder = new StringBuilder(1028);
			int num = UnsafeNativeMethods.PassportGetDomainAttribute(this._iPassport, strAttribute, iLCID, strDomain, stringBuilder, 1024);
			if (num >= 0)
			{
				return stringBuilder.ToString();
			}
			throw new COMException(SR.GetString("Passport_method_failed"), num);
		}

		// Token: 0x06004CAD RID: 19629 RVA: 0x00106108 File Offset: 0x00104308
		public object Ticket(string strAttribute)
		{
			object result = new object();
			int num = UnsafeNativeMethods.PassportTicket(this._iPassport, strAttribute, out result);
			if (num < 0)
			{
				throw new COMException(SR.GetString("Passport_method_failed"), num);
			}
			return result;
		}

		// Token: 0x06004CAE RID: 19630 RVA: 0x00106140 File Offset: 0x00104340
		public object GetCurrentConfig(string strAttribute)
		{
			object result = new object();
			int num = UnsafeNativeMethods.PassportGetCurrentConfig(this._iPassport, strAttribute, out result);
			if (num < 0)
			{
				throw new COMException(SR.GetString("Passport_method_failed"), num);
			}
			return result;
		}

		// Token: 0x17001696 RID: 5782
		// (get) Token: 0x06004CAF RID: 19631 RVA: 0x00106178 File Offset: 0x00104378
		public string HexPUID
		{
			get
			{
				StringBuilder stringBuilder = new StringBuilder(1024);
				int num = UnsafeNativeMethods.PassportHexPUID(this._iPassport, stringBuilder, 1024);
				if (num >= 0)
				{
					return stringBuilder.ToString();
				}
				throw new COMException(SR.GetString("Passport_method_failed"), num);
			}
		}

		// Token: 0x06004CB0 RID: 19632 RVA: 0x001061BD File Offset: 0x001043BD
		void IDisposable.Dispose()
		{
			if (this._iPassport != IntPtr.Zero)
			{
				UnsafeNativeMethods.PassportDestroy(this._iPassport);
			}
			this._iPassport = IntPtr.Zero;
			GC.SuppressFinalize(this);
		}

		// Token: 0x06004CB1 RID: 19633 RVA: 0x001061F0 File Offset: 0x001043F0
		public static void SignOut(string strSignOutDotGifFileName)
		{
			HttpContext httpContext = HttpContext.Current;
			string[] array = new string[]
			{
				"MSPAuth",
				"MSPProf",
				"MSPConsent",
				"MSPSecAuth",
				"MSPProfC"
			};
			string[] array2 = new string[]
			{
				"TicketDomain",
				"TicketDomain",
				"ProfileDomain",
				"SecureDomain",
				"TicketDomain"
			};
			string[] array3 = new string[]
			{
				"TicketPath",
				"TicketPath",
				"ProfilePath",
				"SecurePath",
				"TicketPath"
			};
			string[] array4 = new string[5];
			string[] array5 = new string[5];
			httpContext.Response.ClearHeaders();
			try
			{
				PassportIdentity passportIdentity;
				if (httpContext.User.Identity is PassportIdentity)
				{
					passportIdentity = (PassportIdentity)httpContext.User.Identity;
				}
				else
				{
					passportIdentity = new PassportIdentity();
				}
				if (passportIdentity != null && PassportIdentity._iPassportVer >= 3)
				{
					for (int i = 0; i < 5; i++)
					{
						object currentConfig = passportIdentity.GetCurrentConfig(array2[i]);
						if (currentConfig != null && currentConfig is string)
						{
							array4[i] = (string)currentConfig;
						}
					}
					for (int i = 0; i < 5; i++)
					{
						object currentConfig2 = passportIdentity.GetCurrentConfig(array3[i]);
						if (currentConfig2 != null && currentConfig2 is string)
						{
							array5[i] = (string)currentConfig2;
						}
					}
				}
			}
			catch
			{
			}
			for (int i = 0; i < 5; i++)
			{
				HttpCookie httpCookie = new HttpCookie(array[i], string.Empty);
				httpCookie.Expires = new DateTime(1998, 1, 1);
				if (array4[i] != null && array4[i].Length > 0)
				{
					httpCookie.Domain = array4[i];
				}
				if (array5[i] != null && array5[i].Length > 0)
				{
					httpCookie.Path = array5[i];
				}
				else
				{
					httpCookie.Path = "/";
				}
				httpContext.Response.Cookies.Add(httpCookie);
			}
			httpContext.Response.Expires = -1;
			httpContext.Response.Cache.SetCacheability(HttpCacheability.NoCache);
			httpContext.Response.AppendHeader("Pragma", "no-cache");
			httpContext.Response.ContentType = "image/gif";
			httpContext.Response.WriteFile(strSignOutDotGifFileName);
			string text = httpContext.Request.QueryString["ru"];
			if (text != null && text.Length > 1)
			{
				httpContext.Response.Redirect(text, false);
			}
		}

		// Token: 0x06004CB2 RID: 19634 RVA: 0x00106488 File Offset: 0x00104688
		public static string Encrypt(string strData)
		{
			return PassportIdentity.CallPassportCryptFunction(0, strData);
		}

		// Token: 0x06004CB3 RID: 19635 RVA: 0x00106491 File Offset: 0x00104691
		public static string Decrypt(string strData)
		{
			return PassportIdentity.CallPassportCryptFunction(1, strData);
		}

		// Token: 0x06004CB4 RID: 19636 RVA: 0x0010649A File Offset: 0x0010469A
		public static string Compress(string strData)
		{
			return PassportIdentity.CallPassportCryptFunction(2, strData);
		}

		// Token: 0x06004CB5 RID: 19637 RVA: 0x001064A3 File Offset: 0x001046A3
		public static string Decompress(string strData)
		{
			return PassportIdentity.CallPassportCryptFunction(3, strData);
		}

		// Token: 0x06004CB6 RID: 19638 RVA: 0x001064AC File Offset: 0x001046AC
		public static int CryptPutHost(string strHost)
		{
			int num = UnsafeNativeMethods.PassportCryptPut(0, strHost);
			if (num < 0)
			{
				throw new COMException(SR.GetString("Passport_method_failed"), num);
			}
			return num;
		}

		// Token: 0x06004CB7 RID: 19639 RVA: 0x001064D8 File Offset: 0x001046D8
		public static int CryptPutSite(string strSite)
		{
			int num = UnsafeNativeMethods.PassportCryptPut(1, strSite);
			if (num < 0)
			{
				throw new COMException(SR.GetString("Passport_method_failed"), num);
			}
			return num;
		}

		// Token: 0x06004CB8 RID: 19640 RVA: 0x00106504 File Offset: 0x00104704
		public static bool CryptIsValid()
		{
			int num = UnsafeNativeMethods.PassportCryptIsValid();
			if (num < 0)
			{
				throw new COMException(SR.GetString("Passport_method_failed"), num);
			}
			return num == 0;
		}

		// Token: 0x06004CB9 RID: 19641 RVA: 0x00106530 File Offset: 0x00104730
		private static string CallPassportCryptFunction(int iFunctionID, string strData)
		{
			int num = (strData == null || strData.Length < 512) ? 512 : strData.Length;
			StringBuilder stringBuilder;
			int num2;
			for (;;)
			{
				num *= 2;
				stringBuilder = new StringBuilder(num);
				num2 = UnsafeNativeMethods.PassportCrypt(iFunctionID, strData, stringBuilder, num);
				if (num2 == 0)
				{
					break;
				}
				if (num2 != -2147024774 && num2 < 0)
				{
					goto Block_5;
				}
				if (num2 != -2147024774 || num >= 10485760)
				{
					goto IL_6C;
				}
			}
			return stringBuilder.ToString();
			Block_5:
			throw new COMException(SR.GetString("Passport_method_failed"), num2);
			IL_6C:
			return null;
		}

		// Token: 0x0400290D RID: 10509
		private string _Name;

		// Token: 0x0400290E RID: 10510
		private bool _Authenticated;

		// Token: 0x0400290F RID: 10511
		private IntPtr _iPassport;

		// Token: 0x04002910 RID: 10512
		private static int _iPassportVer;

		// Token: 0x04002911 RID: 10513
		private bool _WWWAuthHeaderSet;
	}
}
