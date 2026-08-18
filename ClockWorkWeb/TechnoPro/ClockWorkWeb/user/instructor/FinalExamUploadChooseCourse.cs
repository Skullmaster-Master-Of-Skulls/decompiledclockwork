using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using ClockWorkController;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;
using TechnoPro.Common.ClientManager.Core.AppointmentsTestBooking;
using TechnoPro.Common.ClientManager.Core.Settings;
using TechnoPro.Common.ClientManager.ICore.AppointmentsTestBooking;
using TechnoPro.Common.Public.Entities.Adapters;
using TechnoPro.Common.Public.Entities.AppointmentBookingStudent;
using TechnoPro.Common.Public.Entities.AppointmentsTestBooking;
using TechnoPro.Common.Public.Entities.Settings;
using TechnoPro.Common.UI.ClientManager.Web.Auth;
using TechnoPro.Common.UI.ClientManager.Web.Core.Impl.Local.LookupCourses;
using TechnoPro.Common.UI.ClientManager.Web.Core.Impl.Local.Web;
using TechnoPro.Common.UI.ClientManager.Web.Core.LookupCourses;
using TechnoPro.Common.UI.Web.Entity.LookupCourses;

namespace TechnoPro.ClockWorkWeb.user.instructor
{
	// Token: 0x020000D6 RID: 214
	public class FinalExamUploadChooseCourse : Page
	{
		// Token: 0x06000664 RID: 1636 RVA: 0x000312AC File Offset: 0x0002F4AC
		private static FinalExamUploadChooseCourse.ExistingExam LoadExamByLucid(int lucid)
		{
			bool flag = lucid < 1;
			FinalExamUploadChooseCourse.ExistingExam result;
			if (flag)
			{
				result = null;
			}
			else
			{
				IExamRequestClientManager examRequestClientManager = new ExamRequestClientManager();
				IList<int> list2;
				IList<PersonBaseDTO> list = examRequestClientManager.LoadStudentsRegisteredInCourseWithStudentListWhoSubmittedExamRequests(lucid, out list2);
				ISessionClientManager sessionClientManager = new SessionClientManager();
				SessionView currentSession = sessionClientManager.GetCurrentSession();
				DateTime startDate = currentSession.StartDate;
				DateTime endDate = currentSession.EndDate;
				IClassTestDefinitionClientManager classTestDefinitionClientManager = new ClassTestDefinitionClientManager();
				IList<ClassTestForExamRequestDTO> list3 = classTestDefinitionClientManager.LoadClassTestsForExamRequestByDateRange(lucid, startDate, endDate, eClassTestType.FinalExam);
				bool flag2 = list3.Count < 1;
				if (flag2)
				{
					result = null;
				}
				else
				{
					bool flag3 = list3.Count == 1;
					if (flag3)
					{
						result = new FinalExamUploadChooseCourse.ExistingExam(list3[0]);
					}
					else
					{
						CutoffTime cutoffForUpdatingTests = new WebSettingsClientManager().GetSettingValue<string>(Setting.INSTRUCTOR_CutoffForUpdatingTests).CutoffTimeFromXml() ?? CutoffTime.None;
						List<ClassTestForExamRequestDTO> list4 = list3.Where(delegate(ClassTestForExamRequestDTO g)
						{
							DateTime startDateTime = g.StartDateTime;
							bool? flag5 = cutoffForUpdatingTests.IsRightNowBeforeCutoffTime(startDateTime);
							return flag5 == null || flag5.Value;
						}).ToList<ClassTestForExamRequestDTO>();
						bool flag4 = list4.Count < 1;
						if (flag4)
						{
							result = null;
						}
						else
						{
							ClassTestForExamRequestDTO classTestForExamRequestDTO = list4.FirstOrDefault((ClassTestForExamRequestDTO g) => g.ExamRequestInstructorChoices.Trim().Length > 0) ?? list3[0];
							result = ((classTestForExamRequestDTO == null) ? null : new FinalExamUploadChooseCourse.ExistingExam(classTestForExamRequestDTO));
						}
					}
				}
			}
			return result;
		}

