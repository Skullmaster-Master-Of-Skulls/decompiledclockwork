using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.ServiceProvider.ContractParameters;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x0200012F RID: 303
	public class ServiceProviderTypeReusableClientProxy : WCFTokenBasedReusableClientProxy<IServiceProviderType>, IServiceProviderType, IService
	{
		// Token: 0x06000BE3 RID: 3043 RVA: 0x0001DE18 File Offset: 0x0001C018
		public ServiceProviderTypeReusableClientProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x06000BE4 RID: 3044 RVA: 0x0001DE23 File Offset: 0x0001C023
		public ServiceProviderTypeReusableClientProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x06000BE5 RID: 3045 RVA: 0x0001DE30 File Offset: 0x0001C030
		public CreateProviderTypeResp CreateProviderType(CreateProviderTypeReq Request)
		{
			return this.WrapServiceMethod<CreateProviderTypeResp>(() => this.Proxy.CreateProviderType(Request));
		}

		// Token: 0x06000BE6 RID: 3046 RVA: 0x0001DE68 File Offset: 0x0001C068
		public DeleteProviderTypeResp DeleteProviderType(DeleteProviderTypeReq Request)
		{
			return this.WrapServiceMethod<DeleteProviderTypeResp>(() => this.Proxy.DeleteProviderType(Request));
		}

		// Token: 0x06000BE7 RID: 3047 RVA: 0x0001DEA0 File Offset: 0x0001C0A0
		public LoadAllProviderTypesResp LoadAllProviderTypes(LoadAllProviderTypesReq Request)
		{
			return this.WrapServiceMethod<LoadAllProviderTypesResp>(() => this.Proxy.LoadAllProviderTypes(Request));
		}

		// Token: 0x06000BE8 RID: 3048 RVA: 0x0001DED8 File Offset: 0x0001C0D8
		public LoadProviderTypeByBehaviourCodeResp LoadProviderTypeByBehaviourCode(LoadProviderTypeByBehaviourCodeReq Request)
		{
			return this.WrapServiceMethod<LoadProviderTypeByBehaviourCodeResp>(() => this.Proxy.LoadProviderTypeByBehaviourCode(Request));
		}

		// Token: 0x06000BE9 RID: 3049 RVA: 0x0001DF10 File Offset: 0x0001C110
		public LoadProviderTypeByIdResp LoadProviderTypeById(LoadProviderTypeByIdReq Request)
		{
			return this.WrapServiceMethod<LoadProviderTypeByIdResp>(() => this.Proxy.LoadProviderTypeById(Request));
		}

		// Token: 0x06000BEA RID: 3050 RVA: 0x0001DF48 File Offset: 0x0001C148
		public UpdateProviderTypeResp UpdateProviderType(UpdateProviderTypeReq Request)
		{
			return this.WrapServiceMethod<UpdateProviderTypeResp>(() => this.Proxy.UpdateProviderType(Request));
		}
	}
}
