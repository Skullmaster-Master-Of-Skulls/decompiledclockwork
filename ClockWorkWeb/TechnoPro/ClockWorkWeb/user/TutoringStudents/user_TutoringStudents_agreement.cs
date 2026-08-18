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

namespace TechnoPro.ClockWorkWeb.user.TutoringStudents
{
	// Token: 0x0200004B RID: 75
	public class user_TutoringStudents_agreement : Page
	{
		// Token: 0x060001C7 RID: 455 RVA: 0x0000BE1C File Offset: 0x0000A01C
		protected void Page_Load(object sender, EventArgs e)
		{
			int pid = this.GetPid();
			bool flag = !this.Page.IsPostBack;
			if (flag)
			{
				ITutoringClientWebClientManager tutoringClientWebClientManager = new TutoringClientWebClientManager();
				tutoringClientWebClientManager.EnforceStudentTuteeRedirects(pid, this.Page, eClockWorkWebPage.TutoringStudents_ConfidentialityAgreement);
				this.lbl_agreement.Text = new WebSettingsClientManager().GetSettingValue<string>(Setting.TUTORING_StudentConfidentialityAgreement);
			}
		}

		// Token: 0x060001C8 RID: 456 RVA: 0x0000BE78 File Offset: 0x0000A078
		private int GetPid()
		{
			return WebAuthenticationAuthorizationWebClientManager.CurrentInstance.GetStudentPid(this.Page);
		}

		// Token: 0x060001C9 RID: 457 RVA: 0x0000BE9C File Offset: 0x0000A09C
		protected void btn_submit_Click(object sender, EventArgs e)
		{
			int pid = this.GetPid();
			IStudentTuteeClientManager studentTuteeClientManager = new StudentTuteeWebClientManager();
			studentTuteeClientManager.RecordConfidentialityAgreementSignedByStudent(pid);
			NavigatorClientManager.CurrentInstance.GotoLastReturnUrl("~/user/TutoringStudents", "default.aspx");
		}

		// Token: 0x0400016C RID: 364
		protected TextBox lbl_agreement;

		// Token: 0x0400016D RID: 365
		protected CheckBox chk_iagree;

		// Token: 0x0400016E RID: 366
		protected CheckBoxValidator CheckBoxValidator1;

		// Token: 0x0400016F RID: 367
		protected Button btn_submit;
	}
}
