using System;
using System.Collections.Generic;
using TechnoPro.Common.Core.AlternativeFormat.Adapters;
using TechnoPro.Common.DAO.AlternativeFormat;
using TechnoPro.Common.DAO.Impl.AlternativeFormat;
using TechnoPro.Common.ICore.AlternativeFormat;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.AlternativeFormat;
using TechnoPro.Common.Public.Entities.Settings;

namespace TechnoPro.Common.Core.AlternativeFormat
{
	// Token: 0x02000159 RID: 345
	public class MediaJobManager : IMediaJobManager, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x1700021A RID: 538
		// (get) Token: 0x06000F6B RID: 3947 RVA: 0x00072C80 File Offset: 0x00070E80
		// (set) Token: 0x06000F6C RID: 3948 RVA: 0x00072C88 File Offset: 0x00070E88
		private IMediaJobDAO MediaJobDAO { get; set; }

		// Token: 0x06000F6D RID: 3949 RVA: 0x00072C91 File Offset: 0x00070E91
		public MediaJobManager(OperationContext opContext)
		{
			this.OpContext = opContext;
			this.MediaJobDAO = new MediaJobDAO(opContext);
		}

		// Token: 0x1700021B RID: 539
		// (get) Token: 0x06000F6E RID: 3950 RVA: 0x00072CB0 File Offset: 0x00070EB0
		// (set) Token: 0x06000F6F RID: 3951 RVA: 0x00072CB8 File Offset: 0x00070EB8
		public OperationContext OpContext { get; set; }

		// Token: 0x06000F70 RID: 3952 RVA: 0x00072CC4 File Offset: 0x00070EC4
		public IList<CompletedMediaJob> GetCompletedJobsByStaffAndDateRange(int assignedStaffId, DateTime startDate, DateTime endDate, int campusId = 0)
		{
			return (campusId > 0) ? this.MediaJobDAO.GetCompletedJobsByStaffAndDateRange(assignedStaffId, startDate, endDate, campusId) : this.MediaJobDAO.GetCompletedJobsByStaffAndDateRange(assignedStaffId, startDate, endDate);
		}

		// Token: 0x06000F71 RID: 3953 RVA: 0x00072CFC File Offset: 0x00070EFC
		public IList<CancelledMediaJob> GetCancelledJobsByStaffAndDateRange(int assignedStaffId, DateTime startDate, DateTime endDate, int campusId = 0)
		{
			return (campusId > 0) ? this.MediaJobDAO.GetCancelledJobsByStaffAndDateRange(assignedStaffId, startDate, endDate, campusId) : this.MediaJobDAO.GetCancelledJobsByStaffAndDateRange(assignedStaffId, startDate, endDate);
		}

		// Token: 0x06000F72 RID: 3954 RVA: 0x00072D34 File Offset: 0x00070F34
		public int AddMediaJobNote(int mediaJobId, MediaJobRunningNote note)
		{
			return this.MediaJobDAO.AddMediaJobNote(mediaJobId, note);
		}

		// Token: 0x06000F73 RID: 3955 RVA: 0x00072D53 File Offset: 0x00070F53
		public void UpdateMediaJobNote(MediaJobRunningNote noteId)
		{
			this.MediaJobDAO.UpdateMediaJobNote(noteId);
		}

		// Token: 0x06000F74 RID: 3956 RVA: 0x00072D64 File Offset: 0x00070F64
		public IList<MediaJobRunningNote> GetRunningNotesByMediaJob(int mediaJobId)
		{
			return this.MediaJobDAO.GetRunningNotesByMediaJob(mediaJobId);
		}

		// Token: 0x06000F75 RID: 3957 RVA: 0x00072D84 File Offset: 0x00070F84
		public MediaJob GetActiveMediaJobById(int mediaJobId)
		{
			return this.MediaJobDAO.GetActiveMediaJobById(mediaJobId);
		}

		// Token: 0x06000F76 RID: 3958 RVA: 0x00072DA4 File Offset: 0x00070FA4
		public IList<MediaJob> GetActiveMediaJobByMediaContentAndFormat(Guid mediaContentId, MediaContentFormat mediaContentFormat, int studentPersonId = 0)
		{
			return this.MediaJobDAO.GetActiveMediaJobByMediaContentAndFormat(mediaContentId, mediaContentFormat, studentPersonId);
		}

		// Token: 0x06000F77 RID: 3959 RVA: 0x00072DC4 File Offset: 0x00070FC4
		public IList<MediaJob> GetActiveMediaJobByMediaContentPerFormatId(int mediaContentPerFormatId, int studentId = 0)
		{
			return this.MediaJobDAO.GetActiveMediaJobByMediaContentPerFormatId(mediaContentPerFormatId, studentId);
		}

