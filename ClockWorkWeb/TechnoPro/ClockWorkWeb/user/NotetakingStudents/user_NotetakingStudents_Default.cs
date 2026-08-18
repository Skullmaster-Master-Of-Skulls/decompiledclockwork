using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using TechnoPro.Common.ClientManager.Core.Settings;
using TechnoPro.Common.Public.Entities.Settings;
using TechnoPro.Common.UI.ClientManager.Web.Auth;
using TechnoPro.Common.UI.Web.Entity;

namespace TechnoPro.ClockWorkWeb.user.NotetakingStudents
{
	// Token: 0x02000090 RID: 144
	public class user_NotetakingStudents_Default : Page
	{
		// Token: 0x060004C9 RID: 1225 RVA: 0x00023414 File Offset: 0x00021614
		protected void Page_Load(object sender, EventArgs e)
		{
			bool flag = this.Page.Master != null && this.Page.Master is IClockWorkMasterPage;
			if (flag)
			{
				((IClockWorkMasterPage)this.Page.Master).SetCurrentPage(eClockWorkWebPage.NotetakingStudents_Help);
			}
			bool flag2 = !this.Page.IsPostBack;
			if (flag2)
			{
				this.lbl_msg.Text = new WebSettingsClientManager().GetSettingValue<string>(Setting.NOTETAKINGB_welcomeMsgStudents);
			}
		}

		// Token: 0x060004CA RID: 1226 RVA: 0x00004120 File Offset: 0x00002320
		private void Page_Init(object sender, EventArgs e)
		{
			WebAuthenticationAuthorizationWebClientManager.CurrentInstance.ExemptThisPageFromAuthentication(this.Page, false);
		}

		// Token: 0x0400029F RID: 671
		protected ScriptManager bbb;

		// Token: 0x040002A0 RID: 672
		protected Label lbl_msg;
	}
}
