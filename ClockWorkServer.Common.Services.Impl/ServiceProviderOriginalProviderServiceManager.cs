using System;
using TechnoPro.ClockWorkServer.Common.Services.Impl.Adapters;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.ServiceProviderOriginal.ContractParameters;
using TechnoPro.Common.Core.Mappers.ServiceProvidersOriginal;
using TechnoPro.Common.Core.ServiceProvidersOriginal;
using TechnoPro.Common.ICore.ServiceProvidersOriginal;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities.ServiceProvidersOriginal;

namespace TechnoPro.ClockWorkServer.Common.Services.Impl
{
	// Token: 0x02000081 RID: 129
	public class ServiceProviderOriginalProviderServiceManager : IServiceProviderOriginalProvider, IService
	{
		// Token: 0x060004C0 RID: 1216 RVA: 0x00016978 File Offset: 0x00014B78
		public LoadProviderByIdResp LoadProviderById(LoadProviderByIdReq Request)
		{
			IServiceProviderOriginalProviderManager serviceProviderOriginalProviderManager = new ServiceProviderOriginalProviderManager(Request.GetOperationContext());
			ServiceProvider serviceProvider = serviceProviderOriginalProviderManager.LoadProviderById(Request.ServiceProviderId);
			return new LoadProviderByIdResp
			{
				Provider = ((serviceProvider == null) ? null : serviceProvider.ToDTO())
			};
		}

		// Token: 0x060004C1 RID: 1217 RVA: 0x000169BC File Offset: 0x00014BBC
		public LoadProviderBaseByIdResp LoadProviderBaseById(LoadProviderBaseByIdReq Request)
		{
			IServiceProviderOriginalProviderManager serviceProviderOriginalProviderManager = new ServiceProviderOriginalProviderManager(Request.GetOperationContext());
			ServiceProviderBase serviceProviderBase = serviceProviderOriginalProviderManager.LoadProviderBaseById(Request.ServiceProviderId);
			return new LoadProviderBaseByIdResp
			{
				ProviderBase = ((serviceProviderBase == null) ? null : serviceProviderBase.ToDTO())
			};
		}
	}
}
