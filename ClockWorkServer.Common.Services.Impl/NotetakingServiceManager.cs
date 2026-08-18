using System;
using System.Collections.Generic;
using System.Linq;
using TechnoPro.ClockWorkServer.Common.Services.Impl.Adapters;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.DataSync;
using TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses;
using TechnoPro.ClockWorkServer.Contracts.DTO.Notetaking;
using TechnoPro.ClockWorkServer.Contracts.DTO.ServiceProviderOriginal;
using TechnoPro.Common.Core.Mappers.DataSync;
using TechnoPro.Common.Core.Mappers.LookupCourses;
using TechnoPro.Common.Core.Mappers.Notetaking;
using TechnoPro.Common.Core.Mappers.ServiceProvider;
using TechnoPro.Common.Core.Mappers.ServiceProvidersOriginal;
using TechnoPro.Common.Core.Notetaking;
using TechnoPro.Common.ICore.Notetaking;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities.DataSync;
using TechnoPro.Common.Public.Entities.LookupCourses;
using TechnoPro.Common.Public.Entities.Notetaking;
using TechnoPro.Common.Public.Entities.ServiceProvidersOriginal;

namespace TechnoPro.ClockWorkServer.Common.Services.Impl
{
	// Token: 0x0200006F RID: 111
	public class NotetakingServiceManager : INotetaking, IService
	{
		// Token: 0x06000414 RID: 1044 RVA: 0x000135D4 File Offset: 0x000117D4
		public int CheckConnectivity()
		{
			return 1;
		}

		// Token: 0x06000415 RID: 1045 RVA: 0x000135E8 File Offset: 0x000117E8
		public LoadNotetakerBaseByUsernameResp LoadNotetakerBaseByUsername(LoadNotetakerBaseByUsernameReq Request)
		{
			INotetakingManager notetakingManager = new NotetakingManager(Request.GetOperationContext());
			NotetakerBase notetakerBase = notetakingManager.LoadNotetakerBaseByUsername(Request.Username);
			return new LoadNotetakerBaseByUsernameResp
			{
				NotetakerBase = ((notetakerBase != null) ? notetakerBase.ToDTO() : null)
			};
		}

		// Token: 0x06000416 RID: 1046 RVA: 0x0001362C File Offset: 0x0001182C
		public LoadNotetakerBaseByIdResp LoadNotetakerBaseById(LoadNotetakerBaseByIdReq Request)
		{
			INotetakingManager notetakingManager = new NotetakingManager(Request.GetOperationContext());
			NotetakerBase notetakerBase = notetakingManager.LoadNotetakerBaseById(Request.ServiceProviderId);
			return new LoadNotetakerBaseByIdResp
			{
				NotetakerBase = ((notetakerBase != null) ? notetakerBase.ToDTO() : null)
			};
		}

		// Token: 0x06000417 RID: 1047 RVA: 0x00013670 File Offset: 0x00011870
		public LoadNotetakerBaseByNotetakeeAndCourseResp LoadNotetakerBaseByNotetakeeAndCourse(LoadNotetakerBaseByNotetakeeAndCourseReq Request)
		{
			INotetakingManager notetakingManager = new NotetakingManager(Request.GetOperationContext());
			NotetakerBase notetakerBase = notetakingManager.LoadNotetakerBaseByNotetakeeAndCourse(Request.NotetakeePersonId, Request.NotetakeeLuCourseId);
			return new LoadNotetakerBaseByNotetakeeAndCourseResp
			{
				NotetakerBase = ((notetakerBase != null) ? notetakerBase.ToDTO() : null)
			};
		}

