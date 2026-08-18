using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using TechnoPro.Common.DAO.Impl.Vets;
using TechnoPro.Common.DAO.Vets;
using TechnoPro.Common.ICore.Vets;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.CustomForms.Data;
using TechnoPro.Common.Public.Entities.Vets;

namespace TechnoPro.Common.Core.Vets
{
	// Token: 0x02000026 RID: 38
	public class VetsBenefitApplicationManager : IVetsBenefitApplicationManager, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x0600013C RID: 316 RVA: 0x00006C8F File Offset: 0x00004E8F
		public VetsBenefitApplicationManager(OperationContext operationContext)
		{
			this.OpContext = operationContext;
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x0600013D RID: 317 RVA: 0x00006CA1 File Offset: 0x00004EA1
		// (set) Token: 0x0600013E RID: 318 RVA: 0x00006CA9 File Offset: 0x00004EA9
		public OperationContext OpContext { get; set; }

		// Token: 0x0600013F RID: 319 RVA: 0x00006CB4 File Offset: 0x00004EB4
		private VetsBenefitApplicationRegistration LoadRegistrationStepData(VetsBenefitApplication baseApp)
		{
			return (baseApp != null) ? baseApp.Clone<VetsBenefitApplicationRegistration>() : null;
		}

		// Token: 0x06000140 RID: 320 RVA: 0x00006CD4 File Offset: 0x00004ED4
		private VetsBenefitApplicationChapter LoadChapterStepData(VetsBenefitApplication baseApp)
		{
			return (baseApp != null) ? baseApp.Clone<VetsBenefitApplicationChapter>() : null;
		}

		// Token: 0x06000141 RID: 321 RVA: 0x00006CF4 File Offset: 0x00004EF4
		private VetsBenefitApplicationBenApp LoadBenAppStepData(VetsBenefitApplication baseApp)
		{
			return (baseApp != null) ? baseApp.Clone<VetsBenefitApplicationBenApp>() : null;
		}

		// Token: 0x06000142 RID: 322 RVA: 0x00006D14 File Offset: 0x00004F14
		private VetsBenefitApplicationAgreement LoadAgreementStepData(VetsBenefitApplication baseApp)
		{
			return (baseApp != null) ? baseApp.Clone<VetsBenefitApplicationAgreement>() : null;
		}

		// Token: 0x06000143 RID: 323 RVA: 0x00006D34 File Offset: 0x00004F34
		[DebuggerStepThrough]
		private Task<VetsBenefitApplicationStatus> LoadStatusStepData(VetsBenefitApplication baseApp, IVetsBenefitApplicationDAO dao)
		{
			VetsBenefitApplicationManager.<LoadStatusStepData>d__9 <LoadStatusStepData>d__ = new VetsBenefitApplicationManager.<LoadStatusStepData>d__9();
			<LoadStatusStepData>d__.<>t__builder = AsyncTaskMethodBuilder<VetsBenefitApplicationStatus>.Create();
			<LoadStatusStepData>d__.<>4__this = this;
			<LoadStatusStepData>d__.baseApp = baseApp;
			<LoadStatusStepData>d__.dao = dao;
			<LoadStatusStepData>d__.<>1__state = -1;
			<LoadStatusStepData>d__.<>t__builder.Start<VetsBenefitApplicationManager.<LoadStatusStepData>d__9>(ref <LoadStatusStepData>d__);
			return <LoadStatusStepData>d__.<>t__builder.Task;
		}

		// Token: 0x06000144 RID: 324 RVA: 0x00006D88 File Offset: 0x00004F88
		[DebuggerStepThrough]
		private Task UpdateBenefitApplicationStudentInfo(Guid BenefitApplicationId, bool? RegistrationCompleted, Guid? ChapterId, bool? BenAppCompleted, bool? StudentAgreeCompleted, eVetsBenefitApplicationStep? PreferredStep)
		{
			VetsBenefitApplicationManager.<UpdateBenefitApplicationStudentInfo>d__10 <UpdateBenefitApplicationStudentInfo>d__ = new VetsBenefitApplicationManager.<UpdateBenefitApplicationStudentInfo>d__10();
			<UpdateBenefitApplicationStudentInfo>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<UpdateBenefitApplicationStudentInfo>d__.<>4__this = this;
			<UpdateBenefitApplicationStudentInfo>d__.BenefitApplicationId = BenefitApplicationId;
			<UpdateBenefitApplicationStudentInfo>d__.RegistrationCompleted = RegistrationCompleted;
			<UpdateBenefitApplicationStudentInfo>d__.ChapterId = ChapterId;
			<UpdateBenefitApplicationStudentInfo>d__.BenAppCompleted = BenAppCompleted;
			<UpdateBenefitApplicationStudentInfo>d__.StudentAgreeCompleted = StudentAgreeCompleted;
			<UpdateBenefitApplicationStudentInfo>d__.PreferredStep = PreferredStep;
			<UpdateBenefitApplicationStudentInfo>d__.<>1__state = -1;
			<UpdateBenefitApplicationStudentInfo>d__.<>t__builder.Start<VetsBenefitApplicationManager.<UpdateBenefitApplicationStudentInfo>d__10>(ref <UpdateBenefitApplicationStudentInfo>d__);
			return <UpdateBenefitApplicationStudentInfo>d__.<>t__builder.Task;
		}

		// Token: 0x06000145 RID: 325 RVA: 0x00006DFC File Offset: 0x00004FFC
		private static void AddHistoryItem(OperationContext opContext, Guid BenefitApplicationId, params eVetsBenefitApplicationModificationType[] modTypes)
		{
			IVetsBenefitApplicationDAO vetsBenefitApplicationDAO = new VetsBenefitApplicationDAO(opContext);
			vetsBenefitApplicationDAO.UpdateVetsBenefitApplicationModificationEntryAsync(BenefitApplicationId, modTypes);
		}

		// Token: 0x06000146 RID: 326 RVA: 0x00006E1C File Offset: 0x0000501C
		private static void SetVetsBenefitApplicationStatus(VetsBenefitApplication app)
		{
			bool flag = app == null;
			if (!flag)
			{
				bool flag2 = app.Chapter != null;
				bool benAppCompleted = app.BenAppCompleted;
				bool studentAgreed = app.StudentAgreed;
				bool registrationCompleted = app.RegistrationCompleted;
				bool flag3 = app.FinalStatus == eVetsRequestStatus.Approved || app.FinalStatus == eVetsRequestStatus.Denied;
				bool flag4 = flag3 || studentAgreed;
				if (flag4)
				{
					app.MinPageAllow = eVetsBenefitApplicationStep.Status;
					app.MaxPageAllow = eVetsBenefitApplicationStep.Status;
				}
				else
				{
					app.MinPageAllow = eVetsBenefitApplicationStep.Registration;
					bool flag5 = !registrationCompleted;
					if (flag5)
					{
						app.MaxPageAllow = eVetsBenefitApplicationStep.Registration;
					}
					else
					{
						bool flag6 = !flag2;
						if (flag6)
						{
							app.MaxPageAllow = eVetsBenefitApplicationStep.ChapterSelection;
						}
						else
						{
							bool flag7 = !benAppCompleted;
							if (flag7)
							{
								app.MaxPageAllow = eVetsBenefitApplicationStep.Application;
							}
							else
							{
								app.MaxPageAllow = eVetsBenefitApplicationStep.Agreement;
							}
						}
					}
				}
			}
		}

		// Token: 0x06000147 RID: 327 RVA: 0x00006EDC File Offset: 0x000050DC
		private static eVetsBenefitApplicationStep GetActualStepToGoTo(eVetsBenefitApplicationStep? livePreferredStep, VetsBenefitApplication app)
		{
			bool flag = app == null;
			eVetsBenefitApplicationStep result;
			if (flag)
			{
				result = eVetsBenefitApplicationStep.Registration;
			}
			else
			{
				int minPageAllow = (int)app.MinPageAllow;
				int maxPageAllow = (int)app.MaxPageAllow;
				eVetsBenefitApplicationStep eVetsBenefitApplicationStep = livePreferredStep ?? ((eVetsBenefitApplicationStep)maxPageAllow);
				int num = (int)eVetsBenefitApplicationStep;
				bool flag2 = num > maxPageAllow;
				if (flag2)
				{
					result = (eVetsBenefitApplicationStep)maxPageAllow;
				}
				else
				{
					bool flag3 = num < minPageAllow;
					if (flag3)
					{
						result = (eVetsBenefitApplicationStep)minPageAllow;
					}
					else
					{
						result = (eVetsBenefitApplicationStep)num;
					}
				}
			}
			return result;
		}

		// Token: 0x06000148 RID: 328 RVA: 0x00006F48 File Offset: 0x00005148
		[DebuggerStepThrough]
		public Task<VetsBenefitApplication> LoadBenefitApplicationByIdAsync(Guid BenefitApplicationId)
		{
			VetsBenefitApplicationManager.<LoadBenefitApplicationByIdAsync>d__14 <LoadBenefitApplicationByIdAsync>d__ = new VetsBenefitApplicationManager.<LoadBenefitApplicationByIdAsync>d__14();
			<LoadBenefitApplicationByIdAsync>d__.<>t__builder = AsyncTaskMethodBuilder<VetsBenefitApplication>.Create();
			<LoadBenefitApplicationByIdAsync>d__.<>4__this = this;
			<LoadBenefitApplicationByIdAsync>d__.BenefitApplicationId = BenefitApplicationId;
			<LoadBenefitApplicationByIdAsync>d__.<>1__state = -1;
			<LoadBenefitApplicationByIdAsync>d__.<>t__builder.Start<VetsBenefitApplicationManager.<LoadBenefitApplicationByIdAsync>d__14>(ref <LoadBenefitApplicationByIdAsync>d__);
			return <LoadBenefitApplicationByIdAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000149 RID: 329 RVA: 0x00006F94 File Offset: 0x00005194
		[DebuggerStepThrough]
		public Task<VetsBenefitApplication> LoadBenefitApplicationBaseAndSingleStepData(Guid BenefitApplicationId, eVetsBenefitApplicationStep? preferredStep)
		{
			VetsBenefitApplicationManager.<LoadBenefitApplicationBaseAndSingleStepData>d__15 <LoadBenefitApplicationBaseAndSingleStepData>d__ = new VetsBenefitApplicationManager.<LoadBenefitApplicationBaseAndSingleStepData>d__15();
			<LoadBenefitApplicationBaseAndSingleStepData>d__.<>t__builder = AsyncTaskMethodBuilder<VetsBenefitApplication>.Create();
			<LoadBenefitApplicationBaseAndSingleStepData>d__.<>4__this = this;
			<LoadBenefitApplicationBaseAndSingleStepData>d__.BenefitApplicationId = BenefitApplicationId;
			<LoadBenefitApplicationBaseAndSingleStepData>d__.preferredStep = preferredStep;
			<LoadBenefitApplicationBaseAndSingleStepData>d__.<>1__state = -1;
			<LoadBenefitApplicationBaseAndSingleStepData>d__.<>t__builder.Start<VetsBenefitApplicationManager.<LoadBenefitApplicationBaseAndSingleStepData>d__15>(ref <LoadBenefitApplicationBaseAndSingleStepData>d__);
			return <LoadBenefitApplicationBaseAndSingleStepData>d__.<>t__builder.Task;
		}

		// Token: 0x0600014A RID: 330 RVA: 0x00006FE8 File Offset: 0x000051E8
		[DebuggerStepThrough]
		public Task SaveVetsChapterAsync(Guid BenefitApplicationId, Guid ChapterId)
		{
			VetsBenefitApplicationManager.<SaveVetsChapterAsync>d__16 <SaveVetsChapterAsync>d__ = new VetsBenefitApplicationManager.<SaveVetsChapterAsync>d__16();
			<SaveVetsChapterAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<SaveVetsChapterAsync>d__.<>4__this = this;
			<SaveVetsChapterAsync>d__.BenefitApplicationId = BenefitApplicationId;
			<SaveVetsChapterAsync>d__.ChapterId = ChapterId;
			<SaveVetsChapterAsync>d__.<>1__state = -1;
			<SaveVetsChapterAsync>d__.<>t__builder.Start<VetsBenefitApplicationManager.<SaveVetsChapterAsync>d__16>(ref <SaveVetsChapterAsync>d__);
			return <SaveVetsChapterAsync>d__.<>t__builder.Task;
		}

		// Token: 0x0600014B RID: 331 RVA: 0x0000703C File Offset: 0x0000523C
		[DebuggerStepThrough]
		public Task SaveVetsRegistrationDataAsync(Guid BenefitApplicationId, bool completedRegistration, int PersonId, IList<CustomDataHolderCollection> Data, params Guid[] dataInstanceIds)
		{
			VetsBenefitApplicationManager.<SaveVetsRegistrationDataAsync>d__17 <SaveVetsRegistrationDataAsync>d__ = new VetsBenefitApplicationManager.<SaveVetsRegistrationDataAsync>d__17();
			<SaveVetsRegistrationDataAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<SaveVetsRegistrationDataAsync>d__.<>4__this = this;
			<SaveVetsRegistrationDataAsync>d__.BenefitApplicationId = BenefitApplicationId;
			<SaveVetsRegistrationDataAsync>d__.completedRegistration = completedRegistration;
			<SaveVetsRegistrationDataAsync>d__.PersonId = PersonId;
			<SaveVetsRegistrationDataAsync>d__.Data = Data;
			<SaveVetsRegistrationDataAsync>d__.dataInstanceIds = dataInstanceIds;
			<SaveVetsRegistrationDataAsync>d__.<>1__state = -1;
			<SaveVetsRegistrationDataAsync>d__.<>t__builder.Start<VetsBenefitApplicationManager.<SaveVetsRegistrationDataAsync>d__17>(ref <SaveVetsRegistrationDataAsync>d__);
			return <SaveVetsRegistrationDataAsync>d__.<>t__builder.Task;
		}

		// Token: 0x0600014C RID: 332 RVA: 0x000070A8 File Offset: 0x000052A8
		[DebuggerStepThrough]
		public Task SaveVetsBenAppDataAsync(Guid BenefitApplicationId, bool completedBenApp, int PersonId, int SemesterId, IList<CustomDataHolderCollection> Data, params Guid[] dataInstanceIds)
		{
			VetsBenefitApplicationManager.<SaveVetsBenAppDataAsync>d__18 <SaveVetsBenAppDataAsync>d__ = new VetsBenefitApplicationManager.<SaveVetsBenAppDataAsync>d__18();
			<SaveVetsBenAppDataAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<SaveVetsBenAppDataAsync>d__.<>4__this = this;
			<SaveVetsBenAppDataAsync>d__.BenefitApplicationId = BenefitApplicationId;
			<SaveVetsBenAppDataAsync>d__.completedBenApp = completedBenApp;
			<SaveVetsBenAppDataAsync>d__.PersonId = PersonId;
			<SaveVetsBenAppDataAsync>d__.SemesterId = SemesterId;
			<SaveVetsBenAppDataAsync>d__.Data = Data;
			<SaveVetsBenAppDataAsync>d__.dataInstanceIds = dataInstanceIds;
			<SaveVetsBenAppDataAsync>d__.<>1__state = -1;
			<SaveVetsBenAppDataAsync>d__.<>t__builder.Start<VetsBenefitApplicationManager.<SaveVetsBenAppDataAsync>d__18>(ref <SaveVetsBenAppDataAsync>d__);
			return <SaveVetsBenAppDataAsync>d__.<>t__builder.Task;
		}

		// Token: 0x0600014D RID: 333 RVA: 0x0000711C File Offset: 0x0000531C
		[DebuggerStepThrough]
		public Task SaveVetsStudentAgreeDataAsync(Guid BenefitApplicationId, bool completedStudentAgree, int PersonId, int SemesterId, IList<CustomDataHolderCollection> Data, params Guid[] dataInstanceIds)
		{
			VetsBenefitApplicationManager.<SaveVetsStudentAgreeDataAsync>d__19 <SaveVetsStudentAgreeDataAsync>d__ = new VetsBenefitApplicationManager.<SaveVetsStudentAgreeDataAsync>d__19();
			<SaveVetsStudentAgreeDataAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<SaveVetsStudentAgreeDataAsync>d__.<>4__this = this;
			<SaveVetsStudentAgreeDataAsync>d__.BenefitApplicationId = BenefitApplicationId;
			<SaveVetsStudentAgreeDataAsync>d__.completedStudentAgree = completedStudentAgree;
			<SaveVetsStudentAgreeDataAsync>d__.PersonId = PersonId;
			<SaveVetsStudentAgreeDataAsync>d__.SemesterId = SemesterId;
			<SaveVetsStudentAgreeDataAsync>d__.Data = Data;
			<SaveVetsStudentAgreeDataAsync>d__.dataInstanceIds = dataInstanceIds;
			<SaveVetsStudentAgreeDataAsync>d__.<>1__state = -1;
			<SaveVetsStudentAgreeDataAsync>d__.<>t__builder.Start<VetsBenefitApplicationManager.<SaveVetsStudentAgreeDataAsync>d__19>(ref <SaveVetsStudentAgreeDataAsync>d__);
			return <SaveVetsStudentAgreeDataAsync>d__.<>t__builder.Task;
		}

		// Token: 0x0600014E RID: 334 RVA: 0x00007190 File Offset: 0x00005390
		[DebuggerStepThrough]
		public Task<Guid?> CreateVetsBenefitApplicationAsync(int PersonId, int SemesterId)
		{
			VetsBenefitApplicationManager.<CreateVetsBenefitApplicationAsync>d__20 <CreateVetsBenefitApplicationAsync>d__ = new VetsBenefitApplicationManager.<CreateVetsBenefitApplicationAsync>d__20();
			<CreateVetsBenefitApplicationAsync>d__.<>t__builder = AsyncTaskMethodBuilder<Guid?>.Create();
			<CreateVetsBenefitApplicationAsync>d__.<>4__this = this;
			<CreateVetsBenefitApplicationAsync>d__.PersonId = PersonId;
			<CreateVetsBenefitApplicationAsync>d__.SemesterId = SemesterId;
			<CreateVetsBenefitApplicationAsync>d__.<>1__state = -1;
			<CreateVetsBenefitApplicationAsync>d__.<>t__builder.Start<VetsBenefitApplicationManager.<CreateVetsBenefitApplicationAsync>d__20>(ref <CreateVetsBenefitApplicationAsync>d__);
			return <CreateVetsBenefitApplicationAsync>d__.<>t__builder.Task;
		}

		// Token: 0x0600014F RID: 335 RVA: 0x000071E4 File Offset: 0x000053E4
		[DebuggerStepThrough]
		public Task<VetsStudentCardInfo> LoadStudentVeteranCardInfoAsync(int PersonId)
		{
			VetsBenefitApplicationManager.<LoadStudentVeteranCardInfoAsync>d__21 <LoadStudentVeteranCardInfoAsync>d__ = new VetsBenefitApplicationManager.<LoadStudentVeteranCardInfoAsync>d__21();
			<LoadStudentVeteranCardInfoAsync>d__.<>t__builder = AsyncTaskMethodBuilder<VetsStudentCardInfo>.Create();
			<LoadStudentVeteranCardInfoAsync>d__.<>4__this = this;
			<LoadStudentVeteranCardInfoAsync>d__.PersonId = PersonId;
			<LoadStudentVeteranCardInfoAsync>d__.<>1__state = -1;
			<LoadStudentVeteranCardInfoAsync>d__.<>t__builder.Start<VetsBenefitApplicationManager.<LoadStudentVeteranCardInfoAsync>d__21>(ref <LoadStudentVeteranCardInfoAsync>d__);
			return <LoadStudentVeteranCardInfoAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000150 RID: 336 RVA: 0x00007230 File Offset: 0x00005430
		[DebuggerStepThrough]
		public Task<Guid?> CreateVetsBenefitApplicationCurrentSemesterAsync(int PersonId)
		{
			VetsBenefitApplicationManager.<CreateVetsBenefitApplicationCurrentSemesterAsync>d__22 <CreateVetsBenefitApplicationCurrentSemesterAsync>d__ = new VetsBenefitApplicationManager.<CreateVetsBenefitApplicationCurrentSemesterAsync>d__22();
			<CreateVetsBenefitApplicationCurrentSemesterAsync>d__.<>t__builder = AsyncTaskMethodBuilder<Guid?>.Create();
			<CreateVetsBenefitApplicationCurrentSemesterAsync>d__.<>4__this = this;
			<CreateVetsBenefitApplicationCurrentSemesterAsync>d__.PersonId = PersonId;
			<CreateVetsBenefitApplicationCurrentSemesterAsync>d__.<>1__state = -1;
			<CreateVetsBenefitApplicationCurrentSemesterAsync>d__.<>t__builder.Start<VetsBenefitApplicationManager.<CreateVetsBenefitApplicationCurrentSemesterAsync>d__22>(ref <CreateVetsBenefitApplicationCurrentSemesterAsync>d__);
			return <CreateVetsBenefitApplicationCurrentSemesterAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000151 RID: 337 RVA: 0x0000727C File Offset: 0x0000547C
		[DebuggerStepThrough]
		public Task<Guid?> CreateVetsBenefitApplicationNextSemesterAsync(int PersonId)
		{
			VetsBenefitApplicationManager.<CreateVetsBenefitApplicationNextSemesterAsync>d__23 <CreateVetsBenefitApplicationNextSemesterAsync>d__ = new VetsBenefitApplicationManager.<CreateVetsBenefitApplicationNextSemesterAsync>d__23();
			<CreateVetsBenefitApplicationNextSemesterAsync>d__.<>t__builder = AsyncTaskMethodBuilder<Guid?>.Create();
			<CreateVetsBenefitApplicationNextSemesterAsync>d__.<>4__this = this;
			<CreateVetsBenefitApplicationNextSemesterAsync>d__.PersonId = PersonId;
			<CreateVetsBenefitApplicationNextSemesterAsync>d__.<>1__state = -1;
			<CreateVetsBenefitApplicationNextSemesterAsync>d__.<>t__builder.Start<VetsBenefitApplicationManager.<CreateVetsBenefitApplicationNextSemesterAsync>d__23>(ref <CreateVetsBenefitApplicationNextSemesterAsync>d__);
			return <CreateVetsBenefitApplicationNextSemesterAsync>d__.<>t__builder.Task;
		}
	}
}
