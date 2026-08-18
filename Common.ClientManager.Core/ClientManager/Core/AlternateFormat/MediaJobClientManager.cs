using System;
using System.Collections.Generic;
using TechnoPro.ClockWorkServer.Client.ServiceFactory;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat;
using TechnoPro.Common.ClientManager.ICore;
using TechnoPro.Common.ClientManager.ICore.AlternateFormat;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities.AlternativeFormat;
using TechnoPro.Common.Unity.IoC;

namespace TechnoPro.Common.ClientManager.Core.AlternateFormat
{
	// Token: 0x0200009F RID: 159
	public class MediaJobClientManager : IMediaJobClientManager, IWebService
	{
		// Token: 0x060005F7 RID: 1527 RVA: 0x0001A65C File Offset: 0x0001885C
		public int AddMediaJobNote(int mediaJobId, MediaJobRunningNoteDTO note)
		{
			AddMediaJobNoteReq addMediaJobNoteReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<AddMediaJobNoteReq>();
			addMediaJobNoteReq.MediaJobId = mediaJobId;
			addMediaJobNoteReq.Note = note;
			return ClientServiceFactory.GetClientInstance<IMediaJob>().AddMediaJobNote(addMediaJobNoteReq).MediaJobNoteId;
		}

		// Token: 0x060005F8 RID: 1528 RVA: 0x0001A69C File Offset: 0x0001889C
		public void UpdateMediaJobNote(MediaJobRunningNoteDTO note)
		{
			UpdateMediaJobNoteReq updateMediaJobNoteReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<UpdateMediaJobNoteReq>();
			updateMediaJobNoteReq.Note = note;
			ClientServiceFactory.GetClientInstance<IMediaJob>().UpdateMediaJobNote(updateMediaJobNoteReq);
		}

		// Token: 0x060005F9 RID: 1529 RVA: 0x0001A6CC File Offset: 0x000188CC
		public IList<MediaJobRunningNoteDTO> GetRunningNotesByMediaJob(int mediaJobId)
		{
			GetRunningNotesByMediaJobReq getRunningNotesByMediaJobReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<GetRunningNotesByMediaJobReq>();
			getRunningNotesByMediaJobReq.MediaJobId = mediaJobId;
			return ClientServiceFactory.GetClientInstance<IMediaJob>().GetRunningNotesByMediaJob(getRunningNotesByMediaJobReq).RunningNoteList;
		}

		// Token: 0x060005FA RID: 1530 RVA: 0x0001A704 File Offset: 0x00018904
		public MediaJobDTO GetActiveMediaJobById(int mediaJobId)
		{
			GetActiveMediaJobByIdReq getActiveMediaJobByIdReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<GetActiveMediaJobByIdReq>();
			getActiveMediaJobByIdReq.MediaJobId = mediaJobId;
			return ClientServiceFactory.GetClientInstance<IMediaJob>().GetActiveMediaJobById(getActiveMediaJobByIdReq).MediaJob;
		}

		// Token: 0x060005FB RID: 1531 RVA: 0x0001A73C File Offset: 0x0001893C
		public IList<MediaJobDTO> GetActiveMediaJobByMediaContentAndFormat(Guid mediaContentId, MediaContentFormat mediaContentFormat, int studentPersonId = 0)
		{
			GetActiveMediaJobByMediaContentAndFormatReq getActiveMediaJobByMediaContentAndFormatReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<GetActiveMediaJobByMediaContentAndFormatReq>();
			getActiveMediaJobByMediaContentAndFormatReq.MediaContentId = mediaContentId.ToString();
			getActiveMediaJobByMediaContentAndFormatReq.ContentFormat = mediaContentFormat;
			getActiveMediaJobByMediaContentAndFormatReq.StudentPersonId = studentPersonId;
			return ClientServiceFactory.GetClientInstance<IMediaJob>().GetActiveMediaJobByMediaContentAndFormat(getActiveMediaJobByMediaContentAndFormatReq).MediaJobs;
		}

