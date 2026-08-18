using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using ClockWorkWebAPIWeb;
using TechnoPro.Common.ClientManager.Core.Settings;
using TechnoPro.Common.Public.Entities.Settings;
using TechnoPro.Common.UI.ClientManager.Web.Auth;

namespace TechnoPro.ClockWorkWeb.user.NotetakingNotetakers
{
	// Token: 0x020000A5 RID: 165
	public class user_NotetakingNotetakers_Info1 : Page
	{
		// Token: 0x0600052D RID: 1325 RVA: 0x00025FE4 File Offset: 0x000241E4
		protected void Page_Load(object sender, EventArgs e)
		{
			ClockWorkWebCore.DisableNoCache(base.Master);
			bool flag = !this.Page.IsPostBack;
			if (flag)
			{
				string text = new WebSettingsClientManager().GetSettingValue<string>(Setting.NOTETAKINGB_NoSampleNotesInfo).Trim();
				bool flag2 = text.Length < 1;
				if (flag2)
				{
					text = "Please attend your classes normally and submit your first lecture notes as sample notes once they are ready.";
				}
				this.lbl_notes.Text = text;
			}
		}

		// Token: 0x0600052E RID: 1326 RVA: 0x00004120 File Offset: 0x00002320
		private void Page_Init(object sender, EventArgs e)
		{
			WebAuthenticationAuthorizationWebClientManager.CurrentInstance.ExemptThisPageFromAuthentication(this.Page, false);
		}

		// Token: 0x0600052F RID: 1327 RVA: 0x00026047 File Offset: 0x00024247
		protected void btn_courses_OnClick(object sender, EventArgs e)
		{
			base.Response.Redirect("NotetakerApp.aspx", true);
		}

		// Token: 0x04000316 RID: 790
		protected ScriptManager bbb;

		// Token: 0x04000317 RID: 791
		protected Label lbl_notes;

		// Token: 0x04000318 RID: 792
		protected Button btn_courses;
	}
}
