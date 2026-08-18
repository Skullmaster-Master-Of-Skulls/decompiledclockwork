using System;

namespace TechnoPro.Common.DAO.Impl.AppointmentsCalendar
{
	// Token: 0x02000162 RID: 354
	public static class QueryStorageAppointmentHoliday
	{
		// Token: 0x04000633 RID: 1587
		internal const string QS_ALL_OLD_RECURRING_SCHEDULE = "SELECT * FROM recurringschedule WHERE personid<0 AND isworkinghours=0";

		// Token: 0x04000634 RID: 1588
		internal const string QS_AllHolidays = "SELECT holidayid,title,description,dt FROM AppointmentsHolidays ORDER BY dt";

		// Token: 0x04000635 RID: 1589
		internal const string QI_CREATE_HOLIDAY = "IF NOT EXISTS(SELECT holidayid FROM appointmentsholidays WHERE title=@title AND dt=@dt)\r\nBEGIN\r\n    INSERT INTO appointmentsholidays (title,description,dt) VALUES (@title,@description,@dt);\r\n    SET @holidayid=(SELECT TOP 1 CAST(SCOPE_IDENTITY() AS int) AS holidayid)\r\nEND";

		// Token: 0x04000636 RID: 1590
		internal const string QU_HOLIDAY = "UPDATE appointmentsholidays SET title=@title,description=@description,dt=@dt WHERE holidayid=@holidayid";

		// Token: 0x04000637 RID: 1591
		internal const string QD_HOLIDAY = "DELETE FROM appointmentsholidays WHERE holidayid=@holidayid";
	}
}
