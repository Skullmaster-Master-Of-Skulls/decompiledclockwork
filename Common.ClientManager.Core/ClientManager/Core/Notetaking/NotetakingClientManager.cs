using System;
using System.Collections.Generic;
using TechnoPro.ClockWorkServer.Client.ServiceFactory;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.DataSync;
using TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses;
using TechnoPro.ClockWorkServer.Contracts.DTO.Notetaking;
using TechnoPro.ClockWorkServer.Contracts.DTO.ServiceProvider;
using TechnoPro.ClockWorkServer.Contracts.DTO.ServiceProviderOriginal;
using TechnoPro.Common.ClientManager.ICore;
using TechnoPro.Common.ClientManager.ICore.Notetaking;
using TechnoPro.Common.Public;
using TechnoPro.Common.Unity.IoC;

namespace TechnoPro.Common.ClientManager.Core.Notetaking
{
	// Token: 0x02000037 RID: 55
	public class NotetakingClientManager : INotetakingClientManager, IWebService
	{
		// Token: 0x060001EF RID: 495 RVA: 0x000095B8 File Offset: 0x000077B8
		public IList<LectureNoteDescriptionDTO> LoadLectureNoteDescriptionsByNotetakerAndCourse(int ServiceProviderId, int NotetakerLuCourseId)
		{
			LoadLectureNoteDescriptionsByNotetakerAndCourseReq loadLectureNoteDescriptionsByNotetakerAndCourseReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadLectureNoteDescriptionsByNotetakerAndCourseReq>();
			loadLectureNoteDescriptionsByNotetakerAndCourseReq.NotetakerLuCourseId = NotetakerLuCourseId;
			loadLectureNoteDescriptionsByNotetakerAndCourseReq.ServiceProviderId = ServiceProviderId;
			return ClientServiceFactory.GetClientInstance<INotetaking>().LoadLectureNoteDescriptionsByNotetakerAndCourse(loadLectureNoteDescriptionsByNotetakerAndCourseReq).LectureNoteDescriptions;
		}

		// Token: 0x060001F0 RID: 496 RVA: 0x000095F8 File Offset: 0x000077F8
		public IList<LectureNoteDescriptionDTO> LoadLectureNoteDescriptionsByStudentAndCourse(int StudentPersonId, int StudentLuCourseId)
		{
			LoadLectureNoteDescriptionsByStudentAndCourseReq loadLectureNoteDescriptionsByStudentAndCourseReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadLectureNoteDescriptionsByStudentAndCourseReq>();
			loadLectureNoteDescriptionsByStudentAndCourseReq.StudentPersonId = StudentPersonId;
			loadLectureNoteDescriptionsByStudentAndCourseReq.StudentLuCourseId = StudentLuCourseId;
			return ClientServiceFactory.GetClientInstance<INotetaking>().LoadLectureNoteDescriptionsByStudentAndCourse(loadLectureNoteDescriptionsByStudentAndCourseReq).LectureNoteDescriptions;
		}

		// Token: 0x060001F1 RID: 497 RVA: 0x00009638 File Offset: 0x00007838
		public LectureNoteDTO LoadLectureNoteById(int NotetakerDocumentId)
		{
			LoadLectureNoteByIdReq loadLectureNoteByIdReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadLectureNoteByIdReq>();
			loadLectureNoteByIdReq.NotetakerDocumentId = NotetakerDocumentId;
			return ClientServiceFactory.GetClientInstance<INotetaking>().LoadLectureNoteById(loadLectureNoteByIdReq).LectureNote;
		}

		// Token: 0x060001F2 RID: 498 RVA: 0x00009670 File Offset: 0x00007870
		public IList<NotetakerBaseWithLookupCourseBaseDTO> LoadMatchingNotetakersWithLectureNoteUploadsByCourse(int LuCourseId)
		{
			LoadMatchingNotetakersWithLectureNoteUploadsByCourseReq loadMatchingNotetakersWithLectureNoteUploadsByCourseReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadMatchingNotetakersWithLectureNoteUploadsByCourseReq>();
			loadMatchingNotetakersWithLectureNoteUploadsByCourseReq.LuCourseId = LuCourseId;
			return ClientServiceFactory.GetClientInstance<INotetaking>().LoadMatchingNotetakersWithLectureNoteUploadsByCourse(loadMatchingNotetakersWithLectureNoteUploadsByCourseReq).Notetakers;
		}

