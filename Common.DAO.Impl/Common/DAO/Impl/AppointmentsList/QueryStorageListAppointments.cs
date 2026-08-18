using System;

namespace TechnoPro.Common.DAO.Impl.AppointmentsList
{
	// Token: 0x0200015D RID: 349
	public class QueryStorageListAppointments
	{
		// Token: 0x04000613 RID: 1555
		internal const string QS_AVAILABILITY2MARKERS_ALL = "SELECT availability2markerid,markertext,markercolourargb,markerordernum FROM availability2marker ORDER BY markerordernum,markertext";

		// Token: 0x04000614 RID: 1556
		internal const string QS_CLOSED_DAYS_BY_PERSONID_AND_DATE_RANGE = "DECLARE @sd datetime, @ed datetime\r\nSET @sd = DATEADD(dd, DATEDIFF(dd,0,@startdate), 0)\r\nSET @ed = DATEADD(dd, DATEDIFF(dd,0,@enddate), 0)\r\nSET @ed = DATEADD( day,1,@ed)\r\n\r\nSELECT    c.Availability2ItemsClosedDaysId,c.personid,c.dateclosed,c.note\r\n            ,p.firstname,p.lastname\r\nFROM        Availability2ItemsClosedDays c LEFT JOIN people p ON p.personid=c.personid\r\nWHERE       c.personid IN (SELECT orderid AS personid FROM splitorderids(@pids,','))\r\n            AND c.dateclosed>=@sd AND c.dateclosed<@ed\r\nORDER BY    c.personid,c.dateclosed";

		// Token: 0x04000615 RID: 1557
		internal const string QS_AVAILABILITIES_OVERLAPPING_WITH_APPOINTMENT = "SELECT\ta.Availability2ItemId,a.personid,a.appointmentid\r\n\t\t,a.isactive,a.isavailable,apps.cancelled,apps.noshow,apps.misccode\r\n        ,a.startdatetime,a.enddatetime,a.colourargb,a.note\r\nFROM\tAvailability2Items a LEFT JOIN apps ON apps.appointmentid=a.appointmentid AND apps.personid=a.personid\r\nWHERE\ta.personid IN (SELECT orderid AS personid FROM splitorderids(@pids,','))\r\n        AND NOT ( enddatetime <= @startdate OR startdatetime >= @enddate)\r\n        AND a.isactive=1\r\n        AND a.isavailable=1\r\nORDER BY a.startdatetime";

		// Token: 0x04000616 RID: 1558
		internal const string QS_AVAILABILITIES_OVERLAPPING_DATETIME_RANGE_AND_USER = "SELECT\ta.Availability2ItemId,a.personid,a.appointmentid\r\n\t\t,apps.startDate,apps.endDate,apps.AppTypeID\r\n\t\t,apps.PersonID AS studentpersonid,pstud.lastName AS studentlastname,pstud.firstName AS studentfirstname,pstud.middleName AS studentmiddlename,pstud.student_no AS studentstudent_no \r\n\t\t,p.firstName,p.lastName\r\n        ,a.isactive,a.isavailable,apps.cancelled,apps.noshow,apps.misccode\r\n        ,a.startdatetime,a.enddatetime,a.colourargb,a.note,am.memotext AS memo\r\n        ,at.description\r\nFROM\tAvailability2Items a LEFT JOIN people p ON p.PersonID=a.PersonID \r\n\t\tLEFT JOIN apps ON apps.AppointmentID=a.appointmentid AND apps.personid IN (SELECT personid FROM PeopleGroups WHERE GroupID=1)\r\n\t\tLEFT JOIN AppointmentTypes at ON at.AppTypeID=apps.AppTypeID \r\n\t\tLEFT JOIN people pstud ON pstud.PersonID=apps.PersonID \r\n        LEFT JOIN appointmentmemos am ON am.appointmentid=apps.appointmentid\r\nWHERE\ta.personid=@pid AND NOT ( a.enddatetime <= @startdatetime OR a.startdatetime >= @enddatetime)\r\n        AND a.isactive=1\r\n        AND a.isavailable=1\r\n        AND (apps.appointmentid IS NULL OR apps.cancelled=0)\r\nORDER BY a.startdatetime";

		// Token: 0x04000617 RID: 1559
		internal const string QS_ALL_AVAILABILITIES_BETWEEN_DATES_WITH_APPID = "SELECT\ta.Availability2ItemId,a.personid,a.appointmentid\r\n\t\t,p.firstName,p.lastName\r\n        ,a.isactive,a.isavailable\r\n        ,a.startdatetime,a.enddatetime,a.colourargb,a.note\r\nFROM\tAvailability2Items a LEFT JOIN people p ON p.PersonID=a.PersonID \r\nWHERE\tNOT ( a.enddatetime <= @startdate OR a.startdatetime >= @enddate)\r\n\t\tAND a.isactive=1\r\nORDER BY a.personid,a.startdatetime";

