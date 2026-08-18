using System;

namespace TechnoPro.Common.DAO.Impl.AppointmentsTestExamViews.QueryStorageAppointmentsTestExamViews
{
	// Token: 0x02000145 RID: 325
	public static class QueryStorageFinalExamsView
	{
		// Token: 0x040005A7 RID: 1447
		internal const string QS_FINAL_EXAMS_VIEW_LIGHT = "DECLARE @sdate datetime = DATEADD(D, 0, DATEDIFF(D, 0, @startdate))\r\nDECLARE @edate datetime = DATEADD(D, 0, DATEDIFF(D, 0, @enddate))\r\n\r\nSELECT  e.examid,e.dateentered,e.[description],e.dateoftest,e.usercomment,e.testduration,\r\n\t\te.testpickedupdate,e.testpickedupnote,e.instructorcontacteddate,e.instructorcontactednote,e.privatenote,\r\n\t\te.instructoracknowledged,e.lastmodified,\r\n\t\te.lucourseid,luc.startdate,luc.enddate,luc.term,lucd.altLookupString AS [subject],luc.course,luc.TimeOfDay,luc.[section],\r\n        coalesce(lucd.altlookupstring,'','') + ' ' + luc.course + luc.timeofday + ' ' + luc.section + ' (' + luc.term + ')' AS CourseDescription,\r\n\t\ta.appointmentid,a.startDate AS appstartdate,a.endDate AS appenddate,a.cancelled,a.appCode,a.AppTypeID,apt.[description] AS apptypedescription,\r\n\t\tatt.PersonID,p.lastName,p.firstName,p.middleName,p.student_no,pg.GroupID,att.noshow  \r\nFROM\texams e LEFT JOIN Appointments a ON a.examid=e.examid \r\n\t\tLEFT JOIN lucourses luc ON luc.LUCourseID=e.lucourseid\r\n\t\tLEFT JOIN lucoursedata lucd ON lucd.luCourseDataID=luc.SubjectID\r\n\t\tLEFT JOIN attendees att ON att.AppointmentID=a.AppointmentID\r\n\t\tLEFT JOIN people p ON p.PersonID=att.PersonID\r\n\t\tLEFT JOIN peoplegroups pg ON pg.PersonID=p.personid\r\n\t\tLEFT JOIN AppointmentTypes apt ON apt.AppTypeID=a.AppTypeID\r\nWHERE\te.visible = 1 \r\n\t\tAND NOT ( ( luc.enddate<@sdate ) OR (luc.startdate > @edate ) )\r\nORDER BY e.examid,a.AppointmentID";
	}
}
