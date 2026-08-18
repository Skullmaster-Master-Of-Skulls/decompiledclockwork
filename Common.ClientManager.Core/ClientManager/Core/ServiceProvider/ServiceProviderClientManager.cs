using System;
using System.Collections.Generic;
using TechnoPro.ClockWorkServer.Client.ServiceFactory;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.ServiceProvider;
using TechnoPro.ClockWorkServer.Contracts.DTO.ServiceProvider.ContractParameters;
using TechnoPro.Common.ClientManager.ICore;
using TechnoPro.Common.ClientManager.ICore.ServiceProvider;
using TechnoPro.Common.Public;
using TechnoPro.Common.Unity.IoC;

namespace TechnoPro.Common.ClientManager.Core.ServiceProvider
{
	// Token: 0x0200001E RID: 30
	public class ServiceProviderClientManager : IServiceProviderClientManager, IWebService
	{
		// Token: 0x060000E7 RID: 231 RVA: 0x00005B44 File Offset: 0x00003D44
		public SPProviderDTO LoadProviderById(int SPProviderId)
		{
			LoadProviderByIdReq loadProviderByIdReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadProviderByIdReq>();
			loadProviderByIdReq.SPProviderId = SPProviderId;
			return ClientServiceFactory.GetClientInstance<TechnoPro.ClockWorkServer.Contracts.IServiceProvider>().LoadProviderById(loadProviderByIdReq).Provider;
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x00005B7C File Offset: 0x00003D7C
		public SPProviderDTO LoadProviderByStudent_no(string Student_no)
		{
			LoadProviderByStudent_noReq loadProviderByStudent_noReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadProviderByStudent_noReq>();
			loadProviderByStudent_noReq.Student_no = Student_no;
			return ClientServiceFactory.GetClientInstance<TechnoPro.ClockWorkServer.Contracts.IServiceProvider>().LoadProviderByStudent_no(loadProviderByStudent_noReq).Provider;
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x00005BB4 File Offset: 0x00003DB4
		public SPProviderDTO LoadProviderByUserName(string UserName)
		{
			LoadProviderByUserNameReq loadProviderByUserNameReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadProviderByUserNameReq>();
			loadProviderByUserNameReq.UserName = UserName;
			return ClientServiceFactory.GetClientInstance<TechnoPro.ClockWorkServer.Contracts.IServiceProvider>().LoadProviderByUserName(loadProviderByUserNameReq).Provider;
		}

		// Token: 0x060000EA RID: 234 RVA: 0x00005BEC File Offset: 0x00003DEC
		public SPProviderDTO LoadProviderByExternalId(string ExternalId)
		{
			LoadProviderByExternalIdReq loadProviderByExternalIdReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadProviderByExternalIdReq>();
			loadProviderByExternalIdReq.ExternalId = ExternalId;
			return ClientServiceFactory.GetClientInstance<TechnoPro.ClockWorkServer.Contracts.IServiceProvider>().LoadProviderByExternalId(loadProviderByExternalIdReq).Provider;
		}

		// Token: 0x060000EB RID: 235 RVA: 0x00005C24 File Offset: 0x00003E24
		public int CreateProvider(SPProviderDTO Provider)
		{
			CreateProviderReq createProviderReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<CreateProviderReq>();
			createProviderReq.Provider = Provider;
			return ClientServiceFactory.GetClientInstance<TechnoPro.ClockWorkServer.Contracts.IServiceProvider>().CreateProvider(createProviderReq).SPProviderId;
		}

		// Token: 0x060000EC RID: 236 RVA: 0x00005C5C File Offset: 0x00003E5C
		public void UpdateProvider(SPProviderDTO Provider)
		{
			UpdateProviderReq updateProviderReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<UpdateProviderReq>();
			updateProviderReq.Provider = Provider;
			ClientServiceFactory.GetClientInstance<TechnoPro.ClockWorkServer.Contracts.IServiceProvider>().UpdateProvider(updateProviderReq);
		}

		// Token: 0x060000ED RID: 237 RVA: 0x00005C8C File Offset: 0x00003E8C
		public bool DeleteProvider(int SPProviderId)
		{
			DeleteProviderReq deleteProviderReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<DeleteProviderReq>();
			deleteProviderReq.SPProviderId = SPProviderId;
			return ClientServiceFactory.GetClientInstance<TechnoPro.ClockWorkServer.Contracts.IServiceProvider>().DeleteProvider(deleteProviderReq).Worked;
		}

		// Token: 0x060000EE RID: 238 RVA: 0x00005CC4 File Offset: 0x00003EC4
		public int AddProviderCourseRegistration(SPProviderCourseRegistrationDTO CourseRegistration)
		{
			AddProviderCourseRegistrationReq addProviderCourseRegistrationReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<AddProviderCourseRegistrationReq>();
			addProviderCourseRegistrationReq.ProviderCourseRegistration = CourseRegistration;
			return ClientServiceFactory.GetClientInstance<TechnoPro.ClockWorkServer.Contracts.IServiceProvider>().AddProviderCourseRegistration(addProviderCourseRegistrationReq).SPProviderCourseRegistrationId;
		}

		// Token: 0x060000EF RID: 239 RVA: 0x00005CFC File Offset: 0x00003EFC
		public void UpdateProviderCourseRegistration(SPProviderCourseRegistrationDTO CourseRegistration)
		{
			UpdateProviderCourseRegistrationReq updateProviderCourseRegistrationReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<UpdateProviderCourseRegistrationReq>();
			updateProviderCourseRegistrationReq.ProviderCourseRegistration = CourseRegistration;
			ClientServiceFactory.GetClientInstance<TechnoPro.ClockWorkServer.Contracts.IServiceProvider>().UpdateProviderCourseRegistration(updateProviderCourseRegistrationReq);
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x00005D2C File Offset: 0x00003F2C
		public void DeleteProviderCourseRegistration(int SPProviderCourseRegistrationId)
		{
			DeleteProviderCourseRegistrationReq deleteProviderCourseRegistrationReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<DeleteProviderCourseRegistrationReq>();
			deleteProviderCourseRegistrationReq.SPProviderCourseRegistrationId = SPProviderCourseRegistrationId;
			ClientServiceFactory.GetClientInstance<TechnoPro.ClockWorkServer.Contracts.IServiceProvider>().DeleteProviderCourseRegistration(deleteProviderCourseRegistrationReq);
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x00005D5C File Offset: 0x00003F5C
		public IList<SPProviderDTO> LoadAllProvidersWithAtLeastOneActiveApplication(DateTime StartDate, DateTime EndDate)
		{
			LoadAllProvidersWithAtLeastOneActiveApplicationReq loadAllProvidersWithAtLeastOneActiveApplicationReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadAllProvidersWithAtLeastOneActiveApplicationReq>();
			loadAllProvidersWithAtLeastOneActiveApplicationReq.StartDate = StartDate;
			loadAllProvidersWithAtLeastOneActiveApplicationReq.EndDate = EndDate;
			return ClientServiceFactory.GetClientInstance<TechnoPro.ClockWorkServer.Contracts.IServiceProvider>().LoadAllProvidersWithAtLeastOneActiveApplication(loadAllProvidersWithAtLeastOneActiveApplicationReq).Providers;
		}
	}
}
