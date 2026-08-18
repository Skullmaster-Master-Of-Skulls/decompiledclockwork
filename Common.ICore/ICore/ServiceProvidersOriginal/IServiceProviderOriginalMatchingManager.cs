using System;
using System.Collections.Generic;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.ServiceProvidersOriginal;

namespace TechnoPro.Common.ICore.ServiceProvidersOriginal
{
	// Token: 0x02000046 RID: 70
	public interface IServiceProviderOriginalMatchingManager : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x060001C3 RID: 451
		void AssignProviderToRequest(int ServiceProviderRequestId, int ServiceProviderId, int ServiceProviderLuCourseId);

		// Token: 0x060001C4 RID: 452
		void AssignPrivateProviderToRequest(int ServiceProviderRequestId);

		// Token: 0x060001C5 RID: 453
		void CancelProvider(int ServiceProviderRequestId, string Note);

		// Token: 0x060001C6 RID: 454
		IList<ServiceProviderApplication> FindPotentialMatchingsByRequest(ServiceRequest Request);

		// Token: 0x060001C7 RID: 455
		IList<ServiceProviderApplication> FindPotentialMatchingsByRequestId(int ServiceProviderRequestId);

		// Token: 0x060001C8 RID: 456
		IList<ServiceProviderApplication> FindPotentialMatchingsByRequestIds(params int[] ServiceProviderRequestId);

		// Token: 0x060001C9 RID: 457
		IList<ServiceProviderApplication> FindPotentialMatchingsBy(params int[] ServiceProviderRequestId);

		// Token: 0x060001CA RID: 458
		IList<int> FindRequestsWithAtLeastOnePotentialMatchings(IList<int> ServiceProviderRequestIds);

		// Token: 0x060001CB RID: 459
		IList<int> FindRequestsWithAtLeastOnePotentialMatchings(DateTime StartDate, DateTime EndDate, params int[] ServiceProviderTypeId);

		// Token: 0x060001CC RID: 460
		IList<ServiceProviderAssignment> LoadAssignmentsByProviderAndAssignedDate(int ServiceProviderId, DateTime StartDate, DateTime EndDate);
	}
}
