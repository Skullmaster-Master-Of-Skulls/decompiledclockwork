using System;
using System.Collections.Generic;
using System.Linq;
using TechnoPro.ClockWorkServer.Common.Services.Impl.Adapters;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms;
using TechnoPro.ClockWorkServer.Contracts.DTO.Intake;
using TechnoPro.Common.Core.Intake;
using TechnoPro.Common.Core.Mappers.DynamicForms;
using TechnoPro.Common.Core.Mappers.Intake;
using TechnoPro.Common.ICore.Intake;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities.DynamicForms;
using TechnoPro.Common.Public.Entities.Intake;

namespace TechnoPro.ClockWorkServer.Common.Services.Impl
{
	// Token: 0x0200004A RID: 74
	public class IntakeAccountServiceManager : IIntakeAccount, IService
	{
		// Token: 0x060002C8 RID: 712 RVA: 0x0000DD64 File Offset: 0x0000BF64
		public CreateNewIntakeAccountResp CreateNewIntakeAccount(CreateNewIntakeAccountReq Request)
		{
			IIntakeAccountManager intakeAccountManager = new IntakeAccountManager(Request.GetOperationContext());
			int newIntakePersonId = intakeAccountManager.CreateNewIntakeAccount(Request.UserAccount.ToDomainObject());
			return new CreateNewIntakeAccountResp
			{
				NewIntakePersonId = newIntakePersonId
			};
		}

		// Token: 0x060002C9 RID: 713 RVA: 0x0000DDA4 File Offset: 0x0000BFA4
		public LoadPendingIntakeEntriesResp LoadPendingIntakeEntries(LoadPendingIntakeEntriesReq Request)
		{
			IIntakeAccountManager intakeAccountManager = new IntakeAccountManager(Request.GetOperationContext());
			LoadPendingIntakeEntriesResp loadPendingIntakeEntriesResp = new LoadPendingIntakeEntriesResp();
			IList<IntakeEntry> list = intakeAccountManager.LoadPendingIntakeEntries();
			IList<IntakeEntryDTO> intakeEntries;
			if (list == null)
			{
				intakeEntries = null;
			}
			else
			{
				intakeEntries = (from g in list
				select g.ToDTO()).ToList<IntakeEntryDTO>();
			}
			loadPendingIntakeEntriesResp.IntakeEntries = intakeEntries;
			return loadPendingIntakeEntriesResp;
		}

		// Token: 0x060002CA RID: 714 RVA: 0x0000DE04 File Offset: 0x0000C004
		public LoadPendingIntakeEntryQueueItemsResp LoadPendingIntakeEntryQueueItems(LoadPendingIntakeEntryQueueItemsReq Request)
		{
			IIntakeAccountManager intakeAccountManager = new IntakeAccountManager(Request.GetOperationContext());
			LoadPendingIntakeEntryQueueItemsResp loadPendingIntakeEntryQueueItemsResp = new LoadPendingIntakeEntryQueueItemsResp();
			IList<IntakeEntryQueueItem> list = intakeAccountManager.LoadPendingIntakeEntryQueueItems();
			IList<IntakeEntryQueueItemDTO> intakeEntries;
			if (list == null)
			{
				intakeEntries = null;
			}
			else
			{
				intakeEntries = (from g in list
				select g.ToDTO()).ToList<IntakeEntryQueueItemDTO>();
			}
			loadPendingIntakeEntryQueueItemsResp.IntakeEntries = intakeEntries;
			return loadPendingIntakeEntryQueueItemsResp;
		}

		// Token: 0x060002CB RID: 715 RVA: 0x0000DE64 File Offset: 0x0000C064
		public UpdateActiveIntakeStatusAndNoteResp UpdateActiveIntakeStatusAndNote(UpdateActiveIntakeStatusAndNoteReq Request)
		{
			IIntakeAccountManager intakeAccountManager = new IntakeAccountManager(Request.GetOperationContext());
			intakeAccountManager.UpdateActiveIntakeStatusAndNote(Request.IntakePersonIds, Request.NewNote, Request.NewIntakeStatusId);
			return new UpdateActiveIntakeStatusAndNoteResp();
		}

		// Token: 0x060002CC RID: 716 RVA: 0x0000DEA0 File Offset: 0x0000C0A0
		public UpdateActiveIntakeStatusResp UpdateActiveIntakeStatus(UpdateActiveIntakeStatusReq Request)
		{
			IIntakeAccountManager intakeAccountManager = new IntakeAccountManager(Request.GetOperationContext());
			intakeAccountManager.UpdateActiveIntakeStatus(Request.IntakePersonIds, Request.NewIntakeStatusId);
			return new UpdateActiveIntakeStatusResp();
		}

		// Token: 0x060002CD RID: 717 RVA: 0x0000DED8 File Offset: 0x0000C0D8
		public UpdateActiveIntakeNoteResp UpdateActiveIntakeNote(UpdateActiveIntakeNoteReq Request)
		{
			IIntakeAccountManager intakeAccountManager = new IntakeAccountManager(Request.GetOperationContext());
			intakeAccountManager.UpdateActiveIntakeNote(Request.IntakePersonIds, Request.NewNote);
			return new UpdateActiveIntakeNoteResp();
		}

