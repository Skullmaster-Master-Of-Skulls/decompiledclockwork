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
	// Token: 0x0200001D RID: 29
	public class ServiceProviderApplicationClientManager : IServiceProviderApplicationClientManager, IWebService
	{
		// Token: 0x060000DE RID: 222 RVA: 0x00005954 File Offset: 0x00003B54
		public SPApplicationDTO LoadApplicationById(int SPApplicationId)
		{
			LoadApplicationByIdReq loadApplicationByIdReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadApplicationByIdReq>();
			loadApplicationByIdReq.SPApplicationId = SPApplicationId;
			return ClientServiceFactory.GetClientInstance<IServiceProviderApplication>().LoadApplicationById(loadApplicationByIdReq).Application;
		}

		// Token: 0x060000DF RID: 223 RVA: 0x0000598C File Offset: 0x00003B8C
		public SPApplicationDTO LoadApplicationByProviderAndType(int SPProviderId, int SPProviderTypeId)
		{
			LoadApplicationByProviderAndTypeReq loadApplicationByProviderAndTypeReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadApplicationByProviderAndTypeReq>();
			loadApplicationByProviderAndTypeReq.SPProviderId = SPProviderId;
			loadApplicationByProviderAndTypeReq.SPProviderTypeId = SPProviderTypeId;
			return ClientServiceFactory.GetClientInstance<IServiceProviderApplication>().LoadApplicationByProviderAndType(loadApplicationByProviderAndTypeReq).Application;
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x000059CC File Offset: 0x00003BCC
		public int CreateApplication(SPApplicationDTO Application)
		{
			CreateApplicationReq createApplicationReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<CreateApplicationReq>();
			createApplicationReq.Application = Application;
			return ClientServiceFactory.GetClientInstance<IServiceProviderApplication>().CreateApplication(createApplicationReq).SPApplicationId;
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x00005A04 File Offset: 0x00003C04
		public void UpdateApplication(SPApplicationDTO Application)
		{
			UpdateApplicationReq updateApplicationReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<UpdateApplicationReq>();
			updateApplicationReq.Application = Application;
			ClientServiceFactory.GetClientInstance<IServiceProviderApplication>().UpdateApplication(updateApplicationReq);
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x00005A34 File Offset: 0x00003C34
		public bool DeleteApplication(int SPApplicationId)
		{
			DeleteApplicationReq deleteApplicationReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<DeleteApplicationReq>();
			deleteApplicationReq.SPApplicationId = SPApplicationId;
			return ClientServiceFactory.GetClientInstance<IServiceProviderApplication>().DeleteApplication(deleteApplicationReq).Worked;
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x00005A6C File Offset: 0x00003C6C
		public void UpdateApplicationAvailabilityType(int SPApplicationId, SPApplicationAvailabilityTypeDTO NewAvailabilityType)
		{
			UpdateApplicationAvailabilityTypeReq updateApplicationAvailabilityTypeReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<UpdateApplicationAvailabilityTypeReq>();
			updateApplicationAvailabilityTypeReq.SPApplicationId = SPApplicationId;
			updateApplicationAvailabilityTypeReq.ApplicationAvailabilityType = NewAvailabilityType;
			ClientServiceFactory.GetClientInstance<IServiceProviderApplication>().UpdateApplicationAvailabilityType(updateApplicationAvailabilityTypeReq);
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x00005AA4 File Offset: 0x00003CA4
		public IList<SPApplicationDTO> LoadApplicationsBySPProviderType(int SPProviderTypeId, DateTime StartDate, DateTime EndDate, bool IncludeInactiveApplications)
		{
			LoadApplicationsBySPProviderTypeReq loadApplicationsBySPProviderTypeReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadApplicationsBySPProviderTypeReq>();
			loadApplicationsBySPProviderTypeReq.SPProviderTypeId = SPProviderTypeId;
			loadApplicationsBySPProviderTypeReq.StartDate = StartDate;
			loadApplicationsBySPProviderTypeReq.EndDate = EndDate;
			loadApplicationsBySPProviderTypeReq.IncludeInactiveApplications = IncludeInactiveApplications;
			return ClientServiceFactory.GetClientInstance<IServiceProviderApplication>().LoadApplicationsBySPProviderType(loadApplicationsBySPProviderTypeReq).Applications;
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x00005AF4 File Offset: 0x00003CF4
		public IList<SPApplicationDTO> LoadApplicationsBySPProvider(int SPProviderId, DateTime StartDate, DateTime EndDate, bool IncludeInactiveApplications)
		{
			LoadApplicationsBySPProviderReq loadApplicationsBySPProviderReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadApplicationsBySPProviderReq>();
			loadApplicationsBySPProviderReq.SPProviderId = SPProviderId;
			loadApplicationsBySPProviderReq.StartDate = StartDate;
			loadApplicationsBySPProviderReq.EndDate = EndDate;
			loadApplicationsBySPProviderReq.IncludeInactiveApplications = IncludeInactiveApplications;
			return ClientServiceFactory.GetClientInstance<IServiceProviderApplication>().LoadApplicationsBySPProvider(loadApplicationsBySPProviderReq).Applications;
		}
	}
}
