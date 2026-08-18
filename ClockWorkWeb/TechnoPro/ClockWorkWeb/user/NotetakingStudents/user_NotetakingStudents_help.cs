using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using TechnoPro.Common.ClientManager.Core.Settings;
using TechnoPro.Common.Public.Entities.Settings;
using TechnoPro.Common.UI.ClientManager.Web.Auth;
using TechnoPro.Common.UI.Web.Entity;

namespace TechnoPro.ClockWorkWeb.user.NotetakingStudents
{
	// Token: 0x02000093 RID: 147
	public class user_NotetakingStudents_help : Page
	{
		// Token: 0x060004DE RID: 1246 RVA: 0x00023AD0 File Offset: 0x00021CD0
		protected void Page_Load(object sender, EventArgs e)
		{
			bool flag = this.Page.Master != null && this.Page.Master is IClockWorkMasterPage;
			if (flag)
			{
				((IClockWorkMasterPage)this.Page.Master).SetCurrentPage(eClockWorkWebPage.NotetakingStudents_FAQ);
			}
			bool flag2 = !this.Page.IsPostBack;
			if (flag2)
			{
				this.lbl_faq.Text = new WebSettingsClientManager().GetSettingValue<string>(Setting.NOTETAKINGB_StudentsFaq);
			}
		}

		// Token: 0x060004DF RID: 1247 RVA: 0x00004120 File Offset: 0x00002320
		private void Page_Init(object sender, EventArgs e)
		{
			WebAuthenticationAuthorizationWebClientManager.CurrentInstance.ExemptThisPageFromAuthentication(this.Page, false);
		}

		// Token: 0x040002BC RID: 700
		protected ScriptManager bbb;

		// Token: 0x040002BD RID: 701
		protected Panel p_help;

		// Token: 0x040002BE RID: 702
		protected Label lbl_faq;
	}
}
