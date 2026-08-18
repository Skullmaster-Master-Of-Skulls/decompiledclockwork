using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.Vets;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Common.Services.Impl
{
	// Token: 0x020000A1 RID: 161
	public class VetsBenefitApplicationServiceManager : IVetsBenefitApplication, IService
	{
		// Token: 0x060005DC RID: 1500 RVA: 0x0001B254 File Offset: 0x00019454
		[DebuggerStepThrough]
		public Task<LoadBenefitApplicationByIdResp> LoadBenefitApplicationByIdAsync(LoadBenefitApplicationByIdReq Request)
		{
			VetsBenefitApplicationServiceManager.<LoadBenefitApplicationByIdAsync>d__0 <LoadBenefitApplicationByIdAsync>d__ = new VetsBenefitApplicationServiceManager.<LoadBenefitApplicationByIdAsync>d__0();
			<LoadBenefitApplicationByIdAsync>d__.<>t__builder = AsyncTaskMethodBuilder<LoadBenefitApplicationByIdResp>.Create();
			<LoadBenefitApplicationByIdAsync>d__.<>4__this = this;
			<LoadBenefitApplicationByIdAsync>d__.Request = Request;
			<LoadBenefitApplicationByIdAsync>d__.<>1__state = -1;
			<LoadBenefitApplicationByIdAsync>d__.<>t__builder.Start<VetsBenefitApplicationServiceManager.<LoadBenefitApplicationByIdAsync>d__0>(ref <LoadBenefitApplicationByIdAsync>d__);
			return <LoadBenefitApplicationByIdAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060005DD RID: 1501 RVA: 0x0001B2A0 File Offset: 0x000194A0
		[DebuggerStepThrough]
		public Task<LoadBenefitApplicationBaseAndSingleStepDataResp> LoadBenefitApplicationBaseAndSingleStepData(LoadBenefitApplicationBaseAndSingleStepDataReq Request)
		{
			VetsBenefitApplicationServiceManager.<LoadBenefitApplicationBaseAndSingleStepData>d__1 <LoadBenefitApplicationBaseAndSingleStepData>d__ = new VetsBenefitApplicationServiceManager.<LoadBenefitApplicationBaseAndSingleStepData>d__1();
			<LoadBenefitApplicationBaseAndSingleStepData>d__.<>t__builder = AsyncTaskMethodBuilder<LoadBenefitApplicationBaseAndSingleStepDataResp>.Create();
			<LoadBenefitApplicationBaseAndSingleStepData>d__.<>4__this = this;
			<LoadBenefitApplicationBaseAndSingleStepData>d__.Request = Request;
			<LoadBenefitApplicationBaseAndSingleStepData>d__.<>1__state = -1;
			<LoadBenefitApplicationBaseAndSingleStepData>d__.<>t__builder.Start<VetsBenefitApplicationServiceManager.<LoadBenefitApplicationBaseAndSingleStepData>d__1>(ref <LoadBenefitApplicationBaseAndSingleStepData>d__);
			return <LoadBenefitApplicationBaseAndSingleStepData>d__.<>t__builder.Task;
		}

		// Token: 0x060005DE RID: 1502 RVA: 0x0001B2EC File Offset: 0x000194EC
		[DebuggerStepThrough]
		public Task<SaveVetsChapterResp> SaveVetsChapterAsync(SaveVetsChapterReq Request)
		{
			VetsBenefitApplicationServiceManager.<SaveVetsChapterAsync>d__2 <SaveVetsChapterAsync>d__ = new VetsBenefitApplicationServiceManager.<SaveVetsChapterAsync>d__2();
			<SaveVetsChapterAsync>d__.<>t__builder = AsyncTaskMethodBuilder<SaveVetsChapterResp>.Create();
			<SaveVetsChapterAsync>d__.<>4__this = this;
			<SaveVetsChapterAsync>d__.Request = Request;
			<SaveVetsChapterAsync>d__.<>1__state = -1;
			<SaveVetsChapterAsync>d__.<>t__builder.Start<VetsBenefitApplicationServiceManager.<SaveVetsChapterAsync>d__2>(ref <SaveVetsChapterAsync>d__);
			return <SaveVetsChapterAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060005DF RID: 1503 RVA: 0x0001B338 File Offset: 0x00019538
		[DebuggerStepThrough]
		public Task<SaveVetsRegistrationDataResp> SaveVetsRegistrationDataAsync(SaveVetsRegistrationDataReq Request)
		{
			VetsBenefitApplicationServiceManager.<SaveVetsRegistrationDataAsync>d__3 <SaveVetsRegistrationDataAsync>d__ = new VetsBenefitApplicationServiceManager.<SaveVetsRegistrationDataAsync>d__3();
			<SaveVetsRegistrationDataAsync>d__.<>t__builder = AsyncTaskMethodBuilder<SaveVetsRegistrationDataResp>.Create();
			<SaveVetsRegistrationDataAsync>d__.<>4__this = this;
			<SaveVetsRegistrationDataAsync>d__.Request = Request;
			<SaveVetsRegistrationDataAsync>d__.<>1__state = -1;
			<SaveVetsRegistrationDataAsync>d__.<>t__builder.Start<VetsBenefitApplicationServiceManager.<SaveVetsRegistrationDataAsync>d__3>(ref <SaveVetsRegistrationDataAsync>d__);
			return <SaveVetsRegistrationDataAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060005E0 RID: 1504 RVA: 0x0001B384 File Offset: 0x00019584
		[DebuggerStepThrough]
		public Task<SaveVetsBenAppDataResp> SaveVetsBenAppDataAsync(SaveVetsBenAppDataReq Request)
		{
			VetsBenefitApplicationServiceManager.<SaveVetsBenAppDataAsync>d__4 <SaveVetsBenAppDataAsync>d__ = new VetsBenefitApplicationServiceManager.<SaveVetsBenAppDataAsync>d__4();
			<SaveVetsBenAppDataAsync>d__.<>t__builder = AsyncTaskMethodBuilder<SaveVetsBenAppDataResp>.Create();
			<SaveVetsBenAppDataAsync>d__.<>4__this = this;
			<SaveVetsBenAppDataAsync>d__.Request = Request;
			<SaveVetsBenAppDataAsync>d__.<>1__state = -1;
			<SaveVetsBenAppDataAsync>d__.<>t__builder.Start<VetsBenefitApplicationServiceManager.<SaveVetsBenAppDataAsync>d__4>(ref <SaveVetsBenAppDataAsync>d__);
			return <SaveVetsBenAppDataAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060005E1 RID: 1505 RVA: 0x0001B3D0 File Offset: 0x000195D0
		[DebuggerStepThrough]
		public Task<SaveVetsStudentAgreeDataResp> SaveVetsStudentAgreeDataAsync(SaveVetsStudentAgreeDataReq Request)
		{
			VetsBenefitApplicationServiceManager.<SaveVetsStudentAgreeDataAsync>d__5 <SaveVetsStudentAgreeDataAsync>d__ = new VetsBenefitApplicationServiceManager.<SaveVetsStudentAgreeDataAsync>d__5();
			<SaveVetsStudentAgreeDataAsync>d__.<>t__builder = AsyncTaskMethodBuilder<SaveVetsStudentAgreeDataResp>.Create();
			<SaveVetsStudentAgreeDataAsync>d__.<>4__this = this;
			<SaveVetsStudentAgreeDataAsync>d__.Request = Request;
			<SaveVetsStudentAgreeDataAsync>d__.<>1__state = -1;
			<SaveVetsStudentAgreeDataAsync>d__.<>t__builder.Start<VetsBenefitApplicationServiceManager.<SaveVetsStudentAgreeDataAsync>d__5>(ref <SaveVetsStudentAgreeDataAsync>d__);
			return <SaveVetsStudentAgreeDataAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060005E2 RID: 1506 RVA: 0x0001B41C File Offset: 0x0001961C
		[DebuggerStepThrough]
		public Task<CreateVetsBenefitApplicationResp> CreateVetsBenefitApplicationAsync(CreateVetsBenefitApplicationReq Request)
		{
			VetsBenefitApplicationServiceManager.<CreateVetsBenefitApplicationAsync>d__6 <CreateVetsBenefitApplicationAsync>d__ = new VetsBenefitApplicationServiceManager.<CreateVetsBenefitApplicationAsync>d__6();
			<CreateVetsBenefitApplicationAsync>d__.<>t__builder = AsyncTaskMethodBuilder<CreateVetsBenefitApplicationResp>.Create();
			<CreateVetsBenefitApplicationAsync>d__.<>4__this = this;
			<CreateVetsBenefitApplicationAsync>d__.Request = Request;
			<CreateVetsBenefitApplicationAsync>d__.<>1__state = -1;
			<CreateVetsBenefitApplicationAsync>d__.<>t__builder.Start<VetsBenefitApplicationServiceManager.<CreateVetsBenefitApplicationAsync>d__6>(ref <CreateVetsBenefitApplicationAsync>d__);
			return <CreateVetsBenefitApplicationAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060005E3 RID: 1507 RVA: 0x0001B468 File Offset: 0x00019668
		[DebuggerStepThrough]
		public Task<CreateVetsBenefitApplicationCurrentSemesterResp> CreateVetsBenefitApplicationCurrentSemesterAsync(CreateVetsBenefitApplicationCurrentSemesterReq Request)
		{
			VetsBenefitApplicationServiceManager.<CreateVetsBenefitApplicationCurrentSemesterAsync>d__7 <CreateVetsBenefitApplicationCurrentSemesterAsync>d__ = new VetsBenefitApplicationServiceManager.<CreateVetsBenefitApplicationCurrentSemesterAsync>d__7();
			<CreateVetsBenefitApplicationCurrentSemesterAsync>d__.<>t__builder = AsyncTaskMethodBuilder<CreateVetsBenefitApplicationCurrentSemesterResp>.Create();
			<CreateVetsBenefitApplicationCurrentSemesterAsync>d__.<>4__this = this;
			<CreateVetsBenefitApplicationCurrentSemesterAsync>d__.Request = Request;
			<CreateVetsBenefitApplicationCurrentSemesterAsync>d__.<>1__state = -1;
			<CreateVetsBenefitApplicationCurrentSemesterAsync>d__.<>t__builder.Start<VetsBenefitApplicationServiceManager.<CreateVetsBenefitApplicationCurrentSemesterAsync>d__7>(ref <CreateVetsBenefitApplicationCurrentSemesterAsync>d__);
			return <CreateVetsBenefitApplicationCurrentSemesterAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060005E4 RID: 1508 RVA: 0x0001B4B4 File Offset: 0x000196B4
		[DebuggerStepThrough]
		public Task<CreateVetsBenefitApplicationNextSemesterResp> CreateVetsBenefitApplicationNextSemesterAsync(CreateVetsBenefitApplicationNextSemesterReq Request)
		{
			VetsBenefitApplicationServiceManager.<CreateVetsBenefitApplicationNextSemesterAsync>d__8 <CreateVetsBenefitApplicationNextSemesterAsync>d__ = new VetsBenefitApplicationServiceManager.<CreateVetsBenefitApplicationNextSemesterAsync>d__8();
			<CreateVetsBenefitApplicationNextSemesterAsync>d__.<>t__builder = AsyncTaskMethodBuilder<CreateVetsBenefitApplicationNextSemesterResp>.Create();
			<CreateVetsBenefitApplicationNextSemesterAsync>d__.<>4__this = this;
			<CreateVetsBenefitApplicationNextSemesterAsync>d__.Request = Request;
			<CreateVetsBenefitApplicationNextSemesterAsync>d__.<>1__state = -1;
			<CreateVetsBenefitApplicationNextSemesterAsync>d__.<>t__builder.Start<VetsBenefitApplicationServiceManager.<CreateVetsBenefitApplicationNextSemesterAsync>d__8>(ref <CreateVetsBenefitApplicationNextSemesterAsync>d__);
			return <CreateVetsBenefitApplicationNextSemesterAsync>d__.<>t__builder.Task;
		}
	}
}
