using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x0200000D RID: 13
	internal class MediaJobClientBaseProxy : ClientBase<IMediaJob>, IMediaJob, IService
	{
		// Token: 0x060000A6 RID: 166 RVA: 0x00003DD4 File Offset: 0x00001FD4
		public MediaJobClientBaseProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x00003DDF File Offset: 0x00001FDF
		public MediaJobClientBaseProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x00003DEC File Offset: 0x00001FEC
		public AddMediaJobNoteResp AddMediaJobNote(AddMediaJobNoteReq request)
		{
			return base.Channel.AddMediaJobNote(request);
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x00003E0C File Offset: 0x0000200C
		public UpdateMediaJobNoteResp UpdateMediaJobNote(UpdateMediaJobNoteReq request)
		{
			return base.Channel.UpdateMediaJobNote(request);
		}

		// Token: 0x060000AA RID: 170 RVA: 0x00003E2C File Offset: 0x0000202C
		public GetRunningNotesByMediaJobResp GetRunningNotesByMediaJob(GetRunningNotesByMediaJobReq request)
		{
			return base.Channel.GetRunningNotesByMediaJob(request);
		}

		// Token: 0x060000AB RID: 171 RVA: 0x00003E4C File Offset: 0x0000204C
		public GetActiveMediaJobByIdResp GetActiveMediaJobById(GetActiveMediaJobByIdReq request)
		{
			return base.Channel.GetActiveMediaJobById(request);
		}

		// Token: 0x060000AC RID: 172 RVA: 0x00003E6C File Offset: 0x0000206C
		public GetActiveMediaJobByMediaContentAndFormatResp GetActiveMediaJobByMediaContentAndFormat(GetActiveMediaJobByMediaContentAndFormatReq request)
		{
			return base.Channel.GetActiveMediaJobByMediaContentAndFormat(request);
		}

		// Token: 0x060000AD RID: 173 RVA: 0x00003E8C File Offset: 0x0000208C
		public GetCountActiveMediaJobByMediaContentPerFormatIdResp GetCountActiveMediaJobByMediaContentPerFormatId(GetCountActiveMediaJobByMediaContentPerFormatIdReq request)
		{
			return base.Channel.GetCountActiveMediaJobByMediaContentPerFormatId(request);
		}

		// Token: 0x060000AE RID: 174 RVA: 0x00003EAC File Offset: 0x000020AC
		public GetCountActiveMediaJobByMediaContentAndFormatResp GetCountActiveMediaJobByMediaContentAndFormat(GetCountActiveMediaJobByMediaContentAndFormatReq request)
		{
			return base.Channel.GetCountActiveMediaJobByMediaContentAndFormat(request);
		}

		// Token: 0x060000AF RID: 175 RVA: 0x00003ECC File Offset: 0x000020CC
		public GetActiveMediaJobsByAssignedStaffResp GetActiveMediaJobsByAssignedStaff(GetActiveMediaJobsByAssignedStaffReq request)
		{
			return base.Channel.GetActiveMediaJobsByAssignedStaff(request);
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x00003EEC File Offset: 0x000020EC
		public GetActiveMediaJobsByExpiredInLessThanResp GetActiveMediaJobsByExpiredInLessThan(GetActiveMediaJobsByExpiredInLessThanReq request)
		{
			return base.Channel.GetActiveMediaJobsByExpiredInLessThan(request);
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x00003F0C File Offset: 0x0000210C
		public GetActiveExpiredMediaJobsResp GetActiveExpiredMediaJobs(GetActiveExpiredMediaJobsReq request)
		{
			return base.Channel.GetActiveExpiredMediaJobs(request);
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x00003F2C File Offset: 0x0000212C
		public GetActiveJobsResp GetActiveJobs(GetActiveJobsReq request)
		{
			return base.Channel.GetActiveJobs(request);
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x00003F4C File Offset: 0x0000214C
		public GetActiveJobsByStudentResp GetActiveJobsByStudent(GetActiveJobsByStudentReq request)
		{
			return base.Channel.GetActiveJobsByStudent(request);
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x00003F6C File Offset: 0x0000216C
		public GetCompletedMediaJobByIdResp GetCompletedMediaJobById(GetCompletedMediaJobByIdReq request)
		{
			return base.Channel.GetCompletedMediaJobById(request);
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x00003F8C File Offset: 0x0000218C
		public GetCancelledMediaJobByIdResp GetCancelledMediaJobById(GetCancelledMediaJobByIdReq request)
		{
			return base.Channel.GetCancelledMediaJobById(request);
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x00003FAC File Offset: 0x000021AC
		public GetCompletedMediaJobByMediaContentAndFormatResp GetCompletedMediaJobByMediaContentAndFormat(GetCompletedMediaJobByMediaContentAndFormatReq request)
		{
			return base.Channel.GetCompletedMediaJobByMediaContentAndFormat(request);
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x00003FCC File Offset: 0x000021CC
		public GetCompletedMediaJobsByAssignedStaffResp GetCompletedMediaJobsByAssignedStaff(GetCompletedMediaJobsByAssignedStaffReq request)
		{
			return base.Channel.GetCompletedMediaJobsByAssignedStaff(request);
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x00003FEC File Offset: 0x000021EC
		public GetCompletedJobsByDateRangeResp GetCompletedJobsByDateRange(GetCompletedJobsByDateRangeReq request)
		{
			return base.Channel.GetCompletedJobsByDateRange(request);
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x0000400C File Offset: 0x0000220C
		public GetCancelledJobsByDateRangeResp GetCancelledJobsByDateRange(GetCancelledJobsByDateRangeReq request)
		{
			return base.Channel.GetCancelledJobsByDateRange(request);
		}

		// Token: 0x060000BA RID: 186 RVA: 0x0000402C File Offset: 0x0000222C
		public GetCompletedJobsResp GetCompletedJobs(GetCompletedJobsReq request)
		{
			return base.Channel.GetCompletedJobs(request);
		}

		// Token: 0x060000BB RID: 187 RVA: 0x0000404C File Offset: 0x0000224C
		public GetCancelledJobsResp GetCancelledJobs(GetCancelledJobsReq request)
		{
			return base.Channel.GetCancelledJobs(request);
		}

		// Token: 0x060000BC RID: 188 RVA: 0x0000406C File Offset: 0x0000226C
		public GetCompletedJobsByStudentResp GetCompletedJobsByStudent(GetCompletedJobsByStudentReq request)
		{
			return base.Channel.GetCompletedJobsByStudent(request);
		}

		// Token: 0x060000BD RID: 189 RVA: 0x0000408C File Offset: 0x0000228C
		public GetCompletedJobsByStudentAndDateRangeResp GetCompletedJobsByStudentAndDateRange(GetCompletedJobsByStudentAndDateRangeReq request)
		{
			return base.Channel.GetCompletedJobsByStudentAndDateRange(request);
		}

		// Token: 0x060000BE RID: 190 RVA: 0x000040AC File Offset: 0x000022AC
		public GetCancelledJobsByStudentAndDateRangeResp GetCancelledJobsByStudentAndDateRange(GetCancelledJobsByStudentAndDateRangeReq request)
		{
			return base.Channel.GetCancelledJobsByStudentAndDateRange(request);
		}

		// Token: 0x060000BF RID: 191 RVA: 0x000040CC File Offset: 0x000022CC
		public GetCompletedJobsByStaffAndDateRangeResp GetCompletedJobsByStaffAndDateRange(GetCompletedJobsByStaffAndDateRangeReq request)
		{
			return base.Channel.GetCompletedJobsByStaffAndDateRange(request);
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x000040EC File Offset: 0x000022EC
		public GetCancelledJobsByStaffAndDateRangeResp GetCancelledJobsByStaffAndDateRange(GetCancelledJobsByStaffAndDateRangeReq request)
		{
			return base.Channel.GetCancelledJobsByStaffAndDateRange(request);
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x0000410C File Offset: 0x0000230C
		public CreateMediaJobResp CreateMediaJob(CreateMediaJobReq request)
		{
			return base.Channel.CreateMediaJob(request);
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x0000412C File Offset: 0x0000232C
		public UpdateMediaJobResp UpdateMediaJob(UpdateMediaJobReq request)
		{
			return base.Channel.UpdateMediaJob(request);
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x0000414C File Offset: 0x0000234C
		public CancelMediaJobResp CancelMediaJob(CancelMediaJobReq request)
		{
			return base.Channel.CancelMediaJob(request);
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x0000416C File Offset: 0x0000236C
		public MarkMediaJobAsCompletedResp MarkMediaJobAsCompleted(MarkMediaJobAsCompletedReq request)
		{
			return base.Channel.MarkMediaJobAsCompleted(request);
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x0000418C File Offset: 0x0000238C
		public ChangeMediaJobStatusResp ChangeMediaJobStatus(ChangeMediaJobStatusReq request)
		{
			return base.Channel.ChangeMediaJobStatus(request);
		}
	}
}