		// Token: 0x06000F78 RID: 3960 RVA: 0x00072DE4 File Offset: 0x00070FE4
		public int GetCountActiveMediaJobByMediaContentPerFormatId(int mediaContentPerFormatId, int studentId = 0)
		{
			return this.MediaJobDAO.GetCountActiveMediaJobByMediaContentPerFormatId(mediaContentPerFormatId, studentId);
		}

		// Token: 0x06000F79 RID: 3961 RVA: 0x00072E04 File Offset: 0x00071004
		public int GetCountActiveMediaJobByMediaContentAndFormat(Guid mediaContentId, MediaContentFormat mediaContentFormat, int studentPersonId = 0)
		{
			return this.MediaJobDAO.GetCountActiveMediaJobByMediaContentAndFormat(mediaContentId, mediaContentFormat, studentPersonId);
		}

		// Token: 0x06000F7A RID: 3962 RVA: 0x00072E24 File Offset: 0x00071024
		public IList<MediaJob> GetActiveMediaJobsByAssignedStaff(int assignedStaffId, int campusId = 0)
		{
			return (campusId > 0) ? this.MediaJobDAO.GetActiveMediaJobsByAssignedStaff(assignedStaffId, campusId) : this.MediaJobDAO.GetActiveMediaJobsByAssignedStaff(assignedStaffId);
		}

		// Token: 0x06000F7B RID: 3963 RVA: 0x00072E58 File Offset: 0x00071058
		public IList<MediaJob> GetActiveMediaJobsByExpiredInLessThan(TimeSpan dueDateIn)
		{
			return this.MediaJobDAO.GetActiveMediaJobsByExpiredInLessThan(dueDateIn);
		}

		// Token: 0x06000F7C RID: 3964 RVA: 0x00072E78 File Offset: 0x00071078
		public IList<MediaJob> GetActiveExpiredMediaJobs()
		{
			return this.MediaJobDAO.GetActiveExpiredMediaJobs();
		}

		// Token: 0x06000F7D RID: 3965 RVA: 0x00072E98 File Offset: 0x00071098
		public IList<MediaJob> GetActiveJobs(int campusId = 0)
		{
			return (campusId > 0) ? this.MediaJobDAO.GetActiveJobs(campusId) : this.MediaJobDAO.GetActiveJobs();
		}

		// Token: 0x06000F7E RID: 3966 RVA: 0x00072EC8 File Offset: 0x000710C8
		public IList<MediaJob> GetActiveJobsByStudent(int studentPersonId, int campusId = 0)
		{
			return (campusId > 0) ? this.MediaJobDAO.GetActiveJobsByStudent(studentPersonId, campusId) : this.MediaJobDAO.GetActiveJobsByStudent(studentPersonId);
		}

		// Token: 0x06000F7F RID: 3967 RVA: 0x00072EFC File Offset: 0x000710FC
		public CompletedMediaJob GetCompletedMediaJobById(int mediaJobId)
		{
			return this.MediaJobDAO.GetCompletedMediaJobById(mediaJobId);
		}

		// Token: 0x06000F80 RID: 3968 RVA: 0x00072F1C File Offset: 0x0007111C
		public CancelledMediaJob GetCancelledMediaJobById(int mediaJobId)
		{
			return this.MediaJobDAO.GetCancelledMediaJobById(mediaJobId);
		}

		// Token: 0x06000F81 RID: 3969 RVA: 0x00072F3C File Offset: 0x0007113C
		public IList<CompletedMediaJob> GetCompletedMediaJobByMediaContentAndFormat(Guid mediaContentId, MediaContentFormat mediaContentFormat, int studentPersonId = 0)
		{
			return this.MediaJobDAO.GetCompletedMediaJobByMediaContentAndFormat(mediaContentId, mediaContentFormat, 0);
		}

		// Token: 0x06000F82 RID: 3970 RVA: 0x00072F5C File Offset: 0x0007115C
		public IList<CompletedMediaJob> GetCompletedMediaJobByMediaContentPerFormatId(int mediaContentPerFormatId, int studentPersonId = 0)
		{
			return this.MediaJobDAO.GetCompletedMediaJobByMediaContentPerFormatId(mediaContentPerFormatId, studentPersonId);
		}

