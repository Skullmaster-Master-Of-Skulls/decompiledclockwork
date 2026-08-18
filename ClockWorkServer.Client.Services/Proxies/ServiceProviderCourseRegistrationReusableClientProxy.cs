using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.ServiceProvider.ContractParameters;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x0200012B RID: 299
	public class ServiceProviderCourseRegistrationReusableClientProxy : WCFTokenBasedReusableClientProxy<IServiceProviderCourseRegistration>, IServiceProviderCourseRegistration, IService
	{
		// Token: 0x06000BCF RID: 3023 RVA: 0x0001DBAA File Offset: 0x0001BDAA
		public ServiceProviderCourseRegistrationReusableClientProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x06000BD0 RID: 3024 RVA: 0x0001DBB5 File Offset: 0x0001BDB5
		public ServiceProviderCourseRegistrationReusableClientProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x06000BD1 RID: 3025 RVA: 0x0001DBC4 File Offset: 0x0001BDC4
		public CreateCourseRegistrationResp CreateCourseRegistration(CreateCourseRegistrationReq Request)
		{
			return this.WrapServiceMethod<CreateCourseRegistrationResp>(() => this.Proxy.CreateCourseRegistration(Request));
		}

		// Token: 0x06000BD2 RID: 3026 RVA: 0x0001DBFC File Offset: 0x0001BDFC
		public DeleteCourseRegistrationResp DeleteCourseRegistration(DeleteCourseRegistrationReq Request)
		{
			return this.WrapServiceMethod<DeleteCourseRegistrationResp>(() => this.Proxy.DeleteCourseRegistration(Request));
		}

		// Token: 0x06000BD3 RID: 3027 RVA: 0x0001DC34 File Offset: 0x0001BE34
		public LoadCourseRegistrationByIdResp LoadCourseRegistrationById(LoadCourseRegistrationByIdReq Request)
		{
			return this.WrapServiceMethod<LoadCourseRegistrationByIdResp>(() => this.Proxy.LoadCourseRegistrationById(Request));
		}

		// Token: 0x06000BD4 RID: 3028 RVA: 0x0001DC6C File Offset: 0x0001BE6C
		public LoadCourseRegistrationsByProviderResp LoadCourseRegistrationsByProvider(LoadCourseRegistrationsByProviderReq Request)
		{
			return this.WrapServiceMethod<LoadCourseRegistrationsByProviderResp>(() => this.Proxy.LoadCourseRegistrationsByProvider(Request));
		}

		// Token: 0x06000BD5 RID: 3029 RVA: 0x0001DCA4 File Offset: 0x0001BEA4
		public UpdateCourseRegistrationResp UpdateCourseRegistration(UpdateCourseRegistrationReq Request)
		{
			return this.WrapServiceMethod<UpdateCourseRegistrationResp>(() => this.Proxy.UpdateCourseRegistration(Request));
		}

		// Token: 0x06000BD6 RID: 3030 RVA: 0x0001DCDC File Offset: 0x0001BEDC
		public UpdateCourseRegistrationStatusResp UpdateCourseRegistrationStatus(UpdateCourseRegistrationStatusReq Request)
		{
			return this.WrapServiceMethod<UpdateCourseRegistrationStatusResp>(() => this.Proxy.UpdateCourseRegistrationStatus(Request));
		}
	}
}
