using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TechnoPro.ClockWorkServer.Contracts.DTO.CustomForms.Data;
using TechnoPro.ClockWorkServer.Contracts.DTO.Vets;
using TechnoPro.Common.ClientManager.ICore;
using TechnoPro.Common.ClientManager.ICore.Vets;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities.Vets;
using TechnoPro.Common.Unity.IoC;
using TechnoPro.Common.Web.Security.Proxy;

namespace TechnoPro.Common.Rest.ClientManager.Core.Vets
{
	// Token: 0x02000002 RID: 2
	public class VetsBenefitApplicationRestClientManager : BearerTokenRestProxy<IVetsBenefitApplicationClientManager>, IVetsBenefitApplicationClientManager, IWebService
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		public VetsBenefitApplicationRestClientManager(string serviceAddress, string token = null) : base(serviceAddress, token)
		{
		}

		// Token: 0x06000002 RID: 2 RVA: 0x0000205A File Offset: 0x0000025A
		public VetsBenefitApplicationRestClientManager(string serviceAddress, string serviceAddressSuffix, string token = null) : base(serviceAddress, serviceAddressSuffix, token)
		{
		}

		// Token: 0x06000003 RID: 3 RVA: 0x00002068 File Offset: 0x00000268
		public async Task<VetsBenefitApplicationDTO> LoadBenefitApplicationByIdAsync(Guid BenefitApplicationId)
		{
			return await this.GetAsync<VetsBenefitApplicationDTO>(string.Format("vetsbenefitapplication/id/{0}", BenefitApplicationId), true).ConfigureAwait(false);
		}

		// Token: 0x06000004 RID: 4 RVA: 0x000020B8 File Offset: 0x000002B8
		public async Task<VetsBenefitApplicationDTO> LoadBenefitApplicationBaseAndSingleStepDataAsync(Guid BenefitApplicationId, eVetsBenefitApplicationStep? preferredStep)
		{
			return await this.GetAsync<VetsBenefitApplicationDTO>(string.Format("vetsbenefitapplication/baseandsingledate/id/{0}/preferredstep/{1}", BenefitApplicationId, preferredStep), true).ConfigureAwait(false);
		}

		// Token: 0x06000005 RID: 5 RVA: 0x00002110 File Offset: 0x00000310
		public async Task SaveVetsChapterAsync(Guid benefitApplicationId, Guid chapterId)
		{
			SaveVetsChapterReq saveVetsChapterReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<SaveVetsChapterReq>();
			saveVetsChapterReq.BenefitApplicationId = benefitApplicationId;
			saveVetsChapterReq.ChapterId = chapterId;
			await this.PostAsync<SaveVetsChapterReq>(saveVetsChapterReq, "vetsbenefitapplication/savevetschapter").ConfigureAwait(false);
		}

		// Token: 0x06000006 RID: 6 RVA: 0x00002168 File Offset: 0x00000368
		public async Task SaveVetsRegistrationDataAsync(Guid benefitApplicationId, bool completedRegistration, int personId, IList<CustomDataHolderCollectionDTO> data, params Guid[] dataInstanceIds)
		{
			SaveVetsRegistrationDataReq saveVetsRegistrationDataReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<SaveVetsRegistrationDataReq>();
			saveVetsRegistrationDataReq.BenefitApplicationId = benefitApplicationId;
			saveVetsRegistrationDataReq.CompletedRegistration = completedRegistration;
			saveVetsRegistrationDataReq.PersonId = personId;
			saveVetsRegistrationDataReq.Data = data;
			saveVetsRegistrationDataReq.DataInstanceIds = dataInstanceIds;
			await this.PostAsync<SaveVetsRegistrationDataReq>(saveVetsRegistrationDataReq, "vetsbenefitapplication/savevetsregistrationdata").ConfigureAwait(false);
		}

		// Token: 0x06000007 RID: 7 RVA: 0x000021D8 File Offset: 0x000003D8
		public async Task SaveVetsBenAppDataAsync(Guid benefitApplicationId, bool completedBenApp, int personId, int semesterId, IList<CustomDataHolderCollectionDTO> data, params Guid[] dataInstanceIds)
		{
			SaveVetsBenAppDataReq saveVetsBenAppDataReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<SaveVetsBenAppDataReq>();
			saveVetsBenAppDataReq.BenefitApplicationId = benefitApplicationId;
			saveVetsBenAppDataReq.CompletedBenApp = completedBenApp;
			saveVetsBenAppDataReq.PersonId = personId;
			saveVetsBenAppDataReq.SemesterId = semesterId;
			saveVetsBenAppDataReq.Data = data;
			saveVetsBenAppDataReq.DataInstanceIds = dataInstanceIds;
			await this.PostAsync<SaveVetsBenAppDataReq>(saveVetsBenAppDataReq, "vetsbenefitapplication/savevetsbenapp").ConfigureAwait(false);
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002250 File Offset: 0x00000450
		public async Task SaveVetsStudentAgreeDataAsync(Guid benefitApplicationId, bool completedStudentAgree, int personId, int semesterId, IList<CustomDataHolderCollectionDTO> data, params Guid[] dataInstanceIds)
		{
			SaveVetsStudentAgreeDataReq saveVetsStudentAgreeDataReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<SaveVetsStudentAgreeDataReq>();
			saveVetsStudentAgreeDataReq.BenefitApplicationId = benefitApplicationId;
			saveVetsStudentAgreeDataReq.CompletedStudentAgree = completedStudentAgree;
			saveVetsStudentAgreeDataReq.PersonId = personId;
			saveVetsStudentAgreeDataReq.SemesterId = semesterId;
			saveVetsStudentAgreeDataReq.Data = data;
			saveVetsStudentAgreeDataReq.DataInstanceIds = dataInstanceIds;
			await this.PostAsync<SaveVetsStudentAgreeDataReq>(saveVetsStudentAgreeDataReq, "vetsbenefitapplication/savevetsstudentagreedata").ConfigureAwait(false);
		}

		// Token: 0x06000009 RID: 9 RVA: 0x000022C8 File Offset: 0x000004C8
		public async Task<Guid?> CreateVetsBenefitApplicationAsync(int personId, int semesterId)
		{
			CreateVetsBenefitApplicationReq createVetsBenefitApplicationReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<CreateVetsBenefitApplicationReq>();
			createVetsBenefitApplicationReq.PersonId = personId;
			createVetsBenefitApplicationReq.SemesterId = semesterId;
			return await this.PostAsync<CreateVetsBenefitApplicationReq, Guid?>(createVetsBenefitApplicationReq, "vetsbenefitapplication").ConfigureAwait(false);
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002320 File Offset: 0x00000520
		public async Task<Guid?> CreateVetsBenefitApplicationCurrentSemesterAsync(int personId)
		{
			return await this.PostAsync<int, Guid?>(personId, "vetsbenefitapplication/currentsemester").ConfigureAwait(false);
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00002370 File Offset: 0x00000570
		public async Task<Guid?> CreateVetsBenefitApplicationNextSemesterAsync(int personId)
		{
			return await this.PostAsync<int, Guid?>(personId, "vetsbenefitapplication/nextsemester").ConfigureAwait(false);
		}
	}
}
