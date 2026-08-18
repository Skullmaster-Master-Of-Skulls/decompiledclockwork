using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using TechnoPro.Common.ClientManager.Core.Settings;
using TechnoPro.Common.Public.Entities.Settings;
using TechnoPro.Common.UI.ClientManager.Web.Auth;
using TechnoPro.Common.UI.Web.Entity;

namespace TechnoPro.ClockWorkWeb.user.TutoringTutors
{
	// Token: 0x02000042 RID: 66
	public class user_TutoringTutors_Default : Page
	{
		// Token: 0x060001A2 RID: 418 RVA: 0x0000B678 File Offset: 0x00009878
		protected void Page_Load(object sender, EventArgs e)
		{
			bool flag = base.Master != null && base.Master is IClockWorkMasterPage;
			if (flag)
			{
				((IClockWorkMasterPage)base.Master).SetCurrentPage(eClockWorkWebPage.TutoringTutors_Help);
			}
			bool flag2 = !this.Page.IsPostBack;
			if (flag2)
			{
				this.lbl_info.Text = new WebSettingsClientManager().GetSettingValue<string>(Setting.TUTORING_TutorHelpText);
			}
		}

		// Token: 0x060001A3 RID: 419 RVA: 0x00004120 File Offset: 0x00002320
		private void Page_Init(object sender, EventArgs e)
		{
			WebAuthenticationAuthorizationWebClientManager.CurrentInstance.ExemptThisPageFromAuthentication(this.Page, false);
		}

		// Token: 0x04000142 RID: 322
		protected Label lbl_info;
	}
}
