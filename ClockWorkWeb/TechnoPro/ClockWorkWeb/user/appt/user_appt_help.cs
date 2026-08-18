using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using TechnoPro.Common.ClientManager.Core.Settings;
using TechnoPro.Common.Public.Entities.Settings;
using TechnoPro.Common.UI.ClientManager.Web.Auth;
using TechnoPro.Common.UI.Web.Entity;

namespace TechnoPro.ClockWorkWeb.user.appt
{
	// Token: 0x020000F5 RID: 245
	public class user_appt_help : Page
	{
		// Token: 0x0600071F RID: 1823 RVA: 0x000369CC File Offset: 0x00034BCC
		protected void Page_Load(object sender, EventArgs e)
		{
			bool flag = this.Page.Master != null && this.Page.Master is IClockWorkMasterPage;
			if (flag)
			{
				((IClockWorkMasterPage)this.Page.Master).SetCurrentPage(eClockWorkWebPage.AppointmentBooking_FAQ);
			}
			bool flag2 = !this.Page.IsPostBack;
			if (flag2)
			{
				this.lbl_override.Text = new WebSettingsClientManager().GetSettingValue<string>(Setting.APPOINTMENTBOOKING_HelpPageOverrideInfo);
			}
		}

		// Token: 0x06000720 RID: 1824 RVA: 0x00004120 File Offset: 0x00002320
		private void Page_Init(object sender, EventArgs e)
		{
			WebAuthenticationAuthorizationWebClientManager.CurrentInstance.ExemptThisPageFromAuthentication(this.Page, false);
		}

		// Token: 0x04000565 RID: 1381
		protected Label lbl_override;
	}
}
