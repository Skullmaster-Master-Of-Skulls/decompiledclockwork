using System;
using System.Collections.Generic;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.ServiceProvider;

namespace TechnoPro.Common.ICore.ServiceProviders
{
	// Token: 0x0200003C RID: 60
	public interface IServiceProviderApplicationManager : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x0600018A RID: 394
		SPApplication LoadApplicationById(int SPApplicationId);

		// Token: 0x0600018B RID: 395
		SPApplication LoadApplicationByProviderAndType(int SPProviderId, int SPProviderTypeId);

		// Token: 0x0600018C RID: 396
		int CreateApplication(SPApplication Application);

		// Token: 0x0600018D RID: 397
		void UpdateApplication(SPApplication Application);

		// Token: 0x0600018E RID: 398
		bool DeleteApplication(int SPApplicationId);

		// Token: 0x0600018F RID: 399
		void UpdateApplicationAvailabilityType(int SPApplicationId, SPApplicationAvailabilityType NewAvailabilityType);

		// Token: 0x06000190 RID: 400
		IList<SPApplication> LoadApplicationsBySPProviderType(int SPProviderTypeId, DateTime StartDate, DateTime EndDate, bool IncludeInactiveApplications);

		// Token: 0x06000191 RID: 401
		IList<SPApplication> LoadApplicationsBySPProvider(int SPProviderId, DateTime StartDate, DateTime EndDate, bool IncludeInactiveApplications);
	}
}
