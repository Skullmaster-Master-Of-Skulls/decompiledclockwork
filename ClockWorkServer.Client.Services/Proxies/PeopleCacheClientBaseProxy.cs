using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x02000106 RID: 262
	internal class PeopleCacheClientBaseProxy : ClientBase<IPeopleCache>, IPeopleCache, IService
	{
		// Token: 0x06000A28 RID: 2600 RVA: 0x00019E01 File Offset: 0x00018001
		public PeopleCacheClientBaseProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x06000A29 RID: 2601 RVA: 0x00019E0C File Offset: 0x0001800C
		public PeopleCacheClientBaseProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x06000A2A RID: 2602 RVA: 0x00019E18 File Offset: 0x00018018
		public void LoadAllUserObjectsIntoCache(LoadAllUserObjectsIntoCacheReq request)
		{
			base.Channel.LoadAllUserObjectsIntoCache(request);
		}
	}
}
