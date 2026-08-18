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
	// Token: 0x0200004D RID: 77
	public class ServiceProviderTypeManager : IServiceProviderTypeManager, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x06000328 RID: 808 RVA: 0x00011BC0 File Offset: 0x0000FDC0
		public ServiceProviderTypeManager(OperationContext opContext)
		{
			this.OpContext = opContext;
			this.dao = new ServiceProviderTypeDAO(opContext);
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x06000329 RID: 809 RVA: 0x00011BDE File Offset: 0x0000FDDE
		// (set) Token: 0x0600032A RID: 810 RVA: 0x00011BE6 File Offset: 0x0000FDE6
		public OperationContext OpContext { get; set; }

		// Token: 0x0600032B RID: 811 RVA: 0x000072EA File Offset: 0x000054EA
		public SPProviderType LoadProviderTypeById(int SPProviderTypeId)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600032C RID: 812 RVA: 0x000072EA File Offset: 0x000054EA
		public IList<SPProviderType> LoadProviderTypeByBehaviourCode(eProviderTypeBehaviourCode Code)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600032D RID: 813 RVA: 0x000072EA File Offset: 0x000054EA
		public IList<SPProviderType> LoadAllProviderTypes()
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600032E RID: 814 RVA: 0x000072EA File Offset: 0x000054EA
		public int CreateProviderType(SPProviderType ProviderType)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600032F RID: 815 RVA: 0x000072EA File Offset: 0x000054EA
		public void UpdateProviderType(SPProviderType ProviderType)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000330 RID: 816 RVA: 0x000072EA File Offset: 0x000054EA
		public void DeleteProviderType(int SPProviderTypeId)
		{
			throw new NotImplementedException();
		}

		// Token: 0x04000099 RID: 153
		public IServiceProviderTypeDAO dao;
	}
}
