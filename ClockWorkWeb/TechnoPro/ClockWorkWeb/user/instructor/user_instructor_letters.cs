using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using TechnoPro.ClockWorkServer.Contracts.DTO.Adapters;
using TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;
using TechnoPro.Common.ClientManager.Core.Settings;
using TechnoPro.Common.Public.Entities.Settings;
using TechnoPro.Common.UI.ClientManager.Web.Auth;
using TechnoPro.Common.UI.ClientManager.Web.Core.Impl.Local.LookupCourses;
using TechnoPro.Common.UI.ClientManager.Web.Core.Impl.Local.Web;
using TechnoPro.Common.UI.Web.ClockWork.Controls;
using TechnoPro.Common.UI.Web.Entity;
using TechnoPro.Common.UI.Web.Entity.LookupCourses;
using Telerik.Web.UI;

namespace TechnoPro.ClockWorkWeb.user.instructor
{
	// Token: 0x020000DD RID: 221
	public class user_instructor_letters : Page
	{
		// Token: 0x060006A0 RID: 1696 RVA: 0x00032C78 File Offset: 0x00030E78
		protected void Page_Load(object sender, EventArgs e)
		{
			int pid = this.GetPid();
			int altContactId = this.GetAltContactId();
			bool flag = pid < 1 && altContactId < 1;
			if (flag)
			{
				int studentPid = this.GetStudentPid();
				bool flag2 = studentPid > 0;
				if (flag2)
				{
					base.Response.Redirect("../../custom/misc/home.aspx", true);
				}
			}
			bool flag3 = this.Page.Master != null && this.Page.Master is IClockWorkMasterPage;
			if (flag3)
			{
				((IClockWorkMasterPage)this.Page.Master).SetCurrentPage(eClockWorkWebPage.Instructor_AccommodationLetters);
			}
			bool settingValue = new WebSettingsClientManager().GetSettingValue<bool>(Setting.INSTRUCTOR_LettersEnabled);
			bool flag4 = !settingValue;
			if (flag4)
			{
				base.Response.Redirect("courses.aspx?code=lettersNotEnabled", true);
			}
		}

		// Token: 0x060006A1 RID: 1697 RVA: 0x00032D38 File Offset: 0x00030F38
		private void Page_Init(object sender, EventArgs e)
		{
			WebSettingsClientManager webSettingsClientManager = new WebSettingsClientManager();
			bool settingValue = webSettingsClientManager.GetSettingValue<bool>(Setting.MODULES_ENABLED_SelfReg);
			bool flag = !settingValue;
			if (flag)
			{
				GridColumn gridColumn = this.gv_courses.Columns.FindByUniqueName("DateAvailable");
				bool flag2 = gridColumn != null;
				if (flag2)
				{
					gridColumn.Visible = false;
				}
			}
		}

		// Token: 0x060006A2 RID: 1698 RVA: 0x00032D8C File Offset: 0x00030F8C
		protected void CtrlTermChooser1_OnSelectedIndexChanged(object sender, EventArgs e)
		{
			SessionView selectedSession = this.CtrlTermChooser1.SelectedSession;
			bool flag = selectedSession != null;
			if (flag)
			{
				this.Session["ic_currentterm"] = selectedSession;
				this.gv_courses.Rebind();
			}
		}

		// Token: 0x060006A3 RID: 1699 RVA: 0x00032DCE File Offset: 0x00030FCE
		protected void ctrlTermChooser1_OnUserInfoRequested(object sender, UserInfoForCourseArgs e)
		{
			e.Info.AlternateContactId = this.GetAltContactId();
			e.Info.InstructorId = this.GetPid();
		}

		// Token: 0x060006A4 RID: 1700 RVA: 0x00032DF8 File Offset: 0x00030FF8
		private int GetStudentPid()
		{
			return WebAuthenticationAuthorizationWebClientManager.CurrentInstance.GetStudentPid(this.Page);
		}

		// Token: 0x060006A5 RID: 1701 RVA: 0x00032E1C File Offset: 0x0003101C
		private int GetPid()
		{
			return WebAuthenticationAuthorizationWebClientManager.CurrentInstance.GetInstructorId(this.Page);
		}

		// Token: 0x060006A6 RID: 1702 RVA: 0x00032E40 File Offset: 0x00031040
		private int GetAltContactId()
		{
			return WebAuthenticationAuthorizationWebClientManager.CurrentInstance.GetAltContactId(this.Page);
		}

		// Token: 0x060006A7 RID: 1703 RVA: 0x00032E64 File Offset: 0x00031064
		protected void gv_courses_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
		{
			int pid = this.GetPid();
			int altContactId = this.GetAltContactId();
			SessionView selectedSession = this.CtrlTermChooser1.SelectedSession;
			bool flag = selectedSession == null;
			if (flag)
			{
				throw new Exception("GetSelectedTermDates needs session but selected session is null.");
			}
			DateTime startDate = selectedSession.StartDate;
			DateTime endDate = selectedSession.EndDate;
			IList<StudentCourseLetterInfo> studentsCoursesLettersAreAllowedForByInstructorAndDateRange = InstructorClientHelper.GetStudentsCoursesLettersAreAllowedForByInstructorAndDateRange(pid, altContactId, startDate, endDate);
			this.gv_courses.DataSource = (from g in studentsCoursesLettersAreAllowedForByInstructorAndDateRange
			select new user_instructor_letters.StudentWithRequestAndCourseInfoWrapper(g)).ToList<user_instructor_letters.StudentWithRequestAndCourseInfoWrapper>();
		}

