using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using ClockWorkWebAPIWeb;
using TechnoPro.Common.ClientManager.Core.Settings;
using TechnoPro.Common.Public.Entities.Settings;
using TechnoPro.Common.UI.ClientManager.Web.Auth;
using TechnoPro.Common.UI.Web.Entity;

namespace TechnoPro.ClockWorkWeb.user.NotetakingNotetakers
{
	// Token: 0x020000A4 RID: 164
	public class user_NotetakingNotetakers_help : Page
	{
		// Token: 0x0600052A RID: 1322 RVA: 0x00025F58 File Offset: 0x00024158
		protected void Page_Load(object sender, EventArgs e)
		{
			ClockWorkWebCore.DisableNoCache(base.Master);
			bool flag = this.Page.Master != null && this.Page.Master is IClockWorkMasterPage;
			if (flag)
			{
				((IClockWorkMasterPage)this.Page.Master).SetCurrentPage(eClockWorkWebPage.NotetakingNotetakers_FAQ);
			}
			bool flag2 = !this.Page.IsPostBack;
			if (flag2)
			{
				string settingValue = new WebSettingsClientManager().GetSettingValue<string>(Setting.NOTETAKINGB_Faq);
				this.lbl_faq.Text = settingValue;
			}
		}

		// Token: 0x0600052B RID: 1323 RVA: 0x00004120 File Offset: 0x00002320
		private void Page_Init(object sender, EventArgs e)
		{
			WebAuthenticationAuthorizationWebClientManager.CurrentInstance.ExemptThisPageFromAuthentication(this.Page, false);
		}

		// Token: 0x04000313 RID: 787
		protected ScriptManager bbb;

		// Token: 0x04000314 RID: 788
		protected Panel p_help;

		// Token: 0x04000315 RID: 789
		protected Label lbl_faq;
	}
}
