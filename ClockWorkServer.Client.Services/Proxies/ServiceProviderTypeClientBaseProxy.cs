using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.ServiceProvider.ContractParameters;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x02000130 RID: 304
	internal class ServiceProviderTypeClientBaseProxy : ClientBase<IServiceProviderType>, IServiceProviderType, IService
	{
		// Token: 0x06000BEB RID: 3051 RVA: 0x0001DF80 File Offset: 0x0001C180
		public ServiceProviderTypeClientBaseProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x06000BEC RID: 3052 RVA: 0x0001DF8B File Offset: 0x0001C18B
		public ServiceProviderTypeClientBaseProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x06000BED RID: 3053 RVA: 0x0001DF98 File Offset: 0x0001C198
		public CreateProviderTypeResp CreateProviderType(CreateProviderTypeReq Request)
		{
			return base.Channel.CreateProviderType(Request);
		}

		// Token: 0x06000BEE RID: 3054 RVA: 0x0001DFB8 File Offset: 0x0001C1B8
		public DeleteProviderTypeResp DeleteProviderType(DeleteProviderTypeReq Request)
		{
			return base.Channel.DeleteProviderType(Request);
		}

		// Token: 0x06000BEF RID: 3055 RVA: 0x0001DFD8 File Offset: 0x0001C1D8
		public LoadAllProviderTypesResp LoadAllProviderTypes(LoadAllProviderTypesReq Request)
		{
			return base.Channel.LoadAllProviderTypes(Request);
		}

		// Token: 0x06000BF0 RID: 3056 RVA: 0x0001DFF8 File Offset: 0x0001C1F8
		public LoadProviderTypeByBehaviourCodeResp LoadProviderTypeByBehaviourCode(LoadProviderTypeByBehaviourCodeReq Request)
		{
			return base.Channel.LoadProviderTypeByBehaviourCode(Request);
		}

		// Token: 0x06000BF1 RID: 3057 RVA: 0x0001E018 File Offset: 0x0001C218
		public LoadProviderTypeByIdResp LoadProviderTypeById(LoadProviderTypeByIdReq Request)
		{
			return base.Channel.LoadProviderTypeById(Request);
		}

		// Token: 0x06000BF2 RID: 3058 RVA: 0x0001E038 File Offset: 0x0001C238
		public UpdateProviderTypeResp UpdateProviderType(UpdateProviderTypeReq Request)
		{
			return base.Channel.UpdateProviderType(Request);
		}
	}
}