		// Token: 0x06000665 RID: 1637 RVA: 0x000313F4 File Offset: 0x0002F5F4
		protected void Page_Load(object sender, EventArgs e)
		{
			bool settingValue = new WebSettingsClientManager().GetSettingValue<bool>(Setting.EXAMBOOKING_FinalExamRequest_Enabled);
			bool flag = !settingValue;
			if (flag)
			{
				base.Response.Redirect("courses.aspx", true);
			}
			else
			{
				int pid = this.GetPid();
				int altContactId = this.GetAltContactId();
				bool flag2 = pid < 1 && altContactId < 1;
				if (flag2)
				{
					NavigatorClientManager.CurrentInstance.NotAllowed(Setting.INSTRUCTOR_ErrorMessage_NotRegistered, this.Page);
				}
				else
				{
					bool flag3 = !this.Page.IsPostBack;
					if (flag3)
					{
						DataTable dataTable = Course.LoadInstructorOrAltContactCourseNowOrFuture(pid, altContactId);
						DataRow dataRow = dataTable.NewRow();
						dataRow["coursedescription"] = "";
						dataRow["lucourseid"] = 0;
						dataRow["startdate"] = DateTime.Now;
						dataRow["enddate"] = DateTime.Now.AddHours(1.0);
						dataRow["subjectid"] = 0;
						dataRow["instructorid"] = 0;
						dataRow["crosslistequivalentcode"] = 0;
						dataRow["session"] = "";
						bool flag4 = dataTable.Rows.Count < 1;
						if (flag4)
						{
							this.p_chooseCourse.Visible = false;
							this.p_noCourses.Visible = true;
						}
						else
						{
							dataTable.Rows.InsertAt(dataRow, 0);
							this.cmb_courses.DataSource = dataTable;
							this.cmb_courses.DataTextField = "coursedescription";
							this.cmb_courses.DataValueField = "lucourseid";
							this.cmb_courses.DataBind();
						}
					}
				}
			}
		}

		// Token: 0x06000666 RID: 1638 RVA: 0x000315CC File Offset: 0x0002F7CC
		private int GetPid()
		{
			return WebAuthenticationAuthorizationWebClientManager.CurrentInstance.GetInstructorId(this.Page);
		}

		// Token: 0x06000667 RID: 1639 RVA: 0x000315F0 File Offset: 0x0002F7F0
		private int GetAltContactId()
		{
			return WebAuthenticationAuthorizationWebClientManager.CurrentInstance.GetAltContactId(this.Page);
		}

		// Token: 0x06000668 RID: 1640 RVA: 0x00031612 File Offset: 0x0002F812
		private void ShowMessage(string msg)
		{
			this.p_topmsg.Visible = true;
			this.lbl_topmsg.Text = msg;
		}

		// Token: 0x06000669 RID: 1641 RVA: 0x00031630 File Offset: 0x0002F830
		public void btn_submit_Click(object sender, EventArgs e)
		{
			string s = this.cmb_courses.SelectedValue ?? "";
			int num;
			int.TryParse(s, out num);
			bool flag = num < 1;
			if (flag)
			{
				this.ShowMessage("Please select a course from the list first.");
			}
			else
			{
				FinalExamUploadChooseCourse.ExistingExam existingExam = FinalExamUploadChooseCourse.LoadExamByLucid(num);
				string url = (existingExam == null) ? string.Format("FinalExamUpload.aspx?lucid={0}", this.GetUrlParameterFromInt(num)) : string.Format("FinalExamUpload.aspx?lucid={0}&examid={1}", this.GetUrlParameterFromInt(num), this.GetUrlParameterFromInt(existingExam.ExamId));
				base.Response.Redirect(url, true);
			}
		}

		// Token: 0x0600066A RID: 1642 RVA: 0x000316C0 File Offset: 0x0002F8C0
		private string GetUrlParameterFromInt(int num)
		{
			return NavigatorClientManager.CurrentInstance.GetUrlParameterFromString(num);
		}

