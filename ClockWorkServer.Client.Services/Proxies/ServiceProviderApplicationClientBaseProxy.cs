using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.ServiceProvider.ContractParameters;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x02000128 RID: 296
	internal class ServiceProviderApplicationClientBaseProxy : ClientBase<IServiceProviderApplication>, IServiceProviderApplication, IService
	{
		// Token: 0x06000BAB RID: 2987 RVA: 0x0001D69C File Offset: 0x0001B89C
		public ServiceProviderApplicationClientBaseProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x06000BAC RID: 2988 RVA: 0x0001D6A7 File Offset: 0x0001B8A7
		public ServiceProviderApplicationClientBaseProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x06000BAD RID: 2989 RVA: 0x0001D6B4 File Offset: 0x0001B8B4
		public CreateApplicationResp CreateApplication(CreateApplicationReq Request)
		{
			return base.Channel.CreateApplication(Request);
		}

		// Token: 0x06000BAE RID: 2990 RVA: 0x0001D6D4 File Offset: 0x0001B8D4
		public DeleteApplicationResp DeleteApplication(DeleteApplicationReq Request)
		{
			return base.Channel.DeleteApplication(Request);
		}

		// Token: 0x06000BAF RID: 2991 RVA: 0x0001D6F4 File Offset: 0x0001B8F4
		public LoadApplicationByProviderAndTypeResp LoadApplicationByProviderAndType(LoadApplicationByProviderAndTypeReq Request)
		{
			return base.Channel.LoadApplicationByProviderAndType(Request);
		}

		// Token: 0x06000BB0 RID: 2992 RVA: 0x0001D714 File Offset: 0x0001B914
		public LoadApplicationsBySPProviderResp LoadApplicationsBySPProvider(LoadApplicationsBySPProviderReq Request)
		{
			return base.Channel.LoadApplicationsBySPProvider(Request);
		}

		// Token: 0x06000BB1 RID: 2993 RVA: 0x0001D734 File Offset: 0x0001B934
		public LoadApplicationsBySPProviderTypeResp LoadApplicationsBySPProviderType(LoadApplicationsBySPProviderTypeReq Request)
		{
			return base.Channel.LoadApplicationsBySPProviderType(Request);
		}

		// Token: 0x06000BB2 RID: 2994 RVA: 0x0001D754 File Offset: 0x0001B954
		public UpdateApplicationResp UpdateApplication(UpdateApplicationReq Request)
		{
			return base.Channel.UpdateApplication(Request);
		}

		// Token: 0x06000BB3 RID: 2995 RVA: 0x0001D774 File Offset: 0x0001B974
		public UpdateApplicationAvailabilityTypeResp UpdateApplicationAvailabilityType(UpdateApplicationAvailabilityTypeReq Request)
		{
			return base.Channel.UpdateApplicationAvailabilityType(Request);
		}

		// Token: 0x06000BB4 RID: 2996 RVA: 0x0001D794 File Offset: 0x0001B994
		public LoadApplicationByIdResp LoadApplicationById(LoadApplicationByIdReq Request)
		{
			return base.Channel.LoadApplicationById(Request);
		}
	}
}
