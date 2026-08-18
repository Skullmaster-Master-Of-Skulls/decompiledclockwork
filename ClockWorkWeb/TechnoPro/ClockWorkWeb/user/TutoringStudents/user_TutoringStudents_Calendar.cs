using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using TechnoPro.ClockWorkServer.Contracts.DTO.Appointments;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsCalendar;
using TechnoPro.ClockWorkServer.Contracts.DTO.MailMergeEntities;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;
using TechnoPro.ClockWorkServer.Contracts.DTO.Tutoring;
using TechnoPro.Common.ClientManager.Core.AppointmentsCalendar;
using TechnoPro.Common.ClientManager.Core.Email;
using TechnoPro.Common.ClientManager.ICore.AppointmentsCalendar;
using TechnoPro.Common.ClientManager.ICore.Email;
using TechnoPro.Common.ClientManager.ICore.Tutoring;
using TechnoPro.Common.Public.Entities.Settings;
using TechnoPro.Common.Public.Entities.Tutoring;
using TechnoPro.Common.UI.ClientManager.Web.Auth;
using TechnoPro.Common.UI.ClientManager.Web.Core.Impl.Local.Email;
using TechnoPro.Common.UI.ClientManager.Web.Core.Impl.Local.Tutoring;
using TechnoPro.Common.UI.ClientManager.Web.Core.Impl.Local.Web;
using TechnoPro.Common.UI.ClientManager.Web.Core.Tutoring;
using TechnoPro.Common.UI.Web.Entity;

namespace TechnoPro.ClockWorkWeb.user.TutoringStudents
{
	// Token: 0x02000053 RID: 83
	public class user_TutoringStudents_Calendar : Page
	{
		// Token: 0x06000206 RID: 518 RVA: 0x0000CB50 File Offset: 0x0000AD50
		protected void Page_Load(object sender, EventArgs e)
		{
			bool flag = base.Master != null && base.Master is IClockWorkMasterPage;
			if (flag)
			{
				((IClockWorkMasterPage)base.Master).SetCurrentPage(eClockWorkWebPage.TutoringStudents_Calendar);
			}
			int num = user_TutoringStudents_Calendar.LookupStudentPid();
			bool flag2 = num < 1;
			if (flag2)
			{
				NavigatorClientManager.CurrentInstance.NotAllowed(Setting.GENERAL_ErrorMessage_NotAClockWorkStudent, this.Page);
			}
			else
			{
				bool flag3 = !this.Page.IsPostBack;
				if (flag3)
				{
					ITutoringClientWebClientManager tutoringClientWebClientManager = new TutoringClientWebClientManager();
					tutoringClientWebClientManager.EnforceStudentTuteeRedirects(num, this.Page, eClockWorkWebPage.TutoringStudents_Calendar);
				}
			}
		}

