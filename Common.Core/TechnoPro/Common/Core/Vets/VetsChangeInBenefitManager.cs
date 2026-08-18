using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TechnoPro.Common.ICore.Vets;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Vets;

namespace TechnoPro.Common.Core.Vets
{
	// Token: 0x02000027 RID: 39
	public class VetsChangeInBenefitManager : IVetsChangeInBenefitManager, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x06000152 RID: 338 RVA: 0x000072C7 File Offset: 0x000054C7
		public VetsChangeInBenefitManager(OperationContext opContext)
		{
			this.OpContext = opContext;
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x06000153 RID: 339 RVA: 0x000072D9 File Offset: 0x000054D9
		// (set) Token: 0x06000154 RID: 340 RVA: 0x000072E1 File Offset: 0x000054E1
		public OperationContext OpContext { get; set; }

		// Token: 0x06000155 RID: 341 RVA: 0x000072EA File Offset: 0x000054EA
		public Task<VetsChangeInBenefitApplication> LoadChangeInBenefitApplicationByIdAsync(int changeInBenefitApplicationId)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000156 RID: 342 RVA: 0x000072EA File Offset: 0x000054EA
		public Task<VetsChangeInBenefitApplication> CreateChangeInBenefitApplicationAsync(int benefitApplicationId, int studentPersonId)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000157 RID: 343 RVA: 0x000072EA File Offset: 0x000054EA
		public Task<IList<VetsChangeInBenefitApplication>> LoadChangeInBenefitApplicationsByBenefitApplicationAsync(int benefitApplicationId)
		{
			throw new NotImplementedException();
		}
	}
}
