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
	// Token: 0x02000082 RID: 130
	public class ServiceProviderApplicationServiceManager : IServiceProviderApplication, IService
	{
		// Token: 0x060004C3 RID: 1219 RVA: 0x00016A00 File Offset: 0x00014C00
		public int CheckConnectivity()
		{
			return 1;
		}

		// Token: 0x060004C4 RID: 1220 RVA: 0x00016A14 File Offset: 0x00014C14
		public LoadApplicationByProviderAndTypeResp LoadApplicationByProviderAndType(LoadApplicationByProviderAndTypeReq Request)
		{
			IServiceProviderApplicationManager serviceProviderApplicationManager = new ServiceProviderApplicationManager(Request.GetOperationContext());
			SPApplication spapplication = serviceProviderApplicationManager.LoadApplicationByProviderAndType(Request.SPProviderId, Request.SPProviderTypeId);
			return new LoadApplicationByProviderAndTypeResp
			{
				Application = ((spapplication == null) ? null : spapplication.ToDTO())
			};
		}

		// Token: 0x060004C5 RID: 1221 RVA: 0x00016A60 File Offset: 0x00014C60
		public CreateApplicationResp CreateApplication(CreateApplicationReq Request)
		{
			IServiceProviderApplicationManager serviceProviderApplicationManager = new ServiceProviderApplicationManager(Request.GetOperationContext());
			int spapplicationId = serviceProviderApplicationManager.CreateApplication(Request.Application.ToDomainObject());
			return new CreateApplicationResp
			{
				SPApplicationId = spapplicationId
			};
		}

		// Token: 0x060004C6 RID: 1222 RVA: 0x00016AA0 File Offset: 0x00014CA0
		public UpdateApplicationResp UpdateApplication(UpdateApplicationReq Request)
		{
			IServiceProviderApplicationManager serviceProviderApplicationManager = new ServiceProviderApplicationManager(Request.GetOperationContext());
			serviceProviderApplicationManager.UpdateApplication(Request.Application.ToDomainObject());
			return new UpdateApplicationResp();
		}

		// Token: 0x060004C7 RID: 1223 RVA: 0x00016AD8 File Offset: 0x00014CD8
		public DeleteApplicationResp DeleteApplication(DeleteApplicationReq Request)
		{
			IServiceProviderApplicationManager serviceProviderApplicationManager = new ServiceProviderApplicationManager(Request.GetOperationContext());
			bool worked = serviceProviderApplicationManager.DeleteApplication(Request.SPApplicationId);
			return new DeleteApplicationResp
			{
				Worked = worked
			};
		}

		// Token: 0x060004C8 RID: 1224 RVA: 0x00016B10 File Offset: 0x00014D10
		public UpdateApplicationAvailabilityTypeResp UpdateApplicationAvailabilityType(UpdateApplicationAvailabilityTypeReq Request)
		{
			IServiceProviderApplicationManager serviceProviderApplicationManager = new ServiceProviderApplicationManager(Request.GetOperationContext());
			serviceProviderApplicationManager.UpdateApplicationAvailabilityType(Request.SPApplicationId, Request.ApplicationAvailabilityType.ToDomainObject());
			return new UpdateApplicationAvailabilityTypeResp();
		}

		// Token: 0x060004C9 RID: 1225 RVA: 0x00016B4C File Offset: 0x00014D4C
		public LoadApplicationsBySPProviderTypeResp LoadApplicationsBySPProviderType(LoadApplicationsBySPProviderTypeReq Request)
		{
			IServiceProviderApplicationManager serviceProviderApplicationManager = new ServiceProviderApplicationManager(Request.GetOperationContext());
			IList<SPApplication> list = serviceProviderApplicationManager.LoadApplicationsBySPProviderType(Request.SPProviderTypeId, Request.StartDate, Request.EndDate, Request.IncludeInactiveApplications);
			return new LoadApplicationsBySPProviderTypeResp
			{
				Applications = ((list == null) ? null : list.ToDTO())
			};
		}

		// Token: 0x060004CA RID: 1226 RVA: 0x00016BA4 File Offset: 0x00014DA4
		public LoadApplicationsBySPProviderResp LoadApplicationsBySPProvider(LoadApplicationsBySPProviderReq Request)
		{
			IServiceProviderApplicationManager serviceProviderApplicationManager = new ServiceProviderApplicationManager(Request.GetOperationContext());
			IList<SPApplication> list = serviceProviderApplicationManager.LoadApplicationsBySPProvider(Request.SPProviderId, Request.StartDate, Request.EndDate, Request.IncludeInactiveApplications);
			LoadApplicationsBySPProviderResp loadApplicationsBySPProviderResp = new LoadApplicationsBySPProviderResp();
			IList<SPApplicationDTO> applications;
			if (list != null)
			{
				applications = list.ToList<SPApplication>().ConvertAll<SPApplicationDTO>((SPApplication f) => f.ToDTO());
			}
			else
			{
				applications = null;
			}
			loadApplicationsBySPProviderResp.Applications = applications;
			return loadApplicationsBySPProviderResp;
		}

		// Token: 0x060004CB RID: 1227 RVA: 0x00016C20 File Offset: 0x00014E20
		public LoadApplicationByIdResp LoadApplicationById(LoadApplicationByIdReq Request)
		{
			IServiceProviderApplicationManager serviceProviderApplicationManager = new ServiceProviderApplicationManager(Request.GetOperationContext());
			SPApplication spapplication = serviceProviderApplicationManager.LoadApplicationById(Request.SPApplicationId);
			return new LoadApplicationByIdResp
			{
				Application = ((spapplication == null) ? null : spapplication.ToDTO())
			};
		}
	}
}
