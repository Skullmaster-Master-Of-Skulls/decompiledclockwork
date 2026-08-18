using System;

namespace TechnoPro.Common.DAO.Impl.Appointments.QueryStorageApps
{
	// Token: 0x02000139 RID: 313
	internal static class QueryStorageAppointmentHistory
	{
		// Token: 0x04000568 RID: 1384
		internal const string QS_APP_LOG_CHANGES_BY_APPID = "";

		// Token: 0x04000569 RID: 1385
		internal const string QS_APPOINTMENT_MODIFIED_DATES_ENTRIES_BY_APPID = "SELECT amd.AppointmentsModifiedDatesID,amd.appointmentID,amd.dateModified,amd.personID,\r\n\t\tp.student_no,p.firstName,p.middleName,p.lastName\r\nFROM\tAppointmentsModifiedDates amd LEFT JOIN people p ON p.PersonID=amd.personID\r\nWHERE\tamd.appointmentID=@appid\r\nORDER BY amd.dateModified";

		// Token: 0x0400056A RID: 1386
		internal const string QS_APPOINTMENT_ARCHIVE_ENTRIES_BY_APPID = "SELECT\taa.RowNumber,aa.auditaction,aa.auditdatetime,\r\n        aa.AppointmentID,aa.AppTypeID,apt.[description] AS apptypedescription,\r\n\t\tapt.appointmentTypeGroupID,atg.title AS apptypegrouptitle,\r\n\t\taa.appCode,aa.startDate,aa.endDate,aa.[subject],aa.[location],\r\n\t\taa.cancelled,aa.isLocked,aa.isHidden,aa.groupCode,aa.extraAttendeesCount,\r\n\t\t0 AS AttendeeId,att.PersonID,p.firstName,p.lastName,p.student_no,att.miscCode,att.noshow,\r\n\t\tapt.isCourse,apt.isWorkshop,apt.defaultColour,\r\n\t\tam.memoText,am.isEncrypted,\r\n\t\taa.personID AS wbpersonid,p2.firstName AS wbfirstname,p2.lastName AS wblastname,p2.student_no AS wbstudent_no,\r\n\t\taa.dateAdded AS datebooked,CAST(NULL AS int) AS overridecolour,CAST(NULL AS datetime) AS actualstarttime,CAST(NULL AS datetime) AS actualendtime,\r\n\t\tCAST(NULL AS int) AS cancelreasonid,CAST(NULL AS varchar(256)) AS cancelreasongroupname,CAST(NULL AS varchar(256)) AS cancelreasontitle,\r\n\t\tCAST(NULL AS int) AS cbpersonid,CAST(NULL AS varbinary(8000)) AS cbfirstname,CAST(NULL AS varbinary(8000)) AS cblastname,CAST(NULL AS varbinary(8000)) AS cbstudent_no,\r\n\t\tCAST(NULL AS datetime) AS cancelleddate,CAST(NULL AS varchar(256)) AS cancelreasontext,\r\n\t\tpg.groupid,ast.appointmentshowtimeasid,ast.showtimeastitle,ast.extraiconid,ast.showtimeascolour\r\nFROM\t(SELECT ROW_NUMBER() OVER(ORDER BY AppointmentId) AS RowNumber,* FROM archive_appointments WHERE appointmentid=@appid) aa LEFT JOIN AppointmentTypes apt ON apt.AppTypeID=aa.AppTypeID\r\n\t\tLEFT JOIN AppointmentTypeGroups atg ON atg.AppointmentTypeGroupID=apt.appointmentTypeGroupID\r\n\t\tLEFT JOIN archive_attendees att ON att.AppointmentID=aa.AppointmentID\r\n\t\tLEFT JOIN people p ON p.PersonID=att.PersonID\r\n\t\tLEFT JOIN archive_appointmentMemos am ON am.AppointmentID=aa.AppointmentID\r\n\t\tLEFT JOIN people p2 ON p2.PersonID=aa.personID\r\n\t\tLEFT JOIN PeopleGroups pg ON pg.PersonID=att.PersonID AND pg.groupid<10\r\n\t\tLEFT JOIN AppointmentShowTimeAs ast ON ast.extraiconid=aa.appcode\r\nORDER BY aa.RowNumber,aa.auditdatetime";
	}
}
