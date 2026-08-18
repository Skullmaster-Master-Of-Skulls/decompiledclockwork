using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using ClockWorkLogger;
using Databases;
using EncryptionClassLibrary;
using TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses;
using TechnoPro.ClockWorkServer.Contracts.DTO.Notetaking;
using TechnoPro.Common.ClientManager.Core.CourseRegistrations;
using TechnoPro.Common.ClientManager.Core.Notetaking;
using TechnoPro.Common.ClientManager.ICore.CourseRegistrations;
using TechnoPro.Common.ClientManager.ICore.Notetaking;
using TechnoPro.Common.Public.Entities.Settings;
using TechnoPro.Common.UI.ClientManager.Web.Auth;
using TechnoPro.Common.UI.ClientManager.Web.Core.Impl.Local.Notetaking;
using TechnoPro.Common.UI.ClientManager.Web.Core.Impl.Local.Web;
using TechnoPro.Common.UI.ClientManager.Web.Core.Notetaking;
using TechnoPro.Common.UI.Web.NotetakingStudents.Entity;
using Telerik.Web.UI;

namespace TechnoPro.ClockWorkWeb.user.NotetakingStudents
{
	// Token: 0x02000092 RID: 146
	public class user_NotetakingStudents_DownloadHistory : Page
	{
		// Token: 0x060004D3 RID: 1235 RVA: 0x000236FC File Offset: 0x000218FC
		protected void Page_Load(object sender, EventArgs e)
		{
			int pid = this.GetPid();
			bool flag = pid < 1;
			if (flag)
			{
				int notetakerPid = this.GetNotetakerPid();
				bool flag2 = notetakerPid > 0;
				if (flag2)
				{
					base.Response.Redirect("~/user/notetakingnotetakers/notetakerapp.aspx", true);
				}
				else
				{
					NavigatorClientManager.CurrentInstance.NotAllowed(Setting.GENERAL_ErrorMessage_NotAClockWorkStudent, this.Page);
				}
			}
			else
			{
				bool flag3 = !this.Page.IsPostBack;
				if (flag3)
				{
					DatabaseLayer clockWork = DatabaseLayerFactory.ClockWork;
					IEncryption encryption = clockWork.Encryption;
					string stringFromUrlParameter = NavigatorClientManager.CurrentInstance.GetStringFromUrlParameter("cd");
					this.lblSampleNotesCourse.Text = stringFromUrlParameter;
				}
			}
		}

		// Token: 0x060004D4 RID: 1236 RVA: 0x0002379C File Offset: 0x0002199C
		private int GetPid()
		{
			return WebAuthenticationAuthorizationWebClientManager.CurrentInstance.GetStudentPid(this.Page);
		}

		// Token: 0x060004D5 RID: 1237 RVA: 0x000237C0 File Offset: 0x000219C0
		private int GetNotetakerPid()
		{
			return WebAuthenticationAuthorizationWebClientManager.CurrentInstance.GetNotetakerId(this.Page);
		}

		// Token: 0x17000194 RID: 404
		// (get) Token: 0x060004D6 RID: 1238 RVA: 0x000237E2 File Offset: 0x000219E2
		private int Lucourseid
		{
			get
			{
				return NavigatorClientManager.CurrentInstance.GetIntFromUrlParameter(base.Request.QueryString["lucid"] ?? "");
			}
		}

		// Token: 0x17000195 RID: 405
		// (get) Token: 0x060004D7 RID: 1239 RVA: 0x0002380C File Offset: 0x00021A0C
		private string CourseDescription
		{
			get
			{
				return NavigatorClientManager.CurrentInstance.GetStringFromUrlParameter("cd");
			}
		}

		// Token: 0x060004D8 RID: 1240 RVA: 0x00023820 File Offset: 0x00021A20
		protected void btn_backToCourseNotes_Click(object sender, EventArgs e)
		{
			int lucourseid = this.Lucourseid;
			string courseDescription = this.CourseDescription;
			string urlParameterFromString = NavigatorClientManager.CurrentInstance.GetUrlParameterFromString(lucourseid);
			string urlParameterFromString2 = NavigatorClientManager.CurrentInstance.GetUrlParameterFromString(courseDescription);
			string url = "notesStudent.aspx?lucid=" + urlParameterFromString + "&cd=" + urlParameterFromString2;
			base.Response.Redirect(url, true);
		}

