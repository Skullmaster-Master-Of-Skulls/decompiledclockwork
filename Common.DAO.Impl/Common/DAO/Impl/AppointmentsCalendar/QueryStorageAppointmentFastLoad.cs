using System;

namespace TechnoPro.Common.DAO.Impl.AppointmentsCalendar
{
	// Token: 0x02000161 RID: 353
	public static class QueryStorageAppointmentFastLoad
	{
		// Token: 0x04000632 RID: 1586
		internal const string QS_GetCurrentFastLoadCutoffDate = "SELECT safevalue FROM miscsafedate WHERE safekey='AppFastLoadCutoffDate'";
	}
}
