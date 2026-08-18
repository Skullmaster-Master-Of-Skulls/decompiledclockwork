using System;
using System.Collections.Generic;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.CourseRegistrations;
using TechnoPro.Common.Public.Entities.DataSync;
using TechnoPro.Common.Public.Entities.LookupCourses;
using TechnoPro.Common.Public.Entities.Notetaking;
using TechnoPro.Common.Public.Entities.ServiceProvider;
using TechnoPro.Common.Public.Entities.ServiceProvidersOriginal;

namespace TechnoPro.Common.ICore.Notetaking
{
	// Token: 0x0200005B RID: 91
	public interface INotetakingManager : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x0600026F RID: 623
		NotetakerBase LoadNotetakerBaseByUsername(string username);

		// Token: 0x06000270 RID: 624
		NotetakerBase LoadNotetakerBaseById(int ServiceProviderId);

		// Token: 0x06000271 RID: 625
		NotetakerBase LoadNotetakerBaseByNotetakeeAndCourse(int NotetakeePersonId, int NotetakeeLuCourseId);

		// Token: 0x06000272 RID: 626
		List<LectureNoteDescription> LoadLectureNoteDescriptionsByNotetakerAndCourse(int ServiceProviderId, int NotetakerLuCourseId);

		// Token: 0x06000273 RID: 627
		List<LectureNoteDescription> LoadLectureNoteDescriptionsByStudentAndCourse(int StudentPersonId, int StudentLuCourseId);

		// Token: 0x06000274 RID: 628
		LectureNote LoadLectureNoteById(int NotetakerDocumentId);

		// Token: 0x06000275 RID: 629
		List<NotetakerBaseWithLookupCourseBase> LoadMatchingNotetakersWithLectureNoteUploadsByCourse(int LuCourseId);

		// Token: 0x06000276 RID: 630
		List<LookupCourseBase> LoadEquivalentCourses(int LuCourseId);

		// Token: 0x06000277 RID: 631
		NotetakerBase LoadNotetakerBaseByStudentNumber(string StudentNumber);

		// Token: 0x06000278 RID: 632
		void ChangeCourseRegistrationStatus(int ServiceProviderApplicationCourseId, eRegistrationStatus NewStatus);

		// Token: 0x06000279 RID: 633
		NotetakerCourseRegistration RegisterNotetakerInCourse(int ServiceProviderId, int Lucid, bool? ExemptCourseFromDataSyncForStudent = null);

		// Token: 0x0600027A RID: 634
		NotetakerCourseRegistration LoadCourseRegistration(int ServiceProviderId, int Lucid);

		// Token: 0x0600027B RID: 635
		void AddPotentialCoursesForNotetaker(int ServiceProviderId, IList<DataSyncExternalCourse> ExternalCourses);

		// Token: 0x0600027C RID: 636
		int CreateNotetakerAccount(SPProvider Provider);

		// Token: 0x0600027D RID: 637
		void RecordStudentDownloadedLectureNote(int PersonId, int NotetakerDocumentId);

		// Token: 0x0600027E RID: 638
		IList<DownloadedLectureNote> LoadStudentDownloadedLectureNoteHistory(int PersonId, int LuCourseId);

		// Token: 0x0600027F RID: 639
		IList<DownloadedLectureNote> LoadStudentDownloadedLectureNoteHistoryLastDateDownloadedForEachLectureNote(int PersonId, int LuCourseId);

		// Token: 0x06000280 RID: 640
		NotetakerBase LoadNotetakerBaseByEmail(string Email);

		// Token: 0x06000281 RID: 641
		int CreateLectureNote(LectureNote lectureNote);

		// Token: 0x06000282 RID: 642
		void UpdateLectureNote(LectureNote lectureNote);

		// Token: 0x06000283 RID: 643
		void DeleteLectureNote(int NotetakerDocumentId);

		// Token: 0x06000284 RID: 644
		IList<DateTime> LoadUniqueAvailableCourseStartDatesByNotetaker(int NotetakerId);

		// Token: 0x06000285 RID: 645
		IList<LookupCourseBase> LoadNotetakerAvailableCourses(int NotetakerId, DateTime StartDate, DateTime EndDate);

		// Token: 0x06000286 RID: 646
		IList<ServiceRequestBase> LoadUniqueStudentsReceivingNotes(int NotetakerId, int LuCourseId);

		// Token: 0x06000287 RID: 647
		bool AssignNotetaker(int studentPid, int studentLucid, int serviceProviderId, int serviceProviderLucid);

		// Token: 0x06000288 RID: 648
		NotetakerBaseWithLookupCourseBase CancelNotetakerAssignment(int studentPid, int studentLucid, string why);
	}
}
