using System;
using System.Linq;
using System.Text;
using System.Web;
using TechnoPro.Common.ClientManager.Core.Settings;
using TechnoPro.Common.ClientManager.ICore.Settings;
using TechnoPro.Common.Public.Entities.Adapters;
using TechnoPro.Common.Public.Entities.Authentication.Authentication;
using TechnoPro.Common.Public.Entities.Settings;
using TechnoPro.Common.Security.Saml;
using TechnoPro.Common.UI.ClientManager.Web.Core.Authentication;

namespace TechnoPro.Common.UI.ClientManager.Web.Auth
{
	// Token: 0x02000004 RID: 4
	public class SamlAuthWebClientManager : ISamlAuthWebClientManager
	{
		// Token: 0x0600000E RID: 14 RVA: 0x0000235C File Offset: 0x0000055C
		private static string EncodeToken(string tokenPlainText)
		{
			byte[] bytes = Encoding.UTF8.GetBytes(tokenPlainText);
			return Convert.ToBase64String(bytes);
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002380 File Offset: 0x00000580
		private string GetPortalGuardResultProcessingPageAbsoluteUrl()
		{
			HttpRequest request = HttpContext.Current.Request;
			string str = string.Concat(new string[]
			{
				request.Url.Scheme,
				"://",
				request.Url.Authority,
				request.ApplicationPath.TrimEnd(new char[]
				{
					'/'
				}),
				"/"
			});
			return str + "misc/LoginPG.aspx";
		}

		// Token: 0x06000010 RID: 16 RVA: 0x000023FC File Offset: 0x000005FC
		public PortalGuardAuthenticationContext GetPortalGuardAuthenticationContext()
		{
			IWebSettingsClientManager webSettingsClientManager = new WebSettingsClientManager();
			string settingValue = webSettingsClientManager.GetSettingValue<string>(Setting.LOGIN_AuthenticationContext);
			AuthenticationContext authenticationContextFromXml = settingValue.GetAuthenticationContextFromXml();
			AuthenticationContextItem authenticationContextItem;
			if (authenticationContextFromXml == null)
			{
				authenticationContextItem = null;
			}
			else
			{
				authenticationContextItem = authenticationContextFromXml.ContextItems.FirstOrDefault((AuthenticationContextItem g) => g.ContextItemType == eAuthenticationContextItemType.PortalGuard && !g.IsDisabled);
			}
			AuthenticationContextItem authenticationContextItem2 = authenticationContextItem;
			bool flag = authenticationContextItem2 == null;
			PortalGuardAuthenticationContext result;
			if (flag)
			{
				result = null;
			}
			else
			{
				result = new PortalGuardAuthenticationContext
				{
					TokenIssuer = (authenticationContextItem2.Args.ContainsKey("token_issuer") ? authenticationContextItem2.Args["token_issuer"] : "").GetTokenIssuerFromXml(),
					SamlAssertionConsumerServiceUrl = this.GetPortalGuardResultProcessingPageAbsoluteUrl(),
					SamlRequestIssuer = (authenticationContextItem2.Args.ContainsKey("request_issuer") ? authenticationContextItem2.Args["request_issuer"] : ""),
					IdpUrl = (authenticationContextItem2.Args.ContainsKey("idp_url") ? authenticationContextItem2.Args["idp_url"] : "")
				};
			}
			return result;
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002514 File Offset: 0x00000714
		public string GenerateRequest(PortalGuardAuthenticationContext portalGuardAuthenticationContext, bool encodeAuthRequest)
		{
			AuthRequest authRequest = new AuthRequest(portalGuardAuthenticationContext.SamlAssertionConsumerServiceUrl, portalGuardAuthenticationContext.SamlRequestIssuer);
			string request = authRequest.GetRequest(AuthRequest.AuthRequestFormat.PlainText);
			return encodeAuthRequest ? SamlAuthWebClientManager.EncodeToken(request) : request;
		}

		// Token: 0x06000012 RID: 18 RVA: 0x0000254C File Offset: 0x0000074C
		public string GenerateLogoutRequest(PortalGuardAuthenticationContext portalGuardAuthenticationContext, bool encodeAuthRequest)
		{
			LogoutRequest logoutRequest = new LogoutRequest(portalGuardAuthenticationContext.SamlAssertionConsumerServiceUrl, portalGuardAuthenticationContext.SamlRequestIssuer);
			string request = logoutRequest.GetRequest(LogoutRequest.LogoutRequestFormat.PlainText);
			return encodeAuthRequest ? SamlAuthWebClientManager.EncodeToken(request) : request;
		}
	}
}
