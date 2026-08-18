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
	// Token: 0x02000163 RID: 355
	public class VetsBenefitApplicationReusableClientProxy : WCFTokenBasedReusableClientProxy<IVetsBenefitApplication>, IVetsBenefitApplication, IService
	{
		// Token: 0x06000DA9 RID: 3497 RVA: 0x00021CA2 File Offset: 0x0001FEA2
		public VetsBenefitApplicationReusableClientProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x06000DAA RID: 3498 RVA: 0x00021CAD File Offset: 0x0001FEAD
		public VetsBenefitApplicationReusableClientProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x06000DAB RID: 3499 RVA: 0x00021CBC File Offset: 0x0001FEBC
		[DebuggerStepThrough]
		public Task<LoadBenefitApplicationByIdResp> LoadBenefitApplicationByIdAsync(LoadBenefitApplicationByIdReq Request)
		{
			VetsBenefitApplicationReusableClientProxy.<LoadBenefitApplicationByIdAsync>d__2 <LoadBenefitApplicationByIdAsync>d__ = new VetsBenefitApplicationReusableClientProxy.<LoadBenefitApplicationByIdAsync>d__2();
			<LoadBenefitApplicationByIdAsync>d__.<>t__builder = AsyncTaskMethodBuilder<LoadBenefitApplicationByIdResp>.Create();
			<LoadBenefitApplicationByIdAsync>d__.<>4__this = this;
			<LoadBenefitApplicationByIdAsync>d__.Request = Request;
			<LoadBenefitApplicationByIdAsync>d__.<>1__state = -1;
			<LoadBenefitApplicationByIdAsync>d__.<>t__builder.Start<VetsBenefitApplicationReusableClientProxy.<LoadBenefitApplicationByIdAsync>d__2>(ref <LoadBenefitApplicationByIdAsync>d__);
			return <LoadBenefitApplicationByIdAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000DAC RID: 3500 RVA: 0x00021D08 File Offset: 0x0001FF08
		[DebuggerStepThrough]
		public Task<LoadBenefitApplicationBaseAndSingleStepDataResp> LoadBenefitApplicationBaseAndSingleStepData(LoadBenefitApplicationBaseAndSingleStepDataReq Request)
		{
			VetsBenefitApplicationReusableClientProxy.<LoadBenefitApplicationBaseAndSingleStepData>d__3 <LoadBenefitApplicationBaseAndSingleStepData>d__ = new VetsBenefitApplicationReusableClientProxy.<LoadBenefitApplicationBaseAndSingleStepData>d__3();
			<LoadBenefitApplicationBaseAndSingleStepData>d__.<>t__builder = AsyncTaskMethodBuilder<LoadBenefitApplicationBaseAndSingleStepDataResp>.Create();
			<LoadBenefitApplicationBaseAndSingleStepData>d__.<>4__this = this;
			<LoadBenefitApplicationBaseAndSingleStepData>d__.Request = Request;
			<LoadBenefitApplicationBaseAndSingleStepData>d__.<>1__state = -1;
			<LoadBenefitApplicationBaseAndSingleStepData>d__.<>t__builder.Start<VetsBenefitApplicationReusableClientProxy.<LoadBenefitApplicationBaseAndSingleStepData>d__3>(ref <LoadBenefitApplicationBaseAndSingleStepData>d__);
			return <LoadBenefitApplicationBaseAndSingleStepData>d__.<>t__builder.Task;
		}

		// Token: 0x06000DAD RID: 3501 RVA: 0x00021D54 File Offset: 0x0001FF54
		[DebuggerStepThrough]
		public Task<SaveVetsChapterResp> SaveVetsChapterAsync(SaveVetsChapterReq Request)
		{
			VetsBenefitApplicationReusableClientProxy.<SaveVetsChapterAsync>d__4 <SaveVetsChapterAsync>d__ = new VetsBenefitApplicationReusableClientProxy.<SaveVetsChapterAsync>d__4();
			<SaveVetsChapterAsync>d__.<>t__builder = AsyncTaskMethodBuilder<SaveVetsChapterResp>.Create();
			<SaveVetsChapterAsync>d__.<>4__this = this;
			<SaveVetsChapterAsync>d__.Request = Request;
			<SaveVetsChapterAsync>d__.<>1__state = -1;
			<SaveVetsChapterAsync>d__.<>t__builder.Start<VetsBenefitApplicationReusableClientProxy.<SaveVetsChapterAsync>d__4>(ref <SaveVetsChapterAsync>d__);
			return <SaveVetsChapterAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000DAE RID: 3502 RVA: 0x00021DA0 File Offset: 0x0001FFA0
		[DebuggerStepThrough]
		public Task<SaveVetsRegistrationDataResp> SaveVetsRegistrationDataAsync(SaveVetsRegistrationDataReq Request)
		{
			VetsBenefitApplicationReusableClientProxy.<SaveVetsRegistrationDataAsync>d__5 <SaveVetsRegistrationDataAsync>d__ = new VetsBenefitApplicationReusableClientProxy.<SaveVetsRegistrationDataAsync>d__5();
			<SaveVetsRegistrationDataAsync>d__.<>t__builder = AsyncTaskMethodBuilder<SaveVetsRegistrationDataResp>.Create();
			<SaveVetsRegistrationDataAsync>d__.<>4__this = this;
			<SaveVetsRegistrationDataAsync>d__.Request = Request;
			<SaveVetsRegistrationDataAsync>d__.<>1__state = -1;
			<SaveVetsRegistrationDataAsync>d__.<>t__builder.Start<VetsBenefitApplicationReusableClientProxy.<SaveVetsRegistrationDataAsync>d__5>(ref <SaveVetsRegistrationDataAsync>d__);
			return <SaveVetsRegistrationDataAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000DAF RID: 3503 RVA: 0x00021DEC File Offset: 0x0001FFEC
		[DebuggerStepThrough]
		public Task<SaveVetsBenAppDataResp> SaveVetsBenAppDataAsync(SaveVetsBenAppDataReq Request)
		{
			VetsBenefitApplicationReusableClientProxy.<SaveVetsBenAppDataAsync>d__6 <SaveVetsBenAppDataAsync>d__ = new VetsBenefitApplicationReusableClientProxy.<SaveVetsBenAppDataAsync>d__6();
			<SaveVetsBenAppDataAsync>d__.<>t__builder = AsyncTaskMethodBuilder<SaveVetsBenAppDataResp>.Create();
			<SaveVetsBenAppDataAsync>d__.<>4__this = this;
			<SaveVetsBenAppDataAsync>d__.Request = Request;
			<SaveVetsBenAppDataAsync>d__.<>1__state = -1;
			<SaveVetsBenAppDataAsync>d__.<>t__builder.Start<VetsBenefitApplicationReusableClientProxy.<SaveVetsBenAppDataAsync>d__6>(ref <SaveVetsBenAppDataAsync>d__);
			return <SaveVetsBenAppDataAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000DB0 RID: 3504 RVA: 0x00021E38 File Offset: 0x00020038
		[DebuggerStepThrough]
		public Task<SaveVetsStudentAgreeDataResp> SaveVetsStudentAgreeDataAsync(SaveVetsStudentAgreeDataReq Request)
		{
			VetsBenefitApplicationReusableClientProxy.<SaveVetsStudentAgreeDataAsync>d__7 <SaveVetsStudentAgreeDataAsync>d__ = new VetsBenefitApplicationReusableClientProxy.<SaveVetsStudentAgreeDataAsync>d__7();
			<SaveVetsStudentAgreeDataAsync>d__.<>t__builder = AsyncTaskMethodBuilder<SaveVetsStudentAgreeDataResp>.Create();
			<SaveVetsStudentAgreeDataAsync>d__.<>4__this = this;
			<SaveVetsStudentAgreeDataAsync>d__.Request = Request;
			<SaveVetsStudentAgreeDataAsync>d__.<>1__state = -1;
			<SaveVetsStudentAgreeDataAsync>d__.<>t__builder.Start<VetsBenefitApplicationReusableClientProxy.<SaveVetsStudentAgreeDataAsync>d__7>(ref <SaveVetsStudentAgreeDataAsync>d__);
			return <SaveVetsStudentAgreeDataAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000DB1 RID: 3505 RVA: 0x00021E84 File Offset: 0x00020084
		[DebuggerStepThrough]
		public Task<CreateVetsBenefitApplicationResp> CreateVetsBenefitApplicationAsync(CreateVetsBenefitApplicationReq Request)
		{
			VetsBenefitApplicationReusableClientProxy.<CreateVetsBenefitApplicationAsync>d__8 <CreateVetsBenefitApplicationAsync>d__ = new VetsBenefitApplicationReusableClientProxy.<CreateVetsBenefitApplicationAsync>d__8();
			<CreateVetsBenefitApplicationAsync>d__.<>t__builder = AsyncTaskMethodBuilder<CreateVetsBenefitApplicationResp>.Create();
			<CreateVetsBenefitApplicationAsync>d__.<>4__this = this;
			<CreateVetsBenefitApplicationAsync>d__.Request = Request;
			<CreateVetsBenefitApplicationAsync>d__.<>1__state = -1;
			<CreateVetsBenefitApplicationAsync>d__.<>t__builder.Start<VetsBenefitApplicationReusableClientProxy.<CreateVetsBenefitApplicationAsync>d__8>(ref <CreateVetsBenefitApplicationAsync>d__);
			return <CreateVetsBenefitApplicationAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000DB2 RID: 3506 RVA: 0x00021ED0 File Offset: 0x000200D0
		[DebuggerStepThrough]
		public Task<CreateVetsBenefitApplicationCurrentSemesterResp> CreateVetsBenefitApplicationCurrentSemesterAsync(CreateVetsBenefitApplicationCurrentSemesterReq Request)
		{
			VetsBenefitApplicationReusableClientProxy.<CreateVetsBenefitApplicationCurrentSemesterAsync>d__9 <CreateVetsBenefitApplicationCurrentSemesterAsync>d__ = new VetsBenefitApplicationReusableClientProxy.<CreateVetsBenefitApplicationCurrentSemesterAsync>d__9();
			<CreateVetsBenefitApplicationCurrentSemesterAsync>d__.<>t__builder = AsyncTaskMethodBuilder<CreateVetsBenefitApplicationCurrentSemesterResp>.Create();
			<CreateVetsBenefitApplicationCurrentSemesterAsync>d__.<>4__this = this;
			<CreateVetsBenefitApplicationCurrentSemesterAsync>d__.Request = Request;
			<CreateVetsBenefitApplicationCurrentSemesterAsync>d__.<>1__state = -1;
			<CreateVetsBenefitApplicationCurrentSemesterAsync>d__.<>t__builder.Start<VetsBenefitApplicationReusableClientProxy.<CreateVetsBenefitApplicationCurrentSemesterAsync>d__9>(ref <CreateVetsBenefitApplicationCurrentSemesterAsync>d__);
			return <CreateVetsBenefitApplicationCurrentSemesterAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000DB3 RID: 3507 RVA: 0x00021F1C File Offset: 0x0002011C
		[DebuggerStepThrough]
		public Task<CreateVetsBenefitApplicationNextSemesterResp> CreateVetsBenefitApplicationNextSemesterAsync(CreateVetsBenefitApplicationNextSemesterReq Request)
		{
			VetsBenefitApplicationReusableClientProxy.<CreateVetsBenefitApplicationNextSemesterAsync>d__10 <CreateVetsBenefitApplicationNextSemesterAsync>d__ = new VetsBenefitApplicationReusableClientProxy.<CreateVetsBenefitApplicationNextSemesterAsync>d__10();
			<CreateVetsBenefitApplicationNextSemesterAsync>d__.<>t__builder = AsyncTaskMethodBuilder<CreateVetsBenefitApplicationNextSemesterResp>.Create();
			<CreateVetsBenefitApplicationNextSemesterAsync>d__.<>4__this = this;
			<CreateVetsBenefitApplicationNextSemesterAsync>d__.Request = Request;
			<CreateVetsBenefitApplicationNextSemesterAsync>d__.<>1__state = -1;
			<CreateVetsBenefitApplicationNextSemesterAsync>d__.<>t__builder.Start<VetsBenefitApplicationReusableClientProxy.<CreateVetsBenefitApplicationNextSemesterAsync>d__10>(ref <CreateVetsBenefitApplicationNextSemesterAsync>d__);
			return <CreateVetsBenefitApplicationNextSemesterAsync>d__.<>t__builder.Task;
		}
	}
}
