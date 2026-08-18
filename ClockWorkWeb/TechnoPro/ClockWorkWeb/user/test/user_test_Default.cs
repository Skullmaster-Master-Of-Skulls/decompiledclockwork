using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using TechnoPro.Common.ClientManager.Core.Settings;
using TechnoPro.Common.Public.Entities.Settings;
using TechnoPro.Common.UI.ClientManager.Web.Auth;
using TechnoPro.Common.UI.Web.Entity;

namespace TechnoPro.ClockWorkWeb.user.test
{
	// Token: 0x02000069 RID: 105
	public class user_test_Default : Page
	{
		// Token: 0x06000425 RID: 1061 RVA: 0x0001EEBC File Offset: 0x0001D0BC
		protected void Page_Load(object sender, EventArgs e)
		{
			bool flag = !this.Page.IsPostBack;
			if (flag)
			{
				bool flag2 = base.Master != null && base.Master is IClockWorkMasterPage;
				if (flag2)
				{
					((IClockWorkMasterPage)base.Master).SetCurrentPage(eClockWorkWebPage.TestBooking_Help);
				}
				string text = new WebSettingsClientManager().GetSettingValue<string>(Setting.TESTBOOKING_Info) ?? "";
				bool flag3 = text.Length > 0;
				if (flag3)
				{
					this.lbl_info.Text = text;
				}
				else
				{
					base.Response.Redirect("AccommodationsLetters.aspx", true);
				}
			}
		}

		// Token: 0x06000426 RID: 1062 RVA: 0x00004120 File Offset: 0x00002320
		private void Page_Init(object sender, EventArgs e)
		{
			WebAuthenticationAuthorizationWebClientManager.CurrentInstance.ExemptThisPageFromAuthentication(this.Page, false);
		}

		// Token: 0x06000427 RID: 1063 RVA: 0x00003E0A File Offset: 0x0000200A
		protected void btn_fake_Click(object sender, EventArgs e)
		{
		}

		// Token: 0x0400020B RID: 523
		protected Panel p_info;

		// Token: 0x0400020C RID: 524
		protected Label lbl_info;
	}
}
