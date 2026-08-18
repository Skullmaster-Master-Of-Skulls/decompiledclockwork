using System;
using System.Collections.Generic;
using System.Linq;
using TechnoPro.ClockWorkServer.Common.Services.Impl.Adapters;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.ServiceProvider;
using TechnoPro.ClockWorkServer.Contracts.DTO.ServiceProvider.ContractParameters;
using TechnoPro.Common.Core.Mappers.ServiceProvider;
using TechnoPro.Common.Core.ServiceProvider;
using TechnoPro.Common.ICore.ServiceProviders;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities.ServiceProvider;

namespace TechnoPro.ClockWorkServer.Common.Services.Impl
{
	// Token: 0x02000085 RID: 133
	public class ServiceProviderServiceManager : TechnoPro.ClockWorkServer.Contracts.IServiceProvider, IService
	{
		// Token: 0x060004D7 RID: 1239 RVA: 0x00016E28 File Offset: 0x00015028
		public int CheckConnectivity()
		{
			return 1;
		}

		// Token: 0x060004D8 RID: 1240 RVA: 0x00016E3C File Offset: 0x0001503C
		public LoadProviderByIdResp LoadProviderById(LoadProviderByIdReq Request)
		{
			IServiceProviderManager serviceProviderManager = new ServiceProviderManager(Request.GetOperationContext());
			SPProvider spprovider = serviceProviderManager.LoadProviderById(Request.SPProviderId);
			return new LoadProviderByIdResp
			{
				Provider = ((spprovider == null) ? null : spprovider.ToDTO())
			};
		}

		// Token: 0x060004D9 RID: 1241 RVA: 0x00016E80 File Offset: 0x00015080
		public LoadProviderByStudent_noResp LoadProviderByStudent_no(LoadProviderByStudent_noReq Request)
		{
			IServiceProviderManager serviceProviderManager = new ServiceProviderManager(Request.GetOperationContext());
			SPProvider spprovider = serviceProviderManager.LoadProviderByStudent_no(Request.Student_no);
			return new LoadProviderByStudent_noResp
			{
				Provider = ((spprovider == null) ? null : spprovider.ToDTO())
			};
		}

		// Token: 0x060004DA RID: 1242 RVA: 0x00016EC4 File Offset: 0x000150C4
		public LoadProviderByUserNameResp LoadProviderByUserName(LoadProviderByUserNameReq Request)
		{
			IServiceProviderManager serviceProviderManager = new ServiceProviderManager(Request.GetOperationContext());
			SPProvider spprovider = serviceProviderManager.LoadProviderByUserName(Request.UserName);
			return new LoadProviderByUserNameResp
			{
				Provider = ((spprovider == null) ? null : spprovider.ToDTO())
			};
		}

		// Token: 0x060004DB RID: 1243 RVA: 0x00016F08 File Offset: 0x00015108
		public LoadProviderByExternalIdResp LoadProviderByExternalId(LoadProviderByExternalIdReq Request)
		{
			IServiceProviderManager serviceProviderManager = new ServiceProviderManager(Request.GetOperationContext());
			SPProvider spprovider = serviceProviderManager.LoadProviderByExternalId(Request.ExternalId);
			return new LoadProviderByExternalIdResp
			{
				Provider = ((spprovider == null) ? null : spprovider.ToDTO())
			};
		}

		// Token: 0x060004DC RID: 1244 RVA: 0x00016F4C File Offset: 0x0001514C
		public CreateProviderResp CreateProvider(CreateProviderReq Request)
		{
			IServiceProviderManager serviceProviderManager = new ServiceProviderManager(Request.GetOperationContext());
			int spproviderId = serviceProviderManager.CreateProvider(Request.Provider.ToDomainObject());
			return new CreateProviderResp
			{
				SPProviderId = spproviderId
			};
		}

		// Token: 0x060004DD RID: 1245 RVA: 0x00016F8C File Offset: 0x0001518C
		public UpdateProviderResp UpdateProvider(UpdateProviderReq Request)
		{
			IServiceProviderManager serviceProviderManager = new ServiceProviderManager(Request.GetOperationContext());
			serviceProviderManager.UpdateProvider(Request.Provider.ToDomainObject());
			return new UpdateProviderResp();
		}

		// Token: 0x060004DE RID: 1246 RVA: 0x00016FC4 File Offset: 0x000151C4
		public DeleteProviderResp DeleteProvider(DeleteProviderReq Request)
		{
			IServiceProviderManager serviceProviderManager = new ServiceProviderManager(Request.GetOperationContext());
			serviceProviderManager.DeleteProvider(Request.SPProviderId);
			return new DeleteProviderResp();
		}

		// Token: 0x060004DF RID: 1247 RVA: 0x00016FF4 File Offset: 0x000151F4
		public AddProviderCourseRegistrationResp AddProviderCourseRegistration(AddProviderCourseRegistrationReq Request)
		{
			IServiceProviderManager serviceProviderManager = new ServiceProviderManager(Request.GetOperationContext());
			int spproviderCourseRegistrationId = serviceProviderManager.AddProviderCourseRegistration(Request.ProviderCourseRegistration.ToDomainObject());
			return new AddProviderCourseRegistrationResp
			{
				SPProviderCourseRegistrationId = spproviderCourseRegistrationId
			};
		}

		// Token: 0x060004E0 RID: 1248 RVA: 0x00017034 File Offset: 0x00015234
		public UpdateProviderCourseRegistrationResp UpdateProviderCourseRegistration(UpdateProviderCourseRegistrationReq Request)
		{
			IServiceProviderManager serviceProviderManager = new ServiceProviderManager(Request.GetOperationContext());
			serviceProviderManager.UpdateProviderCourseRegistration(Request.ProviderCourseRegistration.ToDomainObject());
			return new UpdateProviderCourseRegistrationResp();
		}

		// Token: 0x060004E1 RID: 1249 RVA: 0x0001706C File Offset: 0x0001526C
		public DeleteProviderCourseRegistrationResp DeleteProviderCourseRegistration(DeleteProviderCourseRegistrationReq Request)
		{
			IServiceProviderManager serviceProviderManager = new ServiceProviderManager(Request.GetOperationContext());
			serviceProviderManager.DeleteProviderCourseRegistration(Request.SPProviderCourseRegistrationId);
			return new DeleteProviderCourseRegistrationResp();
		}

		// Token: 0x060004E2 RID: 1250 RVA: 0x0001709C File Offset: 0x0001529C
		public LoadAllProvidersWithAtLeastOneActiveApplicationResp LoadAllProvidersWithAtLeastOneActiveApplication(LoadAllProvidersWithAtLeastOneActiveApplicationReq Request)
		{
			IServiceProviderManager serviceProviderManager = new ServiceProviderManager(Request.GetOperationContext());
			IList<SPProvider> list = serviceProviderManager.LoadAllProvidersWithAtLeastOneActiveApplication(Request.StartDate, Request.EndDate);
			LoadAllProvidersWithAtLeastOneActiveApplicationResp loadAllProvidersWithAtLeastOneActiveApplicationResp = new LoadAllProvidersWithAtLeastOneActiveApplicationResp();
			IList<SPProviderDTO> providers;
			if (list != null)
			{
				providers = list.ToList<SPProvider>().ConvertAll<SPProviderDTO>((SPProvider f) => f.ToDTO());
			}
			else
			{
				providers = null;
			}
			loadAllProvidersWithAtLeastOneActiveApplicationResp.Providers = providers;
			return loadAllProvidersWithAtLeastOneActiveApplicationResp;
		}
	}
}
