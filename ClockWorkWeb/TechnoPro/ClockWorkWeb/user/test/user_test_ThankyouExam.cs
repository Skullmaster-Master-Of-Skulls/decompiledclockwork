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
	// Token: 0x02000071 RID: 113
	public class user_test_ThankyouExam : Page
	{
		// Token: 0x06000448 RID: 1096 RVA: 0x0001F890 File Offset: 0x0001DA90
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

		// Token: 0x06000449 RID: 1097 RVA: 0x0001F92F File Offset: 0x0001DB2F
		protected void btn_again_click(object sender, EventArgs e)
		{
			base.Response.Redirect("bookexam.aspx", true);
		}

		// Token: 0x0600044A RID: 1098 RVA: 0x00004233 File Offset: 0x00002433
		protected void btn_logout_Click(object sender, EventArgs e)
		{
			WebAuthenticationAuthorizationWebClientManager.CurrentInstance.Logout();
		}

		// Token: 0x0400021E RID: 542
		protected Label lbl_thankyou;

		// Token: 0x0400021F RID: 543
		protected Panel p_bookagain;

		// Token: 0x04000220 RID: 544
		protected Label lbl_bookagain;

		// Token: 0x04000221 RID: 545
		protected Button btn_again;

		// Token: 0x04000222 RID: 546
		protected Button btn_logout;
	}
}
