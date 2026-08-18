using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using TechnoPro.ClockWorkServer.Contracts.DTO.Adapters;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentBookingStudent.BookingRequest;
using TechnoPro.ClockWorkServer.Contracts.DTO.AvailabilitySchedule;
using TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms;
using TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses;
using TechnoPro.ClockWorkServer.Contracts.DTO.MailMergeEntities;
using TechnoPro.ClockWorkServer.Contracts.DTO.TPMailMan;
using TechnoPro.ClockWorkServer.Contracts.DTO.Tutoring;
using TechnoPro.Common.ClientManager.Core.AvailabilitySchedule;
using TechnoPro.Common.ClientManager.Core.CourseRegistrations;
using TechnoPro.Common.ClientManager.Core.DynamicForms;
using TechnoPro.Common.ClientManager.Core.Email;
using TechnoPro.Common.ClientManager.Core.Settings;
using TechnoPro.Common.ClientManager.Core.Tutoring;
using TechnoPro.Common.ClientManager.ICore.AvailabilitySchedule;
using TechnoPro.Common.ClientManager.ICore.CourseRegistrations;
using TechnoPro.Common.ClientManager.ICore.DynamicForms;
using TechnoPro.Common.ClientManager.ICore.Email;
using TechnoPro.Common.ClientManager.ICore.Settings;
using TechnoPro.Common.ClientManager.ICore.Tutoring;
using TechnoPro.Common.Public.Entities.Settings;
using TechnoPro.Common.Public.Entities.Tutoring;
using TechnoPro.Common.TextFormat.Adapters;
using TechnoPro.Common.UI.ClientManager.Web.Auth;
using TechnoPro.Common.UI.ClientManager.Web.Core.Impl.Local.Email;
using TechnoPro.Common.UI.ClientManager.Web.Core.Impl.Local.LookupCourses;
using TechnoPro.Common.UI.ClientManager.Web.Core.Impl.Local.Tutoring;
using TechnoPro.Common.UI.ClientManager.Web.Core.Impl.Local.Web;
using TechnoPro.Common.UI.ClientManager.Web.Core.LookupCourses;
using TechnoPro.Common.UI.ClientManager.Web.Core.Tutoring;
using TechnoPro.Common.UI.Web.Entity;
using TechnoPro.Common.UI.Web.Entity.LookupCourses;

namespace TechnoPro.ClockWorkWeb.user.TutoringStudents
{
	// Token: 0x0200004C RID: 76
	public class user_TutoringStudents_book : Page
	{
		// Token: 0x060001CB RID: 459 RVA: 0x0000BED4 File Offset: 0x0000A0D4
		protected void Page_Load(object sender, EventArgs e)
		{
			int studentPersonId = user_TutoringStudents_book.LookupStudentPid();
			bool flag = !this.Page.IsPostBack;
			if (flag)
			{
				ITutoringClientWebClientManager tutoringClientWebClientManager = new TutoringClientWebClientManager();
				tutoringClientWebClientManager.EnforceStudentTuteeRedirects(studentPersonId, this.Page, eClockWorkWebPage.TutoringStudents_Calendar);
				int num = NavigatorClientManager.CurrentInstance.ConvertUrlStringToIntParameter(base.Request.QueryString["tpid"]);
				bool flag2 = num > 0;
				if (flag2)
				{
					this.tpid.Value = num.ToString();
				}
			}
		}

		// Token: 0x060001CC RID: 460 RVA: 0x0000BF50 File Offset: 0x0000A150
		[WebMethod]
		public static IList<CourseReg> GetStudentCourseList()
		{
			int num = user_TutoringStudents_book.LookupStudentPid();
			bool flag = num < 1;
			IList<CourseReg> result;
			if (flag)
			{
				result = null;
			}
			else
			{
				ISessionClientManager sessionClientManager = new SessionClientManager();
				SessionView currentSession = sessionClientManager.GetCurrentSession();
				ICourseRegistrationClientManager courseRegistrationClientManager = new CourseRegistrationClientManager();
				IList<CourseRegistrationDTO> list = courseRegistrationClientManager.LoadStudentsCourses(currentSession.StartDate, currentSession.EndDate, num, false);
				IList<CourseReg> list2;
				if (list == null)
				{
					list2 = null;
				}
				else
				{
					list2 = (from g in list
					select new CourseReg
					{
						LuCourseId = g.Course.LuCourseId,
						CourseTitle = g.Course.GetCourseDescription()
					}).ToList<CourseReg>();
				}
				result = list2;
			}
			return result;
		}