		// Token: 0x06000F83 RID: 3971 RVA: 0x00072F7C File Offset: 0x0007117C
		public IList<CompletedMediaJob> GetCompletedMediaJobsByAssignedStaff(int assignedStaffId, int campusId = 0)
		{
			return (campusId > 0) ? this.MediaJobDAO.GetCompletedMediaJobsByAssignedStaff(assignedStaffId, campusId) : this.MediaJobDAO.GetCompletedMediaJobsByAssignedStaff(assignedStaffId);
		}

		// Token: 0x06000F84 RID: 3972 RVA: 0x00072FB0 File Offset: 0x000711B0
		public IList<CompletedMediaJob> GetCompletedJobsByDateRange(DateTime startDate, DateTime endDate, int campusId = 0)
		{
			return (campusId > 0) ? this.MediaJobDAO.GetCompletedJobsByDateRange(startDate, endDate, campusId) : this.MediaJobDAO.GetCompletedJobsByDateRange(startDate, endDate);
		}

		// Token: 0x06000F85 RID: 3973 RVA: 0x00072FE4 File Offset: 0x000711E4
		public IList<CancelledMediaJob> GetCancelledJobsByDateRange(DateTime startDate, DateTime endDate, int campusId = 0)
		{
			return (campusId > 0) ? this.MediaJobDAO.GetCancelledJobsByDateRange(startDate, endDate, campusId) : this.MediaJobDAO.GetCancelledJobsByDateRange(startDate, endDate);
		}

		// Token: 0x06000F86 RID: 3974 RVA: 0x00073018 File Offset: 0x00071218
		public IList<CompletedMediaJob> GetCompletedJobs(int campusId = 0)
		{
			return (campusId > 0) ? this.MediaJobDAO.GetCompletedJobs(campusId) : this.MediaJobDAO.GetCompletedJobs();
		}

		// Token: 0x06000F87 RID: 3975 RVA: 0x00073048 File Offset: 0x00071248
		public IList<CancelledMediaJob> GetCancelledJobs(int campusId = 0)
		{
			return (campusId > 0) ? this.MediaJobDAO.GetCancelledJobs(campusId) : this.MediaJobDAO.GetCancelledJobs();
		}

		// Token: 0x06000F88 RID: 3976 RVA: 0x00073078 File Offset: 0x00071278
		public IList<CompletedMediaJob> GetCompletedJobsByStudent(int studentPersonId, int campusId = 0)
		{
			return (campusId > 0) ? this.MediaJobDAO.GetCompletedJobsByStudent(studentPersonId, campusId) : this.MediaJobDAO.GetCompletedJobsByStudent(studentPersonId);
		}

		// Token: 0x06000F89 RID: 3977 RVA: 0x000730AC File Offset: 0x000712AC
		public IList<CompletedMediaJob> GetCompletedJobsByStudentAndDateRange(int studentPersonId, DateTime startDate, DateTime endDate, int campusId = 0)
		{
			return (campusId > 0) ? this.MediaJobDAO.GetCompletedJobsByStudentAndDateRange(studentPersonId, startDate, endDate, campusId) : this.MediaJobDAO.GetCompletedJobsByStudentAndDateRange(studentPersonId, startDate, endDate);
		}

		// Token: 0x06000F8A RID: 3978 RVA: 0x000730E4 File Offset: 0x000712E4
		public IList<CancelledMediaJob> GetCancelledJobsByStudentAndDateRange(int studentPersonId, DateTime startDate, DateTime endDate, int campusId = 0)
		{
			return (campusId > 0) ? this.MediaJobDAO.GetCancelledJobsByStudentAndDateRange(studentPersonId, startDate, endDate, campusId) : this.MediaJobDAO.GetCancelledJobsByStudentAndDateRange(studentPersonId, startDate, endDate);
		}

		// Token: 0x06000F8B RID: 3979 RVA: 0x0007311C File Offset: 0x0007131C
		public int CreateMediaJob(MediaJob mediaJob)
		{
			mediaJob.JobCurrentStatusNameGeneral = "Created";
			return this.MediaJobDAO.CreateMediaJob(mediaJob);
		}

		// Token: 0x06000F8C RID: 3980 RVA: 0x00073146 File Offset: 0x00071346
		public void UpdateMediaJob(MediaJob mediaJob)
		{
			this.MediaJobDAO.UpdateMediaJob(mediaJob);
		}

