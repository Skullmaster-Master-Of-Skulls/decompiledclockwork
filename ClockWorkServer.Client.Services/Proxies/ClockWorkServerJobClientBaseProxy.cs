using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.ClockWorkServerJob;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x02000068 RID: 104
	internal class ClockWorkServerJobClientBaseProxy : ClientBase<IClockWorkServerJob>, IClockWorkServerJob, IService
	{
		// Token: 0x06000477 RID: 1143 RVA: 0x0000CAE0 File Offset: 0x0000ACE0
		public ClockWorkServerJobClientBaseProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x06000478 RID: 1144 RVA: 0x0000CAEB File Offset: 0x0000ACEB
		public ClockWorkServerJobClientBaseProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x06000479 RID: 1145 RVA: 0x0000CAF8 File Offset: 0x0000ACF8
		public GetClockWorkServerJobsResp GetClockWorkServerJobs(GetClockWorkServerJobsReq request)
		{
			return base.Channel.GetClockWorkServerJobs(request);
		}

		// Token: 0x0600047A RID: 1146 RVA: 0x0000CB18 File Offset: 0x0000AD18
		public GetClockWorkServerJobByIdResp GetClockWorkServerJobById(GetClockWorkServerJobByIdReq request)
		{
			return base.Channel.GetClockWorkServerJobById(request);
		}

		// Token: 0x0600047B RID: 1147 RVA: 0x0000CB38 File Offset: 0x0000AD38
		public CreateClockWorkServerJobResp CreateClockWorkServerJob(CreateClockWorkServerJobReq request)
		{
			return base.Channel.CreateClockWorkServerJob(request);
		}

		// Token: 0x0600047C RID: 1148 RVA: 0x0000CB58 File Offset: 0x0000AD58
		public UpdateClockWorkServerJobResp UpdateClockWorkServerJob(UpdateClockWorkServerJobReq request)
		{
			return base.Channel.UpdateClockWorkServerJob(request);
		}

		// Token: 0x0600047D RID: 1149 RVA: 0x0000CB78 File Offset: 0x0000AD78
		public RemoveClockWorkServerJobResp RemoveClockWorkServerJob(RemoveClockWorkServerJobReq request)
		{
			return base.Channel.RemoveClockWorkServerJob(request);
		}

		// Token: 0x0600047E RID: 1150 RVA: 0x0000CB98 File Offset: 0x0000AD98
		public GetClockWorkServerExecutingLogsByJobResp GetClockWorkServerExecutingLogsByJob(GetClockWorkServerExecutingLogsByJobReq request)
		{
			return base.Channel.GetClockWorkServerExecutingLogsByJob(request);
		}

		// Token: 0x0600047F RID: 1151 RVA: 0x0000CBB8 File Offset: 0x0000ADB8
		public GetClockWorkServerExecutingLogsResp GetClockWorkServerExecutingLogs(GetClockWorkServerExecutingLogsReq request)
		{
			return base.Channel.GetClockWorkServerExecutingLogs(request);
		}

		// Token: 0x06000480 RID: 1152 RVA: 0x0000CBD8 File Offset: 0x0000ADD8
		public GetClockWorkServerJobTypesResp GetClockWorkServerJobTypes(GetClockWorkServerJobTypesReq request)
		{
			return base.Channel.GetClockWorkServerJobTypes(request);
		}

		// Token: 0x06000481 RID: 1153 RVA: 0x0000CBF8 File Offset: 0x0000ADF8
		public RunClockWorkServerJobNowResp RunClockWorkServerJobNow(RunClockWorkServerJobNowReq request)
		{
			return base.Channel.RunClockWorkServerJobNow(request);
		}

		// Token: 0x06000482 RID: 1154 RVA: 0x0000CC18 File Offset: 0x0000AE18
		public EnableClockWorkServerJobResp EnableClockWorkServerJob(EnableClockWorkServerJobReq request)
		{
			return base.Channel.EnableClockWorkServerJob(request);
		}

		// Token: 0x06000483 RID: 1155 RVA: 0x0000CC38 File Offset: 0x0000AE38
		public DisableClockWorkServerJobResp DisableClockWorkServerJob(DisableClockWorkServerJobReq request)
		{
			return base.Channel.DisableClockWorkServerJob(request);
		}
	}
}
