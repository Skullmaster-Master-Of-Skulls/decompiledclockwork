using System;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities.OperationContexts;
using TechnoPro.Common.Public.Entities.ServiceProvidersOriginal;

namespace TechnoPro.Common.Core.ServiceProvidersOriginal
{
	// Token: 0x02000009 RID: 9
	public interface IServiceProviderOriginalRequestDetailDAO : IBaseOperationContext<ServiceProvidersOperationContext>
	{
		// Token: 0x0600000A RID: 10
		ServiceProviderRequestDetail LoadServiceRequestDetailByRequestId(int serviceProviderRequestId);
	}
}
