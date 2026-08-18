using System;
using System.IO;

namespace TechnoPro.Common.UI.ClientManager.Web.Core.Notetaking
{
	// Token: 0x0200000E RID: 14
	public interface INotetakingWebClientManager
	{
		// Token: 0x06000029 RID: 41
		void NotifyStudentsNewLectureNotesHaveBeenUploaded(int NotetakerId, int LuCourseId, DateTime LectureDate);

		// Token: 0x0600002A RID: 42
		bool UploadLectureNote(Stream sFile, int sizeInBytes, string docName, string notes, int notetakerID, int courseID, DateTime lectureDate, bool isSampleNotes, out Exception ex);

		// Token: 0x0600002B RID: 43
		bool DownloadLectureNoteToBrowser(int docID);
	}
}
