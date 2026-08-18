using System;
using System.Collections.Generic;
using TechnoPro.ClockWorkServer.Contracts.DTO.DataSync;
using TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses;
using TechnoPro.ClockWorkServer.Contracts.DTO.Notetaking;
using TechnoPro.ClockWorkServer.Contracts.DTO.ServiceProvider;
using TechnoPro.ClockWorkServer.Contracts.DTO.ServiceProviderOriginal;
using TechnoPro.Common.ClientManager.ICore;
using TechnoPro.Common.ClientManager.ICore.Notetaking;
using TechnoPro.Common.Public;
using TechnoPro.Common.Unity.IoC;
using TechnoPro.Common.Web.Security.Proxy;

namespace TechnoPro.Common.Rest.ClientManager.Core.Notetaking
{
	// Token: 0x0200002C RID: 44
	public class NotetakingRestClientManager : BearerTokenRestProxy<INotetakingClientManager>, INotetakingClientManager, IWebService
	{
		// Token: 0x06000188 RID: 392 RVA: 0x00005BA0 File Offset: 0x00003DA0
		public NotetakingRestClientManager(string serviceAddress, string token = null) : base(serviceAddress, token)
		{
		}

		// Token: 0x06000189 RID: 393 RVA: 0x00005BAA File Offset: 0x00003DAA
		public NotetakingRestClientManager(string serviceAddress, string serviceAddressSuffix, string token = null) : base(serviceAddress, serviceAddressSuffix, token)
		{
		}

		// Token: 0x0600018A RID: 394 RVA: 0x00005BB5 File Offset: 0x00003DB5
		public IList<LectureNoteDescriptionDTO> LoadLectureNoteDescriptionsByNotetakerAndCourse(int notetakeePersonId, int notetakeeLuCourseId)
		{
			return base.GetMany<LectureNoteDescriptionDTO>(string.Format("notetaking/notetakerbase/notetakeepersonid/{0}/notetakeelucourseid/{1}", notetakeePersonId, notetakeeLuCourseId), true);
		}

		// Token: 0x0600018B RID: 395 RVA: 0x00005BD4 File Offset: 0x00003DD4
		public IList<LectureNoteDescriptionDTO> LoadLectureNoteDescriptionsByStudentAndCourse(int StudentPersonId, int StudentLuCourseId)
		{
			return base.GetMany<LectureNoteDescriptionDTO>(string.Format("notetaking/lecturenotedescriptions/studentpersonid/{0}/studentlucourseid/{1}", StudentPersonId, StudentLuCourseId), true);
		}

		// Token: 0x0600018C RID: 396 RVA: 0x00005BF3 File Offset: 0x00003DF3
		public LectureNoteDTO LoadLectureNoteById(int NotetakerDocumentId)
		{
			return base.Get<LectureNoteDTO>(string.Format("notetaking/lecturenote/notetakerdocumentid/{0}", NotetakerDocumentId), true);
		}

		// Token: 0x0600018D RID: 397 RVA: 0x00005C0C File Offset: 0x00003E0C
		public IList<NotetakerBaseWithLookupCourseBaseDTO> LoadMatchingNotetakersWithLectureNoteUploadsByCourse(int LuCourseId)
		{
			return base.GetMany<NotetakerBaseWithLookupCourseBaseDTO>(string.Format("notetaking/matchingnotetakerswithlecturenoteuploads/lucouseid/{0}", LuCourseId), true);
		}

		// Token: 0x0600018E RID: 398 RVA: 0x00005C25 File Offset: 0x00003E25
		public IList<DownloadedLectureNoteDTO> LoadStudentDownloadedLectureNoteHistory(int PersonId, int LuCourseId)
		{
			return base.GetMany<DownloadedLectureNoteDTO>(string.Format("notetaking/studentdonwloadedlecturenotehistory/personid/{0}/lucourseid/{1}", PersonId, LuCourseId), true);
		}

