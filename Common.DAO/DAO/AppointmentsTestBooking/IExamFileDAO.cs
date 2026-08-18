using System;
using System.Collections.Generic;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.AppointmentsTestBooking;

namespace TechnoPro.Common.DAO.AppointmentsTestBooking
{
	// Token: 0x020000B9 RID: 185
	public interface IExamFileDAO : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x060004F7 RID: 1271
		IList<ExamFile> LoadExamFilesByExam(int ExamId, bool IncludeDeletedFiles, bool LoadFileData);

		// Token: 0x060004F8 RID: 1272
		ExamFile LoadExamFileById(int ExamFileId);

		// Token: 0x060004F9 RID: 1273
		int CreateExamFile(ExamFile ExamFile);

		// Token: 0x060004FA RID: 1274
		void DeleteExamFile(int ExamFileId);

		// Token: 0x060004FB RID: 1275
		IList<int> LoadExamFileIdsOlderThanDate(DateTime cutoffDate);

		// Token: 0x060004FC RID: 1276
		IList<int> LoadExamFileIdsWhereCourseEndDateIsInThePast(int courseEndDateOffsetInDays);
	}
}
