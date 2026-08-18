using System;
using System.Collections.Generic;
using TechnoPro.ClockWorkServer.Client.ServiceFactory;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.ServiceProvider;
using TechnoPro.ClockWorkServer.Contracts.DTO.ServiceProvider.ContractParameters;
using TechnoPro.Common.ClientManager.ICore;
using TechnoPro.Common.ClientManager.ICore.ServiceProvider;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities.ServiceProvider;
using TechnoPro.Common.Unity.IoC;

namespace TechnoPro.Common.ClientManager.Core.ServiceProvider
{
	// Token: 0x02000020 RID: 32
	public class ServiceProviderTypeClientManager : IServiceProviderTypeClientManager, IWebService
	{
		// Token: 0x060000FA RID: 250 RVA: 0x00005EEC File Offset: 0x000040EC
		public SPProviderTypeDTO LoadProviderTypeById(int SPProviderTypeId)
		{
			LoadProviderTypeByIdReq loadProviderTypeByIdReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadProviderTypeByIdReq>();
			loadProviderTypeByIdReq.SPProviderTypeId = SPProviderTypeId;
			return ClientServiceFactory.GetClientInstance<IServiceProviderType>().LoadProviderTypeById(loadProviderTypeByIdReq).ProviderType;
		}

		// Token: 0x060000FB RID: 251 RVA: 0x00005F24 File Offset: 0x00004124
		public IList<SPProviderTypeDTO> LoadProviderTypeByBehaviourCode(eProviderTypeBehaviourCode Code)
		{
			LoadProviderTypeByBehaviourCodeReq loadProviderTypeByBehaviourCodeReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadProviderTypeByBehaviourCodeReq>();
			loadProviderTypeByBehaviourCodeReq.BehaviourCode = Code;
			return ClientServiceFactory.GetClientInstance<IServiceProviderType>().LoadProviderTypeByBehaviourCode(loadProviderTypeByBehaviourCodeReq).ProviderTypes;
		}

		// Token: 0x060000FC RID: 252 RVA: 0x00005F5C File Offset: 0x0000415C
		public IList<SPProviderTypeDTO> LoadAllProviderTypes()
		{
			LoadAllProviderTypesReq request = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadAllProviderTypesReq>();
			return ClientServiceFactory.GetClientInstance<IServiceProviderType>().LoadAllProviderTypes(request).ProviderTypes;
		}

		// Token: 0x060000FD RID: 253 RVA: 0x00005F8C File Offset: 0x0000418C
		public int CreateProviderType(SPProviderTypeDTO ProviderType)
		{
			CreateProviderTypeReq createProviderTypeReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<CreateProviderTypeReq>();
			createProviderTypeReq.ProviderType = ProviderType;
			return ClientServiceFactory.GetClientInstance<IServiceProviderType>().CreateProviderType(createProviderTypeReq).SPProviderTypeId;
		}

		// Token: 0x060000FE RID: 254 RVA: 0x00005FC4 File Offset: 0x000041C4
		public void UpdateProviderType(SPProviderTypeDTO ProviderType)
		{
			UpdateProviderTypeReq updateProviderTypeReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<UpdateProviderTypeReq>();
			updateProviderTypeReq.ProviderType = ProviderType;
			ClientServiceFactory.GetClientInstance<IServiceProviderType>().UpdateProviderType(updateProviderTypeReq);
		}

		// Token: 0x060000FF RID: 255 RVA: 0x00005FF4 File Offset: 0x000041F4
		public void DeleteProviderType(int SPProviderTypeId)
		{
			DeleteProviderTypeReq deleteProviderTypeReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<DeleteProviderTypeReq>();
			deleteProviderTypeReq.SPProviderTypeId = SPProviderTypeId;
			ClientServiceFactory.GetClientInstance<IServiceProviderType>().DeleteProviderType(deleteProviderTypeReq);
		}
	}
}
