using System;
using System.Collections.Generic;
using TechnoPro.ClockWorkServer.Common.Services.Impl.Adapters;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat;
using TechnoPro.Common.Core.AlternativeFormat;
using TechnoPro.Common.Core.Mappers.AlternativeFormat;
using TechnoPro.Common.ICore.AlternativeFormat;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities.AlternativeFormat;

namespace TechnoPro.ClockWorkServer.Common.Services.Impl
{
	// Token: 0x02000006 RID: 6
	public class MediaJobServiceManager : IMediaJob, IService
	{
		// Token: 0x06000033 RID: 51 RVA: 0x00002BFC File Offset: 0x00000DFC
		public int CheckConnectivity()
		{
			return 1;
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00002C10 File Offset: 0x00000E10
		public AddMediaJobNoteResp AddMediaJobNote(AddMediaJobNoteReq request)
		{
			IMediaJobManager mediaJobManager = new MediaJobManager(request.GetOperationContext());
			return new AddMediaJobNoteResp
			{
				MediaJobNoteId = mediaJobManager.AddMediaJobNote(request.MediaJobId, request.Note.ToDomainObject())
			};
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00002C54 File Offset: 0x00000E54
		public UpdateMediaJobNoteResp UpdateMediaJobNote(UpdateMediaJobNoteReq request)
		{
			IMediaJobManager mediaJobManager = new MediaJobManager(request.GetOperationContext());
			mediaJobManager.UpdateMediaJobNote(request.Note.ToDomainObject());
			return new UpdateMediaJobNoteResp();
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00002C8C File Offset: 0x00000E8C
		public GetRunningNotesByMediaJobResp GetRunningNotesByMediaJob(GetRunningNotesByMediaJobReq request)
		{
			IMediaJobManager mediaJobManager = new MediaJobManager(request.GetOperationContext());
			return new GetRunningNotesByMediaJobResp
			{
				RunningNoteList = mediaJobManager.GetRunningNotesByMediaJob(request.MediaJobId).ToDTO()
			};
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00002CC8 File Offset: 0x00000EC8
		public GetActiveMediaJobByIdResp GetActiveMediaJobById(GetActiveMediaJobByIdReq request)
		{
			IMediaJobManager mediaJobManager = new MediaJobManager(request.GetOperationContext());
			return new GetActiveMediaJobByIdResp
			{
				MediaJob = mediaJobManager.GetActiveMediaJobById(request.MediaJobId).ToDTO()
			};
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00002D04 File Offset: 0x00000F04
		public GetActiveMediaJobByMediaContentAndFormatResp GetActiveMediaJobByMediaContentAndFormat(GetActiveMediaJobByMediaContentAndFormatReq request)
		{
			IMediaJobManager mediaJobManager = new MediaJobManager(request.GetOperationContext());
			return new GetActiveMediaJobByMediaContentAndFormatResp
			{
				MediaJobs = mediaJobManager.GetActiveMediaJobByMediaContentAndFormat(new Guid(request.MediaContentId), request.ContentFormat, 0).ToDTO()
			};
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00002D4C File Offset: 0x00000F4C
		public GetCountActiveMediaJobByMediaContentPerFormatIdResp GetCountActiveMediaJobByMediaContentPerFormatId(GetCountActiveMediaJobByMediaContentPerFormatIdReq request)
		{
			IMediaJobManager mediaJobManager = new MediaJobManager(request.GetOperationContext());
			return new GetCountActiveMediaJobByMediaContentPerFormatIdResp
			{
				CountActiveJobs = mediaJobManager.GetCountActiveMediaJobByMediaContentPerFormatId(request.MediaContentPerFormatId, request.StudentId)
			};
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00002D88 File Offset: 0x00000F88
		public GetCountActiveMediaJobByMediaContentAndFormatResp GetCountActiveMediaJobByMediaContentAndFormat(GetCountActiveMediaJobByMediaContentAndFormatReq request)
		{
			IMediaJobManager mediaJobManager = new MediaJobManager(request.GetOperationContext());
			return new GetCountActiveMediaJobByMediaContentAndFormatResp
			{
				CountActiveJobs = mediaJobManager.GetCountActiveMediaJobByMediaContentAndFormat(new Guid(request.MediaContentId), request.ContentFormat, 0)
			};
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00002DCC File Offset: 0x00000FCC
		public GetActiveMediaJobsByAssignedStaffResp GetActiveMediaJobsByAssignedStaff(GetActiveMediaJobsByAssignedStaffReq request)
		{
			IMediaJobManager mediaJobManager = new MediaJobManager(request.GetOperationContext());
			return new GetActiveMediaJobsByAssignedStaffResp
			{
				MediaJobList = mediaJobManager.GetActiveMediaJobsByAssignedStaff(request.AssignedStaffId, request.CampusId).ToDTO()
			};
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00002E10 File Offset: 0x00001010
		public GetActiveMediaJobsByExpiredInLessThanResp GetActiveMediaJobsByExpiredInLessThan(GetActiveMediaJobsByExpiredInLessThanReq request)
		{
			IMediaJobManager mediaJobManager = new MediaJobManager(request.GetOperationContext());
			return new GetActiveMediaJobsByExpiredInLessThanResp
			{
				MediaJobList = mediaJobManager.GetActiveMediaJobsByExpiredInLessThan(request.DueDateIn).ToDTO()
			};
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00002E4C File Offset: 0x0000104C
		public GetActiveExpiredMediaJobsResp GetActiveExpiredMediaJobs(GetActiveExpiredMediaJobsReq request)
		{
			IMediaJobManager mediaJobManager = new MediaJobManager(request.GetOperationContext());
			return new GetActiveExpiredMediaJobsResp
			{
				MediaJobList = mediaJobManager.GetActiveExpiredMediaJobs().ToDTO()
			};
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00002E84 File Offset: 0x00001084
		public GetActiveJobsResp GetActiveJobs(GetActiveJobsReq request)
		{
			IMediaJobManager mediaJobManager = new MediaJobManager(request.GetOperationContext());
			return new GetActiveJobsResp
			{
				MediaJobList = mediaJobManager.GetActiveJobs(request.CampusId).ToDTO()
			};
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00002EC0 File Offset: 0x000010C0
		public GetActiveJobsByStudentResp GetActiveJobsByStudent(GetActiveJobsByStudentReq request)
		{
			IMediaJobManager mediaJobManager = new MediaJobManager(request.GetOperationContext());
			return new GetActiveJobsByStudentResp
			{
				MediaJobList = mediaJobManager.GetActiveJobsByStudent(request.StudentPersonId, request.CampusId).ToDTO()
			};
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00002F04 File Offset: 0x00001104
		public GetCompletedMediaJobByIdResp GetCompletedMediaJobById(GetCompletedMediaJobByIdReq request)
		{
			IMediaJobManager mediaJobManager = new MediaJobManager(request.GetOperationContext());
			return new GetCompletedMediaJobByIdResp
			{
				MediaJob = mediaJobManager.GetCompletedMediaJobById(request.MediaJobId).ToDTO()
			};
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00002F40 File Offset: 0x00001140
		public GetCancelledMediaJobByIdResp GetCancelledMediaJobById(GetCancelledMediaJobByIdReq request)
		{
			IMediaJobManager mediaJobManager = new MediaJobManager(request.GetOperationContext());
			return new GetCancelledMediaJobByIdResp
			{
				MediaJob = mediaJobManager.GetCancelledMediaJobById(request.MediaJobId).ToDTO()
			};
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00002F7C File Offset: 0x0000117C
		public GetCompletedMediaJobByMediaContentAndFormatResp GetCompletedMediaJobByMediaContentAndFormat(GetCompletedMediaJobByMediaContentAndFormatReq request)
		{
			IMediaJobManager mediaJobManager = new MediaJobManager(request.GetOperationContext());
			return new GetCompletedMediaJobByMediaContentAndFormatResp
			{
				MediaJobs = mediaJobManager.GetCompletedMediaJobByMediaContentAndFormat(new Guid(request.MediaContentId), request.ContentFormat, 0).ToDTO()
			};
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00002FC4 File Offset: 0x000011C4
		public GetCompletedMediaJobsByAssignedStaffResp GetCompletedMediaJobsByAssignedStaff(GetCompletedMediaJobsByAssignedStaffReq request)
		{
			IMediaJobManager mediaJobManager = new MediaJobManager(request.GetOperationContext());
			return new GetCompletedMediaJobsByAssignedStaffResp
			{
				MediaJobList = mediaJobManager.GetCompletedMediaJobsByAssignedStaff(request.AssignedStaffId, request.CampusId).ToDTO()
			};
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00003008 File Offset: 0x00001208
		public GetCompletedJobsByDateRangeResp GetCompletedJobsByDateRange(GetCompletedJobsByDateRangeReq request)
		{
			IMediaJobManager mediaJobManager = new MediaJobManager(request.GetOperationContext());
			return new GetCompletedJobsByDateRangeResp
			{
				MediaJobList = mediaJobManager.GetCompletedJobsByDateRange(request.StartDate, request.EndDate, request.CampusId).ToDTO()
			};
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00003050 File Offset: 0x00001250
		public GetCancelledJobsByDateRangeResp GetCancelledJobsByDateRange(GetCancelledJobsByDateRangeReq request)
		{
			IMediaJobManager mediaJobManager = new MediaJobManager(request.GetOperationContext());
			return new GetCancelledJobsByDateRangeResp
			{
				MediaJobList = mediaJobManager.GetCancelledJobsByDateRange(request.StartDate, request.EndDate, request.CampusId).ToDTO()
			};
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00003098 File Offset: 0x00001298
		public GetCompletedJobsResp GetCompletedJobs(GetCompletedJobsReq request)
		{
			IMediaJobManager mediaJobManager = new MediaJobManager(request.GetOperationContext());
			return new GetCompletedJobsResp
			{
				MediaJobList = mediaJobManager.GetCompletedJobs(request.CampusId).ToDTO()
			};
		}

		// Token: 0x06000047 RID: 71 RVA: 0x000030D4 File Offset: 0x000012D4
		public GetCancelledJobsResp GetCancelledJobs(GetCancelledJobsReq request)
		{
			IMediaJobManager mediaJobManager = new MediaJobManager(request.GetOperationContext());
			return new GetCancelledJobsResp
			{
				MediaJobList = mediaJobManager.GetCancelledJobs(request.CampusId).ToDTO()
			};
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00003110 File Offset: 0x00001310
		public GetCompletedJobsByStudentResp GetCompletedJobsByStudent(GetCompletedJobsByStudentReq request)
		{
			IMediaJobManager mediaJobManager = new MediaJobManager(request.GetOperationContext());
			return new GetCompletedJobsByStudentResp
			{
				MediaJobList = mediaJobManager.GetCompletedJobsByStudent(request.StudentPersonId, request.CampusId).ToDTO()
			};
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00003154 File Offset: 0x00001354
		public GetCompletedJobsByStudentAndDateRangeResp GetCompletedJobsByStudentAndDateRange(GetCompletedJobsByStudentAndDateRangeReq request)
		{
			IMediaJobManager mediaJobManager = new MediaJobManager(request.GetOperationContext());
			return new GetCompletedJobsByStudentAndDateRangeResp
			{
				MediaJobList = mediaJobManager.GetCompletedJobsByStudentAndDateRange(request.StudentPersonId, request.StartDate, request.EndDate, request.CampusId).ToDTO()
			};
		}

		// Token: 0x0600004A RID: 74 RVA: 0x000031A4 File Offset: 0x000013A4
		public GetCancelledJobsByStudentAndDateRangeResp GetCancelledJobsByStudentAndDateRange(GetCancelledJobsByStudentAndDateRangeReq request)
		{
			IMediaJobManager mediaJobManager = new MediaJobManager(request.GetOperationContext());
			return new GetCancelledJobsByStudentAndDateRangeResp
			{
				MediaJobList = mediaJobManager.GetCancelledJobsByStudentAndDateRange(request.StudentPersonId, request.StartDate, request.EndDate, request.CampusId).ToDTO()
			};
		}

		// Token: 0x0600004B RID: 75 RVA: 0x000031F4 File Offset: 0x000013F4
		public GetCompletedJobsByStaffAndDateRangeResp GetCompletedJobsByStaffAndDateRange(GetCompletedJobsByStaffAndDateRangeReq request)
		{
			IMediaJobManager mediaJobManager = new MediaJobManager(request.GetOperationContext());
			return new GetCompletedJobsByStaffAndDateRangeResp
			{
				MediaJobList = mediaJobManager.GetCompletedJobsByStaffAndDateRange(request.AssignedStaffId, request.StartDate, request.EndDate, request.CampusId).ToDTO()
			};
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00003244 File Offset: 0x00001444
		public GetCancelledJobsByStaffAndDateRangeResp GetCancelledJobsByStaffAndDateRange(GetCancelledJobsByStaffAndDateRangeReq request)
		{
			IMediaJobManager mediaJobManager = new MediaJobManager(request.GetOperationContext());
			return new GetCancelledJobsByStaffAndDateRangeResp
			{
				MediaJobList = mediaJobManager.GetCancelledJobsByStaffAndDateRange(request.AssignedStaffId, request.StartDate, request.EndDate, request.CampusId).ToDTO()
			};
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00003294 File Offset: 0x00001494
		public CreateMediaJobResp CreateMediaJob(CreateMediaJobReq request)
		{
			IMediaJobManager mediaJobManager = new MediaJobManager(request.GetOperationContext());
			return new CreateMediaJobResp
			{
				MediaJobId = mediaJobManager.CreateMediaJob(request.MediaJob.ToDomainObject())
			};
		}

		// Token: 0x0600004E RID: 78 RVA: 0x000032D0 File Offset: 0x000014D0
		public UpdateMediaJobResp UpdateMediaJob(UpdateMediaJobReq request)
		{
			IMediaJobManager mediaJobManager = new MediaJobManager(request.GetOperationContext());
			mediaJobManager.UpdateMediaJob(request.MediaJob.ToDomainObject());
			return new UpdateMediaJobResp();
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00003308 File Offset: 0x00001508
		public CancelMediaJobResp CancelMediaJob(CancelMediaJobReq request)
		{
			IMediaJobManager mediaJobManager = new MediaJobManager(request.GetOperationContext());
			CancelledMediaJob cancelledMediaJob = request.MediaJob.ToDomainObject().CopyToCancelledMediaJob();
			bool flag = cancelledMediaJob != null;
			if (flag)
			{
				cancelledMediaJob.CancellationReason = request.CancelNotes;
			}
			IList<MediaContentRequestedInfo> list = mediaJobManager.CancelMediaJob(cancelledMediaJob);
			return new CancelMediaJobResp
			{
				MediaContentRequestedInfoList = list.ToDTO()
			};
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00003368 File Offset: 0x00001568
		public MarkMediaJobAsCompletedResp MarkMediaJobAsCompleted(MarkMediaJobAsCompletedReq request)
		{
			IMediaJobManager mediaJobManager = new MediaJobManager(request.GetOperationContext());
			CompletedMediaJob completedMediaJob = request.MediaJob.ToDomainObject().CopyToCompletedMediaJob();
			bool flag = completedMediaJob != null;
			if (flag)
			{
				completedMediaJob.CompletedNotes = request.CompletedNotes;
			}
			IList<MediaContentRequestedInfo> list = mediaJobManager.MarkMediaJobAsCompleted(completedMediaJob, request.AvailableStartTime, request.AvailableEndTime);
			return new MarkMediaJobAsCompletedResp
			{
				MediaContentRequestedInfoList = list.ToDTO()
			};
		}

		// Token: 0x06000051 RID: 81 RVA: 0x000033D4 File Offset: 0x000015D4
		public ChangeMediaJobStatusResp ChangeMediaJobStatus(ChangeMediaJobStatusReq request)
		{
			IMediaJobManager mediaJobManager = new MediaJobManager(request.GetOperationContext());
			string generalStatusName = request.GeneralStatusName;
			string publisherStatusName = request.PublisherStatusName;
			string vendorStatusName = request.VendorStatusName;
			string inHouseStatusName = request.InHouseStatusName;
			mediaJobManager.ChangeMediaJobStatus(request.MediaJobId, request.StatusChangedNotes, ref generalStatusName, ref publisherStatusName, ref vendorStatusName, ref inHouseStatusName);
			return new ChangeMediaJobStatusResp
			{
				MediaJobId = request.MediaJobId,
				GeneralStatusName = generalStatusName,
				VendorStatusName = vendorStatusName,
				PublisherStatusName = publisherStatusName,
				InHouseStatusName = inHouseStatusName
			};
		}
	}
}
