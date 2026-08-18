using System;

namespace TechnoPro.Common.DAO.AutoTestBooking.Legacy.QueryStorage
{
	// Token: 0x02000005 RID: 5
	public static class QueryStorageBooker
	{
		// Token: 0x04000003 RID: 3
		internal const string QS_Select_CourseTimetableByStudentForCoursesWithDatesOverlappingTargetDate = "SELECT  luc.StartDate,luc.EndDate,t.*\r\nFROM Courses c LEFT JOIN LUCourses luc ON luc.LUCourseID=c.luCourseID \r\n\t\tLEFT JOIN timetable t ON t.lucourseid=c.luCourseID \r\nWHERE\tc.personID=@pid \r\n\t\tAND NOT c.lucourseid=@lucid \r\n\t\tAND (c.registrationstatus IS NULL OR NOT c.registrationstatus=2)\r\n\t\tAND @targetdate >= luc.startdate AND @targetdate <= luc.enddate \r\n\t\tAND NOT t.timetableid IS NULL";

		// Token: 0x04000004 RID: 4
		internal const string QS_Select_RoomSchedules = "SELECT    app.appointmentid,app.startdate,app.enddate,app.cancelled,app.apptypeid,att.personid \r\nFROM        appointments app LEFT JOIN attendees att ON att.appointmentid=app.appointmentid \r\nWHERE       att.personid IN (SELECT orderid AS personid FROM splitorderids(@pids,',')) AND app.startdate>=@sdate AND app.enddate<=@edate AND app.cancelled=0 ORDER BY app.startdate";

		// Token: 0x04000005 RID: 5
		internal const string QS_Select_RoomSchedulesWithExceptionAppId = "SELECT    app.appointmentid,app.startdate,app.enddate,app.cancelled,app.apptypeid,att.personid \r\nFROM        appointments app LEFT JOIN attendees att ON att.appointmentid=app.appointmentid \r\nWHERE       NOT app.appointmentid=@appid AND att.personid IN (SELECT orderid AS personid FROM splitorderids(@pids,',')) AND app.startdate>=@sdate AND app.enddate<=@edate AND app.cancelled=0 ORDER BY app.startdate";

		// Token: 0x04000006 RID: 6
		internal const string QS_Select_StudentScheduleExceptAppointment = "SELECT    app.appointmentid,app.startdate,app.enddate,app.cancelled,app.apptypeid,att.personid \r\nFROM        appointments app LEFT JOIN attendees att ON att.appointmentid=app.appointmentid \r\nWHERE       NOT app.appointmentid=@appid AND att.personid IN (SELECT orderid AS personid FROM splitorderids(@pids,',')) AND app.startdate>=@sdate AND app.enddate<=@edate AND app.cancelled=0 ORDER BY app.startdate";

		// Token: 0x04000007 RID: 7
		internal const string QS_Select_Availability = "SELECT    a.personid,a.availabilitygroupid,a.availabilitydate,a.availability,-1 AS roomid \r\nFROM        availabilityschedule a \r\nWHERE       a.availabilitydate>=@sdate AND a.availabilitydate <=@edate AND a.personid IN (SELECT orderid AS personid FROM splitorderids(@pids,',')) AND a.availabilitygroupid IN (SELECT orderid AS availabilitygroupid FROM splitorderids( @agids, ',' ) ) \r\nORDER BY a.personid,a.availabilitydate,a.availabilitygroupid";
	}
}
