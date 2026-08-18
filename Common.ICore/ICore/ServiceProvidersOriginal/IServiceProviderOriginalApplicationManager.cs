using System;
using System.Collections.Generic;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.ServiceProvidersOriginal;

namespace TechnoPro.Common.ICore.ServiceProvidersOriginal
{
	// Token: 0x02000045 RID: 69
	public interface IServiceProviderOriginalApplicationManager : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x060001BD RID: 445
		int CreateApplication(ServiceProviderApplication Application);

		// Token: 0x060001BE RID: 446
		void DeleteApplication(int ServiceProviderApplicationId);

		// Token: 0x060001BF RID: 447
		void UpdateApplication(ServiceProviderApplication Application);

		// Token: 0x060001C0 RID: 448
		ServiceProviderApplication LoadApplicationByProviderAndTypeAndDate(int ServiceProviderId, int ServiceProviderTypeId, DateTime StartDate, DateTime EndDate);

		// Token: 0x060001C1 RID: 449
		IList<ServiceProviderApplication> LoadApplicationsByTypeAndDate(int ServiceProviderTypeId, DateTime StartDate, DateTime EndDate);

		// Token: 0x060001C2 RID: 450
		IList<ServiceProviderApplicationBase> LoadApplicationBasesByTypeAndDate(int ServiceProviderTypeId, DateTime StartDate, DateTime EndDate);
	}
}
