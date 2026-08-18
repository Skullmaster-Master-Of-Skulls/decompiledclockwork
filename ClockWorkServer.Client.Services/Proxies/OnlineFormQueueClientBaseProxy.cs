using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.OnlineForms;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x020000FE RID: 254
	internal class OnlineFormQueueClientBaseProxy : ClientBase<IOnlineFormQueue>, IOnlineFormQueue, IService
	{
		// Token: 0x060009E2 RID: 2530 RVA: 0x00019464 File Offset: 0x00017664
		public OnlineFormQueueClientBaseProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x060009E3 RID: 2531 RVA: 0x0001946F File Offset: 0x0001766F
		public OnlineFormQueueClientBaseProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x060009E4 RID: 2532 RVA: 0x0001947C File Offset: 0x0001767C
		public DeleteOnlineFormQueueItemResp DeleteOnlineFormQueueItem(DeleteOnlineFormQueueItemReq request)
		{
			return base.Channel.DeleteOnlineFormQueueItem(request);
		}

		// Token: 0x060009E5 RID: 2533 RVA: 0x0001949C File Offset: 0x0001769C
		public LoadLookupOnlineFormStatusesResp LoadLookupOnlineFormStatuses(LoadLookupOnlineFormStatusesReq request)
		{
			return base.Channel.LoadLookupOnlineFormStatuses(request);
		}

		// Token: 0x060009E6 RID: 2534 RVA: 0x000194BC File Offset: 0x000176BC
		public LoadOnlineFormQueueItemResp LoadOnlineFormQueueItem(LoadOnlineFormQueueItemReq request)
		{
			return base.Channel.LoadOnlineFormQueueItem(request);
		}

		// Token: 0x060009E7 RID: 2535 RVA: 0x000194DC File Offset: 0x000176DC
		public LoadOnlineFormQueueItemFormDataItemsResp LoadOnlineFormQueueItemFormDataItems(LoadOnlineFormQueueItemFormDataItemsReq request)
		{
			return base.Channel.LoadOnlineFormQueueItemFormDataItems(request);
		}

		// Token: 0x060009E8 RID: 2536 RVA: 0x000194FC File Offset: 0x000176FC
		public LoadOnlineFormQueueItemsResp LoadOnlineFormQueueItems(LoadOnlineFormQueueItemsReq request)
		{
			return base.Channel.LoadOnlineFormQueueItems(request);
		}

		// Token: 0x060009E9 RID: 2537 RVA: 0x0001951C File Offset: 0x0001771C
		public UpdateOnlineFormQueueItemStaffNoteAndStatusResp UpdateOnlineFormQueueItemStaffNoteAndStatus(UpdateOnlineFormQueueItemStaffNoteAndStatusReq request)
		{
			return base.Channel.UpdateOnlineFormQueueItemStaffNoteAndStatus(request);
		}

		// Token: 0x060009EA RID: 2538 RVA: 0x0001953C File Offset: 0x0001773C
		public UpdateOnlineFormQueueItemStaffNoteResp UpdateOnlineFormQueueItemStaffNote(UpdateOnlineFormQueueItemStaffNoteReq request)
		{
			return base.Channel.UpdateOnlineFormQueueItemStaffNote(request);
		}

		// Token: 0x060009EB RID: 2539 RVA: 0x0001955C File Offset: 0x0001775C
		public UpdateOnlineFormQueueItemStatusResp UpdateOnlineFormQueueItemStatus(UpdateOnlineFormQueueItemStatusReq request)
		{
			return base.Channel.UpdateOnlineFormQueueItemStatus(request);
		}

		// Token: 0x060009EC RID: 2540 RVA: 0x0001957C File Offset: 0x0001777C
		public LoadAllStudentOnlineFormsResp LoadAllStudentOnlineForms(LoadAllStudentOnlineFormsReq Request)
		{
			return base.Channel.LoadAllStudentOnlineForms(Request);
		}

		// Token: 0x060009ED RID: 2541 RVA: 0x0001959C File Offset: 0x0001779C
		public LoadOnlineFormQueueFormsWithOpenItemsCountResp LoadOnlineFormQueueFormsWithOpenItemsCount(LoadOnlineFormQueueFormsWithOpenItemsCountReq request)
		{
			return base.Channel.LoadOnlineFormQueueFormsWithOpenItemsCount(request);
		}

		// Token: 0x060009EE RID: 2542 RVA: 0x000195BC File Offset: 0x000177BC
		public LoadAllowedOnlineFormsResp LoadAllowedOnlineForms(LoadAllowedOnlineFormsReq request)
		{
			return base.Channel.LoadAllowedOnlineForms(request);
		}
	}
}
