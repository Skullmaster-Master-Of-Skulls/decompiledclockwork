using System;
using System.Web.UI;
using TechnoPro.ClockWorkWeb.ctrls.Tutoring.Tutee;
using TechnoPro.Common.UI.ClientManager.Web.Auth;
using TechnoPro.Common.UI.ClientManager.Web.Core.Impl.Local.Tutoring;
using TechnoPro.Common.UI.ClientManager.Web.Core.Tutoring;
using TechnoPro.Common.UI.Web.Entity;

namespace TechnoPro.ClockWorkWeb.user.TutoringStudents
{
	// Token: 0x0200005C RID: 92
	public class user_TutoringStudents_SubmitComment : Page
	{
		// Token: 0x0600023C RID: 572 RVA: 0x0000D6F4 File Offset: 0x0000B8F4
		protected void Page_Load(object sender, EventArgs e)
		{
			int studentPersonId = this.LookupStudentPid();
			bool flag = !this.Page.IsPostBack;
			if (flag)
			{
				ITutoringClientWebClientManager tutoringClientWebClientManager = new TutoringClientWebClientManager();
				tutoringClientWebClientManager.EnforceStudentTuteeRedirects(studentPersonId, this.Page, eClockWorkWebPage.TutoringStudents_SubmitComment);
				bool flag2 = base.Master != null && base.Master is IClockWorkMasterPage;
				if (flag2)
				{
					IClockWorkMasterPage clockWorkMasterPage = (IClockWorkMasterPage)base.Master;
					clockWorkMasterPage.SetCurrentPage(eClockWorkWebPage.TutoringStudents_SubmitComment);
					clockWorkMasterPage.SetCausesValidationForAllMenuItems(false);
				}
			}
		}

		// Token: 0x0600023D RID: 573 RVA: 0x0000D774 File Offset: 0x0000B974
		private int LookupStudentPid_DontTryToAuthenticate()
		{
			return WebAuthenticationAuthorizationWebClientManager.CurrentInstance.GetStudentPid_DontTryToAuthenticate(this.Page);
		}

		// Token: 0x0600023E RID: 574 RVA: 0x0000D798 File Offset: 0x0000B998
		private int LookupStudentPid()
		{
			return WebAuthenticationAuthorizationWebClientManager.CurrentInstance.GetStudentPid(this.Page);
		}

		// Token: 0x0400019B RID: 411
		protected ctrls_Tutoring_Tutee_CtrlSubmitCommentTutee ctrlSubmitCommentTutee1;
	}
}
