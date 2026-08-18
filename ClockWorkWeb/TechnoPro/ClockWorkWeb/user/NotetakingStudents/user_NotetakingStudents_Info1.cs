using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using TechnoPro.Common.ClientManager.Core.Settings;
using TechnoPro.Common.Public.Entities.Settings;
using TechnoPro.Common.UI.ClientManager.Web.Auth;

namespace TechnoPro.ClockWorkWeb.user.NotetakingStudents
{
	// Token: 0x02000094 RID: 148
	public class user_NotetakingStudents_Info1 : Page
	{
		// Token: 0x060004E1 RID: 1249 RVA: 0x00023B4C File Offset: 0x00021D4C
		protected void Page_Load(object sender, EventArgs e)
		{
			bool flag = !this.Page.IsPostBack;
			if (flag)
			{
				this.lbl_notes.Text = new WebSettingsClientManager().GetSettingValue<string>(Setting.NOTETAKINGB_NoSampleNotesInfo);
			}
		}

		// Token: 0x060004E2 RID: 1250 RVA: 0x00004120 File Offset: 0x00002320
		private void Page_Init(object sender, EventArgs e)
		{
			WebAuthenticationAuthorizationWebClientManager.CurrentInstance.ExemptThisPageFromAuthentication(this.Page, false);
		}

		// Token: 0x040002BF RID: 703
		protected ScriptManager bbb;

		// Token: 0x040002C0 RID: 704
		protected Label lbl_notes;
	}
}