		// Token: 0x060001F3 RID: 499 RVA: 0x000096A8 File Offset: 0x000078A8
		public IList<DownloadedLectureNoteDTO> LoadStudentDownloadedLectureNoteHistory(int PersonId, int LuCourseId)
		{
			LoadStudentDownloadedLectureNoteHistoryReq loadStudentDownloadedLectureNoteHistoryReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadStudentDownloadedLectureNoteHistoryReq>();
			loadStudentDownloadedLectureNoteHistoryReq.PersonId = PersonId;
			loadStudentDownloadedLectureNoteHistoryReq.LuCourseId = LuCourseId;
			return ClientServiceFactory.GetClientInstance<INotetaking>().LoadStudentDownloadedLectureNoteHistory(loadStudentDownloadedLectureNoteHistoryReq).DownloadedLectureNotes;
		}

		// Token: 0x060001F4 RID: 500 RVA: 0x000096E8 File Offset: 0x000078E8
		public IList<DownloadedLectureNoteDTO> LoadStudentDownloadedLectureNoteHistoryLastDateDownloadedForEachLectureNote(int PersonId, int LuCourseId)
		{
			LoadStudentDownloadedLectureNoteHistoryLastDateDownloadedForEachLectureNoteReq loadStudentDownloadedLectureNoteHistoryLastDateDownloadedForEachLectureNoteReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadStudentDownloadedLectureNoteHistoryLastDateDownloadedForEachLectureNoteReq>();
			loadStudentDownloadedLectureNoteHistoryLastDateDownloadedForEachLectureNoteReq.PersonId = PersonId;
			loadStudentDownloadedLectureNoteHistoryLastDateDownloadedForEachLectureNoteReq.LuCourseId = LuCourseId;
			return ClientServiceFactory.GetClientInstance<INotetaking>().LoadStudentDownloadedLectureNoteHistoryLastDateDownloadedForEachLectureNote(loadStudentDownloadedLectureNoteHistoryLastDateDownloadedForEachLectureNoteReq).DownloadedLectureNotes;
		}

		// Token: 0x060001F5 RID: 501 RVA: 0x00009728 File Offset: 0x00007928
		public int CreateLectureNote(LectureNoteDTO lectureNote)
		{
			CreateLectureNoteReq createLectureNoteReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<CreateLectureNoteReq>();
			createLectureNoteReq.LectureNote = lectureNote;
			return ClientServiceFactory.GetClientInstance<INotetaking>().CreateLectureNote(createLectureNoteReq).NotetakerDocumentId;
		}

		// Token: 0x060001F6 RID: 502 RVA: 0x00009760 File Offset: 0x00007960
		public void UpdateLectureNote(LectureNoteDTO lectureNote)
		{
			UpdateLectureNoteReq updateLectureNoteReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<UpdateLectureNoteReq>();
			updateLectureNoteReq.LectureNote = lectureNote;
			ClientServiceFactory.GetClientInstance<INotetaking>().UpdateLectureNote(updateLectureNoteReq);
		}

		// Token: 0x060001F7 RID: 503 RVA: 0x00009790 File Offset: 0x00007990
		public void DeleteLectureNote(int NotetakerDocumentId)
		{
			DeleteLectureNoteReq deleteLectureNoteReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<DeleteLectureNoteReq>();
			deleteLectureNoteReq.NotetakerDocumentId = NotetakerDocumentId;
			ClientServiceFactory.GetClientInstance<INotetaking>().DeleteLectureNote(deleteLectureNoteReq);
		}

		// Token: 0x060001F8 RID: 504 RVA: 0x000097C0 File Offset: 0x000079C0
		public NotetakerBaseDTO LoadNotetakerBaseByUsername(string username)
		{
			LoadNotetakerBaseByUsernameReq loadNotetakerBaseByUsernameReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadNotetakerBaseByUsernameReq>();
			loadNotetakerBaseByUsernameReq.Username = username;
			return ClientServiceFactory.GetClientInstance<INotetaking>().LoadNotetakerBaseByUsername(loadNotetakerBaseByUsernameReq).NotetakerBase;
		}

