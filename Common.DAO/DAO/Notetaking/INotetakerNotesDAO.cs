using System;
using System.Collections.Generic;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Notetaking;

namespace TechnoPro.Common.DAO.Notetaking
{
	// Token: 0x02000048 RID: 72
	public interface INotetakerNotesDAO : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x06000181 RID: 385
		IList<LectureNoteDescription> LoadLectureNoteDescriptions(DateTime courseStartDate, DateTime courseEndDate);

		// Token: 0x06000182 RID: 386
		IList<LectureNoteDescription> LoadMarkedForDeletionLectureNoteDescriptions(DateTime courseStartDate, DateTime courseEndDate);

		// Token: 0x06000183 RID: 387
		int DeleteAllNotesMarkedForDeletionTodayOrEarlier();

		// Token: 0x06000184 RID: 388
		int DeleteAllNotesMarkedForDeletion();

		// Token: 0x06000185 RID: 389
		void RemoveAllNotesDeletionMarks();

		// Token: 0x06000186 RID: 390
		void RemoveNotesDeletionMarks(params int[] notetakerDocumentIds);

		// Token: 0x06000187 RID: 391
		void AddNotesDeletionMarks(DateTime newDateOfDeletion, params int[] notetakerDocumentIds);

		// Token: 0x06000188 RID: 392
		IDictionary<DateTime, long> GetTotalFileSizeByMonth();
	}
}
