using System;
using System.Collections.Generic;
using TechnoPro.ClockWorkServer.Contracts.DTO.ServiceProvider;
using TechnoPro.Common.Public;

namespace TechnoPro.Common.ClientManager.ICore.ServiceProvider
{
	// Token: 0x0200001A RID: 26
	public interface IServiceProviderApplicationClientManager : IWebService
	{
		// Token: 0x0600009B RID: 155
		SPApplicationDTO LoadApplicationById(int SPApplicationId);

		// Token: 0x0600009C RID: 156
		SPApplicationDTO LoadApplicationByProviderAndType(int SPProviderId, int SPProviderTypeId);

		// Token: 0x0600009D RID: 157
		int CreateApplication(SPApplicationDTO Application);

		// Token: 0x0600009E RID: 158
		void UpdateApplication(SPApplicationDTO Application);

		// Token: 0x0600009F RID: 159
		bool DeleteApplication(int SPApplicationId);

		// Token: 0x060000A0 RID: 160
		void UpdateApplicationAvailabilityType(int SPApplicationId, SPApplicationAvailabilityTypeDTO NewAvailabilityType);

		// Token: 0x060000A1 RID: 161
		IList<SPApplicationDTO> LoadApplicationsBySPProviderType(int SPProviderTypeId, DateTime StartDate, DateTime EndDate, bool IncludeInactiveApplications);

		// Token: 0x060000A2 RID: 162
		IList<SPApplicationDTO> LoadApplicationsBySPProvider(int SPProviderId, DateTime StartDate, DateTime EndDate, bool IncludeInactiveApplications);
	}
}
