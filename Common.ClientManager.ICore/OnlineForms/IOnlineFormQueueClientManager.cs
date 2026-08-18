using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms;
using TechnoPro.ClockWorkServer.Contracts.DTO.OnlineForms;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities.OnlineForms;

namespace TechnoPro.Common.ClientManager.ICore.OnlineForms
{
	// Token: 0x02000031 RID: 49
	public interface IOnlineFormQueueClientManager : IWebService
	{
		// Token: 0x06000149 RID: 329
		Task<IList<OnlineFormStatusDTO>> LoadLookupOnlineFormStatusesAsync();

		// Token: 0x0600014A RID: 330
		Task<IList<OnlineFormQueueItemDTO>> LoadOnlineFormQueueItemsAsync(int OnlineFormId, DateTime startDate, DateTime? endDate, int filterByAssignedCounsellorPid, params eOnlineFormStatusType[] onlineFormTypesToExclude);

		// Token: 0x0600014B RID: 331
		Task<OnlineFormQueueItemDTO> UpdateOnlineFormQueueItemStaffNoteAndStatusAsync(int peopleOnlineFormId, int? newPeopleOnlineFormStatusId, string newStaffNote);

		// Token: 0x0600014C RID: 332
		Task<OnlineFormQueueItemDTO> UpdateOnlineFormQueueItemStaffNoteAsync(int peopleOnlineFormId, string newStaffNote);

		// Token: 0x0600014D RID: 333
		Task<OnlineFormQueueItemDTO> UpdateOnlineFormQueueItemStatusAsync(int peopleOnlineFormId, int? newPeopleOnlineFormStatusId);

		// Token: 0x0600014E RID: 334
		Task<IList<OnlineFormDTO>> LoadAllowedOnlineFormsAsync();

		// Token: 0x0600014F RID: 335
		Task<bool> DeleteOnlineFormQueueItemAsync(int peopleOnlineFormId);

		// Token: 0x06000150 RID: 336
		Task<IList<DynamicDataDTO>> LoadOnlineFormQueueItemFormDataItemsAsync(int peopleOnlineFormId);

		// Token: 0x06000151 RID: 337
		Task<OnlineFormQueueItemDTO> LoadOnlineFormQueueItemAsync(int peopleOnlineFormId);

		// Token: 0x06000152 RID: 338
		Task<IList<OnlineFormQueueItemDTO>> LoadAllStudentOnlineFormsAsync(int studentPersonId);

		// Token: 0x06000153 RID: 339
		Task<IList<OnlineFormIdWithOpenItemsCountDTO>> LoadOnlineFormQueueFormsWithOpenItemsCountAsync(DateTime startDate, DateTime? endDate, int filterByAssignedCounsellorPid);
	}
}
