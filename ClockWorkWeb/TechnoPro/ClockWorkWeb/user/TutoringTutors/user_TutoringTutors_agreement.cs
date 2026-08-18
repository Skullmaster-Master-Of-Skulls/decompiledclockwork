using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using skmValidators;
using TechnoPro.Common.ClientManager.Core.Settings;
using TechnoPro.Common.ClientManager.ICore.Tutoring;
using TechnoPro.Common.Public.Entities.Settings;
using TechnoPro.Common.UI.ClientManager.Web.Auth;
using TechnoPro.Common.UI.ClientManager.Web.Core.Impl.Local.Tutoring;
using TechnoPro.Common.UI.ClientManager.Web.Core.Impl.Local.Web;
using TechnoPro.Common.UI.ClientManager.Web.Core.Tutoring;
using TechnoPro.Common.UI.Web.Entity;

namespace TechnoPro.ClockWorkWeb.user.TutoringTutors
{
	// Token: 0x02000036 RID: 54
	public class user_TutoringTutors_agreement : Page
	{
		// Token: 0x06000146 RID: 326 RVA: 0x00009EC4 File Offset: 0x000080C4
		protected void Page_Load(object sender, EventArgs e)
		{
			int pid = this.GetPid();
			bool flag = !this.Page.IsPostBack;
			if (flag)
			{
				ITutoringClientWebClientManager tutoringClientWebClientManager = new TutoringClientWebClientManager();
				tutoringClientWebClientManager.EnforceTutoringRedirects(pid, this.Page, eClockWorkWebPage.TutoringTutors_ConfidentialityAgreement);
				this.lbl_agreement.Text = new WebSettingsClientManager().GetSettingValue<string>(Setting.TUTORING_TutorConfidentialityAgreement);
			}
		}

		// Token: 0x06000147 RID: 327 RVA: 0x00009F20 File Offset: 0x00008120
		private int GetPid()
		{
			return WebAuthenticationAuthorizationWebClientManager.CurrentInstance.GetStudentPid(this.Page);
		}

		// Token: 0x06000148 RID: 328 RVA: 0x00009F44 File Offset: 0x00008144
		protected void btn_submit_Click(object sender, EventArgs e)
		{
			int pid = this.GetPid();
			ITutorClientManager tutorClientManager = new TutorWebClientManager();
			tutorClientManager.RecordConfidentialityAgreementSignedByTutor(pid);
			NavigatorClientManager.CurrentInstance.GotoLastReturnUrl("~/user/TutoringTutors", "default.aspx");
		}

		// Token: 0x04000101 RID: 257
		protected Label lblTitle;

		// Token: 0x04000102 RID: 258
		protected TextBox lbl_agreement;

		// Token: 0x04000103 RID: 259
		protected Panel p_options;

		// Token: 0x04000104 RID: 260
		protected CheckBox chk_iagree;

		// Token: 0x04000105 RID: 261
		protected CheckBoxValidator CheckBoxValidator1;

		// Token: 0x04000106 RID: 262
		protected Button btn_submit;
	}
}
