using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.OnlineForms;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x020000FD RID: 253
	public class OnlineFormQueueReusableClientProxy : WCFTokenBasedReusableClientProxy<IOnlineFormQueue>, IOnlineFormQueue, IService
	{
		// Token: 0x060009D5 RID: 2517 RVA: 0x000191E2 File Offset: 0x000173E2
		public OnlineFormQueueReusableClientProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x060009D6 RID: 2518 RVA: 0x000191ED File Offset: 0x000173ED
		public OnlineFormQueueReusableClientProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x060009D7 RID: 2519 RVA: 0x000191FC File Offset: 0x000173FC
		public LoadOnlineFormQueueItemResp LoadOnlineFormQueueItem(LoadOnlineFormQueueItemReq request)
		{
			return this.WrapServiceMethod<LoadOnlineFormQueueItemResp>(() => this.Proxy.LoadOnlineFormQueueItem(request));
		}

		// Token: 0x060009D8 RID: 2520 RVA: 0x00019234 File Offset: 0x00017434
		public LoadOnlineFormQueueItemsResp LoadOnlineFormQueueItems(LoadOnlineFormQueueItemsReq request)
		{
			return this.WrapServiceMethod<LoadOnlineFormQueueItemsResp>(() => this.Proxy.LoadOnlineFormQueueItems(request));
		}

		// Token: 0x060009D9 RID: 2521 RVA: 0x0001926C File Offset: 0x0001746C
		public DeleteOnlineFormQueueItemResp DeleteOnlineFormQueueItem(DeleteOnlineFormQueueItemReq request)
		{
			return this.WrapServiceMethod<DeleteOnlineFormQueueItemResp>(() => this.Proxy.DeleteOnlineFormQueueItem(request));
		}

		// Token: 0x060009DA RID: 2522 RVA: 0x000192A4 File Offset: 0x000174A4
		public LoadLookupOnlineFormStatusesResp LoadLookupOnlineFormStatuses(LoadLookupOnlineFormStatusesReq request)
		{
			return this.WrapServiceMethod<LoadLookupOnlineFormStatusesResp>(() => this.Proxy.LoadLookupOnlineFormStatuses(request));
		}

		// Token: 0x060009DB RID: 2523 RVA: 0x000192DC File Offset: 0x000174DC
		public LoadOnlineFormQueueItemFormDataItemsResp LoadOnlineFormQueueItemFormDataItems(LoadOnlineFormQueueItemFormDataItemsReq request)
		{
			return this.WrapServiceMethod<LoadOnlineFormQueueItemFormDataItemsResp>(() => this.Proxy.LoadOnlineFormQueueItemFormDataItems(request));
		}

		// Token: 0x060009DC RID: 2524 RVA: 0x00019314 File Offset: 0x00017514
		public LoadAllowedOnlineFormsResp LoadAllowedOnlineForms(LoadAllowedOnlineFormsReq request)
		{
			return this.WrapServiceMethod<LoadAllowedOnlineFormsResp>(() => this.Proxy.LoadAllowedOnlineForms(request));
		}

		// Token: 0x060009DD RID: 2525 RVA: 0x0001934C File Offset: 0x0001754C
		public UpdateOnlineFormQueueItemStaffNoteAndStatusResp UpdateOnlineFormQueueItemStaffNoteAndStatus(UpdateOnlineFormQueueItemStaffNoteAndStatusReq request)
		{
			return this.WrapServiceMethod<UpdateOnlineFormQueueItemStaffNoteAndStatusResp>(() => this.Proxy.UpdateOnlineFormQueueItemStaffNoteAndStatus(request));
		}

		// Token: 0x060009DE RID: 2526 RVA: 0x00019384 File Offset: 0x00017584
		public UpdateOnlineFormQueueItemStaffNoteResp UpdateOnlineFormQueueItemStaffNote(UpdateOnlineFormQueueItemStaffNoteReq request)
		{
			return this.WrapServiceMethod<UpdateOnlineFormQueueItemStaffNoteResp>(() => this.Proxy.UpdateOnlineFormQueueItemStaffNote(request));
		}

		// Token: 0x060009DF RID: 2527 RVA: 0x000193BC File Offset: 0x000175BC
		public UpdateOnlineFormQueueItemStatusResp UpdateOnlineFormQueueItemStatus(UpdateOnlineFormQueueItemStatusReq request)
		{
			return this.WrapServiceMethod<UpdateOnlineFormQueueItemStatusResp>(() => this.Proxy.UpdateOnlineFormQueueItemStatus(request));
		}

		// Token: 0x060009E0 RID: 2528 RVA: 0x000193F4 File Offset: 0x000175F4
		public LoadAllStudentOnlineFormsResp LoadAllStudentOnlineForms(LoadAllStudentOnlineFormsReq Request)
		{
			return this.WrapServiceMethod<LoadAllStudentOnlineFormsResp>(() => this.Proxy.LoadAllStudentOnlineForms(Request));
		}

		// Token: 0x060009E1 RID: 2529 RVA: 0x0001942C File Offset: 0x0001762C
		public LoadOnlineFormQueueFormsWithOpenItemsCountResp LoadOnlineFormQueueFormsWithOpenItemsCount(LoadOnlineFormQueueFormsWithOpenItemsCountReq request)
		{
			return this.WrapServiceMethod<LoadOnlineFormQueueFormsWithOpenItemsCountResp>(() => this.Proxy.LoadOnlineFormQueueFormsWithOpenItemsCount(request));
		}
	}
}
