using System;

namespace TechnoPro.Common.DAO.Impl.Notetaking
{
	// Token: 0x02000086 RID: 134
	internal static class QueryStorageNotetakerNotes
	{
		// Token: 0x04000178 RID: 376
		internal const string QS_NOTE_FILE_SIZES_BY_MONTH = "SELECT DATEADD(day,-DAY(nd.lectureDate)+1,nd.lectureDate) AS LectureDateMonthYear,nd.SizeInBytes INTO #t1 FROM NotetakerDocument nd \r\nSELECT\tDISTINCT LectureDateMonthYear,SUM(SizeInBytes) AS TotalSizeInBytes\r\nFROM\t#t1\r\nGROUP BY LectureDateMonthYear\r\nORDER BY LectureDateMonthYear DESC\r\n\r\nDROP TABLE #t1";

		// Token: 0x04000179 RID: 377
		internal const string QS_LOAD_LECTURE_NOTE_DESCRIPTIONS_BY_LECTURE_DATE = "DECLARE @sd datetime = DATEADD(D, 0, DATEDIFF(D, 0, @startdate))\r\nDECLARE @ed datetime = DATEADD(D, 1, DATEDIFF(D, 0, @enddate))\r\n\r\nSELECT NotetakerDocumentId INTO #t1 FROM NotetakerDocument WHERE LUCourseId IN (SELECT LUCourseId FROM LUCourses WHERE NOT ( ( enddate<@sd) OR (startdate > @ed) ))\r\n\r\nSELECT\tnd.NotetakerDocumentId,nd.lectureDate,nd.dateCreated,nd.notes,nd.docName,nd.sizeInBytes,nd.DeletionDate,\r\n\t\tnd.LUCourseId,luc.StartDate,luc.EndDate,luc.Duration,luc.Term,luc.SubjectID,lucd.altLookupString AS [subject],\r\n\t\tluc.[Section],luc.Course,luc.TimeOfDay,luc.campus,luc.department,luc.CourseNote,luc.location,\r\n\t\tnd.NotetakerID AS serviceproviderid,sp.email,sp.firstname,sp.middlename,sp.lastname,sp.student_no,sp.altid\r\nFROM\tNotetakerDocument nd LEFT JOIN LUCourses luc ON luc.LUCourseID=nd.LUCourseId\r\n\t\tLEFT JOIN LUCourseData lucd ON lucd.luCourseDataID=luc.SubjectID\r\n\t\tLEFT JOIN ServiceProviders sp ON sp.ServiceProviderId=nd.NotetakerID\r\nWHERE\tnd.NotetakerDocumentId IN (SELECT NotetakerDocumentId FROM #t1)\r\n\r\nDROP TABLE #t1";

		// Token: 0x0400017A RID: 378
		internal const string QS_LOAD_MARKED_FOR_DELETION_LECTURE_NOTE_DESCRIPTIONS_BY_LECTURE_DATE = "DECLARE @sd datetime = DATEADD(D, 0, DATEDIFF(D, 0, @startdate))\r\nDECLARE @ed datetime = DATEADD(D, 1, DATEDIFF(D, 0, @enddate))\r\n\r\nSELECT NotetakerDocumentId INTO #t1 FROM NotetakerDocument WHERE LUCourseId IN (SELECT LUCourseId FROM LUCourses WHERE NOT ( ( enddate<@sd) OR (startdate > @ed) ))\r\n\r\nSELECT\tnd.NotetakerDocumentId,nd.lectureDate,nd.dateCreated,nd.notes,nd.docName,nd.sizeInBytes,nd.DeletionDate,\r\n\t\tnd.LUCourseId,luc.StartDate,luc.EndDate,luc.Duration,luc.Term,luc.SubjectID,lucd.altLookupString AS [subject],\r\n\t\tluc.[Section],luc.Course,luc.TimeOfDay,luc.campus,luc.department,luc.CourseNote,luc.location,\r\n\t\tnd.NotetakerID AS serviceproviderid,sp.email,sp.firstname,sp.middlename,sp.lastname,sp.student_no,sp.altid\r\nFROM\tNotetakerDocument nd LEFT JOIN LUCourses luc ON luc.LUCourseID=nd.LUCourseId\r\n\t\tLEFT JOIN LUCourseData lucd ON lucd.luCourseDataID=luc.SubjectID\r\n\t\tLEFT JOIN ServiceProviders sp ON sp.ServiceProviderId=nd.NotetakerID\r\nWHERE\tnd.NotetakerDocumentId IN (SELECT NotetakerDocumentId FROM #t1)\r\n\r\nDROP TABLE #t1 AND NOT nd.DeletionDate IS NULL";

		// Token: 0x0400017B RID: 379
		internal const string QD_DELETE_NOTES_MARKED_FOR_DELETION = "SET @ct = (SELECT COUNT(NotetakerDocumentId) FROM NotetakerDocument WHERE NOT DeletionDate IS NULL)\r\nDELETE FROM NotetakerDocument WHERE NOT DeletionDate IS NULL";

		// Token: 0x0400017C RID: 380
		internal const string QD_DELETE_NOTES_MARKED_FOR_DELETION_TODAY = "DECLARE @maxDate datetime = DATEADD(D, 1, DATEDIFF(D, 0, GETDATE()))\r\nSET @ct = (SELECT COUNT(NotetakerDocumentId) FROM NotetakerDocument WHERE NOT DeletionDate IS NULL AND DeletionDate<@maxDate)\r\nDELETE FROM NotetakerDocument WHERE NOT DeletionDate IS NULL AND DeletionDate<@maxDate";

		// Token: 0x0400017D RID: 381
		internal const string QU_REMOVE_ALL_MARKS_FOR_DELETION = "UPDATE NotetakerDocument SET DeletionDate=NULL";

		// Token: 0x0400017E RID: 382
		internal const string QU_REMOVE_MARKS_FOR_DELETION = "UPDATE NotetakerDocument SET DeletionDate=NULL WHERE NotetakerDocumentId IN (SELECT orderid AS NotetakerDocumentId FROM SplitOrderIds(@ids,','))";

		// Token: 0x0400017F RID: 383
		internal const string QU_ADD_MARKS_FOR_DELETION = "UPDATE NotetakerDocument SET DeletionDate=@dt WHERE NotetakerDocumentId IN (SELECT orderid AS NotetakerDocumentId FROM SplitOrderIds(@ids,','))";
	}
}
