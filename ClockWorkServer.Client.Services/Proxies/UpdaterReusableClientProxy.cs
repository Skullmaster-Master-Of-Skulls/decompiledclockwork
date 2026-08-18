using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.Updates;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x02000159 RID: 345
	public class UpdaterReusableClientProxy : WCFTokenBasedReusableClientProxy<IUpdater>, IUpdater, IService, IConnectivity
	{
		// Token: 0x06000D46 RID: 3398 RVA: 0x00020F02 File Offset: 0x0001F102
		public UpdaterReusableClientProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x06000D47 RID: 3399 RVA: 0x00020F0D File Offset: 0x0001F10D
		public UpdaterReusableClientProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x06000D48 RID: 3400 RVA: 0x00020F1C File Offset: 0x0001F11C
		public UpdateResponse GetUpdate(UpdateRequest updateRequest)
		{
			return this.WrapServiceMethod<UpdateResponse>(() => this.Proxy.GetUpdate(updateRequest));
		}

		// Token: 0x06000D49 RID: 3401 RVA: 0x00020F54 File Offset: 0x0001F154
		public ApplyUpdateResp ApplyUpdate(ApplyUpdateReq applyUpdateRequest)
		{
			return this.WrapServiceMethod<ApplyUpdateResp>(() => this.Proxy.ApplyUpdate(applyUpdateRequest));
		}

		// Token: 0x06000D4A RID: 3402 RVA: 0x00020F8C File Offset: 0x0001F18C
		public AvailableUpdateResp GetAvailableUpdates(AvailableUpdateReq availableUpdateRequest)
		{
			return this.WrapServiceMethod<AvailableUpdateResp>(() => this.Proxy.GetAvailableUpdates(availableUpdateRequest));
		}

		// Token: 0x06000D4B RID: 3403 RVA: 0x00020FC4 File Offset: 0x0001F1C4
		public CancelOnScheduleUpdateResp CancelOnScheduleUpdates(CancelOnScheduleUpdatesReq cancelOnScheduleUpdatesReq)
		{
			return this.WrapServiceMethod<CancelOnScheduleUpdateResp>(() => this.Proxy.CancelOnScheduleUpdates(cancelOnScheduleUpdatesReq));
		}

		// Token: 0x06000D4C RID: 3404 RVA: 0x00020FFC File Offset: 0x0001F1FC
		public GetOnScheduleUpdatesResp GetOnScheduleUpdates(GetOnScheduleUpdatesReq getOnScheduleUpdatesReq)
		{
			return this.WrapServiceMethod<GetOnScheduleUpdatesResp>(() => this.Proxy.GetOnScheduleUpdates(getOnScheduleUpdatesReq));
		}

		// Token: 0x06000D4D RID: 3405 RVA: 0x00021034 File Offset: 0x0001F234
		public UploadUpdateFilesResp UploadUpdateFiles(UploadUpdateFilesReq Request)
		{
			return this.WrapServiceMethod<UploadUpdateFilesResp>(() => this.Proxy.UploadUpdateFiles(Request));
		}

		// Token: 0x06000D4E RID: 3406 RVA: 0x0002106C File Offset: 0x0001F26C
		public ForceUpdatingServiceToRunResp ForceUpdatingServiceToRun(ForceUpdatingServiceToRunReq Request)
		{
			return this.WrapServiceMethod<ForceUpdatingServiceToRunResp>(() => this.Proxy.ForceUpdatingServiceToRun(Request));
		}
	}
}
