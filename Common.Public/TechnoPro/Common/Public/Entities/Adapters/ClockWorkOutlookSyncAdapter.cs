using System;
using System.Collections.Generic;
using TechnoPro.Common.Public.Entities.AppointmentSync;
using TechnoPro.Common.Public.Entities.AppointmentSync.Adapters;

namespace TechnoPro.Common.Public.Entities.Adapters
{
	// Token: 0x020005D6 RID: 1494
	public static class ClockWorkOutlookSyncAdapter
	{
		// Token: 0x0600300B RID: 12299 RVA: 0x0003BB00 File Offset: 0x00039D00
		public static bool GetIsMappingAvailable(this ClockWorkExternalAppMapping Mapping)
		{
			return Mapping != null && Mapping.ClockWorkAppointmentId > 0 && !string.IsNullOrEmpty(Mapping.ExternalApplicationUniqueAppointmentId);
		}

		// Token: 0x0600300C RID: 12300 RVA: 0x0003BB30 File Offset: 0x00039D30
		public static ClockWorkExternalApplicationSyncUser GetDelegateSyncUser(this SyncApplicationSettings syncApplicationSettings)
		{
			return syncApplicationSettings.SyncUsers.Find((ClockWorkExternalApplicationSyncUser su) => su.ExternalApplicationUsername.Equals(syncApplicationSettings.SyncConnection.UserCredentials.Username, StringComparison.OrdinalIgnoreCase));
		}

		// Token: 0x0600300D RID: 12301 RVA: 0x0003BB70 File Offset: 0x00039D70
		public static string GetTitleAndSubTitle(this ClockWorkSyncAppointment Appointment)
		{
			string text = string.IsNullOrEmpty(Appointment.Subtitle) ? "" : Appointment.Subtitle;
			bool flag = Appointment.AppointmentType == null || string.IsNullOrEmpty(Appointment.AppointmentType.Description);
			string result;
			if (flag)
			{
				result = text;
			}
			else
			{
				bool flag2 = text.Length > 0;
				if (flag2)
				{
					result = string.Format("{0}: {1}", Appointment.AppointmentType.Description, Appointment.Subtitle);
				}
				else
				{
					result = "";
				}
			}
			return result;
		}

		// Token: 0x0600300E RID: 12302 RVA: 0x0003BBF4 File Offset: 0x00039DF4
		public static string GetToString(this ClockWorkSyncAppointment Appointment)
		{
			string format = "{0}: {1} to {2} . {3} . Attendees={4}";
			object[] array = new object[5];
			array[0] = Appointment.AppointmentId.ToString();
			array[1] = Appointment.StartDateTime.ToString("yyyy-MM-dd H:mm");
			array[2] = Appointment.EndDateTime.ToString("H:mm");
			array[3] = Appointment.GetTitleAndSubTitle();
			array[4] = string.Join(", ", Appointment.Attendees.ConvertAll<string>((ClockWorkSyncAttendee att) => string.Format("{0}. {1}", att.Attendee.PersonId.ToString(), att.Attendee.GetName())).ToArray());
			return string.Format(format, array);
		}

		// Token: 0x0600300F RID: 12303 RVA: 0x0003BC98 File Offset: 0x00039E98
		public static string GetToString(this ExternalAppointment Appointment)
		{
			List<ExternalAttendee> list = (List<ExternalAttendee>)Appointment.Attendees;
			string format = "{0}: {1} to {2} . {3} . Attendees={4}";
			object[] array = new object[5];
			array[0] = Appointment.UniqueId;
			array[1] = Appointment.StartDate.ToString("yyyy-MM-dd H:mm");
			array[2] = Appointment.EndDate.ToString("H:mm");
			array[3] = Appointment.Subject;
			array[4] = string.Join(", ", list.ConvertAll<string>((ExternalAttendee att) => string.Format("{0}. {1}", att.Username, att.Name)).ToArray());
			return string.Format(format, array);
		}
	}
}