		// Token: 0x060005FC RID: 1532 RVA: 0x0001A790 File Offset: 0x00018990
		public IList<MediaJobDTO> GetActiveMediaJobsByAssignedStaff(int assignedStaffId, int campusId = 0)
		{
			GetActiveMediaJobsByAssignedStaffReq getActiveMediaJobsByAssignedStaffReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<GetActiveMediaJobsByAssignedStaffReq>();
			getActiveMediaJobsByAssignedStaffReq.AssignedStaffId = assignedStaffId;
			getActiveMediaJobsByAssignedStaffReq.CampusId = campusId;
			return ClientServiceFactory.GetClientInstance<IMediaJob>().GetActiveMediaJobsByAssignedStaff(getActiveMediaJobsByAssignedStaffReq).MediaJobList;
		}

		// Token: 0x060005FD RID: 1533 RVA: 0x0001A7D0 File Offset: 0x000189D0
		public IList<MediaJobDTO> GetActiveMediaJobsByExpiredInLessThan(TimeSpan dueDateIn)
		{
			GetActiveMediaJobsByExpiredInLessThanReq getActiveMediaJobsByExpiredInLessThanReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<GetActiveMediaJobsByExpiredInLessThanReq>();
			getActiveMediaJobsByExpiredInLessThanReq.DueDateIn = dueDateIn;
			return ClientServiceFactory.GetClientInstance<IMediaJob>().GetActiveMediaJobsByExpiredInLessThan(getActiveMediaJobsByExpiredInLessThanReq).MediaJobList;
		}

		// Token: 0x060005FE RID: 1534 RVA: 0x0001A808 File Offset: 0x00018A08
		public IList<MediaJobDTO> GetActiveExpiredMediaJobs()
		{
			GetActiveExpiredMediaJobsReq request = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<GetActiveExpiredMediaJobsReq>();
			return ClientServiceFactory.GetClientInstance<IMediaJob>().GetActiveExpiredMediaJobs(request).MediaJobList;
		}

		// Token: 0x060005FF RID: 1535 RVA: 0x0001A838 File Offset: 0x00018A38
		public IList<MediaJobDTO> GetActiveJobs(int campusId = 0)
		{
			GetActiveJobsReq getActiveJobsReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<GetActiveJobsReq>();
			getActiveJobsReq.CampusId = campusId;
			return ClientServiceFactory.GetClientInstance<IMediaJob>().GetActiveJobs(getActiveJobsReq).MediaJobList;
		}

		// Token: 0x06000600 RID: 1536 RVA: 0x0001A870 File Offset: 0x00018A70
		public IList<MediaJobDTO> GetActiveJobsByStudent(int studentPersonId, int campusId = 0)
		{
			GetActiveJobsByStudentReq getActiveJobsByStudentReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<GetActiveJobsByStudentReq>();
			getActiveJobsByStudentReq.StudentPersonId = studentPersonId;
			getActiveJobsByStudentReq.CampusId = campusId;
			return ClientServiceFactory.GetClientInstance<IMediaJob>().GetActiveJobsByStudent(getActiveJobsByStudentReq).MediaJobList;
		}

		// Token: 0x06000601 RID: 1537 RVA: 0x0001A8B0 File Offset: 0x00018AB0
		public CompletedMediaJobDTO GetCompletedMediaJobById(int mediaJobId)
		{
			GetCompletedMediaJobByIdReq getCompletedMediaJobByIdReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<GetCompletedMediaJobByIdReq>();
			getCompletedMediaJobByIdReq.MediaJobId = mediaJobId;
			return ClientServiceFactory.GetClientInstance<IMediaJob>().GetCompletedMediaJobById(getCompletedMediaJobByIdReq).MediaJob;
		}

