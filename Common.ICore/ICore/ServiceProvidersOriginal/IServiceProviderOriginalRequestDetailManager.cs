using System;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.ServiceProvidersOriginal;

namespace TechnoPro.Common.ICore.ServiceProvidersOriginal
{
	// Token: 0x02000049 RID: 73
	public interface IServiceProviderOriginalRequestDetailManager : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x060001D9 RID: 473
		int CreateRequestDetail(ServiceProviderRequestDetail Detail);

		// Token: 0x060001DA RID: 474
		void DeleteRequestDetail(int ServiceProviderRequestDetailId);

		// Token: 0x060001DB RID: 475
		void UpdateRequestDetail(ServiceProviderRequestDetail Detail);

		// Token: 0x060001DC RID: 476
		ServiceProviderRequestDetail LoadServiceRequestDetailByRequestId(int serviceProviderRequestId);
	}
}
