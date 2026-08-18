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
	// Token: 0x02000086 RID: 134
	public class ServiceProviderTypeServiceManager : IServiceProviderType, IService
	{
		// Token: 0x060004E4 RID: 1252 RVA: 0x0001710C File Offset: 0x0001530C
		public int CheckConnectivity()
		{
			return 1;
		}

		// Token: 0x060004E5 RID: 1253 RVA: 0x00017120 File Offset: 0x00015320
		public LoadProviderTypeByIdResp LoadProviderTypeById(LoadProviderTypeByIdReq Request)
		{
			IServiceProviderTypeManager serviceProviderTypeManager = new ServiceProviderTypeManager(Request.GetOperationContext());
			SPProviderType spproviderType = serviceProviderTypeManager.LoadProviderTypeById(Request.SPProviderTypeId);
			return new LoadProviderTypeByIdResp
			{
				ProviderType = ((spproviderType == null) ? null : spproviderType.ToDTO())
			};
		}

		// Token: 0x060004E6 RID: 1254 RVA: 0x00017164 File Offset: 0x00015364
		public LoadProviderTypeByBehaviourCodeResp LoadProviderTypeByBehaviourCode(LoadProviderTypeByBehaviourCodeReq Request)
		{
			IServiceProviderTypeManager serviceProviderTypeManager = new ServiceProviderTypeManager(Request.GetOperationContext());
			IList<SPProviderType> list = serviceProviderTypeManager.LoadProviderTypeByBehaviourCode(Request.BehaviourCode);
			LoadProviderTypeByBehaviourCodeResp loadProviderTypeByBehaviourCodeResp = new LoadProviderTypeByBehaviourCodeResp();
			IList<SPProviderTypeDTO> providerTypes;
			if (list != null)
			{
				providerTypes = list.ToList<SPProviderType>().ConvertAll<SPProviderTypeDTO>((SPProviderType f) => f.ToDTO());
			}
			else
			{
				providerTypes = null;
			}
			loadProviderTypeByBehaviourCodeResp.ProviderTypes = providerTypes;
			return loadProviderTypeByBehaviourCodeResp;
		}

		// Token: 0x060004E7 RID: 1255 RVA: 0x000171CC File Offset: 0x000153CC
		public LoadAllProviderTypesResp LoadAllProviderTypes(LoadAllProviderTypesReq Request)
		{
			IServiceProviderTypeManager serviceProviderTypeManager = new ServiceProviderTypeManager(Request.GetOperationContext());
			IList<SPProviderType> list = serviceProviderTypeManager.LoadAllProviderTypes();
			LoadAllProviderTypesResp loadAllProviderTypesResp = new LoadAllProviderTypesResp();
			IList<SPProviderTypeDTO> providerTypes;
			if (list != null)
			{
				providerTypes = list.ToList<SPProviderType>().ConvertAll<SPProviderTypeDTO>((SPProviderType f) => f.ToDTO());
			}
			else
			{
				providerTypes = null;
			}
			loadAllProviderTypesResp.ProviderTypes = providerTypes;
			return loadAllProviderTypesResp;
		}

		// Token: 0x060004E8 RID: 1256 RVA: 0x00017230 File Offset: 0x00015430
		public CreateProviderTypeResp CreateProviderType(CreateProviderTypeReq Request)
		{
			IServiceProviderTypeManager serviceProviderTypeManager = new ServiceProviderTypeManager(Request.GetOperationContext());
			int spproviderTypeId = serviceProviderTypeManager.CreateProviderType(Request.ProviderType.ToDomainObject());
			return new CreateProviderTypeResp
			{
				SPProviderTypeId = spproviderTypeId
			};
		}

		// Token: 0x060004E9 RID: 1257 RVA: 0x00017270 File Offset: 0x00015470
		public UpdateProviderTypeResp UpdateProviderType(UpdateProviderTypeReq Request)
		{
			IServiceProviderTypeManager serviceProviderTypeManager = new ServiceProviderTypeManager(Request.GetOperationContext());
			serviceProviderTypeManager.UpdateProviderType(Request.ProviderType.ToDomainObject());
			return new UpdateProviderTypeResp();
		}

		// Token: 0x060004EA RID: 1258 RVA: 0x000172A8 File Offset: 0x000154A8
		public DeleteProviderTypeResp DeleteProviderType(DeleteProviderTypeReq Request)
		{
			IServiceProviderTypeManager serviceProviderTypeManager = new ServiceProviderTypeManager(Request.GetOperationContext());
			serviceProviderTypeManager.DeleteProviderType(Request.SPProviderTypeId);
			return new DeleteProviderTypeResp();
		}
	}
}
