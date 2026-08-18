using System;
using System.Collections.Generic;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.AlternativeFormat;

namespace TechnoPro.Common.DAO.AlternativeFormat
{
	// Token: 0x020000C9 RID: 201
	public interface IMediaJobDAO : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x0600058C RID: 1420
		int AddMediaJobNote(int mediaJobId, MediaJobRunningNote note);

		// Token: 0x0600058D RID: 1421
		void UpdateMediaJobNote(MediaJobRunningNote noteId);

		// Token: 0x0600058E RID: 1422
		IList<MediaJobRunningNote> GetRunningNotesByMediaJob(int mediaJobId);

		// Token: 0x0600058F RID: 1423
		MediaJob GetActiveMediaJobById(int mediaJobId);

		// Token: 0x06000590 RID: 1424
		IList<MediaJob> GetActiveMediaJobByMediaContentAndFormat(Guid mediaContentId, MediaContentFormat mediaContentFormat, int studentPersonId = 0);

		// Token: 0x06000591 RID: 1425
		IList<MediaJob> GetActiveMediaJobByMediaContentPerFormatId(int mediaContentPerFormatId, int studentId = 0);

		// Token: 0x06000592 RID: 1426
		int GetCountActiveMediaJobByMediaContentPerFormatId(int mediaContentPerFormatId, int studentId = 0);

		// Token: 0x06000593 RID: 1427
		int GetCountActiveMediaJobByMediaContentAndFormat(Guid mediaContentId, MediaContentFormat mediaContentFormat, int studentPersonId = 0);

		// Token: 0x06000594 RID: 1428
		IList<MediaJob> GetActiveMediaJobsByAssignedStaff(int assignedStaffId);

		// Token: 0x06000595 RID: 1429
		IList<MediaJob> GetActiveMediaJobsByAssignedStaff(int assignedStaffId, int campusId);

		// Token: 0x06000596 RID: 1430
		IList<MediaJob> GetActiveMediaJobsByExpiredInLessThan(TimeSpan dueDateIn);

		// Token: 0x06000597 RID: 1431
		IList<MediaJob> GetActiveExpiredMediaJobs();

		// Token: 0x06000598 RID: 1432
		IList<MediaJob> GetActiveJobs();

		// Token: 0x06000599 RID: 1433
		IList<MediaJob> GetActiveJobs(int campusId);

		// Token: 0x0600059A RID: 1434
		IList<MediaJob> GetActiveJobsByStudent(int studentPersonId);

		// Token: 0x0600059B RID: 1435
		IList<MediaJob> GetActiveJobsByStudent(int studentPersonId, int campusId);

		// Token: 0x0600059C RID: 1436
		CompletedMediaJob GetCompletedMediaJobById(int mediaJobId);

		// Token: 0x0600059D RID: 1437
		IList<CompletedMediaJob> GetCompletedMediaJobByMediaContentAndFormat(Guid mediaContentId, MediaContentFormat mediaContentFormat, int studentPersonId = 0);

		// Token: 0x0600059E RID: 1438
		IList<CompletedMediaJob> GetCompletedMediaJobByMediaContentPerFormatId(int mediaContentPerFormatId, int studentPersonId = 0);

		// Token: 0x0600059F RID: 1439
		IList<CompletedMediaJob> GetCompletedMediaJobsByAssignedStaff(int assignedStaffId);

		// Token: 0x060005A0 RID: 1440
		IList<CompletedMediaJob> GetCompletedMediaJobsByAssignedStaff(int assignedStaffId, int campusId);

		// Token: 0x060005A1 RID: 1441
		IList<CompletedMediaJob> GetCompletedJobsByDateRange(DateTime startDate, DateTime endDate);

		// Token: 0x060005A2 RID: 1442
		IList<CompletedMediaJob> GetCompletedJobsByDateRange(DateTime startDate, DateTime endDate, int campusId);

		// Token: 0x060005A3 RID: 1443
		IList<CompletedMediaJob> GetCompletedJobs();

		// Token: 0x060005A4 RID: 1444
		IList<CompletedMediaJob> GetCompletedJobs(int campusId);

		// Token: 0x060005A5 RID: 1445
		IList<CompletedMediaJob> GetCompletedJobsByStudent(int studentPersonId);

		// Token: 0x060005A6 RID: 1446
		IList<CompletedMediaJob> GetCompletedJobsByStudent(int studentPersonId, int campusId);

		// Token: 0x060005A7 RID: 1447
		IList<CompletedMediaJob> GetCompletedJobsByStudentAndDateRange(int studentPersonId, DateTime startDate, DateTime endDate);

		// Token: 0x060005A8 RID: 1448
		IList<CompletedMediaJob> GetCompletedJobsByStudentAndDateRange(int studentPersonId, DateTime startDate, DateTime endDate, int campusId);

		// Token: 0x060005A9 RID: 1449
		IList<CompletedMediaJob> GetCompletedJobsByStaffAndDateRange(int assignedStaffId, DateTime startDate, DateTime endDate);

		// Token: 0x060005AA RID: 1450
		IList<CompletedMediaJob> GetCompletedJobsByStaffAndDateRange(int assignedStaffId, DateTime startDate, DateTime endDate, int campusId);

		// Token: 0x060005AB RID: 1451
		CancelledMediaJob GetCancelledMediaJobById(int mediaJobId);

		// Token: 0x060005AC RID: 1452
		IList<CancelledMediaJob> GetCancelledJobsByDateRange(DateTime startDate, DateTime endDate);

		// Token: 0x060005AD RID: 1453
		IList<CancelledMediaJob> GetCancelledJobsByDateRange(DateTime startDate, DateTime endDate, int campusId);

		// Token: 0x060005AE RID: 1454
		IList<CancelledMediaJob> GetCancelledJobs();

		// Token: 0x060005AF RID: 1455
		IList<CancelledMediaJob> GetCancelledJobs(int campusId);

		// Token: 0x060005B0 RID: 1456
		IList<CancelledMediaJob> GetCancelledJobsByStudentAndDateRange(int studentPersonId, DateTime startDate, DateTime endDate);

		// Token: 0x060005B1 RID: 1457
		IList<CancelledMediaJob> GetCancelledJobsByStudentAndDateRange(int studentPersonId, DateTime startDate, DateTime endDate, int campusId);

		// Token: 0x060005B2 RID: 1458
		IList<CancelledMediaJob> GetCancelledJobsByStaffAndDateRange(int assignedStaffId, DateTime startDate, DateTime endDate);

		// Token: 0x060005B3 RID: 1459
		IList<CancelledMediaJob> GetCancelledJobsByStaffAndDateRange(int assignedStaffId, DateTime startDate, DateTime endDate, int campusId);

		// Token: 0x060005B4 RID: 1460
		int CreateMediaJob(MediaJob mediaJob);

		// Token: 0x060005B5 RID: 1461
		void UpdateMediaJob(MediaJob mediaJob);

		// Token: 0x060005B6 RID: 1462
		void CancelMediaJob(int mediaJobId, string changeNotes);

		// Token: 0x060005B7 RID: 1463
		void MarkMediaJobAsCompleted(int mediaJobId, string changeNotes);

		// Token: 0x060005B8 RID: 1464
		void ChangeMediaJobStatus(int mediaJobId, string changeNotes, string generalStatusnName, string publisherStatusName, string vendorStatusName, string inHouseStatusName);

		// Token: 0x060005B9 RID: 1465
		bool AvailableJobsByContentFormatIdAndStudentId(int mediaContentPerFormatId, int studentId);
	}
}
