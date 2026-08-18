using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using TechnoPro.Common.UI.ClientManager.Web.Auth;

namespace TechnoPro.ClockWorkWeb.user.Forms
{
	// Token: 0x020000EE RID: 238
	public class thankyou : Page
	{
		// Token: 0x060006FA RID: 1786 RVA: 0x00003E0A File Offset: 0x0000200A
		protected void Page_Load(object sender, EventArgs e)
		{
		}

		// Token: 0x060006FB RID: 1787 RVA: 0x00004120 File Offset: 0x00002320
		private void Page_Init(object sender, EventArgs e)
		{
			WebAuthenticationAuthorizationWebClientManager.CurrentInstance.ExemptThisPageFromAuthentication(this.Page, false);
		}

		// Token: 0x04000548 RID: 1352
		protected Button btn_home;
	}
}