		// Token: 0x040004D7 RID: 1239
		protected Label lbl_Title;

		// Token: 0x040004D8 RID: 1240
		protected Panel p_topmsg;

		// Token: 0x040004D9 RID: 1241
		protected Image img_topmsg;

		// Token: 0x040004DA RID: 1242
		protected Label lbl_topmsg;

		// Token: 0x040004DB RID: 1243
		protected Panel p_noCourses;

		// Token: 0x040004DC RID: 1244
		protected Panel p_chooseCourse;

		// Token: 0x040004DD RID: 1245
		protected Label lbl_course;

		// Token: 0x040004DE RID: 1246
		protected DropDownList cmb_courses;

		// Token: 0x040004DF RID: 1247
		protected Button btn_submit;

		// Token: 0x040004E0 RID: 1248
		protected RequiredFieldValidator vcourse;

		// Token: 0x040004E1 RID: 1249
		protected Label lbl_students;

		// Token: 0x02000208 RID: 520
		public class ExistingExam
		{
			// Token: 0x06000DD7 RID: 3543 RVA: 0x0000AF9E File Offset: 0x0000919E
			public ExistingExam()
			{
			}

			// Token: 0x06000DD8 RID: 3544 RVA: 0x0004F804 File Offset: 0x0004DA04
			public ExistingExam(ClassTestForExamRequestDTO classTestForExamRequest)
			{
				bool flag = classTestForExamRequest == null;
				if (!flag)
				{
					this.ExamRequestInstructorChoices = classTestForExamRequest.ExamRequestInstructorChoices;
					this.LuCourseId = ((classTestForExamRequest.Course == null) ? 0 : classTestForExamRequest.Course.LuCourseId);
					this.ExamId = classTestForExamRequest.ExamId;
					this.DateOfTest = classTestForExamRequest.StartDateTime.Date;
					this.Title = string.Join(": ", (from g in new string[]
					{
						this.DateOfTest.ToString("MMM d, yyyy"),
						(this.ExamRequestInstructorChoices ?? "").Trim()
					}
					where g.Length > 0
					select g).ToArray<string>());
				}
			}

			// Token: 0x17000310 RID: 784
			// (get) Token: 0x06000DD9 RID: 3545 RVA: 0x0004F8DF File Offset: 0x0004DADF
			// (set) Token: 0x06000DDA RID: 3546 RVA: 0x0004F8E7 File Offset: 0x0004DAE7
			public int ExamId { get; set; }

			// Token: 0x17000311 RID: 785
			// (get) Token: 0x06000DDB RID: 3547 RVA: 0x0004F8F0 File Offset: 0x0004DAF0
			// (set) Token: 0x06000DDC RID: 3548 RVA: 0x0004F8F8 File Offset: 0x0004DAF8
			public int LuCourseId { get; set; }

			// Token: 0x17000312 RID: 786
			// (get) Token: 0x06000DDD RID: 3549 RVA: 0x0004F901 File Offset: 0x0004DB01
			// (set) Token: 0x06000DDE RID: 3550 RVA: 0x0004F909 File Offset: 0x0004DB09
			public DateTime DateOfTest { get; set; }

			// Token: 0x17000313 RID: 787
			// (get) Token: 0x06000DDF RID: 3551 RVA: 0x0004F912 File Offset: 0x0004DB12
			// (set) Token: 0x06000DE0 RID: 3552 RVA: 0x0004F91A File Offset: 0x0004DB1A
			public string ExamRequestInstructorChoices { get; set; }

			// Token: 0x17000314 RID: 788
			// (get) Token: 0x06000DE1 RID: 3553 RVA: 0x0004F923 File Offset: 0x0004DB23
			// (set) Token: 0x06000DE2 RID: 3554 RVA: 0x0004F92B File Offset: 0x0004DB2B
			public string Title { get; set; }
		}
	}
}
