using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.Caching;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x02000058 RID: 88
	internal class ServerCacheClientBaseProxy : ClientBase<IServerCache>, IServerCache, IService
	{
		// Token: 0x06000419 RID: 1049 RVA: 0x0000BEE1 File Offset: 0x0000A0E1
		public ServerCacheClientBaseProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x0600041A RID: 1050 RVA: 0x0000BEEC File Offset: 0x0000A0EC
		public ServerCacheClientBaseProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x0600041B RID: 1051 RVA: 0x0000BEF8 File Offset: 0x0000A0F8
		public void ClearServerCache(ClearServerCacheReq Request)
		{
			base.Channel.ClearServerCache(Request);
		}

		// Token: 0x0600041C RID: 1052 RVA: 0x0000BF08 File Offset: 0x0000A108
		public void ClearAllUsersCache(ClearAllUsersCacheReq Request)
		{
			base.Channel.ClearAllUsersCache(Request);
		}

		// Token: 0x0600041D RID: 1053 RVA: 0x0000BF18 File Offset: 0x0000A118
		public void ClearServerCacheAllSubItems(ClearServerCacheAllSubItemsReq Request)
		{
			base.Channel.ClearServerCacheAllSubItems(Request);
		}

		// Token: 0x0600041E RID: 1054 RVA: 0x0000BF28 File Offset: 0x0000A128
		public void ClearCacheItems(ClearCacheItemsReq Request)
		{
			base.Channel.ClearCacheItems(Request);
		}
	}
}
