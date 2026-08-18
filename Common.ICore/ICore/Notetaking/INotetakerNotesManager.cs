using System;
using System.Collections.Generic;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Notetaking;

namespace TechnoPro.Common.ICore.Notetaking
{
	// Token: 0x0200005A RID: 90
	public interface INotetakerNotesManager : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x06000267 RID: 615
		IList<LectureNoteDescription> LoadLectureNoteDescriptions(DateTime courseStartDate, DateTime courseEndDate, bool onlyShowFilesMarkedForDeletion);

		// Token: 0x06000268 RID: 616
		int DeleteAllNotesMarkedForDeletionTodayOrEarlier();

		// Token: 0x06000269 RID: 617
		int DeleteAllNotesMarkedForDeletion();

		// Token: 0x0600026A RID: 618
		void RemoveAllNotesDeletionMarks();

		// Token: 0x0600026B RID: 619
		void RemoveNotesDeletionMarks(params int[] notetakerDocumentIds);

		// Token: 0x0600026C RID: 620
		void AddNotesDeletionMarks(DateTime newDateOfDeletion, params int[] notetakerDocumentIds);

		// Token: 0x0600026D RID: 621
		LectureNote DownloadLectureNote(int notetakerDocumentId);

		// Token: 0x0600026E RID: 622
		IDictionary<DateTime, long> GetTotalFileSizeByMonth();
	}
}
