using System;
using System.Collections.Generic;
using System.Linq;
using TechnoPro.ClockWorkServer.Common.Services.Impl.Adapters;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms;
using TechnoPro.ClockWorkServer.Contracts.DTO.OnlineForms;
using TechnoPro.Common.Core.Mappers.DynamicForms;
using TechnoPro.Common.Core.Mappers.OnlineForms;
using TechnoPro.Common.Core.OnlineForms;
using TechnoPro.Common.ICore.OnlineForms;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities.DynamicForms;
using TechnoPro.Common.Public.Entities.OnlineForms;

namespace TechnoPro.ClockWorkServer.Common.Services.Impl
{
	// Token: 0x02000070 RID: 112
	public class OnlineFormQueueServiceManager : IOnlineFormQueue, IService
	{
		// Token: 0x0600042B RID: 1067 RVA: 0x00013CB0 File Offset: 0x00011EB0
		public DeleteOnlineFormQueueItemResp DeleteOnlineFormQueueItem(DeleteOnlineFormQueueItemReq request)
		{
			IOnlineFormQueueManager onlineFormQueueManager = new OnlineFormQueueManager(request.GetOperationContext());
			return new DeleteOnlineFormQueueItemResp
			{
				CompletedSuccessfully = onlineFormQueueManager.DeleteOnlineFormQueueItem(request.PeopleOnlineFormId)
			};
		}

		// Token: 0x0600042C RID: 1068 RVA: 0x00013CE8 File Offset: 0x00011EE8
		public LoadLookupOnlineFormStatusesResp LoadLookupOnlineFormStatuses(LoadLookupOnlineFormStatusesReq request)
		{
			IOnlineFormQueueManager onlineFormQueueManager = new OnlineFormQueueManager(request.GetOperationContext());
			LoadLookupOnlineFormStatusesResp loadLookupOnlineFormStatusesResp = new LoadLookupOnlineFormStatusesResp();
			IList<OnlineFormStatus> list = onlineFormQueueManager.LoadLookupOnlineFormStatuses();
			IList<OnlineFormStatusDTO> onlineFormStatuses;
			if (list == null)
			{
				onlineFormStatuses = null;
			}
			else
			{
				onlineFormStatuses = (from g in list
				select g.ToDTO()).ToList<OnlineFormStatusDTO>();
			}
			loadLookupOnlineFormStatusesResp.OnlineFormStatuses = onlineFormStatuses;
			return loadLookupOnlineFormStatusesResp;
		}

		// Token: 0x0600042D RID: 1069 RVA: 0x00013D48 File Offset: 0x00011F48
		public LoadOnlineFormQueueItemResp LoadOnlineFormQueueItem(LoadOnlineFormQueueItemReq request)
		{
			IOnlineFormQueueManager onlineFormQueueManager = new OnlineFormQueueManager(request.GetOperationContext());
			LoadOnlineFormQueueItemResp loadOnlineFormQueueItemResp = new LoadOnlineFormQueueItemResp();
			OnlineFormQueueItem onlineFormQueueItem = onlineFormQueueManager.LoadOnlineFormQueueItem(request.PeopleOnlineFormId);
			loadOnlineFormQueueItemResp.Item = ((onlineFormQueueItem != null) ? onlineFormQueueItem.ToDTO() : null);
			return loadOnlineFormQueueItemResp;
		}

		// Token: 0x0600042E RID: 1070 RVA: 0x00013D8C File Offset: 0x00011F8C
		public LoadOnlineFormQueueItemFormDataItemsResp LoadOnlineFormQueueItemFormDataItems(LoadOnlineFormQueueItemFormDataItemsReq request)
		{
			IOnlineFormQueueManager onlineFormQueueManager = new OnlineFormQueueManager(request.GetOperationContext());
			LoadOnlineFormQueueItemFormDataItemsResp loadOnlineFormQueueItemFormDataItemsResp = new LoadOnlineFormQueueItemFormDataItemsResp();
			IList<DynamicData> list = onlineFormQueueManager.LoadOnlineFormQueueItemFormDataItems(request.PeopleOnlineFormId);
			IList<DynamicDataDTO> dataItems;
			if (list == null)
			{
				dataItems = null;
			}
			else
			{
				dataItems = (from g in list
				select g.ToDTO()).ToList<DynamicDataDTO>();
			}
			loadOnlineFormQueueItemFormDataItemsResp.DataItems = dataItems;
			return loadOnlineFormQueueItemFormDataItemsResp;
		}

