using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.Startup;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x0200013E RID: 318
	internal class StartupClientBaseProxy : ClientBase<IStartup>, IStartup, IService
	{
		// Token: 0x06000C49 RID: 3145 RVA: 0x0001EB84 File Offset: 0x0001CD84
		public StartupClientBaseProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x06000C4A RID: 3146 RVA: 0x0001EB8F File Offset: 0x0001CD8F
		public StartupClientBaseProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x06000C4B RID: 3147 RVA: 0x0001EB9C File Offset: 0x0001CD9C
		public LoadCacheClusterFullResp LoadCacheClusterFull(LoadCacheClusterFullReq Request)
		{
			return base.Channel.LoadCacheClusterFull(Request);
		}
	}
}
