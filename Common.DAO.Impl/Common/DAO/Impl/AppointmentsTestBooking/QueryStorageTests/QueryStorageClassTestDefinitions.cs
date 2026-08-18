using System;

namespace TechnoPro.Common.DAO.Impl.AppointmentsTestBooking.QueryStorageTests
{
	// Token: 0x02000153 RID: 339
	public static class QueryStorageClassTestDefinitions
	{
		// Token: 0x040005DC RID: 1500
		internal const string QS_CLASS_TESTS_FOR_DISPLAY = "DECLARE @sd datetime = DATEADD(D, 0, DATEDIFF(D, 0, @startdate))\r\nDECLARE @ed datetime = DATEADD(D, 1, DATEDIFF(D, 0, @enddate))\r\n\r\nSELECT\tDISTINCT e.examid,e.lucourseid,\r\n        luc.startdate,luc.enddate,luc.duration,luc.term,luc.subjectid,lucd.altLookupString AS subject,luc.course,luc.[section],\r\n        luc.timeofday,luc.campus,\r\n        luc.instructorid,lucd2.altLookupString AS instructorname,lucd2.email AS instructoremail,lucd2.phone AS instructorphone,\r\n        lucd2.username AS instructorusername,lucd2.externalid AS instructorexternalid,lucd2.id AS instructoremployeeid,\r\n\t\te.dateoftest,e.testduration,e.filename AS [location],\r\n        e.typecode,\r\n        CASE WHEN e.typecode='F' THEN 'Final' ELSE 'Midterm' END AS typecode2,\r\n\t\te.instructorcontacteddate,e.instructorcontactednote,e.testpickedupdate,e.testpickedupnote\r\nFROM\texams e LEFT JOIN LUCourses luc ON luc.LUCourseID=e.lucourseid\r\n\t\tLEFT JOIN LUCourseData lucd ON lucd.luCourseDataID=luc.subjectid\r\n        LEFT JOIN lucoursedata lucd2 ON lucd2.lucoursedataid=luc.instructorid\r\nWHERE\te.dateoftest>=@sd AND e.dateoftest<@ed \r\n\t\tAND (e.visible IS NULL OR e.visible=1)\r\nORDER BY e.dateoftest";

		// Token: 0x040005DD RID: 1501
		internal const string QS_INSTRUCTOR_SUBMITTED_TEST_INFO = "SELECT wholastmodified FROM exams WHERE examid=@examid";

		// Token: 0x040005DE RID: 1502
		internal const string QS_CLASS_TESTS_BY_COURSE_AND_DATE_RANGE = "SELECT e.examid,e.lucourseid,e.dateoftest,e.testduration,e.filename AS location,e.dateentered,\r\n         e.whoentered,e.typecode,e.extendedproperties AS externalexamid,\r\n         luc.startdate,luc.enddate,luc.duration,luc.term,luc.subjectid,lucd.altlookupstring AS subject,\r\n         luc.course,luc.[section],luc.timeofday,\r\n         e.usercomment,e.testpickedupdate,e.testpickedupnote,e.instructorcontacteddate,e.instructorcontactednote,e.privatenote,e.instructoracknowledged,\r\n         e.[description]\r\nFROM     exams e LEFT JOIN lucourses luc ON luc.lucourseid=e.lucourseid\r\n         LEFT JOIN lucoursedata lucd ON lucd.lucoursedataid=luc.subjectid\r\nWHERE    e.lucourseid=@lucid AND e.dateoftest>=@sd AND e.dateoftest<@ed AND (@typecode='' OR e.typecode=@typecode)";

		// Token: 0x040005DF RID: 1503
		internal const string QS_CLASS_TEST_BY_ID = "SELECT e.examid,e.lucourseid,e.dateoftest,e.testduration,e.filename AS location,e.dateentered,\r\n         e.whoentered,e.typecode,e.extendedproperties AS externalexamid,\r\n         luc.startdate,luc.enddate,luc.duration,luc.term,luc.subjectid,lucd.altlookupstring AS subject,\r\n         luc.course,luc.[section],luc.timeofday,\r\n         e.usercomment,e.testpickedupdate,e.testpickedupnote,e.instructorcontacteddate,e.instructorcontactednote,e.privatenote,e.instructoracknowledged,\r\n         e.[description]\r\nFROM     exams e LEFT JOIN lucourses luc ON luc.lucourseid=e.lucourseid\r\n         LEFT JOIN lucoursedata lucd ON lucd.lucoursedataid=luc.subjectid\r\nWHERE    e.examid=@examid";