		// Token: 0x06000418 RID: 1048 RVA: 0x000136BC File Offset: 0x000118BC
		public LoadLectureNoteDescriptionsByNotetakerAndCourseResp LoadLectureNoteDescriptionsByNotetakerAndCourse(LoadLectureNoteDescriptionsByNotetakerAndCourseReq Request)
		{
			INotetakingManager notetakingManager = new NotetakingManager(Request.GetOperationContext());
			List<LectureNoteDescription> list = notetakingManager.LoadLectureNoteDescriptionsByNotetakerAndCourse(Request.ServiceProviderId, Request.NotetakerLuCourseId);
			LoadLectureNoteDescriptionsByNotetakerAndCourseResp loadLectureNoteDescriptionsByNotetakerAndCourseResp = new LoadLectureNoteDescriptionsByNotetakerAndCourseResp();
			List<LectureNoteDescriptionDTO> lectureNoteDescriptions;
			if (list == null)
			{
				lectureNoteDescriptions = null;
			}
			else
			{
				lectureNoteDescriptions = list.ConvertAll<LectureNoteDescriptionDTO>((LectureNoteDescription f) => f.ToDTO());
			}
			loadLectureNoteDescriptionsByNotetakerAndCourseResp.LectureNoteDescriptions = lectureNoteDescriptions;
			return loadLectureNoteDescriptionsByNotetakerAndCourseResp;
		}

		// Token: 0x06000419 RID: 1049 RVA: 0x00013724 File Offset: 0x00011924
		public LoadLectureNoteDescriptionsByStudentAndCourseResp LoadLectureNoteDescriptionsByStudentAndCourse(LoadLectureNoteDescriptionsByStudentAndCourseReq Request)
		{
			INotetakingManager notetakingManager = new NotetakingManager(Request.GetOperationContext());
			List<LectureNoteDescription> list = notetakingManager.LoadLectureNoteDescriptionsByStudentAndCourse(Request.StudentPersonId, Request.StudentLuCourseId);
			LoadLectureNoteDescriptionsByStudentAndCourseResp loadLectureNoteDescriptionsByStudentAndCourseResp = new LoadLectureNoteDescriptionsByStudentAndCourseResp();
			List<LectureNoteDescriptionDTO> lectureNoteDescriptions;
			if (list == null)
			{
				lectureNoteDescriptions = null;
			}
			else
			{
				lectureNoteDescriptions = (from f in list
				select f.ToDTO()).ToList<LectureNoteDescriptionDTO>();
			}
			loadLectureNoteDescriptionsByStudentAndCourseResp.LectureNoteDescriptions = lectureNoteDescriptions;
			return loadLectureNoteDescriptionsByStudentAndCourseResp;
		}

		// Token: 0x0600041A RID: 1050 RVA: 0x00013794 File Offset: 0x00011994
		public LoadLectureNoteByIdResp LoadLectureNoteById(LoadLectureNoteByIdReq Request)
		{
			INotetakingManager notetakingManager = new NotetakingManager(Request.GetOperationContext());
			LectureNote lectureNote = notetakingManager.LoadLectureNoteById(Request.NotetakerDocumentId);
			return new LoadLectureNoteByIdResp
			{
				LectureNote = ((lectureNote != null) ? lectureNote.ToDTO() : null)
			};
		}

		// Token: 0x0600041B RID: 1051 RVA: 0x000137D8 File Offset: 0x000119D8
		public LoadMatchingNotetakersWithLectureNoteUploadsByCourseResp LoadMatchingNotetakersWithLectureNoteUploadsByCourse(LoadMatchingNotetakersWithLectureNoteUploadsByCourseReq Request)
		{
			INotetakingManager notetakingManager = new NotetakingManager(Request.GetOperationContext());
			List<NotetakerBaseWithLookupCourseBase> list = notetakingManager.LoadMatchingNotetakersWithLectureNoteUploadsByCourse(Request.LuCourseId);
			LoadMatchingNotetakersWithLectureNoteUploadsByCourseResp loadMatchingNotetakersWithLectureNoteUploadsByCourseResp = new LoadMatchingNotetakersWithLectureNoteUploadsByCourseResp();
			List<NotetakerBaseWithLookupCourseBaseDTO> notetakers;
			if (list == null)
			{
				notetakers = null;
			}
			else
			{
				notetakers = list.ConvertAll<NotetakerBaseWithLookupCourseBaseDTO>((NotetakerBaseWithLookupCourseBase f) => f.ToDTO());
			}
			loadMatchingNotetakersWithLectureNoteUploadsByCourseResp.Notetakers = notetakers;
			return loadMatchingNotetakersWithLectureNoteUploadsByCourseResp;
		}

