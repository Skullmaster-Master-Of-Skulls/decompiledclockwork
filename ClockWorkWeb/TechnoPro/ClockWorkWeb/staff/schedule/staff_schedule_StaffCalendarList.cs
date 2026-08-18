using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using TechnoPro.ClockWorkWeb.ctrls.Staff.Calendar;
using TechnoPro.Common.UI.ClientManager.Web.Auth;
using TechnoPro.Common.UI.Web.Entity;
using TechnoPro.Common.UI.Web.Entity.Web.EventArgs;

namespace TechnoPro.ClockWorkWeb.staff.schedule
{
	// Token: 0x0200010A RID: 266
	public class staff_schedule_StaffCalendarList : Page
	{
		// Token: 0x060007DE RID: 2014 RVA: 0x0003A358 File Offset: 0x00038558
		protected void Page_Load(object sender, EventArgs e)
		{
			int pid = this.GetPid();
			bool flag = !this.Page.IsPostBack;
			if (flag)
			{
				object obj = this.Session["apptUsingListCalendar"];
				bool flag2 = obj != null && (bool)obj;
				bool flag3 = !flag2;
				if (flag3)
				{
					base.Response.Redirect("StaffCalendar.aspx", true);
				}
				bool flag4 = base.Master != null && base.Master is IClockWorkMasterPage;
				if (flag4)
				{
					((IClockWorkMasterPage)base.Master).SetCurrentPage(eClockWorkWebPage.Staff_Calendar);
				}
				Style style = new Style();
				this.Page.Header.StyleSheet.CreateStyleRule(style, this, ".rsAptResize { visibility: hidden; }");
			}
		}

		// Token: 0x060007DF RID: 2015 RVA: 0x0003A415 File Offset: 0x00038615
		protected void btn_goToCalendarView_Click(object sender, EventArgs e)
		{
			this.Session.Remove("apptUsingListCalendar");
			base.Response.Redirect("StaffCalendar.aspx", true);
		}

		// Token: 0x060007E0 RID: 2016 RVA: 0x0003A43B File Offset: 0x0003863B
		protected void ctrlStaffListCalendar1_OnLoggedInUserPidRequested(object sender, UserEventArgs e)
		{
			e.PersonId = this.GetPid();
		}

		// Token: 0x060007E1 RID: 2017 RVA: 0x0003A44C File Offset: 0x0003864C
		private int GetPid()
		{
			return WebAuthenticationAuthorizationWebClientManager.CurrentInstance.GetStudentPid(this.Page);
		}

		// Token: 0x04000610 RID: 1552
		protected ScriptManager bbb;

		// Token: 0x04000611 RID: 1553
		protected Panel p_msg;

		// Token: 0x04000612 RID: 1554
		protected Label lbl_msg;

		// Token: 0x04000613 RID: 1555
		protected Panel p_gotoListView;

		// Token: 0x04000614 RID: 1556
		protected LinkButton btn_goToCalendarView;

		// Token: 0x04000615 RID: 1557
		protected ctrls_Staff_Calendar_CtrlStaffCalendarListView ctrlStaffListCalendar1;
	}
}