		// Token: 0x04000618 RID: 1560
		internal const string QS_AVAILABILITY_BY_ID = "SELECT\ta.Availability2ItemId,a.personid,a.appointmentid\r\n\t\t,p.firstName,p.lastName\r\n        ,a.isactive,a.isavailable\r\n        ,a.startdatetime,a.enddatetime,a.colourargb,a.note\r\nFROM\tAvailability2Items a LEFT JOIN people p ON p.PersonID=a.PersonID \r\nWHERE\ta.Availability2ItemId=@id\r\n\t\tAND a.isactive=1\r\nORDER BY a.startdatetime";

		// Token: 0x04000619 RID: 1561
		internal const string QS_AVAILABILITY_BY_USER_AND_DATE = "DECLARE @sd datetime, @ed datetime\r\nSET @sd = DATEADD(dd, DATEDIFF(dd,0,@dt), 0)\r\nSET @ed = DATEADD( day,1,@sd)\r\n\r\nSELECT\ta.Availability2ItemId,a.personid,a.appointmentid\r\n\t\t,p.firstName,p.lastName\r\n        ,a.isactive,a.isavailable\r\n        ,a.startdatetime,a.enddatetime,a.colourargb,a.note\r\nFROM\tAvailability2Items a LEFT JOIN people p ON p.PersonID=a.PersonID \r\nWHERE\ta.personid=@pid \r\n\t\tAND a.startdatetime >= @sd AND a.startdatetime < @ed\r\n\t\tAND a.isactive=1\r\nORDER BY a.startdatetime";

		// Token: 0x0400061A RID: 1562
		internal const string QS_AVAILABILITIES_BY_USERS_AND_DATES = "SELECT\ta.Availability2ItemId,a.personid,a.appointmentid\r\n\t\t,p.firstName,p.lastName\r\n        ,a.isactive,a.isavailable\r\n        ,a.startdatetime,a.enddatetime,a.colourargb,a.note\r\nFROM\tAvailability2Items a LEFT JOIN people p ON p.PersonID=a.PersonID \r\nWHERE\ta.personid IN (SELECT orderid AS personid FROM splitorderids(@pids,','))\r\n\t\tAND a.startdatetime >= @sd AND a.startdatetime <= @ed\r\n\t\tAND a.isactive=1\r\nORDER BY a.personid,a.startdatetime";

		// Token: 0x0400061B RID: 1563
		internal const string QS_SINGLE_DAY_AVAILABILITY_STATUSES_BY_DATE_RANGE_AND_USER = "SELECT DISTINCT \r\n\tCONVERT(DATETIME, FLOOR(CONVERT(FLOAT, a.startdatetime))) AS dt,\r\n\tCASE WHEN a.appointmentid IS NULL OR a.appointmentid IN (SELECT appointmentid FROM appointments WHERE cancelled=1)\r\n\t\tTHEN CAST(1 AS bit) ELSE CAST(0 AS bit) END As HasAvailableSlot,\r\n\t--CASE WHEN a.appointmentid IS NULL THEN CAST(0 AS bit) ELSE CAST(1 AS bit) END As HasBookedSlot,\r\n\tCASE WHEN NOT c.availability2itemscloseddaysid IS NULL THEN CAST(1 as bit) ELSE CAST(0 as bit) END AS IsClosed\r\nFROM availability2items a LEFT JOIN availability2itemscloseddays c \r\n\t\tON c.personid=a.personid AND CONVERT(DATETIME, FLOOR(CONVERT(FLOAT, c.dateclosed)))=CONVERT(DATETIME, FLOOR(CONVERT(FLOAT, a.startdatetime)))\r\nWHERE a.startdatetime>=@sdt AND a.startdatetime<@edt AND a.personid=@pid\r\nORDER BY dt,HasAvailableSlot";