		// Token: 0x06000602 RID: 1538 RVA: 0x0001A8E8 File Offset: 0x00018AE8
		public CancelledMediaJobDTO GetCancelledMediaJobById(int mediaJobId)
		{
			GetCancelledMediaJobByIdReq getCancelledMediaJobByIdReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<GetCancelledMediaJobByIdReq>();
			getCancelledMediaJobByIdReq.MediaJobId = mediaJobId;
			return ClientServiceFactory.GetClientInstance<IMediaJob>().GetCancelledMediaJobById(getCancelledMediaJobByIdReq).MediaJob;
		}

		// Token: 0x06000603 RID: 1539 RVA: 0x0001A920 File Offset: 0x00018B20
		public IList<CompletedMediaJobDTO> GetCompletedMediaJobByMediaContentAndFormat(Guid mediaContentId, MediaContentFormat mediaContentFormat)
		{
			GetCompletedMediaJobByMediaContentAndFormatReq getCompletedMediaJobByMediaContentAndFormatReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<GetCompletedMediaJobByMediaContentAndFormatReq>();
			getCompletedMediaJobByMediaContentAndFormatReq.MediaContentId = mediaContentId.ToString();
			getCompletedMediaJobByMediaContentAndFormatReq.ContentFormat = mediaContentFormat;
			return ClientServiceFactory.GetClientInstance<IMediaJob>().GetCompletedMediaJobByMediaContentAndFormat(getCompletedMediaJobByMediaContentAndFormatReq).MediaJobs;
		}

		// Token: 0x06000604 RID: 1540 RVA: 0x0001A96C File Offset: 0x00018B6C
		public IList<CompletedMediaJobDTO> GetCompletedMediaJobsByAssignedStaff(int assignedStaffId, int campusId = 0)
		{
			GetCompletedMediaJobsByAssignedStaffReq getCompletedMediaJobsByAssignedStaffReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<GetCompletedMediaJobsByAssignedStaffReq>();
			getCompletedMediaJobsByAssignedStaffReq.AssignedStaffId = assignedStaffId;
			getCompletedMediaJobsByAssignedStaffReq.CampusId = campusId;
			return ClientServiceFactory.GetClientInstance<IMediaJob>().GetCompletedMediaJobsByAssignedStaff(getCompletedMediaJobsByAssignedStaffReq).MediaJobList;
		}

		// Token: 0x06000605 RID: 1541 RVA: 0x0001A9AC File Offset: 0x00018BAC
		public IList<CompletedMediaJobDTO> GetCompletedJobsByDateRange(DateTime startDate, DateTime endDate, int campusId = 0)
		{
			GetCompletedJobsByDateRangeReq getCompletedJobsByDateRangeReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<GetCompletedJobsByDateRangeReq>();
			getCompletedJobsByDateRangeReq.StartDate = startDate;
			getCompletedJobsByDateRangeReq.EndDate = endDate;
			getCompletedJobsByDateRangeReq.CampusId = campusId;
			return ClientServiceFactory.GetClientInstance<IMediaJob>().GetCompletedJobsByDateRange(getCompletedJobsByDateRangeReq).MediaJobList;
		}

		// Token: 0x06000606 RID: 1542 RVA: 0x0001A9F4 File Offset: 0x00018BF4
		public IList<CancelledMediaJobDTO> GetCancelledJobsByDateRange(DateTime startDate, DateTime endDate, int campusId = 0)
		{
			GetCancelledJobsByDateRangeReq getCancelledJobsByDateRangeReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<GetCancelledJobsByDateRangeReq>();
			getCancelledJobsByDateRangeReq.StartDate = startDate;
			getCancelledJobsByDateRangeReq.EndDate = endDate;
			getCancelledJobsByDateRangeReq.CampusId = campusId;
			return ClientServiceFactory.GetClientInstance<IMediaJob>().GetCancelledJobsByDateRange(getCancelledJobsByDateRangeReq).MediaJobList;
		}

