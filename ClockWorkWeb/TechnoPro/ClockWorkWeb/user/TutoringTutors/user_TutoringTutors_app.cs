using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using TechnoPro.ClockWorkServer.Contracts.DTO.Adapters;
using TechnoPro.ClockWorkServer.Contracts.DTO.Appointments;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsList;
using TechnoPro.Common.ClientManager.Core.AppointmentsList;
using TechnoPro.Common.ClientManager.Core.DynamicForms;
using TechnoPro.Common.ClientManager.ICore.AppointmentsList;
using TechnoPro.Common.ClientManager.ICore.DynamicForms;
using TechnoPro.Common.TextFormat.Adapters;
using TechnoPro.Common.UI.ClientManager.Web.Auth;
using TechnoPro.Common.UI.ClientManager.Web.Core.Impl.Local.Tutoring;
using TechnoPro.Common.UI.ClientManager.Web.Core.Impl.Local.Web;
using TechnoPro.Common.UI.ClientManager.Web.Core.Tutoring;
using TechnoPro.Common.UI.ClientManager.Web.Core.Web;
using TechnoPro.Common.UI.Web.Entity;

namespace TechnoPro.ClockWorkWeb.user.TutoringTutors
{
	// Token: 0x02000037 RID: 55
	public class user_TutoringTutors_app : Page
	{
		// Token: 0x0600014A RID: 330 RVA: 0x00009F7C File Offset: 0x0000817C
		protected void Page_Load(object sender, EventArgs e)
		{
			int tutorPersonId = this.LookupStudentPid();
			bool flag = !this.Page.IsPostBack;
			if (flag)
			{
				ITutoringClientWebClientManager tutoringClientWebClientManager = new TutoringClientWebClientManager();
				tutoringClientWebClientManager.EnforceTutoringRedirects(tutorPersonId, this.Page, eClockWorkWebPage.TutoringTutors_Calendar);
				bool flag2 = base.Master != null && base.Master is IClockWorkMasterPage;
				if (flag2)
				{
					((IClockWorkMasterPage)base.Master).SetCurrentPage(eClockWorkWebPage.TutoringTutors_Calendar);
				}
				ListAppointmentDTO appointment = this.GetAppointment();
				bool flag3 = appointment != null;
				if (flag3)
				{
					this.lbl_date.Text = appointment.StartDate.ToString("MMMM d, yyyy");
					this.lbl_startTime.Text = appointment.StartDate.ToString("h:mm tt");
					this.lbl_duration.Text = appointment.GetDurationDescription();
					this.lbl_students.Text = ((appointment.Student == null) ? "" : appointment.Student.GetStudentName());
					bool isCancelled = appointment.IsCancelled;
					if (isCancelled)
					{
						this.rbtns_noshow.Items[2].Selected = true;
					}
					else
					{
						bool isIn = appointment.IsIn;
						if (isIn)
						{
							this.rbtns_noshow.Items[0].Selected = true;
						}
						else
						{
							bool isNoShow = appointment.IsNoShow;
							if (isNoShow)
							{
								this.rbtns_noshow.Items[1].Selected = true;
							}
						}
					}
					bool flag4 = appointment.ActualStartDateTime != null;
					if (flag4)
					{
						this.txtActualStartTime2.Text = appointment.ActualStartDateTime.Value.ToString("h:mm tt");
					}
					bool flag5 = appointment.ActualEndDateTime != null;
					if (flag5)
					{
						this.txtActualEndTime2.Text = appointment.ActualEndDateTime.Value.ToString("h:mm tt");
					}
					string text = (string.IsNullOrEmpty(appointment.Memo) ? "" : appointment.Memo.ConvertRtfToPlainText()).Trim();
					this.lbl_student_note.Text = ((text.Length > 0) ? base.Server.HtmlEncode(text) : "No note provided");
					IAppointmentNotesClientManager appointmentNotesClientManager = new AppointmentNotesClientManager();
					string text2 = appointmentNotesClientManager.LoadAppointmentNotesRtfFromFirstRtfInFirstFormAttachedToAppointmentType(this.GetStudentPid(appointment), appointment.AppointmentId, (appointment.AppType == null) ? 0 : appointment.AppType.AppTypeId);
					bool flag6 = !string.IsNullOrEmpty(text2);
					if (flag6)
					{
						this.txt_notes.Text = text2.ConvertRtfToPlainText();
					}
				}
				else
				{
					base.Response.Redirect("calendar.aspx", true);
				}
			}
		}

