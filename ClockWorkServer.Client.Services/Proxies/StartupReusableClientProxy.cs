using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.Startup;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x0200013D RID: 317
	public class StartupReusableClientProxy : WCFTokenBasedReusableClientProxy<IStartup>, IStartup, IService
	{
		// Token: 0x06000C46 RID: 3142 RVA: 0x0001EB32 File Offset: 0x0001CD32
		public StartupReusableClientProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x06000C47 RID: 3143 RVA: 0x0001EB3D File Offset: 0x0001CD3D
		public StartupReusableClientProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x06000C48 RID: 3144 RVA: 0x0001EB4C File Offset: 0x0001CD4C
		public LoadCacheClusterFullResp LoadCacheClusterFull(LoadCacheClusterFullReq Request)
		{
			return this.WrapServiceMethod<LoadCacheClusterFullResp>(() => this.Proxy.LoadCacheClusterFull(Request));
		}
	}
}