		// Token: 0x0400061C RID: 1564
		internal const string QS_APPOINTMENTS_BY_USERS_AND_DATES = "SELECT\tapps.appointmentid,apps.startDate,apps.endDate,apps.AppTypeID,apps.personid AS personid\r\n        ,p.firstname AS firstname,p.lastname AS lastname\r\n\t\t,att.PersonID AS studentpersonid\r\n        ,pstud.lastName AS studentlastname,pstud.firstName AS studentfirstname,pstud.middleName AS studentmiddlename,pstud.student_no AS studentstudent_no \r\n\t\t,apps.cancelled,att.noshow,att.misccode\r\n        ,am.memotext AS memo\r\n        ,at.description,apps.appcode,\r\n        0 AS FirstAppointmentId\r\nFROM\tapps LEFT JOIN AppointmentTypes at ON at.AppTypeID=apps.AppTypeID \r\n        LEFT JOIN attendees att ON att.appointmentid=apps.AppointmentID AND att.personid IN (SELECT personid FROM peoplegroups WHERE groupid=1) --students\r\n\t\tLEFT JOIN people pstud ON pstud.PersonID=att.PersonID \r\n        LEFT JOIN appointmentmemos am ON am.appointmentid=apps.appointmentid\r\n        LEFT JOIN people p ON p.personid=apps.personid\r\nWHERE\tapps.personid IN (SELECT orderid AS personid FROM splitorderids(@pids,','))\r\n\t\tAND apps.startdate >= @sd AND apps.startdate <= @ed\r\n\t\tAND apps.cancelled=0\r\nORDER BY apps.personid,apps.startdate";

		// Token: 0x0400061D RID: 1565
		internal const string QS_GET_USERS_FIRSTAPPOINTMENT = "SET @minappid=(SELECT MIN(appointmentid) FROM apps WHERE personid=@pid AND cancelled=0 AND noshow=0)";

		// Token: 0x0400061E RID: 1566
		internal const string QS_AVAILABILITY_WITHOUT_APPOINTMENTS_BY_USERS_AND_DATE = "SELECT\ta.Availability2ItemId,a.personid,a.appointmentid\r\n\t\t,p.firstName,p.lastName\r\n        ,a.isactive,a.isavailable\r\n        ,a.startdatetime,a.enddatetime,a.colourargb,a.note\r\nFROM\tAvailability2Items a LEFT JOIN people p ON p.PersonID=a.PersonID \r\n\t\tLEFT JOIN Availability2ItemsClosedDays ac ON ac.personid=a.personid AND ac.dateclosed=DATEADD(dd,DATEDIFF(dd,0,a.startdatetime),0)\r\nWHERE\ta.personid IN (SELECT orderid AS personid FROM splitorderids(@pids,','))\r\n\t\tAND a.startdatetime >= @startdate AND a.startdatetime < @enddate\r\n        AND a.appointmentid IS NULL\r\n\t\tAND a.isactive=1\r\n        AND ac.Availability2ItemsClosedDaysId IS NULL\r\nORDER BY a.startdatetime,a.personid";

		// Token: 0x0400061F RID: 1567
		internal const string QI_AVAILABILITY2MARKER = "INSERT INTO availability2marker (markertext,markercolourargb,markerordernum) VALUES (@markertext,@markercolourargb,@markerordernum);\r\nSET @availability2markerid=(SELECT CAST( SCOPE_IDENTITY() AS int))";

		// Token: 0x04000620 RID: 1568
		internal const string QI_CLOSED_DAY = "IF EXISTS(SELECT Availability2ItemsClosedDaysId FROM Availability2ItemsClosedDays WHERE personid=@pid AND dateclosed=@date)\r\nBEGIN\r\n    UPDATE Availability2ItemsClosedDays SET note=@note WHERE personid=@pid AND dateclosed=@date\r\n    SELECT Availability2ItemsClosedDaysId FROM Availability2ItemsClosedDays WHERE personid=@pid AND dateclosed=@date\r\nEND\r\nELSE\r\nBEGIN\r\n    INSERT INTO Availability2ItemsClosedDays(personid,dateclosed,note) VALUES (@pid,@date,@note);\r\n    SELECT TOP 1 CAST(SCOPE_IDENTITY() AS int) AS Availability2ItemsClosedDaysId\r\nEND";

		// Token: 0x04000621 RID: 1569
		internal const string QI_APPOINTMENT = "INSERT INTO appointments (apptypeid,startdate,enddate,cancelled,dateadded,personid,ishidden,islocked\r\n                            ,overridecolour,extraattendeescount,appcode,groupcode,actualstarttime,actualendtime\r\n                            ,location,examid,caseid,totalbreakminutes,sittingid,subject)\r\nVALUES (@apptypeid,@sdt,@edt,@cancelled,getdate(),@whobooked,0,0\r\n        ,0,0,@appcode,-1,NULL,NULL\r\n        ,NULL,NULL,NULL,0,NULL,NULL);\r\nSELECT TOP 1 CAST(SCOPE_IDENTITY() AS int) AS appointmentid;";

