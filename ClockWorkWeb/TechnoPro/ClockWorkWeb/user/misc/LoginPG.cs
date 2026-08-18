using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using ClockWorkLogger;
using TechnoPro.Common.Public.Entities.Authentication.Authentication;
using TechnoPro.Common.UI.ClientManager.Web.Auth;
using TechnoPro.Common.UI.ClientManager.Web.Core.Authentication;
using TechnoPro.Common.UI.ClientManager.Web.Core.Impl.Local.Web;
using TechnoPro.Common.UI.ClientManager.Web.Core.Web;

namespace TechnoPro.ClockWorkWeb.user.misc
{
	// Token: 0x020000BA RID: 186
	public class LoginPG : Page
	{
		// Token: 0x060005A6 RID: 1446 RVA: 0x000246A7 File Offset: 0x000228A7
		protected void Page_Load(object sender, EventArgs e)
		{
			NavigatorClientManager.CurrentInstance.EnsurePageNotCached();
		}

		// Token: 0x060005A7 RID: 1447 RVA: 0x0002A028 File Offset: 0x00028228
		protected void Page_Init(object sender, EventArgs e)
		{
			WebAuthenticationAuthorizationWebClientManager.CurrentInstance.ExemptThisPageFromAuthentication(this.Page, false);
			ISamlAuthWebClientManager samlAuthWebClientManager = new SamlAuthWebClientManager();
			PortalGuardAuthenticationContext portalGuardAuthenticationContext = samlAuthWebClientManager.GetPortalGuardAuthenticationContext();
			bool flag = portalGuardAuthenticationContext == null;
			if (flag)
			{
				this.SendToRegularLoginBecausePortalGuardNotConfigured();
			}
			else
			{
				this.SendRequestToPortalGuard(portalGuardAuthenticationContext);
			}
		}

		// Token: 0x060005A8 RID: 1448 RVA: 0x0002A070 File Offset: 0x00028270
		private void SendRequestToPortalGuard(PortalGuardAuthenticationContext portalGuardAuthenticationContext)
		{
			this.Page.Form.Attributes.Add("action", portalGuardAuthenticationContext.IdpUrl ?? "");
			ISamlAuthWebClientManager samlAuthWebClientManager = new SamlAuthWebClientManager();
			this.SetHiddenValue(this.literal_samlRequest, "SAMLRequest", samlAuthWebClientManager.GenerateRequest(portalGuardAuthenticationContext, true));
			INavigatorClientManager navigatorClientManager = new NavigatorClientManager();
			string text = navigatorClientManager.GetLastReturnUrl("~/user/misc/home.aspx");
			int num = text.IndexOf("login.aspx", StringComparison.OrdinalIgnoreCase);
			bool flag = num >= 0;
			if (flag)
			{
				text = text.Substring(0, num) + "default.aspx";
			}
			CWLogger.Logger.Trace("LoginPG.aspx:SendRequestToPortalGuard:lastReturnUrl={0}", text ?? "NULL");
			this.SetHiddenValue(this.literal_relaystate, "RelayState", text);
		}

		// Token: 0x060005A9 RID: 1449 RVA: 0x0002A134 File Offset: 0x00028334
		private void SetHiddenValue(Literal literalControl, string hiddenVariableName, string value)
		{
			literalControl.Text = string.Concat(new string[]
			{
				"<input type='hidden' name='",
				hiddenVariableName,
				"' value='",
				value,
				"' />"
			});
		}

		// Token: 0x060005AA RID: 1450 RVA: 0x0002A169 File Offset: 0x00028369
		private void SendToRegularLoginBecausePortalGuardNotConfigured()
		{
			CWLogger.Logger.Warn("/user/misc/LoginPG.aspx:Portal Guard authentication is not configured.  Redirecting to home page...");
			base.Response.Redirect("~/custom/misc/home.aspx", true);
		}

		// Token: 0x04000408 RID: 1032
		protected Literal literal_samlRequest;

		// Token: 0x04000409 RID: 1033
		protected Literal literal_relaystate;
	}
}
