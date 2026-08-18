using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.Updates;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x0200015A RID: 346
	internal class UpdaterClientBaseProxy : ClientBase<IUpdater>, IUpdater, IService, IConnectivity
	{
		// Token: 0x06000D4F RID: 3407 RVA: 0x000210A4 File Offset: 0x0001F2A4
		public UpdaterClientBaseProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x06000D50 RID: 3408 RVA: 0x000210AF File Offset: 0x0001F2AF
		public UpdaterClientBaseProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x06000D51 RID: 3409 RVA: 0x000210BC File Offset: 0x0001F2BC
		public int CheckConnectivity()
		{
			return base.Channel.CheckConnectivity();
		}

		// Token: 0x06000D52 RID: 3410 RVA: 0x000210DC File Offset: 0x0001F2DC
		public UpdateResponse GetUpdate(UpdateRequest updateRequest)
		{
			return base.Channel.GetUpdate(updateRequest);
		}

		// Token: 0x06000D53 RID: 3411 RVA: 0x000210FC File Offset: 0x0001F2FC
		public ApplyUpdateResp ApplyUpdate(ApplyUpdateReq applyUpdateRequest)
		{
			return base.Channel.ApplyUpdate(applyUpdateRequest);
		}

		// Token: 0x06000D54 RID: 3412 RVA: 0x0002111C File Offset: 0x0001F31C
		public AvailableUpdateResp GetAvailableUpdates(AvailableUpdateReq availableUpdateRequest)
		{
			return base.Channel.GetAvailableUpdates(availableUpdateRequest);
		}

		// Token: 0x06000D55 RID: 3413 RVA: 0x0002113C File Offset: 0x0001F33C
		public CancelOnScheduleUpdateResp CancelOnScheduleUpdates(CancelOnScheduleUpdatesReq cancelOnScheduleUpdatesReq)
		{
			return base.Channel.CancelOnScheduleUpdates(cancelOnScheduleUpdatesReq);
		}

		// Token: 0x06000D56 RID: 3414 RVA: 0x0002115C File Offset: 0x0001F35C
		public GetOnScheduleUpdatesResp GetOnScheduleUpdates(GetOnScheduleUpdatesReq getOnScheduleUpdatesReq)
		{
			return base.Channel.GetOnScheduleUpdates(getOnScheduleUpdatesReq);
		}

		// Token: 0x06000D57 RID: 3415 RVA: 0x0002117C File Offset: 0x0001F37C
		public UploadUpdateFilesResp UploadUpdateFiles(UploadUpdateFilesReq Request)
		{
			return base.Channel.UploadUpdateFiles(Request);
		}

		// Token: 0x06000D58 RID: 3416 RVA: 0x0002119C File Offset: 0x0001F39C
		public ForceUpdatingServiceToRunResp ForceUpdatingServiceToRun(ForceUpdatingServiceToRunReq Request)
		{
			return base.Channel.ForceUpdatingServiceToRun(Request);
		}
	}
}
