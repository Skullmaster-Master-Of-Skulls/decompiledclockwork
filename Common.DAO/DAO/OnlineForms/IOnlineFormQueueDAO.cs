using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.OnlineForms;

namespace TechnoPro.Common.DAO.OnlineForms
{
	// Token: 0x02000046 RID: 70
	public interface IOnlineFormQueueDAO : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x0600016B RID: 363
		Task<IList<OnlineFormStatus>> LoadLookupOnlineFormStatusesAsync();

		// Token: 0x0600016C RID: 364
		Task<IList<OnlineFormQueueItem>> LoadOnlineFormQueueItemsAsync(int OnlineFormId, DateTime startDate, DateTime? endDate, int filterByAssignedCounsellorPid, params int[] statusIdsToExclude);

		// Token: 0x0600016D RID: 365
		Task<OnlineFormQueueItem> UpdateOnlineFormQueueItemStaffNoteAndStatusAsync(int peopleOnlineFormId, int? newPeopleOnlineFormStatusId, string newStaffNote);

		// Token: 0x0600016E RID: 366
		Task<OnlineFormQueueItem> UpdateOnlineFormQueueItemStaffNoteAsync(int peopleOnlineFormId, string newStaffNote);

		// Token: 0x0600016F RID: 367
		Task<OnlineFormQueueItem> UpdateOnlineFormQueueItemStatusAsync(int peopleOnlineFormId, int? newPeopleOnlineFormStatusId);

		// Token: 0x06000170 RID: 368
		Task<bool> DeleteOnlineFormQueueItemAsync(int peopleOnlineFormId);

		// Token: 0x06000171 RID: 369
		Task<OnlineFormQueueItem> LoadOnlineFormQueueItemAsync(int peopleOnlineFormId);

		// Token: 0x06000172 RID: 370
		Task<int?> LoadOnlineFormIdByPeopleOnlineFormIdAsync(int peopleOnlineFormId);

		// Token: 0x06000173 RID: 371
		Task<IList<OnlineFormQueueItem>> LoadAllStudentOnlineFormsAsync(int studentPersonId);

		// Token: 0x06000174 RID: 372
		Task<IList<OnlineFormIdWithOpenItemsCount>> LoadOnlineFormQueueFormsWithOpenItemsCountAsync(IList<int> onlineFormIds, DateTime startDate, DateTime? endDate, int filterByAssignedCounsellorPid, params int[] statusIdsToExclude);

		// Token: 0x06000175 RID: 373
		OnlineFormQueueItem LoadOnlineFormQueueItem(int peopleOnlineFormId);

		// Token: 0x06000176 RID: 374
		IList<OnlineFormStatus> LoadLookupOnlineFormStatuses();

		// Token: 0x06000177 RID: 375
		IList<OnlineFormQueueItem> LoadOnlineFormQueueItems(int OnlineFormId, DateTime startDate, DateTime? endDate, int filterByAssignedCounsellorPid, params int[] statusIdsToExclude);

		// Token: 0x06000178 RID: 376
		OnlineFormQueueItem UpdateOnlineFormQueueItemStaffNoteAndStatus(int peopleOnlineFormId, int? newPeopleOnlineFormStatusId, string newStaffNote);

		// Token: 0x06000179 RID: 377
		OnlineFormQueueItem UpdateOnlineFormQueueItemStaffNote(int peopleOnlineFormId, string newStaffNote);

		// Token: 0x0600017A RID: 378
		OnlineFormQueueItem UpdateOnlineFormQueueItemStatus(int peopleOnlineFormId, int? newPeopleOnlineFormStatusId);

		// Token: 0x0600017B RID: 379
		bool DeleteOnlineFormQueueItem(int peopleOnlineFormId);

		// Token: 0x0600017C RID: 380
		int? LoadOnlineFormIdByPeopleOnlineFormId(int peopleOnlineFormId);

		// Token: 0x0600017D RID: 381
		IList<OnlineFormQueueItem> LoadAllStudentOnlineForms(int studentPersonId);

		// Token: 0x0600017E RID: 382
		IList<OnlineFormIdWithOpenItemsCount> LoadOnlineFormQueueFormsWithOpenItemsCount(IList<int> onlineFormIds, DateTime startDate, DateTime? endDate, int filterByAssignedCounsellorPid, params int[] statusIdsToExclude);
	}
}
