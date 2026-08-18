using System;
using System.Collections.Generic;
using TechnoPro.ClockWorkServer.Client.ServiceFactory;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.CourseRegistrations;
using TechnoPro.ClockWorkServer.Contracts.DTO.ServiceProvider;
using TechnoPro.ClockWorkServer.Contracts.DTO.ServiceProvider.ContractParameters;
using TechnoPro.Common.ClientManager.ICore;
using TechnoPro.Common.ClientManager.ICore.ServiceProvider;
using TechnoPro.Common.Public;
using TechnoPro.Common.Unity.IoC;

namespace TechnoPro.Common.ClientManager.Core.ServiceProvider
{
	// Token: 0x0200001F RID: 31
	public class ServiceProviderCourseRegistrationClientManager : IServiceProviderCourseRegistrationClientManager, IWebService
	{
		// Token: 0x060000F3 RID: 243 RVA: 0x00005D9C File Offset: 0x00003F9C
		public IList<SPProviderCourseRegistrationDTO> LoadCourseRegistrationsByProvider(int SPProviderId, DateTime StartDate, DateTime EndDate)
		{
			LoadCourseRegistrationsByProviderReq loadCourseRegistrationsByProviderReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadCourseRegistrationsByProviderReq>();
			loadCourseRegistrationsByProviderReq.SPProviderId = SPProviderId;
			loadCourseRegistrationsByProviderReq.StartDate = StartDate;
			loadCourseRegistrationsByProviderReq.EndDate = EndDate;
			return ClientServiceFactory.GetClientInstance<IServiceProviderCourseRegistration>().LoadCourseRegistrationsByProvider(loadCourseRegistrationsByProviderReq).CourseRegistrations;
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x00005DE4 File Offset: 0x00003FE4
		public SPProviderCourseRegistrationDTO LoadCourseRegistrationById(int SPProviderCourseRegistrationId)
		{
			LoadCourseRegistrationByIdReq loadCourseRegistrationByIdReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadCourseRegistrationByIdReq>();
			loadCourseRegistrationByIdReq.SPProviderCourseRegistrationId = SPProviderCourseRegistrationId;
			return ClientServiceFactory.GetClientInstance<IServiceProviderCourseRegistration>().LoadCourseRegistrationById(loadCourseRegistrationByIdReq).CourseRegistration;
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x00005E1C File Offset: 0x0000401C
		public void UpdateCourseRegistrationStatus(int SPProviderCourseRegistrationId, CourseRegistrationStatusDTO NewStatus)
		{
			UpdateCourseRegistrationStatusReq updateCourseRegistrationStatusReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<UpdateCourseRegistrationStatusReq>();
			updateCourseRegistrationStatusReq.SPProviderCourseRegistrationId = SPProviderCourseRegistrationId;
			updateCourseRegistrationStatusReq.NewCourseRegistrationStatus = NewStatus;
			ClientServiceFactory.GetClientInstance<IServiceProviderCourseRegistration>().UpdateCourseRegistrationStatus(updateCourseRegistrationStatusReq);
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x00005E54 File Offset: 0x00004054
		public void UpdateCourseRegistration(SPProviderCourseRegistrationDTO ProviderCourseRegistration)
		{
			UpdateCourseRegistrationReq updateCourseRegistrationReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<UpdateCourseRegistrationReq>();
			updateCourseRegistrationReq.CourseRegistration = ProviderCourseRegistration;
			ClientServiceFactory.GetClientInstance<IServiceProviderCourseRegistration>().UpdateCourseRegistration(updateCourseRegistrationReq);
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x00005E84 File Offset: 0x00004084
		public void DeleteCourseRegistration(int SPProviderCourseRegistrationId)
		{
			TechnoPro.ClockWorkServer.Contracts.DTO.ServiceProvider.ContractParameters.DeleteCourseRegistrationReq deleteCourseRegistrationReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<TechnoPro.ClockWorkServer.Contracts.DTO.ServiceProvider.ContractParameters.DeleteCourseRegistrationReq>();
			deleteCourseRegistrationReq.SPProviderCourseRegistrationId = SPProviderCourseRegistrationId;
			ClientServiceFactory.GetClientInstance<IServiceProviderCourseRegistration>().DeleteCourseRegistration(deleteCourseRegistrationReq);
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x00005EB4 File Offset: 0x000040B4
		public int CreateCourseRegistration(SPProviderCourseRegistrationDTO ProviderCourseRegistration)
		{
			CreateCourseRegistrationReq createCourseRegistrationReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<CreateCourseRegistrationReq>();
			createCourseRegistrationReq.CourseRegistration = ProviderCourseRegistration;
			return ClientServiceFactory.GetClientInstance<IServiceProviderCourseRegistration>().CreateCourseRegistration(createCourseRegistrationReq).SPProviderCourseRegistrationID;
		}
	}
}
