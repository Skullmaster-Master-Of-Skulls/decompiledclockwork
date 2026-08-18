using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using TechnoPro.Common.ClientManager.Core.Settings;
using TechnoPro.Common.ClientManager.ICore.Settings;
using TechnoPro.Common.Public.Entities.Settings;
using TechnoPro.Common.UI.ClientManager.Web.Auth;
using TechnoPro.Common.UI.Web.Entity;

namespace TechnoPro.ClockWorkWeb.user.SelfRegC
{
	// Token: 0x02000085 RID: 133
	public class user_SelfRegC_Default : Page
	{
		// Token: 0x06000491 RID: 1169 RVA: 0x000210C4 File Offset: 0x0001F2C4
		protected void Page_Load(object sender, EventArgs e)
		{
			bool flag = this.Page.Master != null && this.Page.Master is IClockWorkMasterPage;
			if (flag)
			{
				((IClockWorkMasterPage)this.Page.Master).SetCurrentPage(eClockWorkWebPage.SelfRegistration_Help);
			}
			bool flag2 = !this.Page.IsPostBack;
			if (flag2)
			{
				IWebSettingsClientManager webSettingsClientManager = new WebSettingsClientManager();
				this.lbl_info.Text = webSettingsClientManager.GetSettingValue<string>(Setting.SELFREGC_InfoText);
			}
		}

		// Token: 0x06000492 RID: 1170 RVA: 0x00004120 File Offset: 0x00002320
		private void Page_Init(object sender, EventArgs e)
		{
			WebAuthenticationAuthorizationWebClientManager.CurrentInstance.ExemptThisPageFromAuthentication(this.Page, false);
		}

		// Token: 0x0400026F RID: 623
		protected Panel p_title;

		// Token: 0x04000270 RID: 624
		protected Label lbl_title;

		// Token: 0x04000271 RID: 625
		protected HyperLink link_help;

		// Token: 0x04000272 RID: 626
		protected Panel p_msg;

		// Token: 0x04000273 RID: 627
		protected Label lbl_msg;

		// Token: 0x04000274 RID: 628
		protected Panel p_info;

		// Token: 0x04000275 RID: 629
		protected Label lbl_info;
	}
}
