using System;

namespace TechnoPro.Common.DAO.Impl.AppointmentsTestBooking.QueryStorageTests
{
	// Token: 0x02000154 RID: 340
	internal static class QueryStorageExamFile
	{
		// Token: 0x040005EE RID: 1518
		internal const string QS_EXAMFILES_BY_EXAM = "SELECT   ef.examfileid,ef.examid,ef.filename,ef.dateentered,ef.whoentered,ef.description,ef.visible,\r\n            CASE WHEN @loadfiles=1 THEN ef.filedata ELSE CAST(NULL AS image) END AS filedata\r\nFROM examfiles ef \r\nWHERE   ef.examid=@examid AND (@includedeletedfiles=1 OR ef.visible=1)\r\nORDER BY ef.dateentered";

		// Token: 0x040005EF RID: 1519
		internal const string QS_EXAMFILE_BY_ID = "SELECT   ef.examfileid,ef.examid,ef.filename,ef.dateentered,ef.whoentered,ef.description,ef.visible,ef.filedata\r\nFROM examfiles ef \r\nWHERE   ef.examfileid=@examfileid";

		// Token: 0x040005F0 RID: 1520
		internal const string QD_EXAMFILE = "DELETE FROM examfiles WHERE examfileid=@examfileid";

		// Token: 0x040005F1 RID: 1521
		internal const string QI_EXAMFILE = "INSERT INTO examfiles \r\n        (examid,filename,filedata,whoentered,description,visible) \r\nVALUES  (@examid,@filename,@filedata,@whoentered,@description,@visible);\r\n\r\nSET @examfileid=(SELECT TOP 1 CAST(SCOPE_IDENTITY() AS int))";

		// Token: 0x040005F2 RID: 1522
		internal const string QS_EXAM_FILE_IDS_BEFORE_DATE = "SELECT DISTINCT examid FROM ExamFiles WHERE dateentered<@dt";

		// Token: 0x040005F3 RID: 1523
		internal const string QS_EXAM_FILE_IDS_COURSE_ENDED = "DECLARE @today datetime = getdate()\r\n\r\nSELECT\tDISTINCT ef.examid\r\nFROM\texamfiles ef LEFT JOIN exams e ON e.examid=ef.examid \r\n\t\tLEFT JOIN lucourses luc ON luc.LUCourseID=e.lucourseid\r\nWHERE\tluc.enddate IS NULL OR DATEADD(day,@numDays,luc.enddate)>@today AND NOT ef.examid IS NULL";
	}
}
