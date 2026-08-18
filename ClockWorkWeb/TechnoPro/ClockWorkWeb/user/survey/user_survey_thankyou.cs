using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using TechnoPro.Common.UI.ClientManager.Web.Auth;

namespace TechnoPro.ClockWorkWeb.user.survey
{
	// Token: 0x02000079 RID: 121
	public class user_survey_thankyou : Page
	{
		// Token: 0x06000465 RID: 1125 RVA: 0x00003E0A File Offset: 0x0000200A
		protected void Page_Load(object sender, EventArgs e)
		{
		}

		// Token: 0x06000466 RID: 1126 RVA: 0x00004120 File Offset: 0x00002320
		private void Page_Init(object sender, EventArgs e)
		{
			WebAuthenticationAuthorizationWebClientManager.CurrentInstance.ExemptThisPageFromAuthentication(this.Page, false);
		}

		// Token: 0x0400023E RID: 574
		protected Button btn_home;
	}
}