		// Token: 0x0600041C RID: 1052 RVA: 0x0001383C File Offset: 0x00011A3C
		public LoadEquivalentCoursesResp LoadEquivalentCourses(LoadEquivalentCoursesReq Request)
		{
			INotetakingManager notetakingManager = new NotetakingManager(Request.GetOperationContext());
			List<LookupCourseBase> list = notetakingManager.LoadEquivalentCourses(Request.LuCourseId);
			LoadEquivalentCoursesResp loadEquivalentCoursesResp = new LoadEquivalentCoursesResp();
			List<LookupCourseBaseDTO> courses;
			if (list == null)
			{
				courses = null;
			}
			else
			{
				courses = list.ConvertAll<LookupCourseBaseDTO>((LookupCourseBase f) => f.ToDTO());
			}
			loadEquivalentCoursesResp.Courses = courses;
			return loadEquivalentCoursesResp;
		}

		// Token: 0x0600041D RID: 1053 RVA: 0x000138A0 File Offset: 0x00011AA0
		public void AddPotentialCoursesForNotetaker(AddPotentialCoursesForNotetakerReq Request)
		{
			INotetakingManager notetakingManager = new NotetakingManager(Request.GetOperationContext());
			IList<DataSyncExternalCourseDTO> externalCourses = Request.ExternalCourses;
			List<DataSyncExternalCourse> list;
			if (externalCourses == null)
			{
				list = null;
			}
			else
			{
				list = externalCourses.ToList<DataSyncExternalCourseDTO>().ConvertAll<DataSyncExternalCourse>((DataSyncExternalCourseDTO g) => g.ToDomainObject());
			}
			List<DataSyncExternalCourse> externalCourses2 = list;
			notetakingManager.AddPotentialCoursesForNotetaker(Request.ServiceProviderId, externalCourses2);
		}

		// Token: 0x0600041E RID: 1054 RVA: 0x00013900 File Offset: 0x00011B00
		public CreateNotetakerAccountResp CreateNotetakerAccount(CreateNotetakerAccountReq Request)
		{
			INotetakingManager notetakingManager = new NotetakingManager(Request.GetOperationContext());
			int serviceProviderId = notetakingManager.CreateNotetakerAccount(Request.Notetaker.ToDomainObject());
			return new CreateNotetakerAccountResp
			{
				ServiceProviderId = serviceProviderId
			};
		}

		// Token: 0x0600041F RID: 1055 RVA: 0x00013940 File Offset: 0x00011B40
		public void RecordStudentDownloadedLectureNote(RecordStudentDownloadedLectureNoteReq Request)
		{
			INotetakingManager notetakingManager = new NotetakingManager(Request.GetOperationContext());
			notetakingManager.RecordStudentDownloadedLectureNote(Request.PersonId, Request.NotetakerDocumentId);
		}

		// Token: 0x06000420 RID: 1056 RVA: 0x00013970 File Offset: 0x00011B70
		public LoadStudentDownloadedLectureNoteHistoryResp LoadStudentDownloadedLectureNoteHistory(LoadStudentDownloadedLectureNoteHistoryReq Request)
		{
			INotetakingManager notetakingManager = new NotetakingManager(Request.GetOperationContext());
			IList<DownloadedLectureNote> list = notetakingManager.LoadStudentDownloadedLectureNoteHistory(Request.PersonId, Request.LuCourseId);
			LoadStudentDownloadedLectureNoteHistoryResp loadStudentDownloadedLectureNoteHistoryResp = new LoadStudentDownloadedLectureNoteHistoryResp();
			IList<DownloadedLectureNoteDTO> downloadedLectureNotes;
			if (list == null)
			{
				downloadedLectureNotes = null;
			}
			else
			{
				downloadedLectureNotes = list.ToList<DownloadedLectureNote>().ConvertAll<DownloadedLectureNoteDTO>((DownloadedLectureNote g) => g.ToDTO());
			}
			loadStudentDownloadedLectureNoteHistoryResp.DownloadedLectureNotes = downloadedLectureNotes;
			return loadStudentDownloadedLectureNoteHistoryResp;
		}