		// Token: 0x040005E0 RID: 1504
		internal const string QS_CLASS_TEST_BY_APPOINTMENTID = "SELECT e.examid,e.lucourseid,e.dateoftest,e.testduration,e.filename AS location,e.dateentered,\r\n         e.whoentered,e.typecode,e.extendedproperties AS externalexamid,\r\n         luc.startdate,luc.enddate,luc.duration,luc.term,luc.subjectid,lucd.altlookupstring AS subject,\r\n         luc.course,luc.[section],luc.timeofday,\r\n         e.usercomment,e.testpickedupdate,e.testpickedupnote,e.instructorcontacteddate,e.instructorcontactednote,e.privatenote,e.instructoracknowledged,\r\n         e.[description]\r\nFROM     appointments a LEFT JOIN exams e ON e.examid=a.examid\r\n         LEFT JOIN lucourses luc ON luc.lucourseid=e.lucourseid\r\n         LEFT JOIN lucoursedata lucd ON lucd.lucoursedataid=luc.subjectid\r\nWHERE    a.appointmentid=@appid";

		// Token: 0x040005E1 RID: 1505
		internal const string QS_CLASS_TEST_BY_ID_CONFIRM_PROF_OR_ALTCONTACT = "SELECT e.examid,e.lucourseid,e.dateoftest,e.testduration,e.filename AS location,e.dateentered,\r\n         e.whoentered,e.typecode,e.extendedproperties AS externalexamid,\r\n         luc.startdate,luc.enddate,luc.duration,luc.term,luc.subjectid,lucd.altlookupstring AS subject,\r\n         luc.course,luc.[section],luc.timeofday,\r\n         e.usercomment,e.testpickedupdate,e.testpickedupnote,e.instructorcontacteddate,e.instructorcontactednote,e.privatenote,e.instructoracknowledged,\r\n         e.[description]\r\nFROM     exams e LEFT JOIN lucourses luc ON luc.lucourseid=e.lucourseid\r\n         LEFT JOIN lucoursedata lucd ON lucd.lucoursedataid=luc.subjectid\r\nWHERE    e.examid=@examid \r\n         AND\r\n         ( \r\n            (@iid<1 OR EXISTS(SELECT lucourseid FROM vInstructorList WHERE lucourseid=e.lucourseid AND instructorid=@iid))\r\n            OR (@aid<1 OR EXISTS(SELECT lucourseid FROM vAlternateContactList WHERE lucourseid=e.lucourseid AND alternatecontactid=@aid))\r\n         )";

		// Token: 0x040005E2 RID: 1506
		internal const string QS_CLASS_TEST_BASE_BY_ID = "SELECT e.examid,e.lucourseid,e.dateoftest,e.testduration,e.filename AS location,e.dateentered,\r\n         e.whoentered,e.typecode,e.extendedproperties AS externalexamid,\r\n         luc.startdate,luc.enddate,luc.duration,luc.term,luc.subjectid,lucd.altlookupstring AS subject,\r\n         luc.course,luc.[section],luc.timeofday\r\nFROM     exams e LEFT JOIN lucourses luc ON luc.lucourseid=e.lucourseid\r\n         LEFT JOIN lucoursedata lucd ON lucd.lucoursedataid=luc.subjectid\r\nWHERE    e.examid=@examid";

