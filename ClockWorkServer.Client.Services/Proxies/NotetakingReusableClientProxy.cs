using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.Notetaking;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x020000F9 RID: 249
	public class NotetakingReusableClientProxy : WCFTokenBasedReusableClientProxy<INotetaking>, INotetaking, IService
	{
		// Token: 0x06000993 RID: 2451 RVA: 0x000187FA File Offset: 0x000169FA
		public NotetakingReusableClientProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x06000994 RID: 2452 RVA: 0x00018805 File Offset: 0x00016A05
		public NotetakingReusableClientProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x06000995 RID: 2453 RVA: 0x00018814 File Offset: 0x00016A14
		public LoadEquivalentCoursesResp LoadEquivalentCourses(LoadEquivalentCoursesReq Request)
		{
			return this.WrapServiceMethod<LoadEquivalentCoursesResp>(() => this.Proxy.LoadEquivalentCourses(Request));
		}

		// Token: 0x06000996 RID: 2454 RVA: 0x0001884C File Offset: 0x00016A4C
		public LoadLectureNoteByIdResp LoadLectureNoteById(LoadLectureNoteByIdReq Request)
		{
			return this.WrapServiceMethod<LoadLectureNoteByIdResp>(() => this.Proxy.LoadLectureNoteById(Request));
		}

		// Token: 0x06000997 RID: 2455 RVA: 0x00018884 File Offset: 0x00016A84
		public LoadLectureNoteDescriptionsByNotetakerAndCourseResp LoadLectureNoteDescriptionsByNotetakerAndCourse(LoadLectureNoteDescriptionsByNotetakerAndCourseReq Request)
		{
			return this.WrapServiceMethod<LoadLectureNoteDescriptionsByNotetakerAndCourseResp>(() => this.Proxy.LoadLectureNoteDescriptionsByNotetakerAndCourse(Request));
		}

		// Token: 0x06000998 RID: 2456 RVA: 0x000188BC File Offset: 0x00016ABC
		public LoadMatchingNotetakersWithLectureNoteUploadsByCourseResp LoadMatchingNotetakersWithLectureNoteUploadsByCourse(LoadMatchingNotetakersWithLectureNoteUploadsByCourseReq Request)
		{
			return this.WrapServiceMethod<LoadMatchingNotetakersWithLectureNoteUploadsByCourseResp>(() => this.Proxy.LoadMatchingNotetakersWithLectureNoteUploadsByCourse(Request));
		}

		// Token: 0x06000999 RID: 2457 RVA: 0x000188F4 File Offset: 0x00016AF4
		public LoadNotetakerBaseByIdResp LoadNotetakerBaseById(LoadNotetakerBaseByIdReq Request)
		{
			return this.WrapServiceMethod<LoadNotetakerBaseByIdResp>(() => this.Proxy.LoadNotetakerBaseById(Request));
		}

		// Token: 0x0600099A RID: 2458 RVA: 0x0001892C File Offset: 0x00016B2C
		public LoadNotetakerBaseByNotetakeeAndCourseResp LoadNotetakerBaseByNotetakeeAndCourse(LoadNotetakerBaseByNotetakeeAndCourseReq Request)
		{
			return this.WrapServiceMethod<LoadNotetakerBaseByNotetakeeAndCourseResp>(() => this.Proxy.LoadNotetakerBaseByNotetakeeAndCourse(Request));
		}

		// Token: 0x0600099B RID: 2459 RVA: 0x00018964 File Offset: 0x00016B64
		public LoadNotetakerBaseByUsernameResp LoadNotetakerBaseByUsername(LoadNotetakerBaseByUsernameReq Request)
		{
			return this.WrapServiceMethod<LoadNotetakerBaseByUsernameResp>(() => this.Proxy.LoadNotetakerBaseByUsername(Request));
		}

		// Token: 0x0600099C RID: 2460 RVA: 0x0001899C File Offset: 0x00016B9C
		public void AddPotentialCoursesForNotetaker(AddPotentialCoursesForNotetakerReq Request)
		{
			this.WrapServiceMethod(delegate()
			{
				this.Proxy.AddPotentialCoursesForNotetaker(Request);
			});
		}

		// Token: 0x0600099D RID: 2461 RVA: 0x000189D4 File Offset: 0x00016BD4
		public CreateNotetakerAccountResp CreateNotetakerAccount(CreateNotetakerAccountReq Request)
		{
			return this.WrapServiceMethod<CreateNotetakerAccountResp>(() => this.Proxy.CreateNotetakerAccount(Request));
		}

		// Token: 0x0600099E RID: 2462 RVA: 0x00018A0C File Offset: 0x00016C0C
		public LoadStudentDownloadedLectureNoteHistoryResp LoadStudentDownloadedLectureNoteHistory(LoadStudentDownloadedLectureNoteHistoryReq Request)
		{
			return this.WrapServiceMethod<LoadStudentDownloadedLectureNoteHistoryResp>(() => this.Proxy.LoadStudentDownloadedLectureNoteHistory(Request));
		}

		// Token: 0x0600099F RID: 2463 RVA: 0x00018A44 File Offset: 0x00016C44
		public void RecordStudentDownloadedLectureNote(RecordStudentDownloadedLectureNoteReq Request)
		{
			this.WrapServiceMethod(delegate()
			{
				this.Proxy.RecordStudentDownloadedLectureNote(Request);
			});
		}

		// Token: 0x060009A0 RID: 2464 RVA: 0x00018A7C File Offset: 0x00016C7C
		public LoadStudentDownloadedLectureNoteHistoryLastDateDownloadedForEachLectureNoteResp LoadStudentDownloadedLectureNoteHistoryLastDateDownloadedForEachLectureNote(LoadStudentDownloadedLectureNoteHistoryLastDateDownloadedForEachLectureNoteReq request)
		{
			return this.WrapServiceMethod<LoadStudentDownloadedLectureNoteHistoryLastDateDownloadedForEachLectureNoteResp>(() => this.Proxy.LoadStudentDownloadedLectureNoteHistoryLastDateDownloadedForEachLectureNote(request));
		}

		// Token: 0x060009A1 RID: 2465 RVA: 0x00018AB4 File Offset: 0x00016CB4
		public CreateLectureNoteResp CreateLectureNote(CreateLectureNoteReq Request)
		{
			return this.WrapServiceMethod<CreateLectureNoteResp>(() => this.Proxy.CreateLectureNote(Request));
		}

		// Token: 0x060009A2 RID: 2466 RVA: 0x00018AEC File Offset: 0x00016CEC
		public UpdateLectureNoteResp UpdateLectureNote(UpdateLectureNoteReq Request)
		{
			return this.WrapServiceMethod<UpdateLectureNoteResp>(() => this.Proxy.UpdateLectureNote(Request));
		}

		// Token: 0x060009A3 RID: 2467 RVA: 0x00018B24 File Offset: 0x00016D24
		public void DeleteLectureNote(DeleteLectureNoteReq Request)
		{
			this.WrapServiceMethod(delegate()
			{
				this.Proxy.DeleteLectureNote(Request);
			});
		}

		// Token: 0x060009A4 RID: 2468 RVA: 0x00018B5C File Offset: 0x00016D5C
		public LoadUniqueAvailableCourseStartDatesByNotetakerResp LoadUniqueAvailableCourseStartDatesByNotetaker(LoadUniqueAvailableCourseStartDatesByNotetakerReq Request)
		{
			return this.WrapServiceMethod<LoadUniqueAvailableCourseStartDatesByNotetakerResp>(() => this.Proxy.LoadUniqueAvailableCourseStartDatesByNotetaker(Request));
		}

		// Token: 0x060009A5 RID: 2469 RVA: 0x00018B94 File Offset: 0x00016D94
		public LoadNotetakerAvailableCoursesResp LoadNotetakerAvailableCourses(LoadNotetakerAvailableCoursesReq Request)
		{
			return this.WrapServiceMethod<LoadNotetakerAvailableCoursesResp>(() => this.Proxy.LoadNotetakerAvailableCourses(Request));
		}

		// Token: 0x060009A6 RID: 2470 RVA: 0x00018BCC File Offset: 0x00016DCC
		public LoadUniqueStudentsReceivingNotesResp LoadUniqueStudentsReceivingNotes(LoadUniqueStudentsReceivingNotesReq Request)
		{
			return this.WrapServiceMethod<LoadUniqueStudentsReceivingNotesResp>(() => this.Proxy.LoadUniqueStudentsReceivingNotes(Request));
		}

		// Token: 0x060009A7 RID: 2471 RVA: 0x00018C04 File Offset: 0x00016E04
		public AssignNotetakerResp AssignNotetaker(AssignNotetakerReq Request)
		{
			return this.WrapServiceMethod<AssignNotetakerResp>(() => this.Proxy.AssignNotetaker(Request));
		}

		// Token: 0x060009A8 RID: 2472 RVA: 0x00018C3C File Offset: 0x00016E3C
		public CancelNotetakerAssignmentResp CancelNotetakerAssignment(CancelNotetakerAssignmentReq Request)
		{
			return this.WrapServiceMethod<CancelNotetakerAssignmentResp>(() => this.Proxy.CancelNotetakerAssignment(Request));
		}

		// Token: 0x060009A9 RID: 2473 RVA: 0x00018C74 File Offset: 0x00016E74
		public LoadLectureNoteDescriptionsByStudentAndCourseResp LoadLectureNoteDescriptionsByStudentAndCourse(LoadLectureNoteDescriptionsByStudentAndCourseReq Request)
		{
			return this.WrapServiceMethod<LoadLectureNoteDescriptionsByStudentAndCourseResp>(() => this.Proxy.LoadLectureNoteDescriptionsByStudentAndCourse(Request));
		}
	}
}