		// Token: 0x060001F9 RID: 505 RVA: 0x000097F8 File Offset: 0x000079F8
		public NotetakerBaseDTO LoadNotetakerBaseById(int ServiceProviderId)
		{
			LoadNotetakerBaseByIdReq loadNotetakerBaseByIdReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadNotetakerBaseByIdReq>();
			loadNotetakerBaseByIdReq.ServiceProviderId = ServiceProviderId;
			return ClientServiceFactory.GetClientInstance<INotetaking>().LoadNotetakerBaseById(loadNotetakerBaseByIdReq).NotetakerBase;
		}

		// Token: 0x060001FA RID: 506 RVA: 0x00009830 File Offset: 0x00007A30
		public NotetakerBaseDTO LoadNotetakerBaseByNotetakeeAndCourse(int NotetakeePersonId, int NotetakeeLuCourseId)
		{
			LoadNotetakerBaseByNotetakeeAndCourseReq loadNotetakerBaseByNotetakeeAndCourseReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadNotetakerBaseByNotetakeeAndCourseReq>();
			loadNotetakerBaseByNotetakeeAndCourseReq.NotetakeePersonId = NotetakeePersonId;
			loadNotetakerBaseByNotetakeeAndCourseReq.NotetakeeLuCourseId = NotetakeeLuCourseId;
			return ClientServiceFactory.GetClientInstance<INotetaking>().LoadNotetakerBaseByNotetakeeAndCourse(loadNotetakerBaseByNotetakeeAndCourseReq).NotetakerBase;
		}

		// Token: 0x060001FB RID: 507 RVA: 0x00009870 File Offset: 0x00007A70
		public IList<LookupCourseBaseDTO> LoadEquivalentCourses(int LuCourseId)
		{
			LoadEquivalentCoursesReq loadEquivalentCoursesReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadEquivalentCoursesReq>();
			loadEquivalentCoursesReq.LuCourseId = LuCourseId;
			return ClientServiceFactory.GetClientInstance<INotetaking>().LoadEquivalentCourses(loadEquivalentCoursesReq).Courses;
		}

		// Token: 0x060001FC RID: 508 RVA: 0x000098A8 File Offset: 0x00007AA8
		public void AddPotentialCoursesForNotetaker(int ServiceProviderId, IList<DataSyncExternalCourseDTO> ExternalCourses)
		{
			AddPotentialCoursesForNotetakerReq addPotentialCoursesForNotetakerReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<AddPotentialCoursesForNotetakerReq>();
			addPotentialCoursesForNotetakerReq.ServiceProviderId = ServiceProviderId;
			addPotentialCoursesForNotetakerReq.ExternalCourses = ExternalCourses;
			ClientServiceFactory.GetClientInstance<INotetaking>().AddPotentialCoursesForNotetaker(addPotentialCoursesForNotetakerReq);
		}

		// Token: 0x060001FD RID: 509 RVA: 0x000098E0 File Offset: 0x00007AE0
		public int CreateNotetakerAccount(SPProviderDTO Provider)
		{
			CreateNotetakerAccountReq createNotetakerAccountReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<CreateNotetakerAccountReq>();
			createNotetakerAccountReq.Notetaker = Provider;
			return ClientServiceFactory.GetClientInstance<INotetaking>().CreateNotetakerAccount(createNotetakerAccountReq).ServiceProviderId;
		}

		// Token: 0x060001FE RID: 510 RVA: 0x00009918 File Offset: 0x00007B18
		public void RecordStudentDownloadedLectureNote(int PersonId, int NotetakerDocumentId)
		{
			RecordStudentDownloadedLectureNoteReq recordStudentDownloadedLectureNoteReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<RecordStudentDownloadedLectureNoteReq>();
			recordStudentDownloadedLectureNoteReq.PersonId = PersonId;
			recordStudentDownloadedLectureNoteReq.NotetakerDocumentId = NotetakerDocumentId;
			ClientServiceFactory.GetClientInstance<INotetaking>().RecordStudentDownloadedLectureNote(recordStudentDownloadedLectureNoteReq);
		}

