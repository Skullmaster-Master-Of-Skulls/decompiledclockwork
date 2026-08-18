using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x0200012D RID: 301
	public class ServiceProviderMatchingReusableClientProxy : WCFTokenBasedReusableClientProxy<IServiceProviderMatching>, IServiceProviderMatching, IService
	{
		// Token: 0x06000BDF RID: 3039 RVA: 0x0001DDEA File Offset: 0x0001BFEA
		public ServiceProviderMatchingReusableClientProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x06000BE0 RID: 3040 RVA: 0x0001DDF5 File Offset: 0x0001BFF5
		public ServiceProviderMatchingReusableClientProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}
	}
}
