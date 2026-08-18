using System;
using System.Collections.Generic;
using System.Linq;

namespace TechnoPro.Common.Public.Entities.AppointmentSync.Adapters
{
	// Token: 0x020004EF RID: 1263
	public static class ExternalAppointmentAdapter
	{
		// Token: 0x06002628 RID: 9768 RVA: 0x00028BB0 File Offset: 0x00026DB0
		public static ExternalAppointmentId ExternalAppointmentId(this ExternalAppointment exApp)
		{
			ExternalAppointmentId result;
			if (exApp == null)
			{
				result = null;
			}
			else
			{
				ExternalAppointmentId externalAppointmentId = new ExternalAppointmentId();
				externalAppointmentId.UniqueId = exApp.UniqueId;
				externalAppointmentId.UniqueId2 = exApp.UniqueId2;
				externalAppointmentId.GlobalAppId = exApp.LegacyGlobalAppointmentId;
				result = externalAppointmentId;
				externalAppointmentId.ClockWorkAppId = ((exApp.Mapping != null) ? exApp.Mapping.ClockWorkAppointmentId : 0);
			}
			return result;
		}

		// Token: 0x06002629 RID: 9769 RVA: 0x00028C14 File Offset: 0x00026E14
		public static string FirstClockWorkSyncAttendee(this ExternalAppointment app, SyncApplicationSettings syncSettings)
		{
			bool flag = app == null;
			string result;
			if (flag)
			{
				result = string.Empty;
			}
			else
			{
				List<string> allSyncUsers = (from u in syncSettings.SyncUsers
				where !string.IsNullOrEmpty(u.ExternalApplicationUsername)
				select u.ExternalApplicationUsername).ToList<string>();
				bool flag2 = app.Organizer != null && !string.IsNullOrEmpty(app.Organizer.Username) && allSyncUsers.Contains(app.Organizer.Username);
				if (flag2)
				{
					result = app.Organizer.Username;
				}
				else
				{
					ExternalAttendee externalAttendee = app.Attendees.FirstOrDefault((ExternalAttendee a) => allSyncUsers.Contains(a.Username));
					result = ((externalAttendee != null) ? (externalAttendee.Username ?? string.Empty) : string.Empty);
				}
			}
			return result;
		}
	}
}
