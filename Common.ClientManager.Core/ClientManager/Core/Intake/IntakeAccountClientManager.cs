using System;
using System.Collections.Generic;
using TechnoPro.ClockWorkServer.Client.ServiceFactory;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms;
using TechnoPro.ClockWorkServer.Contracts.DTO.Intake;
using TechnoPro.Common.ClientManager.ICore;
using TechnoPro.Common.ClientManager.ICore.Intake;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities.Intake;
using TechnoPro.Common.Unity.IoC;

namespace TechnoPro.Common.ClientManager.Core.Intake
{
	// Token: 0x0200005A RID: 90
	public class IntakeAccountClientManager : IIntakeAccountClientManager, IWebService
	{
		// Token: 0x06000328 RID: 808 RVA: 0x0000DCC0 File Offset: 0x0000BEC0
		public int CreateNewIntakeAccount(IntakeUserAccountDTO UserAccount)
		{
			CreateNewIntakeAccountReq createNewIntakeAccountReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<CreateNewIntakeAccountReq>();
			createNewIntakeAccountReq.UserAccount = UserAccount;
			return ClientServiceFactory.GetClientInstance<IIntakeAccount>().CreateNewIntakeAccount(createNewIntakeAccountReq).NewIntakePersonId;
		}

		// Token: 0x06000329 RID: 809 RVA: 0x0000DCF8 File Offset: 0x0000BEF8
		public IList<IntakeEntryDTO> LoadPendingIntakeEntries()
		{
			LoadPendingIntakeEntriesReq request = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadPendingIntakeEntriesReq>();
			return ClientServiceFactory.GetClientInstance<IIntakeAccount>().LoadPendingIntakeEntries(request).IntakeEntries;
		}

		// Token: 0x0600032A RID: 810 RVA: 0x0000DD28 File Offset: 0x0000BF28
		public void UpdateActiveIntakeStatusAndNote(int[] intakePersonIds, string newNote, Guid newIntakeStatusId)
		{
			UpdateActiveIntakeStatusAndNoteReq updateActiveIntakeStatusAndNoteReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<UpdateActiveIntakeStatusAndNoteReq>();
			updateActiveIntakeStatusAndNoteReq.IntakePersonIds = intakePersonIds;
			updateActiveIntakeStatusAndNoteReq.NewNote = newNote;
			updateActiveIntakeStatusAndNoteReq.NewIntakeStatusId = newIntakeStatusId;
			ClientServiceFactory.GetClientInstance<IIntakeAccount>().UpdateActiveIntakeStatusAndNote(updateActiveIntakeStatusAndNoteReq);
		}

		// Token: 0x0600032B RID: 811 RVA: 0x0000DD68 File Offset: 0x0000BF68
		public void UpdateActiveIntakeStatus(int[] intakePersonIds, Guid newIntakeStatusId)
		{
			UpdateActiveIntakeStatusReq updateActiveIntakeStatusReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<UpdateActiveIntakeStatusReq>();
			updateActiveIntakeStatusReq.IntakePersonIds = intakePersonIds;
			updateActiveIntakeStatusReq.NewIntakeStatusId = newIntakeStatusId;
			ClientServiceFactory.GetClientInstance<IIntakeAccount>().UpdateActiveIntakeStatus(updateActiveIntakeStatusReq);
		}

		// Token: 0x0600032C RID: 812 RVA: 0x0000DDA0 File Offset: 0x0000BFA0
		public void UpdateActiveIntakeNote(int[] intakePersonIds, string newNote)
		{
			UpdateActiveIntakeNoteReq updateActiveIntakeNoteReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<UpdateActiveIntakeNoteReq>();
			updateActiveIntakeNoteReq.IntakePersonIds = intakePersonIds;
			updateActiveIntakeNoteReq.NewNote = newNote;
			ClientServiceFactory.GetClientInstance<IIntakeAccount>().UpdateActiveIntakeNote(updateActiveIntakeNoteReq);
		}

		// Token: 0x0600032D RID: 813 RVA: 0x0000DDD8 File Offset: 0x0000BFD8
		public void RemoveIntake(string student_no)
		{
			RemoveIntakeReq removeIntakeReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<RemoveIntakeReq>();
			removeIntakeReq.StudentNumber = student_no;
			ClientServiceFactory.GetClientInstance<IIntakeAccount>().RemoveIntake(removeIntakeReq);
		}

