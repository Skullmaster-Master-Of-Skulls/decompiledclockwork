using System;
using System.Web.UI;
using TechnoPro.ClockWorkWeb.ctrls.Tutoring.Tutor;
using TechnoPro.Common.UI.ClientManager.Web.Auth;
using TechnoPro.Common.UI.ClientManager.Web.Core.Impl.Local.Tutoring;
using TechnoPro.Common.UI.ClientManager.Web.Core.Tutoring;
using TechnoPro.Common.UI.Web.Entity;

namespace TechnoPro.ClockWorkWeb.user.TutoringTutors
{
	// Token: 0x02000045 RID: 69
	public class user_TutoringTutors_SubmitComment : Page
	{
		// Token: 0x060001B0 RID: 432 RVA: 0x0000BA40 File Offset: 0x00009C40
		protected void Page_Load(object sender, EventArgs e)
		{
			int tutorPersonId = this.LookupStudentPid();
			bool flag = !this.Page.IsPostBack;
			if (flag)
			{
				ITutoringClientWebClientManager tutoringClientWebClientManager = new TutoringClientWebClientManager();
				tutoringClientWebClientManager.EnforceTutoringRedirects(tutorPersonId, this.Page, eClockWorkWebPage.TutoringTutors_SubmitComment);
				bool flag2 = base.Master != null && base.Master is IClockWorkMasterPage;
				if (flag2)
				{
					IClockWorkMasterPage clockWorkMasterPage = (IClockWorkMasterPage)base.Master;
					clockWorkMasterPage.SetCurrentPage(eClockWorkWebPage.TutoringTutors_SubmitComment);
					clockWorkMasterPage.SetCausesValidationForAllMenuItems(false);
				}
			}
		}

		// Token: 0x060001B1 RID: 433 RVA: 0x0000BABC File Offset: 0x00009CBC
		private int LookupStudentPid()
		{
			return WebAuthenticationAuthorizationWebClientManager.CurrentInstance.GetStudentPid(this.Page);
		}

		// Token: 0x04000157 RID: 343
		protected ctrls_Tutoring_Tutor_CtrlSubmitCommentTutor ctrlSubmitCommentTutor1;
	}
}
