using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking;
using TechnoPro.Common.ClientManager.Core.AppointmentsTestBooking;
using TechnoPro.Common.ClientManager.Core.Settings;
using TechnoPro.Common.ClientManager.ICore.AppointmentsTestBooking;
using TechnoPro.Common.Public.Entities.Adapters;
using TechnoPro.Common.Public.Entities.Settings;
using TechnoPro.Common.UI.Web.Entity.AppointmentsTestBooking.FinalExamRequest;
using TechnoPro.Common.UI.Web.Entity.Common.EventArgs;
using TechnoPro.Common.UI.Web.Entity.Common.FileUpload;
using TechnoPro.Common.UI.Web.Entity.Instructor.FinalExamRequest;

namespace TechnoPro.ClockWorkWeb.ctrls.Instructor.FinalExamUploadRequestsMode
{
	// Token: 0x02000148 RID: 328
	public class CtrlInstructorExamConfirmExamDetails : UserControl
	{
		// Token: 0x06000A04 RID: 2564 RVA: 0x00045F78 File Offset: 0x00044178
		protected void Page_Load(object sender, EventArgs e)
		{
			bool flag = !this.Page.IsPostBack;
			if (flag)
			{
				WebSettingsClientManager webSettingsClientManager = new WebSettingsClientManager();
				string settingValue = webSettingsClientManager.GetSettingValue<string>(Setting.INSTRUCTOR_ConfirmExamDetaislIntroMessage);
				this.lbl_submitinstructions.Text = settingValue;
				bool settingValue2 = webSettingsClientManager.GetSettingValue<bool>(Setting.INSTRUCTOR_InstructorConfirm_ShowStudentList);
				bool flag2 = !settingValue2;
				if (flag2)
				{
					this.p_students.Visible = false;
				}
			}
		}

		// Token: 0x14000020 RID: 32
		// (add) Token: 0x06000A05 RID: 2565 RVA: 0x00045FDC File Offset: 0x000441DC
		// (remove) Token: 0x06000A06 RID: 2566 RVA: 0x00046014 File Offset: 0x00044214
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event EventHandler<NumberEventArgs> OnLuCourseIdRequired;

		// Token: 0x06000A07 RID: 2567 RVA: 0x00046049 File Offset: 0x00044249
		public void SetSubmitReminderText(string txt)
		{
			this.lbl_submitreminder.Text = (txt ?? "");
		}

		// Token: 0x06000A08 RID: 2568 RVA: 0x00046064 File Offset: 0x00044264
		private int GetLuCourseId()
		{
			EventHandler<NumberEventArgs> onLuCourseIdRequired = this.OnLuCourseIdRequired;
			bool flag = onLuCourseIdRequired == null;
			int result;
			if (flag)
			{
				result = 0;
			}
			else
			{
				NumberEventArgs numberEventArgs = new NumberEventArgs();
				onLuCourseIdRequired(this, numberEventArgs);
				result = numberEventArgs.Number;
			}
			return result;
		}

