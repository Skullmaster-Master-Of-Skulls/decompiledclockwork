using System;
using System.Collections.Generic;
using TechnoPro.Common.Public.Entities.Appointments;
using TechnoPro.Common.Public.Entities.People;

namespace TechnoPro.Common.Public.Entities.Adapters
{
	// Token: 0x020005D5 RID: 1493
	public static class AppointmentAdapter
	{
		// Token: 0x06002FFF RID: 12287 RVA: 0x0003B7C0 File Offset: 0x000399C0
		public static bool GetIsRecurring(this BaseBasicAppointment Appointment)
		{
			return Appointment.GroupCode > 0;
		}

		// Token: 0x06003000 RID: 12288 RVA: 0x0003B7DC File Offset: 0x000399DC
		public static int GetDurationInMinutes(this BaseBasicAppointment Appointment)
		{
			return (int)(Appointment.EndDateTime - Appointment.StartDateTime).TotalMinutes;
		}

		// Token: 0x06003001 RID: 12289 RVA: 0x0003B808 File Offset: 0x00039A08
		public static string GetDurationDescriptionShort(this int DurationInMinutes)
		{
			return AppointmentAdapter.GetDurationDescription(DurationInMinutes, "h", "h", "m", "m");
		}

		// Token: 0x06003002 RID: 12290 RVA: 0x0003B834 File Offset: 0x00039A34
		public static string GetDurationDescription(this int DurationInMinutes)
		{
			return AppointmentAdapter.GetDurationDescription(DurationInMinutes, "hour", "hours", "minute", "minutes");
		}

		// Token: 0x06003003 RID: 12291 RVA: 0x0003B860 File Offset: 0x00039A60
		private static string GetDurationDescription(int DurationInMinutes, string hourString, string hoursString, string minuteString, string minutesString)
		{
			int num = (int)(Convert.ToDouble(DurationInMinutes) / 60.0);
			int num2 = DurationInMinutes - num * 60;
			bool flag = num > 0 && num2 > 0;
			string result;
			if (flag)
			{
				result = ((num == 1) ? ("1 " + hourString) : (num.ToString() + " " + hoursString)) + " and " + ((num2 == 1) ? ("1 " + minuteString) : (num2.ToString() + " " + minutesString));
			}
			else
			{
				bool flag2 = num2 > 0;
				if (flag2)
				{
					result = ((num2 == 1) ? ("1 " + minuteString) : (num2.ToString() + " " + minutesString));
				}
				else
				{
					bool flag3 = num > 0;
					if (flag3)
					{
						result = ((num == 1) ? ("1 " + hourString) : (num.ToString() + " " + hoursString));
					}
					else
					{
						result = "";
					}
				}
			}
			return result;
		}

		// Token: 0x06003004 RID: 12292 RVA: 0x0003B958 File Offset: 0x00039B58
		public static int GetDefaultColourArgb(this BaseBasicAppointment Appointment)
		{
			return (Appointment.AppType == null) ? 0 : Appointment.AppType.DefaultColourArgb;
		}

		// Token: 0x06003005 RID: 12293 RVA: 0x0003B980 File Offset: 0x00039B80
		public static List<PersonBase> GetStudents(this BaseBasicAppointment Appointment)
		{
			List<Attendee> list = Appointment.Attendees.FindAll((Attendee att) => att.Person.CoreGroup == eCoreGroup.Students);
			return list.ConvertAll<PersonBase>((Attendee pb) => pb.Person);
		}

		// Token: 0x06003006 RID: 12294 RVA: 0x0003B9E4 File Offset: 0x00039BE4
		public static PersonBase GetFirstStudent(this BaseBasicAppointment Appointment)
		{
			List<PersonBase> students = Appointment.GetStudents();
			return (students.Count > 0) ? students[0] : null;
		}

		// Token: 0x06003007 RID: 12295 RVA: 0x0003BA10 File Offset: 0x00039C10
		public static List<Attendee> GetStudentAttendees(this BaseBasicAppointment Appointment)
		{
			return Appointment.Attendees.FindAll((Attendee att) => att.Person.CoreGroup == eCoreGroup.Students);
		}

		// Token: 0x06003008 RID: 12296 RVA: 0x0003BA4C File Offset: 0x00039C4C
		public static List<Attendee> GetAttendees(this BaseBasicAppointment Appointment)
		{
			return Appointment.Attendees.FindAll((Attendee att) => att.Person.CoreGroup != eCoreGroup.Rooms && att.Person.CoreGroup != eCoreGroup.Resources);
		}

		// Token: 0x06003009 RID: 12297 RVA: 0x0003BA88 File Offset: 0x00039C88
		public static List<Attendee> GetNonStudentAttendees(this BaseBasicAppointment Appointment)
		{
			return Appointment.Attendees.FindAll((Attendee att) => att.Person.CoreGroup != eCoreGroup.Rooms && att.Person.CoreGroup != eCoreGroup.Resources && att.Person.CoreGroup != eCoreGroup.Students);
		}

		// Token: 0x0600300A RID: 12298 RVA: 0x0003BAC4 File Offset: 0x00039CC4
		public static List<Attendee> GetStaffAttendees(this BaseBasicAppointment Appointment)
		{
			return Appointment.Attendees.FindAll((Attendee att) => att.Person.CoreGroup == eCoreGroup.Staff);
		}
	}
}