		// Token: 0x0600014B RID: 331 RVA: 0x0000A224 File Offset: 0x00008424
		private int LookupStudentPid()
		{
			return WebAuthenticationAuthorizationWebClientManager.CurrentInstance.GetStudentPid(this.Page);
		}

		// Token: 0x0600014C RID: 332 RVA: 0x0000A248 File Offset: 0x00008448
		private int GetAppId()
		{
			INavigatorClientManager navigatorClientManager = new NavigatorClientManager();
			return navigatorClientManager.ConvertUrlStringToIntParameter(base.Request["appid"] ?? "");
		}

		// Token: 0x0600014D RID: 333 RVA: 0x00003E0A File Offset: 0x0000200A
		private void Page_Init(object sender, EventArgs e)
		{
		}

		// Token: 0x0600014E RID: 334 RVA: 0x0000A280 File Offset: 0x00008480
		private int GetStudentPid(ListAppointmentDTO app)
		{
			bool flag = app == null || app.Student == null;
			int result;
			if (flag)
			{
				result = 0;
			}
			else
			{
				result = app.Student.PersonId;
			}
			return result;
		}

		// Token: 0x0600014F RID: 335 RVA: 0x0000A2B4 File Offset: 0x000084B4
		private int GetStudentPid()
		{
			ListAppointmentDTO appointment = this.GetAppointment();
			return this.GetStudentPid(appointment);
		}

		// Token: 0x06000150 RID: 336 RVA: 0x0000A2D4 File Offset: 0x000084D4
		private ListAppointmentDTO GetAppointment()
		{
			bool flag = this.app == null;
			if (flag)
			{
				int appId = this.GetAppId();
				IListAppointmentClientManager listAppointmentClientManager = new ListAppointmentClientManager();
				this.app = listAppointmentClientManager.LoadAppointmentById(appId, false);
			}
			return this.app;
		}

		// Token: 0x06000151 RID: 337 RVA: 0x0000A316 File Offset: 0x00008516
		private void ShowMessage(string msg)
		{
			this.lbl_msg.Text = msg;
			this.p_msg.Visible = true;
		}

		// Token: 0x06000152 RID: 338 RVA: 0x0000A333 File Offset: 0x00008533
		protected void btn_cancel_Click(object sender, EventArgs e)
		{
			this.ShowMessage("This appointment was not cancelled because you do not have permissions to cancel it.  Please contact us if you have questions.");
		}

		// Token: 0x06000153 RID: 339 RVA: 0x0000A342 File Offset: 0x00008542
		protected void btn_back_Click(object sender, EventArgs e)
		{
			base.Response.Redirect("TutorCalendar.aspx", true);
		}

		// Token: 0x06000154 RID: 340 RVA: 0x0000A357 File Offset: 0x00008557
		protected void btn_noShow_Click(object sender, EventArgs e)
		{
			this.ShowMessage("This student was not marked no-show because you do not have permissions to do so.  Please contact us if you have questions.");
		}

