using System;

namespace TechnoPro.Common.DAO.Impl.Appointments
{
	// Token: 0x02000131 RID: 305
	public static class QueryStorageAttendee
	{
		// Token: 0x04000516 RID: 1302
		internal const string QI_INSERTORUPDATE_ATTENDEE = "IF NOT EXISTS(SELECT attendeeid FROM attendees WHERE AppointmentID=@appid AND PersonID=@pid)\r\n\tINSERT INTO attendees(AppointmentID,PersonID,noShow,miscCode) VALUES (@appid,@pid,@noshow,@misccode)\r\nELSE\r\n\tUPDATE attendees SET noShow=@noshow,miscCode=@misccode WHERE AppointmentID=@appid AND PersonID=@pid\r\nSET @attendeeid=(SELECT TOP 1 attendeeid FROM attendees WHERE AppointmentID=@appid AND PersonID=@pid);";

		// Token: 0x04000517 RID: 1303
		internal const string QU_UPDATE_ATTENDEE_NOSHOW_BY_APPID_PID = "UPDATE attendees SET noShow=@noshow WHERE AppointmentID=@appid AND PersonID=@pid";

		// Token: 0x04000518 RID: 1304
		internal const string QU_UPDATE_ATTENDEE_NOSHOW_BY_ATTENDEEID = "SET @appid=(SELECT appointmentid FROM attendees WHERE attendeeid=@attendeeid); UPDATE attendees SET noShow=@noshow WHERE AttendeeID=@attendeeid";

		// Token: 0x04000519 RID: 1305
		internal const string QU_UPDATE_ATTENDEE_MISCCODE_BY_APPID_PID = "UPDATE attendees SET miscCode=@misccode WHERE AppointmentID=@appid AND PersonID=@pid";

		// Token: 0x0400051A RID: 1306
		internal const string QU_UPDATE_ATTENDEE_MISCCODE_BY_ATTENDEEID = "SET @appid=(SELECT appointmentid FROM attendees WHERE attendeeid=@attendeeid); UPDATE attendees SET miscCode=@misccode WHERE AttendeeID=@attendeeid";

		// Token: 0x0400051B RID: 1307
		internal const string QD_ATTENDEE_BY_APPID_PID = "DELETE FROM attendees WHERE AppointmentID=@appid AND PersonID=@pid";

		// Token: 0x0400051C RID: 1308
		internal const string QD_ATTENDEE_BY_ATTENDEEID = "SET @appid=(SELECT appointmentid FROM attendees WHERE attendeeid=@attendeeid); DELETE FROM attendees WHERE AttendeeID=@attendeeid";

		// Token: 0x0400051D RID: 1309
		internal const string QD_ATTENDEES_NOT_IN_LIST = "DELETE FROM attendees \r\n\tWHERE AppointmentID=@appid \r\n\t\tAND NOT PersonID IN (SELECT orderid AS PersonID FROM SplitOrderIDs(@pids,',')) \r\n\t\tAND NOT PersonID IN (SELECT PersonID FROM PeopleGroups WHERE GroupID=3)";

		// Token: 0x0400051E RID: 1310
		internal const string QS_TRY_TO_REMOVE_ATTENDEES2 = "select ExternalId from appointments app\r\ninner join Attendees att on att.AppointmentID = app.AppointmentID\r\nwhere app.ExternalId is not null and att.AttendeeId in (select orderid as AttendeeId from SplitOrderIDs(@attids, ',')) and app.ExternalId = att.PersonId";

		// Token: 0x0400051F RID: 1311
		internal const string QS_TRY_TO_REMOVE_ATTENDEES = "select ExternalId from appointments where AppointmentID = @appid and ExternalId is not null and ExternalId in (select orderid as ExternalId from SplitOrderIDs(@personids, ','))";

		// Token: 0x04000520 RID: 1312
		internal const string QS_CHECK_IS_DOUBLE_BOOKED = "SELECT orderid AS apptypeid INTO #t1 FROM splitorderids(COALESCE(@apptypeids,''),',')\r\n\r\nSELECT  COUNT(DISTINCT att.appointmentid)\r\nFROM    attendees att LEFT JOIN appointments app ON app.AppointmentID=att.AppointmentID\r\nWHERE\tatt.PersonID=@pid \r\n\t\tAND app.cancelled=0\r\n\t\tAND app.startDate<@enddate AND app.endDate>@startdate\r\n        AND (@apptypeids IS NULL OR @apptypeids='' OR app.AppTypeID IN (SELECT apptypeid FROM #t1))\r\n\r\nDROP TABLE #t1";

		// Token: 0x04000521 RID: 1313
		internal const string QS_APPOINTMENTID_BY_ATTENDEEID = "SELECT appointmentid FROM attendees WHERE attendeeid=@attendeeid";