		// Token: 0x060006A8 RID: 1704 RVA: 0x00032EF4 File Offset: 0x000310F4
		protected void gv_courses_ItemDataBound(object sender, GridItemEventArgs e)
		{
			bool flag = e.Item is GridPagerItem;
			if (flag)
			{
				Label label = (Label)e.Item.FindControl("ChangePageSizeLabel");
				RadComboBox radComboBox = (RadComboBox)e.Item.FindControl("PageSizeComboBox");
				bool flag2 = label != null && radComboBox != null;
				if (flag2)
				{
					label.Visible = false;
					radComboBox.Label = "Page_size:";
				}
			}
		}

		// Token: 0x060006A9 RID: 1705 RVA: 0x00032F68 File Offset: 0x00031168
		protected void gv_courses_ItemCommand(object source, GridCommandEventArgs e)
		{
			object commandArgument = e.CommandArgument;
			string text = ((commandArgument != null) ? commandArgument.ToString().Trim() : null) ?? "";
			int num = text.IndexOf('.');
			bool flag = num < 1;
			if (!flag)
			{
				int parameter;
				int parameter2;
				bool flag2 = !int.TryParse(text.Substring(0, num), out parameter) || !int.TryParse(text.Substring(num + 1), out parameter2);
				if (!flag2)
				{
					base.Response.Redirect(string.Format("iletter.aspx?pid={0}&lucid={1}", NavigatorClientManager.CurrentInstance.ConvertIntParameterToUrlString(parameter), NavigatorClientManager.CurrentInstance.ConvertIntParameterToUrlString(parameter2)), true);
				}
			}
		}

		// Token: 0x0400050A RID: 1290
		protected Table t_term;

		// Token: 0x0400050B RID: 1291
		protected Label lblTitle;

		// Token: 0x0400050C RID: 1292
		protected CtrlTermChooser CtrlTermChooser1;

		// Token: 0x0400050D RID: 1293
		protected Label lbl_title;

		// Token: 0x0400050E RID: 1294
		protected RadGrid gv_courses;

		// Token: 0x0200020E RID: 526
		internal class StudentWithRequestAndCourseInfoWrapper : WrapperBase<StudentCourseLetterInfo>
		{
			// Token: 0x06000DF1 RID: 3569 RVA: 0x0004F9FB File Offset: 0x0004DBFB
			public StudentWithRequestAndCourseInfoWrapper()
			{
			}

			// Token: 0x06000DF2 RID: 3570 RVA: 0x0004FA05 File Offset: 0x0004DC05
			public StudentWithRequestAndCourseInfoWrapper(StudentCourseLetterInfo info) : base(info)
			{
			}

			// Token: 0x17000315 RID: 789
			// (get) Token: 0x06000DF3 RID: 3571 RVA: 0x0004FA10 File Offset: 0x0004DC10
			public string Student
			{
				get
				{
					StudentCourseLetterInfo item = base.Item;
					return (((item != null) ? item.Student : null) == null) ? "" : base.Item.Student.GetStudentName();
				}
			}

			// Token: 0x17000316 RID: 790
			// (get) Token: 0x06000DF4 RID: 3572 RVA: 0x0004FA3D File Offset: 0x0004DC3D
			public string Course
			{
				get
				{
					StudentCourseLetterInfo item = base.Item;
					return (((item != null) ? item.CourseBase : null) == null) ? "" : base.Item.CourseBase.GetCourseDescription();
				}
			}

			// Token: 0x17000317 RID: 791
			// (get) Token: 0x06000DF5 RID: 3573 RVA: 0x0004FA6C File Offset: 0x0004DC6C
			public DateTime? DateAvailable
			{
				get
				{
					StudentCourseLetterInfo item = base.Item;
					return (item != null) ? item.DateApproved : null;
				}
			}

			// Token: 0x17000318 RID: 792
			// (get) Token: 0x06000DF6 RID: 3574 RVA: 0x0004FA94 File Offset: 0x0004DC94
			public DateTime? ConfirmationDate
			{
				get
				{
					StudentCourseLetterInfo item = base.Item;
					return (item != null) ? item.DateLetterReturned : null;
				}
			}

			// Token: 0x17000319 RID: 793
			// (get) Token: 0x06000DF7 RID: 3575 RVA: 0x0004FABC File Offset: 0x0004DCBC
			public string PidAndLucid
			{
				get
				{
					return (base.Item == null) ? "" : ((base.Item.Student ?? new BasicPersonDTO()).PersonId.ToString() + "." + (base.Item.CourseBase ?? new LookupCourseBaseDTO()).LuCourseId.ToString());
				}
			}
		}
	}
}