		// Token: 0x0600032E RID: 814 RVA: 0x0000DE08 File Offset: 0x0000C008
		public IList<IntakeStatusDTO> LoadLookupStatuses()
		{
			LoadLookupStatusesReq request = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadLookupStatusesReq>();
			return ClientServiceFactory.GetClientInstance<IIntakeAccount>().LoadLookupStatuses(request).IntakeStatuses;
		}

		// Token: 0x0600032F RID: 815 RVA: 0x0000DE38 File Offset: 0x0000C038
		public CreateRealStudentAccountFromIntakeResultDTO CreateRealStudentAccountFromIntakeAndRemoveIntake(string snum, int[] gids)
		{
			CreateRealStudentAccountFromIntakeAndRemoveIntakeReq createRealStudentAccountFromIntakeAndRemoveIntakeReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<CreateRealStudentAccountFromIntakeAndRemoveIntakeReq>();
			createRealStudentAccountFromIntakeAndRemoveIntakeReq.StudentNumber = snum;
			createRealStudentAccountFromIntakeAndRemoveIntakeReq.GroupIds = gids;
			CreateRealStudentAccountFromIntakeAndRemoveIntakeResp createRealStudentAccountFromIntakeAndRemoveIntakeResp = ClientServiceFactory.GetClientInstance<IIntakeAccount>().CreateRealStudentAccountFromIntakeAndRemoveIntake(createRealStudentAccountFromIntakeAndRemoveIntakeReq);
			return (createRealStudentAccountFromIntakeAndRemoveIntakeResp != null) ? createRealStudentAccountFromIntakeAndRemoveIntakeResp.CreateStudentResult : null;
		}

		// Token: 0x06000330 RID: 816 RVA: 0x0000DE7C File Offset: 0x0000C07C
		public IList<DynamicDataDTO> LoadIntakeFormData(string snum)
		{
			LoadIntakeFormDataReq loadIntakeFormDataReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadIntakeFormDataReq>();
			loadIntakeFormDataReq.StudentNumber = snum;
			LoadIntakeFormDataResp loadIntakeFormDataResp = ClientServiceFactory.GetClientInstance<IIntakeAccount>().LoadIntakeFormData(loadIntakeFormDataReq);
			return (loadIntakeFormDataResp != null) ? loadIntakeFormDataResp.DynamicData : null;
		}

		// Token: 0x06000331 RID: 817 RVA: 0x0000DEB8 File Offset: 0x0000C0B8
		public IDictionary<string, ePreIntakeStatus> GetIntakeStatuses(params string[] studentNumbers)
		{
			GetIntakeStatusesReq getIntakeStatusesReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<GetIntakeStatusesReq>();
			getIntakeStatusesReq.StudentNumbers = studentNumbers;
			GetIntakeStatusesResp intakeStatuses = ClientServiceFactory.GetClientInstance<IIntakeAccount>().GetIntakeStatuses(getIntakeStatusesReq);
			return (intakeStatuses != null) ? intakeStatuses.IntakeStatuses : null;
		}

		// Token: 0x06000332 RID: 818 RVA: 0x0000DEF4 File Offset: 0x0000C0F4
		public void RemoveIntakes(int[] intakePersonIds)
		{
			RemoveIntakesReq removeIntakesReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<RemoveIntakesReq>();
			removeIntakesReq.IntakePersonIds = intakePersonIds;
			ClientServiceFactory.GetClientInstance<IIntakeAccount>().RemoveIntakes(removeIntakesReq);
		}

		// Token: 0x06000333 RID: 819 RVA: 0x0000DF24 File Offset: 0x0000C124
		public IList<IntakeEntryQueueItemDTO> LoadPendingIntakeEntryQueueItems()
		{
			LoadPendingIntakeEntryQueueItemsReq request = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadPendingIntakeEntryQueueItemsReq>();
			return ClientServiceFactory.GetClientInstance<IIntakeAccount>().LoadPendingIntakeEntryQueueItems(request).IntakeEntries;
		}

		// Token: 0x06000334 RID: 820 RVA: 0x0000DF54 File Offset: 0x0000C154
		public void SyncIntakeData(string snum, bool removeIntakesWhenDone)
		{
			SyncIntakeDataReq syncIntakeDataReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<SyncIntakeDataReq>();
			syncIntakeDataReq.StudentNumber = snum;
			syncIntakeDataReq.RemoveIntakeWhenDone = removeIntakesWhenDone;
			ClientServiceFactory.GetClientInstance<IIntakeAccount>().SyncIntakeData(syncIntakeDataReq);
		}
	}
}
