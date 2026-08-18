using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using ClockWorkWebAPI;
using ClockWorkWebAPIWeb;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentBookingStudent;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentBookingStudent.BookingRequest;
using TechnoPro.ClockWorkServer.Contracts.DTO.MailMergeEntities;
using TechnoPro.Common.ClientManager.Core.AppointmentsCalendar;
using TechnoPro.Common.ClientManager.Core.Email;
using TechnoPro.Common.ClientManager.Core.Settings;
using TechnoPro.Common.ClientManager.ICore.AppointmentsCalendar;
using TechnoPro.Common.ClientManager.ICore.Email;
using TechnoPro.Common.ClientManager.ICore.Settings;
using TechnoPro.Common.Public.Adapters;
using TechnoPro.Common.Public.Entities.Adapters;
using TechnoPro.Common.Public.Entities.Settings;
using TechnoPro.Common.UI.ClientManager.Web.Auth;
using TechnoPro.Common.UI.ClientManager.Web.Core.Impl.Local.Email;
using TechnoPro.Common.UI.ClientManager.Web.Core.Impl.Local.Web;

namespace TechnoPro.ClockWorkWeb.user.appt
{
	// Token: 0x020000F3 RID: 243
	public class user_TutorSchedule_ConfirmBooking : Page
	{
		// Token: 0x06000711 RID: 1809 RVA: 0x000360F0 File Offset: 0x000342F0
		private int GetPid()
		{
			return WebAuthenticationAuthorizationWebClientManager.CurrentInstance.GetStudentPid(this.Page);
		}