		// Token: 0x060004D9 RID: 1241 RVA: 0x00023878 File Offset: 0x00021A78
		protected void gv_courses_ItemCommand(object source, GridCommandEventArgs e)
		{
			object commandArgument = e.CommandArgument;
			bool flag = commandArgument != null;
			int num;
			if (flag)
			{
				string text = commandArgument.ToString().Trim();
				bool flag2 = text.Length > 0;
				if (flag2)
				{
					try
					{
						num = int.Parse(text);
					}
					catch
					{
						num = 0;
					}
				}
				else
				{
					num = 0;
				}
			}
			else
			{
				num = 0;
			}
			string commandName = e.CommandName;
			if (commandName == "download")
			{
				INotetakingWebClientManager notetakingWebClientManager = new NotetakingWebClientManager();
				bool flag3 = notetakingWebClientManager.DownloadLectureNoteToBrowser(num);
				bool flag4 = !flag3;
				if (flag4)
				{
					CWLogger.Logger.Warn("NotetakingStudents.notesStudent.aspx:Downloading note failed");
				}
				else
				{
					try
					{
						int pid = this.GetPid();
						INotetakingClientManager notetakingClientManager = new NotetakingClientManager();
						notetakingClientManager.RecordStudentDownloadedLectureNote(pid, num);
					}
					catch (Exception ex)
					{
						CWLogger.Logger.Error("NotetakingStudents.notesStudent.aspx:FailedToRecordStudentDownloadedLectureNote:Error={0}", ex.ToString());
					}
				}
			}
		}

		// Token: 0x060004DA RID: 1242 RVA: 0x00023974 File Offset: 0x00021B74
		protected void gv_courses_ItemCreated(object sender, GridItemEventArgs e)
		{
			bool flag = e.Item is GridDataItem;
			if (flag)
			{
				GridDataItem gridDataItem = e.Item as GridDataItem;
				TableCell tableCell = gridDataItem["col_lectureDate"];
				bool flag2 = tableCell != null;
				if (flag2)
				{
					tableCell.Attributes["scope"] = "row";
				}
			}
		}

		// Token: 0x060004DB RID: 1243 RVA: 0x000239CC File Offset: 0x00021BCC
		private void ShowMessage(string msg)
		{
			this.p_topmsg.Visible = true;
			this.lbl_topmsg.Text = msg;
		}

		// Token: 0x060004DC RID: 1244 RVA: 0x000239EC File Offset: 0x00021BEC
		protected void gv_courses_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
		{
			int pid = this.GetPid();
			bool flag = pid > 0;
			if (flag)
			{
				int lucourseid = this.Lucourseid;
				bool flag2 = lucourseid > 0;
				if (flag2)
				{
					ICourseRegistrationClientManager courseRegistrationClientManager = new CourseRegistrationClientManager();
					CourseRegistrationDTO courseRegistrationDTO = courseRegistrationClientManager.LoadCourseRegistrationsByStudentAndCourse(pid, lucourseid);
					bool flag3 = courseRegistrationDTO == null || courseRegistrationDTO.RegistrationStatus == eRegistrationStatusDTO.Dropped;
					if (flag3)
					{
						this.ShowMessage("Invalid course.");
						CWLogger.Logger.Warn("NotetakingStudents.DownloadHistory.aspx:InvalidCourse:Pid={0}:Lucid={1}", pid.ToString(), lucourseid.ToString());
						return;
					}
					INotetakingClientManager notetakingClientManager = new NotetakingClientManager();
					List<DownloadedNoteWrapper> dataSource = notetakingClientManager.LoadStudentDownloadedLectureNoteHistoryLastDateDownloadedForEachLectureNote(pid, lucourseid).ToList<DownloadedLectureNoteDTO>().ConvertAll<DownloadedNoteWrapper>((DownloadedLectureNoteDTO g) => new DownloadedNoteWrapper(g));
					this.gv_courses.DataSource = dataSource;
					return;
				}
			}
			this.gv_courses.DataSource = null;
		}

		// Token: 0x040002AD RID: 685
		protected ScriptManager bbb;

		// Token: 0x040002AE RID: 686
		protected Table tt;

		// Token: 0x040002AF RID: 687
		protected Panel p_Title;

		// Token: 0x040002B0 RID: 688
		protected Label lblTitle;

		// Token: 0x040002B1 RID: 689
		protected Label lblSampleNotesCourse;

		// Token: 0x040002B2 RID: 690
		protected Panel p_topmsg;

		// Token: 0x040002B3 RID: 691
		protected Image img_topmsg;

		// Token: 0x040002B4 RID: 692
		protected Label lbl_topmsg;

		// Token: 0x040002B5 RID: 693
		protected Button Button1;

		// Token: 0x040002B6 RID: 694
		protected Panel lbl_intro;

		// Token: 0x040002B7 RID: 695
		protected Label lbl_DownloadLectureNotesInfo;

		// Token: 0x040002B8 RID: 696
		protected Panel p_notes;

		// Token: 0x040002B9 RID: 697
		protected RadGrid gv_courses;

		// Token: 0x040002BA RID: 698
		protected Panel p_b;

		// Token: 0x040002BB RID: 699
		protected Button btn_backToCourseNotes;
	}
}
