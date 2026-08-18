using System;
using System.Collections.Generic;
using TechnoPro.ClockWorkServer.Contracts.DTO.Notetaking;

namespace TechnoPro.Common.ClientManager.ICore.Notetaking
{
	// Token: 0x02000032 RID: 50
	public interface INotetakerNotesClientManager
	{
		// Token: 0x06000154 RID: 340
		IList<LectureNoteDescriptionDTO> LoadLectureNoteDescriptions(DateTime courseStartDate, DateTime courseEndDate, bool onlyShowFilesMarkedForDeletion);

		// Token: 0x06000155 RID: 341
		int DeleteAllNotesMarkedForDeletionTodayOrEarlier();

		// Token: 0x06000156 RID: 342
		int DeleteAllNotesMarkedForDeletion();

		// Token: 0x06000157 RID: 343
		void RemoveAllNotesDeletionMarks();

		// Token: 0x06000158 RID: 344
		void RemoveNotesDeletionMarks(params int[] notetakerDocumentIds);

		// Token: 0x06000159 RID: 345
		void AddNotesDeletionMarks(DateTime newDateOfDeletion, params int[] notetakerDocumentIds);

		// Token: 0x0600015A RID: 346
		LectureNoteDTO DownloadLectureNote(int notetakerDocumentId);

		// Token: 0x0600015B RID: 347
		IDictionary<DateTime, long> GetTotalFileSizeByMonth();
	}
}
