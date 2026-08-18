using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using TechnoPro.Common.Core.DynamicForms;
using TechnoPro.Common.Core.UserSettingsPermissions;
using TechnoPro.Common.DAO.Impl.OnlineForms;
using TechnoPro.Common.DAO.OnlineForms;
using TechnoPro.Common.ICore.DynamicForms;
using TechnoPro.Common.ICore.OnlineForms;
using TechnoPro.Common.ICore.UserSettingsPermissions;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.DynamicForms;
using TechnoPro.Common.Public.Entities.OnlineForms;
using TechnoPro.Common.Public.Entities.UserSettingsPermissions.OldUserSettings;
using TechnoPro.Common.Public.Exceptions.PermissionDenied;
using TechnoPro.Common.Unity.IoC;

namespace TechnoPro.Common.Core.OnlineForms
{
	// Token: 0x020000AC RID: 172
	public class OnlineFormQueueManager : IOnlineFormQueueManager, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x0600064E RID: 1614 RVA: 0x00024D82 File Offset: 0x00022F82
		// (set) Token: 0x0600064F RID: 1615 RVA: 0x00024D8A File Offset: 0x00022F8A
		public OperationContext OpContext { get; set; }

		// Token: 0x06000650 RID: 1616 RVA: 0x00024D93 File Offset: 0x00022F93
		public OnlineFormQueueManager(OperationContext opContext)
		{
			this.OpContext = opContext;
		}

