using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x0200000C RID: 12
	public class MediaJobReusableClientProxy : WCFTokenBasedReusableClientProxy<IMediaJob>, IMediaJob, IService
	{
		// Token: 0x06000086 RID: 134 RVA: 0x0000372B File Offset: 0x0000192B
		public MediaJobReusableClientProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x06000087 RID: 135 RVA: 0x00003736 File Offset: 0x00001936
		public MediaJobReusableClientProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x06000088 RID: 136 RVA: 0x00003744 File Offset: 0x00001944
		public AddMediaJobNoteResp AddMediaJobNote(AddMediaJobNoteReq request)
		{
			return this.WrapServiceMethod<AddMediaJobNoteResp>(() => this.Proxy.AddMediaJobNote(request));
		}

		// Token: 0x06000089 RID: 137 RVA: 0x0000377C File Offset: 0x0000197C
		public CancelMediaJobResp CancelMediaJob(CancelMediaJobReq request)
		{
			return this.WrapServiceMethod<CancelMediaJobResp>(() => this.Proxy.CancelMediaJob(request));
		}

		// Token: 0x0600008A RID: 138 RVA: 0x000037B4 File Offset: 0x000019B4
		public ChangeMediaJobStatusResp ChangeMediaJobStatus(ChangeMediaJobStatusReq request)
		{
			return this.WrapServiceMethod<ChangeMediaJobStatusResp>(() => this.Proxy.ChangeMediaJobStatus(request));
		}

		// Token: 0x0600008B RID: 139 RVA: 0x000037EC File Offset: 0x000019EC
		public GetCancelledJobsByStudentAndDateRangeResp GetCancelledJobsByStudentAndDateRange(GetCancelledJobsByStudentAndDateRangeReq request)
		{
			return this.WrapServiceMethod<GetCancelledJobsByStudentAndDateRangeResp>(() => this.Proxy.GetCancelledJobsByStudentAndDateRange(request));
		}

		// Token: 0x0600008C RID: 140 RVA: 0x00003824 File Offset: 0x00001A24
		public GetCompletedJobsByStaffAndDateRangeResp GetCompletedJobsByStaffAndDateRange(GetCompletedJobsByStaffAndDateRangeReq request)
		{
			return this.WrapServiceMethod<GetCompletedJobsByStaffAndDateRangeResp>(() => this.Proxy.GetCompletedJobsByStaffAndDateRange(request));
		}

		// Token: 0x0600008D RID: 141 RVA: 0x0000385C File Offset: 0x00001A5C
		public GetCancelledJobsByStaffAndDateRangeResp GetCancelledJobsByStaffAndDateRange(GetCancelledJobsByStaffAndDateRangeReq request)
		{
			return this.WrapServiceMethod<GetCancelledJobsByStaffAndDateRangeResp>(() => this.Proxy.GetCancelledJobsByStaffAndDateRange(request));
		}

		// Token: 0x0600008E RID: 142 RVA: 0x00003894 File Offset: 0x00001A94
		public CreateMediaJobResp CreateMediaJob(CreateMediaJobReq request)
		{
			return this.WrapServiceMethod<CreateMediaJobResp>(() => this.Proxy.CreateMediaJob(request));
		}

		// Token: 0x0600008F RID: 143 RVA: 0x000038CC File Offset: 0x00001ACC
		public GetActiveExpiredMediaJobsResp GetActiveExpiredMediaJobs(GetActiveExpiredMediaJobsReq request)
		{
			return this.WrapServiceMethod<GetActiveExpiredMediaJobsResp>(() => this.Proxy.GetActiveExpiredMediaJobs(request));
		}

		// Token: 0x06000090 RID: 144 RVA: 0x00003904 File Offset: 0x00001B04
		public GetActiveJobsResp GetActiveJobs(GetActiveJobsReq request)
		{
			return this.WrapServiceMethod<GetActiveJobsResp>(() => this.Proxy.GetActiveJobs(request));
		}

		// Token: 0x06000091 RID: 145 RVA: 0x0000393C File Offset: 0x00001B3C
		public GetActiveJobsByStudentResp GetActiveJobsByStudent(GetActiveJobsByStudentReq request)
		{
			return this.WrapServiceMethod<GetActiveJobsByStudentResp>(() => this.Proxy.GetActiveJobsByStudent(request));
		}

		// Token: 0x06000092 RID: 146 RVA: 0x00003974 File Offset: 0x00001B74
		public GetActiveMediaJobByIdResp GetActiveMediaJobById(GetActiveMediaJobByIdReq request)
		{
			return this.WrapServiceMethod<GetActiveMediaJobByIdResp>(() => this.Proxy.GetActiveMediaJobById(request));
		}

		// Token: 0x06000093 RID: 147 RVA: 0x000039AC File Offset: 0x00001BAC
		public GetActiveMediaJobByMediaContentAndFormatResp GetActiveMediaJobByMediaContentAndFormat(GetActiveMediaJobByMediaContentAndFormatReq request)
		{
			return this.WrapServiceMethod<GetActiveMediaJobByMediaContentAndFormatResp>(() => this.Proxy.GetActiveMediaJobByMediaContentAndFormat(request));
		}

		// Token: 0x06000094 RID: 148 RVA: 0x000039E4 File Offset: 0x00001BE4
		public GetCountActiveMediaJobByMediaContentPerFormatIdResp GetCountActiveMediaJobByMediaContentPerFormatId(GetCountActiveMediaJobByMediaContentPerFormatIdReq request)
		{
			return this.WrapServiceMethod<GetCountActiveMediaJobByMediaContentPerFormatIdResp>(() => this.Proxy.GetCountActiveMediaJobByMediaContentPerFormatId(request));
		}

		// Token: 0x06000095 RID: 149 RVA: 0x00003A1C File Offset: 0x00001C1C
		public GetCountActiveMediaJobByMediaContentAndFormatResp GetCountActiveMediaJobByMediaContentAndFormat(GetCountActiveMediaJobByMediaContentAndFormatReq request)
		{
			return this.WrapServiceMethod<GetCountActiveMediaJobByMediaContentAndFormatResp>(() => this.Proxy.GetCountActiveMediaJobByMediaContentAndFormat(request));
		}

		// Token: 0x06000096 RID: 150 RVA: 0x00003A54 File Offset: 0x00001C54
		public GetActiveMediaJobsByAssignedStaffResp GetActiveMediaJobsByAssignedStaff(GetActiveMediaJobsByAssignedStaffReq request)
		{
			return this.WrapServiceMethod<GetActiveMediaJobsByAssignedStaffResp>(() => this.Proxy.GetActiveMediaJobsByAssignedStaff(request));
		}

		// Token: 0x06000097 RID: 151 RVA: 0x00003A8C File Offset: 0x00001C8C
		public GetActiveMediaJobsByExpiredInLessThanResp GetActiveMediaJobsByExpiredInLessThan(GetActiveMediaJobsByExpiredInLessThanReq request)
		{
			return this.WrapServiceMethod<GetActiveMediaJobsByExpiredInLessThanResp>(() => this.Proxy.GetActiveMediaJobsByExpiredInLessThan(request));
		}

		// Token: 0x06000098 RID: 152 RVA: 0x00003AC4 File Offset: 0x00001CC4
		public GetCompletedMediaJobByIdResp GetCompletedMediaJobById(GetCompletedMediaJobByIdReq request)
		{
			return this.WrapServiceMethod<GetCompletedMediaJobByIdResp>(() => this.Proxy.GetCompletedMediaJobById(request));
		}

		// Token: 0x06000099 RID: 153 RVA: 0x00003AFC File Offset: 0x00001CFC
		public GetCancelledMediaJobByIdResp GetCancelledMediaJobById(GetCancelledMediaJobByIdReq request)
		{
			return this.WrapServiceMethod<GetCancelledMediaJobByIdResp>(() => this.Proxy.GetCancelledMediaJobById(request));
		}

		// Token: 0x0600009A RID: 154 RVA: 0x00003B34 File Offset: 0x00001D34
		public GetCancelledJobsByDateRangeResp GetCancelledJobsByDateRange(GetCancelledJobsByDateRangeReq request)
		{
			return this.WrapServiceMethod<GetCancelledJobsByDateRangeResp>(() => this.Proxy.GetCancelledJobsByDateRange(request));
		}

		// Token: 0x0600009B RID: 155 RVA: 0x00003B6C File Offset: 0x00001D6C
		public GetCompletedJobsResp GetCompletedJobs(GetCompletedJobsReq request)
		{
			return this.WrapServiceMethod<GetCompletedJobsResp>(() => this.Proxy.GetCompletedJobs(request));
		}

		// Token: 0x0600009C RID: 156 RVA: 0x00003BA4 File Offset: 0x00001DA4
		public GetCancelledJobsResp GetCancelledJobs(GetCancelledJobsReq request)
		{
			return this.WrapServiceMethod<GetCancelledJobsResp>(() => this.Proxy.GetCancelledJobs(request));
		}

		// Token: 0x0600009D RID: 157 RVA: 0x00003BDC File Offset: 0x00001DDC
		public GetCompletedJobsByDateRangeResp GetCompletedJobsByDateRange(GetCompletedJobsByDateRangeReq request)
		{
			return this.WrapServiceMethod<GetCompletedJobsByDateRangeResp>(() => this.Proxy.GetCompletedJobsByDateRange(request));
		}

		// Token: 0x0600009E RID: 158 RVA: 0x00003C14 File Offset: 0x00001E14
		public GetCompletedJobsByStudentResp GetCompletedJobsByStudent(GetCompletedJobsByStudentReq request)
		{
			return this.WrapServiceMethod<GetCompletedJobsByStudentResp>(() => this.Proxy.GetCompletedJobsByStudent(request));
		}

		// Token: 0x0600009F RID: 159 RVA: 0x00003C4C File Offset: 0x00001E4C
		public GetCompletedJobsByStudentAndDateRangeResp GetCompletedJobsByStudentAndDateRange(GetCompletedJobsByStudentAndDateRangeReq request)
		{
			return this.WrapServiceMethod<GetCompletedJobsByStudentAndDateRangeResp>(() => this.Proxy.GetCompletedJobsByStudentAndDateRange(request));
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x00003C84 File Offset: 0x00001E84
		public GetCompletedMediaJobByMediaContentAndFormatResp GetCompletedMediaJobByMediaContentAndFormat(GetCompletedMediaJobByMediaContentAndFormatReq request)
		{
			return this.WrapServiceMethod<GetCompletedMediaJobByMediaContentAndFormatResp>(() => this.Proxy.GetCompletedMediaJobByMediaContentAndFormat(request));
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x00003CBC File Offset: 0x00001EBC
		public GetCompletedMediaJobsByAssignedStaffResp GetCompletedMediaJobsByAssignedStaff(GetCompletedMediaJobsByAssignedStaffReq request)
		{
			return this.WrapServiceMethod<GetCompletedMediaJobsByAssignedStaffResp>(() => this.Proxy.GetCompletedMediaJobsByAssignedStaff(request));
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x00003CF4 File Offset: 0x00001EF4
		public GetRunningNotesByMediaJobResp GetRunningNotesByMediaJob(GetRunningNotesByMediaJobReq request)
		{
			return this.WrapServiceMethod<GetRunningNotesByMediaJobResp>(() => this.Proxy.GetRunningNotesByMediaJob(request));
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x00003D2C File Offset: 0x00001F2C
		public MarkMediaJobAsCompletedResp MarkMediaJobAsCompleted(MarkMediaJobAsCompletedReq request)
		{
			return this.WrapServiceMethod<MarkMediaJobAsCompletedResp>(() => this.Proxy.MarkMediaJobAsCompleted(request));
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x00003D64 File Offset: 0x00001F64
		public UpdateMediaJobResp UpdateMediaJob(UpdateMediaJobReq request)
		{
			return this.WrapServiceMethod<UpdateMediaJobResp>(() => this.Proxy.UpdateMediaJob(request));
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x00003D9C File Offset: 0x00001F9C
		public UpdateMediaJobNoteResp UpdateMediaJobNote(UpdateMediaJobNoteReq request)
		{
			return this.WrapServiceMethod<UpdateMediaJobNoteResp>(() => this.Proxy.UpdateMediaJobNote(request));
		}
	}
}