		// Token: 0x060001FF RID: 511 RVA: 0x00009950 File Offset: 0x00007B50
		public IList<DateTime> LoadUniqueAvailableCourseStartDatesByNotetaker(int NotetakerId)
		{
			LoadUniqueAvailableCourseStartDatesByNotetakerReq loadUniqueAvailableCourseStartDatesByNotetakerReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadUniqueAvailableCourseStartDatesByNotetakerReq>();
			loadUniqueAvailableCourseStartDatesByNotetakerReq.NotetakerId = NotetakerId;
			return ClientServiceFactory.GetClientInstance<INotetaking>().LoadUniqueAvailableCourseStartDatesByNotetaker(loadUniqueAvailableCourseStartDatesByNotetakerReq).UniqueDates;
		}

		// Token: 0x06000200 RID: 512 RVA: 0x00009988 File Offset: 0x00007B88
		public IList<LookupCourseBaseDTO> LoadNotetakerAvailableCourses(int NotetakerId, DateTime StartDate, DateTime EndDate)
		{
			LoadNotetakerAvailableCoursesReq loadNotetakerAvailableCoursesReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadNotetakerAvailableCoursesReq>();
			loadNotetakerAvailableCoursesReq.NotetakerId = NotetakerId;
			loadNotetakerAvailableCoursesReq.StartDate = StartDate;
			loadNotetakerAvailableCoursesReq.EndDate = EndDate;
			return ClientServiceFactory.GetClientInstance<INotetaking>().LoadNotetakerAvailableCourses(loadNotetakerAvailableCoursesReq).CourseBases;
		}

		// Token: 0x06000201 RID: 513 RVA: 0x000099D0 File Offset: 0x00007BD0
		public IList<ServiceRequestBaseDTO> LoadUniqueStudentsReceivingNotes(int NotetakerId, int LuCourseId)
		{
			LoadUniqueStudentsReceivingNotesReq loadUniqueStudentsReceivingNotesReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadUniqueStudentsReceivingNotesReq>();
			loadUniqueStudentsReceivingNotesReq.NotetakerId = NotetakerId;
			loadUniqueStudentsReceivingNotesReq.NotetakerLuCourseId = LuCourseId;
			return ClientServiceFactory.GetClientInstance<INotetaking>().LoadUniqueStudentsReceivingNotes(loadUniqueStudentsReceivingNotesReq).Assignments;
		}

		// Token: 0x06000202 RID: 514 RVA: 0x00009A10 File Offset: 0x00007C10
		public bool AssignNotetaker(int studentPid, int studentLucid, int serviceProviderId, int serviceProviderLucid)
		{
			AssignNotetakerReq assignNotetakerReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<AssignNotetakerReq>();
			assignNotetakerReq.StudentPersonId = studentPid;
			assignNotetakerReq.StudentLuCourseId = studentLucid;
			assignNotetakerReq.NotetakerId = serviceProviderId;
			assignNotetakerReq.NotetakerLuCourseId = serviceProviderLucid;
			return ClientServiceFactory.GetClientInstance<INotetaking>().AssignNotetaker(assignNotetakerReq).WasThisTheFirstStudentAssignedToThisNotetakerAndCourse;
		}

		// Token: 0x06000203 RID: 515 RVA: 0x00009A60 File Offset: 0x00007C60
		public NotetakerBaseWithLookupCourseBaseDTO CancelNotetakerAssignment(int studentPid, int studentLucid, string why)
		{
			CancelNotetakerAssignmentReq cancelNotetakerAssignmentReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<CancelNotetakerAssignmentReq>();
			cancelNotetakerAssignmentReq.StudentPersonId = studentPid;
			cancelNotetakerAssignmentReq.StudentLuCourseId = studentLucid;
			cancelNotetakerAssignmentReq.Why = why;
			return ClientServiceFactory.GetClientInstance<INotetaking>().CancelNotetakerAssignment(cancelNotetakerAssignmentReq).NotetakerAndCourse;
		}
	}
}