		// Token: 0x060001CD RID: 461 RVA: 0x0000BFD4 File Offset: 0x0000A1D4
		[WebMethod]
		public static IList<TutorDTO> FindTutors(string searchString, int lucid)
		{
			ITutorClientManager tutorClientManager = new TutorWebClientManager();
			SearchForTutorsResp searchForTutorsResp = tutorClientManager.SearchForTutors(lucid, searchString, 40);
			return (searchForTutorsResp != null) ? searchForTutorsResp.Tutors : null;
		}

		// Token: 0x060001CE RID: 462 RVA: 0x0000C004 File Offset: 0x0000A204
		[WebMethod]
		public static TutorDTO LoadTutor(int tutorId)
		{
			bool flag = tutorId < 1;
			TutorDTO result;
			if (flag)
			{
				result = null;
			}
			else
			{
				ITutorClientManager tutorClientManager = new TutorWebClientManager();
				IDictionary<int, eTutorStatus> tutorStatuses = tutorClientManager.GetTutorStatuses(new int[]
				{
					tutorId
				});
				bool flag2 = tutorStatuses == null || !tutorStatuses.ContainsKey(tutorId) || tutorStatuses[tutorId] != eTutorStatus.TutorActive;
				if (flag2)
				{
					result = null;
				}
				else
				{
					result = tutorClientManager.LoadTutorById(tutorId);
				}
			}
			return result;
		}

		// Token: 0x060001CF RID: 463 RVA: 0x0000C068 File Offset: 0x0000A268
		[WebMethod]
		public static TutorInfoWrapper LoadTutorInfo(int tutorId)
		{
			ITutorClientManager tutorClientManager = new TutorWebClientManager();
			IDictionary<int, eTutorStatus> tutorStatuses = tutorClientManager.GetTutorStatuses(new int[]
			{
				tutorId
			});
			bool flag = tutorStatuses == null || !tutorStatuses.ContainsKey(tutorId) || tutorStatuses[tutorId] != eTutorStatus.TutorActive;
			TutorInfoWrapper result;
			if (flag)
			{
				result = null;
			}
			else
			{
				TutorDTO tutorDTO = tutorClientManager.LoadTutorById(tutorId);
				bool flag2 = tutorDTO == null;
				if (flag2)
				{
					result = null;
				}
				else
				{
					IWebSettingsClientManager webSettingsClientManager = new WebSettingsClientManager();
					int settingValue = webSettingsClientManager.GetSettingValue<int>(Setting.TUTORING_BioFormNum);
					IDynamicDataClientManager dynamicDataClientManager = new DynamicDataClientManager();
					IList<DynamicDataDTO> list = dynamicDataClientManager.LoadData(new DynamicDataContextDTO
					{
						PrimaryId = tutorId
					}, new DynamicFormDTO
					{
						ScreenNum = settingValue
					});
					List<DynamicDataDTO> data = (list != null) ? list.ToList<DynamicDataDTO>() : null;
					result = new TutorInfoWrapper(tutorDTO, data);
				}
			}
			return result;
		}

		// Token: 0x060001D0 RID: 464 RVA: 0x0000C124 File Offset: 0x0000A324
		[WebMethod]
		public static BookTutoringAppointmentAttemptResult CheckIfAppCanBeBooked(int tutorPersonId, string startDateTime, string endDateTime)
		{
			return user_TutoringStudents_book.TryToBookApp(tutorPersonId, startDateTime, endDateTime, "", false);
		}

