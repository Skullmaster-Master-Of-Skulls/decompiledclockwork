using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.ServiceProviderOriginal.ContractParameters;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x02000125 RID: 293
	public class ServiceProviderOriginalProviderReusableClientProxy : WCFTokenBasedReusableClientProxy<IServiceProviderOriginalProvider>, IServiceProviderOriginalProvider, IService
	{
		// Token: 0x06000B99 RID: 2969 RVA: 0x0001D3E2 File Offset: 0x0001B5E2
		public ServiceProviderOriginalProviderReusableClientProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x06000B9A RID: 2970 RVA: 0x0001D3ED File Offset: 0x0001B5ED
		public ServiceProviderOriginalProviderReusableClientProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x06000B9B RID: 2971 RVA: 0x0001D3FC File Offset: 0x0001B5FC
		public LoadProviderBaseByIdResp LoadProviderBaseById(LoadProviderBaseByIdReq Request)
		{
			return this.WrapServiceMethod<LoadProviderBaseByIdResp>(() => this.Proxy.LoadProviderBaseById(Request));
		}

		// Token: 0x06000B9C RID: 2972 RVA: 0x0001D434 File Offset: 0x0001B634
		public LoadProviderByIdResp LoadProviderById(LoadProviderByIdReq Request)
		{
			return this.WrapServiceMethod<LoadProviderByIdResp>(() => this.Proxy.LoadProviderById(Request));
		}
	}
}
