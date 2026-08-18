using System;
using System.Collections.Generic;
using TechnoPro.Common.DAO.Impl.ServiceProvider;
using TechnoPro.Common.DAO.ServiceProvider;
using TechnoPro.Common.ICore.ServiceProviders;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.ServiceProvider;

namespace TechnoPro.Common.Core.ServiceProvider
{
	// Token: 0x02000049 RID: 73
	public class ServiceProviderApplicationManager : IServiceProviderApplicationManager, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x06000303 RID: 771 RVA: 0x00011B04 File Offset: 0x0000FD04
		public ServiceProviderApplicationManager(OperationContext opContext)
		{
			this.OpContext = opContext;
			this.dao = new ServiceProviderApplicationDAO(opContext);
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x06000304 RID: 772 RVA: 0x00011B22 File Offset: 0x0000FD22
		// (set) Token: 0x06000305 RID: 773 RVA: 0x00011B2A File Offset: 0x0000FD2A
		public OperationContext OpContext { get; set; }

		// Token: 0x06000306 RID: 774 RVA: 0x000072EA File Offset: 0x000054EA
		public SPApplication LoadApplicationById(int SPApplicationId)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000307 RID: 775 RVA: 0x000072EA File Offset: 0x000054EA
		public SPApplication LoadApplicationByProviderAndType(int SPProviderId, int SPProviderTypeId)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000308 RID: 776 RVA: 0x000072EA File Offset: 0x000054EA
		public int CreateApplication(SPApplication Application)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000309 RID: 777 RVA: 0x000072EA File Offset: 0x000054EA
		public void UpdateApplication(SPApplication Application)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600030A RID: 778 RVA: 0x000072EA File Offset: 0x000054EA
		public bool DeleteApplication(int SPApplicationId)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600030B RID: 779 RVA: 0x000072EA File Offset: 0x000054EA
		public void UpdateApplicationAvailabilityType(int SPApplicationId, SPApplicationAvailabilityType NewAvailabilityType)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600030C RID: 780 RVA: 0x000072EA File Offset: 0x000054EA
		public IList<SPApplication> LoadApplicationsBySPProviderType(int SPProviderTypeId, DateTime StartDate, DateTime EndDate, bool IncludeInactiveApplications)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600030D RID: 781 RVA: 0x000072EA File Offset: 0x000054EA
		public IList<SPApplication> LoadApplicationsBySPProvider(int SPProviderId, DateTime StartDate, DateTime EndDate, bool IncludeInactiveApplications)
		{
			throw new NotImplementedException();
		}

		// Token: 0x04000091 RID: 145
		public IServiceProviderApplicationDAO dao;
	}
}
