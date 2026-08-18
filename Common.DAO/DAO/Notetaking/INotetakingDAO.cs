using System;
using System.Collections.Generic;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.CourseRegistrations;
using TechnoPro.Common.Public.Entities.LookupCourses;
using TechnoPro.Common.Public.Entities.Notetaking;
using TechnoPro.Common.Public.Entities.ServiceProvider;
using TechnoPro.Common.Public.Entities.ServiceProvidersOriginal;

namespace TechnoPro.Common.DAO.Notetaking
{
	// Token: 0x02000049 RID: 73
	public interface INotetakingDAO : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x06000189 RID: 393
		NotetakerBase LoadNotetakerBaseByUsername(string username);

		// Token: 0x0600018A RID: 394
		NotetakerBase LoadNotetakerBaseById(int ServiceProviderId);

		// Token: 0x0600018B RID: 395
		NotetakerBase LoadNotetakerBaseByNotetakeeAndCourse(int NotetakeePersonId, int NotetakeeLuCourseId);

		// Token: 0x0600018C RID: 396
		List<LectureNoteDescription> LoadLectureNoteDescriptionsByNotetakerAndCourse(int ServiceProviderId, int NotetakerLuCourseId);

		// Token: 0x0600018D RID: 397
		List<LectureNoteDescription> LoadLectureNoteDescriptionsByStudentAndCourse(int StudentPersonId, int StudentLuCourseId);

		// Token: 0x0600018E RID: 398
		LectureNote LoadLectureNoteById(int NotetakerDocumentId);

		// Token: 0x0600018F RID: 399
		List<NotetakerBaseWithLookupCourseBase> LoadMatchingNotetakersWithLectureNoteUploadsByCourse(int LuCourseId, int EquivalentSettingNum);

		// Token: 0x06000190 RID: 400
		List<LookupCourseBase> LoadEquivalentCourses(int LuCourseId, int EquivalentSettingNum);

		// Token: 0x06000191 RID: 401
		NotetakerBase LoadNotetakerBaseByStudentNumber(string StudentNumber);

		// Token: 0x06000192 RID: 402
		void ChangeCourseRegistrationStatus(int ServiceProviderApplicationCourseId, eRegistrationStatus NewStatus);

		// Token: 0x06000193 RID: 403
		NotetakerCourseRegistration RegisterNotetakerInCourse(int ServiceProviderId, int Lucid, bool? ExemptCourseFromDataSyncForStudent = null);

		// Token: 0x06000194 RID: 404
		NotetakerCourseRegistration LoadCourseRegistration(int ServiceProviderId, int Lucid);

		// Token: 0x06000195 RID: 405
		int CreateNotetakerAccount(SPProvider Provider);

		// Token: 0x06000196 RID: 406
		void RecordStudentDownloadedLectureNote(int PersonId, int NotetakerDocumentId);

		// Token: 0x06000197 RID: 407
		IList<DownloadedLectureNote> LoadStudentDownloadedLectureNoteHistory(int PersonId, int LuCourseId);

		// Token: 0x06000198 RID: 408
		IList<DownloadedLectureNote> LoadStudentDownloadedLectureNoteHistoryLastDateDownloadedForEachLectureNote(int PersonId, int LuCourseId);

		// Token: 0x06000199 RID: 409
		NotetakerBase LoadNotetakerBaseByEmail(string Email);

		// Token: 0x0600019A RID: 410
		int CreateLectureNote(LectureNote lectureNote);

		// Token: 0x0600019B RID: 411
		void UpdateLectureNote(LectureNote lectureNote);

		// Token: 0x0600019C RID: 412
		void DeleteLectureNote(int NotetakerDocumentId);

		// Token: 0x0600019D RID: 413
		IList<DateTime> LoadUniqueCourseStartDatesForNotetakerAvailableCourses(int NotetakerId);

		// Token: 0x0600019E RID: 414
		IList<LookupCourseBase> LoadNotetakerAvailableCourses(int NotetakerId, DateTime StartDate, DateTime EndDate);

		// Token: 0x0600019F RID: 415
		IList<ServiceRequestBase> LoadUniqueStudentsReceivingNotes(int NotetakerId, int LuCourseId, int ServiceProviderType);

		// Token: 0x060001A0 RID: 416
		int CreateOrRetrieveSpAppIdForCourses(int ServiceProviderId);

		// Token: 0x060001A1 RID: 417
		void AddServiceProviderApplicationCourse(int spaid, int lucid);

		// Token: 0x060001A2 RID: 418
		bool AssignNotetaker(int studentPid, int studentLucid, int serviceProviderId, int serviceProviderLucid);

		// Token: 0x060001A3 RID: 419
		NotetakerBaseWithLookupCourseBase CancelNotetakerAssignment(int studentPid, int studentLucid, string why);
	}
}
