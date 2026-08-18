using System;
using TechnoPro.ClockWorkServer.Common.Services.Impl.Adapters;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.Startup;
using TechnoPro.Common.Core.Mappers.Startup;
using TechnoPro.Common.Core.Startup;
using TechnoPro.Common.ICore.Startup;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities.Startup;

namespace TechnoPro.ClockWorkServer.Common.Services.Impl
{
	// Token: 0x0200008C RID: 140
	public class StartupServiceManager : IStartup, IService
	{
		// Token: 0x0600050F RID: 1295 RVA: 0x00017ADC File Offset: 0x00015CDC
		public LoadCacheClusterFullResp LoadCacheClusterFull(LoadCacheClusterFullReq Request)
		{
			IStartupManager startupManager = new StartupManager(Request.GetOperationContext());
			CacheClusterFull cacheClusterFull = startupManager.LoadCacheClusterFull();
			return new LoadCacheClusterFullResp
			{
				Info = ((cacheClusterFull == null) ? null : cacheClusterFull.ToDTO())
			};
		}

		// Token: 0x06000510 RID: 1296 RVA: 0x00017B1C File Offset: 0x00015D1C
		public int CheckConnectivity()
		{
			return 1;
		}
	}
}
