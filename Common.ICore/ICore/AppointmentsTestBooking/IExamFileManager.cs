using System;
using System.Collections.Generic;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.AppointmentsTestBooking;

namespace TechnoPro.Common.ICore.AppointmentsTestBooking
{
	// Token: 0x020000C6 RID: 198
	public interface IExamFileManager : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x06000601 RID: 1537
		IList<ExamFile> LoadExamFilesByExam(int ExamId, bool IncludeDeletedFiles, bool LoadFileData);

		// Token: 0x06000602 RID: 1538
		ExamFile LoadExamFileById(int ExamFileId);

		// Token: 0x06000603 RID: 1539
		int CreateExamFile(ExamFile ExamFile);

		// Token: 0x06000604 RID: 1540
		void DeleteExamFile(int ExamFileId);

		// Token: 0x06000605 RID: 1541
		IList<ExamFile> LoadExamFilesByExamCheckProfAltContactPermissions(int InstructorId, int AltContactId, int ExamId, bool IncludeDeletedFiles, bool LoadFileData);

		// Token: 0x06000606 RID: 1542
		ExamFile LoadExamFileByIdCheckProfAltContactPermissions(int ExamId, int InstructorId, int AltContactId, int ExamFileId);

		// Token: 0x06000607 RID: 1543
		IList<int> LoadExamFileIdsOlderThanDate(DateTime cutoffDate);

		// Token: 0x06000608 RID: 1544
		IList<int> LoadExamFileIdsWhereCourseEndDateIsInThePast(int courseEndDateOffsetInDays);
	}
}