		// Token: 0x06000421 RID: 1057 RVA: 0x000139E0 File Offset: 0x00011BE0
		public LoadStudentDownloadedLectureNoteHistoryLastDateDownloadedForEachLectureNoteResp LoadStudentDownloadedLectureNoteHistoryLastDateDownloadedForEachLectureNote(LoadStudentDownloadedLectureNoteHistoryLastDateDownloadedForEachLectureNoteReq Request)
		{
			INotetakingManager notetakingManager = new NotetakingManager(Request.GetOperationContext());
			IList<DownloadedLectureNote> list = notetakingManager.LoadStudentDownloadedLectureNoteHistoryLastDateDownloadedForEachLectureNote(Request.PersonId, Request.LuCourseId);
			LoadStudentDownloadedLectureNoteHistoryLastDateDownloadedForEachLectureNoteResp loadStudentDownloadedLectureNoteHistoryLastDateDownloadedForEachLectureNoteResp = new LoadStudentDownloadedLectureNoteHistoryLastDateDownloadedForEachLectureNoteResp();
			IList<DownloadedLectureNoteDTO> downloadedLectureNotes;
			if (list == null)
			{
				downloadedLectureNotes = null;
			}
			else
			{
				downloadedLectureNotes = list.ToList<DownloadedLectureNote>().ConvertAll<DownloadedLectureNoteDTO>((DownloadedLectureNote g) => g.ToDTO());
			}
			loadStudentDownloadedLectureNoteHistoryLastDateDownloadedForEachLectureNoteResp.DownloadedLectureNotes = downloadedLectureNotes;
			return loadStudentDownloadedLectureNoteHistoryLastDateDownloadedForEachLectureNoteResp;
		}

		// Token: 0x06000422 RID: 1058 RVA: 0x00013A50 File Offset: 0x00011C50
		public CreateLectureNoteResp CreateLectureNote(CreateLectureNoteReq Request)
		{
			INotetakingManager notetakingManager = new NotetakingManager(Request.GetOperationContext());
			INotetakingManager notetakingManager2 = notetakingManager;
			LectureNoteDTO lectureNote = Request.LectureNote;
			int notetakerDocumentId = notetakingManager2.CreateLectureNote((lectureNote != null) ? lectureNote.ToDomainObject() : null);
			return new CreateLectureNoteResp
			{
				NotetakerDocumentId = notetakerDocumentId
			};
		}

		// Token: 0x06000423 RID: 1059 RVA: 0x00013A94 File Offset: 0x00011C94
		public UpdateLectureNoteResp UpdateLectureNote(UpdateLectureNoteReq Request)
		{
			INotetakingManager notetakingManager = new NotetakingManager(Request.GetOperationContext());
			INotetakingManager notetakingManager2 = notetakingManager;
			LectureNoteDTO lectureNote = Request.LectureNote;
			notetakingManager2.UpdateLectureNote((lectureNote != null) ? lectureNote.ToDomainObject() : null);
			return new UpdateLectureNoteResp();
		}

		// Token: 0x06000424 RID: 1060 RVA: 0x00013AD0 File Offset: 0x00011CD0
		public void DeleteLectureNote(DeleteLectureNoteReq Request)
		{
			INotetakingManager notetakingManager = new NotetakingManager(Request.GetOperationContext());
			notetakingManager.DeleteLectureNote(Request.NotetakerDocumentId);
		}

		// Token: 0x06000425 RID: 1061 RVA: 0x00013AF8 File Offset: 0x00011CF8
		public LoadUniqueAvailableCourseStartDatesByNotetakerResp LoadUniqueAvailableCourseStartDatesByNotetaker(LoadUniqueAvailableCourseStartDatesByNotetakerReq Request)
		{
			INotetakingManager notetakingManager = new NotetakingManager(Request.GetOperationContext());
			return new LoadUniqueAvailableCourseStartDatesByNotetakerResp
			{
				UniqueDates = notetakingManager.LoadUniqueAvailableCourseStartDatesByNotetaker(Request.NotetakerId)
			};
		}