		// Token: 0x060002CE RID: 718 RVA: 0x0000DF10 File Offset: 0x0000C110
		public RemoveIntakeResp RemoveIntake(RemoveIntakeReq Request)
		{
			IIntakeAccountManager intakeAccountManager = new IntakeAccountManager(Request.GetOperationContext());
			intakeAccountManager.RemoveIntake(Request.StudentNumber);
			return new RemoveIntakeResp();
		}

		// Token: 0x060002CF RID: 719 RVA: 0x0000DF40 File Offset: 0x0000C140
		public LoadLookupStatusesResp LoadLookupStatuses(LoadLookupStatusesReq Request)
		{
			IIntakeAccountManager intakeAccountManager = new IntakeAccountManager(Request.GetOperationContext());
			LoadLookupStatusesResp loadLookupStatusesResp = new LoadLookupStatusesResp();
			IList<IntakeStatus> list = intakeAccountManager.LoadLookupStatuses();
			IList<IntakeStatusDTO> intakeStatuses;
			if (list == null)
			{
				intakeStatuses = null;
			}
			else
			{
				intakeStatuses = (from g in list
				select g.ToDTO()).ToList<IntakeStatusDTO>();
			}
			loadLookupStatusesResp.IntakeStatuses = intakeStatuses;
			return loadLookupStatusesResp;
		}

		// Token: 0x060002D0 RID: 720 RVA: 0x0000DFA0 File Offset: 0x0000C1A0
		public CreateRealStudentAccountFromIntakeAndRemoveIntakeResp CreateRealStudentAccountFromIntakeAndRemoveIntake(CreateRealStudentAccountFromIntakeAndRemoveIntakeReq Request)
		{
			IIntakeAccountManager intakeAccountManager = new IntakeAccountManager(Request.GetOperationContext());
			CreateRealStudentAccountFromIntakeAndRemoveIntakeResp createRealStudentAccountFromIntakeAndRemoveIntakeResp = new CreateRealStudentAccountFromIntakeAndRemoveIntakeResp();
			IIntakeAccountManager intakeAccountManager2 = intakeAccountManager;
			string studentNumber = Request.StudentNumber;
			int[] groupIds = Request.GroupIds;
			CreateRealStudentAccountFromIntakeResult createRealStudentAccountFromIntakeResult = intakeAccountManager2.CreateRealStudentAccountFromIntakeAndRemoveIntake(studentNumber, (groupIds != null) ? groupIds.ToList<int>() : null);
			createRealStudentAccountFromIntakeAndRemoveIntakeResp.CreateStudentResult = ((createRealStudentAccountFromIntakeResult != null) ? createRealStudentAccountFromIntakeResult.ToDTO() : null);
			return createRealStudentAccountFromIntakeAndRemoveIntakeResp;
		}

		// Token: 0x060002D1 RID: 721 RVA: 0x0000DFF4 File Offset: 0x0000C1F4
		public LoadIntakeFormDataResp LoadIntakeFormData(LoadIntakeFormDataReq Request)
		{
			IIntakeAccountManager intakeAccountManager = new IntakeAccountManager(Request.GetOperationContext());
			LoadIntakeFormDataResp loadIntakeFormDataResp = new LoadIntakeFormDataResp();
			IList<DynamicData> list = intakeAccountManager.LoadIntakeFormData(Request.StudentNumber);
			IList<DynamicDataDTO> dynamicData;
			if (list == null)
			{
				dynamicData = null;
			}
			else
			{
				dynamicData = (from g in list
				select g.ToDTO()).ToList<DynamicDataDTO>();
			}
			loadIntakeFormDataResp.DynamicData = dynamicData;
			return loadIntakeFormDataResp;
		}

		// Token: 0x060002D2 RID: 722 RVA: 0x0000E05C File Offset: 0x0000C25C
		public GetIntakeStatusesResp GetIntakeStatuses(GetIntakeStatusesReq Request)
		{
			IIntakeAccountManager intakeAccountManager = new IntakeAccountManager(Request.GetOperationContext());
			return new GetIntakeStatusesResp
			{
				IntakeStatuses = intakeAccountManager.GetIntakeStatuses(Request.StudentNumbers)
			};
		}

		// Token: 0x060002D3 RID: 723 RVA: 0x0000E094 File Offset: 0x0000C294
		public RemoveIntakesResp RemoveIntakes(RemoveIntakesReq Request)
		{
			IIntakeAccountManager intakeAccountManager = new IntakeAccountManager(Request.GetOperationContext());
			intakeAccountManager.RemoveIntakes(Request.IntakePersonIds);
			return new RemoveIntakesResp();
		}

		// Token: 0x060002D4 RID: 724 RVA: 0x0000E0C4 File Offset: 0x0000C2C4
		public SyncIntakeDataResp SyncIntakeData(SyncIntakeDataReq Request)
		{
			IIntakeAccountManager intakeAccountManager = new IntakeAccountManager(Request.GetOperationContext());
			intakeAccountManager.SyncIntakeData(Request.StudentNumber, Request.RemoveIntakeWhenDone);
			return new SyncIntakeDataResp();
		}
	}
}
