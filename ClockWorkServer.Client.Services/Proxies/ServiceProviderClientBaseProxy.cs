using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.ServiceProvider.ContractParameters;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x0200012A RID: 298
	internal class ServiceProviderClientBaseProxy : ClientBase<TechnoPro.ClockWorkServer.Contracts.IServiceProvider>, TechnoPro.ClockWorkServer.Contracts.IServiceProvider, IService
	{
		// Token: 0x06000BC2 RID: 3010 RVA: 0x0001DA34 File Offset: 0x0001BC34
		public ServiceProviderClientBaseProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x06000BC3 RID: 3011 RVA: 0x0001DA3F File Offset: 0x0001BC3F
		public ServiceProviderClientBaseProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x06000BC4 RID: 3012 RVA: 0x0001DA4C File Offset: 0x0001BC4C
		public AddProviderCourseRegistrationResp AddProviderCourseRegistration(AddProviderCourseRegistrationReq Request)
		{
			return base.Channel.AddProviderCourseRegistration(Request);
		}

		// Token: 0x06000BC5 RID: 3013 RVA: 0x0001DA6C File Offset: 0x0001BC6C
		public CreateProviderResp CreateProvider(CreateProviderReq Request)
		{
			return base.Channel.CreateProvider(Request);
		}

		// Token: 0x06000BC6 RID: 3014 RVA: 0x0001DA8C File Offset: 0x0001BC8C
		public DeleteProviderResp DeleteProvider(DeleteProviderReq Request)
		{
			return base.Channel.DeleteProvider(Request);
		}

		// Token: 0x06000BC7 RID: 3015 RVA: 0x0001DAAC File Offset: 0x0001BCAC
		public DeleteProviderCourseRegistrationResp DeleteProviderCourseRegistration(DeleteProviderCourseRegistrationReq Request)
		{
			return base.Channel.DeleteProviderCourseRegistration(Request);
		}

		// Token: 0x06000BC8 RID: 3016 RVA: 0x0001DACC File Offset: 0x0001BCCC
		public LoadAllProvidersWithAtLeastOneActiveApplicationResp LoadAllProvidersWithAtLeastOneActiveApplication(LoadAllProvidersWithAtLeastOneActiveApplicationReq Request)
		{
			return base.Channel.LoadAllProvidersWithAtLeastOneActiveApplication(Request);
		}

		// Token: 0x06000BC9 RID: 3017 RVA: 0x0001DAEC File Offset: 0x0001BCEC
		public LoadProviderByExternalIdResp LoadProviderByExternalId(LoadProviderByExternalIdReq Request)
		{
			return base.Channel.LoadProviderByExternalId(Request);
		}

		// Token: 0x06000BCA RID: 3018 RVA: 0x0001DB0C File Offset: 0x0001BD0C
		public LoadProviderByIdResp LoadProviderById(LoadProviderByIdReq Request)
		{
			return base.Channel.LoadProviderById(Request);
		}

		// Token: 0x06000BCB RID: 3019 RVA: 0x0001DB2C File Offset: 0x0001BD2C
		public LoadProviderByStudent_noResp LoadProviderByStudent_no(LoadProviderByStudent_noReq Request)
		{
			return base.Channel.LoadProviderByStudent_no(Request);
		}

		// Token: 0x06000BCC RID: 3020 RVA: 0x0001DB4C File Offset: 0x0001BD4C
		public LoadProviderByUserNameResp LoadProviderByUserName(LoadProviderByUserNameReq Request)
		{
			return base.Channel.LoadProviderByUserName(Request);
		}

		// Token: 0x06000BCD RID: 3021 RVA: 0x0001DB6C File Offset: 0x0001BD6C
		public UpdateProviderResp UpdateProvider(UpdateProviderReq Request)
		{
			return base.Channel.UpdateProvider(Request);
		}

		// Token: 0x06000BCE RID: 3022 RVA: 0x0001DB8C File Offset: 0x0001BD8C
		public UpdateProviderCourseRegistrationResp UpdateProviderCourseRegistration(UpdateProviderCourseRegistrationReq Request)
		{
			return base.Channel.UpdateProviderCourseRegistration(Request);
		}
	}
}
