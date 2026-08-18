using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using ClockWorkLogger;
using skmValidators;
using TechnoPro.ClockWorkServer.Contracts.DTO.Adapters;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentBookingStudent.BookingRequest;
using TechnoPro.ClockWorkServer.Contracts.DTO.MailMergeEntities;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;
using TechnoPro.ClockWorkServer.Contracts.DTO.TPMailMan;
using TechnoPro.ClockWorkServer.Contracts.DTO.Tutoring;
using TechnoPro.Common.ClientManager.Core.Email;
using TechnoPro.Common.ClientManager.Core.People;
using TechnoPro.Common.ClientManager.Core.Settings;
using TechnoPro.Common.ClientManager.ICore.Email;
using TechnoPro.Common.ClientManager.ICore.People;
using TechnoPro.Common.ClientManager.ICore.Settings;
using TechnoPro.Common.ClientManager.ICore.Tutoring;
using TechnoPro.Common.Public.Entities.Settings;
using TechnoPro.Common.UI.ClientManager.Web.Auth;
using TechnoPro.Common.UI.ClientManager.Web.Core.Impl.Local.Email;
using TechnoPro.Common.UI.ClientManager.Web.Core.Impl.Local.Tutoring;
using TechnoPro.Common.UI.ClientManager.Web.Core.Impl.Local.Web;
using TechnoPro.Common.UI.Web.Entity;
using Telerik.Web.UI;

namespace TechnoPro.ClockWorkWeb.user.TutoringStudents
{
	// Token: 0x0200005D RID: 93
	public class user_TutoringStudents_TutorCalendarConfirmBooking : Page
	{
		// Token: 0x06000240 RID: 576 RVA: 0x0000D7BC File Offset: 0x0000B9BC
		protected void Page_Load(object sender, EventArgs e)
		{
			int num = this.LookupStudentPid();
			bool flag = !this.Page.IsPostBack;
			if (flag)
			{
				bool flag2 = this.Page.Master != null && this.Page.Master is IClockWorkMasterPage;
				if (flag2)
				{
					((IClockWorkMasterPage)this.Page.Master).SetCurrentPage(eClockWorkWebPage.TutoringStudents_ScheduleAppointment);
				}
				user_TutoringStudents_TutorCalendarConfirmBooking.BookingRequest bookingRequest = this.GetBookingRequest();
				bool flag3 = bookingRequest == null || bookingRequest.TutorPid < 1;
				if (flag3)
				{
					this.ReturnToSender();
				}
				else
				{
					this.btn_tutorProfile.OnClientClick = "return ShowEditForm2(" + bookingRequest.TutorPid.ToString() + ");";
					IPeopleClientManager peopleClientManager = new PeopleClientManager();
					PersonBaseDTO personBaseDTO = peopleClientManager.LoadPersonById(bookingRequest.TutorPid);
					this.lbl_date.Text = bookingRequest.StartDateTime.ToString("ddd MMMM d, yyyy");
					this.lbl_tutor.Text = ((personBaseDTO == null) ? "" : personBaseDTO.GetName());
					this.lbl_startTime.Text = bookingRequest.StartDateTime.ToString("h:mm tt");
					this.lbl_endTime.Text = bookingRequest.EndDateTime.ToString("h:mm tt");
				}
			}
		}

		// Token: 0x06000241 RID: 577 RVA: 0x0000D90F File Offset: 0x0000BB0F
		private void ReturnToSender()
		{
			NavigatorClientManager.CurrentInstance.GotoLastReturnUrl("~/user/TutoringStudents", "default.aspx");
		}

