using System;
using TechnoPro.ClockWorkServer.Client.ServiceFactory;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.ServiceProviderOriginal;
using TechnoPro.ClockWorkServer.Contracts.DTO.ServiceProviderOriginal.ContractParameters;
using TechnoPro.Common.ClientManager.ICore;
using TechnoPro.Common.ClientManager.ICore.ServiceProviderOriginal;
using TechnoPro.Common.Public;
using TechnoPro.Common.Unity.IoC;

namespace TechnoPro.Common.ClientManager.Core.ServiceProviderOriginal
{
	// Token: 0x02000024 RID: 36
	public class ServiceProviderOriginalProviderClientManager : IServiceProviderOriginalProviderClientManager, IWebService
	{
		// Token: 0x06000115 RID: 277 RVA: 0x00006434 File Offset: 0x00004634
		public ServiceProviderDTO LoadProviderById(int ServiceProviderId)
		{
			LoadProviderByIdReq loadProviderByIdReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadProviderByIdReq>();
			loadProviderByIdReq.ServiceProviderId = ServiceProviderId;
			return ClientServiceFactory.GetClientInstance<IServiceProviderOriginalProvider>().LoadProviderById(loadProviderByIdReq).Provider;
		}

		// Token: 0x06000116 RID: 278 RVA: 0x0000646C File Offset: 0x0000466C
		public ServiceProviderBaseDTO LoadProviderBaseById(int ServiceProviderId)
		{
			LoadProviderBaseByIdReq loadProviderBaseByIdReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadProviderBaseByIdReq>();
			loadProviderBaseByIdReq.ServiceProviderId = ServiceProviderId;
			return ClientServiceFactory.GetClientInstance<IServiceProviderOriginalProvider>().LoadProviderBaseById(loadProviderBaseByIdReq).ProviderBase;
		}
	}
}
