using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x0200012E RID: 302
	internal class ServiceProviderMatchingClientProxy : ClientBase<IServiceProviderMatching>, IServiceProviderMatching, IService
	{
		// Token: 0x06000BE1 RID: 3041 RVA: 0x0001DE01 File Offset: 0x0001C001
		public ServiceProviderMatchingClientProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x06000BE2 RID: 3042 RVA: 0x0001DE0C File Offset: 0x0001C00C
		public ServiceProviderMatchingClientProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}
	}
}
