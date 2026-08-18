using System;
using System.Collections.Generic;
using TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities.AlternativeFormat;

namespace TechnoPro.Common.ClientManager.ICore.AlternateFormat
{
	// Token: 0x0200009B RID: 155
	public interface IMediaJobClientManager : IWebService
	{
		// Token: 0x060004DC RID: 1244
		IList<MediaJobDTO> SplitJobIntoChapters(MediaJobDTO job, params string[] chapterTitles);

		// Token: 0x060004DD RID: 1245
		int AddMediaJobNote(int mediaJobId, MediaJobRunningNoteDTO note);

		// Token: 0x060004DE RID: 1246
		void UpdateMediaJobNote(MediaJobRunningNoteDTO note);

		// Token: 0x060004DF RID: 1247
		IList<MediaJobRunningNoteDTO> GetRunningNotesByMediaJob(int mediaJobId);

		// Token: 0x060004E0 RID: 1248
		MediaJobDTO GetActiveMediaJobById(int mediaJobId);

		// Token: 0x060004E1 RID: 1249
		IList<MediaJobDTO> GetActiveMediaJobByMediaContentAndFormat(Guid mediaContentId, MediaContentFormat mediaContentFormat, int studentPersonId = 0);

		// Token: 0x060004E2 RID: 1250
		IList<MediaJobDTO> GetActiveMediaJobsByAssignedStaff(int assignedStaffId, int campusId = 0);

		// Token: 0x060004E3 RID: 1251
		IList<MediaJobDTO> GetActiveMediaJobsByExpiredInLessThan(TimeSpan dueDateIn);

		// Token: 0x060004E4 RID: 1252
		IList<MediaJobDTO> GetActiveExpiredMediaJobs();

		// Token: 0x060004E5 RID: 1253
		IList<MediaJobDTO> GetActiveJobs(int campusId = 0);

		// Token: 0x060004E6 RID: 1254
		IList<MediaJobDTO> GetActiveJobsByStudent(int studentPersonId, int campusId = 0);

		// Token: 0x060004E7 RID: 1255
		CompletedMediaJobDTO GetCompletedMediaJobById(int mediaJobId);

		// Token: 0x060004E8 RID: 1256
		IList<CompletedMediaJobDTO> GetCompletedMediaJobByMediaContentAndFormat(Guid mediaContentId, MediaContentFormat mediaContentFormat);

		// Token: 0x060004E9 RID: 1257
		IList<CompletedMediaJobDTO> GetCompletedMediaJobsByAssignedStaff(int assignedStaffId, int campusId = 0);

		// Token: 0x060004EA RID: 1258
		IList<CompletedMediaJobDTO> GetCompletedJobsByDateRange(DateTime startDate, DateTime endDate, int campusId = 0);

		// Token: 0x060004EB RID: 1259
		IList<CompletedMediaJobDTO> GetCompletedJobs(int campusId = 0);

		// Token: 0x060004EC RID: 1260
		IList<CompletedMediaJobDTO> GetCompletedJobsByStudent(int studentPersonId, int campusId = 0);

		// Token: 0x060004ED RID: 1261
		IList<CompletedMediaJobDTO> GetCompletedJobsByStudentAndDateRange(int studentPersonId, DateTime startDate, DateTime endDate, int campusId = 0);

		// Token: 0x060004EE RID: 1262
		IList<CompletedMediaJobDTO> GetCompletedJobsByStaffAndDateRange(int assignedStaffId, DateTime startDate, DateTime endDate, int campusId = 0);

		// Token: 0x060004EF RID: 1263
		CancelledMediaJobDTO GetCancelledMediaJobById(int mediaJobId);

		// Token: 0x060004F0 RID: 1264
		IList<CancelledMediaJobDTO> GetCancelledJobsByDateRange(DateTime startDate, DateTime endDate, int campusId = 0);

		// Token: 0x060004F1 RID: 1265
		IList<CancelledMediaJobDTO> GetCancelledJobs(int campusId = 0);

		// Token: 0x060004F2 RID: 1266
		IList<CancelledMediaJobDTO> GetCancelledJobsByStudentAndDateRange(int studentPersonId, DateTime startDate, DateTime endDate, int campusId = 0);

		// Token: 0x060004F3 RID: 1267
		IList<CancelledMediaJobDTO> GetCancelledJobsByStaffAndDateRange(int assignedStaffId, DateTime startDate, DateTime endDate, int campusId = 0);

		// Token: 0x060004F4 RID: 1268
		int CreateMediaJob(MediaJobDTO mediaJob);

		// Token: 0x060004F5 RID: 1269
		void UpdateMediaJob(MediaJobDTO mediaJob);

		// Token: 0x060004F6 RID: 1270
		IList<MediaContentRequestedInfoDTO> CancelMediaJob(MediaJobDTO mediaJob, string changeNotes);

		// Token: 0x060004F7 RID: 1271
		IList<MediaContentRequestedInfoDTO> MarkMediaJobAsCompleted(MediaJobDTO mediaJob, string changeNotes, DateTime availableStartTime, DateTime availableEndTime);

		// Token: 0x060004F8 RID: 1272
		void ChangeMediaJobStatus(int mediaJobId, string changeNotes, ref string generalStatusnName, ref string publisherStatusName, ref string vendorStatusName, ref string inHouseStatusName);
	}
}
