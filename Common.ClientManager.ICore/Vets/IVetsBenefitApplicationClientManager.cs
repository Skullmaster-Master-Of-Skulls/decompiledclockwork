using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TechnoPro.ClockWorkServer.Contracts.DTO.CustomForms.Data;
using TechnoPro.ClockWorkServer.Contracts.DTO.Vets;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities.Vets;

namespace TechnoPro.Common.ClientManager.ICore.Vets
{
	// Token: 0x02000003 RID: 3
	public interface IVetsBenefitApplicationClientManager : IWebService
	{
		// Token: 0x06000005 RID: 5
		Task<VetsBenefitApplicationDTO> LoadBenefitApplicationByIdAsync(Guid BenefitApplicationId);

		// Token: 0x06000006 RID: 6
		Task<VetsBenefitApplicationDTO> LoadBenefitApplicationBaseAndSingleStepDataAsync(Guid BenefitApplicationId, eVetsBenefitApplicationStep? preferredStep);

		// Token: 0x06000007 RID: 7
		Task SaveVetsChapterAsync(Guid benefitApplicationId, Guid chapterId);

		// Token: 0x06000008 RID: 8
		Task SaveVetsRegistrationDataAsync(Guid benefitApplicationId, bool completedRegistration, int personId, IList<CustomDataHolderCollectionDTO> data, params Guid[] dataInstanceIds);

		// Token: 0x06000009 RID: 9
		Task SaveVetsBenAppDataAsync(Guid benefitApplicationId, bool completedBenApp, int personId, int semesterId, IList<CustomDataHolderCollectionDTO> data, params Guid[] dataInstanceIds);

		// Token: 0x0600000A RID: 10
		Task SaveVetsStudentAgreeDataAsync(Guid benefitApplicationId, bool completedStudentAgree, int personId, int semesterId, IList<CustomDataHolderCollectionDTO> data, params Guid[] dataInstanceIds);

		// Token: 0x0600000B RID: 11
		Task<Guid?> CreateVetsBenefitApplicationAsync(int personId, int semesterId);

		// Token: 0x0600000C RID: 12
		Task<Guid?> CreateVetsBenefitApplicationCurrentSemesterAsync(int personId);

		// Token: 0x0600000D RID: 13
		Task<Guid?> CreateVetsBenefitApplicationNextSemesterAsync(int personId);
	}
}