		// Token: 0x060001D1 RID: 465 RVA: 0x0000C144 File Offset: 0x0000A344
		[WebMethod]
		public static BookTutoringAppointmentAttemptResult BookAppointment(int tutorPersonId, string startDateTime, string endDateTime, string noteFromStudent)
		{
			int num = user_TutoringStudents_book.LookupStudentPid();
			bool flag = num < 1;
			BookTutoringAppointmentAttemptResult result;
			if (flag)
			{
				result = new BookTutoringAppointmentAttemptResult("Authentication is expired.");
			}
			else
			{
				ITutorClientManager tutorClientManager = new TutorWebClientManager();
				IDictionary<int, eTutorStatus> tutorStatuses = tutorClientManager.GetTutorStatuses(new int[]
				{
					tutorPersonId
				});
				bool flag2 = tutorStatuses == null || !tutorStatuses.ContainsKey(tutorPersonId) || tutorStatuses[tutorPersonId] != eTutorStatus.TutorActive;
				if (flag2)
				{
					result = new BookTutoringAppointmentAttemptResult
					{
						PassedChecks = false,
						PublicMessage = "Tutor cannot be found"
					};
				}
				else
				{
					BookTutoringAppointmentAttemptResult bookTutoringAppointmentAttemptResult = user_TutoringStudents_book.TryToBookApp(tutorPersonId, startDateTime, endDateTime, noteFromStudent, true);
					bool flag3 = bookTutoringAppointmentAttemptResult.PassedChecks && bookTutoringAppointmentAttemptResult.AppointmentId > 0;
					if (flag3)
					{
						IEmailClientManager emailClientManager = new EmailClientManager();
						Dictionary<string, string> args = new Dictionary<string, string>().InsertBaseUserMailMergeValues();
						MailMergeContextDTO context = new MailMergeContextDTO
						{
							PersonId = num,
							AppointmentId = bookTutoringAppointmentAttemptResult.AppointmentId,
							AltPersonId = tutorPersonId
						};
						SendEmailsResp sendEmailsResp = emailClientManager.SendEmail(context, Setting.TUTORING_TuteeEmail_BookingConfirmation, TechnoPro.Common.Public.Entities.Settings.Group.TUTORING, args);
						context = new MailMergeContextDTO
						{
							PersonId = tutorPersonId,
							AppointmentId = bookTutoringAppointmentAttemptResult.AppointmentId,
							AltPersonId = num
						};
						args = new Dictionary<string, string>().InsertBaseUserMailMergeValues();
						sendEmailsResp = emailClientManager.SendEmail(context, Setting.TUTORING_TutorEmail_StudentBookedAppointmentNotification, TechnoPro.Common.Public.Entities.Settings.Group.TUTORING, args);
					}
					result = bookTutoringAppointmentAttemptResult;
				}
			}
			return result;
		}

		// Token: 0x060001D2 RID: 466 RVA: 0x0000C294 File Offset: 0x0000A494
		[WebMethod]
		public static IList<MyTutorDTO> LoadMyTutors()
		{
			int num = user_TutoringStudents_book.LookupStudentPid();
			bool flag = num < 1;
			IList<MyTutorDTO> result;
			if (flag)
			{
				result = null;
			}
			else
			{
				ISessionClientManager sessionClientManager = new SessionClientManager();
				SessionView currentSession = sessionClientManager.GetCurrentSession();
				SessionView sessionView = sessionClientManager.SubtractSession(1, currentSession);
				SessionView sessionView2 = sessionClientManager.AddSession(1, currentSession);
				IStudentTuteeClientManager studentTuteeClientManager = new StudentTuteeClientManager();
				result = studentTuteeClientManager.GetStudentMyTutors(num, new DateTime?(sessionView.StartDate), new DateTime?(sessionView2.EndDate));
			}
			return result;
		}