		// Token: 0x0600042F RID: 1071 RVA: 0x00013DF4 File Offset: 0x00011FF4
		public LoadOnlineFormQueueItemsResp LoadOnlineFormQueueItems(LoadOnlineFormQueueItemsReq request)
		{
			IOnlineFormQueueManager onlineFormQueueManager = new OnlineFormQueueManager(request.GetOperationContext());
			LoadOnlineFormQueueItemsResp loadOnlineFormQueueItemsResp = new LoadOnlineFormQueueItemsResp();
			IList<OnlineFormQueueItem> list = onlineFormQueueManager.LoadOnlineFormQueueItems(request.OnlineFormId, request.StartDate, request.EndDate, request.FilterByAssignedCounsellorPid, request.OnlineFormTypesToExclude);
			IList<OnlineFormQueueItemDTO> items;
			if (list == null)
			{
				items = null;
			}
			else
			{
				items = (from g in list
				select g.ToDTO()).ToList<OnlineFormQueueItemDTO>();
			}
			loadOnlineFormQueueItemsResp.Items = items;
			return loadOnlineFormQueueItemsResp;
		}

		// Token: 0x06000430 RID: 1072 RVA: 0x00013E74 File Offset: 0x00012074
		public UpdateOnlineFormQueueItemStaffNoteAndStatusResp UpdateOnlineFormQueueItemStaffNoteAndStatus(UpdateOnlineFormQueueItemStaffNoteAndStatusReq request)
		{
			IOnlineFormQueueManager onlineFormQueueManager = new OnlineFormQueueManager(request.GetOperationContext());
			UpdateOnlineFormQueueItemStaffNoteAndStatusResp updateOnlineFormQueueItemStaffNoteAndStatusResp = new UpdateOnlineFormQueueItemStaffNoteAndStatusResp();
			OnlineFormQueueItem onlineFormQueueItem = onlineFormQueueManager.UpdateOnlineFormQueueItemStaffNoteAndStatus(request.PeopleOnlineFormId, request.NewPeopleOnlineFormStatusId, request.NewStaffNote);
			updateOnlineFormQueueItemStaffNoteAndStatusResp.RefreshedItem = ((onlineFormQueueItem != null) ? onlineFormQueueItem.ToDTO() : null);
			return updateOnlineFormQueueItemStaffNoteAndStatusResp;
		}

		// Token: 0x06000431 RID: 1073 RVA: 0x00013EC4 File Offset: 0x000120C4
		public UpdateOnlineFormQueueItemStaffNoteResp UpdateOnlineFormQueueItemStaffNote(UpdateOnlineFormQueueItemStaffNoteReq request)
		{
			IOnlineFormQueueManager onlineFormQueueManager = new OnlineFormQueueManager(request.GetOperationContext());
			UpdateOnlineFormQueueItemStaffNoteResp updateOnlineFormQueueItemStaffNoteResp = new UpdateOnlineFormQueueItemStaffNoteResp();
			OnlineFormQueueItem onlineFormQueueItem = onlineFormQueueManager.UpdateOnlineFormQueueItemStaffNote(request.PeopleOnlineFormId, request.NewStaffNote);
			updateOnlineFormQueueItemStaffNoteResp.RefreshedItem = ((onlineFormQueueItem != null) ? onlineFormQueueItem.ToDTO() : null);
			return updateOnlineFormQueueItemStaffNoteResp;
		}

