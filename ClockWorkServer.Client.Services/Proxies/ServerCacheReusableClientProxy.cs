using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.Caching;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x02000057 RID: 87
	public class ServerCacheReusableClientProxy : WCFTokenBasedReusableClientProxy<IServerCache>, IServerCache, IService
	{
		// Token: 0x06000413 RID: 1043 RVA: 0x0000BDEA File Offset: 0x00009FEA
		public ServerCacheReusableClientProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x06000414 RID: 1044 RVA: 0x0000BDF5 File Offset: 0x00009FF5
		public ServerCacheReusableClientProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x06000415 RID: 1045 RVA: 0x0000BE04 File Offset: 0x0000A004
		public void ClearServerCache(ClearServerCacheReq Request)
		{
			this.WrapServiceMethod(delegate()
			{
				this.Proxy.ClearServerCache(Request);
			});
		}

		// Token: 0x06000416 RID: 1046 RVA: 0x0000BE3C File Offset: 0x0000A03C
		public void ClearAllUsersCache(ClearAllUsersCacheReq Request)
		{
			this.WrapServiceMethod(delegate()
			{
				this.Proxy.ClearAllUsersCache(Request);
			});
		}

		// Token: 0x06000417 RID: 1047 RVA: 0x0000BE74 File Offset: 0x0000A074
		public void ClearServerCacheAllSubItems(ClearServerCacheAllSubItemsReq Request)
		{
			this.WrapServiceMethod(delegate()
			{
				this.Proxy.ClearServerCacheAllSubItems(Request);
			});
		}

		// Token: 0x06000418 RID: 1048 RVA: 0x0000BEAC File Offset: 0x0000A0AC
		public void ClearCacheItems(ClearCacheItemsReq Request)
		{
			this.WrapServiceMethod(delegate()
			{
				this.Proxy.ClearCacheItems(Request);
			});
		}
	}
}
