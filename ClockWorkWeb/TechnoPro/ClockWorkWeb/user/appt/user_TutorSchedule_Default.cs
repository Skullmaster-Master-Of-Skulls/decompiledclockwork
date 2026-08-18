using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using TechnoPro.Common.ClientManager.Core.Settings;
using TechnoPro.Common.Public.Entities.Settings;
using TechnoPro.Common.UI.ClientManager.Web.Auth;
using TechnoPro.Common.UI.Web.Entity;

namespace TechnoPro.ClockWorkWeb.user.appt
{
	// Token: 0x020000F4 RID: 244
	public class user_TutorSchedule_Default : Page
	{
		// Token: 0x0600071C RID: 1820 RVA: 0x00036948 File Offset: 0x00034B48
		protected void Page_Load(object sender, EventArgs e)
		{
			bool flag = this.Page.Master != null && this.Page.Master is IClockWorkMasterPage;
			if (flag)
			{
				((IClockWorkMasterPage)this.Page.Master).SetCurrentPage(eClockWorkWebPage.AppointmentBooking_Help);
			}
			bool flag2 = !this.Page.IsPostBack;
			if (flag2)
			{
				this.lbl_intro.Text = new WebSettingsClientManager().GetSettingValue<string>(Setting.APPOINTMENTBOOKING_info);
			}
		}

		// Token: 0x0600071D RID: 1821 RVA: 0x00004120 File Offset: 0x00002320
		private void Page_Init(object sender, EventArgs e)
		{
			WebAuthenticationAuthorizationWebClientManager.CurrentInstance.ExemptThisPageFromAuthentication(this.Page, false);
		}

		// Token: 0x04000564 RID: 1380
		protected Label lbl_intro;
	}
}
