using System;
using System.Collections.Generic;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities.OperationContexts;
using TechnoPro.Common.Public.Entities.ServiceProvidersOriginal;

namespace TechnoPro.Common.Core.ServiceProvidersOriginal
{
	// Token: 0x02000006 RID: 6
	public interface IServiceProviderOriginalMatchingDAO : IBaseOperationContext<ServiceProvidersOperationContext>
	{
		// Token: 0x06000002 RID: 2
		IList<ServiceProviderAssignment> LoadAssignmentsByProviderAndAssignedDate(int ServiceProviderId, DateTime StartDate, DateTime EndDate);
	}
}
