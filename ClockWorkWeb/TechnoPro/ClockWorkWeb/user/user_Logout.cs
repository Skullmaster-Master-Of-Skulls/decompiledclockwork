using System;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using TechnoPro.Common.UI.ClientManager.Web.Auth;

namespace TechnoPro.ClockWorkWeb.user
{
	// Token: 0x0200001A RID: 26
	public class user_Logout : Page
	{
		// Token: 0x06000089 RID: 137 RVA: 0x00004233 File Offset: 0x00002433
		protected void Page_Load(object sender, EventArgs e)
		{
			WebAuthenticationAuthorizationWebClientManager.CurrentInstance.Logout();
		}

		// Token: 0x0600008A RID: 138 RVA: 0x00004120 File Offset: 0x00002320
		private void Page_Init(object sender, EventArgs e)
		{
			WebAuthenticationAuthorizationWebClientManager.CurrentInstance.ExemptThisPageFromAuthentication(this.Page, false);
		}

		// Token: 0x04000032 RID: 50
		protected HtmlForm form1;
	}
}
