using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Vets;

namespace TechnoPro.Common.ICore.Vets
{
	// Token: 0x02000011 RID: 17
	public interface IVetsChangeInBenefitManager : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x0600006B RID: 107
		Task<VetsChangeInBenefitApplication> LoadChangeInBenefitApplicationByIdAsync(int changeInBenefitApplicationId);

		// Token: 0x0600006C RID: 108
		Task<VetsChangeInBenefitApplication> CreateChangeInBenefitApplicationAsync(int benefitApplicationId, int studentPersonId);

		// Token: 0x0600006D RID: 109
		Task<IList<VetsChangeInBenefitApplication>> LoadChangeInBenefitApplicationsByBenefitApplicationAsync(int benefitApplicationId);
	}
}