		// Token: 0x06000432 RID: 1074 RVA: 0x00013F0C File Offset: 0x0001210C
		public UpdateOnlineFormQueueItemStatusResp UpdateOnlineFormQueueItemStatus(UpdateOnlineFormQueueItemStatusReq request)
		{
			IOnlineFormQueueManager onlineFormQueueManager = new OnlineFormQueueManager(request.GetOperationContext());
			UpdateOnlineFormQueueItemStatusResp updateOnlineFormQueueItemStatusResp = new UpdateOnlineFormQueueItemStatusResp();
			OnlineFormQueueItem onlineFormQueueItem = onlineFormQueueManager.UpdateOnlineFormQueueItemStatus(request.PeopleOnlineFormId, request.NewPeopleOnlineFormStatusId);
			updateOnlineFormQueueItemStatusResp.RefreshedItem = ((onlineFormQueueItem != null) ? onlineFormQueueItem.ToDTO() : null);
			return updateOnlineFormQueueItemStatusResp;
		}

		// Token: 0x06000433 RID: 1075 RVA: 0x00013F54 File Offset: 0x00012154
		public LoadAllowedOnlineFormsResp LoadAllowedOnlineForms(LoadAllowedOnlineFormsReq request)
		{
			IOnlineFormQueueManager onlineFormQueueManager = new OnlineFormQueueManager(request.GetOperationContext());
			LoadAllowedOnlineFormsResp loadAllowedOnlineFormsResp = new LoadAllowedOnlineFormsResp();
			IList<OnlineForm> list = onlineFormQueueManager.LoadAllowedOnlineForms();
			IList<OnlineFormDTO> allowedOnlineForms;
			if (list == null)
			{
				allowedOnlineForms = null;
			}
			else
			{
				allowedOnlineForms = (from g in list
				select g.ToDTO()).ToList<OnlineFormDTO>();
			}
			loadAllowedOnlineFormsResp.AllowedOnlineForms = allowedOnlineForms;
			return loadAllowedOnlineFormsResp;
		}

		// Token: 0x06000434 RID: 1076 RVA: 0x00013FB4 File Offset: 0x000121B4
		public LoadAllStudentOnlineFormsResp LoadAllStudentOnlineForms(LoadAllStudentOnlineFormsReq Request)
		{
			IOnlineFormQueueManager onlineFormQueueManager = new OnlineFormQueueManager(Request.GetOperationContext());
			LoadAllStudentOnlineFormsResp loadAllStudentOnlineFormsResp = new LoadAllStudentOnlineFormsResp();
			IList<OnlineFormQueueItem> list = onlineFormQueueManager.LoadAllStudentOnlineForms(Request.StudentPersonId);
			IList<OnlineFormQueueItemDTO> items;
			if (list == null)
			{
				items = null;
			}
			else
			{
				items = (from g in list
				select g.ToDTO()).ToList<OnlineFormQueueItemDTO>();
			}
			loadAllStudentOnlineFormsResp.Items = items;
			return loadAllStudentOnlineFormsResp;
		}

		// Token: 0x06000435 RID: 1077 RVA: 0x0001401C File Offset: 0x0001221C
		public LoadOnlineFormQueueFormsWithOpenItemsCountResp LoadOnlineFormQueueFormsWithOpenItemsCount(LoadOnlineFormQueueFormsWithOpenItemsCountReq request)
		{
			IOnlineFormQueueManager onlineFormQueueManager = new OnlineFormQueueManager(request.GetOperationContext());
			LoadOnlineFormQueueFormsWithOpenItemsCountResp loadOnlineFormQueueFormsWithOpenItemsCountResp = new LoadOnlineFormQueueFormsWithOpenItemsCountResp();
			IList<OnlineFormIdWithOpenItemsCount> list = onlineFormQueueManager.LoadOnlineFormQueueFormsWithOpenItemsCount(request.StartDate, request.EndDate, request.FilterByAssignedCounsellorPid);
			IList<OnlineFormIdWithOpenItemsCountDTO> items;
			if (list == null)
			{
				items = null;
			}
			else
			{
				items = (from g in list
				select g.ToDTO()).ToList<OnlineFormIdWithOpenItemsCountDTO>();
			}
			loadOnlineFormQueueFormsWithOpenItemsCountResp.Items = items;
			return loadOnlineFormQueueFormsWithOpenItemsCountResp;
		}
	}
}