		// Token: 0x06000242 RID: 578 RVA: 0x0000D928 File Offset: 0x0000BB28
		private user_TutoringStudents_TutorCalendarConfirmBooking.BookingRequest GetBookingRequest()
		{
			object obj = this.Session["TutorBookingRequest"];
			bool flag = obj == null || !(obj is string);
			user_TutoringStudents_TutorCalendarConfirmBooking.BookingRequest result;
			if (flag)
			{
				result = null;
			}
			else
			{
				string text = (string)obj;
				bool flag2 = string.IsNullOrEmpty(text);
				if (flag2)
				{
					result = null;
				}
				else
				{
					string[] array = text.Split(new char[]
					{
						'`'
					});
					bool flag3 = array.Length < 4;
					if (flag3)
					{
						result = null;
					}
					else
					{
						string str = array[0].Trim();
						string s = str + " " + array[1];
						DateTime startDateTime;
						bool flag4 = !DateTime.TryParse(s, out startDateTime);
						if (flag4)
						{
							result = null;
						}
						else
						{
							string s2 = str + " " + array[2];
							DateTime endDateTime;
							bool flag5 = !DateTime.TryParse(s2, out endDateTime);
							if (flag5)
							{
								result = null;
							}
							else
							{
								int tutorPid;
								bool flag6 = !int.TryParse(array[3], out tutorPid);
								if (flag6)
								{
									result = null;
								}
								else
								{
									result = new user_TutoringStudents_TutorCalendarConfirmBooking.BookingRequest
									{
										StartDateTime = startDateTime,
										EndDateTime = endDateTime,
										TutorPid = tutorPid
									};
								}
							}
						}
					}
				}
			}
			return result;
		}

		// Token: 0x06000243 RID: 579 RVA: 0x0000DA40 File Offset: 0x0000BC40
		private int LookupStudentPid()
		{
			return WebAuthenticationAuthorizationWebClientManager.CurrentInstance.GetStudentPid(this.Page);
		}

		// Token: 0x06000244 RID: 580 RVA: 0x0000DA62 File Offset: 0x0000BC62
		protected void btn_cancel_Click(object sender, EventArgs e)
		{
			this.ReturnToSender();
		}

		// Token: 0x06000245 RID: 581 RVA: 0x0000DA6C File Offset: 0x0000BC6C
		protected void btn_book_Click(object sender, EventArgs e)
		{
			int num = this.LookupStudentPid();
			bool flag = num < 1;
			if (flag)
			{
				this.ReturnToSender();
			}
			else
			{
				user_TutoringStudents_TutorCalendarConfirmBooking.BookingRequest bookingRequest = this.GetBookingRequest();
				bool flag2 = bookingRequest == null || bookingRequest.TutorPid < 1;
				if (flag2)
				{
					this.ReturnToSender();
				}
				else
				{
					IWebSettingsClientManager webSettingsClientManager = new WebSettingsClientManager();
					int settingValue = webSettingsClientManager.GetSettingValue<int>(Setting.TUTORING_Appointment_Type_Id);
					AppointmentBookingReqDTO bookingRequest2 = new AppointmentBookingReqDTO
					{
						StudentPersonId = num,
						StaffPersonId = bookingRequest.TutorPid,
						AppTypeId = settingValue,
						StartDateTime = bookingRequest.StartDateTime,
						EndDateTime = bookingRequest.EndDateTime
					};
					ITutorClientManager tutorClientManager = new TutorWebClientManager();
					AppointmentBookingResDTO appointmentBookingResDTO = tutorClientManager.TryToBookTutorAppointment(bookingRequest2, true);
					bool flag3 = appointmentBookingResDTO.PassedChecks && appointmentBookingResDTO.AppointmentId > 0;
					if (flag3)
					{
						IEmailClientManager emailClientManager = new EmailClientManager();
						TutorDTO tutorDTO = tutorClientManager.LoadTutorById(bookingRequest.TutorPid);
						Dictionary<string, string> dictionary = new Dictionary<string, string>().InsertBaseUserMailMergeValues();
						dictionary.Add("tutorname", (tutorDTO == null) ? "" : tutorDTO.GetName());
						MailMergeContextDTO context = new MailMergeContextDTO
						{
							PersonId = num,
							AppointmentId = appointmentBookingResDTO.AppointmentId
						};
						SendEmailsResp sendEmailsResp = emailClientManager.SendEmail(context, Setting.TUTORING_TuteeEmail_BookingConfirmation, TechnoPro.Common.Public.Entities.Settings.Group.TUTORING, dictionary);
						base.Response.Redirect("ConfirmBooking.aspx", true);
					}
					else
					{
						this.ShowErrorMessage("Your appointment could not be booked due to the following reason:<br /><b>" + ((appointmentBookingResDTO == null || string.IsNullOrEmpty(appointmentBookingResDTO.PublicMessage)) ? "Unknown" : appointmentBookingResDTO.PublicMessage) + "</b>");
						CWLogger.Logger.Error("user/TutoringStudents/TutorCalendarConfirmBooking:CtrlBook:FailedToBookAppointment:passedChecks={0}:newAppId={1}", appointmentBookingResDTO.PassedChecks.ToString(), appointmentBookingResDTO.AppointmentId.ToString());
					}
				}
			}
		}

