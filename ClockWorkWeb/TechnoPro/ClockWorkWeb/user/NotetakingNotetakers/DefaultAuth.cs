using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using ClockWorkWebAPIWeb;
using TechnoPro.Common.Core.Settings;
using TechnoPro.Common.Public.Entities.Settings;
using TechnoPro.Common.UI.Web.Entity;

namespace TechnoPro.ClockWorkWeb.user.NotetakingNotetakers
{
	// Token: 0x020000A1 RID: 161
	public class DefaultAuth : Page
	{
		// Token: 0x0600051F RID: 1311 RVA: 0x00025820 File Offset: 0x00023A20
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

		// Token: 0x06000520 RID: 1312 RVA: 0x00003E0A File Offset: 0x0000200A
		private void Page_Init(object sender, EventArgs e)
		{
		}

		// Token: 0x04000301 RID: 769
		protected ScriptManager bbb;

		// Token: 0x04000302 RID: 770
		protected Label lbl_msg;
	}
}
