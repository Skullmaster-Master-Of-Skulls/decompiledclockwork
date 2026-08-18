using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using TechnoPro.ClockWorkWeb.ctrls;
using TechnoPro.Common.ClientManager.Core.Settings;
using TechnoPro.Common.Public.Entities.Settings;
using TechnoPro.Common.UI.ClientManager.Web.Auth;
using TechnoPro.Common.UI.ClientManager.Web.Core.Impl.Local.Web;

namespace TechnoPro.ClockWorkWeb.user.NotetakingStudents
{
	// Token: 0x02000096 RID: 150
	public class user_NotetakingStudents_login : Page
	{
		// Token: 0x060004E7 RID: 1255 RVA: 0x00023BB0 File Offset: 0x00021DB0
		protected void Page_Load(object sender, EventArgs e)
		{
			bool flag = !this.Page.IsPostBack;
			if (flag)
			{
				NavigatorClientManager.CurrentInstance.SetReturnUrlSpecific("/user/notetakingstudents/courses.aspx");
			}
		}

		// Token: 0x060004E8 RID: 1256 RVA: 0x00005AEE File Offset: 0x00003CEE
		private void Page_Init(object sender, EventArgs e)
		{
			WebAuthenticationAuthorizationWebClientManager.CurrentInstance.ExemptThisPageFromAuthentication(this.Page, true);
		}

		// Token: 0x060004E9 RID: 1257 RVA: 0x00023BE4 File Offset: 0x00021DE4
		protected override void OnPreRender(EventArgs e)
		{
			this.ClockWorkLoginControl1.UsernameLabel = new WebSettingsClientManager().GetSettingValue<string>(Setting.LOGIN_StudentLoginUsernameLabelText);
			this.ClockWorkLoginControl1.InstructionText = new WebSettingsClientManager().GetSettingValue<string>(Setting.LOGIN_StudentLoginInstructionText);
			base.OnPreRender(e);
		}

		// Token: 0x040002C3 RID: 707
		protected ScriptManager bbb;

		// Token: 0x040002C4 RID: 708
		protected Panel p_title;

		// Token: 0x040002C5 RID: 709
		protected Label lbl_title;

		// Token: 0x040002C6 RID: 710
		protected ClockWorkLoginControl ClockWorkLoginControl1;
	}
}
