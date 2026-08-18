using System;
using System.Linq;

namespace TechnoPro.Common.Public.Entities.AppointmentSync.Adapters
{
	// Token: 0x020004ED RID: 1261
	public static class ClockWorkSyncAppAdapter
	{
		// Token: 0x06002626 RID: 9766 RVA: 0x00028B2C File Offset: 0x00026D2C
		public static string FirstClockWorkSyncAttendee(this ClockWorkSyncAppointment cwapp, SyncApplicationSettings syncSettings)
		{
			bool flag = cwapp == null;
			string result;
			if (flag)
			{
				result = string.Empty;
			}
			else
			{
				ClockWorkExternalApplicationSyncUser clockWorkExternalApplicationSyncUser = syncSettings.SyncUsers.FirstOrDefault((ClockWorkExternalApplicationSyncUser u) => !string.IsNullOrEmpty(u.ExternalApplicationUsername) && cwapp.Attendees.Any((ClockWorkSyncAttendee a) => a.Attendee.PersonId == u.ClockWorkUser.PersonId));
				result = ((clockWorkExternalApplicationSyncUser != null) ? clockWorkExternalApplicationSyncUser.ExternalApplicationUsername : string.Empty);
			}
			return result;
		}
	}
}
