using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using ClockWorkWebAPIWeb;
using TechnoPro.Common.Core.Settings;
using TechnoPro.Common.Public.Entities.Settings;
using TechnoPro.Common.UI.ClientManager.Web.Auth;
using TechnoPro.Common.UI.Web.Entity;

namespace TechnoPro.ClockWorkWeb.user.NotetakingNotetakers
{
	// Token: 0x020000A0 RID: 160
	public class user_NotetakingNotetakers_Default : Page
	{
		// Token: 0x0600051C RID: 1308 RVA: 0x00025798 File Offset: 0x00023998
		protected void Page_Load(object sender, EventArgs e)
		{
			ClockWorkWebCore.DisableNoCache(base.Master);
			bool flag = this.Page.Master != null && this.Page.Master is IClockWorkMasterPage;
			if (flag)
			{
				((IClockWorkMasterPage)this.Page.Master).SetCurrentPage(eClockWorkWebPage.NotetakingNotetakers_Help);
			}
			bool flag2 = !this.Page.IsPostBack;
			if (flag2)
			{
				this.lbl_msg.Text = SettingManager.GetInstance().GetSettingValue<string>(Setting.NOTETAKINGB_welcomeMsg);
			}
		}

		// Token: 0x0600051D RID: 1309 RVA: 0x00004120 File Offset: 0x00002320
		private void Page_Init(object sender, EventArgs e)
		{
			WebAuthenticationAuthorizationWebClientManager.CurrentInstance.ExemptThisPageFromAuthentication(this.Page, false);
		}

		// Token: 0x040002FF RID: 767
		protected ScriptManager bbb;

		// Token: 0x04000300 RID: 768
		protected Label lbl_msg;
	}
}
