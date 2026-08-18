using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms;
using TechnoPro.ClockWorkServer.Contracts.DTO.OnlineForms;
using TechnoPro.Common.ClientManager.ICore.OnlineForms;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities.OnlineForms;

namespace TechnoPro.Common.ClientManager.Core.OnlineForms
{
	// Token: 0x02000035 RID: 53
	public class OnlineFormQueueClientManager : IOnlineFormQueueClientManager, IWebService
	{
		// Token: 0x060001DA RID: 474 RVA: 0x00009094 File Offset: 0x00007294
		[DebuggerStepThrough]
		public Task<bool> DeleteOnlineFormQueueItemAsync(int peopleOnlineFormId)
		{
			OnlineFormQueueClientManager.<DeleteOnlineFormQueueItemAsync>d__0 <DeleteOnlineFormQueueItemAsync>d__ = new OnlineFormQueueClientManager.<DeleteOnlineFormQueueItemAsync>d__0();
			<DeleteOnlineFormQueueItemAsync>d__.<>t__builder = AsyncTaskMethodBuilder<bool>.Create();
			<DeleteOnlineFormQueueItemAsync>d__.<>4__this = this;
			<DeleteOnlineFormQueueItemAsync>d__.peopleOnlineFormId = peopleOnlineFormId;
			<DeleteOnlineFormQueueItemAsync>d__.<>1__state = -1;
			<DeleteOnlineFormQueueItemAsync>d__.<>t__builder.Start<OnlineFormQueueClientManager.<DeleteOnlineFormQueueItemAsync>d__0>(ref <DeleteOnlineFormQueueItemAsync>d__);
			return <DeleteOnlineFormQueueItemAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060001DB RID: 475 RVA: 0x000090E0 File Offset: 0x000072E0
		[DebuggerStepThrough]
		public Task<IList<OnlineFormStatusDTO>> LoadLookupOnlineFormStatusesAsync()
		{
			OnlineFormQueueClientManager.<LoadLookupOnlineFormStatusesAsync>d__1 <LoadLookupOnlineFormStatusesAsync>d__ = new OnlineFormQueueClientManager.<LoadLookupOnlineFormStatusesAsync>d__1();
			<LoadLookupOnlineFormStatusesAsync>d__.<>t__builder = AsyncTaskMethodBuilder<IList<OnlineFormStatusDTO>>.Create();
			<LoadLookupOnlineFormStatusesAsync>d__.<>4__this = this;
			<LoadLookupOnlineFormStatusesAsync>d__.<>1__state = -1;
			<LoadLookupOnlineFormStatusesAsync>d__.<>t__builder.Start<OnlineFormQueueClientManager.<LoadLookupOnlineFormStatusesAsync>d__1>(ref <LoadLookupOnlineFormStatusesAsync>d__);
			return <LoadLookupOnlineFormStatusesAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060001DC RID: 476 RVA: 0x00009124 File Offset: 0x00007324
		[DebuggerStepThrough]
		public Task<OnlineFormQueueItemDTO> LoadOnlineFormQueueItemAsync(int peopleOnlineFormId)
		{
			OnlineFormQueueClientManager.<LoadOnlineFormQueueItemAsync>d__2 <LoadOnlineFormQueueItemAsync>d__ = new OnlineFormQueueClientManager.<LoadOnlineFormQueueItemAsync>d__2();
			<LoadOnlineFormQueueItemAsync>d__.<>t__builder = AsyncTaskMethodBuilder<OnlineFormQueueItemDTO>.Create();
			<LoadOnlineFormQueueItemAsync>d__.<>4__this = this;
			<LoadOnlineFormQueueItemAsync>d__.peopleOnlineFormId = peopleOnlineFormId;
			<LoadOnlineFormQueueItemAsync>d__.<>1__state = -1;
			<LoadOnlineFormQueueItemAsync>d__.<>t__builder.Start<OnlineFormQueueClientManager.<LoadOnlineFormQueueItemAsync>d__2>(ref <LoadOnlineFormQueueItemAsync>d__);
			return <LoadOnlineFormQueueItemAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060001DD RID: 477 RVA: 0x00009170 File Offset: 0x00007370
		[DebuggerStepThrough]
		public Task<IList<DynamicDataDTO>> LoadOnlineFormQueueItemFormDataItemsAsync(int peopleOnlineFormId)
		{
			OnlineFormQueueClientManager.<LoadOnlineFormQueueItemFormDataItemsAsync>d__3 <LoadOnlineFormQueueItemFormDataItemsAsync>d__ = new OnlineFormQueueClientManager.<LoadOnlineFormQueueItemFormDataItemsAsync>d__3();
			<LoadOnlineFormQueueItemFormDataItemsAsync>d__.<>t__builder = AsyncTaskMethodBuilder<IList<DynamicDataDTO>>.Create();
			<LoadOnlineFormQueueItemFormDataItemsAsync>d__.<>4__this = this;
			<LoadOnlineFormQueueItemFormDataItemsAsync>d__.peopleOnlineFormId = peopleOnlineFormId;
			<LoadOnlineFormQueueItemFormDataItemsAsync>d__.<>1__state = -1;
			<LoadOnlineFormQueueItemFormDataItemsAsync>d__.<>t__builder.Start<OnlineFormQueueClientManager.<LoadOnlineFormQueueItemFormDataItemsAsync>d__3>(ref <LoadOnlineFormQueueItemFormDataItemsAsync>d__);
			return <LoadOnlineFormQueueItemFormDataItemsAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060001DE RID: 478 RVA: 0x000091BC File Offset: 0x000073BC
		[DebuggerStepThrough]
		public Task<IList<OnlineFormQueueItemDTO>> LoadOnlineFormQueueItemsAsync(int surveyId, DateTime startDate, DateTime? endDate, int filterByAssignedCounsellorPid, params eOnlineFormStatusType[] surveyTypesToExclude)
		{
			OnlineFormQueueClientManager.<LoadOnlineFormQueueItemsAsync>d__4 <LoadOnlineFormQueueItemsAsync>d__ = new OnlineFormQueueClientManager.<LoadOnlineFormQueueItemsAsync>d__4();
			<LoadOnlineFormQueueItemsAsync>d__.<>t__builder = AsyncTaskMethodBuilder<IList<OnlineFormQueueItemDTO>>.Create();
			<LoadOnlineFormQueueItemsAsync>d__.<>4__this = this;
			<LoadOnlineFormQueueItemsAsync>d__.surveyId = surveyId;
			<LoadOnlineFormQueueItemsAsync>d__.startDate = startDate;
			<LoadOnlineFormQueueItemsAsync>d__.endDate = endDate;
			<LoadOnlineFormQueueItemsAsync>d__.filterByAssignedCounsellorPid = filterByAssignedCounsellorPid;
			<LoadOnlineFormQueueItemsAsync>d__.surveyTypesToExclude = surveyTypesToExclude;
			<LoadOnlineFormQueueItemsAsync>d__.<>1__state = -1;
			<LoadOnlineFormQueueItemsAsync>d__.<>t__builder.Start<OnlineFormQueueClientManager.<LoadOnlineFormQueueItemsAsync>d__4>(ref <LoadOnlineFormQueueItemsAsync>d__);
			return <LoadOnlineFormQueueItemsAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060001DF RID: 479 RVA: 0x00009228 File Offset: 0x00007428
		[DebuggerStepThrough]
		public Task<OnlineFormQueueItemDTO> UpdateOnlineFormQueueItemStaffNoteAndStatusAsync(int peopleOnlineFormId, int? newPeopleOnlineFormStatusId, string newStaffNote)
		{
			OnlineFormQueueClientManager.<UpdateOnlineFormQueueItemStaffNoteAndStatusAsync>d__5 <UpdateOnlineFormQueueItemStaffNoteAndStatusAsync>d__ = new OnlineFormQueueClientManager.<UpdateOnlineFormQueueItemStaffNoteAndStatusAsync>d__5();
			<UpdateOnlineFormQueueItemStaffNoteAndStatusAsync>d__.<>t__builder = AsyncTaskMethodBuilder<OnlineFormQueueItemDTO>.Create();
			<UpdateOnlineFormQueueItemStaffNoteAndStatusAsync>d__.<>4__this = this;
			<UpdateOnlineFormQueueItemStaffNoteAndStatusAsync>d__.peopleOnlineFormId = peopleOnlineFormId;
			<UpdateOnlineFormQueueItemStaffNoteAndStatusAsync>d__.newPeopleOnlineFormStatusId = newPeopleOnlineFormStatusId;
			<UpdateOnlineFormQueueItemStaffNoteAndStatusAsync>d__.newStaffNote = newStaffNote;
			<UpdateOnlineFormQueueItemStaffNoteAndStatusAsync>d__.<>1__state = -1;
			<UpdateOnlineFormQueueItemStaffNoteAndStatusAsync>d__.<>t__builder.Start<OnlineFormQueueClientManager.<UpdateOnlineFormQueueItemStaffNoteAndStatusAsync>d__5>(ref <UpdateOnlineFormQueueItemStaffNoteAndStatusAsync>d__);
			return <UpdateOnlineFormQueueItemStaffNoteAndStatusAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060001E0 RID: 480 RVA: 0x00009284 File Offset: 0x00007484
		[DebuggerStepThrough]
		public Task<OnlineFormQueueItemDTO> UpdateOnlineFormQueueItemStaffNoteAsync(int peopleOnlineFormId, string newStaffNote)
		{
			OnlineFormQueueClientManager.<UpdateOnlineFormQueueItemStaffNoteAsync>d__6 <UpdateOnlineFormQueueItemStaffNoteAsync>d__ = new OnlineFormQueueClientManager.<UpdateOnlineFormQueueItemStaffNoteAsync>d__6();
			<UpdateOnlineFormQueueItemStaffNoteAsync>d__.<>t__builder = AsyncTaskMethodBuilder<OnlineFormQueueItemDTO>.Create();
			<UpdateOnlineFormQueueItemStaffNoteAsync>d__.<>4__this = this;
			<UpdateOnlineFormQueueItemStaffNoteAsync>d__.peopleOnlineFormId = peopleOnlineFormId;
			<UpdateOnlineFormQueueItemStaffNoteAsync>d__.newStaffNote = newStaffNote;
			<UpdateOnlineFormQueueItemStaffNoteAsync>d__.<>1__state = -1;
			<UpdateOnlineFormQueueItemStaffNoteAsync>d__.<>t__builder.Start<OnlineFormQueueClientManager.<UpdateOnlineFormQueueItemStaffNoteAsync>d__6>(ref <UpdateOnlineFormQueueItemStaffNoteAsync>d__);
			return <UpdateOnlineFormQueueItemStaffNoteAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060001E1 RID: 481 RVA: 0x000092D8 File Offset: 0x000074D8
		[DebuggerStepThrough]
		public Task<OnlineFormQueueItemDTO> UpdateOnlineFormQueueItemStatusAsync(int peopleOnlineFormId, int? newPeopleOnlineFormStatusId)
		{
			OnlineFormQueueClientManager.<UpdateOnlineFormQueueItemStatusAsync>d__7 <UpdateOnlineFormQueueItemStatusAsync>d__ = new OnlineFormQueueClientManager.<UpdateOnlineFormQueueItemStatusAsync>d__7();
			<UpdateOnlineFormQueueItemStatusAsync>d__.<>t__builder = AsyncTaskMethodBuilder<OnlineFormQueueItemDTO>.Create();
			<UpdateOnlineFormQueueItemStatusAsync>d__.<>4__this = this;
			<UpdateOnlineFormQueueItemStatusAsync>d__.peopleOnlineFormId = peopleOnlineFormId;
			<UpdateOnlineFormQueueItemStatusAsync>d__.newPeopleOnlineFormStatusId = newPeopleOnlineFormStatusId;
			<UpdateOnlineFormQueueItemStatusAsync>d__.<>1__state = -1;
			<UpdateOnlineFormQueueItemStatusAsync>d__.<>t__builder.Start<OnlineFormQueueClientManager.<UpdateOnlineFormQueueItemStatusAsync>d__7>(ref <UpdateOnlineFormQueueItemStatusAsync>d__);
			return <UpdateOnlineFormQueueItemStatusAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060001E2 RID: 482 RVA: 0x0000932C File Offset: 0x0000752C
		[DebuggerStepThrough]
		public Task<IList<OnlineFormDTO>> LoadAllowedOnlineFormsAsync()
		{
			OnlineFormQueueClientManager.<LoadAllowedOnlineFormsAsync>d__8 <LoadAllowedOnlineFormsAsync>d__ = new OnlineFormQueueClientManager.<LoadAllowedOnlineFormsAsync>d__8();
			<LoadAllowedOnlineFormsAsync>d__.<>t__builder = AsyncTaskMethodBuilder<IList<OnlineFormDTO>>.Create();
			<LoadAllowedOnlineFormsAsync>d__.<>4__this = this;
			<LoadAllowedOnlineFormsAsync>d__.<>1__state = -1;
			<LoadAllowedOnlineFormsAsync>d__.<>t__builder.Start<OnlineFormQueueClientManager.<LoadAllowedOnlineFormsAsync>d__8>(ref <LoadAllowedOnlineFormsAsync>d__);
			return <LoadAllowedOnlineFormsAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060001E3 RID: 483 RVA: 0x00009370 File Offset: 0x00007570
		[DebuggerStepThrough]
		public Task<IList<OnlineFormQueueItemDTO>> LoadAllStudentOnlineFormsAsync(int studentPersonId)
		{
			OnlineFormQueueClientManager.<LoadAllStudentOnlineFormsAsync>d__9 <LoadAllStudentOnlineFormsAsync>d__ = new OnlineFormQueueClientManager.<LoadAllStudentOnlineFormsAsync>d__9();
			<LoadAllStudentOnlineFormsAsync>d__.<>t__builder = AsyncTaskMethodBuilder<IList<OnlineFormQueueItemDTO>>.Create();
			<LoadAllStudentOnlineFormsAsync>d__.<>4__this = this;
			<LoadAllStudentOnlineFormsAsync>d__.studentPersonId = studentPersonId;
			<LoadAllStudentOnlineFormsAsync>d__.<>1__state = -1;
			<LoadAllStudentOnlineFormsAsync>d__.<>t__builder.Start<OnlineFormQueueClientManager.<LoadAllStudentOnlineFormsAsync>d__9>(ref <LoadAllStudentOnlineFormsAsync>d__);
			return <LoadAllStudentOnlineFormsAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060001E4 RID: 484 RVA: 0x000093BC File Offset: 0x000075BC
		[DebuggerStepThrough]
		public Task<IList<OnlineFormIdWithOpenItemsCountDTO>> LoadOnlineFormQueueFormsWithOpenItemsCountAsync(DateTime startDate, DateTime? endDate, int filterByAssignedCounsellorPid)
		{
			OnlineFormQueueClientManager.<LoadOnlineFormQueueFormsWithOpenItemsCountAsync>d__10 <LoadOnlineFormQueueFormsWithOpenItemsCountAsync>d__ = new OnlineFormQueueClientManager.<LoadOnlineFormQueueFormsWithOpenItemsCountAsync>d__10();
			<LoadOnlineFormQueueFormsWithOpenItemsCountAsync>d__.<>t__builder = AsyncTaskMethodBuilder<IList<OnlineFormIdWithOpenItemsCountDTO>>.Create();
			<LoadOnlineFormQueueFormsWithOpenItemsCountAsync>d__.<>4__this = this;
			<LoadOnlineFormQueueFormsWithOpenItemsCountAsync>d__.startDate = startDate;
			<LoadOnlineFormQueueFormsWithOpenItemsCountAsync>d__.endDate = endDate;
			<LoadOnlineFormQueueFormsWithOpenItemsCountAsync>d__.filterByAssignedCounsellorPid = filterByAssignedCounsellorPid;
			<LoadOnlineFormQueueFormsWithOpenItemsCountAsync>d__.<>1__state = -1;
			<LoadOnlineFormQueueFormsWithOpenItemsCountAsync>d__.<>t__builder.Start<OnlineFormQueueClientManager.<LoadOnlineFormQueueFormsWithOpenItemsCountAsync>d__10>(ref <LoadOnlineFormQueueFormsWithOpenItemsCountAsync>d__);
			return <LoadOnlineFormQueueFormsWithOpenItemsCountAsync>d__.<>t__builder.Task;
		}
	}
}
