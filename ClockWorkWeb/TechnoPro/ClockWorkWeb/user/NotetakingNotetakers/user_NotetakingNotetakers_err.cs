using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using TechnoPro.Common.UI.ClientManager.Web.Auth;

namespace TechnoPro.ClockWorkWeb.user.NotetakingNotetakers
{
	// Token: 0x020000A3 RID: 163
	public class user_NotetakingNotetakers_err : Page
	{
		// Token: 0x06000527 RID: 1319 RVA: 0x00003E0A File Offset: 0x0000200A
		protected void Page_Load(object sender, EventArgs e)
		{
		}

		// Token: 0x06000528 RID: 1320 RVA: 0x00004233 File Offset: 0x00002433
		protected void btn_logout_Click(object sender, EventArgs e)
		{
			WebAuthenticationAuthorizationWebClientManager.CurrentInstance.Logout();
		}

		// Token: 0x04000311 RID: 785
		protected Button btn_tryAgain;

		// Token: 0x04000312 RID: 786
		protected Button btn_logout;
	}
}