		// Token: 0x04000522 RID: 1314
		internal const string QS_DOUBLE_BOOKED_ATTENDEES = "SELECT orderid AS personid INTO #temp1 FROM splitorderids(@pids,',');\r\n\r\nSELECT DISTINCT(t1.personid) AS personid\r\nFROM #temp1 t1 LEFT JOIN attendees att ON att.personid=t1.personid\r\n    LEFT JOIN appointments a ON a.appointmentid=att.appointmentid\r\nWHERE   NOT ( ( a.enddate<@sd ) OR (a.startdate>@ed ) )\r\n        AND NOT a.appointmentid IS NULL\r\n        AND a.cancelled=0\r\n        AND NOT att.appointmentid=@appid\r\n\r\nDROP TABLE #temp1";

		// Token: 0x04000523 RID: 1315
		internal const string QS_ATTENDEE_BY_ATTENDEEID = "SELECT att.attendeeid,att.appointmentid,att.personid,att.noshow,att.misccode,p.firstname,p.lastname,p.middlename,p.student_no,MIN(pg.groupid) AS groupid\r\nFROM attendees att LEFT JOIN people p ON p.personid=att.personid\r\n    LEFT JOIN peoplegroups pg ON pg.personid=att.personid\r\nWHERE att.attendeeid=@attendeeid\r\nGROUP BY att.attendeeid,att.appointmentid,att.personid,att.noshow,att.misccode,p.firstname,p.lastname,p.middlename,p.student_no";

		// Token: 0x04000524 RID: 1316
		internal const string QS_ATTENDEE_BY_APPID_PID = "SELECT att.attendeeid,att.appointmentid,att.personid,att.noshow,att.misccode,p.firstname,p.lastname,p.middlename,p.student_no,MIN(pg.groupid) AS groupid\r\nFROM attendees att LEFT JOIN people p ON p.personid=att.personid\r\n    LEFT JOIN peoplegroups pg ON pg.personid=att.personid\r\nWHERE att.appointmentid=@appid AND att.personid=@pid\r\nGROUP BY att.attendeeid,att.appointmentid,att.personid,att.noshow,att.misccode,p.firstname,p.lastname,p.middlename,p.student_no";

		// Token: 0x04000525 RID: 1317
		internal const string QS_ATTENDEES_BY_APPOINTMENTID = "SELECT att.attendeeid,att.appointmentid,att.personid,att.noshow,att.misccode,p.firstname,p.lastname,p.middlename,p.student_no,MIN(pg.groupid) AS groupid\r\nFROM attendees att LEFT JOIN people p ON p.personid=att.personid\r\n    LEFT JOIN peoplegroups pg ON pg.personid=att.personid\r\nWHERE att.appointmentid=@appid\r\nGROUP BY att.attendeeid,att.appointmentid,att.personid,att.noshow,att.misccode,p.firstname,p.lastname,p.middlename,p.student_no";

		// Token: 0x04000526 RID: 1318
		internal const string QS_ATTENDEES_BY_APPOINTMENTIDS = "SELECT orderid AS appointmentid INTO #tappids FROM splitorderids(@appids,',')\r\n\r\nSELECT  att.attendeeid,att.appointmentid,att.personid,att.noshow,att.misccode,p.firstname,p.lastname,p.middlename,p.student_no,pg.groupid\r\nFROM    attendees att LEFT JOIN people p ON p.personid=att.personid\r\n        LEFT JOIN peoplegroups pg ON pg.personid=att.personid\r\nWHERE   att.appointmentid IN (SELECT appointmentid FROM #tappids)\r\nORDER BY att.appointmentid,att.attendeeid,att.PersonID,pg.GroupID\r\n\r\nDROP TABLE #tappids";

		// Token: 0x04000527 RID: 1319
		internal const string QS_ATTENDEES_WHO_NO_SHOWED = "SELECT att.attendeeid,att.appointmentid,att.personid,att.noshow,att.misccode,p.firstname,p.lastname,p.middlename,p.student_no,MIN(pg.groupid) AS groupid\r\nFROM apps a LEFT JOIN attendees att ON att.appointmentid=a.appointmentid\r\n        LEFT JOIN people p ON p.personid=att.personid\r\n    LEFT JOIN peoplegroups pg ON pg.personid=att.personid\r\nWHERE a.startdate>=@mindate AND att.noshow=1 AND a.cancelled=0 AND NOT a.appointmentid IN (SELECT appointmentid FROM appointmenticons WHERE iconnum=@iconid)\r\n    AND (@apptypeids='' OR a.apptypeid IN (SELECT orderid AS apptypeid FROM splitorderids(@apptypeids,',')))\r\nGROUP BY att.attendeeid,att.appointmentid,att.personid,att.noshow,att.misccode,p.firstname,p.lastname,p.middlename,p.student_no\r\nORDER BY personid,appointmentid";
	}
}
