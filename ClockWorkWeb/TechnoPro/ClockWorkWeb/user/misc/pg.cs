using System;
using System.Web.UI;
using TechnoPro.ClockWorkServer.Contracts.DTO.Authentication.Authentication;
using TechnoPro.Common.Public.Entities.Authentication.Authentication;
using TechnoPro.Common.UI.ClientManager.Web.Auth;
using TechnoPro.Common.UI.ClientManager.Web.Core.Authentication;
using TechnoPro.Common.UI.ClientManager.Web.Core.Impl.Local.Web;
using TechnoPro.Common.UI.ClientManager.Web.Core.Web;
using TechnoPro.Common.UI.Web.Entity.AuthenticationAuthorization;

namespace TechnoPro.ClockWorkWeb.user.misc
{
	// Token: 0x020000C3 RID: 195
	public class pg : Page
	{
		// Token: 0x060005C7 RID: 1479 RVA: 0x0002A900 File Offset: 0x00028B00
		protected void Page_Load(object sender, EventArgs e)
		{
			NavigatorClientManager.CurrentInstance.EnsurePageNotCached();
			ISamlAuthWebClientManager samlAuthWebClientManager = new SamlAuthWebClientManager();
			PortalGuardAuthenticationContext portalGuardAuthenticationContext = samlAuthWebClientManager.GetPortalGuardAuthenticationContext();
			bool flag = portalGuardAuthenticationContext == null;
			if (!flag)
			{
				IWebAuthenticationAuthorizationWebClientManager webAuthenticationAuthorizationWebClientManager = new WebAuthenticationAuthorizationWebClientManager();
				AuthenticationArgsDTO environmentVariables = webAuthenticationAuthorizationWebClientManager.GetEnvironmentVariables();
				ClockWorkIdentity clockWorkIdentity = webAuthenticationAuthorizationWebClientManager.TryToLoginRightNowWithoutCredentials(environmentVariables);
				bool flag2 = clockWorkIdentity != null;
				if (flag2)
				{
					this.GotoRelayState(base.Request.Form["RelayState"] ?? "");
				}
				else
				{
					this.GotoHomePage();
				}
			}
		}

		// Token: 0x060005C8 RID: 1480 RVA: 0x00004120 File Offset: 0x00002320
		private void Page_Init(object sender, EventArgs e)
		{
			WebAuthenticationAuthorizationWebClientManager.CurrentInstance.ExemptThisPageFromAuthentication(this.Page, false);
		}

		// Token: 0x060005C9 RID: 1481 RVA: 0x0002A984 File Offset: 0x00028B84
		private void GotoRelayState(string relayState)
		{
			bool flag = string.IsNullOrEmpty(relayState);
			if (flag)
			{
				this.GotoHomePage();
			}
			else
			{
				base.Response.Redirect(relayState, true);
			}
		}

		// Token: 0x060005CA RID: 1482 RVA: 0x0002A9B4 File Offset: 0x00028BB4
		private void GotoHomePage()
		{
			INavigatorClientManager navigatorClientManager = new NavigatorClientManager();
			navigatorClientManager.GotoHomePage();
		}
	}
}