		// Token: 0x06000651 RID: 1617 RVA: 0x00024DA8 File Offset: 0x00022FA8
		[DebuggerStepThrough]
		public Task<IList<OnlineFormStatus>> LoadLookupOnlineFormStatusesAsync()
		{
			OnlineFormQueueManager.<LoadLookupOnlineFormStatusesAsync>d__5 <LoadLookupOnlineFormStatusesAsync>d__ = new OnlineFormQueueManager.<LoadLookupOnlineFormStatusesAsync>d__5();
			<LoadLookupOnlineFormStatusesAsync>d__.<>t__builder = AsyncTaskMethodBuilder<IList<OnlineFormStatus>>.Create();
			<LoadLookupOnlineFormStatusesAsync>d__.<>4__this = this;
			<LoadLookupOnlineFormStatusesAsync>d__.<>1__state = -1;
			<LoadLookupOnlineFormStatusesAsync>d__.<>t__builder.Start<OnlineFormQueueManager.<LoadLookupOnlineFormStatusesAsync>d__5>(ref <LoadLookupOnlineFormStatusesAsync>d__);
			return <LoadLookupOnlineFormStatusesAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000652 RID: 1618 RVA: 0x00024DEC File Offset: 0x00022FEC
		public IList<OnlineFormStatus> LoadLookupOnlineFormStatuses()
		{
			ICacheStorageManager cacheStorageManager = ObjectFactory.Resolve<ICacheStorageManager>();
			IList<OnlineFormStatus> list = cacheStorageManager["OnlineFormLookupStatuses"] as IList<OnlineFormStatus>;
			bool flag = list == null;
			if (flag)
			{
				IOnlineFormQueueDAO onlineFormQueueDAO = new OnlineFormQueueDAO(this.OpContext);
				list = onlineFormQueueDAO.LoadLookupOnlineFormStatuses();
				cacheStorageManager.Insert("OnlineFormLookupStatuses", list, TimeSpan.FromHours(24.0));
			}
			return list;
		}

		// Token: 0x06000653 RID: 1619 RVA: 0x00024E50 File Offset: 0x00023050
		[DebuggerStepThrough]
		public Task<IList<OnlineFormQueueItem>> LoadOnlineFormQueueItemsAsync(int onlineFormId, DateTime startDate, DateTime? endDate, int filterByAssignedCounsellorPid, params eOnlineFormStatusType[] onlineFormTypesToExclude)
		{
			OnlineFormQueueManager.<LoadOnlineFormQueueItemsAsync>d__7 <LoadOnlineFormQueueItemsAsync>d__ = new OnlineFormQueueManager.<LoadOnlineFormQueueItemsAsync>d__7();
			<LoadOnlineFormQueueItemsAsync>d__.<>t__builder = AsyncTaskMethodBuilder<IList<OnlineFormQueueItem>>.Create();
			<LoadOnlineFormQueueItemsAsync>d__.<>4__this = this;
			<LoadOnlineFormQueueItemsAsync>d__.onlineFormId = onlineFormId;
			<LoadOnlineFormQueueItemsAsync>d__.startDate = startDate;
			<LoadOnlineFormQueueItemsAsync>d__.endDate = endDate;
			<LoadOnlineFormQueueItemsAsync>d__.filterByAssignedCounsellorPid = filterByAssignedCounsellorPid;
			<LoadOnlineFormQueueItemsAsync>d__.onlineFormTypesToExclude = onlineFormTypesToExclude;
			<LoadOnlineFormQueueItemsAsync>d__.<>1__state = -1;
			<LoadOnlineFormQueueItemsAsync>d__.<>t__builder.Start<OnlineFormQueueManager.<LoadOnlineFormQueueItemsAsync>d__7>(ref <LoadOnlineFormQueueItemsAsync>d__);
			return <LoadOnlineFormQueueItemsAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000654 RID: 1620 RVA: 0x00024EBC File Offset: 0x000230BC
		public IList<OnlineFormQueueItem> LoadOnlineFormQueueItems(int onlineFormId, DateTime startDate, DateTime? endDate, int filterByAssignedCounsellorPid, params eOnlineFormStatusType[] onlineFormTypesToExclude)
		{
			bool flag = onlineFormTypesToExclude != null && onlineFormTypesToExclude.Length != 0;
			int[] statusIdsToExclude;
			if (flag)
			{
				IList<OnlineFormStatus> source = this.LoadLookupOnlineFormStatuses();
				statusIdsToExclude = (from g in source
				where onlineFormTypesToExclude.Contains(g.StatusType)
				select g into m
				select m.PeopleOnlineFormStatusId).ToArray<int>();
			}
			else
			{
				statusIdsToExclude = null;
			}
			IOldUserSettingManager oldUserSettingManager = new OldUserSettingManager(this.OpContext);
			List<int> settingValue_ConcatenatedIntList = oldUserSettingManager.GetSettingValue_ConcatenatedIntList(this.OpContext.WhoAmI, eSettingCode.SETTING_OnlineForms_AllowedOnlineFormsInOnlineFormsQueue);
			bool flag2 = onlineFormId < 1 || !settingValue_ConcatenatedIntList.Contains(onlineFormId);
			if (flag2)
			{
				throw new PermissionDeniedException("OnlineFormID not allowed (" + onlineFormId.ToString() + ")");
			}
			IOnlineFormQueueDAO onlineFormQueueDAO = new OnlineFormQueueDAO(this.OpContext);
			return onlineFormQueueDAO.LoadOnlineFormQueueItems(onlineFormId, startDate, endDate, filterByAssignedCounsellorPid, statusIdsToExclude);
		}

		// Token: 0x06000655 RID: 1621 RVA: 0x00024FB8 File Offset: 0x000231B8
		[DebuggerStepThrough]
		public Task<IList<OnlineFormIdWithOpenItemsCount>> LoadOnlineFormQueueFormsWithOpenItemsCountAsync(DateTime startDate, DateTime? endDate, int filterByAssignedCounsellorPid)
		{
			OnlineFormQueueManager.<LoadOnlineFormQueueFormsWithOpenItemsCountAsync>d__9 <LoadOnlineFormQueueFormsWithOpenItemsCountAsync>d__ = new OnlineFormQueueManager.<LoadOnlineFormQueueFormsWithOpenItemsCountAsync>d__9();
			<LoadOnlineFormQueueFormsWithOpenItemsCountAsync>d__.<>t__builder = AsyncTaskMethodBuilder<IList<OnlineFormIdWithOpenItemsCount>>.Create();
			<LoadOnlineFormQueueFormsWithOpenItemsCountAsync>d__.<>4__this = this;
			<LoadOnlineFormQueueFormsWithOpenItemsCountAsync>d__.startDate = startDate;
			<LoadOnlineFormQueueFormsWithOpenItemsCountAsync>d__.endDate = endDate;
			<LoadOnlineFormQueueFormsWithOpenItemsCountAsync>d__.filterByAssignedCounsellorPid = filterByAssignedCounsellorPid;
			<LoadOnlineFormQueueFormsWithOpenItemsCountAsync>d__.<>1__state = -1;
			<LoadOnlineFormQueueFormsWithOpenItemsCountAsync>d__.<>t__builder.Start<OnlineFormQueueManager.<LoadOnlineFormQueueFormsWithOpenItemsCountAsync>d__9>(ref <LoadOnlineFormQueueFormsWithOpenItemsCountAsync>d__);
			return <LoadOnlineFormQueueFormsWithOpenItemsCountAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000656 RID: 1622 RVA: 0x00025014 File Offset: 0x00023214
		public IList<OnlineFormIdWithOpenItemsCount> LoadOnlineFormQueueFormsWithOpenItemsCount(DateTime startDate, DateTime? endDate, int filterByAssignedCounsellorPid)
		{
			IList<OnlineFormStatus> source = this.LoadLookupOnlineFormStatuses();
			int[] statusIdsToExclude = (from g in source
			where g.StatusType == eOnlineFormStatusType.ClosedComplete || g.StatusType == eOnlineFormStatusType.ClosedIncomplete
			select g into m
			select m.PeopleOnlineFormStatusId).ToArray<int>();
			IOldUserSettingManager oldUserSettingManager = new OldUserSettingManager(this.OpContext);
			List<int> settingValue_ConcatenatedIntList = oldUserSettingManager.GetSettingValue_ConcatenatedIntList(this.OpContext.WhoAmI, eSettingCode.SETTING_OnlineForms_AllowedOnlineFormsInOnlineFormsQueue);
			IOnlineFormQueueDAO onlineFormQueueDAO = new OnlineFormQueueDAO(this.OpContext);
			return onlineFormQueueDAO.LoadOnlineFormQueueFormsWithOpenItemsCount(settingValue_ConcatenatedIntList, startDate, endDate, filterByAssignedCounsellorPid, statusIdsToExclude);
		}

		// Token: 0x06000657 RID: 1623 RVA: 0x000250BC File Offset: 0x000232BC
		[DebuggerStepThrough]
		public Task<bool> DeleteOnlineFormQueueItemAsync(int peopleOnlineFormId)
		{
			OnlineFormQueueManager.<DeleteOnlineFormQueueItemAsync>d__11 <DeleteOnlineFormQueueItemAsync>d__ = new OnlineFormQueueManager.<DeleteOnlineFormQueueItemAsync>d__11();
			<DeleteOnlineFormQueueItemAsync>d__.<>t__builder = AsyncTaskMethodBuilder<bool>.Create();
			<DeleteOnlineFormQueueItemAsync>d__.<>4__this = this;
			<DeleteOnlineFormQueueItemAsync>d__.peopleOnlineFormId = peopleOnlineFormId;
			<DeleteOnlineFormQueueItemAsync>d__.<>1__state = -1;
			<DeleteOnlineFormQueueItemAsync>d__.<>t__builder.Start<OnlineFormQueueManager.<DeleteOnlineFormQueueItemAsync>d__11>(ref <DeleteOnlineFormQueueItemAsync>d__);
			return <DeleteOnlineFormQueueItemAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000658 RID: 1624 RVA: 0x00025108 File Offset: 0x00023308
		public bool DeleteOnlineFormQueueItem(int peopleOnlineFormId)
		{
			IOnlineFormQueueDAO onlineFormQueueDAO = new OnlineFormQueueDAO(this.OpContext);
			int? num = onlineFormQueueDAO.LoadOnlineFormIdByPeopleOnlineFormId(peopleOnlineFormId);
			bool flag;
			if (num != null)
			{
				int? num2 = num;
				int num3 = 1;
				flag = (num2.GetValueOrDefault() < num3 & num2 != null);
			}
			else
			{
				flag = true;
			}
			bool flag2 = flag;
			bool result;
			if (flag2)
			{
				result = false;
			}
			else
			{
				IOldUserSettingManager oldUserSettingManager = new OldUserSettingManager(this.OpContext);
				List<int> settingValue_ConcatenatedIntList = oldUserSettingManager.GetSettingValue_ConcatenatedIntList(this.OpContext.WhoAmI, eSettingCode.SETTING_OnlineForms_AllowedOnlineFormsInOnlineFormsQueue);
				int? num2 = num;
				int num3 = 1;
				bool flag3 = (num2.GetValueOrDefault() < num3 & num2 != null) || !settingValue_ConcatenatedIntList.Contains(num.Value);
				if (flag3)
				{
					throw new PermissionDeniedException("OnlineFormID not allowed (" + num.ToString() + ")");
				}
				result = onlineFormQueueDAO.DeleteOnlineFormQueueItem(peopleOnlineFormId);
			}
			return result;
		}

		// Token: 0x06000659 RID: 1625 RVA: 0x000251E4 File Offset: 0x000233E4
		[DebuggerStepThrough]
		public Task<IList<DynamicData>> LoadOnlineFormQueueItemFormDataItemsAsync(int peopleOnlineFormId)
		{
			OnlineFormQueueManager.<LoadOnlineFormQueueItemFormDataItemsAsync>d__13 <LoadOnlineFormQueueItemFormDataItemsAsync>d__ = new OnlineFormQueueManager.<LoadOnlineFormQueueItemFormDataItemsAsync>d__13();
			<LoadOnlineFormQueueItemFormDataItemsAsync>d__.<>t__builder = AsyncTaskMethodBuilder<IList<DynamicData>>.Create();
			<LoadOnlineFormQueueItemFormDataItemsAsync>d__.<>4__this = this;
			<LoadOnlineFormQueueItemFormDataItemsAsync>d__.peopleOnlineFormId = peopleOnlineFormId;
			<LoadOnlineFormQueueItemFormDataItemsAsync>d__.<>1__state = -1;
			<LoadOnlineFormQueueItemFormDataItemsAsync>d__.<>t__builder.Start<OnlineFormQueueManager.<LoadOnlineFormQueueItemFormDataItemsAsync>d__13>(ref <LoadOnlineFormQueueItemFormDataItemsAsync>d__);
			return <LoadOnlineFormQueueItemFormDataItemsAsync>d__.<>t__builder.Task;
		}

		// Token: 0x0600065A RID: 1626 RVA: 0x00025230 File Offset: 0x00023430
		public IList<DynamicData> LoadOnlineFormQueueItemFormDataItems(int peopleOnlineFormId)
		{
			OnlineFormQueueItem onlineFormQueueItem = this.LoadOnlineFormQueueItem(peopleOnlineFormId);
			bool flag = onlineFormQueueItem == null;
			IList<DynamicData> result;
			if (flag)
			{
				result = null;
			}
			else
			{
				IDynamicDataManager dynamicDataManager = new DynamicDataManager(this.OpContext);
				result = dynamicDataManager.LoadData(new DynamicDataContext
				{
					PrimaryId = onlineFormQueueItem.Student.PersonId,
					SecondaryId = onlineFormQueueItem.PeopleOnlineFormId
				}, onlineFormQueueItem.OnlineForm.ScreenNum, eDynamicFormType.OnlineForm);
			}
			return result;
		}

		// Token: 0x0600065B RID: 1627 RVA: 0x0002529C File Offset: 0x0002349C
		public OnlineFormQueueItem LoadOnlineFormQueueItem(int peopleOnlineFormId)
		{
			IOnlineFormQueueDAO onlineFormQueueDAO = new OnlineFormQueueDAO(this.OpContext);
			OnlineFormQueueItem onlineFormQueueItem = onlineFormQueueDAO.LoadOnlineFormQueueItem(peopleOnlineFormId);
			bool flag = onlineFormQueueItem == null;
			OnlineFormQueueItem result;
			if (flag)
			{
				result = onlineFormQueueItem;
			}
			else
			{
				IOldUserSettingManager oldUserSettingManager = new OldUserSettingManager(this.OpContext);
				List<int> settingValue_ConcatenatedIntList = oldUserSettingManager.GetSettingValue_ConcatenatedIntList(this.OpContext.WhoAmI, eSettingCode.SETTING_OnlineForms_AllowedOnlineFormsInOnlineFormsQueue);
				int onlineFormId = onlineFormQueueItem.OnlineForm.OnlineFormId;
				bool flag2 = onlineFormId < 1 || !settingValue_ConcatenatedIntList.Contains(onlineFormId);
				if (flag2)
				{
					throw new PermissionDeniedException("OnlineFormID not allowed (" + onlineFormId.ToString() + ")");
				}
				result = onlineFormQueueItem;
			}
			return result;
		}

		// Token: 0x0600065C RID: 1628 RVA: 0x0002533C File Offset: 0x0002353C
		[DebuggerStepThrough]
		public Task<OnlineFormQueueItem> LoadOnlineFormQueueItemAsync(int peopleOnlineFormId)
		{
			OnlineFormQueueManager.<LoadOnlineFormQueueItemAsync>d__16 <LoadOnlineFormQueueItemAsync>d__ = new OnlineFormQueueManager.<LoadOnlineFormQueueItemAsync>d__16();
			<LoadOnlineFormQueueItemAsync>d__.<>t__builder = AsyncTaskMethodBuilder<OnlineFormQueueItem>.Create();
			<LoadOnlineFormQueueItemAsync>d__.<>4__this = this;
			<LoadOnlineFormQueueItemAsync>d__.peopleOnlineFormId = peopleOnlineFormId;
			<LoadOnlineFormQueueItemAsync>d__.<>1__state = -1;
			<LoadOnlineFormQueueItemAsync>d__.<>t__builder.Start<OnlineFormQueueManager.<LoadOnlineFormQueueItemAsync>d__16>(ref <LoadOnlineFormQueueItemAsync>d__);
			return <LoadOnlineFormQueueItemAsync>d__.<>t__builder.Task;
		}

		// Token: 0x0600065D RID: 1629 RVA: 0x00025388 File Offset: 0x00023588
		[DebuggerStepThrough]
		public Task<IList<OnlineForm>> LoadAllowedOnlineFormsAsync()
		{
			OnlineFormQueueManager.<LoadAllowedOnlineFormsAsync>d__17 <LoadAllowedOnlineFormsAsync>d__ = new OnlineFormQueueManager.<LoadAllowedOnlineFormsAsync>d__17();
			<LoadAllowedOnlineFormsAsync>d__.<>t__builder = AsyncTaskMethodBuilder<IList<OnlineForm>>.Create();
			<LoadAllowedOnlineFormsAsync>d__.<>4__this = this;
			<LoadAllowedOnlineFormsAsync>d__.<>1__state = -1;
			<LoadAllowedOnlineFormsAsync>d__.<>t__builder.Start<OnlineFormQueueManager.<LoadAllowedOnlineFormsAsync>d__17>(ref <LoadAllowedOnlineFormsAsync>d__);
			return <LoadAllowedOnlineFormsAsync>d__.<>t__builder.Task;
		}

		// Token: 0x0600065E RID: 1630 RVA: 0x000253CC File Offset: 0x000235CC
		public IList<OnlineForm> LoadAllowedOnlineForms()
		{
			IOnlineFormManager onlineFormManager = new OnlineFormManager(this.OpContext);
			List<OnlineForm> allOnlineForms = onlineFormManager.GetAllOnlineForms();
			IOldUserSettingManager oldUserSettingManager = new OldUserSettingManager(this.OpContext);
			List<int> allowedOnlineFormIds = oldUserSettingManager.GetSettingValue_ConcatenatedIntList(this.OpContext.WhoAmI, eSettingCode.SETTING_OnlineForms_AllowedOnlineFormsInOnlineFormsQueue);
			return (allOnlineForms != null) ? (from g in allOnlineForms
			where allowedOnlineFormIds.Contains(g.OnlineFormId)
			select g).ToList<OnlineForm>() : null;
		}

		// Token: 0x0600065F RID: 1631 RVA: 0x00025440 File Offset: 0x00023640
		[DebuggerStepThrough]
		public Task<OnlineFormQueueItem> UpdateOnlineFormQueueItemStaffNoteAndStatusAsync(int peopleOnlineFormId, int? newPeopleOnlineFormStatusId, string newStaffNote)
		{
			OnlineFormQueueManager.<UpdateOnlineFormQueueItemStaffNoteAndStatusAsync>d__19 <UpdateOnlineFormQueueItemStaffNoteAndStatusAsync>d__ = new OnlineFormQueueManager.<UpdateOnlineFormQueueItemStaffNoteAndStatusAsync>d__19();
			<UpdateOnlineFormQueueItemStaffNoteAndStatusAsync>d__.<>t__builder = AsyncTaskMethodBuilder<OnlineFormQueueItem>.Create();
			<UpdateOnlineFormQueueItemStaffNoteAndStatusAsync>d__.<>4__this = this;
			<UpdateOnlineFormQueueItemStaffNoteAndStatusAsync>d__.peopleOnlineFormId = peopleOnlineFormId;
			<UpdateOnlineFormQueueItemStaffNoteAndStatusAsync>d__.newPeopleOnlineFormStatusId = newPeopleOnlineFormStatusId;
			<UpdateOnlineFormQueueItemStaffNoteAndStatusAsync>d__.newStaffNote = newStaffNote;
			<UpdateOnlineFormQueueItemStaffNoteAndStatusAsync>d__.<>1__state = -1;
			<UpdateOnlineFormQueueItemStaffNoteAndStatusAsync>d__.<>t__builder.Start<OnlineFormQueueManager.<UpdateOnlineFormQueueItemStaffNoteAndStatusAsync>d__19>(ref <UpdateOnlineFormQueueItemStaffNoteAndStatusAsync>d__);
			return <UpdateOnlineFormQueueItemStaffNoteAndStatusAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000660 RID: 1632 RVA: 0x0002549C File Offset: 0x0002369C
		public OnlineFormQueueItem UpdateOnlineFormQueueItemStaffNoteAndStatus(int peopleOnlineFormId, int? newPeopleOnlineFormStatusId, string newStaffNote)
		{
			IOnlineFormQueueDAO dao = new OnlineFormQueueDAO(this.OpContext);
			return this.UpdateOnlineFormQueueItem(peopleOnlineFormId, () => dao.UpdateOnlineFormQueueItemStaffNoteAndStatus(peopleOnlineFormId, newPeopleOnlineFormStatusId, newStaffNote));
		}

		// Token: 0x06000661 RID: 1633 RVA: 0x000254F4 File Offset: 0x000236F4
		[DebuggerStepThrough]
		public Task<OnlineFormQueueItem> UpdateOnlineFormQueueItemStaffNoteAsync(int peopleOnlineFormId, string newStaffNote)
		{
			OnlineFormQueueManager.<UpdateOnlineFormQueueItemStaffNoteAsync>d__21 <UpdateOnlineFormQueueItemStaffNoteAsync>d__ = new OnlineFormQueueManager.<UpdateOnlineFormQueueItemStaffNoteAsync>d__21();
			<UpdateOnlineFormQueueItemStaffNoteAsync>d__.<>t__builder = AsyncTaskMethodBuilder<OnlineFormQueueItem>.Create();
			<UpdateOnlineFormQueueItemStaffNoteAsync>d__.<>4__this = this;
			<UpdateOnlineFormQueueItemStaffNoteAsync>d__.peopleOnlineFormId = peopleOnlineFormId;
			<UpdateOnlineFormQueueItemStaffNoteAsync>d__.newStaffNote = newStaffNote;
			<UpdateOnlineFormQueueItemStaffNoteAsync>d__.<>1__state = -1;
			<UpdateOnlineFormQueueItemStaffNoteAsync>d__.<>t__builder.Start<OnlineFormQueueManager.<UpdateOnlineFormQueueItemStaffNoteAsync>d__21>(ref <UpdateOnlineFormQueueItemStaffNoteAsync>d__);
			return <UpdateOnlineFormQueueItemStaffNoteAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000662 RID: 1634 RVA: 0x00025548 File Offset: 0x00023748
		public OnlineFormQueueItem UpdateOnlineFormQueueItemStaffNote(int peopleOnlineFormId, string newStaffNote)
		{
			IOnlineFormQueueDAO dao = new OnlineFormQueueDAO(this.OpContext);
			return this.UpdateOnlineFormQueueItem(peopleOnlineFormId, () => dao.UpdateOnlineFormQueueItemStaffNote(peopleOnlineFormId, newStaffNote));
		}

		// Token: 0x06000663 RID: 1635 RVA: 0x00025598 File Offset: 0x00023798
		[DebuggerStepThrough]
		public Task<OnlineFormQueueItem> UpdateOnlineFormQueueItemStatusAsync(int peopleOnlineFormId, int? newPeopleOnlineFormStatusId)
		{
			OnlineFormQueueManager.<UpdateOnlineFormQueueItemStatusAsync>d__23 <UpdateOnlineFormQueueItemStatusAsync>d__ = new OnlineFormQueueManager.<UpdateOnlineFormQueueItemStatusAsync>d__23();
			<UpdateOnlineFormQueueItemStatusAsync>d__.<>t__builder = AsyncTaskMethodBuilder<OnlineFormQueueItem>.Create();
			<UpdateOnlineFormQueueItemStatusAsync>d__.<>4__this = this;
			<UpdateOnlineFormQueueItemStatusAsync>d__.peopleOnlineFormId = peopleOnlineFormId;
			<UpdateOnlineFormQueueItemStatusAsync>d__.newPeopleOnlineFormStatusId = newPeopleOnlineFormStatusId;
			<UpdateOnlineFormQueueItemStatusAsync>d__.<>1__state = -1;
			<UpdateOnlineFormQueueItemStatusAsync>d__.<>t__builder.Start<OnlineFormQueueManager.<UpdateOnlineFormQueueItemStatusAsync>d__23>(ref <UpdateOnlineFormQueueItemStatusAsync>d__);
			return <UpdateOnlineFormQueueItemStatusAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000664 RID: 1636 RVA: 0x000255EC File Offset: 0x000237EC
		public OnlineFormQueueItem UpdateOnlineFormQueueItemStatus(int peopleOnlineFormId, int? newPeopleOnlineFormStatusId)
		{
			IOnlineFormQueueDAO dao = new OnlineFormQueueDAO(this.OpContext);
			return this.UpdateOnlineFormQueueItem(peopleOnlineFormId, () => dao.UpdateOnlineFormQueueItemStatus(peopleOnlineFormId, newPeopleOnlineFormStatusId));
		}

		// Token: 0x06000665 RID: 1637 RVA: 0x0002563C File Offset: 0x0002383C
		[DebuggerStepThrough]
		private Task<OnlineFormQueueItem> UpdateOnlineFormQueueItemAsync(int peopleOnlineFormId, Func<Task<OnlineFormQueueItem>> updateItem)
		{
			OnlineFormQueueManager.<UpdateOnlineFormQueueItemAsync>d__25 <UpdateOnlineFormQueueItemAsync>d__ = new OnlineFormQueueManager.<UpdateOnlineFormQueueItemAsync>d__25();
			<UpdateOnlineFormQueueItemAsync>d__.<>t__builder = AsyncTaskMethodBuilder<OnlineFormQueueItem>.Create();
			<UpdateOnlineFormQueueItemAsync>d__.<>4__this = this;
			<UpdateOnlineFormQueueItemAsync>d__.peopleOnlineFormId = peopleOnlineFormId;
			<UpdateOnlineFormQueueItemAsync>d__.updateItem = updateItem;
			<UpdateOnlineFormQueueItemAsync>d__.<>1__state = -1;
			<UpdateOnlineFormQueueItemAsync>d__.<>t__builder.Start<OnlineFormQueueManager.<UpdateOnlineFormQueueItemAsync>d__25>(ref <UpdateOnlineFormQueueItemAsync>d__);
			return <UpdateOnlineFormQueueItemAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000666 RID: 1638 RVA: 0x00025690 File Offset: 0x00023890
		private OnlineFormQueueItem UpdateOnlineFormQueueItem(int peopleOnlineFormId, Func<OnlineFormQueueItem> updateItem)
		{
			bool flag = peopleOnlineFormId < 1;
			OnlineFormQueueItem result;
			if (flag)
			{
				result = null;
			}
			else
			{
				IOnlineFormQueueDAO onlineFormQueueDAO = new OnlineFormQueueDAO(this.OpContext);
				int? num = onlineFormQueueDAO.LoadOnlineFormIdByPeopleOnlineFormId(peopleOnlineFormId);
				bool flag2;
				if (num != null)
				{
					int? num2 = num;
					int num3 = 1;
					flag2 = (num2.GetValueOrDefault() < num3 & num2 != null);
				}
				else
				{
					flag2 = true;
				}
				bool flag3 = flag2;
				if (flag3)
				{
					result = null;
				}
				else
				{
					IOldUserSettingManager oldUserSettingManager = new OldUserSettingManager(this.OpContext);
					List<int> settingValue_ConcatenatedIntList = oldUserSettingManager.GetSettingValue_ConcatenatedIntList(this.OpContext.WhoAmI, eSettingCode.SETTING_OnlineForms_AllowedOnlineFormsInOnlineFormsQueue);
					bool flag4 = !settingValue_ConcatenatedIntList.Contains(num.Value);
					if (flag4)
					{
						throw new PermissionDeniedException("OnlineFormID not allowed (" + num.ToString() + ")");
					}
					result = updateItem();
				}
			}
			return result;
		}

		// Token: 0x06000667 RID: 1639 RVA: 0x0002575C File Offset: 0x0002395C
		[DebuggerStepThrough]
		public Task<IList<OnlineFormQueueItem>> LoadAllStudentOnlineFormsAsync(int studentPersonId)
		{
			OnlineFormQueueManager.<LoadAllStudentOnlineFormsAsync>d__27 <LoadAllStudentOnlineFormsAsync>d__ = new OnlineFormQueueManager.<LoadAllStudentOnlineFormsAsync>d__27();
			<LoadAllStudentOnlineFormsAsync>d__.<>t__builder = AsyncTaskMethodBuilder<IList<OnlineFormQueueItem>>.Create();
			<LoadAllStudentOnlineFormsAsync>d__.<>4__this = this;
			<LoadAllStudentOnlineFormsAsync>d__.studentPersonId = studentPersonId;
			<LoadAllStudentOnlineFormsAsync>d__.<>1__state = -1;
			<LoadAllStudentOnlineFormsAsync>d__.<>t__builder.Start<OnlineFormQueueManager.<LoadAllStudentOnlineFormsAsync>d__27>(ref <LoadAllStudentOnlineFormsAsync>d__);
			return <LoadAllStudentOnlineFormsAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000668 RID: 1640 RVA: 0x000257A8 File Offset: 0x000239A8
		public IList<OnlineFormQueueItem> LoadAllStudentOnlineForms(int studentPersonId)
		{
			IOnlineFormQueueDAO onlineFormQueueDAO = new OnlineFormQueueDAO(this.OpContext);
			IList<OnlineFormQueueItem> list = onlineFormQueueDAO.LoadAllStudentOnlineForms(studentPersonId);
			IOldUserSettingManager oldUserSettingManager = new OldUserSettingManager(this.OpContext);
			List<int> allowedOnlineFormIds = oldUserSettingManager.GetSettingValue_ConcatenatedIntList(this.OpContext.WhoAmI, eSettingCode.SETTING_OnlineForms_AllowedOnlineFormsInOnlineFormsQueue);
			return (list != null) ? (from g in list
			where allowedOnlineFormIds.Contains(g.OnlineForm.OnlineFormId)
			select g).ToList<OnlineFormQueueItem>() : null;
		}
	}
}
