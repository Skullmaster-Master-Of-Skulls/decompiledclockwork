using System;
using TechnoPro.Common.DAO.Impl.Institution;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Adapters;
using TechnoPro.Common.Public.Entities;

namespace TechnoPro.Common.Core.Institution
{
	// Token: 0x020000ED RID: 237
	public class InstitutionManager : IInstitutionManager, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x0600093A RID: 2362 RVA: 0x0000672B File Offset: 0x0000492B
		public InstitutionManager()
		{
		}

		// Token: 0x0600093B RID: 2363 RVA: 0x0003B51B File Offset: 0x0003971B
		public InstitutionManager(OperationContext opContext)
		{
			this.OpContext = opContext;
		}

		// Token: 0x0600093C RID: 2364 RVA: 0x0003B530 File Offset: 0x00039730
		public string GetInstitutionUniqueName()
		{
			InstitutionDAO institutionDAO = new InstitutionDAO();
			return institutionDAO.GetInstitutionUniqueName().ApplyAzureStorageContainerNamingConventionRules();
		}

		// Token: 0x0600093D RID: 2365 RVA: 0x0003B554 File Offset: 0x00039754
		public string GetInstitutionName()
		{
			InstitutionDAO institutionDAO = new InstitutionDAO();
			return institutionDAO.GetInstitutionName();
		}

		// Token: 0x17000153 RID: 339
		// (get) Token: 0x0600093E RID: 2366 RVA: 0x0003B572 File Offset: 0x00039772
		// (set) Token: 0x0600093F RID: 2367 RVA: 0x0003B57A File Offset: 0x0003977A
		public OperationContext OpContext { get; set; }
	}
}