		// Token: 0x06000607 RID: 1543 RVA: 0x0001AA3C File Offset: 0x00018C3C
		public IList<CompletedMediaJobDTO> GetCompletedJobs(int campusId = 0)
		{
			GetCompletedJobsReq getCompletedJobsReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<GetCompletedJobsReq>();
			getCompletedJobsReq.CampusId = campusId;
			return ClientServiceFactory.GetClientInstance<IMediaJob>().GetCompletedJobs(getCompletedJobsReq).MediaJobList;
		}

		// Token: 0x06000608 RID: 1544 RVA: 0x0001AA74 File Offset: 0x00018C74
		public IList<CancelledMediaJobDTO> GetCancelledJobs(int campusId = 0)
		{
			GetCancelledJobsReq getCancelledJobsReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<GetCancelledJobsReq>();
			getCancelledJobsReq.CampusId = campusId;
			return ClientServiceFactory.GetClientInstance<IMediaJob>().GetCancelledJobs(getCancelledJobsReq).MediaJobList;
		}

		// Token: 0x06000609 RID: 1545 RVA: 0x0001AAAC File Offset: 0x00018CAC
		public IList<CompletedMediaJobDTO> GetCompletedJobsByStudent(int studentPersonId, int campusId = 0)
		{
			GetCompletedJobsByStudentReq getCompletedJobsByStudentReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<GetCompletedJobsByStudentReq>();
			getCompletedJobsByStudentReq.StudentPersonId = studentPersonId;
			getCompletedJobsByStudentReq.CampusId = campusId;
			return ClientServiceFactory.GetClientInstance<IMediaJob>().GetCompletedJobsByStudent(getCompletedJobsByStudentReq).MediaJobList;
		}

		// Token: 0x0600060A RID: 1546 RVA: 0x0001AAEC File Offset: 0x00018CEC
		public IList<CompletedMediaJobDTO> GetCompletedJobsByStudentAndDateRange(int studentPersonId, DateTime startDate, DateTime endDate, int campusId = 0)
		{
			GetCompletedJobsByStudentAndDateRangeReq getCompletedJobsByStudentAndDateRangeReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<GetCompletedJobsByStudentAndDateRangeReq>();
			getCompletedJobsByStudentAndDateRangeReq.StudentPersonId = studentPersonId;
			getCompletedJobsByStudentAndDateRangeReq.StartDate = startDate;
			getCompletedJobsByStudentAndDateRangeReq.EndDate = endDate;
			getCompletedJobsByStudentAndDateRangeReq.CampusId = campusId;
			return ClientServiceFactory.GetClientInstance<IMediaJob>().GetCompletedJobsByStudentAndDateRange(getCompletedJobsByStudentAndDateRangeReq).MediaJobList;
		}

		// Token: 0x0600060B RID: 1547 RVA: 0x0001AB3C File Offset: 0x00018D3C
		public IList<CancelledMediaJobDTO> GetCancelledJobsByStudentAndDateRange(int studentPersonId, DateTime startDate, DateTime endDate, int campusId = 0)
		{
			GetCancelledJobsByStudentAndDateRangeReq getCancelledJobsByStudentAndDateRangeReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<GetCancelledJobsByStudentAndDateRangeReq>();
			getCancelledJobsByStudentAndDateRangeReq.StudentPersonId = studentPersonId;
			getCancelledJobsByStudentAndDateRangeReq.StartDate = startDate;
			getCancelledJobsByStudentAndDateRangeReq.EndDate = endDate;
			getCancelledJobsByStudentAndDateRangeReq.CampusId = campusId;
			return ClientServiceFactory.GetClientInstance<IMediaJob>().GetCancelledJobsByStudentAndDateRange(getCancelledJobsByStudentAndDateRangeReq).MediaJobList;
		}

