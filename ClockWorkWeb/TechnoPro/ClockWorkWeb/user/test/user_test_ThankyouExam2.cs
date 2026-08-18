using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using ClockWorkWebAPI.TestBooking;
using TechnoPro.Common.ClientManager.Core.Settings;
using TechnoPro.Common.Public.Entities.Settings;
using TechnoPro.Common.UI.ClientManager.Web.Auth;
using TechnoPro.Common.UI.Web.Entity;

namespace TechnoPro.ClockWorkWeb.user.test
{
	// Token: 0x02000072 RID: 114
	public class user_test_ThankyouExam2 : Page
	{
		// Token: 0x0600044C RID: 1100 RVA: 0x0001F944 File Offset: 0x0001DB44
		protected void Page_Load(object sender, EventArgs e)
		{
			bool flag = !this.Page.IsPostBack;
			if (flag)
			{
				bool flag2 = base.Master != null && base.Master is IClockWorkMasterPage;
				if (flag2)
				{
					((IClockWorkMasterPage)base.Master).SetCurrentPage(eClockWorkWebPage.TestBooking_BookExam);
				}
				this.lbl_thankyou.Text = new WebSettingsClientManager().GetSettingValue<string>(Setting.EXAMBOOKING_WizardSetting_FinishedBookingMsg);
				object obj = this.Session["lastbookedtest"];
				bool flag3 = obj != null && obj is BookedTest;
				if (flag3)
				{
					BookedTest bookedTest = (BookedTest)obj;
				}
			}
		}

		// Token: 0x0600044D RID: 1101 RVA: 0x0001F9E3 File Offset: 0x0001DBE3
		protected void btn_again_click(object sender, EventArgs e)
		{
			base.Response.Redirect("bookexam2.aspx", true);
		}

		// Token: 0x0600044E RID: 1102 RVA: 0x00004233 File Offset: 0x00002433
		protected void btn_logout_Click(object sender, EventArgs e)
		{
			WebAuthenticationAuthorizationWebClientManager.CurrentInstance.Logout();
		}

		// Token: 0x04000223 RID: 547
		protected Panel p_main;

		// Token: 0x04000224 RID: 548
		protected Label lbl_thankyou;

		// Token: 0x04000225 RID: 549
		protected Panel p_bookagain;

		// Token: 0x04000226 RID: 550
		protected Label lbl_bookagain;

		// Token: 0x04000227 RID: 551
		protected Button btn_again;

		// Token: 0x04000228 RID: 552
		protected Button btn_logout;
	}
}
