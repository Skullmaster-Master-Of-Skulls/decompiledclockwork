using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.DynamicForms;
using TechnoPro.Common.Public.Entities.OnlineForms;

namespace TechnoPro.Common.ICore.OnlineForms
{
	// Token: 0x02000058 RID: 88
	public interface IOnlineFormQueueManager : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x06000250 RID: 592
		Task<IList<OnlineFormStatus>> LoadLookupOnlineFormStatusesAsync();

		// Token: 0x06000251 RID: 593
		Task<IList<OnlineFormQueueItem>> LoadOnlineFormQueueItemsAsync(int onlineFormId, DateTime startDate, DateTime? endDate, int filterByAssignedCounsellorPid, params eOnlineFormStatusType[] onlineFormTypesToExclude);

		// Token: 0x06000252 RID: 594
		Task<IList<OnlineFormIdWithOpenItemsCount>> LoadOnlineFormQueueFormsWithOpenItemsCountAsync(DateTime startDate, DateTime? endDate, int filterByAssignedCounsellorPid);

		// Token: 0x06000253 RID: 595
		Task<OnlineFormQueueItem> UpdateOnlineFormQueueItemStaffNoteAndStatusAsync(int peopleOnlineFormId, int? newPeopleOnlineFormStatusId, string newStaffNote);

		// Token: 0x06000254 RID: 596
		Task<OnlineFormQueueItem> UpdateOnlineFormQueueItemStaffNoteAsync(int peopleOnlineFormId, string newStaffNote);

		// Token: 0x06000255 RID: 597
		Task<OnlineFormQueueItem> UpdateOnlineFormQueueItemStatusAsync(int peopleOnlineFormId, int? newPeopleOnlineFormStatusId);

		// Token: 0x06000256 RID: 598
		Task<bool> DeleteOnlineFormQueueItemAsync(int peopleOnlineFormId);

		// Token: 0x06000257 RID: 599
		Task<IList<DynamicData>> LoadOnlineFormQueueItemFormDataItemsAsync(int peopleOnlineFormId);

		// Token: 0x06000258 RID: 600
		Task<OnlineFormQueueItem> LoadOnlineFormQueueItemAsync(int peopleOnlineFormId);

		// Token: 0x06000259 RID: 601
		Task<IList<OnlineForm>> LoadAllowedOnlineFormsAsync();

		// Token: 0x0600025A RID: 602
		Task<IList<OnlineFormQueueItem>> LoadAllStudentOnlineFormsAsync(int studentPersonId);

		// Token: 0x0600025B RID: 603
		IList<DynamicData> LoadOnlineFormQueueItemFormDataItems(int peopleOnlineFormId);

		// Token: 0x0600025C RID: 604
		IList<OnlineFormStatus> LoadLookupOnlineFormStatuses();

		// Token: 0x0600025D RID: 605
		IList<OnlineFormQueueItem> LoadOnlineFormQueueItems(int onlineFormId, DateTime startDate, DateTime? endDate, int filterByAssignedCounsellorPid, params eOnlineFormStatusType[] onlineFormTypesToExclude);

		// Token: 0x0600025E RID: 606
		IList<OnlineFormIdWithOpenItemsCount> LoadOnlineFormQueueFormsWithOpenItemsCount(DateTime startDate, DateTime? endDate, int filterByAssignedCounsellorPid);

		// Token: 0x0600025F RID: 607
		OnlineFormQueueItem UpdateOnlineFormQueueItemStaffNoteAndStatus(int peopleOnlineFormId, int? newPeopleOnlineFormStatusId, string newStaffNote);

		// Token: 0x06000260 RID: 608
		OnlineFormQueueItem UpdateOnlineFormQueueItemStaffNote(int peopleOnlineFormId, string newStaffNote);

		// Token: 0x06000261 RID: 609
		OnlineFormQueueItem UpdateOnlineFormQueueItemStatus(int peopleOnlineFormId, int? newPeopleOnlineFormStatusId);

		// Token: 0x06000262 RID: 610
		bool DeleteOnlineFormQueueItem(int peopleOnlineFormId);

		// Token: 0x06000263 RID: 611
		OnlineFormQueueItem LoadOnlineFormQueueItem(int peopleOnlineFormId);

		// Token: 0x06000264 RID: 612
		IList<OnlineForm> LoadAllowedOnlineForms();

		// Token: 0x06000265 RID: 613
		IList<OnlineFormQueueItem> LoadAllStudentOnlineForms(int studentPersonId);
	}
}
