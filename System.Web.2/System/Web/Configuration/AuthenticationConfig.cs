using System;
using System.Web.Util;

namespace System.Web.Configuration
{
	// Token: 0x0200069D RID: 1693
	internal static class AuthenticationConfig
	{
		// Token: 0x1700175B RID: 5979
		// (get) Token: 0x06005164 RID: 20836 RVA: 0x00117F08 File Offset: 0x00116108
		// (set) Token: 0x06005165 RID: 20837 RVA: 0x00117F43 File Offset: 0x00116143
		internal static AuthenticationMode Mode
		{
			get
			{
				if (AuthenticationConfig.s_explicitMode != null)
				{
					return AuthenticationConfig.s_explicitMode.Value;
				}
				AuthenticationSection authentication = RuntimeConfig.GetAppConfig().Authentication;
				authentication.ValidateAuthenticationMode();
				return authentication.Mode;
			}
			set
			{
				AuthenticationConfig.s_explicitMode = new AuthenticationMode?(value);
			}
		}

		// Token: 0x06005166 RID: 20838 RVA: 0x00117F50 File Offset: 0x00116150
		internal static string GetCompleteLoginUrl(HttpContext context, string loginUrl)
		{
			if (string.IsNullOrEmpty(loginUrl))
			{
				return string.Empty;
			}
			if (UrlPath.IsRelativeUrl(loginUrl))
			{
				loginUrl = UrlPath.Combine(HttpRuntime.AppDomainAppVirtualPathString, loginUrl);
			}
			return loginUrl;
		}

		// Token: 0x06005167 RID: 20839 RVA: 0x00117F78 File Offset: 0x00116178
		internal static bool AccessingLoginPage(HttpContext context, string loginUrl)
		{
			if (string.IsNullOrEmpty(loginUrl))
			{
				return false;
			}
			loginUrl = AuthenticationConfig.GetCompleteLoginUrl(context, loginUrl);
			if (string.IsNullOrEmpty(loginUrl))
			{
				return false;
			}
			int num = loginUrl.IndexOf('?');
			if (num >= 0)
			{
				loginUrl = loginUrl.Substring(0, num);
			}
			string path = context.Request.Path;
			if (StringUtil.EqualsIgnoreCase(path, loginUrl))
			{
				return true;
			}
			if (loginUrl.IndexOf('%') >= 0)
			{
				string s = HttpUtility.UrlDecode(loginUrl);
				if (StringUtil.EqualsIgnoreCase(path, s))
				{
					return true;
				}
				s = HttpUtility.UrlDecode(loginUrl, context.Request.ContentEncoding);
				if (StringUtil.EqualsIgnoreCase(path, s))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x04002B06 RID: 11014
		private static AuthenticationMode? s_explicitMode;
	}
}
