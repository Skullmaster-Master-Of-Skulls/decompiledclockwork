using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.ClockWorkServerJob;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x02000067 RID: 103
	public class ClockWorkServerJobReusableClientProxy : WCFTokenBasedReusableClientProxy<IClockWorkServerJob>, IClockWorkServerJob, IService
	{
		// Token: 0x0600046A RID: 1130 RVA: 0x0000C85E File Offset: 0x0000AA5E
		public ClockWorkServerJobReusableClientProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x0600046B RID: 1131 RVA: 0x0000C869 File Offset: 0x0000AA69
		public ClockWorkServerJobReusableClientProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x0600046C RID: 1132 RVA: 0x0000C878 File Offset: 0x0000AA78
		public GetClockWorkServerJobsResp GetClockWorkServerJobs(GetClockWorkServerJobsReq request)
		{
			return this.WrapServiceMethod<GetClockWorkServerJobsResp>(() => this.Proxy.GetClockWorkServerJobs(request));
		}

		// Token: 0x0600046D RID: 1133 RVA: 0x0000C8B0 File Offset: 0x0000AAB0
		public GetClockWorkServerJobByIdResp GetClockWorkServerJobById(GetClockWorkServerJobByIdReq request)
		{
			return this.WrapServiceMethod<GetClockWorkServerJobByIdResp>(() => this.Proxy.GetClockWorkServerJobById(request));
		}

		// Token: 0x0600046E RID: 1134 RVA: 0x0000C8E8 File Offset: 0x0000AAE8
		public CreateClockWorkServerJobResp CreateClockWorkServerJob(CreateClockWorkServerJobReq request)
		{
			return this.WrapServiceMethod<CreateClockWorkServerJobResp>(() => this.Proxy.CreateClockWorkServerJob(request));
		}

		// Token: 0x0600046F RID: 1135 RVA: 0x0000C920 File Offset: 0x0000AB20
		public UpdateClockWorkServerJobResp UpdateClockWorkServerJob(UpdateClockWorkServerJobReq request)
		{
			return this.WrapServiceMethod<UpdateClockWorkServerJobResp>(() => this.Proxy.UpdateClockWorkServerJob(request));
		}

		// Token: 0x06000470 RID: 1136 RVA: 0x0000C958 File Offset: 0x0000AB58
		public RemoveClockWorkServerJobResp RemoveClockWorkServerJob(RemoveClockWorkServerJobReq request)
		{
			return this.WrapServiceMethod<RemoveClockWorkServerJobResp>(() => this.Proxy.RemoveClockWorkServerJob(request));
		}

		// Token: 0x06000471 RID: 1137 RVA: 0x0000C990 File Offset: 0x0000AB90
		public GetClockWorkServerExecutingLogsByJobResp GetClockWorkServerExecutingLogsByJob(GetClockWorkServerExecutingLogsByJobReq request)
		{
			return this.WrapServiceMethod<GetClockWorkServerExecutingLogsByJobResp>(() => this.Proxy.GetClockWorkServerExecutingLogsByJob(request));
		}

		// Token: 0x06000472 RID: 1138 RVA: 0x0000C9C8 File Offset: 0x0000ABC8
		public GetClockWorkServerExecutingLogsResp GetClockWorkServerExecutingLogs(GetClockWorkServerExecutingLogsReq request)
		{
			return this.WrapServiceMethod<GetClockWorkServerExecutingLogsResp>(() => this.Proxy.GetClockWorkServerExecutingLogs(request));
		}

		// Token: 0x06000473 RID: 1139 RVA: 0x0000CA00 File Offset: 0x0000AC00
		public GetClockWorkServerJobTypesResp GetClockWorkServerJobTypes(GetClockWorkServerJobTypesReq request)
		{
			return this.WrapServiceMethod<GetClockWorkServerJobTypesResp>(() => this.Proxy.GetClockWorkServerJobTypes(request));
		}

		// Token: 0x06000474 RID: 1140 RVA: 0x0000CA38 File Offset: 0x0000AC38
		public RunClockWorkServerJobNowResp RunClockWorkServerJobNow(RunClockWorkServerJobNowReq request)
		{
			return this.WrapServiceMethod<RunClockWorkServerJobNowResp>(() => this.Proxy.RunClockWorkServerJobNow(request));
		}

		// Token: 0x06000475 RID: 1141 RVA: 0x0000CA70 File Offset: 0x0000AC70
		public EnableClockWorkServerJobResp EnableClockWorkServerJob(EnableClockWorkServerJobReq request)
		{
			return this.WrapServiceMethod<EnableClockWorkServerJobResp>(() => this.Proxy.EnableClockWorkServerJob(request));
		}

		// Token: 0x06000476 RID: 1142 RVA: 0x0000CAA8 File Offset: 0x0000ACA8
		public DisableClockWorkServerJobResp DisableClockWorkServerJob(DisableClockWorkServerJobReq request)
		{
			return this.WrapServiceMethod<DisableClockWorkServerJobResp>(() => this.Proxy.DisableClockWorkServerJob(request));
		}
	}
}
