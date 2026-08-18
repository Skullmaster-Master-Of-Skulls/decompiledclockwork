using System;
using System.Threading.Tasks;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Vets;

namespace TechnoPro.Common.DAO.Vets
{
	// Token: 0x02000013 RID: 19
	public interface IVetsBenefitApplicationDAO : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x06000028 RID: 40
		Task<VetsBenefitApplication> LoadBenefitApplicationByIdAsync(Guid BenefitApplicationId);

		// Token: 0x06000029 RID: 41
		Task<VetsBenefitApplicationStatus> LoadBenefitApplicationStatusByIdAsync(VetsBenefitApplicationStatus baseApplication);

		// Token: 0x0600002A RID: 42
		Task UpdateBenefitApplicationStudentInfoAsync(Guid BenefitApplicationId, bool? RegistrationCompleted, Guid? ChapterId, bool? BenAppCompleted, bool? StudentAgreeCompleted, eVetsBenefitApplicationStep? PreferredStep);

		// Token: 0x0600002B RID: 43
		Task<Guid?> CreateVetsBenefitApplicationAsync(int PersonId, int SemesterId);

		// Token: 0x0600002C RID: 44
		Task UpdateVetsBenefitApplicationModificationEntryAsync(Guid BenefitApplicationId, params eVetsBenefitApplicationModificationType[] ModificationTypes);

		// Token: 0x0600002D RID: 45
		Task<VetsStudentCardInfo> LoadStudentVeteranCardInfoAsync(int PersonId);
	}
}
