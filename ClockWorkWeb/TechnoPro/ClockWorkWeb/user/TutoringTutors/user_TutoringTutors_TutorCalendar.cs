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
	// Token: 0x02000046 RID: 70
	public class user_TutoringTutors_TutorCalendar : Page
	{
		// Token: 0x060001B3 RID: 435 RVA: 0x0000BAE0 File Offset: 0x00009CE0
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
				bool flag3 = flag2;
				if (flag3)
				{
					base.Response.Redirect("TutorCalendarList.aspx", true);
				}
				bool flag4 = base.Master != null && base.Master is IClockWorkMasterPage;
				if (flag4)
				{
					((IClockWorkMasterPage)base.Master).SetCurrentPage(eClockWorkWebPage.TutoringTutors_Calendar);
				}
				Style style = new Style();
				this.Page.Header.StyleSheet.CreateStyleRule(style, this, ".rsAptResize { visibility: hidden; }");
			}
		}

		// Token: 0x060001B4 RID: 436 RVA: 0x0000BBB0 File Offset: 0x00009DB0
		protected void ctrlTutorCalendar1_OnLoggedInUserPidRequested(object sender, UserEventArgs e)
		{
			e.PersonId = this.GetPid();
		}

		// Token: 0x060001B5 RID: 437 RVA: 0x0000BBC0 File Offset: 0x00009DC0
		private int GetPid()
		{
			return WebAuthenticationAuthorizationWebClientManager.CurrentInstance.GetStudentPid(this.Page);
		}

		// Token: 0x060001B6 RID: 438 RVA: 0x0000BBE2 File Offset: 0x00009DE2
		protected void btn_goToListView_Click(object sender, EventArgs e)
		{
			this.Session.Add("apptUsingListCalendar", true);
			base.Response.Redirect("TutorCalendarList.aspx", true);
		}

		// Token: 0x04000158 RID: 344
		protected Panel p_msg;

		// Token: 0x04000159 RID: 345
		protected Label lbl_msg;

		// Token: 0x0400015A RID: 346
		protected Panel p_gotoListView;

		// Token: 0x0400015B RID: 347
		protected LinkButton btn_goToListView;

		// Token: 0x0400015C RID: 348
		protected ctrls_Tutoring_Tutor_CtrlTutorCalendar ctrlTutorCalendar1;
	}
}