		// Token: 0x06000207 RID: 519 RVA: 0x0000CBE4 File Offset: 0x0000ADE4
		[WebMethod]
		public static DeleteAppointmentResultWrapper TryDeleteAppointment(int appid)
		{
			int pid = user_TutoringStudents_Calendar.LookupStudentPid();
			bool flag = pid < 1;
			DeleteAppointmentResultWrapper result;
			if (flag)
			{
				result = new DeleteAppointmentResultWrapper
				{
					Worked = false,
					PublicMessage = "Authentication expired.  Please logout and login, then try again."
				};
			}
			else
			{
				ITutorClientManager tutorClientManager = new TutorWebClientManager();
				TutorAppointmentDTO tutorAppointmentDTO = tutorClientManager.LoadTutorAppointment(appid);
				bool flag2 = tutorAppointmentDTO == null;
				if (flag2)
				{
					result = new DeleteAppointmentResultWrapper
					{
						Worked = false,
						PublicMessage = "Appointment does not exist."
					};
				}
				else
				{
					bool flag3 = tutorAppointmentDTO.Attendees.FirstOrDefault((AttendeeDTO g) => g.Person.PersonId == pid) == null;
					if (flag3)
					{
						result = new DeleteAppointmentResultWrapper
						{
							Worked = false,
							PublicMessage = "You do not have permissions to do this."
						};
					}
					else
					{
						bool flag4 = tutorAppointmentDTO.StartDateTime < DateTime.Now;
						if (flag4)
						{
							result = new DeleteAppointmentResultWrapper
							{
								Worked = false,
								PublicMessage = "Appointment is in the past."
							};
						}
						else
						{
							AppCancelInfoDTO cancelInfo = new AppCancelInfoDTO
							{
								CancelledBy = new PersonBaseDTO
								{
									PersonId = user_TutoringStudents_Calendar.LookupStudentPid()
								},
								CancelledDate = DateTime.Now,
								CancelReason = null,
								CancelReasonText = ""
							};
							IAppointmentClientManager appointmentClientManager = new AppointmentClientManager();
							appointmentClientManager.CancelAppointment(appid, cancelInfo);
							bool flag5 = tutorAppointmentDTO.Tutor != null;
							if (flag5)
							{
								MailMergeContextDTO mailMergeContextDTO = new MailMergeContextDTO();
								mailMergeContextDTO.AltPersonId = tutorAppointmentDTO.Tutor.Person.PersonId;
								AttendeeDTO student = tutorAppointmentDTO.Student;
								int? num;
								if (student == null)
								{
									num = null;
								}
								else
								{
									PersonBaseDTO person = student.Person;
									num = ((person != null) ? new int?(person.PersonId) : null);
								}
								mailMergeContextDTO.PersonId = (num ?? 0);
								mailMergeContextDTO.AppointmentId = appid;
								MailMergeContextDTO context = mailMergeContextDTO;
								Dictionary<string, string> args = new Dictionary<string, string>().InsertBaseUserMailMergeValues();
								IEmailClientManager emailClientManager = new EmailClientManager();
								emailClientManager.SendEmail(context, Setting.TUTORING_TutorEmail_StudentCancelledAppointment, TechnoPro.Common.Public.Entities.Settings.Group.TUTORING, args);
							}
							result = new DeleteAppointmentResultWrapper
							{
								Worked = true
							};
						}
					}
				}
			}
			return result;
		}

		// Token: 0x06000208 RID: 520 RVA: 0x0000CDF8 File Offset: 0x0000AFF8
		[WebMethod]
		public static IList<CalendarAppointmentWrapper> LoadMyAppointments(string start, string end)
		{
			int pid = user_TutoringStudents_Calendar.LookupStudentPid();
			bool flag = pid < 1;
			IList<CalendarAppointmentWrapper> result;
			if (flag)
			{
				result = new List<CalendarAppointmentWrapper>();
			}
			else
			{
				DateTime startDateTime;
				DateTime endDateTime;
				bool flag2 = !DateTime.TryParse(start, out startDateTime) || !DateTime.TryParse(end, out endDateTime);
				if (flag2)
				{
					result = new List<CalendarAppointmentWrapper>();
				}
				else
				{
					IAppointmentClientManager appointmentClientManager = new AppointmentClientManager();
					IList<AppointmentDTO> source = appointmentClientManager.LoadAppointments(new List<int>
					{
						pid
					}, null, true, false, false, startDateTime, endDateTime);
					int[] tutorPersonIds = (from m in source.SelectMany((AppointmentDTO g) => g.Attendees)
					where m.Person.PersonId != pid && m.Person.CoreGroup != eCoreGroupDTO.Rooms
					select m into h
					select h.Person.PersonId).Distinct<int>().ToArray<int>();
					ITutorClientManager tutorClientManager = new TutorWebClientManager();
					IDictionary<int, eTutorStatus> tutorStatuses = tutorClientManager.GetTutorStatuses(tutorPersonIds);
					List<int> tutorPids = (from g in tutorStatuses
					where g.Value == eTutorStatus.TutorActive
					select g into m
					select m.Key).ToList<int>();
					result = (from g in source
					select new CalendarAppointmentWrapper(g, tutorPids)).ToList<CalendarAppointmentWrapper>();
				}
			}
			return result;
		}

		// Token: 0x06000209 RID: 521 RVA: 0x0000CF70 File Offset: 0x0000B170
		private static int LookupStudentPid()
		{
			return WebAuthenticationAuthorizationWebClientManager.CurrentInstance.GetStudentPid();
		}

		// Token: 0x04000189 RID: 393
		protected Label lblTitle;
	}
}