		// Token: 0x06000712 RID: 1810 RVA: 0x00036114 File Offset: 0x00034314
		private void CollectBookingRequest()
		{
			string s = base.Request.QueryString["agid"];
			string s2 = base.Request.QueryString["sd"];
			string s3 = base.Request.QueryString["ed"];
			int availabilityGroupId;
			bool flag = !int.TryParse(s, out availabilityGroupId);
			if (flag)
			{
				availabilityGroupId = 0;
			}
			DateTime minValue;
			bool flag2 = !DateTime.TryParse(s2, out minValue);
			if (flag2)
			{
				minValue = DateTime.MinValue;
			}
			DateTime minValue2;
			bool flag3 = !DateTime.TryParse(s3, out minValue2);
			if (flag3)
			{
				minValue2 = DateTime.MinValue;
			}
			this._bookingRequest = new user_TutorSchedule_ConfirmBooking.BookingRequest
			{
				ChannelId = base.Request.QueryString["channelId"],
				CalendarTitle = base.Request.QueryString["ctitle"],
				AvailabilityGroupId = availabilityGroupId,
				Start = minValue,
				End = minValue2
			};
			bool flag4 = string.IsNullOrWhiteSpace(this._bookingRequest.ChannelId);
			if (flag4)
			{
				this._bookingRequest.ErrorReason = user_TutorSchedule_ConfirmBooking.eInvalidRequestReason.InvalidChannelId;
			}
			else
			{
				bool flag5 = string.IsNullOrWhiteSpace(this._bookingRequest.CalendarTitle);
				if (flag5)
				{
					this._bookingRequest.ErrorReason = user_TutorSchedule_ConfirmBooking.eInvalidRequestReason.InvalidCalendarTitle;
				}
				else
				{
					bool flag6 = this._bookingRequest.AvailabilityGroupId < 1;
					if (flag6)
					{
						this._bookingRequest.ErrorReason = user_TutorSchedule_ConfirmBooking.eInvalidRequestReason.InvalidAvailabilityGroupId;
					}
					else
					{
						bool flag7 = this._bookingRequest.Start == DateTime.MinValue;
						if (flag7)
						{
							this._bookingRequest.ErrorReason = user_TutorSchedule_ConfirmBooking.eInvalidRequestReason.InvalidStart;
						}
						else
						{
							bool flag8 = this._bookingRequest.End == DateTime.MinValue;
							if (flag8)
							{
								this._bookingRequest.ErrorReason = user_TutorSchedule_ConfirmBooking.eInvalidRequestReason.InvalidEnd;
							}
							else
							{
								bool flag9 = this._bookingRequest.Start >= this._bookingRequest.End;
								if (flag9)
								{
									this._bookingRequest.ErrorReason = user_TutorSchedule_ConfirmBooking.eInvalidRequestReason.InvalidStartAndEnd;
								}
								else
								{
									this._bookingRequest.IsValid = true;
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x06000713 RID: 1811 RVA: 0x0003630C File Offset: 0x0003450C
		private void Page_Init(object sender, EventArgs e)
		{
			this.CollectBookingRequest();
			bool flag = !this._bookingRequest.IsValid;
			if (!flag)
			{
				int pid = this.GetPid();
				bool flag2 = pid < 1;
				if (flag2)
				{
					IWebSettingsClientManager webSettingsClientManager = new WebSettingsClientManager();
					bool settingValue = webSettingsClientManager.GetSettingValue<bool>(Setting.APPOINTMENTBOOKING_allowNonClockWorkStudentsToRegister);
					bool flag3 = settingValue;
					if (flag3)
					{
						NavigatorClientManager navigatorClientManager = new NavigatorClientManager();
						string query = base.Request.Url.Query;
						navigatorClientManager.SetReturnUrlSpecific("/user/appt/ConfirmBooking.aspx" + query);
						base.Response.Redirect("NewUser.aspx", true);
					}
					else
					{
						base.Response.Redirect("Message.aspx?msgcode=notallowed", true);
					}
				}
				else
				{
					IAppointmentBookingStudentClientManager appointmentBookingStudentClientManager = new AppointmentBookingStudentClientManager();
					IList<ChannelDTO> activeChannelsForStudent = appointmentBookingStudentClientManager.GetActiveChannelsForStudent(pid);
					ChannelDTO channelDTO = activeChannelsForStudent.FirstOrDefault((ChannelDTO g) => g.Id.Equals(this._bookingRequest.ChannelId, StringComparison.OrdinalIgnoreCase));
					bool flag4 = channelDTO == null;
					if (flag4)
					{
						this._bookingRequest.IsValid = false;
						this._bookingRequest.ErrorReason = user_TutorSchedule_ConfirmBooking.eInvalidRequestReason.MissingChannel;
					}
					else
					{
						ChannelAvailabilityDTO channelAvailabilityDTO = channelDTO.Availabilities.FirstOrDefault((ChannelAvailabilityDTO h) => h.AvailabilityGroupId == this._bookingRequest.AvailabilityGroupId);
						bool flag5 = channelAvailabilityDTO == null;
						if (flag5)
						{
							this._bookingRequest.IsValid = false;
							this._bookingRequest.ErrorReason = user_TutorSchedule_ConfirmBooking.eInvalidRequestReason.MissingAvailability;
						}
						else
						{
							this._screenNum = channelAvailabilityDTO.PreBookScreenNum;
							this._bookingRequest.ScreenNum = this._screenNum;
							this._bookingRequest.AvailabilityTitle = channelAvailabilityDTO.Title;
							bool flag6 = this._bookingRequest.ScreenNum > 0;
							if (flag6)
							{
								string exemptCids = "";
								DynamicControlLayoutHelper dynamicControlLayoutHelper = null;
								DynamicScreenLayout.ControlsToScreen(ref dynamicControlLayoutHelper, base.Cache, this._bookingRequest.ScreenNum, this.p_data, null, false, false, exemptCids);
							}
							else
							{
								this.p_bookingForm.Visible = false;
							}
						}
					}
				}
			}
		}

		// Token: 0x06000714 RID: 1812 RVA: 0x000364DC File Offset: 0x000346DC
		protected void Page_Load(object sender, EventArgs e)
		{
			bool flag = !this.Page.IsPostBack;
			if (flag)
			{
				IWebSettingsClientManager webSettingsClientManager = new WebSettingsClientManager();
				bool settingValue = webSettingsClientManager.GetSettingValue<bool>(Setting.APPOINTMENTBOOKING_EnableBookingAppointments);
				bool flag2 = !settingValue;
				if (flag2)
				{
					this.ShowErrorMessage("Scheduling not allowed", "Scheduling appointments is currently disabled in the system.  Please contact us if you require assistance.");
					base.Response.Redirect("Message.aspx?msgcode=notallowed", true);
				}
				else
				{
					bool flag3 = !this._bookingRequest.IsValid;
					if (flag3)
					{
						user_TutorSchedule_ConfirmBooking.InvalidRequestReasonAttribute attribute = this._bookingRequest.ErrorReason.GetAttribute<user_TutorSchedule_ConfirmBooking.InvalidRequestReasonAttribute>();
						this.ShowErrorMessage(((attribute != null) ? attribute.Title : null) ?? "", ((attribute != null) ? attribute.Message : null) ?? "");
					}
					else
					{
						this.lbl_appDescription.Text = (this._bookingRequest.AvailabilityTitle ?? "?");
						this.lbl_appDate.Text = this._bookingRequest.Start.ToString("dddd MMMM d, yyyy");
						this.lbl_appWho.Text = (this._bookingRequest.CalendarTitle ?? "?");
						int durationInMinutes = Convert.ToInt32((this._bookingRequest.End - this._bookingRequest.Start).TotalMinutes);
						string durationDescription = durationInMinutes.GetDurationDescription();
						string arg = this._bookingRequest.Start.ToString("h:mm tt");
						string arg2 = this._bookingRequest.End.ToString("h:mm tt");
						this.lbl_appTime.Text = string.Format("{0} - {1} ({2})", arg, arg2, durationDescription);
					}
				}
			}
		}

		// Token: 0x06000715 RID: 1813 RVA: 0x0003669C File Offset: 0x0003489C
		private void ShowErrorMessage(string title, string message)
		{
			this.p_err.Visible = true;
			this.lbl_err_title.Text = title;
			this.lbl_err.Text = message;
			this.btn_cancel.Text = "Go back";
			this.btn_submit.Visible = false;
			this.p_appDetails.Visible = false;
		}

		// Token: 0x06000716 RID: 1814 RVA: 0x000366FC File Offset: 0x000348FC
		protected void btn_submit_Click(object sender, EventArgs e)
		{
			int pid = this.GetPid();
			bool flag = pid <= 0;
			if (flag)
			{
				IWebSettingsClientManager webSettingsClientManager = new WebSettingsClientManager();
				bool settingValue = webSettingsClientManager.GetSettingValue<bool>(Setting.APPOINTMENTBOOKING_allowNonClockWorkStudentsToRegister);
				bool flag2 = settingValue;
				if (flag2)
				{
					base.Response.Redirect("NewUser.aspx", true);
				}
				else
				{
					base.Response.Redirect("Message.aspx?msgcode=notallowed", true);
				}
			}
			else
			{
				bool isValid = this._bookingRequest.IsValid;
				if (isValid)
				{
					IAppointmentBookingStudentClientManager appointmentBookingStudentClientManager = new AppointmentBookingStudentClientManager();
					AppointmentBookingResDTO appointmentBookingResDTO = appointmentBookingStudentClientManager.TryToBookStudentAppointment(pid, this._bookingRequest.ChannelId, this._bookingRequest.AvailabilityGroupId, this._bookingRequest.CalendarTitle, this._bookingRequest.Start, this._bookingRequest.End);
					bool flag3 = appointmentBookingResDTO == null || !appointmentBookingResDTO.PassedChecks || appointmentBookingResDTO.AppointmentId < 1;
					if (flag3)
					{
						this.ShowErrorMessage("Appointment could not be booked", ((appointmentBookingResDTO != null) ? appointmentBookingResDTO.PublicMessage : null) ?? "Error - appointment could not be booked.  Please click the 'back' button and try again or contact us for assistance.");
					}
					else
					{
						bool flag4 = this._screenNum > 0;
						if (flag4)
						{
							DynamicScreenLayout.SaveDynamicData(ScreenType.ScreenType_PerAppointment, pid, appointmentBookingResDTO.AppointmentId, this._screenNum, base.Cache, this.p_data, "");
						}
						IEmailClientManager emailClientManager = new EmailClientManager();
						Dictionary<string, string> args = new Dictionary<string, string>().InsertBaseUserMailMergeValues();
						emailClientManager.SendEmail(new MailMergeContextDTO
						{
							PersonId = pid,
							AppointmentId = appointmentBookingResDTO.AppointmentId
						}, Setting.APPOINTMENTBOOKING_email_book, TechnoPro.Common.Public.Entities.Settings.Group.APPOINTMENTBOOKING, args);
						this.ClearAppsCache();
						string url = "book.aspx?successfulBooking=1";
						base.Response.Redirect(url, true);
					}
				}
			}
		}

		// Token: 0x06000717 RID: 1815 RVA: 0x000368A0 File Offset: 0x00034AA0
		private void ClearAppsCache()
		{
			int pid = this.GetPid();
			string key = "studentapps" + pid.ToString();
			bool flag = base.Cache[key] != null;
			if (flag)
			{
				base.Cache.Remove(key);
			}
			key = "studentwaitinglist" + pid.ToString();
			bool flag2 = base.Cache[key] != null;
			if (flag2)
			{
				base.Cache.Remove(key);
			}
		}

		// Token: 0x06000718 RID: 1816 RVA: 0x0000D21E File Offset: 0x0000B41E
		protected void btn_cancel_Click(object sender, EventArgs e)
		{
			base.Response.Redirect("book.aspx", true);
		}

		// Token: 0x04000556 RID: 1366
		private int _screenNum;

		// Token: 0x04000557 RID: 1367
		private user_TutorSchedule_ConfirmBooking.BookingRequest _bookingRequest;

		// Token: 0x04000558 RID: 1368
		protected Panel p_err;

		// Token: 0x04000559 RID: 1369
		protected Label lbl_err_title;

		// Token: 0x0400055A RID: 1370
		protected Label lbl_err;

		// Token: 0x0400055B RID: 1371
		protected Panel p_appDetails;

		// Token: 0x0400055C RID: 1372
		protected Label lbl_appDate;

		// Token: 0x0400055D RID: 1373
		protected Label lbl_appTime;

		// Token: 0x0400055E RID: 1374
		protected Label lbl_appDescription;

		// Token: 0x0400055F RID: 1375
		protected Label lbl_appWho;

		// Token: 0x04000560 RID: 1376
		protected Panel p_bookingForm;

		// Token: 0x04000561 RID: 1377
		protected Panel p_data;

		// Token: 0x04000562 RID: 1378
		protected Button btn_cancel;

		// Token: 0x04000563 RID: 1379
		protected Button btn_submit;

		// Token: 0x02000224 RID: 548
		internal enum eInvalidRequestReason
		{
			// Token: 0x04000AB4 RID: 2740
			[user_TutorSchedule_ConfirmBooking.InvalidRequestReasonAttribute]
			None,
			// Token: 0x04000AB5 RID: 2741
			[user_TutorSchedule_ConfirmBooking.InvalidRequestReasonAttribute("Invalid request", "There was a problem with this request.  Please click the 'Go back' button and try again. [channel]")]
			InvalidChannelId,
			// Token: 0x04000AB6 RID: 2742
			[user_TutorSchedule_ConfirmBooking.InvalidRequestReasonAttribute("Invalid request", "There was a problem with this request.  Please click the 'Go back' button and try again. [calendar]")]
			InvalidCalendarTitle,
			// Token: 0x04000AB7 RID: 2743
			[user_TutorSchedule_ConfirmBooking.InvalidRequestReasonAttribute("Invalid request", "There was a problem with this request.  Please click the 'Go back' button and try again. [availability]")]
			InvalidAvailabilityGroupId,
			// Token: 0x04000AB8 RID: 2744
			[user_TutorSchedule_ConfirmBooking.InvalidRequestReasonAttribute("Invalid request", "There was a problem with this request.  Please click the 'Go back' button and try again. [start]")]
			InvalidStart,
			// Token: 0x04000AB9 RID: 2745
			[user_TutorSchedule_ConfirmBooking.InvalidRequestReasonAttribute("Invalid request", "There was a problem with this request.  Please click the 'Go back' button and try again. [end]")]
			InvalidEnd,
			// Token: 0x04000ABA RID: 2746
			[user_TutorSchedule_ConfirmBooking.InvalidRequestReasonAttribute("Invalid request", "There was a problem with this request.  Please click the 'Go back' button and try again. [start/end]")]
			InvalidStartAndEnd,
			// Token: 0x04000ABB RID: 2747
			[user_TutorSchedule_ConfirmBooking.InvalidRequestReasonAttribute("Invalid request", "There was a problem with this request.  Please click the 'Go back' button and try again. [Channel]")]
			MissingChannel,
			// Token: 0x04000ABC RID: 2748
			[user_TutorSchedule_ConfirmBooking.InvalidRequestReasonAttribute("Invalid request", "There was a problem with this request.  Please click the 'Go back' button and try again. [Availability]")]
			MissingAvailability,
			// Token: 0x04000ABD RID: 2749
			[user_TutorSchedule_ConfirmBooking.InvalidRequestReasonAttribute("Invalid request (passed cutoff)", "The date and time you have chosen is no longer available.  Please click the 'Go back' button and select another availability.")]
			PassedCutoff
		}

		// Token: 0x02000225 RID: 549
		internal class InvalidRequestReasonAttribute : Attribute
		{
			// Token: 0x06000E63 RID: 3683 RVA: 0x0002077F File Offset: 0x0001E97F
			public InvalidRequestReasonAttribute()
			{
			}

			// Token: 0x06000E64 RID: 3684 RVA: 0x00050837 File Offset: 0x0004EA37
			public InvalidRequestReasonAttribute(string title, string msg)
			{
				this.Title = title;
				this.Message = msg;
			}

			// Token: 0x17000338 RID: 824
			// (get) Token: 0x06000E65 RID: 3685 RVA: 0x00050851 File Offset: 0x0004EA51
			// (set) Token: 0x06000E66 RID: 3686 RVA: 0x00050859 File Offset: 0x0004EA59
			public string Title { get; set; }

			// Token: 0x17000339 RID: 825
			// (get) Token: 0x06000E67 RID: 3687 RVA: 0x00050862 File Offset: 0x0004EA62
			// (set) Token: 0x06000E68 RID: 3688 RVA: 0x0005086A File Offset: 0x0004EA6A
			public string Message { get; set; }
		}

		// Token: 0x02000226 RID: 550
		internal class BookingRequest
		{
			// Token: 0x1700033A RID: 826
			// (get) Token: 0x06000E69 RID: 3689 RVA: 0x00050873 File Offset: 0x0004EA73
			// (set) Token: 0x06000E6A RID: 3690 RVA: 0x0005087B File Offset: 0x0004EA7B
			public string ChannelId { get; set; }

			// Token: 0x1700033B RID: 827
			// (get) Token: 0x06000E6B RID: 3691 RVA: 0x00050884 File Offset: 0x0004EA84
			// (set) Token: 0x06000E6C RID: 3692 RVA: 0x0005088C File Offset: 0x0004EA8C
			public string CalendarTitle { get; set; }

			// Token: 0x1700033C RID: 828
			// (get) Token: 0x06000E6D RID: 3693 RVA: 0x00050895 File Offset: 0x0004EA95
			// (set) Token: 0x06000E6E RID: 3694 RVA: 0x0005089D File Offset: 0x0004EA9D
			public int AvailabilityGroupId { get; set; }

			// Token: 0x1700033D RID: 829
			// (get) Token: 0x06000E6F RID: 3695 RVA: 0x000508A6 File Offset: 0x0004EAA6
			// (set) Token: 0x06000E70 RID: 3696 RVA: 0x000508AE File Offset: 0x0004EAAE
			public DateTime Start { get; set; }

			// Token: 0x1700033E RID: 830
			// (get) Token: 0x06000E71 RID: 3697 RVA: 0x000508B7 File Offset: 0x0004EAB7
			// (set) Token: 0x06000E72 RID: 3698 RVA: 0x000508BF File Offset: 0x0004EABF
			public DateTime End { get; set; }

			// Token: 0x1700033F RID: 831
			// (get) Token: 0x06000E73 RID: 3699 RVA: 0x000508C8 File Offset: 0x0004EAC8
			// (set) Token: 0x06000E74 RID: 3700 RVA: 0x000508D0 File Offset: 0x0004EAD0
			public bool IsValid { get; set; }

			// Token: 0x17000340 RID: 832
			// (get) Token: 0x06000E75 RID: 3701 RVA: 0x000508D9 File Offset: 0x0004EAD9
			// (set) Token: 0x06000E76 RID: 3702 RVA: 0x000508E1 File Offset: 0x0004EAE1
			public int ScreenNum { get; set; }

			// Token: 0x17000341 RID: 833
			// (get) Token: 0x06000E77 RID: 3703 RVA: 0x000508EA File Offset: 0x0004EAEA
			// (set) Token: 0x06000E78 RID: 3704 RVA: 0x000508F2 File Offset: 0x0004EAF2
			public string AvailabilityTitle { get; set; }

			// Token: 0x17000342 RID: 834
			// (get) Token: 0x06000E79 RID: 3705 RVA: 0x000508FB File Offset: 0x0004EAFB
			// (set) Token: 0x06000E7A RID: 3706 RVA: 0x00050903 File Offset: 0x0004EB03
			public user_TutorSchedule_ConfirmBooking.eInvalidRequestReason ErrorReason { get; set; }
		}
	}
}
