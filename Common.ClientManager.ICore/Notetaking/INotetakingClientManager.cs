using System;
using System.Collections.Generic;
using TechnoPro.ClockWorkServer.Contracts.DTO.DataSync;
using TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses;
using TechnoPro.ClockWorkServer.Contracts.DTO.Notetaking;
using TechnoPro.ClockWorkServer.Contracts.DTO.ServiceProvider;
using TechnoPro.ClockWorkServer.Contracts.DTO.ServiceProviderOriginal;
using TechnoPro.Common.Public;

namespace TechnoPro.Common.ClientManager.ICore.Notetaking
{
	// Token: 0x02000033 RID: 51
	public interface INotetakingClientManager : IWebService
	{
		// Token: 0x0600015C RID: 348
		IList<LectureNoteDescriptionDTO> LoadLectureNoteDescriptionsByNotetakerAndCourse(int ServiceProviderId, int NotetakerLuCourseId);

		// Token: 0x0600015D RID: 349
		IList<LectureNoteDescriptionDTO> LoadLectureNoteDescriptionsByStudentAndCourse(int StudentPersonId, int StudentLuCourseId);

		// Token: 0x0600015E RID: 350
		LectureNoteDTO LoadLectureNoteById(int NotetakerDocumentId);

		// Token: 0x0600015F RID: 351
		IList<NotetakerBaseWithLookupCourseBaseDTO> LoadMatchingNotetakersWithLectureNoteUploadsByCourse(int LuCourseId);

		// Token: 0x06000160 RID: 352
		IList<DownloadedLectureNoteDTO> LoadStudentDownloadedLectureNoteHistory(int PersonId, int LuCourseId);

		// Token: 0x06000161 RID: 353
		IList<DownloadedLectureNoteDTO> LoadStudentDownloadedLectureNoteHistoryLastDateDownloadedForEachLectureNote(int PersonId, int LuCourseId);

		// Token: 0x06000162 RID: 354
		int CreateLectureNote(LectureNoteDTO lectureNote);

		// Token: 0x06000163 RID: 355
		void UpdateLectureNote(LectureNoteDTO lectureNote);

		// Token: 0x06000164 RID: 356
		void DeleteLectureNote(int NotetakerDocumentId);

		// Token: 0x06000165 RID: 357
		NotetakerBaseDTO LoadNotetakerBaseByUsername(string username);

		// Token: 0x06000166 RID: 358
		NotetakerBaseDTO LoadNotetakerBaseById(int ServiceProviderId);

		// Token: 0x06000167 RID: 359
		NotetakerBaseDTO LoadNotetakerBaseByNotetakeeAndCourse(int NotetakeePersonId, int NotetakeeLuCourseId);

		// Token: 0x06000168 RID: 360
		IList<LookupCourseBaseDTO> LoadEquivalentCourses(int LuCourseId);

		// Token: 0x06000169 RID: 361
		void AddPotentialCoursesForNotetaker(int ServiceProviderId, IList<DataSyncExternalCourseDTO> ExternalCourses);

		// Token: 0x0600016A RID: 362
		int CreateNotetakerAccount(SPProviderDTO Provider);

		// Token: 0x0600016B RID: 363
		void RecordStudentDownloadedLectureNote(int PersonId, int NotetakerDocumentId);

		// Token: 0x0600016C RID: 364
		IList<DateTime> LoadUniqueAvailableCourseStartDatesByNotetaker(int NotetakerId);

		// Token: 0x0600016D RID: 365
		IList<LookupCourseBaseDTO> LoadNotetakerAvailableCourses(int NotetakerId, DateTime StartDate, DateTime EndDate);

		// Token: 0x0600016E RID: 366
		IList<ServiceRequestBaseDTO> LoadUniqueStudentsReceivingNotes(int NotetakerId, int LuCourseId);

		// Token: 0x0600016F RID: 367
		bool AssignNotetaker(int studentPid, int studentLucid, int serviceProviderId, int serviceProviderLucid);

		// Token: 0x06000170 RID: 368
		NotetakerBaseWithLookupCourseBaseDTO CancelNotetakerAssignment(int studentPid, int studentLucid, string why);
	}
}
