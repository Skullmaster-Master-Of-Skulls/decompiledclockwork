using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.ServiceProvider.ContractParameters;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x02000129 RID: 297
	public class ServiceProviderReusableClientProxy : WCFTokenBasedReusableClientProxy<TechnoPro.ClockWorkServer.Contracts.IServiceProvider>, TechnoPro.ClockWorkServer.Contracts.IServiceProvider, IService
	{
		// Token: 0x06000BB5 RID: 2997 RVA: 0x0001D7B2 File Offset: 0x0001B9B2
		public ServiceProviderReusableClientProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x06000BB6 RID: 2998 RVA: 0x0001D7BD File Offset: 0x0001B9BD
		public ServiceProviderReusableClientProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x06000BB7 RID: 2999 RVA: 0x0001D7CC File Offset: 0x0001B9CC
		public AddProviderCourseRegistrationResp AddProviderCourseRegistration(AddProviderCourseRegistrationReq Request)
		{
			return this.WrapServiceMethod<AddProviderCourseRegistrationResp>(() => this.Proxy.AddProviderCourseRegistration(Request));
		}

		// Token: 0x06000BB8 RID: 3000 RVA: 0x0001D804 File Offset: 0x0001BA04
		public CreateProviderResp CreateProvider(CreateProviderReq Request)
		{
			return this.WrapServiceMethod<CreateProviderResp>(() => this.Proxy.CreateProvider(Request));
		}

		// Token: 0x06000BB9 RID: 3001 RVA: 0x0001D83C File Offset: 0x0001BA3C
		public DeleteProviderResp DeleteProvider(DeleteProviderReq Request)
		{
			return this.WrapServiceMethod<DeleteProviderResp>(() => this.Proxy.DeleteProvider(Request));
		}

		// Token: 0x06000BBA RID: 3002 RVA: 0x0001D874 File Offset: 0x0001BA74
		public DeleteProviderCourseRegistrationResp DeleteProviderCourseRegistration(DeleteProviderCourseRegistrationReq Request)
		{
			return this.WrapServiceMethod<DeleteProviderCourseRegistrationResp>(() => this.Proxy.DeleteProviderCourseRegistration(Request));
		}

		// Token: 0x06000BBB RID: 3003 RVA: 0x0001D8AC File Offset: 0x0001BAAC
		public LoadAllProvidersWithAtLeastOneActiveApplicationResp LoadAllProvidersWithAtLeastOneActiveApplication(LoadAllProvidersWithAtLeastOneActiveApplicationReq Request)
		{
			return this.WrapServiceMethod<LoadAllProvidersWithAtLeastOneActiveApplicationResp>(() => this.Proxy.LoadAllProvidersWithAtLeastOneActiveApplication(Request));
		}

		// Token: 0x06000BBC RID: 3004 RVA: 0x0001D8E4 File Offset: 0x0001BAE4
		public LoadProviderByExternalIdResp LoadProviderByExternalId(LoadProviderByExternalIdReq Request)
		{
			return this.WrapServiceMethod<LoadProviderByExternalIdResp>(() => this.Proxy.LoadProviderByExternalId(Request));
		}

		// Token: 0x06000BBD RID: 3005 RVA: 0x0001D91C File Offset: 0x0001BB1C
		public LoadProviderByIdResp LoadProviderById(LoadProviderByIdReq Request)
		{
			return this.WrapServiceMethod<LoadProviderByIdResp>(() => this.Proxy.LoadProviderById(Request));
		}

		// Token: 0x06000BBE RID: 3006 RVA: 0x0001D954 File Offset: 0x0001BB54
		public LoadProviderByStudent_noResp LoadProviderByStudent_no(LoadProviderByStudent_noReq Request)
		{
			return this.WrapServiceMethod<LoadProviderByStudent_noResp>(() => this.Proxy.LoadProviderByStudent_no(Request));
		}

		// Token: 0x06000BBF RID: 3007 RVA: 0x0001D98C File Offset: 0x0001BB8C
		public LoadProviderByUserNameResp LoadProviderByUserName(LoadProviderByUserNameReq Request)
		{
			return this.WrapServiceMethod<LoadProviderByUserNameResp>(() => this.Proxy.LoadProviderByUserName(Request));
		}

		// Token: 0x06000BC0 RID: 3008 RVA: 0x0001D9C4 File Offset: 0x0001BBC4
		public UpdateProviderResp UpdateProvider(UpdateProviderReq Request)
		{
			return this.WrapServiceMethod<UpdateProviderResp>(() => this.Proxy.UpdateProvider(Request));
		}

		// Token: 0x06000BC1 RID: 3009 RVA: 0x0001D9FC File Offset: 0x0001BBFC
		public UpdateProviderCourseRegistrationResp UpdateProviderCourseRegistration(UpdateProviderCourseRegistrationReq Request)
		{
			return this.WrapServiceMethod<UpdateProviderCourseRegistrationResp>(() => this.Proxy.UpdateProviderCourseRegistration(Request));
		}
	}
}
