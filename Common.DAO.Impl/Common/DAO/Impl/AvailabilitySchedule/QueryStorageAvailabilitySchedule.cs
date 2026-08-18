using System;

namespace TechnoPro.Common.DAO.Impl.AvailabilitySchedule
{
	// Token: 0x02000120 RID: 288
	public class QueryStorageAvailabilitySchedule
	{
		// Token: 0x040004D7 RID: 1239
		internal const string QS_AVAILABILITY_BY_MULTIPLE_USERS_GROUPS_AND_DATE = "DECLARE @startdate datetime = DATEADD(D, 0, DATEDIFF(D, 0, @sd))\r\nDECLARE @enddate datetime = DATEADD(D, 1, DATEDIFF(D, 0, @ed))\r\n\r\nSELECT orderid AS personid INTO #t1 FROM splitorderids(@pids,',');\r\nSELECT orderid AS availabilitygroupid INTO #t2 FROM splitorderids(COALESCE(@gids,''),',');\r\n\r\nSELECT    av.availabilityscheduleid,av.availabilitygroupid,ag.availabilitytitle AS availabilitygrouptitle,\r\n            ag.availabilitydescription AS availabilitygroupdescription,ag.colour,ag.pattern,\r\n            av.personid,p.firstname,p.lastname,p.student_no,p.middlename,\r\n            av.availabilitydate,av.availabilitysubcode,av.availability,\r\n            NULL AS roompersonid,NULL AS roomfirstname,NULL AS roomlastname,NULL AS roomstudent_no,\r\n            av.AvailabilityBoundaries\r\nFROM        availabilityschedule av LEFT JOIN availabilitygroup ag ON ag.availabilitygroupid=av.availabilitygroupid\r\n            LEFT JOIN people p ON p.personid=av.personid\r\nWHERE       av.personid IN (SELECT personid FROM #t1) \r\n            AND av.availabilitydate>=@startdate AND av.availabilitydate<@enddate\r\n            AND (@gids IS NULL OR ag.availabilitygroupid IN (SELECT availabilitygroupid FROM #t2))\r\nORDER BY    av.personid,av.availabilitygroupid,av.availabilitydate;\r\n\r\nDROP TABLE #t1;\r\nDROP TABLE #t2";

		// Token: 0x040004D8 RID: 1240
		internal const string QS_DAYS_WITH_AVAILABILITY_BY_PERSONID = "DECLARE @startdate datetime = DATEADD(D, 0, DATEDIFF(D, 0, @sd))\r\nDECLARE @enddate datetime = DATEADD(D, 1, DATEDIFF(D, 0, @ed))\r\n\r\nSELECT orderid AS availabilitygroupid INTO #t2 FROM splitorderids(COALESCE(@gids,''),',');\r\n\r\nSELECT  DISTINCT av.availabilitydate \r\nFROM    availabilityschedule av \r\nWHERE   av.personid=@pid \r\n        AND av.availabilitygroupid IN (SELECT availabilitygroupid FROM #t2)\r\n        AND av.availabilitydate>=@startdate AND av.availabilitydate<@enddate\r\nORDER BY av.availabilitydate;\r\n\r\nDROP TABLE #t2";

		// Token: 0x040004D9 RID: 1241
		internal const string QS_AVAILABILITY_BY_CONTEXT_AND_DATE_RANGE = "DECLARE @startdate datetime = DATEADD(D, 0, DATEDIFF(D, 0, @sd))\r\nDECLARE @enddate datetime = DATEADD(D, 1, DATEDIFF(D, 0, @ed))\r\n\r\nSELECT    av.availabilityscheduleid,av.availabilitygroupid,ag.availabilitytitle AS availabilitygrouptitle,\r\n            ag.availabilitydescription AS availabilitygroupdescription,ag.colour,ag.pattern,\r\n            av.personid,p.firstname,p.lastname,p.student_no,p.middlename,\r\n            av.availabilitydate,av.availabilitysubcode,av.availability,\r\n            NULL AS roompersonid,NULL AS roomfirstname,NULL AS roomlastname,NULL AS roomstudent_no,\r\n            av.AvailabilityBoundaries\r\nFROM        availabilityschedule av LEFT JOIN availabilitygroup ag ON ag.availabilitygroupid=av.availabilitygroupid\r\n            LEFT JOIN people p ON p.personid=av.personid\r\nWHERE       av.personid=@pid \r\n            AND av.availabilitydate>=@startdate AND av.availabilitydate<@enddate\r\n            AND ag.availabilitygroupid=@groupid\r\nORDER BY    av.personid,av.availabilitygroupid,av.availabilitydate";

		// Token: 0x040004DA RID: 1242
		internal const string QS_AVAILABILITY_BY_CONTEXT_AND_SPECIFIC_DATES = "SELECT date AS dt INTO #t1 FROM splitdates(@dates)\r\n\r\nSELECT    av.availabilityscheduleid,av.availabilitygroupid,ag.availabilitytitle AS availabilitygrouptitle,\r\n            ag.availabilitydescription AS availabilitygroupdescription,ag.colour,ag.pattern,\r\n            av.personid,p.firstname,p.lastname,p.student_no,p.middlename,\r\n            av.availabilitydate,av.availabilitysubcode,av.availability,\r\n            NULL AS roompersonid,NULL AS roomfirstname,NULL AS roomlastname,NULL AS roomstudent_no,\r\n            av.AvailabilityBoundaries,#t1.dt\r\nFROM        availabilityschedule av LEFT JOIN availabilitygroup ag ON ag.availabilitygroupid=av.availabilitygroupid\r\n            LEFT JOIN people p ON p.personid=av.personid\r\n\t\t\tLEFT JOIN #t1 ON #t1.dt=av.availabilitydate\r\nWHERE       av.personid=@pid \r\n\t\t\tAND ag.availabilitygroupid=@groupid\r\n\t\t\tAND NOT #t1.dt IS NULL\r\nORDER BY    av.personid,av.availabilitygroupid,av.availabilitydate\r\n\r\nDROP TABLE #t1";

		// Token: 0x040004DB RID: 1243
		internal const string QD_AVAILABILITY_BY_CONTEXT_AND_DAY = "DELETE FROM AvailabilitySchedule WHERE personid=@pid AND availabilitygroupid=@gid AND availabilitydate=@date";

		// Token: 0x040004DC RID: 1244
		internal const string QU_AVAILABILITY_SCHEDULE_BY_CONTEXT_AND_DATE = "IF EXISTS(SELECT availabilityscheduleid FROM availabilityschedule WHERE personid=@pid AND availabilitygroupid=@gid AND availabilitydate=@date)\r\n    UPDATE availabilityschedule SET availability=@availability,AvailabilityBoundaries=@availabilityBoundaries WHERE personid=@pid AND availabilitygroupid=@gid AND availabilitydate=@date\r\nELSE \r\n    INSERT INTO availabilityschedule (personid,availabilitygroupid,availabilitydate,availability,AvailabilityBoundaries) \r\n        VALUES (@pid,@gid,@date,@availability,@availabilityBoundaries)";

		// Token: 0x040004DD RID: 1245
		internal const string QS_ALL_AVAILABILITY_GROUPS = "SELECT availabilitygroupid,availabilitytitle,availabilitydescription,colour,pattern FROM availabilitygroup ORDER BY availabilitytitle";
	}
}