		// Token: 0x06000F8D RID: 3981 RVA: 0x00073158 File Offset: 0x00071358
		public IList<MediaContentRequestedInfo> CancelMediaJob(CancelledMediaJob mediaJob)
		{
			StudentMediaRequestDAO studentMediaRequestDAO = new StudentMediaRequestDAO(this.OpContext);
			this.MediaJobDAO.CancelMediaJob(mediaJob.MediaJobId, mediaJob.CancellationReason);
			IList<MediaContentRequestedInfo> list = studentMediaRequestDAO.LoadAllMediaRequestInfoByJobId(mediaJob.MediaJobId);
			List<MediaContentRequestedInfo> list2 = new List<MediaContentRequestedInfo>();
			foreach (MediaContentRequestedInfo mediaContentRequestedInfo in list)
			{
				mediaContentRequestedInfo.ContentDetailRequested.MediaContentPerFormatId = mediaJob.MediaContentPerFormatId;
				mediaContentRequestedInfo.IsCancelled = true;
				mediaContentRequestedInfo.CompletedDateTime = new DateTime?(DateTime.Now);
				mediaContentRequestedInfo.RequestStatus = MediaRequestStatus.Rejected_by_Staff;
				studentMediaRequestDAO.DeleteStudentContentMediaRequestInfo(mediaContentRequestedInfo, MediaRequestStatus.Rejected_by_Staff);
				MediaContentRequestedInfo mediaContentRequestedInfo2 = studentMediaRequestDAO.LoadArchiveMediaContentRequestInfoById(mediaContentRequestedInfo.MediaContentRequestedInfoID);
				bool flag = mediaContentRequestedInfo2 != null;
				if (flag)
				{
					list2.Add(mediaContentRequestedInfo2);
					mediaContentRequestedInfo2.NotifyStudentsAsync(Setting.ALTERNATEFORMAT_Email_CancelledStudentRequestNotification, this.OpContext);
				}
			}
			return list2;
		}

		// Token: 0x06000F8E RID: 3982 RVA: 0x0007325C File Offset: 0x0007145C
		public IList<MediaContentRequestedInfo> MarkMediaJobAsCompleted(CompletedMediaJob mediaJob, DateTime availableStartTime, DateTime availableEndTime)
		{
			StudentMediaRequestManager studentMediaRequestManager = new StudentMediaRequestManager(this.OpContext);
			this.MediaJobDAO.MarkMediaJobAsCompleted(mediaJob.MediaJobId, mediaJob.CompletedNotes);
			IList<MediaContentRequestedInfo> list = studentMediaRequestManager.LoadAllMediaRequestInfoByJobId(mediaJob.MediaJobId);
			List<MediaContentRequestedInfo> list2 = new List<MediaContentRequestedInfo>();
			foreach (MediaContentRequestedInfo mediaContentRequestedInfo in list)
			{
				bool flag = !mediaContentRequestedInfo.ContentDetailRequested.MediaContent.ProofOfPurchaseRequired;
				MediaRequestStatus status;
				if (flag)
				{
					status = MediaRequestStatus.Ready_To_Download;
				}
				else
				{
					bool flag2 = mediaContentRequestedInfo.ProofOfPurchaseId == 0;
					if (flag2)
					{
						status = MediaRequestStatus.Completed_but_Pending_of_Proof_of_Purchase;
					}
					else
					{
						bool flag3 = mediaContentRequestedInfo.ProofOfPurchase.WhoAcceptedProofOfPurchase == null;
						if (flag3)
						{
							status = MediaRequestStatus.Completed_but_Pending_of_Proof_of_Purchase_Acceptance;
						}
						else
						{
							status = MediaRequestStatus.Ready_To_Download;
						}
					}
				}
				MediaContentRequestedInfo mediaContentRequestedInfo2 = studentMediaRequestManager.MarkMediaContentRequestedAsCompleted(mediaContentRequestedInfo.MediaContentRequestedInfoID, status, availableStartTime, availableEndTime, mediaJob.MediaContentPerFormatId);
				bool flag4 = mediaContentRequestedInfo2 != null;
				if (flag4)
				{
					list2.Add(mediaContentRequestedInfo2);
				}
			}
			return list2;
		}

		// Token: 0x06000F8F RID: 3983 RVA: 0x00073368 File Offset: 0x00071568
		public void ChangeMediaJobStatus(int mediaJobId, string changeNotes, ref string generalStatusName, ref string publisherStatusName, ref string vendorStatusName, ref string inHouseStatusName)
		{
			bool flag = generalStatusName.Equals("created", StringComparison.OrdinalIgnoreCase) && (!string.IsNullOrEmpty(publisherStatusName) || !string.IsNullOrEmpty(vendorStatusName) || !string.IsNullOrEmpty(inHouseStatusName));
			if (flag)
			{
				generalStatusName = string.Empty;
			}
			this.MediaJobDAO.ChangeMediaJobStatus(mediaJobId, changeNotes, generalStatusName, publisherStatusName, vendorStatusName, inHouseStatusName);
		}
	}
}