		// Token: 0x04000622 RID: 1570
		internal const string QI_ATTENDEE = "INSERT INTO attendees (personid,appointmentid,noshow,misccode) VALUES (@pid,@appointmentid,0,-1)";

		// Token: 0x04000623 RID: 1571
		internal const string QI_AVAILABILITY = "INSERT INTO Availability2Items (personid,startdatetime,enddatetime,appointmentid,colourargb,note,isactive,isavailable) \r\nSELECT @pid,@startdatetime,@enddatetime,NULL,@colourargb,@note,1,1\r\nWHERE NOT EXISTS(SELECT availability2itemid FROM availability2items WHERE personid=@pid AND NOT ( enddatetime <= @startdatetime OR startdatetime >= @enddatetime));\r\nSELECT CAST(SCOPE_IDENTITY() AS int) AS availability2itemid";

		// Token: 0x04000624 RID: 1572
		internal const string QI_MEMO = "INSERT INTO appointmentmemos (appointmentid,memotext,isencrypted) VALUES (@appointmentid,@memo,1)";

		// Token: 0x04000625 RID: 1573
		internal const string QU_AVAILABILITY2MARKER = "UPDATE availability2marker SET markertext=@markertext,markercolourargb=@markercolourargb,markerordernum=@markerordernum WHERE availability2markerid=@availability2markerid";

		// Token: 0x04000626 RID: 1574
		internal const string QU_AVAILABILITY = "UPDATE availability2items SET colourargb=@colourargb,note=@note,startdatetime=@startdatetime,enddatetime=@enddatetime WHERE availability2itemid=@id";

		// Token: 0x04000627 RID: 1575
		internal const string QU_AVAILABILITY_APPOINTMENT_ID = "UPDATE availability2items SET appointmentid=@appointmentid WHERE availability2itemid=@availability2itemid";

		// Token: 0x04000628 RID: 1576
		internal const string QU_APPOINTMENT = "UPDATE appointments SET apptypeid=@apptypeid,startdate=@sdt,enddate=@edt,appcode=@appcode,cancelled=@cancelled\r\nWHERE appointmentid=@appointmentid";

		// Token: 0x04000629 RID: 1577
		internal const string QU_ATTENDEES = "DELETE FROM attendees WHERE appointmentid=@appointmentid AND NOT (personid=@staffpid OR personid=@studentpid);\r\n\r\nIF NOT EXISTS(SELECT attendeeid FROM attendees WHERE appointmentid=@appointmentid AND personid=@staffpid)\r\n    INSERT INTO attendees (personid,appointmentid,noshow,misccode) VALUES (@staffpid,@appointmentid,0,-1)\r\n\r\nIF NOT EXISTS(SELECT attendeeid FROM attendees WHERE appointmentid=@appointmentid AND personid=@studentpid)\r\n    INSERT INTO attendees (personid,appointmentid,noshow,misccode) VALUES (@studentpid,@appointmentid,@noshow,@misccode)\r\nELSE\r\n    UPDATE attendees SET noshow=@noshow,misccode=@misccode WHERE appointmentid=@appointmentid AND personid=@studentpid";

		// Token: 0x0400062A RID: 1578
		internal const string QU_MEMO = "IF NOT EXISTS(SELECT appointmentid FROM appointmentmemos WHERE appointmentid=@appointmentid)\r\n    INSERT INTO appointmentmemos (appointmentid,memotext,isencrypted) VALUES (@appointmentid,@memo,1)\r\nELSE\r\n    UPDATE appointmentmemos SET memotext=@memo WHERE appointmentid=@appointmentid";

		// Token: 0x0400062B RID: 1579
		internal const string QU_CLEAR_AVAILABILITY = "DECLARE @id int\r\nSET @id = (SELECT TOP 1 availability2itemid FROM availability2items WHERE appointmentid=@appid ORDER BY startdatetime)\r\nIF ( NOT @id IS NULL )\r\n    UPDATE availability2items SET appointmentid=NULL WHERE availability2itemid=@id AND NOT appointmentid=@appid";

		// Token: 0x0400062C RID: 1580
		internal const string QD_AVAILABILITY2MARKER = "DELETE FROM availability2marker WHERE availability2markerid=@availability2markerid";

		// Token: 0x0400062D RID: 1581
		internal const string QD_CLOSED_DAY_BY_PERSONID_AND_DATE = "DELETE FROM Availability2ItemsClosedDays WHERE personid=@pid AND dateclosed=@dt";

		// Token: 0x0400062E RID: 1582
		internal const string QD_AVAILABILITY = "DELETE FROM Availability2Items WHERE availability2itemid=@id";
	}
}
