using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using TechnoPro.ClockWorkWeb.ctrls.Tutoring.Tutor;
using TechnoPro.Common.UI.ClientManager.Web.Auth;
using TechnoPro.Common.UI.ClientManager.Web.Core.Impl.Local.Tutoring;
using TechnoPro.Common.UI.ClientManager.Web.Core.Tutoring;
using TechnoPro.Common.UI.Web.Entity;
using TechnoPro.Common.UI.Web.Entity.Web.EventArgs;

namespace TechnoPro.ClockWorkWeb.user.TutoringTutors
{
	// Token: 0x02000047 RID: 71
	public class user_TutoringTutors_TutorCalendarList : Page
	{
		// Token: 0x060001B8 RID: 440 RVA: 0x0000BC10 File Offset: 0x00009E10
		protected void Page_Load(object sender, EventArgs e)
		{
			int pid = this.GetPid();
			bool flag = !this.Page.IsPostBack;
			if (flag)
			{
				ITutoringClientWebClientManager tutoringClientWebClientManager = new TutoringClientWebClientManager();
				tutoringClientWebClientManager.EnforceTutoringRedirects(pid, this.Page, eClockWorkWebPage.TutoringTutors_Calendar);
				object obj = this.Session["apptUsingListCalendar"];
				bool flag2 = obj != null && (bool)obj;
				bool flag3 = !flag2;
				if (flag3)
				{
					base.Response.Redirect("TutorCalendar.aspx", true);
				}
				bool flag4 = base.Master != null && base.Master is IClockWorkMasterPage;
				if (flag4)
				{
					((IClockWorkMasterPage)base.Master).SetCurrentPage(eClockWorkWebPage.TutoringTutors_Calendar);
				}
			}
		}

		// Token: 0x060001B9 RID: 441 RVA: 0x0000BCBE File Offset: 0x00009EBE
		protected void ctrlTutorListCalendar1_OnLoggedInUserPidRequested(object sender, UserEventArgs e)
		{
			e.PersonId = this.GetPid();
		}

		// Token: 0x060001BA RID: 442 RVA: 0x0000BCD0 File Offset: 0x00009ED0
		private int GetPid()
		{
			return WebAuthenticationAuthorizationWebClientManager.CurrentInstance.GetStudentPid(this.Page);
		}

		// Token: 0x060001BB RID: 443 RVA: 0x0000BCF2 File Offset: 0x00009EF2
		protected void btn_goToCalendarView_Click(object sender, EventArgs e)
		{
			this.Session.Remove("apptUsingListCalendar");
			base.Response.Redirect("TutorCalendar.aspx", true);
		}

		// Token: 0x0400015D RID: 349
		protected Panel p_msg;

		// Token: 0x0400015E RID: 350
		protected Label lbl_msg;

		// Token: 0x0400015F RID: 351
		protected Panel p_gotoListView;

		// Token: 0x04000160 RID: 352
		protected LinkButton btn_goToCalendarView;

		// Token: 0x04000161 RID: 353
		protected ctrls_Tutoring_Tutor_CtrlTutorListCalendar ctrlTutorListCalendar1;
	}
}
