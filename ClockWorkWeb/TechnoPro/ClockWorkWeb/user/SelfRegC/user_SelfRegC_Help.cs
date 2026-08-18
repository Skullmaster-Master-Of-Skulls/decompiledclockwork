using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using TechnoPro.Common.ClientManager.Core.Settings;
using TechnoPro.Common.Public.Entities.Settings;
using TechnoPro.Common.UI.ClientManager.Web.Auth;
using TechnoPro.Common.UI.Web.Entity;

namespace TechnoPro.ClockWorkWeb.user.SelfRegC
{
	// Token: 0x02000086 RID: 134
	public class user_SelfRegC_Help : Page
	{
		// Token: 0x06000494 RID: 1172 RVA: 0x00021144 File Offset: 0x0001F344
		protected void Page_Load(object sender, EventArgs e)
		{
			bool flag = this.Page.Master != null && this.Page.Master is IClockWorkMasterPage;
			if (flag)
			{
				((IClockWorkMasterPage)this.Page.Master).SetCurrentPage(eClockWorkWebPage.SelfRegistration_FAQ);
			}
			bool flag2 = !this.Page.IsPostBack;
			if (flag2)
			{
				this.lbl_info.Text = new WebSettingsClientManager().GetSettingValue<string>(Setting.SELFREGC_HelpText);
			}
		}

		// Token: 0x06000495 RID: 1173 RVA: 0x00004120 File Offset: 0x00002320
		private void Page_Init(object sender, EventArgs e)
		{
			WebAuthenticationAuthorizationWebClientManager.CurrentInstance.ExemptThisPageFromAuthentication(this.Page, false);
		}

		// Token: 0x04000276 RID: 630
		protected Panel p_title;

		// Token: 0x04000277 RID: 631
		protected Label lbl_title;

		// Token: 0x04000278 RID: 632
		protected Label lbl_info;
	}
}
