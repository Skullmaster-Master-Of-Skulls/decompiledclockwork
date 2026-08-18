using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using TechnoPro.Common.UI.ClientManager.Web.Auth;

namespace TechnoPro.ClockWorkWeb.user.misc
{
	// Token: 0x020000BC RID: 188
	public class user_Logout2 : Page
	{
		// Token: 0x060005B0 RID: 1456 RVA: 0x0002A364 File Offset: 0x00028564
		protected void Page_Load(object sender, EventArgs e)
		{
			bool flag = !this.Page.IsPostBack;
			if (flag)
			{
				WebAuthenticationAuthorizationWebClientManager.CurrentInstance.Logout();
			}
		}

		// Token: 0x060005B1 RID: 1457 RVA: 0x00004120 File Offset: 0x00002320
		private void Page_Init(object sender, EventArgs e)
		{
			WebAuthenticationAuthorizationWebClientManager.CurrentInstance.ExemptThisPageFromAuthentication(this.Page, false);
		}

		// Token: 0x0400040B RID: 1035
		protected HyperLink link_home;
	}
}
