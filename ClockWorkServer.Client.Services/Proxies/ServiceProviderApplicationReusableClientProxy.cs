using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.ServiceProvider.ContractParameters;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x02000127 RID: 295
	public class ServiceProviderApplicationReusableClientProxy : WCFTokenBasedReusableClientProxy<IServiceProviderApplication>, IServiceProviderApplication, IService
	{
		// Token: 0x06000BA1 RID: 2977 RVA: 0x0001D4C2 File Offset: 0x0001B6C2
		public ServiceProviderApplicationReusableClientProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x06000BA2 RID: 2978 RVA: 0x0001D4CD File Offset: 0x0001B6CD
		public ServiceProviderApplicationReusableClientProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x06000BA3 RID: 2979 RVA: 0x0001D4DC File Offset: 0x0001B6DC
		public CreateApplicationResp CreateApplication(CreateApplicationReq Request)
		{
			return this.WrapServiceMethod<CreateApplicationResp>(() => this.Proxy.CreateApplication(Request));
		}

		// Token: 0x06000BA4 RID: 2980 RVA: 0x0001D514 File Offset: 0x0001B714
		public DeleteApplicationResp DeleteApplication(DeleteApplicationReq Request)
		{
			return this.WrapServiceMethod<DeleteApplicationResp>(() => this.Proxy.DeleteApplication(Request));
		}

		// Token: 0x06000BA5 RID: 2981 RVA: 0x0001D54C File Offset: 0x0001B74C
		public LoadApplicationByProviderAndTypeResp LoadApplicationByProviderAndType(LoadApplicationByProviderAndTypeReq Request)
		{
			return this.WrapServiceMethod<LoadApplicationByProviderAndTypeResp>(() => this.Proxy.LoadApplicationByProviderAndType(Request));
		}

		// Token: 0x06000BA6 RID: 2982 RVA: 0x0001D584 File Offset: 0x0001B784
		public LoadApplicationsBySPProviderResp LoadApplicationsBySPProvider(LoadApplicationsBySPProviderReq Request)
		{
			return this.WrapServiceMethod<LoadApplicationsBySPProviderResp>(() => this.Proxy.LoadApplicationsBySPProvider(Request));
		}

		// Token: 0x06000BA7 RID: 2983 RVA: 0x0001D5BC File Offset: 0x0001B7BC
		public LoadApplicationsBySPProviderTypeResp LoadApplicationsBySPProviderType(LoadApplicationsBySPProviderTypeReq Request)
		{
			return this.WrapServiceMethod<LoadApplicationsBySPProviderTypeResp>(() => this.Proxy.LoadApplicationsBySPProviderType(Request));
		}

		// Token: 0x06000BA8 RID: 2984 RVA: 0x0001D5F4 File Offset: 0x0001B7F4
		public UpdateApplicationResp UpdateApplication(UpdateApplicationReq Request)
		{
			return this.WrapServiceMethod<UpdateApplicationResp>(() => this.Proxy.UpdateApplication(Request));
		}

		// Token: 0x06000BA9 RID: 2985 RVA: 0x0001D62C File Offset: 0x0001B82C
		public UpdateApplicationAvailabilityTypeResp UpdateApplicationAvailabilityType(UpdateApplicationAvailabilityTypeReq Request)
		{
			return this.WrapServiceMethod<UpdateApplicationAvailabilityTypeResp>(() => this.Proxy.UpdateApplicationAvailabilityType(Request));
		}

		// Token: 0x06000BAA RID: 2986 RVA: 0x0001D664 File Offset: 0x0001B864
		public LoadApplicationByIdResp LoadApplicationById(LoadApplicationByIdReq Request)
		{
			return this.WrapServiceMethod<LoadApplicationByIdResp>(() => this.Proxy.LoadApplicationById(Request));
		}
	}
}