		// Token: 0x0600018F RID: 399 RVA: 0x00005C44 File Offset: 0x00003E44
		public IList<DownloadedLectureNoteDTO> LoadStudentDownloadedLectureNoteHistoryLastDateDownloadedForEachLectureNote(int PersonId, int LuCourseId)
		{
			return base.GetMany<DownloadedLectureNoteDTO>(string.Format("notetaking/studentdonwloadedlecturenotehistorylastdatedownloadedforeachlecturenote/personid/{0}/lucourseid/{1}", PersonId, LuCourseId), true);
		}

		// Token: 0x06000190 RID: 400 RVA: 0x00005C63 File Offset: 0x00003E63
		public int CreateLectureNote(LectureNoteDTO lectureNote)
		{
			return base.Post<LectureNoteDTO, int>(lectureNote, "notetaking/createlecturenote");
		}

		// Token: 0x06000191 RID: 401 RVA: 0x00005C71 File Offset: 0x00003E71
		public void UpdateLectureNote(LectureNoteDTO lectureNote)
		{
			base.Put<LectureNoteDTO>(lectureNote, "notetaking/updatelecturenote");
		}

		// Token: 0x06000192 RID: 402 RVA: 0x00005C7F File Offset: 0x00003E7F
		public void DeleteLectureNote(int NotetakerDocumentId)
		{
			base.Delete(string.Format("notetaking/lecturenote/notetakerdocumentid/{0}", NotetakerDocumentId));
		}

		// Token: 0x06000193 RID: 403 RVA: 0x00005C97 File Offset: 0x00003E97
		public NotetakerBaseDTO LoadNotetakerBaseByUsername(string username)
		{
			return base.Get<NotetakerBaseDTO>(string.Format("notetaking/notetakerbase/username/{0}", username), true);
		}

		// Token: 0x06000194 RID: 404 RVA: 0x00005CAB File Offset: 0x00003EAB
		public NotetakerBaseDTO LoadNotetakerBaseById(int ServiceProviderId)
		{
			return base.Get<NotetakerBaseDTO>(string.Format("notetaking/notetakerbase/serviceproviderid/{0}", ServiceProviderId), true);
		}

		// Token: 0x06000195 RID: 405 RVA: 0x00005CC4 File Offset: 0x00003EC4
		public NotetakerBaseDTO LoadNotetakerBaseByNotetakeeAndCourse(int NotetakeePersonId, int NotetakeeLuCourseId)
		{
			return base.Get<NotetakerBaseDTO>(string.Format("notetaking/notetakerbase/notetakeepersonid/{0}/notetakeelucourseid/{1}", NotetakeePersonId, NotetakeeLuCourseId), true);
		}

		// Token: 0x06000196 RID: 406 RVA: 0x00005CE3 File Offset: 0x00003EE3
		public IList<LookupCourseBaseDTO> LoadEquivalentCourses(int LuCourseId)
		{
			return base.GetMany<LookupCourseBaseDTO>(string.Format("notetaking/equivalentcourses/lucouseid/{0}", LuCourseId), true);
		}

		// Token: 0x06000197 RID: 407 RVA: 0x00005CFC File Offset: 0x00003EFC
		public void AddPotentialCoursesForNotetaker(int ServiceProviderId, IList<DataSyncExternalCourseDTO> ExternalCourses)
		{
			AddPotentialCoursesForNotetakerReq addPotentialCoursesForNotetakerReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<AddPotentialCoursesForNotetakerReq>();
			addPotentialCoursesForNotetakerReq.ServiceProviderId = ServiceProviderId;
			addPotentialCoursesForNotetakerReq.ExternalCourses = ExternalCourses;
			base.Post<AddPotentialCoursesForNotetakerReq>(addPotentialCoursesForNotetakerReq, "notetaking/addpotentialcoursesfornotetaker");
		}

