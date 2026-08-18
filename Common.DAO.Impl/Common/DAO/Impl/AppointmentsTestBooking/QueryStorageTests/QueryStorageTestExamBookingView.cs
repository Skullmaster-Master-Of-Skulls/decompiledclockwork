using System;

namespace TechnoPro.Common.DAO.Impl.AppointmentsTestBooking.QueryStorageTests
{
	// Token: 0x02000157 RID: 343
	public static class QueryStorageTestExamBookingView
	{
		// Token: 0x04000602 RID: 1538
		internal const string QS_BookingsLarge = "LoadTestsExams2";

		// Token: 0x04000603 RID: 1539
		internal const string QS_BookingsSmall = "LoadTestsExams2";

		// Token: 0x04000604 RID: 1540
		internal const string QS_ClassTestDefinitions_Small_With_Extended_Info = "SELECT\tDISTINCT e.examid,e.lucourseid\r\n,coalesce(lucd.altLookupString,',') + ' ' + luc.course + ' ' + luc.timeofday + ' section ' + luc.section AS coursedescription\r\n,e.dateoftest,e.testduration,e.dateoftest AS teststarttime,DATEADD(n,e.testduration,e.dateoftest) AS testendtime\r\n,e.instructorcontacteddate,e.instructorcontactednote\r\n,e.testpickedupdate,e.testpickedupnote\r\n,e.typecode\r\n,CASE WHEN e.typecode='F' THEN 'Final'\r\nELSE 'Midterm' END AS [TestType2]\r\n@COLNAMES\r\nFROM exams e LEFT JOIN LUCourses luc ON luc.LUCourseID=e.lucourseid\r\nLEFT JOIN LUCourseData lucd ON lucd.luCourseDataID=luc.subjectid\r\n@FROMS\r\nWHERE e.dateoftest>=@sd AND e.dateoftest<@ed AND (e.visible IS NULL OR e.visible=1)";

		// Token: 0x04000605 RID: 1541
		internal const string QS_ClassTestDefinitions_Small_With_Extended_Info_By_Exam_Id = "SELECT\tDISTINCT e.examid,e.lucourseid\r\n,coalesce(lucd.altLookupString,',') + ' ' + luc.course + ' ' + luc.timeofday + ' section ' + luc.section AS coursedescription\r\n,e.dateoftest,e.testduration,e.dateoftest AS teststarttime,DATEADD(n,e.testduration,e.dateoftest) AS testendtime\r\n,e.instructorcontacteddate,e.instructorcontactednote\r\n,e.testpickedupdate,e.testpickedupnote\r\n,e.typecode\r\n,CASE WHEN e.typecode='F' THEN 'Final'\r\nELSE 'Midterm' END AS [TestType2]\r\n@COLNAMES\r\nFROM exams e LEFT JOIN LUCourses luc ON luc.LUCourseID=e.lucourseid\r\nLEFT JOIN LUCourseData lucd ON lucd.luCourseDataID=luc.subjectid\r\n@FROMS\r\nWHERE e.dateoftest>=@sd AND e.dateoftest<@ed AND (e.visible IS NULL OR e.visible=1)";

		// Token: 0x04000606 RID: 1542
		internal const string QS_UnbookedStudentsSmall = "sp_TestBooking_UnbookedStudents";
	}
}