		// Token: 0x060001D3 RID: 467 RVA: 0x0000C304 File Offset: 0x0000A504
		private static BookTutoringAppointmentAttemptResult TryToBookApp(int tutorPersonId, string startDateTime, string endDateTime, string noteFromStudent, bool actuallyBookTheAppointment)
		{
			ITutorClientManager tutorClientManager = new TutorClientManager();
			TutorDTO tutorDTO = (tutorPersonId > 0) ? tutorClientManager.LoadTutorById(tutorPersonId) : null;
			bool flag = tutorDTO == null;
			BookTutoringAppointmentAttemptResult result;
			if (flag)
			{
				result = new BookTutoringAppointmentAttemptResult("Invalid tutor");
			}
			else
			{
				int num = user_TutoringStudents_book.LookupStudentPid();
				bool flag2 = num < 1;
				if (flag2)
				{
					result = new BookTutoringAppointmentAttemptResult("Login expired - logout and login again.");
				}
				else
				{
					DateTime startDateTime2;
					DateTime endDateTime2;
					bool flag3 = !DateTime.TryParse(startDateTime, out startDateTime2) || !DateTime.TryParse(endDateTime, out endDateTime2);
					if (flag3)
					{
						result = new BookTutoringAppointmentAttemptResult("Invalid date / time.");
					}
					else
					{
						IWebSettingsClientManager webSettingsClientManager = new WebSettingsClientManager();
						int settingValue = webSettingsClientManager.GetSettingValue<int>(Setting.TUTORING_Appointment_Type_Id);
						AppointmentBookingReqDTO bookingRequest = new AppointmentBookingReqDTO
						{
							StudentPersonId = num,
							StaffPersonId = tutorPersonId,
							AppTypeId = settingValue,
							StartDateTime = startDateTime2,
							EndDateTime = endDateTime2,
							MemoRtf = ((noteFromStudent.Length < 1) ? null : noteFromStudent.ConvertPlainTextToRtf())
						};
						ITutorClientManager tutorClientManager2 = new TutorWebClientManager();
						AppointmentBookingResDTO appointmentBookingResDTO = tutorClientManager2.TryToBookTutorAppointment(bookingRequest, actuallyBookTheAppointment);
						result = new BookTutoringAppointmentAttemptResult
						{
							PassedChecks = (appointmentBookingResDTO != null && appointmentBookingResDTO.PassedChecks),
							AppointmentId = ((appointmentBookingResDTO != null) ? appointmentBookingResDTO.AppointmentId : 0),
							PublicMessage = (((appointmentBookingResDTO != null) ? appointmentBookingResDTO.PublicMessage : null) ?? "Unknown error")
						};
					}
				}
			}
			return result;
		}

		// Token: 0x060001D4 RID: 468 RVA: 0x0000C458 File Offset: 0x0000A658
		[WebMethod]
		public static IList<AvailabilityEvent> LoadAvailabilitySchedule(DateTime startDate, DateTime endDate, int[] tutorIds)
		{
			ITutoringClientWebClientManager tutoringClientWebClientManager = new TutoringClientWebClientManager();
			int agid = tutoringClientWebClientManager.TutorAvailabilityScheduleGroupId;
			ITutorClientManager tutorClientManager = new TutorWebClientManager();
			IDictionary<int, eTutorStatus> tutorStatuses = tutorClientManager.GetTutorStatuses(tutorIds);
			int[] array;
			if (tutorStatuses == null)
			{
				array = null;
			}
			else
			{
				array = (from g in tutorStatuses
				where g.Value == eTutorStatus.TutorActive
				select g into h
				select h.Key).ToArray<int>();
			}
			tutorIds = (array ?? new int[0]);
			List<AvailabilityScheduleContextDTO> contexts = (from g in tutorIds
			select new AvailabilityScheduleContextDTO
			{
				PersonId = g,
				AvailabilityGroupId = agid
			}).ToList<AvailabilityScheduleContextDTO>();
			IAvailabilityScheduleClientManager availabilityScheduleClientManager = new AvailabilityScheduleClientManager();
			IList<AvailabilityScheduleItemsForContextDTO> list = availabilityScheduleClientManager.LoadUnbookedAvailabilityItemsByMultipleContextsAndDateRange(contexts, startDate, Convert.ToInt32((endDate.Date - startDate.Date).TotalDays));
			List<AvailabilityEvent> list2 = new List<AvailabilityEvent>();
			foreach (AvailabilityScheduleItemsForContextDTO availabilityScheduleItemsForContextDTO in list)
			{
				int personId = availabilityScheduleItemsForContextDTO.Context.PersonId;
				foreach (AvailabilityScheduleItemInfoDTO availabilityScheduleItemInfoDTO in availabilityScheduleItemsForContextDTO.AvailabilityScheduleItems)
				{
					AvailabilityEvent item = new AvailabilityEvent(personId, availabilityScheduleItemInfoDTO.DayAndTime.Date, availabilityScheduleItemInfoDTO.DayAndTime.Time.StartTime, availabilityScheduleItemInfoDTO.DayAndTime.Time.EndTime, "available");
					list2.Add(item);
				}
			}
			return list2;
		}

		// Token: 0x060001D5 RID: 469 RVA: 0x0000C624 File Offset: 0x0000A824
		private static int LookupStudentPid()
		{
			return WebAuthenticationAuthorizationWebClientManager.CurrentInstance.GetStudentPid();
		}

		// Token: 0x04000170 RID: 368
		protected HtmlInputHidden tpid;
	}
}
