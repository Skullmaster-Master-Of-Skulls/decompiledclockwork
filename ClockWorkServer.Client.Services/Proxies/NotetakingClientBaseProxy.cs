using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.Notetaking;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x020000FA RID: 250
	internal class NotetakingClientBaseProxy : ClientBase<INotetaking>, INotetaking, IService
	{
		// Token: 0x060009AA RID: 2474 RVA: 0x00018CAC File Offset: 0x00016EAC
		public NotetakingClientBaseProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x060009AB RID: 2475 RVA: 0x00018CB7 File Offset: 0x00016EB7
		public NotetakingClientBaseProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x060009AC RID: 2476 RVA: 0x00018CC4 File Offset: 0x00016EC4
		public LoadEquivalentCoursesResp LoadEquivalentCourses(LoadEquivalentCoursesReq Request)
		{
			return base.Channel.LoadEquivalentCourses(Request);
		}

		// Token: 0x060009AD RID: 2477 RVA: 0x00018CE4 File Offset: 0x00016EE4
		public LoadLectureNoteByIdResp LoadLectureNoteById(LoadLectureNoteByIdReq Request)
		{
			return base.Channel.LoadLectureNoteById(Request);
		}

		// Token: 0x060009AE RID: 2478 RVA: 0x00018D04 File Offset: 0x00016F04
		public LoadLectureNoteDescriptionsByNotetakerAndCourseResp LoadLectureNoteDescriptionsByNotetakerAndCourse(LoadLectureNoteDescriptionsByNotetakerAndCourseReq Request)
		{
			return base.Channel.LoadLectureNoteDescriptionsByNotetakerAndCourse(Request);
		}

		// Token: 0x060009AF RID: 2479 RVA: 0x00018D24 File Offset: 0x00016F24
		public LoadMatchingNotetakersWithLectureNoteUploadsByCourseResp LoadMatchingNotetakersWithLectureNoteUploadsByCourse(LoadMatchingNotetakersWithLectureNoteUploadsByCourseReq Request)
		{
			return base.Channel.LoadMatchingNotetakersWithLectureNoteUploadsByCourse(Request);
		}

		// Token: 0x060009B0 RID: 2480 RVA: 0x00018D44 File Offset: 0x00016F44
		public LoadNotetakerBaseByIdResp LoadNotetakerBaseById(LoadNotetakerBaseByIdReq Request)
		{
			return base.Channel.LoadNotetakerBaseById(Request);
		}

		// Token: 0x060009B1 RID: 2481 RVA: 0x00018D64 File Offset: 0x00016F64
		public LoadNotetakerBaseByNotetakeeAndCourseResp LoadNotetakerBaseByNotetakeeAndCourse(LoadNotetakerBaseByNotetakeeAndCourseReq Request)
		{
			return base.Channel.LoadNotetakerBaseByNotetakeeAndCourse(Request);
		}

		// Token: 0x060009B2 RID: 2482 RVA: 0x00018D84 File Offset: 0x00016F84
		public LoadNotetakerBaseByUsernameResp LoadNotetakerBaseByUsername(LoadNotetakerBaseByUsernameReq Request)
		{
			return base.Channel.LoadNotetakerBaseByUsername(Request);
		}

		// Token: 0x060009B3 RID: 2483 RVA: 0x00018DA2 File Offset: 0x00016FA2
		public void AddPotentialCoursesForNotetaker(AddPotentialCoursesForNotetakerReq Request)
		{
			base.Channel.AddPotentialCoursesForNotetaker(Request);
		}

		// Token: 0x060009B4 RID: 2484 RVA: 0x00018DB4 File Offset: 0x00016FB4
		public CreateNotetakerAccountResp CreateNotetakerAccount(CreateNotetakerAccountReq Request)
		{
			return base.Channel.CreateNotetakerAccount(Request);
		}

		// Token: 0x060009B5 RID: 2485 RVA: 0x00018DD4 File Offset: 0x00016FD4
		public LoadStudentDownloadedLectureNoteHistoryResp LoadStudentDownloadedLectureNoteHistory(LoadStudentDownloadedLectureNoteHistoryReq Request)
		{
			return base.Channel.LoadStudentDownloadedLectureNoteHistory(Request);
		}

		// Token: 0x060009B6 RID: 2486 RVA: 0x00018DF2 File Offset: 0x00016FF2
		public void RecordStudentDownloadedLectureNote(RecordStudentDownloadedLectureNoteReq Request)
		{
			base.Channel.RecordStudentDownloadedLectureNote(Request);
		}

		// Token: 0x060009B7 RID: 2487 RVA: 0x00018E04 File Offset: 0x00017004
		public LoadStudentDownloadedLectureNoteHistoryLastDateDownloadedForEachLectureNoteResp LoadStudentDownloadedLectureNoteHistoryLastDateDownloadedForEachLectureNote(LoadStudentDownloadedLectureNoteHistoryLastDateDownloadedForEachLectureNoteReq request)
		{
			return base.Channel.LoadStudentDownloadedLectureNoteHistoryLastDateDownloadedForEachLectureNote(request);
		}

		// Token: 0x060009B8 RID: 2488 RVA: 0x00018E24 File Offset: 0x00017024
		public CreateLectureNoteResp CreateLectureNote(CreateLectureNoteReq Request)
		{
			return base.Channel.CreateLectureNote(Request);
		}

		// Token: 0x060009B9 RID: 2489 RVA: 0x00018E44 File Offset: 0x00017044
		public UpdateLectureNoteResp UpdateLectureNote(UpdateLectureNoteReq Request)
		{
			return base.Channel.UpdateLectureNote(Request);
		}

		// Token: 0x060009BA RID: 2490 RVA: 0x00018E62 File Offset: 0x00017062
		public void DeleteLectureNote(DeleteLectureNoteReq Request)
		{
			base.Channel.DeleteLectureNote(Request);
		}

		// Token: 0x060009BB RID: 2491 RVA: 0x00018E74 File Offset: 0x00017074
		public LoadUniqueAvailableCourseStartDatesByNotetakerResp LoadUniqueAvailableCourseStartDatesByNotetaker(LoadUniqueAvailableCourseStartDatesByNotetakerReq Request)
		{
			return base.Channel.LoadUniqueAvailableCourseStartDatesByNotetaker(Request);
		}

		// Token: 0x060009BC RID: 2492 RVA: 0x00018E94 File Offset: 0x00017094
		public LoadNotetakerAvailableCoursesResp LoadNotetakerAvailableCourses(LoadNotetakerAvailableCoursesReq Request)
		{
			return base.Channel.LoadNotetakerAvailableCourses(Request);
		}

		// Token: 0x060009BD RID: 2493 RVA: 0x00018EB4 File Offset: 0x000170B4
		public LoadUniqueStudentsReceivingNotesResp LoadUniqueStudentsReceivingNotes(LoadUniqueStudentsReceivingNotesReq Request)
		{
			return base.Channel.LoadUniqueStudentsReceivingNotes(Request);
		}

		// Token: 0x060009BE RID: 2494 RVA: 0x00018ED4 File Offset: 0x000170D4
		public AssignNotetakerResp AssignNotetaker(AssignNotetakerReq Request)
		{
			return base.Channel.AssignNotetaker(Request);
		}

		// Token: 0x060009BF RID: 2495 RVA: 0x00018EF4 File Offset: 0x000170F4
		public CancelNotetakerAssignmentResp CancelNotetakerAssignment(CancelNotetakerAssignmentReq Request)
		{
			return base.Channel.CancelNotetakerAssignment(Request);
		}

		// Token: 0x060009C0 RID: 2496 RVA: 0x00018F14 File Offset: 0x00017114
		public LoadLectureNoteDescriptionsByStudentAndCourseResp LoadLectureNoteDescriptionsByStudentAndCourse(LoadLectureNoteDescriptionsByStudentAndCourseReq Request)
		{
			return base.Channel.LoadLectureNoteDescriptionsByStudentAndCourse(Request);
		}
	}
}
