using System;
using System.Linq;

namespace TechnoPro.Common.Public.Entities.AppointmentSync.Adapters
{
	// Token: 0x020004F0 RID: 1264
	public static class ExternalAttendeeAdapter
	{
		// Token: 0x0600262A RID: 9770 RVA: 0x00028D10 File Offset: 0x00026F10
		public static int GetPid(this ExternalAttendee att, SyncOperationContext opContext)
		{
			ClockWorkExternalApplicationSyncUser clockWorkExternalApplicationSyncUser = (opContext.SyncSettings != null && opContext.SyncSettings.SyncUsers != null) ? opContext.SyncSettings.SyncUsers.FirstOrDefault((ClockWorkExternalApplicationSyncUser u) => u.ExternalApplicationUsername.Equals(att.Username, StringComparison.OrdinalIgnoreCase)) : null;
			return (clockWorkExternalApplicationSyncUser != null && clockWorkExternalApplicationSyncUser.ClockWorkUser != null) ? clockWorkExternalApplicationSyncUser.ClockWorkUser.PersonId : 0;
		}
	}
}