		// Token: 0x06000155 RID: 341 RVA: 0x0000A368 File Offset: 0x00008568
		protected void btn_submit_Click(object sender, EventArgs e)
		{
			ListAppointmentDTO appointment = this.GetAppointment();
			int studentPid = this.GetStudentPid();
			bool flag = appointment == null || studentPid < 1;
			if (!flag)
			{
				appointment.IsNoShow = this.rbtns_noshow.Items[1].Selected;
				appointment.IsIn = this.rbtns_noshow.Items[0].Selected;
				appointment.IsCancelled = this.rbtns_noshow.Items[2].Selected;
				string text = this.txtActualStartTime2.Text.Trim();
				string text2 = this.txtActualEndTime2.Text.Trim();
				string str = appointment.StartDate.ToString("yyyy-MM-dd");
				DateTime minValue;
				bool flag2 = text.Length < 1 || !DateTime.TryParse(str + " " + text, out minValue);
				if (flag2)
				{
					minValue = DateTime.MinValue;
				}
				DateTime minValue2;
				bool flag3 = text2.Length < 1 || !DateTime.TryParse(str + " " + text2, out minValue2);
				if (flag3)
				{
					minValue2 = DateTime.MinValue;
				}
				appointment.ActualStartDateTime = ((minValue == DateTime.MinValue) ? null : new DateTime?(minValue));
				appointment.ActualEndDateTime = ((minValue2 == DateTime.MinValue) ? null : new DateTime?(minValue2));
				IListAppointmentClientManager listAppointmentClientManager = new ListAppointmentClientManager();
				listAppointmentClientManager.UpdateListAppointment(appointment);
				string plainText = this.txt_notes.Text.Trim();
				IAppointmentNotesClientManager appointmentNotesClientManager = new AppointmentNotesClientManager();
				string notesRtf = plainText.ConvertPlainTextToRtf();
				IAppointmentNotesClientManager appointmentNotesClientManager2 = appointmentNotesClientManager;
				int studentPid2 = this.GetStudentPid(appointment);
				int appointmentId = appointment.AppointmentId;
				AppTypeDTO appType = appointment.AppType;
				appointmentNotesClientManager2.SaveAppointmentNotesToFirstRtfInFirstFormAttachedToAppointmentType(studentPid2, appointmentId, (appType != null) ? appType.AppTypeId : 0, notesRtf);
				base.Response.Redirect("TutorCalendar.aspx", true);
			}
		}

		// Token: 0x04000107 RID: 263
		private ListAppointmentDTO app = null;

		// Token: 0x04000108 RID: 264
		protected Panel p_msg;

		// Token: 0x04000109 RID: 265
		protected Label lbl_msg;

		// Token: 0x0400010A RID: 266
		protected Label lbl_date_title;

		// Token: 0x0400010B RID: 267
		protected TextBox lbl_date;

		// Token: 0x0400010C RID: 268
		protected Label Label1;

		// Token: 0x0400010D RID: 269
		protected TextBox lbl_startTime;

		// Token: 0x0400010E RID: 270
		protected Label Label2;

		// Token: 0x0400010F RID: 271
		protected TextBox lbl_duration;

		// Token: 0x04000110 RID: 272
		protected Label lbl_students;

		// Token: 0x04000111 RID: 273
		protected RadioButtonList rbtns_noshow;

		// Token: 0x04000112 RID: 274
		protected Label lbl_optionalNote;

		// Token: 0x04000113 RID: 275
		protected TextBox lbl_student_note;

		// Token: 0x04000114 RID: 276
		protected Label Label3;

		// Token: 0x04000115 RID: 277
		protected TextBox txt_notes;

		// Token: 0x04000116 RID: 278
		protected Label lbl_actualTimeInfo;

		// Token: 0x04000117 RID: 279
		protected TextBox txtActualStartTime2;

		// Token: 0x04000118 RID: 280
		protected TextBox txtActualEndTime2;

		// Token: 0x04000119 RID: 281
		protected Panel p_toolbar;

		// Token: 0x0400011A RID: 282
		protected Button btn_submit;

		// Token: 0x0400011B RID: 283
		protected Button btn_cancel;
	}
}
