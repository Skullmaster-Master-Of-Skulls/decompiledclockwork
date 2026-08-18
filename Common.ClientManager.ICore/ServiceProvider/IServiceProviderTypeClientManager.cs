using System;
using System.Collections.Generic;
using TechnoPro.ClockWorkServer.Contracts.DTO.ServiceProvider;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities.ServiceProvider;

namespace TechnoPro.Common.ClientManager.ICore.ServiceProvider
{
	// Token: 0x0200001D RID: 29
	public interface IServiceProviderTypeClientManager : IWebService
	{
		// Token: 0x060000B4 RID: 180
		SPProviderTypeDTO LoadProviderTypeById(int SPProviderTypeId);

		// Token: 0x060000B5 RID: 181
		IList<SPProviderTypeDTO> LoadProviderTypeByBehaviourCode(eProviderTypeBehaviourCode Code);

		// Token: 0x060000B6 RID: 182
		IList<SPProviderTypeDTO> LoadAllProviderTypes();

		// Token: 0x060000B7 RID: 183
		int CreateProviderType(SPProviderTypeDTO ProviderType);

		// Token: 0x060000B8 RID: 184
		void UpdateProviderType(SPProviderTypeDTO ProviderType);

		// Token: 0x060000B9 RID: 185
		void DeleteProviderType(int SPProviderTypeId);
	}
}