		// Token: 0x06000198 RID: 408 RVA: 0x00005D30 File Offset: 0x00003F30
		public int CreateNotetakerAccount(SPProviderDTO Provider)
		{
			CreateNotetakerAccountReq createNotetakerAccountReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<CreateNotetakerAccountReq>();
			createNotetakerAccountReq.Notetaker = Provider;
			return base.Post<CreateNotetakerAccountReq, int>(createNotetakerAccountReq, "notetaking/createnotetakeraccount");
		}

		// Token: 0x06000199 RID: 409 RVA: 0x00005D5C File Offset: 0x00003F5C
		public void RecordStudentDownloadedLectureNote(int PersonId, int NotetakerDocumentId)
		{
			RecordStudentDownloadedLectureNoteReq recordStudentDownloadedLectureNoteReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<RecordStudentDownloadedLectureNoteReq>();
			recordStudentDownloadedLectureNoteReq.PersonId = PersonId;
			recordStudentDownloadedLectureNoteReq.NotetakerDocumentId = NotetakerDocumentId;
			base.Post<RecordStudentDownloadedLectureNoteReq>(recordStudentDownloadedLectureNoteReq, "notetaking/recordstudentdownloadedlecturenote");
		}

		// Token: 0x0600019A RID: 410 RVA: 0x00005D8E File Offset: 0x00003F8E
		public IList<DateTime> LoadUniqueAvailableCourseStartDatesByNotetaker(int NotetakerId)
		{
			return base.GetMany<DateTime>(string.Format("notetaking/uniqueavailablecoursestartdates/notetakerid/{0}", NotetakerId), true);
		}

		// Token: 0x0600019B RID: 411 RVA: 0x00005DA7 File Offset: 0x00003FA7
		public IList<LookupCourseBaseDTO> LoadNotetakerAvailableCourses(int NotetakerId, DateTime StartDate, DateTime EndDate)
		{
			return base.GetMany<LookupCourseBaseDTO>(string.Format("notetaking/notetakeravailablecourses/notetakerid/{0}/range/{1}/{2}", NotetakerId, StartDate, EndDate), true);
		}

		// Token: 0x0600019C RID: 412 RVA: 0x00005DCC File Offset: 0x00003FCC
		public IList<ServiceRequestBaseDTO> LoadUniqueStudentsReceivingNotes(int NotetakerId, int LuCourseId)
		{
			return base.GetMany<ServiceRequestBaseDTO>(string.Format("notetaking/uniquestudentsreceivingnotes/notetakerid/{0}/notetakerlucourseid/{1}", NotetakerId, LuCourseId), true);
		}

		// Token: 0x0600019D RID: 413 RVA: 0x00005DEC File Offset: 0x00003FEC
		public bool AssignNotetaker(int studentPid, int studentLucid, int serviceProviderId, int serviceProviderLucid)
		{
			AssignNotetakerReq assignNotetakerReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<AssignNotetakerReq>();
			assignNotetakerReq.StudentPersonId = studentPid;
			assignNotetakerReq.StudentLuCourseId = studentLucid;
			assignNotetakerReq.NotetakerId = serviceProviderId;
			assignNotetakerReq.NotetakerLuCourseId = serviceProviderLucid;
			return base.Post<AssignNotetakerReq, bool>(assignNotetakerReq, "notetaking/assignnotetaker");
		}

		// Token: 0x0600019E RID: 414 RVA: 0x00005E30 File Offset: 0x00004030
		public NotetakerBaseWithLookupCourseBaseDTO CancelNotetakerAssignment(int studentPid, int studentLucid, string why)
		{
			CancelNotetakerAssignmentReq cancelNotetakerAssignmentReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<CancelNotetakerAssignmentReq>();
			cancelNotetakerAssignmentReq.StudentPersonId = studentPid;
			cancelNotetakerAssignmentReq.StudentLuCourseId = studentLucid;
			cancelNotetakerAssignmentReq.Why = why;
			return base.Post<CancelNotetakerAssignmentReq, NotetakerBaseWithLookupCourseBaseDTO>(cancelNotetakerAssignmentReq, "notetaking/cancelnotetakerassignment");
		}
	}
}