		// Token: 0x06000426 RID: 1062 RVA: 0x00013B30 File Offset: 0x00011D30
		public LoadNotetakerAvailableCoursesResp LoadNotetakerAvailableCourses(LoadNotetakerAvailableCoursesReq Request)
		{
			INotetakingManager notetakingManager = new NotetakingManager(Request.GetOperationContext());
			IList<LookupCourseBase> list = notetakingManager.LoadNotetakerAvailableCourses(Request.NotetakerId, Request.StartDate, Request.EndDate);
			LoadNotetakerAvailableCoursesResp loadNotetakerAvailableCoursesResp = new LoadNotetakerAvailableCoursesResp();
			IList<LookupCourseBaseDTO> courseBases;
			if (list == null)
			{
				courseBases = null;
			}
			else
			{
				courseBases = (from g in list
				select g.ToDTO()).ToList<LookupCourseBaseDTO>();
			}
			loadNotetakerAvailableCoursesResp.CourseBases = courseBases;
			return loadNotetakerAvailableCoursesResp;
		}

		// Token: 0x06000427 RID: 1063 RVA: 0x00013BA4 File Offset: 0x00011DA4
		public LoadUniqueStudentsReceivingNotesResp LoadUniqueStudentsReceivingNotes(LoadUniqueStudentsReceivingNotesReq Request)
		{
			INotetakingManager notetakingManager = new NotetakingManager(Request.GetOperationContext());
			IList<ServiceRequestBase> list = notetakingManager.LoadUniqueStudentsReceivingNotes(Request.NotetakerId, Request.NotetakerLuCourseId);
			LoadUniqueStudentsReceivingNotesResp loadUniqueStudentsReceivingNotesResp = new LoadUniqueStudentsReceivingNotesResp();
			IList<ServiceRequestBaseDTO> assignments;
			if (list == null)
			{
				assignments = null;
			}
			else
			{
				assignments = (from g in list
				select g.ToDTO()).ToList<ServiceRequestBaseDTO>();
			}
			loadUniqueStudentsReceivingNotesResp.Assignments = assignments;
			return loadUniqueStudentsReceivingNotesResp;
		}

		// Token: 0x06000428 RID: 1064 RVA: 0x00013C14 File Offset: 0x00011E14
		public AssignNotetakerResp AssignNotetaker(AssignNotetakerReq Request)
		{
			INotetakingManager notetakingManager = new NotetakingManager(Request.GetOperationContext());
			bool wasThisTheFirstStudentAssignedToThisNotetakerAndCourse = notetakingManager.AssignNotetaker(Request.StudentPersonId, Request.StudentLuCourseId, Request.NotetakerId, Request.NotetakerLuCourseId);
			return new AssignNotetakerResp
			{
				WasThisTheFirstStudentAssignedToThisNotetakerAndCourse = wasThisTheFirstStudentAssignedToThisNotetakerAndCourse
			};
		}

		// Token: 0x06000429 RID: 1065 RVA: 0x00013C60 File Offset: 0x00011E60
		public CancelNotetakerAssignmentResp CancelNotetakerAssignment(CancelNotetakerAssignmentReq Request)
		{
			INotetakingManager notetakingManager = new NotetakingManager(Request.GetOperationContext());
			NotetakerBaseWithLookupCourseBase notetakerBaseWithLookupCourseBase = notetakingManager.CancelNotetakerAssignment(Request.StudentPersonId, Request.StudentLuCourseId, Request.Why);
			return new CancelNotetakerAssignmentResp
			{
				NotetakerAndCourse = ((notetakerBaseWithLookupCourseBase != null) ? notetakerBaseWithLookupCourseBase.ToDTO() : null)
			};
		}
	}
}
