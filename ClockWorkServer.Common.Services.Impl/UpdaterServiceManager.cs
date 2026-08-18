using System;
using System.Collections.Generic;
using System.IO;
using TechnoPro.ClockWorkServer.Common.Services.Impl.Adapters;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.Updates;
using TechnoPro.ClockWorkServer.Core.Impl;
using TechnoPro.Common.Core.Mappers.Files;
using TechnoPro.Common.Core.Mappers.Updates;
using TechnoPro.Common.Core.Updates;
using TechnoPro.Common.ICore.Updates;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities.Updates;
using TechnoPro.Common.Unity.IoC;

namespace TechnoPro.ClockWorkServer.Common.Services.Impl
{
	// Token: 0x0200009C RID: 156
	public class UpdaterServiceManager : IUpdater, IService, IConnectivity
	{
		// Token: 0x060005AD RID: 1453 RVA: 0x00017A1F File Offset: 0x00015C1F
		public UpdateResponse GetUpdate(UpdateRequest updateReq)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060005AE RID: 1454 RVA: 0x0001A794 File Offset: 0x00018994
		private string GetServerWcfBinPath()
		{
			string serverVirtualDirectory = ObjectFactory.Resolve<ServerExecutingContext>().ServerVirtualDirectory;
			return Path.Combine(serverVirtualDirectory, "bin");
		}

		// Token: 0x060005AF RID: 1455 RVA: 0x0001A7BC File Offset: 0x000189BC
		public AvailableUpdateResp GetAvailableUpdates(AvailableUpdateReq request)
		{
			IUpdateManager updateManager = new UpdateManager(request.GetOperationContext(), this.GetServerWcfBinPath());
			return new AvailableUpdateResp
			{
				UpdatesInfo = updateManager.GetAvailableUpdates().ToDTO()
			};
		}

		// Token: 0x060005B0 RID: 1456 RVA: 0x0001A7F8 File Offset: 0x000189F8
		public ApplyUpdateResp ApplyUpdate(ApplyUpdateReq request)
		{
			IUpdateManager updateManager = new UpdateManager(request.GetOperationContext(), this.GetServerWcfBinPath());
			updateManager.ApplyUpdates(request.Updates.ToDomainObject());
			return new ApplyUpdateResp();
		}

		// Token: 0x060005B1 RID: 1457 RVA: 0x0001A834 File Offset: 0x00018A34
		public int CheckConnectivity()
		{
			return 1;
		}

		// Token: 0x060005B2 RID: 1458 RVA: 0x0001A848 File Offset: 0x00018A48
		public GetOnScheduleUpdatesResp GetOnScheduleUpdates(GetOnScheduleUpdatesReq request)
		{
			IUpdateManager updateManager = new UpdateManager(request.GetOperationContext(), this.GetServerWcfBinPath());
			return new GetOnScheduleUpdatesResp
			{
				UpdatesInfo = updateManager.GetOnScheduleUpdates().ToDTO()
			};
		}

		// Token: 0x060005B3 RID: 1459 RVA: 0x0001A884 File Offset: 0x00018A84
		public CancelOnScheduleUpdateResp CancelOnScheduleUpdates(CancelOnScheduleUpdatesReq request)
		{
			IUpdateManager updateManager = new UpdateManager(request.GetOperationContext(), this.GetServerWcfBinPath());
			updateManager.CancelOnScheduleUpdates(request.Updates.ToDomainObject());
			return new CancelOnScheduleUpdateResp();
		}

		// Token: 0x060005B4 RID: 1460 RVA: 0x0001A8C0 File Offset: 0x00018AC0
		public UploadUpdateFilesResp UploadUpdateFiles(UploadUpdateFilesReq request)
		{
			IUpdateManager updateManager = new UpdateManager(request.GetOperationContext(), this.GetServerWcfBinPath());
			IList<UploadUpdateFileResult> list = updateManager.UploadUpdateFiles(request.Updates.ToDomainObject());
			return new UploadUpdateFilesResp
			{
				UploadFilesResult = list.ToDTO()
			};
		}

		// Token: 0x060005B5 RID: 1461 RVA: 0x0001A908 File Offset: 0x00018B08
		public ForceUpdatingServiceToRunResp ForceUpdatingServiceToRun(ForceUpdatingServiceToRunReq Request)
		{
			IUpdateManager updateManager = new UpdateManager(Request.GetOperationContext(), this.GetServerWcfBinPath());
			ForceUpdatingServiceToRunResp result;
			try
			{
				updateManager.ForceUpdatingServiceToRun();
				result = new ForceUpdatingServiceToRunResp
				{
					Worked = true,
					ErrorMessage = null
				};
			}
			catch (Exception ex)
			{
				result = new ForceUpdatingServiceToRunResp
				{
					Worked = false,
					ErrorMessage = ex.ToString()
				};
			}
			return result;
		}
	}
}
