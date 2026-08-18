using System;
using System.Collections.Generic;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.ServiceProvidersOriginal;

namespace TechnoPro.Common.ICore.ServiceProvidersOriginal
{
	// Token: 0x0200004A RID: 74
	public interface IServiceProviderOriginalRequestManager : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x060001DD RID: 477
		ServiceRequest LoadRequestById(int ServiceProviderRequestId);

		// Token: 0x060001DE RID: 478
		IList<ServiceRequest> LoadRequestsByDate(DateTime StartDate, DateTime EndDate);

		// Token: 0x060001DF RID: 479
		IList<ServiceRequest> LoadRequestsByDateAndType(DateTime StartDate, DateTime EndDate, params int[] ServiceProviderTypeId);

		// Token: 0x060001E0 RID: 480
		int CreateRequest(ServiceRequest Request);

		// Token: 0x060001E1 RID: 481
		void DeleteRequest(int ServiceProviderRequestId);

		// Token: 0x060001E2 RID: 482
		void UpdateRequest(ServiceRequest Request);
	}
}
