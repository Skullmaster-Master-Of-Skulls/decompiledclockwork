using System;
using System.Web.UI;
using TechnoPro.ClockWorkWeb.ctrls;
using TechnoPro.Common.UI.ClientManager.Web.Auth;

namespace TechnoPro.ClockWorkWeb.user.misc
{
	// Token: 0x020000B9 RID: 185
	public class user_misc_Login : Page
	{
		// Token: 0x060005A3 RID: 1443 RVA: 0x00003E0A File Offset: 0x0000200A
		protected void Page_Load(object sender, EventArgs e)
		{
		}

		// Token: 0x060005A4 RID: 1444 RVA: 0x00005AEE File Offset: 0x00003CEE
		private void Page_Init(object sender, EventArgs e)
		{
			WebAuthenticationAuthorizationWebClientManager.CurrentInstance.ExemptThisPageFromAuthentication(this.Page, true);
		}

		// Token: 0x04000407 RID: 1031
		protected ClockWorkLoginControl ClockWorkLoginControl1;
	}
}
