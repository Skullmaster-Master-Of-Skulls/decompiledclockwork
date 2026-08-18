using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.ServiceProvider.ContractParameters;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x0200012C RID: 300
	internal class ServiceProviderCourseRegistrationClientBaseProxy : ClientBase<IServiceProviderCourseRegistration>, IServiceProviderCourseRegistration, IService
	{
		// Token: 0x06000BD7 RID: 3031 RVA: 0x0001DD14 File Offset: 0x0001BF14
		public ServiceProviderCourseRegistrationClientBaseProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x06000BD8 RID: 3032 RVA: 0x0001DD1F File Offset: 0x0001BF1F
		public ServiceProviderCourseRegistrationClientBaseProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x06000BD9 RID: 3033 RVA: 0x0001DD2C File Offset: 0x0001BF2C
		public CreateCourseRegistrationResp CreateCourseRegistration(CreateCourseRegistrationReq Request)
		{
			return base.Channel.CreateCourseRegistration(Request);
		}

		// Token: 0x06000BDA RID: 3034 RVA: 0x0001DD4C File Offset: 0x0001BF4C
		public DeleteCourseRegistrationResp DeleteCourseRegistration(DeleteCourseRegistrationReq Request)
		{
			return base.Channel.DeleteCourseRegistration(Request);
		}

		// Token: 0x06000BDB RID: 3035 RVA: 0x0001DD6C File Offset: 0x0001BF6C
		public LoadCourseRegistrationByIdResp LoadCourseRegistrationById(LoadCourseRegistrationByIdReq Request)
		{
			return base.Channel.LoadCourseRegistrationById(Request);
		}

		// Token: 0x06000BDC RID: 3036 RVA: 0x0001DD8C File Offset: 0x0001BF8C
		public LoadCourseRegistrationsByProviderResp LoadCourseRegistrationsByProvider(LoadCourseRegistrationsByProviderReq Request)
		{
			return base.Channel.LoadCourseRegistrationsByProvider(Request);
		}

		// Token: 0x06000BDD RID: 3037 RVA: 0x0001DDAC File Offset: 0x0001BFAC
		public UpdateCourseRegistrationResp UpdateCourseRegistration(UpdateCourseRegistrationReq Request)
		{
			return base.Channel.UpdateCourseRegistration(Request);
		}

		// Token: 0x06000BDE RID: 3038 RVA: 0x0001DDCC File Offset: 0x0001BFCC
		public UpdateCourseRegistrationStatusResp UpdateCourseRegistrationStatus(UpdateCourseRegistrationStatusReq Request)
		{
			return base.Channel.UpdateCourseRegistrationStatus(Request);
		}
	}
}
