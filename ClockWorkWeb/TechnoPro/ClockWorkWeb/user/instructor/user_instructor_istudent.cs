using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Databases;
using TechnoPro.ClockWorkServer.Contracts.DTO.Adapters;
using TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;
using TechnoPro.Common.UI.ClientManager.Web.Auth;
using TechnoPro.Common.UI.ClientManager.Web.Core.Impl.Local.LookupCourses;
using TechnoPro.Common.UI.ClientManager.Web.Core.Impl.Local.Web;

namespace TechnoPro.ClockWorkWeb.user.instructor
{
	// Token: 0x020000DC RID: 220
	public class user_instructor_istudent : Page
	{
		// Token: 0x06000699 RID: 1689 RVA: 0x00032900 File Offset: 0x00030B00
		private int GetPid()
		{
			return WebAuthenticationAuthorizationWebClientManager.CurrentInstance.GetInstructorId(this.Page);
		}

		// Token: 0x0600069A RID: 1690 RVA: 0x00032924 File Offset: 0x00030B24
		private int GetAltContactId()
		{
			return WebAuthenticationAuthorizationWebClientManager.CurrentInstance.GetAltContactId(this.Page);
		}

		// Token: 0x0600069B RID: 1691 RVA: 0x00032948 File Offset: 0x00030B48
		protected void Page_Load(object sender, EventArgs e)
		{
			DatabaseLayer clockWork = DatabaseLayerFactory.ClockWork;
			int pid = this.GetPid();
			int altContactId = this.GetAltContactId();
			bool flag = pid <= 0 && altContactId <= 0;
			if (flag)
			{
				base.Response.Redirect("Message.aspx?msgcode=notallowed");
			}
			else
			{
				bool flag2 = !this.Page.IsPostBack;
				if (flag2)
				{
					int luCourseId = this.GetLuCourseId();
					IList<StudentCourseLetterInfo> studentsCoursesLettersAreAllowedForInstructorByInstructorAndCourse = InstructorClientHelper.GetStudentsCoursesLettersAreAllowedForInstructorByInstructorAndCourse(pid, altContactId, luCourseId);
					List<StudentCourseLetterInfo> list = ((studentsCoursesLettersAreAllowedForInstructorByInstructorAndCourse != null) ? studentsCoursesLettersAreAllowedForInstructorByInstructorAndCourse.ToList<StudentCourseLetterInfo>() : null) ?? new List<StudentCourseLetterInfo>();
					list.Sort(delegate(StudentCourseLetterInfo g1, StudentCourseLetterInfo g2)
					{
						BasicPersonDTO student4 = g1.Student;
						string text2 = ((student4 != null) ? student4.GetStudentName() : null) ?? "";
						BasicPersonDTO student5 = g2.Student;
						return text2.CompareTo((student5 != null) ? student5.GetStudentName() : null);
					});
					bool flag3 = false;
					foreach (StudentCourseLetterInfo studentCourseLetterInfo in list)
					{
						DateTime? dateLetterReturned = studentCourseLetterInfo.DateLetterReturned;
						BasicPersonDTO student = studentCourseLetterInfo.Student;
						string arg = ((student != null) ? student.LastName : null) ?? "";
						BasicPersonDTO student2 = studentCourseLetterInfo.Student;
						string arg2 = ((student2 != null) ? student2.FirstName : null) ?? "";
						BasicPersonDTO student3 = studentCourseLetterInfo.Student;
						int num = (student3 != null) ? student3.PersonId : 0;
						string text = string.Format("{0}, {1}{2}", arg, arg2, (dateLetterReturned == null) ? "" : string.Format(" [receipt confirmed on {0}]", dateLetterReturned.Value.ToString("MMMM d, yyyy")));
						ListItem item = new ListItem(text, num.ToString());
						this.rl_students.Items.Add(item);
						bool flag4 = flag3;
						if (!flag4)
						{
							flag3 = true;
							LookupCourseBaseDTO courseBase = studentCourseLetterInfo.CourseBase;
							string arg3 = ((courseBase != null) ? courseBase.Subject.SubjectDescription : null) ?? "";
							LookupCourseBaseDTO courseBase2 = studentCourseLetterInfo.CourseBase;
							string arg4 = ((courseBase2 != null) ? courseBase2.Course : null) ?? "";
							LookupCourseBaseDTO courseBase3 = studentCourseLetterInfo.CourseBase;
							string arg5 = ((courseBase3 != null) ? courseBase3.Section : null) ?? "";
							this.lbl_title.Text = string.Format("Accommodation Letters for {0} {1} {2}", arg3, arg4, arg5);
						}
					}
					bool flag5 = list.Count > 0;
					if (!flag5)
					{
						this.btn_submit.Enabled = false;
						this.p_info.Text = "<br />There are no students for this course with available accommodation letters.";
					}
				}
			}
		}

		// Token: 0x0600069C RID: 1692 RVA: 0x00032BC8 File Offset: 0x00030DC8
		private int GetLuCourseId()
		{
			return NavigatorClientManager.CurrentInstance.GetIntFromUrlParameter(base.Request.QueryString["lucid"] ?? "");
		}

		// Token: 0x0600069D RID: 1693 RVA: 0x00032C04 File Offset: 0x00030E04
		protected void btn_submit_Click(object sender, EventArgs e)
		{
			bool flag = this.rl_students.SelectedItem == null;
			if (!flag)
			{
				int luCourseId = this.GetLuCourseId();
				string str = NavigatorClientManager.CurrentInstance.ConvertIntParameterToUrlString(luCourseId);
				int parameter;
				int.TryParse(this.rl_students.SelectedValue, out parameter);
				string str2 = NavigatorClientManager.CurrentInstance.ConvertIntParameterToUrlString(parameter);
				base.Response.Redirect("iletter.aspx?lucid=" + str + "&pid=" + str2, true);
			}
		}

		// Token: 0x0600069E RID: 1694 RVA: 0x0002FC01 File Offset: 0x0002DE01
		protected void btn_back_Click(object sender, EventArgs e)
		{
			base.Response.Redirect("courses.aspx", true);
		}

		// Token: 0x04000503 RID: 1283
		protected Panel p_top;

		// Token: 0x04000504 RID: 1284
		protected Label lbl_title;

		// Token: 0x04000505 RID: 1285
		protected Label p_info;

		// Token: 0x04000506 RID: 1286
		protected Panel p_list;

		// Token: 0x04000507 RID: 1287
		protected RadioButtonList rl_students;

		// Token: 0x04000508 RID: 1288
		protected Button btn_submit;

		// Token: 0x04000509 RID: 1289
		protected Button btn_back;
	}
}