		// Token: 0x06000A09 RID: 2569 RVA: 0x000460A0 File Offset: 0x000442A0
		public void UpdateDisplay(string courseDescription, IList<FinalExamDay> datesAndTimes, int minutesDuration, string dynamicFormInfoSummary, FileForUploadSet files, IEnumerable<ExamFileDTO> previousFiles)
		{
			this.lbl_summary_course.Text = (courseDescription ?? "");
			List<FinalExamDay> list = datesAndTimes.ToList<FinalExamDay>();
			list.Sort((FinalExamDay g1, FinalExamDay g2) => g1.Level.CompareTo(g2.Level));
			string str = string.Join("<br />\r\n", (from g in list
			select "&nbsp;&nbsp;&nbsp;" + g.Level.ToString() + ". " + g.Date.ToString("MMM d, yyyy . h:mm tt")).ToArray<string>());
			this.lbl_summary_testDateAndTime.Text = str + "<br /><br />Exam duration: " + minutesDuration.GetDurationDescription();
			string text = (dynamicFormInfoSummary ?? "").Trim();
			this.lbl_summary_testInfo.Text = ((text.Length > 0) ? text : "<i>No information has been entered yet.</i>");
			WebSettingsClientManager webSettingsClientManager = new WebSettingsClientManager();
			bool settingValue = webSettingsClientManager.GetSettingValue<bool>(Setting.INSTRUCTOR_InstructorConfirm_ShowStudentList);
			bool flag = settingValue;
			if (flag)
			{
				IList<StudentWrapper> students = this.GetStudents();
				Label label = this.lbl_studentssummary;
				string text2;
				if (students.Count <= 0)
				{
					text2 = "<i>No students have submitted final exam requests yet for this course.</i>";
				}
				else
				{
					text2 = "<ul>" + string.Join(" ", (from g in students
					select "<li>" + g.NameAndNumber + "</li>").ToArray<string>()) + "</ul>";
				}
				label.Text = text2;
			}
			FileForUploadSet fileForUploadSet;
			if (files != null && files.FilesForUpload != null)
			{
				fileForUploadSet = files;
			}
			else
			{
				(fileForUploadSet = new FileForUploadSet()).FilesForUpload = new List<FileForUpload>();
			}
			FileForUploadSet fileForUploadSet2 = fileForUploadSet;
			Label label2 = this.lbl_filesForUpload;
			string str2 = "<ul>";
			string str3;
			if (fileForUploadSet2.FilesForUpload.Count <= 0)
			{
				str3 = "<i>No new files to upload.</i>";
			}
			else
			{
				str3 = string.Join(" ", (from g in fileForUploadSet2.FilesForUpload
				select "<li>" + g.Filename + "</li>").ToArray<string>());
			}
			label2.Text = str2 + str3 + "</ul>";
			List<ExamFileDTO> list2 = (previousFiles ?? new List<ExamFileDTO>()).ToList<ExamFileDTO>();
			Label label3 = this.lbl_previousFiles;
			string str4 = "<ul>";
			string str5;
			if (list2.Count <= 0)
			{
				str5 = "<i>No previous files have been uploaded.</i>";
			}
			else
			{
				str5 = string.Join(" ", (from g in list2
				select "<li>" + ((g.File == null) ? "" : (g.File.FileName ?? "")) + "</li>").ToArray<string>());
			}
			label3.Text = str4 + str5 + "</ul>";
		}

		// Token: 0x06000A0A RID: 2570 RVA: 0x000462FC File Offset: 0x000444FC
		public IList<StudentWrapper> GetStudents()
		{
			int luCourseId = this.GetLuCourseId();
			IExamRequestClientManager examRequestClientManager = new ExamRequestClientManager();
			IList<ExamRequestDTO> source = examRequestClientManager.LoadRequestsByCourse(luCourseId);
			return (from g in source
			select new StudentWrapper(g.Student)).ToList<StudentWrapper>();
		}

		// Token: 0x040007C6 RID: 1990
		protected Label lbl_submitinstructions;

		// Token: 0x040007C7 RID: 1991
		protected Label lbl_submitreminder;

		// Token: 0x040007C8 RID: 1992
		protected Panel p_info;

		// Token: 0x040007C9 RID: 1993
		protected Label lbl_contactInfo;

		// Token: 0x040007CA RID: 1994
		protected Panel p_testDetails;

		// Token: 0x040007CB RID: 1995
		protected Label lbl_summary_course;

		// Token: 0x040007CC RID: 1996
		protected Label lbl_summary_testDateAndTime;

		// Token: 0x040007CD RID: 1997
		protected Panel p_students;

		// Token: 0x040007CE RID: 1998
		protected Label lbl_studentssummary;

		// Token: 0x040007CF RID: 1999
		protected Panel p_testInformation;

		// Token: 0x040007D0 RID: 2000
		protected Label lbl_summary_testInfo;

		// Token: 0x040007D1 RID: 2001
		protected Panel p_examCopyUpload;

		// Token: 0x040007D2 RID: 2002
		protected Label lbl_filesForUpload;

		// Token: 0x040007D3 RID: 2003
		protected Panel Panel1;

		// Token: 0x040007D4 RID: 2004
		protected Label lbl_previousFiles;

		// Token: 0x040007D5 RID: 2005
		protected LinkButton btn_printPage;
	}
}
