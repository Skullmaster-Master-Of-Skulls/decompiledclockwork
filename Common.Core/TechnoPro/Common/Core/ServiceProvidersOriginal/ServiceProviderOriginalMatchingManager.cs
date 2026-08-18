using System;
using System.Collections.Generic;
using TechnoPro.Common.Core.Adapters;
using TechnoPro.Common.ICore.ServiceProvidersOriginal;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.ServiceProvidersOriginal;

namespace TechnoPro.Common.Core.ServiceProvidersOriginal
{
	// Token: 0x02000053 RID: 83
	public class ServiceProviderOriginalMatchingManager : IServiceProviderOriginalMatchingManager, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x1700008D RID: 141
		// (get) Token: 0x06000362 RID: 866 RVA: 0x00011F4D File Offset: 0x0001014D
		// (set) Token: 0x06000363 RID: 867 RVA: 0x00011F55 File Offset: 0x00010155
		public IServiceProviderOriginalMatchingDAO dao { get; set; }

		// Token: 0x06000364 RID: 868 RVA: 0x00011F5E File Offset: 0x0001015E
		public ServiceProviderOriginalMatchingManager(OperationContext opContext)
		{
			this.OpContext = opContext;
			this.dao = new ServiceProviderOriginalMatchingDAO(this.OpContext.GetProviderTypes());
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x06000365 RID: 869 RVA: 0x00011F87 File Offset: 0x00010187
		// (set) Token: 0x06000366 RID: 870 RVA: 0x00011F8F File Offset: 0x0001018F
		public OperationContext OpContext { get; set; }

		// Token: 0x06000367 RID: 871 RVA: 0x000072EA File Offset: 0x000054EA
		public void AssignProviderToRequest(int ServiceProviderRequestId, int ServiceProviderId, int ServiceProviderLuCourseId)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000368 RID: 872 RVA: 0x000072EA File Offset: 0x000054EA
		public void AssignPrivateProviderToRequest(int ServiceProviderRequestId)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000369 RID: 873 RVA: 0x000072EA File Offset: 0x000054EA
		public void CancelProvider(int ServiceProviderRequestId, string Note)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600036A RID: 874 RVA: 0x000072EA File Offset: 0x000054EA
		public IList<ServiceProviderApplication> FindPotentialMatchingsByRequest(ServiceRequest Request)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600036B RID: 875 RVA: 0x000072EA File Offset: 0x000054EA
		public IList<ServiceProviderApplication> FindPotentialMatchingsByRequestId(int ServiceProviderRequestId)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600036C RID: 876 RVA: 0x000072EA File Offset: 0x000054EA
		public IList<ServiceProviderApplication> FindPotentialMatchingsByRequestIds(params int[] ServiceProviderRequestId)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600036D RID: 877 RVA: 0x000072EA File Offset: 0x000054EA
		public IList<ServiceProviderApplication> FindPotentialMatchingsBy(params int[] ServiceProviderRequestId)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600036E RID: 878 RVA: 0x000072EA File Offset: 0x000054EA
		public IList<int> FindRequestsWithAtLeastOnePotentialMatchings(IList<int> ServiceProviderRequestIds)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600036F RID: 879 RVA: 0x000072EA File Offset: 0x000054EA
		public IList<int> FindRequestsWithAtLeastOnePotentialMatchings(DateTime StartDate, DateTime EndDate, params int[] ServiceProviderTypeId)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000370 RID: 880 RVA: 0x00011F98 File Offset: 0x00010198
		public IList<ServiceProviderAssignment> LoadAssignmentsByProviderAndAssignedDate(int ServiceProviderId, DateTime StartDate, DateTime EndDate)
		{
			return this.dao.LoadAssignmentsByProviderAndAssignedDate(ServiceProviderId, StartDate, EndDate);
		}
	}
}
