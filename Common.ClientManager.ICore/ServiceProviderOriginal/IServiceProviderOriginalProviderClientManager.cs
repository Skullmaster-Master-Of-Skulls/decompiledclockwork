using System;
using TechnoPro.ClockWorkServer.Contracts.DTO.ServiceProviderOriginal;
using TechnoPro.Common.Public;

namespace TechnoPro.Common.ClientManager.ICore.ServiceProviderOriginal
{
	// Token: 0x02000021 RID: 33
	public interface IServiceProviderOriginalProviderClientManager : IWebService
	{
		// Token: 0x060000CB RID: 203
		ServiceProviderDTO LoadProviderById(int ServiceProviderId);

		// Token: 0x060000CC RID: 204
		ServiceProviderBaseDTO LoadProviderBaseById(int ServiceProviderId);
	}
}
