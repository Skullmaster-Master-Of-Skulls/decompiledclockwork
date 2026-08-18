using System;
using System.Collections.Generic;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.AlternativeFormat;

namespace TechnoPro.Common.ICore.AlternativeFormat
{
	// Token: 0x020000F0 RID: 240
	public interface IMediaJobManager : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x0600079F RID: 1951
		int AddMediaJobNote(int mediaJobId, MediaJobRunningNote note);

		// Token: 0x060007A0 RID: 1952
		void UpdateMediaJobNote(MediaJobRunningNote note);

		// Token: 0x060007A1 RID: 1953
		IList<MediaJobRunningNote> GetRunningNotesByMediaJob(int mediaJobId);

		// Token: 0x060007A2 RID: 1954
		MediaJob GetActiveMediaJobById(int mediaJobId);

		// Token: 0x060007A3 RID: 1955
		IList<MediaJob> GetActiveMediaJobByMediaContentAndFormat(Guid mediaContentId, MediaContentFormat mediaContentFormat, int studentPersonId = 0);

		// Token: 0x060007A4 RID: 1956
		IList<MediaJob> GetActiveMediaJobByMediaContentPerFormatId(int mediaContentPerFormatId, int studentId = 0);

		// Token: 0x060007A5 RID: 1957
		int GetCountActiveMediaJobByMediaContentPerFormatId(int mediaContentPerFormatId, int studentId = 0);

		// Token: 0x060007A6 RID: 1958
		int GetCountActiveMediaJobByMediaContentAndFormat(Guid mediaContentId, MediaContentFormat mediaContentFormat, int studentPersonId = 0);

		// Token: 0x060007A7 RID: 1959
		IList<MediaJob> GetActiveMediaJobsByAssignedStaff(int assignedStaffId, int campusId = 0);

		// Token: 0x060007A8 RID: 1960
		IList<MediaJob> GetActiveMediaJobsByExpiredInLessThan(TimeSpan dueDateIn);

		// Token: 0x060007A9 RID: 1961
		IList<MediaJob> GetActiveExpiredMediaJobs();

		// Token: 0x060007AA RID: 1962
		IList<MediaJob> GetActiveJobs(int campusId = 0);

		// Token: 0x060007AB RID: 1963
		IList<MediaJob> GetActiveJobsByStudent(int studentPersonId, int campusId = 0);

		// Token: 0x060007AC RID: 1964
		CompletedMediaJob GetCompletedMediaJobById(int mediaJobId);

		// Token: 0x060007AD RID: 1965
		IList<CompletedMediaJob> GetCompletedMediaJobByMediaContentAndFormat(Guid mediaContentId, MediaContentFormat mediaContentFormat, int studentPersonId = 0);

		// Token: 0x060007AE RID: 1966
		IList<CompletedMediaJob> GetCompletedMediaJobByMediaContentPerFormatId(int mediaContentPerFormatId, int studentPersonId = 0);

		// Token: 0x060007AF RID: 1967
		IList<CompletedMediaJob> GetCompletedMediaJobsByAssignedStaff(int assignedStaffId, int campusId = 0);

		// Token: 0x060007B0 RID: 1968
		IList<CompletedMediaJob> GetCompletedJobsByDateRange(DateTime startDate, DateTime endDate, int campusId = 0);

		// Token: 0x060007B1 RID: 1969
		IList<CompletedMediaJob> GetCompletedJobs(int campusId = 0);

		// Token: 0x060007B2 RID: 1970
		IList<CompletedMediaJob> GetCompletedJobsByStudent(int studentPersonId, int campusId = 0);

		// Token: 0x060007B3 RID: 1971
		IList<CompletedMediaJob> GetCompletedJobsByStudentAndDateRange(int studentPersonId, DateTime startDate, DateTime endDate, int campusId = 0);

		// Token: 0x060007B4 RID: 1972
		IList<CompletedMediaJob> GetCompletedJobsByStaffAndDateRange(int assignedStaffId, DateTime startDate, DateTime endDate, int campusId = 0);

		// Token: 0x060007B5 RID: 1973
		CancelledMediaJob GetCancelledMediaJobById(int mediaJobId);

		// Token: 0x060007B6 RID: 1974
		IList<CancelledMediaJob> GetCancelledJobsByDateRange(DateTime startDate, DateTime endDate, int campusId = 0);

		// Token: 0x060007B7 RID: 1975
		IList<CancelledMediaJob> GetCancelledJobs(int campusId = 0);

		// Token: 0x060007B8 RID: 1976
		IList<CancelledMediaJob> GetCancelledJobsByStudentAndDateRange(int studentPersonId, DateTime startDate, DateTime endDate, int campusId = 0);

		// Token: 0x060007B9 RID: 1977
		IList<CancelledMediaJob> GetCancelledJobsByStaffAndDateRange(int assignedStaffId, DateTime startDate, DateTime endDate, int campusId = 0);

		// Token: 0x060007BA RID: 1978
		int CreateMediaJob(MediaJob mediaJob);

		// Token: 0x060007BB RID: 1979
		void UpdateMediaJob(MediaJob mediaJob);

		// Token: 0x060007BC RID: 1980
		IList<MediaContentRequestedInfo> CancelMediaJob(CancelledMediaJob mediaJob);

		// Token: 0x060007BD RID: 1981
		IList<MediaContentRequestedInfo> MarkMediaJobAsCompleted(CompletedMediaJob mediaJob, DateTime availableStartTime, DateTime availableEndTime);

		// Token: 0x060007BE RID: 1982
		void ChangeMediaJobStatus(int mediaJobId, string changeNotes, ref string generalStatusnName, ref string publisherStatusName, ref string vendorStatusName, ref string inHouseStatusName);
	}
}
