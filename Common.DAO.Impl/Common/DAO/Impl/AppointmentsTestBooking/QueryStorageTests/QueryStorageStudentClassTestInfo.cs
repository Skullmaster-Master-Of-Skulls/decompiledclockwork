using System;

namespace TechnoPro.Common.DAO.Impl.AppointmentsTestBooking.QueryStorageTests
{
	// Token: 0x02000156 RID: 342
	public static class QueryStorageStudentClassTestInfo
	{
		// Token: 0x040005F9 RID: 1529
		internal const string QS_EXAM_STATUS_BY_APP_ID = "SELECT ac.appointmentid,ac.examstatuslookupid,el.title,el.colourargb,el.hidefromstudent\r\nFROM appointmentcourses ac LEFT JOIN examstatuslookup el ON el.examstatuslookupid=ac.examstatuslookupid\r\nWHERE NOT ac.examstatuslookupid IS NULL AND ac.appointmentid=@appid";

		// Token: 0x040005FA RID: 1530
		internal const string QS_STUDENT_CLASS_TEST_BY_ID = "SELECT ac.appointmentid,\r\n        ac.appointmentcourseid,ac.lucourseid AS slucourseid,ac.originalstartdatetime,ac.originalenddatetime,ac.testnote,ac.studentnote,ac.instructoracknowledgevalue,ac.instructoracknowledgedate,\r\n        ac.privatenote2,ac.testpickedupdate AS stestpickedupdate,ac.testpickedupnote AS stestpickedupnote,ac.extendedproperties AS sextendedproperties,\r\n        luc.startdate AS sstartdate,luc.enddate AS senddate,luc.duration AS sduration,luc.term AS sterm,luc.subjectid AS ssubjectid,lucd.altlookupstring AS ssubject,\r\n        luc.course AS scourse,luc.[section] AS ssection,luc.timeofday AS stimeofday\r\nFROM\tappointmentcourses ac LEFT JOIN lucourses luc ON luc.lucourseid=ac.lucourseid\r\n        LEFT JOIN lucoursedata lucd ON lucd.lucoursedataid=luc.subjectid\r\nWHERE ac.appointmentid=@appid";

		// Token: 0x040005FB RID: 1531
		internal const string QS_STUDENT_CLASS_TESTS_BY_IDS = "SELECT ac.appointmentid,\r\n        ac.appointmentcourseid,ac.lucourseid AS slucourseid,ac.originalstartdatetime,ac.originalenddatetime,ac.testnote,ac.studentnote,ac.instructoracknowledgevalue,ac.instructoracknowledgedate,\r\n        ac.privatenote2,ac.testpickedupdate AS stestpickedupdate,ac.testpickedupnote AS stestpickedupnote,ac.extendedproperties AS sextendedproperties,\r\n        luc.startdate AS sstartdate,luc.enddate AS senddate,luc.duration AS sduration,luc.term AS sterm,luc.subjectid AS ssubjectid,lucd.altlookupstring AS ssubject,\r\n        luc.course AS scourse,luc.[section] AS ssection,luc.timeofday AS stimeofday\r\nFROM\tappointmentcourses ac LEFT JOIN lucourses luc ON luc.lucourseid=ac.lucourseid\r\n        LEFT JOIN lucoursedata lucd ON lucd.lucoursedataid=luc.subjectid\r\nWHERE ac.appointmentid IN (SELECT orderid AS appointmentid FROM splitorderids(@appids,','))";

		// Token: 0x040005FC RID: 1532
		internal const string QD_STUDENT_CLASS_TEST = "DELETE FROM appointmentcourses WHERE appointmentcourseid=@appointmentcourseid";

		// Token: 0x040005FD RID: 1533
		internal const string QD_STUDENT_CLASS_TEST_BY_APP_ID = "DELETE FROM appointmentcourses WHERE appointmentid=@appid";

		// Token: 0x040005FE RID: 1534
		internal const string QU_STUDENT_REPORTED_CLASS_TIME_TO_EXAM_CLASS_TIME = "SELECT e.dateoftest AS startdatetime,dateadd(minute,e.testduration,e.dateoftest) AS enddatetime INTO #t1 FROM appointments a LEFT JOIN exams e ON e.examid=a.examid WHERE a.appointmentid=@appid;\r\nDECLARE @sdt datetime, @edt datetime;\r\nSET @sdt=(SELECT TOP 1 startdatetime FROM #t1);\r\nSET @edt=(SELECT TOP 1 enddatetime FROM #t1);\r\n\r\nIF NOT @sdt IS NULL AND NOT @edt IS NULL\r\n    UPDATE appointmentcourses SET originalstartdatetime=@sdt,originalenddatetime=@edt WHERE appointmentid=@appid;\r\n\r\nDROP TABLE #t1";

		// Token: 0x040005FF RID: 1535
		internal const string QU_BOOKING_NOTE = "UPDATE appointmentcourses SET studentnote=@note WHERE appointmentid=@appid";

		// Token: 0x04000600 RID: 1536
		internal const string QU_PRIVATE_NOTE = "UPDATE appointmentcourses SET privatenote2=@note WHERE appointmentid=@appid";

		// Token: 0x04000601 RID: 1537
		internal const string QU_EXAM_STATUS_BY_APPID = "UPDATE appointmentcourses SET examstatuslookupid=@examstatuslookupid WHERE appointmentid=@appid";
	}
}