		// Token: 0x0600060C RID: 1548 RVA: 0x0001AB8C File Offset: 0x00018D8C
		public IList<CompletedMediaJobDTO> GetCompletedJobsByStaffAndDateRange(int assignedStaffId, DateTime startDate, DateTime endDate, int campusId = 0)
		{
			GetCompletedJobsByStaffAndDateRangeReq getCompletedJobsByStaffAndDateRangeReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<GetCompletedJobsByStaffAndDateRangeReq>();
			getCompletedJobsByStaffAndDateRangeReq.AssignedStaffId = assignedStaffId;
			getCompletedJobsByStaffAndDateRangeReq.StartDate = startDate;
			getCompletedJobsByStaffAndDateRangeReq.EndDate = endDate;
			getCompletedJobsByStaffAndDateRangeReq.CampusId = campusId;
			return ClientServiceFactory.GetClientInstance<IMediaJob>().GetCompletedJobsByStaffAndDateRange(getCompletedJobsByStaffAndDateRangeReq).MediaJobList;
		}

		// Token: 0x0600060D RID: 1549 RVA: 0x0001ABDC File Offset: 0x00018DDC
		public IList<CancelledMediaJobDTO> GetCancelledJobsByStaffAndDateRange(int assignedStaffId, DateTime startDate, DateTime endDate, int campusId = 0)
		{
			GetCancelledJobsByStaffAndDateRangeReq getCancelledJobsByStaffAndDateRangeReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<GetCancelledJobsByStaffAndDateRangeReq>();
			getCancelledJobsByStaffAndDateRangeReq.AssignedStaffId = assignedStaffId;
			getCancelledJobsByStaffAndDateRangeReq.StartDate = startDate;
			getCancelledJobsByStaffAndDateRangeReq.EndDate = endDate;
			getCancelledJobsByStaffAndDateRangeReq.CampusId = campusId;
			return ClientServiceFactory.GetClientInstance<IMediaJob>().GetCancelledJobsByStaffAndDateRange(getCancelledJobsByStaffAndDateRangeReq).MediaJobList;
		}

		// Token: 0x0600060E RID: 1550 RVA: 0x0001AC2C File Offset: 0x00018E2C
		public int CreateMediaJob(MediaJobDTO mediaJob)
		{
			CreateMediaJobReq createMediaJobReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<CreateMediaJobReq>();
			createMediaJobReq.MediaJob = mediaJob;
			return ClientServiceFactory.GetClientInstance<IMediaJob>().CreateMediaJob(createMediaJobReq).MediaJobId;
		}

		// Token: 0x0600060F RID: 1551 RVA: 0x0001AC64 File Offset: 0x00018E64
		public void UpdateMediaJob(MediaJobDTO mediaJob)
		{
			UpdateMediaJobReq updateMediaJobReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<UpdateMediaJobReq>();
			updateMediaJobReq.MediaJob = mediaJob;
			ClientServiceFactory.GetClientInstance<IMediaJob>().UpdateMediaJob(updateMediaJobReq);
		}

		// Token: 0x06000610 RID: 1552 RVA: 0x0001AC94 File Offset: 0x00018E94
		public IList<MediaContentRequestedInfoDTO> CancelMediaJob(MediaJobDTO mediaJob, string changeNotes)
		{
			CancelMediaJobReq cancelMediaJobReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<CancelMediaJobReq>();
			cancelMediaJobReq.MediaJob = mediaJob;
			cancelMediaJobReq.CancelNotes = changeNotes;
			return ClientServiceFactory.GetClientInstance<IMediaJob>().CancelMediaJob(cancelMediaJobReq).MediaContentRequestedInfoList;
		}

		// Token: 0x06000611 RID: 1553 RVA: 0x0001ACD4 File Offset: 0x00018ED4
		public IList<MediaContentRequestedInfoDTO> MarkMediaJobAsCompleted(MediaJobDTO mediaJob, string changeNotes, DateTime availableStartTime, DateTime availableEndTime)
		{
			MarkMediaJobAsCompletedReq markMediaJobAsCompletedReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<MarkMediaJobAsCompletedReq>();
			markMediaJobAsCompletedReq.MediaJob = mediaJob;
			markMediaJobAsCompletedReq.CompletedNotes = changeNotes;
			markMediaJobAsCompletedReq.AvailableStartTime = availableStartTime;
			markMediaJobAsCompletedReq.AvailableEndTime = availableEndTime;
			return ClientServiceFactory.GetClientInstance<IMediaJob>().MarkMediaJobAsCompleted(markMediaJobAsCompletedReq).MediaContentRequestedInfoList;
		}

