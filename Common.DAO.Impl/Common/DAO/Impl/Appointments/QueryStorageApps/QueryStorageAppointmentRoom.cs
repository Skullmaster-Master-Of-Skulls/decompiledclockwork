using System;

namespace TechnoPro.Common.DAO.Impl.Appointments.QueryStorageApps
{
	// Token: 0x0200013A RID: 314
	public static class QueryStorageAppointmentRoom
	{
		// Token: 0x0400056B RID: 1387
		internal const string QS_ROOMS_BY_GROUPS = "SELECT DISTINCT pg.personid,p.firstname,p.middlename,p.lastname,p.student_no\r\nFROM    peoplegroups pg LEFT JOIN people p ON p.personid=pg.personid\r\nWHERE   pg.groupid IN (SELECT orderid AS groupid FROM splitorderids(@gids,',')) \r\n        AND pg.groupid IN (SELECT groupid FROM peoplegroups WHERE groupid=3) AND p.isactive=1";

		// Token: 0x0400056C RID: 1388
		internal const string QS_ALL_ROOMS = "SELECT    pg.personid,p.firstname,p.middlename,p.lastname,p.student_no\r\nFROM        peoplegroups pg LEFT JOIN people p ON p.personid=pg.personid\r\nWHERE       pg.groupid=3 AND p.isactive=1";

		// Token: 0x0400056D RID: 1389
		internal const string QS_ROOM_BY_ID = "SELECT    p.personid,p.firstname,p.middlename,p.lastname,p.student_no\r\nFROM        people p\r\nWHERE       p.personid=@pid";

		// Token: 0x0400056E RID: 1390
		internal const string QS_ROOMS_BY_IDS_WITH_AVAILABILITY = "SELECT orderid AS personid INTO #t1 FROM splitorderids(@pids,',');\r\n\r\nSELECT    pg.personid,p.firstname,p.middlename,p.lastname,p.student_no,\r\n          CASE WHEN EXISTS(SELECT appointmentid FROM apps WHERE cancelled=0 AND personid=pg.personid AND NOT ( ( enddate<=@startdate ) OR (startdate >= @enddate ) ))\r\n            THEN CAST(0 AS bit)\r\n          ELSE CAST(1 as bit) END AS isavailable\r\nFROM      peoplegroups pg LEFT JOIN people p ON p.personid=pg.personid\r\nWHERE     pg.groupid=3 AND p.isactive=1\r\n          AND pg.personid IN (SELECT personid FROM #t1);\r\n\r\nDROP TABLE #t1";
	}
}
