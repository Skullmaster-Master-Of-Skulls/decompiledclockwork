using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Threading.Tasks;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.Vets;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x02000164 RID: 356
	internal class VetsBenefitApplicationClientBaseProxy : ClientBase<IVetsBenefitApplication>, IVetsBenefitApplication, IService
	{
		// Token: 0x06000DB4 RID: 3508 RVA: 0x00021F67 File Offset: 0x00020167
		public VetsBenefitApplicationClientBaseProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x06000DB5 RID: 3509 RVA: 0x00021F72 File Offset: 0x00020172
		public VetsBenefitApplicationClientBaseProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x06000DB6 RID: 3510 RVA: 0x00021F80 File Offset: 0x00020180
		[DebuggerStepThrough]
		public Task<LoadBenefitApplicationByIdResp> LoadBenefitApplicationByIdAsync(LoadBenefitApplicationByIdReq Request)
		{
			VetsBenefitApplicationClientBaseProxy.<LoadBenefitApplicationByIdAsync>d__2 <LoadBenefitApplicationByIdAsync>d__ = new VetsBenefitApplicationClientBaseProxy.<LoadBenefitApplicationByIdAsync>d__2();
			<LoadBenefitApplicationByIdAsync>d__.<>t__builder = AsyncTaskMethodBuilder<LoadBenefitApplicationByIdResp>.Create();
			<LoadBenefitApplicationByIdAsync>d__.<>4__this = this;
			<LoadBenefitApplicationByIdAsync>d__.Request = Request;
			<LoadBenefitApplicationByIdAsync>d__.<>1__state = -1;
			<LoadBenefitApplicationByIdAsync>d__.<>t__builder.Start<VetsBenefitApplicationClientBaseProxy.<LoadBenefitApplicationByIdAsync>d__2>(ref <LoadBenefitApplicationByIdAsync>d__);
			return <LoadBenefitApplicationByIdAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000DB7 RID: 3511 RVA: 0x00021FCC File Offset: 0x000201CC
		[DebuggerStepThrough]
		public Task<LoadBenefitApplicationBaseAndSingleStepDataResp> LoadBenefitApplicationBaseAndSingleStepData(LoadBenefitApplicationBaseAndSingleStepDataReq Request)
		{
			VetsBenefitApplicationClientBaseProxy.<LoadBenefitApplicationBaseAndSingleStepData>d__3 <LoadBenefitApplicationBaseAndSingleStepData>d__ = new VetsBenefitApplicationClientBaseProxy.<LoadBenefitApplicationBaseAndSingleStepData>d__3();
			<LoadBenefitApplicationBaseAndSingleStepData>d__.<>t__builder = AsyncTaskMethodBuilder<LoadBenefitApplicationBaseAndSingleStepDataResp>.Create();
			<LoadBenefitApplicationBaseAndSingleStepData>d__.<>4__this = this;
			<LoadBenefitApplicationBaseAndSingleStepData>d__.Request = Request;
			<LoadBenefitApplicationBaseAndSingleStepData>d__.<>1__state = -1;
			<LoadBenefitApplicationBaseAndSingleStepData>d__.<>t__builder.Start<VetsBenefitApplicationClientBaseProxy.<LoadBenefitApplicationBaseAndSingleStepData>d__3>(ref <LoadBenefitApplicationBaseAndSingleStepData>d__);
			return <LoadBenefitApplicationBaseAndSingleStepData>d__.<>t__builder.Task;
		}

		// Token: 0x06000DB8 RID: 3512 RVA: 0x00022018 File Offset: 0x00020218
		[DebuggerStepThrough]
		public Task<SaveVetsChapterResp> SaveVetsChapterAsync(SaveVetsChapterReq Request)
		{
			VetsBenefitApplicationClientBaseProxy.<SaveVetsChapterAsync>d__4 <SaveVetsChapterAsync>d__ = new VetsBenefitApplicationClientBaseProxy.<SaveVetsChapterAsync>d__4();
			<SaveVetsChapterAsync>d__.<>t__builder = AsyncTaskMethodBuilder<SaveVetsChapterResp>.Create();
			<SaveVetsChapterAsync>d__.<>4__this = this;
			<SaveVetsChapterAsync>d__.Request = Request;
			<SaveVetsChapterAsync>d__.<>1__state = -1;
			<SaveVetsChapterAsync>d__.<>t__builder.Start<VetsBenefitApplicationClientBaseProxy.<SaveVetsChapterAsync>d__4>(ref <SaveVetsChapterAsync>d__);
			return <SaveVetsChapterAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000DB9 RID: 3513 RVA: 0x00022064 File Offset: 0x00020264
		[DebuggerStepThrough]
		public Task<SaveVetsRegistrationDataResp> SaveVetsRegistrationDataAsync(SaveVetsRegistrationDataReq Request)
		{
			VetsBenefitApplicationClientBaseProxy.<SaveVetsRegistrationDataAsync>d__5 <SaveVetsRegistrationDataAsync>d__ = new VetsBenefitApplicationClientBaseProxy.<SaveVetsRegistrationDataAsync>d__5();
			<SaveVetsRegistrationDataAsync>d__.<>t__builder = AsyncTaskMethodBuilder<SaveVetsRegistrationDataResp>.Create();
			<SaveVetsRegistrationDataAsync>d__.<>4__this = this;
			<SaveVetsRegistrationDataAsync>d__.Request = Request;
			<SaveVetsRegistrationDataAsync>d__.<>1__state = -1;
			<SaveVetsRegistrationDataAsync>d__.<>t__builder.Start<VetsBenefitApplicationClientBaseProxy.<SaveVetsRegistrationDataAsync>d__5>(ref <SaveVetsRegistrationDataAsync>d__);
			return <SaveVetsRegistrationDataAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000DBA RID: 3514 RVA: 0x000220B0 File Offset: 0x000202B0
		[DebuggerStepThrough]
		public Task<SaveVetsBenAppDataResp> SaveVetsBenAppDataAsync(SaveVetsBenAppDataReq Request)
		{
			VetsBenefitApplicationClientBaseProxy.<SaveVetsBenAppDataAsync>d__6 <SaveVetsBenAppDataAsync>d__ = new VetsBenefitApplicationClientBaseProxy.<SaveVetsBenAppDataAsync>d__6();
			<SaveVetsBenAppDataAsync>d__.<>t__builder = AsyncTaskMethodBuilder<SaveVetsBenAppDataResp>.Create();
			<SaveVetsBenAppDataAsync>d__.<>4__this = this;
			<SaveVetsBenAppDataAsync>d__.Request = Request;
			<SaveVetsBenAppDataAsync>d__.<>1__state = -1;
			<SaveVetsBenAppDataAsync>d__.<>t__builder.Start<VetsBenefitApplicationClientBaseProxy.<SaveVetsBenAppDataAsync>d__6>(ref <SaveVetsBenAppDataAsync>d__);
			return <SaveVetsBenAppDataAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000DBB RID: 3515 RVA: 0x000220FC File Offset: 0x000202FC
		[DebuggerStepThrough]
		public Task<SaveVetsStudentAgreeDataResp> SaveVetsStudentAgreeDataAsync(SaveVetsStudentAgreeDataReq Request)
		{
			VetsBenefitApplicationClientBaseProxy.<SaveVetsStudentAgreeDataAsync>d__7 <SaveVetsStudentAgreeDataAsync>d__ = new VetsBenefitApplicationClientBaseProxy.<SaveVetsStudentAgreeDataAsync>d__7();
			<SaveVetsStudentAgreeDataAsync>d__.<>t__builder = AsyncTaskMethodBuilder<SaveVetsStudentAgreeDataResp>.Create();
			<SaveVetsStudentAgreeDataAsync>d__.<>4__this = this;
			<SaveVetsStudentAgreeDataAsync>d__.Request = Request;
			<SaveVetsStudentAgreeDataAsync>d__.<>1__state = -1;
			<SaveVetsStudentAgreeDataAsync>d__.<>t__builder.Start<VetsBenefitApplicationClientBaseProxy.<SaveVetsStudentAgreeDataAsync>d__7>(ref <SaveVetsStudentAgreeDataAsync>d__);
			return <SaveVetsStudentAgreeDataAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000DBC RID: 3516 RVA: 0x00022148 File Offset: 0x00020348
		[DebuggerStepThrough]
		public Task<CreateVetsBenefitApplicationResp> CreateVetsBenefitApplicationAsync(CreateVetsBenefitApplicationReq Request)
		{
			VetsBenefitApplicationClientBaseProxy.<CreateVetsBenefitApplicationAsync>d__8 <CreateVetsBenefitApplicationAsync>d__ = new VetsBenefitApplicationClientBaseProxy.<CreateVetsBenefitApplicationAsync>d__8();
			<CreateVetsBenefitApplicationAsync>d__.<>t__builder = AsyncTaskMethodBuilder<CreateVetsBenefitApplicationResp>.Create();
			<CreateVetsBenefitApplicationAsync>d__.<>4__this = this;
			<CreateVetsBenefitApplicationAsync>d__.Request = Request;
			<CreateVetsBenefitApplicationAsync>d__.<>1__state = -1;
			<CreateVetsBenefitApplicationAsync>d__.<>t__builder.Start<VetsBenefitApplicationClientBaseProxy.<CreateVetsBenefitApplicationAsync>d__8>(ref <CreateVetsBenefitApplicationAsync>d__);
			return <CreateVetsBenefitApplicationAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000DBD RID: 3517 RVA: 0x00022194 File Offset: 0x00020394
		[DebuggerStepThrough]
		public Task<CreateVetsBenefitApplicationCurrentSemesterResp> CreateVetsBenefitApplicationCurrentSemesterAsync(CreateVetsBenefitApplicationCurrentSemesterReq Request)
		{
			VetsBenefitApplicationClientBaseProxy.<CreateVetsBenefitApplicationCurrentSemesterAsync>d__9 <CreateVetsBenefitApplicationCurrentSemesterAsync>d__ = new VetsBenefitApplicationClientBaseProxy.<CreateVetsBenefitApplicationCurrentSemesterAsync>d__9();
			<CreateVetsBenefitApplicationCurrentSemesterAsync>d__.<>t__builder = AsyncTaskMethodBuilder<CreateVetsBenefitApplicationCurrentSemesterResp>.Create();
			<CreateVetsBenefitApplicationCurrentSemesterAsync>d__.<>4__this = this;
			<CreateVetsBenefitApplicationCurrentSemesterAsync>d__.Request = Request;
			<CreateVetsBenefitApplicationCurrentSemesterAsync>d__.<>1__state = -1;
			<CreateVetsBenefitApplicationCurrentSemesterAsync>d__.<>t__builder.Start<VetsBenefitApplicationClientBaseProxy.<CreateVetsBenefitApplicationCurrentSemesterAsync>d__9>(ref <CreateVetsBenefitApplicationCurrentSemesterAsync>d__);
			return <CreateVetsBenefitApplicationCurrentSemesterAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000DBE RID: 3518 RVA: 0x000221E0 File Offset: 0x000203E0
		[DebuggerStepThrough]
		public Task<CreateVetsBenefitApplicationNextSemesterResp> CreateVetsBenefitApplicationNextSemesterAsync(CreateVetsBenefitApplicationNextSemesterReq Request)
		{
			VetsBenefitApplicationClientBaseProxy.<CreateVetsBenefitApplicationNextSemesterAsync>d__10 <CreateVetsBenefitApplicationNextSemesterAsync>d__ = new VetsBenefitApplicationClientBaseProxy.<CreateVetsBenefitApplicationNextSemesterAsync>d__10();
			<CreateVetsBenefitApplicationNextSemesterAsync>d__.<>t__builder = AsyncTaskMethodBuilder<CreateVetsBenefitApplicationNextSemesterResp>.Create();
			<CreateVetsBenefitApplicationNextSemesterAsync>d__.<>4__this = this;
			<CreateVetsBenefitApplicationNextSemesterAsync>d__.Request = Request;
			<CreateVetsBenefitApplicationNextSemesterAsync>d__.<>1__state = -1;
			<CreateVetsBenefitApplicationNextSemesterAsync>d__.<>t__builder.Start<VetsBenefitApplicationClientBaseProxy.<CreateVetsBenefitApplicationNextSemesterAsync>d__10>(ref <CreateVetsBenefitApplicationNextSemesterAsync>d__);
			return <CreateVetsBenefitApplicationNextSemesterAsync>d__.<>t__builder.Task;
		}
	}
}
