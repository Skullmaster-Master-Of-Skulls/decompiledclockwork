using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.CustomForms.Data;
using TechnoPro.Common.Public.Entities.Vets;

namespace TechnoPro.Common.ICore.Vets
{
	// Token: 0x02000010 RID: 16
	public interface IVetsBenefitApplicationManager : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x06000061 RID: 97
		Task<VetsBenefitApplication> LoadBenefitApplicationByIdAsync(Guid BenefitApplicationId);

		// Token: 0x06000062 RID: 98
		Task<VetsBenefitApplication> LoadBenefitApplicationBaseAndSingleStepData(Guid BenefitApplicationId, eVetsBenefitApplicationStep? preferredStep);

		// Token: 0x06000063 RID: 99
		Task SaveVetsChapterAsync(Guid BenefitApplicationId, Guid ChapterId);

		// Token: 0x06000064 RID: 100
		Task SaveVetsRegistrationDataAsync(Guid BenefitApplicationId, bool completedRegistration, int PersonId, IList<CustomDataHolderCollection> Data, params Guid[] dataInstanceIds);

		// Token: 0x06000065 RID: 101
		Task SaveVetsBenAppDataAsync(Guid BenefitApplicationId, bool completedBenApp, int PersonId, int SemesterId, IList<CustomDataHolderCollection> Data, params Guid[] dataInstanceIds);

		// Token: 0x06000066 RID: 102
		Task SaveVetsStudentAgreeDataAsync(Guid BenefitApplicationId, bool completedStudentAgree, int PersonId, int SemesterId, IList<CustomDataHolderCollection> Data, params Guid[] dataInstanceIds);

		// Token: 0x06000067 RID: 103
		Task<Guid?> CreateVetsBenefitApplicationAsync(int PersonId, int SemesterId);

		// Token: 0x06000068 RID: 104
		Task<VetsStudentCardInfo> LoadStudentVeteranCardInfoAsync(int PersonId);

		// Token: 0x06000069 RID: 105
		Task<Guid?> CreateVetsBenefitApplicationCurrentSemesterAsync(int PersonId);

		// Token: 0x0600006A RID: 106
		Task<Guid?> CreateVetsBenefitApplicationNextSemesterAsync(int PersonId);
	}
}