		// Token: 0x040005E3 RID: 1507
		internal const string QS_CLASS_TESTS_BY_COURSE = "SELECT e.examid,e.lucourseid,e.dateoftest,e.testduration,e.filename AS location,e.dateentered,\r\n         e.whoentered,e.typecode,e.extendedproperties AS externalexamid,\r\n         luc.startdate,luc.enddate,luc.duration,luc.term,luc.subjectid,lucd.altlookupstring AS subject,\r\n         luc.course,luc.[section],luc.timeofday,\r\n         e.usercomment,e.testpickedupdate,e.testpickedupnote,e.instructorcontacteddate,e.instructorcontactednote,e.privatenote,e.instructoracknowledged,\r\n         e.[description]\r\nFROM     exams e LEFT JOIN lucourses luc ON luc.lucourseid=e.lucourseid\r\n         LEFT JOIN lucoursedata lucd ON lucd.lucoursedataid=luc.subjectid\r\nWHERE    e.lucourseid=@lucid AND (@typecode='' OR e.typecode=@typecode)";

		// Token: 0x040005E4 RID: 1508
		internal const string QU_REMOVE_INSTRUCTOR_HAS_SUBMITTED_INFO_ABOUT_THIS_TEST_MARKER = "UPDATE exams SET lastmodified=null,wholastmodified=null WHERE examid=@examid";

		// Token: 0x040005E5 RID: 1509
		internal const string QU_TEST_PICKED_UP = "UPDATE exams SET testpickedupnote=@testpickedupnote,testpickedupdate=@testpickedupdate WHERE examid=@examid";

		// Token: 0x040005E6 RID: 1510
		internal const string QU_INSTRUCTOR_CONTACTED = "UPDATE exams SET instructorcontactednote=@instructorcontactednote,instructorcontacteddate=@instructorcontacteddate WHERE examid=@examid";

		// Token: 0x040005E7 RID: 1511
		internal const string QU_INSTRUCTOR_LAST_MODIFIED = "UPDATE exams SET wholastmodified=@who WHERE examid=@examid";

		// Token: 0x040005E8 RID: 1512
		internal const string QU_CLASS_TEST_BASE = "UPDATE exams SET filename=@location,dateoftest=@dateoftest,testduration=@testduration,typecode=@typecode,extendedproperties=@externalexamid\r\nWHERE examid=@examid";

		// Token: 0x040005E9 RID: 1513
		internal const string QU_CLASS_TEST = "UPDATE exams SET filename=@location,dateoftest=@dateoftest,testduration=@testduration,typecode=@typecode,extendedproperties=@externalexamid,\r\ntestpickedupdate=@testpickedupdate,usercomment=@testdelivered,testpickedupnote=@testpickedupnote,privatenote=@privatenote,instructorcontacteddate=@instructorcontacteddate,instructorcontactednote=@instructorcontactednote,instructoracknowledged=@instructoracknowledged\r\nWHERE examid=@examid";

		// Token: 0x040005EA RID: 1514
		internal const string QU_CLASS_TEST_TEST_DELIVERED = "UPDATE exams SET usercomment=@testdelivered WHERE examid=@examid";

		// Token: 0x040005EB RID: 1515
		internal const string QI_CLASS_TEST = "INSERT INTO exams (whoentered,lucourseid,filename,dateoftest,testduration,typecode,extendedproperties,testpickedupdate,usercomment,testpickedupnote,privatenote,instructorcontacteddate,instructorcontactednote,instructoracknowledged)\r\nVALUES (@whoami,@lucid,@location,@dateoftest,@testduration,@typecode,@externalexamid,@testpickedupdate,@testdelivered,@testpickedupnote,@privatenote,@instructorcontacteddate,@instructorcontactednote,@instructoracknowledged);\r\nSET @examid=(SELECT TOP 1 CAST(SCOPE_IDENTITY() AS int))";

		// Token: 0x040005EC RID: 1516
		internal const string QI_CLASS_TEST_BASE = "INSERT INTO exams (whoentered,lucourseid,filename,dateoftest,testduration,typecode,extendedproperties)\r\nVALUES (@whoami,@lucid,@location,@dateoftest,@testduration,@typecode,@externalexamid);\r\nSET @examid=(SELECT TOP 1 CAST(SCOPE_IDENTITY() AS int))";

		// Token: 0x040005ED RID: 1517
		internal const string QD_CLASS_TEST = "DELETE FROM exams WHERE examid=@examid AND NOT examid IN (SELECT examid FROM appointments)";
	}
}
