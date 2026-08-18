using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TechnoPro.ClockWorkServer.Contracts.DTO.Appointments;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsCalendar;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;
using TechnoPro.Common.DataStructure;
using TechnoPro.Common.Public.Entities.Adapters;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Adapters
{
	// Token: 0x02000C7D RID: 3197
	public static class AppointmentAdapter
	{
		// Token: 0x06004295 RID: 17045 RVA: 0x0002087C File Offset: 0x0001EA7C
		public static bool GetIsAllDayApointment(this BaseBasicAppointmentDTO Appointment)
		{
			bool flag = Appointment == null;
			return !flag && (Appointment.StartDateTime.Hour == 0 && Appointment.StartDateTime.Minute == 0 && Appointment.EndDateTime.Hour == 23) && Appointment.EndDateTime.Minute == 59;
		}

		// Token: 0x06004296 RID: 17046 RVA: 0x000208E4 File Offset: 0x0001EAE4
		public static PersonBaseDTO GetRoom(this BaseBasicAppointmentDTO Appointment)
		{
			bool flag = Appointment == null;
			PersonBaseDTO result;
			if (flag)
			{
				result = null;
			}
			else
			{
				AttendeeDTO attendeeDTO = Appointment.Attendees.Find((AttendeeDTO att) => att.Person.CoreGroup == eCoreGroupDTO.Rooms);
				result = ((attendeeDTO == null) ? null : attendeeDTO.Person);
			}
			return result;
		}

		// Token: 0x06004297 RID: 17047 RVA: 0x0002093C File Offset: 0x0001EB3C
		public static List<AttendeeDTO> GetStudentAttendees(this IList<AttendeeDTO> attendees)
		{
			bool flag = attendees == null;
			List<AttendeeDTO> result;
			if (flag)
			{
				result = new List<AttendeeDTO>();
			}
			else
			{
				result = attendees.ToList<AttendeeDTO>().FindAll((AttendeeDTO att) => att.Person.CoreGroup == eCoreGroupDTO.Students);
			}
			return result;
		}

		// Token: 0x06004298 RID: 17048 RVA: 0x00020988 File Offset: 0x0001EB88
		public static List<PersonBaseDTO> GetStudents(this BaseBasicAppointmentDTO Appointment)
		{
			bool flag = Appointment == null;
			List<PersonBaseDTO> result;
			if (flag)
			{
				result = new List<PersonBaseDTO>();
			}
			else
			{
				List<AttendeeDTO> studentAttendees = Appointment.Attendees.GetStudentAttendees();
				result = studentAttendees.ConvertAll<PersonBaseDTO>((AttendeeDTO pb) => pb.Person);
			}
			return result;
		}

		// Token: 0x06004299 RID: 17049 RVA: 0x000209DC File Offset: 0x0001EBDC
		public static PersonBaseDTO GetFirstStudent(this BaseBasicAppointmentDTO Appointment)
		{
			List<PersonBaseDTO> students = Appointment.GetStudents();
			return (students.Count > 0) ? students[0] : null;
		}

		// Token: 0x0600429A RID: 17050 RVA: 0x00020A08 File Offset: 0x0001EC08
		public static List<AttendeeDTO> GetStudentAttendees(this BaseBasicAppointmentDTO Appointment)
		{
			bool flag = Appointment == null;
			List<AttendeeDTO> result;
			if (flag)
			{
				result = new List<AttendeeDTO>();
			}
			else
			{
				result = Appointment.Attendees.GetStudentAttendees();
			}
			return result;
		}

		// Token: 0x0600429B RID: 17051 RVA: 0x00020A38 File Offset: 0x0001EC38
		public static string GetAttendeesDescription(this IList<AttendeeDTO> Attendees)
		{
			bool flag = Attendees == null || Attendees.Count < 1;
			string result;
			if (flag)
			{
				result = "";
			}
			else
			{
				result = string.Join(", ", Attendees.ToList<AttendeeDTO>().ConvertAll<string>((AttendeeDTO g) => g.Person.GetName()).ToArray());
			}
			return result;
		}

		// Token: 0x0600429C RID: 17052 RVA: 0x00020AA0 File Offset: 0x0001ECA0
		public static int GetAppCode(this BaseBasicAppointmentDTO Appointment)
		{
			return (Appointment.ShowTimeAs == null) ? -1 : Appointment.ShowTimeAs.AppCode;
		}

		// Token: 0x0600429D RID: 17053 RVA: 0x00020AC8 File Offset: 0x0001ECC8
		public static int GetAppTypeId(this BaseBasicAppointmentDTO Appointment)
		{
			return (Appointment.AppType == null) ? -1 : Appointment.AppType.AppTypeId;
		}

		// Token: 0x0600429E RID: 17054 RVA: 0x00020AF0 File Offset: 0x0001ECF0
		public static string GetAppTypeDescription(this BaseBasicAppointmentDTO Appointment)
		{
			return (Appointment.AppType == null) ? "" : ((Appointment.AppType.Description == null) ? "" : Appointment.AppType.Description);
		}

		// Token: 0x0600429F RID: 17055 RVA: 0x00020B30 File Offset: 0x0001ED30
		public static bool GetIsRecurring(this BaseBasicAppointmentDTO Appointment)
		{
			return Appointment.GroupCode > 0;
		}

		// Token: 0x060042A0 RID: 17056 RVA: 0x00020B4C File Offset: 0x0001ED4C
		public static bool GetIsTentative(this BaseBasicAppointmentDTO Appointment)
		{
			return Appointment.ShowTimeAs != null && Appointment.ShowTimeAs.IsTentative;
		}

		// Token: 0x060042A1 RID: 17057 RVA: 0x00020B74 File Offset: 0x0001ED74
		public static void SetIsTentative(this BaseBasicAppointmentDTO Appointment, bool IsTentative)
		{
			bool flag = Appointment.ShowTimeAs == null;
			if (flag)
			{
				Appointment.ShowTimeAs = new AppShowTimeAsTypeDTO();
			}
			Appointment.ShowTimeAs.SetIsTentative(IsTentative);
		}

		// Token: 0x060042A2 RID: 17058 RVA: 0x00020BA8 File Offset: 0x0001EDA8
		public static bool GetIsAtLeastOneAttendeeNoShow(this BaseBasicAppointmentDTO Appointment)
		{
			bool flag = Appointment == null || Appointment.Attendees == null;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				result = (Appointment.Attendees.Find((AttendeeDTO att) => att.IsNoShow) != null);
			}
			return result;
		}

		// Token: 0x060042A3 RID: 17059 RVA: 0x00020C00 File Offset: 0x0001EE00
		public static string GetTitleAndSubtitle(this BaseBasicAppointmentDTO Appointment)
		{
			bool flag = Appointment == null;
			string result;
			if (flag)
			{
				result = "";
			}
			else
			{
				string text = (Appointment.AppType == null) ? "" : Appointment.AppType.Description;
				string text2 = Appointment.SubTitle;
				bool flag2 = text == null;
				if (flag2)
				{
					text = "";
				}
				bool flag3 = text2 == null;
				if (flag3)
				{
					text2 = "";
				}
				result = string.Format("{0}{1}{2}", text, (!string.IsNullOrEmpty(text) && !string.IsNullOrEmpty(text2)) ? " - " : "", (text2 == null) ? "" : text2);
			}
			return result;
		}

		// Token: 0x060042A4 RID: 17060 RVA: 0x00020C98 File Offset: 0x0001EE98
		public static int GetWhoBookedPersonId(this BaseExtendedAppointmentDTO Appointment)
		{
			return (Appointment.WhoBooked == null) ? 0 : Appointment.WhoBooked.PersonId;
		}

		// Token: 0x060042A5 RID: 17061 RVA: 0x00020CC0 File Offset: 0x0001EEC0
		public static bool GetIsPointOfContact(this BaseBasicAppointmentDTO Appointment)
		{
			DateTime startDateTime = Appointment.StartDateTime;
			DateTime endDateTime = Appointment.EndDateTime;
			return startDateTime.Hour == 0 && endDateTime.Hour == 1 && startDateTime.Minute == 0 && startDateTime.Minute == 0;
		}

		// Token: 0x060042A6 RID: 17062 RVA: 0x00020D0C File Offset: 0x0001EF0C
		public static int GetDurationInMinutes(this BaseBasicAppointmentDTO Appointment)
		{
			DateTime startDateTime = Appointment.StartDateTime;
			DateTime d = new DateTime(startDateTime.Year, startDateTime.Month, startDateTime.Day, Appointment.EndDateTime.Hour, Appointment.EndDateTime.Minute, 0);
			return Convert.ToInt32((d - startDateTime).TotalMinutes);
		}

		// Token: 0x060042A7 RID: 17063 RVA: 0x00020D74 File Offset: 0x0001EF74
		public static string GetDescription(this Range<DateTime> DateRange)
		{
			DateTime start = DateRange.Start;
			DateTime end = DateRange.End;
			return string.Format("{0} to {1}", start.ToString("h:mm tt"), end.ToString("h:mm tt"));
		}

		// Token: 0x060042A8 RID: 17064 RVA: 0x00020DB8 File Offset: 0x0001EFB8
		public static DateTime? GetOriginalStartDateTime(this AppointmentDTO Appointment)
		{
			return null;
		}

		// Token: 0x060042A9 RID: 17065 RVA: 0x00020DD4 File Offset: 0x0001EFD4
		public static DateTime? GetOriginalEndDateTime(this AppointmentDTO Appointment)
		{
			return null;
		}

		// Token: 0x060042AA RID: 17066 RVA: 0x00020DF0 File Offset: 0x0001EFF0
		public static int GetExamId(this AppointmentDTO Appointment)
		{
			bool flag = Appointment == null || Appointment.TestExamInfo == null;
			int result;
			if (flag)
			{
				result = 0;
			}
			else
			{
				result = Appointment.TestExamInfo.ExamId;
			}
			return result;
		}

		// Token: 0x060042AB RID: 17067 RVA: 0x00020E24 File Offset: 0x0001F024
		public static string GetDurationDescription(this BaseBasicAppointmentDTO Appointment)
		{
			int durationInMinutes = Appointment.GetDurationInMinutes();
			return durationInMinutes.GetDurationDescription();
		}

		// Token: 0x060042AC RID: 17068 RVA: 0x00020E44 File Offset: 0x0001F044
		public static string GetDurationDescriptionShort(this BaseBasicAppointmentDTO Appointment)
		{
			int durationInMinutes = Appointment.GetDurationInMinutes();
			return durationInMinutes.GetDurationDescriptionShort();
		}

		// Token: 0x060042AD RID: 17069 RVA: 0x00020E64 File Offset: 0x0001F064
		public static string TestNote(this AppointmentDTO Appointment)
		{
			bool flag = Appointment == null || Appointment.TestExamInfo == null;
			string result;
			if (flag)
			{
				result = "";
			}
			else
			{
				result = (Appointment.TestExamInfo.TestNote ?? "");
			}
			return result;
		}

		// Token: 0x060042AE RID: 17070 RVA: 0x00020EA8 File Offset: 0x0001F0A8
		public static string StudentNote(this AppointmentDTO Appointment)
		{
			bool flag = Appointment == null || Appointment.TestExamInfo == null;
			string result;
			if (flag)
			{
				result = "";
			}
			else
			{
				result = (Appointment.TestExamInfo.StudentNote ?? "");
			}
			return result;
		}

		// Token: 0x060042AF RID: 17071 RVA: 0x00020EEC File Offset: 0x0001F0EC
		public static int GetMaxAttendees(this AppointmentDTO Appointment)
		{
			return (Appointment == null || Appointment.WorkshopInfo == null) ? 0 : Appointment.WorkshopInfo.MaxAttendeeCount;
		}

		// Token: 0x060042B0 RID: 17072 RVA: 0x00020F18 File Offset: 0x0001F118
		public static int GetLucid(this AppointmentDTO Appointment)
		{
			return (Appointment == null || Appointment.TestExamInfo == null || Appointment.TestExamInfo.Course == null) ? 0 : Appointment.TestExamInfo.Course.LuCourseId;
		}

		// Token: 0x060042B1 RID: 17073 RVA: 0x00020F58 File Offset: 0x0001F158
		public static int GetWorkshopId(this AppointmentDTO Appointment)
		{
			bool flag = Appointment == null || Appointment.WorkshopInfo == null;
			int result;
			if (flag)
			{
				result = 0;
			}
			else
			{
				result = Appointment.WorkshopInfo.WorkshopId;
			}
			return result;
		}

		// Token: 0x060042B2 RID: 17074 RVA: 0x00020F8C File Offset: 0x0001F18C
		public static string GetWorkshopTitle(this AppointmentDTO Appointment)
		{
			bool flag = Appointment == null || Appointment.WorkshopInfo == null;
			string result;
			if (flag)
			{
				result = "";
			}
			else
			{
				result = (Appointment.WorkshopInfo.WorkshopTitle ?? "");
			}
			return result;
		}

		// Token: 0x060042B3 RID: 17075 RVA: 0x00020FD0 File Offset: 0x0001F1D0
		public static string GetIconListString(this IList<AppointmentIconDTO> Icons)
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (AppointmentIconDTO appointmentIconDTO in Icons)
			{
				string text = appointmentIconDTO.Icon.IconText ?? "";
				string text2 = appointmentIconDTO.Icon.IconLetterIdentifier ?? "";
				bool flag = stringBuilder.Length > 0;
				if (flag)
				{
					stringBuilder.AppendFormat(", ", Array.Empty<object>());
				}
				bool flag2 = text.Length > 0 && text2.Length > 0;
				if (flag2)
				{
					stringBuilder.AppendFormat("{0} {1}", text, text2);
				}
				else
				{
					bool flag3 = text.Length > 0;
					if (flag3)
					{
						stringBuilder.Append(text);
					}
					else
					{
						bool flag4 = text2.Length > 0;
						if (flag4)
						{
							stringBuilder.Append(text2);
						}
						else
						{
							stringBuilder.Append(appointmentIconDTO.Icon.IconNum.ToString());
						}
					}
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060042B4 RID: 17076 RVA: 0x000210F8 File Offset: 0x0001F2F8
		public static bool GetIsUserOwnerOrInAttendeesList(this AppointmentDTO app, int pid)
		{
			bool flag = app.WhoBooked != null && app.WhoBooked.PersonId == pid;
			bool result;
			if (flag)
			{
				result = true;
			}
			else
			{
				bool flag2 = app.Attendees != null && app.Attendees.FirstOrDefault((AttendeeDTO g) => g.Person.PersonId == pid) != null;
				result = flag2;
			}
			return result;
		}

		// Token: 0x060042B5 RID: 17077 RVA: 0x0002116C File Offset: 0x0001F36C
		public static bool GetIsAppointmentPrivateForUser(this AppointmentDTO Appointment, int WhoAmIPid)
		{
			bool flag = !Appointment.IsPrivate;
			return !flag && !Appointment.GetIsUserOwnerOrInAttendeesList(WhoAmIPid);
		}

		// Token: 0x060042B6 RID: 17078 RVA: 0x0002119C File Offset: 0x0001F39C
		public static bool GetIsAppointmentLockedForUser(this AppointmentDTO Appointment, int WhoAmIPid)
		{
			bool flag = !Appointment.IsLocked;
			return !flag && !Appointment.GetIsUserOwnerOrInAttendeesList(WhoAmIPid);
		}

		// Token: 0x060042B7 RID: 17079 RVA: 0x000211CC File Offset: 0x0001F3CC
		public static string GetStatus(this BaseExtendedAppointmentDTO Appointment)
		{
			bool flag = Appointment == null;
			string result;
			if (flag)
			{
				result = "";
			}
			else
			{
				bool isCancelled = Appointment.IsCancelled;
				if (isCancelled)
				{
					result = "Cancelled";
				}
				else
				{
					bool isTentative = Appointment.IsTentative;
					if (isTentative)
					{
						result = "Tentative";
					}
					else
					{
						bool isAtLeastOneAttendeeNoShow = Appointment.GetIsAtLeastOneAttendeeNoShow();
						if (isAtLeastOneAttendeeNoShow)
						{
							result = "No-show";
						}
						else
						{
							result = "";
						}
					}
				}
			}
			return result;
		}

		// Token: 0x060042B8 RID: 17080 RVA: 0x0002122C File Offset: 0x0001F42C
		public static string GetRoomDescriptionForDisplay(this AppointmentRoomDTO room)
		{
			bool flag = room == null;
			string result;
			if (flag)
			{
				result = "";
			}
			else
			{
				string roomTitle = room.RoomTitle;
				string roomDescription = room.RoomDescription;
				bool flag2 = string.IsNullOrEmpty(roomTitle);
				bool flag3 = string.IsNullOrEmpty(roomDescription);
				bool flag4 = flag3 && flag2;
				if (flag4)
				{
					result = "";
				}
				else
				{
					bool flag5 = flag3;
					if (flag5)
					{
						result = roomTitle;
					}
					else
					{
						bool flag6 = flag2;
						if (flag6)
						{
							result = roomDescription;
						}
						else
						{
							result = roomTitle + " " + roomDescription;
						}
					}
				}
			}
			return result;
		}

		// Token: 0x060042B9 RID: 17081 RVA: 0x000212A8 File Offset: 0x0001F4A8
		public static bool GetIsFacilitator(this AttendeeDTO Attendee)
		{
			return Attendee != null && (Attendee.MiscCode & 1) > 0;
		}

		// Token: 0x060042BA RID: 17082 RVA: 0x000212CC File Offset: 0x0001F4CC
		public static string GetStudentAppointmentDescriptionShort(this BaseBasicAppointmentDTO BasicAppointment, int StudentPersonId)
		{
			bool flag = BasicAppointment == null;
			string result;
			if (flag)
			{
				result = "";
			}
			else
			{
				AttendeeDTO attendeeDTO = BasicAppointment.Attendees.FirstOrDefault((AttendeeDTO g) => g.Person.PersonId == StudentPersonId && StudentPersonId > 0);
				string appTypeDescription = BasicAppointment.GetAppTypeDescription();
				List<AttendeeDTO> attendees = (from g in BasicAppointment.Attendees
				where g.Person.CoreGroup == eCoreGroupDTO.Staff || g.GetIsFacilitator()
				select g).ToList<AttendeeDTO>();
				string attendeesDescription = attendees.GetAttendeesDescription();
				List<string> list = new List<string>();
				bool isTentative = BasicAppointment.IsTentative;
				if (isTentative)
				{
					list.Add("Tentative");
				}
				bool isCancelled = BasicAppointment.IsCancelled;
				if (isCancelled)
				{
					list.Add("Cancelled");
				}
				bool flag2 = attendeeDTO != null && attendeeDTO.IsNoShow;
				if (flag2)
				{
					list.Add("No-show");
				}
				List<string> list2 = new List<string>();
				list2.Add(BasicAppointment.StartDateTime.ToString("yyyy-MM-dd h:mm tt"));
				bool flag3 = appTypeDescription.Length > 0;
				if (flag3)
				{
					list2.Add(appTypeDescription);
				}
				bool flag4 = attendeesDescription.Length > 0;
				if (flag4)
				{
					list2.Add(attendeesDescription);
				}
				bool flag5 = list.Count > 0;
				if (flag5)
				{
					list2.Add(" [" + string.Join(" ", list.ToArray()) + "]");
				}
				result = string.Join(" ", list2.ToArray());
			}
			return result;
		}
	}
}