		// Token: 0x06000612 RID: 1554 RVA: 0x0001AD24 File Offset: 0x00018F24
		public void ChangeMediaJobStatus(int mediaJobId, string changeNotes, ref string generalStatusnName, ref string publisherStatusName, ref string vendorStatusName, ref string inHouseStatusName)
		{
			ChangeMediaJobStatusReq changeMediaJobStatusReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<ChangeMediaJobStatusReq>();
			changeMediaJobStatusReq.MediaJobId = mediaJobId;
			changeMediaJobStatusReq.StatusChangedNotes = changeNotes;
			changeMediaJobStatusReq.GeneralStatusName = generalStatusnName;
			changeMediaJobStatusReq.PublisherStatusName = publisherStatusName;
			changeMediaJobStatusReq.VendorStatusName = vendorStatusName;
			changeMediaJobStatusReq.InHouseStatusName = inHouseStatusName;
			ChangeMediaJobStatusResp changeMediaJobStatusResp = ClientServiceFactory.GetClientInstance<IMediaJob>().ChangeMediaJobStatus(changeMediaJobStatusReq);
			generalStatusnName = changeMediaJobStatusResp.GeneralStatusName;
			publisherStatusName = changeMediaJobStatusResp.PublisherStatusName;
			vendorStatusName = changeMediaJobStatusResp.VendorStatusName;
			inHouseStatusName = changeMediaJobStatusResp.InHouseStatusName;
		}

		// Token: 0x06000613 RID: 1555 RVA: 0x0001ADA4 File Offset: 0x00018FA4
		public IList<MediaJobDTO> SplitJobIntoChapters(MediaJobDTO job, params string[] chapterTitles)
		{
			bool flag = chapterTitles == null || chapterTitles.Length == 0;
			IList<MediaJobDTO> result;
			if (flag)
			{
				result = null;
			}
			else
			{
				List<MediaJobDTO> list = new List<MediaJobDTO>();
				job.JobTitle = chapterTitles[0];
				list.Add(job);
				this.UpdateMediaJob(job);
				IStudentMediaRequestClientManager studentMediaRequestClientManager = new StudentMediaRequestClientManager();
				IList<MediaContentRequestedInfoDTO> list2 = studentMediaRequestClientManager.LoadAllMediaRequestInfoByJobId(job.MediaJobId);
				for (int i = 1; i < chapterTitles.Length; i++)
				{
					MediaJobDTO mediaJobDTO = job.Clone();
					mediaJobDTO.JobTitle = chapterTitles[i];
					mediaJobDTO.JobCurrentStatusNameAboutInHouse = string.Empty;
					mediaJobDTO.JobCurrentStatusNameAboutPublisher = string.Empty;
					mediaJobDTO.JobCurrentStatusNameAboutVendor = string.Empty;
					mediaJobDTO.JobCurrentStatusNameGeneral = "Created";
					mediaJobDTO.MediaJobId = this.CreateMediaJob(mediaJobDTO);
					foreach (MediaContentRequestedInfoDTO mediaContentRequestedInfoDTO in list2)
					{
						MediaContentRequestedInfoDTO mediaContentRequestedInfoDTO2 = mediaContentRequestedInfoDTO.Clone();
						mediaContentRequestedInfoDTO2.MediaJobId = mediaJobDTO.MediaJobId;
						mediaContentRequestedInfoDTO2.CreatedDatetime = DateTime.Now;
						mediaContentRequestedInfoDTO2.MediaContentRequestedInfoID = studentMediaRequestClientManager.AddStudentContentMediaRequestInfo(mediaContentRequestedInfoDTO2);
					}
					list.Add(mediaJobDTO);
				}
				result = list;
			}
			return result;
		}
	}
}
