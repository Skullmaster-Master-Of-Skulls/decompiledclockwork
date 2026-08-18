using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x02000105 RID: 261
	public class PeopleCacheReusableClientProxy : WCFReusableClientProxy<IPeopleCache>, IPeopleCache, IService
	{
		// Token: 0x06000A25 RID: 2597 RVA: 0x00019DA2 File Offset: 0x00017FA2
		public PeopleCacheReusableClientProxy(string endpoint) : base(endpoint)
		{
			base.IncludeProxyHeader = false;
		}

		// Token: 0x06000A26 RID: 2598 RVA: 0x00019DB5 File Offset: 0x00017FB5
		public PeopleCacheReusableClientProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
			base.IncludeProxyHeader = false;
		}

		// Token: 0x06000A27 RID: 2599 RVA: 0x00019DCC File Offset: 0x00017FCC
		public void LoadAllUserObjectsIntoCache(LoadAllUserObjectsIntoCacheReq request)
		{
			this.WrapServiceMethod(delegate()
			{
				this.Proxy.LoadAllUserObjectsIntoCache(request);
			});
		}
	}
}
