using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using TechnoPro.ClockWorkWeb.ctrls;
using TechnoPro.Common.UI.ClientManager.Web.Auth;

namespace TechnoPro.ClockWorkWeb.user.student
{
	// Token: 0x02000081 RID: 129
	public class Login : Page
	{
		// Token: 0x0600047C RID: 1148 RVA: 0x00003E0A File Offset: 0x0000200A
		protected void Page_Load(object sender, EventArgs e)
		{
		}

		// Token: 0x0600047D RID: 1149 RVA: 0x00005AEE File Offset: 0x00003CEE
		private void Page_Init(object sender, EventArgs e)
		{
			WebAuthenticationAuthorizationWebClientManager.CurrentInstance.ExemptThisPageFromAuthentication(this.Page, true);
		}

		// Token: 0x04000258 RID: 600
		protected Panel p_title;

		// Token: 0x04000259 RID: 601
		protected Label lbl_title;

		// Token: 0x0400025A RID: 602
		protected ClockWorkLoginControl cwLogin1;
	}
}
