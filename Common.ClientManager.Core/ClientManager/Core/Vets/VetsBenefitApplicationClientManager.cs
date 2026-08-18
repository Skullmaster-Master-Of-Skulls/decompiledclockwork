using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using TechnoPro.ClockWorkServer.Contracts.DTO.CustomForms.Data;
using TechnoPro.ClockWorkServer.Contracts.DTO.Vets;
using TechnoPro.Common.ClientManager.ICore.Vets;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities.Vets;

namespace TechnoPro.Common.ClientManager.Core.Vets
{
	// Token: 0x02000006 RID: 6
	public class VetsBenefitApplicationClientManager : IVetsBenefitApplicationClientManager, IWebService
	{
		// Token: 0x06000028 RID: 40 RVA: 0x00002A64 File Offset: 0x00000C64
		[DebuggerStepThrough]
		public Task<VetsBenefitApplicationDTO> LoadBenefitApplicationByIdAsync(Guid BenefitApplicationId)
		{
			VetsBenefitApplicationClientManager.<LoadBenefitApplicationByIdAsync>d__0 <LoadBenefitApplicationByIdAsync>d__ = new VetsBenefitApplicationClientManager.<LoadBenefitApplicationByIdAsync>d__0();
			<LoadBenefitApplicationByIdAsync>d__.<>t__builder = AsyncTaskMethodBuilder<VetsBenefitApplicationDTO>.Create();
			<LoadBenefitApplicationByIdAsync>d__.<>4__this = this;
			<LoadBenefitApplicationByIdAsync>d__.BenefitApplicationId = BenefitApplicationId;
			<LoadBenefitApplicationByIdAsync>d__.<>1__state = -1;
			<LoadBenefitApplicationByIdAsync>d__.<>t__builder.Start<VetsBenefitApplicationClientManager.<LoadBenefitApplicationByIdAsync>d__0>(ref <LoadBenefitApplicationByIdAsync>d__);
			return <LoadBenefitApplicationByIdAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00002AB0 File Offset: 0x00000CB0
		[DebuggerStepThrough]
		public Task<VetsBenefitApplicationDTO> LoadBenefitApplicationBaseAndSingleStepDataAsync(Guid BenefitApplicationId, eVetsBenefitApplicationStep? preferredStep)
		{
			VetsBenefitApplicationClientManager.<LoadBenefitApplicationBaseAndSingleStepDataAsync>d__1 <LoadBenefitApplicationBaseAndSingleStepDataAsync>d__ = new VetsBenefitApplicationClientManager.<LoadBenefitApplicationBaseAndSingleStepDataAsync>d__1();
			<LoadBenefitApplicationBaseAndSingleStepDataAsync>d__.<>t__builder = AsyncTaskMethodBuilder<VetsBenefitApplicationDTO>.Create();
			<LoadBenefitApplicationBaseAndSingleStepDataAsync>d__.<>4__this = this;
			<LoadBenefitApplicationBaseAndSingleStepDataAsync>d__.BenefitApplicationId = BenefitApplicationId;
			<LoadBenefitApplicationBaseAndSingleStepDataAsync>d__.preferredStep = preferredStep;
			<LoadBenefitApplicationBaseAndSingleStepDataAsync>d__.<>1__state = -1;
			<LoadBenefitApplicationBaseAndSingleStepDataAsync>d__.<>t__builder.Start<VetsBenefitApplicationClientManager.<LoadBenefitApplicationBaseAndSingleStepDataAsync>d__1>(ref <LoadBenefitApplicationBaseAndSingleStepDataAsync>d__);
			return <LoadBenefitApplicationBaseAndSingleStepDataAsync>d__.<>t__builder.Task;
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00002B04 File Offset: 0x00000D04
		[DebuggerStepThrough]
		public Task SaveVetsChapterAsync(Guid benefitApplicationId, Guid chapterId)
		{
			VetsBenefitApplicationClientManager.<SaveVetsChapterAsync>d__2 <SaveVetsChapterAsync>d__ = new VetsBenefitApplicationClientManager.<SaveVetsChapterAsync>d__2();
			<SaveVetsChapterAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<SaveVetsChapterAsync>d__.<>4__this = this;
			<SaveVetsChapterAsync>d__.benefitApplicationId = benefitApplicationId;
			<SaveVetsChapterAsync>d__.chapterId = chapterId;
			<SaveVetsChapterAsync>d__.<>1__state = -1;
			<SaveVetsChapterAsync>d__.<>t__builder.Start<VetsBenefitApplicationClientManager.<SaveVetsChapterAsync>d__2>(ref <SaveVetsChapterAsync>d__);
			return <SaveVetsChapterAsync>d__.<>t__builder.Task;
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00002B58 File Offset: 0x00000D58
		[DebuggerStepThrough]
		public Task SaveVetsRegistrationDataAsync(Guid benefitApplicationId, bool completedRegistration, int personId, IList<CustomDataHolderCollectionDTO> data, params Guid[] dataInstanceIds)
		{
			VetsBenefitApplicationClientManager.<SaveVetsRegistrationDataAsync>d__3 <SaveVetsRegistrationDataAsync>d__ = new VetsBenefitApplicationClientManager.<SaveVetsRegistrationDataAsync>d__3();
			<SaveVetsRegistrationDataAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<SaveVetsRegistrationDataAsync>d__.<>4__this = this;
			<SaveVetsRegistrationDataAsync>d__.benefitApplicationId = benefitApplicationId;
			<SaveVetsRegistrationDataAsync>d__.completedRegistration = completedRegistration;
			<SaveVetsRegistrationDataAsync>d__.personId = personId;
			<SaveVetsRegistrationDataAsync>d__.data = data;
			<SaveVetsRegistrationDataAsync>d__.dataInstanceIds = dataInstanceIds;
			<SaveVetsRegistrationDataAsync>d__.<>1__state = -1;
			<SaveVetsRegistrationDataAsync>d__.<>t__builder.Start<VetsBenefitApplicationClientManager.<SaveVetsRegistrationDataAsync>d__3>(ref <SaveVetsRegistrationDataAsync>d__);
			return <SaveVetsRegistrationDataAsync>d__.<>t__builder.Task;
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00002BC4 File Offset: 0x00000DC4
		[DebuggerStepThrough]
		public Task SaveVetsBenAppDataAsync(Guid benefitApplicationId, bool completedBenApp, int personId, int semesterId, IList<CustomDataHolderCollectionDTO> data, params Guid[] dataInstanceIds)
		{
			VetsBenefitApplicationClientManager.<SaveVetsBenAppDataAsync>d__4 <SaveVetsBenAppDataAsync>d__ = new VetsBenefitApplicationClientManager.<SaveVetsBenAppDataAsync>d__4();
			<SaveVetsBenAppDataAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<SaveVetsBenAppDataAsync>d__.<>4__this = this;
			<SaveVetsBenAppDataAsync>d__.benefitApplicationId = benefitApplicationId;
			<SaveVetsBenAppDataAsync>d__.completedBenApp = completedBenApp;
			<SaveVetsBenAppDataAsync>d__.personId = personId;
			<SaveVetsBenAppDataAsync>d__.semesterId = semesterId;
			<SaveVetsBenAppDataAsync>d__.data = data;
			<SaveVetsBenAppDataAsync>d__.dataInstanceIds = dataInstanceIds;
			<SaveVetsBenAppDataAsync>d__.<>1__state = -1;
			<SaveVetsBenAppDataAsync>d__.<>t__builder.Start<VetsBenefitApplicationClientManager.<SaveVetsBenAppDataAsync>d__4>(ref <SaveVetsBenAppDataAsync>d__);
			return <SaveVetsBenAppDataAsync>d__.<>t__builder.Task;
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00002C38 File Offset: 0x00000E38
		[DebuggerStepThrough]
		public Task SaveVetsStudentAgreeDataAsync(Guid benefitApplicationId, bool completedStudentAgree, int personId, int semesterId, IList<CustomDataHolderCollectionDTO> data, params Guid[] dataInstanceIds)
		{
			VetsBenefitApplicationClientManager.<SaveVetsStudentAgreeDataAsync>d__5 <SaveVetsStudentAgreeDataAsync>d__ = new VetsBenefitApplicationClientManager.<SaveVetsStudentAgreeDataAsync>d__5();
			<SaveVetsStudentAgreeDataAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<SaveVetsStudentAgreeDataAsync>d__.<>4__this = this;
			<SaveVetsStudentAgreeDataAsync>d__.benefitApplicationId = benefitApplicationId;
			<SaveVetsStudentAgreeDataAsync>d__.completedStudentAgree = completedStudentAgree;
			<SaveVetsStudentAgreeDataAsync>d__.personId = personId;
			<SaveVetsStudentAgreeDataAsync>d__.semesterId = semesterId;
			<SaveVetsStudentAgreeDataAsync>d__.data = data;
			<SaveVetsStudentAgreeDataAsync>d__.dataInstanceIds = dataInstanceIds;
			<SaveVetsStudentAgreeDataAsync>d__.<>1__state = -1;
			<SaveVetsStudentAgreeDataAsync>d__.<>t__builder.Start<VetsBenefitApplicationClientManager.<SaveVetsStudentAgreeDataAsync>d__5>(ref <SaveVetsStudentAgreeDataAsync>d__);
			return <SaveVetsStudentAgreeDataAsync>d__.<>t__builder.Task;
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00002CAC File Offset: 0x00000EAC
		[DebuggerStepThrough]
		public Task<Guid?> CreateVetsBenefitApplicationAsync(int personId, int semesterId)
		{
			VetsBenefitApplicationClientManager.<CreateVetsBenefitApplicationAsync>d__6 <CreateVetsBenefitApplicationAsync>d__ = new VetsBenefitApplicationClientManager.<CreateVetsBenefitApplicationAsync>d__6();
			<CreateVetsBenefitApplicationAsync>d__.<>t__builder = AsyncTaskMethodBuilder<Guid?>.Create();
			<CreateVetsBenefitApplicationAsync>d__.<>4__this = this;
			<CreateVetsBenefitApplicationAsync>d__.personId = personId;
			<CreateVetsBenefitApplicationAsync>d__.semesterId = semesterId;
			<CreateVetsBenefitApplicationAsync>d__.<>1__state = -1;
			<CreateVetsBenefitApplicationAsync>d__.<>t__builder.Start<VetsBenefitApplicationClientManager.<CreateVetsBenefitApplicationAsync>d__6>(ref <CreateVetsBenefitApplicationAsync>d__);
			return <CreateVetsBenefitApplicationAsync>d__.<>t__builder.Task;
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00002D00 File Offset: 0x00000F00
		[DebuggerStepThrough]
		public Task<Guid?> CreateVetsBenefitApplicationCurrentSemesterAsync(int personId)
		{
			VetsBenefitApplicationClientManager.<CreateVetsBenefitApplicationCurrentSemesterAsync>d__7 <CreateVetsBenefitApplicationCurrentSemesterAsync>d__ = new VetsBenefitApplicationClientManager.<CreateVetsBenefitApplicationCurrentSemesterAsync>d__7();
			<CreateVetsBenefitApplicationCurrentSemesterAsync>d__.<>t__builder = AsyncTaskMethodBuilder<Guid?>.Create();
			<CreateVetsBenefitApplicationCurrentSemesterAsync>d__.<>4__this = this;
			<CreateVetsBenefitApplicationCurrentSemesterAsync>d__.personId = personId;
			<CreateVetsBenefitApplicationCurrentSemesterAsync>d__.<>1__state = -1;
			<CreateVetsBenefitApplicationCurrentSemesterAsync>d__.<>t__builder.Start<VetsBenefitApplicationClientManager.<CreateVetsBenefitApplicationCurrentSemesterAsync>d__7>(ref <CreateVetsBenefitApplicationCurrentSemesterAsync>d__);
			return <CreateVetsBenefitApplicationCurrentSemesterAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00002D4C File Offset: 0x00000F4C
		[DebuggerStepThrough]
		public Task<Guid?> CreateVetsBenefitApplicationNextSemesterAsync(int personId)
		{
			VetsBenefitApplicationClientManager.<CreateVetsBenefitApplicationNextSemesterAsync>d__8 <CreateVetsBenefitApplicationNextSemesterAsync>d__ = new VetsBenefitApplicationClientManager.<CreateVetsBenefitApplicationNextSemesterAsync>d__8();
			<CreateVetsBenefitApplicationNextSemesterAsync>d__.<>t__builder = AsyncTaskMethodBuilder<Guid?>.Create();
			<CreateVetsBenefitApplicationNextSemesterAsync>d__.<>4__this = this;
			<CreateVetsBenefitApplicationNextSemesterAsync>d__.personId = personId;
			<CreateVetsBenefitApplicationNextSemesterAsync>d__.<>1__state = -1;
			<CreateVetsBenefitApplicationNextSemesterAsync>d__.<>t__builder.Start<VetsBenefitApplicationClientManager.<CreateVetsBenefitApplicationNextSemesterAsync>d__8>(ref <CreateVetsBenefitApplicationNextSemesterAsync>d__);
			return <CreateVetsBenefitApplicationNextSemesterAsync>d__.<>t__builder.Task;
		}
	}
}
