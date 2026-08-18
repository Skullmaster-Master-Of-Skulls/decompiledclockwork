using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using TechnoPro.Common.ClientManager.Core.Settings;
using TechnoPro.Common.Public.Entities.Settings;
using TechnoPro.Common.UI.ClientManager.Web.Auth;
using TechnoPro.Common.UI.Web.Entity;

namespace TechnoPro.ClockWorkWeb.user.workshop2
{
	// Token: 0x02000020 RID: 32
	public class user_workshop2_Default : Page
	{
		// Token: 0x060000C5 RID: 197 RVA: 0x00005A50 File Offset: 0x00003C50
		protected void Page_Load(object sender, EventArgs e)
		{
			bool flag = this.Page.Master != null && this.Page.Master is IClockWorkMasterPage;
			if (flag)
			{
				((IClockWorkMasterPage)this.Page.Master).SetCurrentPage(eClockWorkWebPage.WorkshopBooking_Help);
			}
			bool flag2 = !this.Page.IsPostBack;
			if (flag2)
			{
				this.lbl_intro.Text = new WebSettingsClientManager().GetSettingValue<string>(Setting.WORKSHOPS_WelcomeMessage);
			}
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x00004120 File Offset: 0x00002320
		private void Page_Init(object sender, EventArgs e)
		{
			WebAuthenticationAuthorizationWebClientManager.CurrentInstance.ExemptThisPageFromAuthentication(this.Page, false);
		}

		// Token: 0x04000083 RID: 131
		protected Label lbl_intro;
	}
}