		// Token: 0x06000246 RID: 582 RVA: 0x0000DC38 File Offset: 0x0000BE38
		private void ShowErrorMessage(string msg)
		{
			this.p_err.Visible = true;
			this.lbl_err.Text = msg;
		}

		// Token: 0x0400019C RID: 412
		protected Panel p_err;

		// Token: 0x0400019D RID: 413
		protected Label lbl_err;

		// Token: 0x0400019E RID: 414
		protected Label lbl_info;

		// Token: 0x0400019F RID: 415
		protected Panel p_info;

		// Token: 0x040001A0 RID: 416
		protected Panel p_appDetails;

		// Token: 0x040001A1 RID: 417
		protected Label lbl_dateLabel;

		// Token: 0x040001A2 RID: 418
		protected Label lbl_date;

		// Token: 0x040001A3 RID: 419
		protected Label lbl_startTime;

		// Token: 0x040001A4 RID: 420
		protected Label lbl_endTime;

		// Token: 0x040001A5 RID: 421
		protected Label lbl_tutorLabel;

		// Token: 0x040001A6 RID: 422
		protected Label lbl_tutor;

		// Token: 0x040001A7 RID: 423
		protected LinkButton btn_tutorProfile;

		// Token: 0x040001A8 RID: 424
		protected Panel p_note;

		// Token: 0x040001A9 RID: 425
		protected Label lbl_noteLabel;

		// Token: 0x040001AA RID: 426
		protected TextBox txt_note;

		// Token: 0x040001AB RID: 427
		protected CheckBox chk_iagree;

		// Token: 0x040001AC RID: 428
		protected CheckBoxValidator cusCustom4;

		// Token: 0x040001AD RID: 429
		protected Button btn_book;

		// Token: 0x040001AE RID: 430
		protected Button btn_cancel;

		// Token: 0x040001AF RID: 431
		protected RadWindowManager RadWindowManager1;

		// Token: 0x040001B0 RID: 432
		protected RadWindow RadWindow1;

		// Token: 0x020001BC RID: 444
		internal class BookingRequest
		{
			// Token: 0x170002C3 RID: 707
			// (get) Token: 0x06000C6C RID: 3180 RVA: 0x0004DC90 File Offset: 0x0004BE90
			// (set) Token: 0x06000C6D RID: 3181 RVA: 0x0004DC98 File Offset: 0x0004BE98
			public DateTime StartDateTime { get; set; }

			// Token: 0x170002C4 RID: 708
			// (get) Token: 0x06000C6E RID: 3182 RVA: 0x0004DCA1 File Offset: 0x0004BEA1
			// (set) Token: 0x06000C6F RID: 3183 RVA: 0x0004DCA9 File Offset: 0x0004BEA9
			public DateTime EndDateTime { get; set; }

			// Token: 0x170002C5 RID: 709
			// (get) Token: 0x06000C70 RID: 3184 RVA: 0x0004DCB2 File Offset: 0x0004BEB2
			// (set) Token: 0x06000C71 RID: 3185 RVA: 0x0004DCBA File Offset: 0x0004BEBA
			public int TutorPid { get; set; }
		}
	}
}
