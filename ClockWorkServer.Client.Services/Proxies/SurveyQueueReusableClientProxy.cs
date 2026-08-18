using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Threading.Tasks;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.Surveys;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x02000149 RID: 329
	public class SurveyQueueReusableClientProxy : WCFTokenBasedReusableClientProxy<ISurveyQueue>, ISurveyQueue, IService
	{
		// Token: 0x06000C92 RID: 3218 RVA: 0x0001F4CE File Offset: 0x0001D6CE
		public SurveyQueueReusableClientProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x06000C93 RID: 3219 RVA: 0x0001F4D9 File Offset: 0x0001D6D9
		public SurveyQueueReusableClientProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x06000C94 RID: 3220 RVA: 0x0001F4E8 File Offset: 0x0001D6E8
		public Task<LoadSurveyQueueItemResp> LoadSurveyQueueItemAsync(LoadSurveyQueueItemReq request)
		{
			return this.WrapServiceMethod<Task<LoadSurveyQueueItemResp>>(() => this.Proxy.LoadSurveyQueueItemAsync(request));
		}

		// Token: 0x06000C95 RID: 3221 RVA: 0x0001F520 File Offset: 0x0001D720
		public Task<LoadSurveyQueueItemsResp> LoadSurveyQueueItemsAsync(LoadSurveyQueueItemsReq request)
		{
			return this.WrapServiceMethod<Task<LoadSurveyQueueItemsResp>>(() => this.Proxy.LoadSurveyQueueItemsAsync(request));
		}

		// Token: 0x06000C96 RID: 3222 RVA: 0x0001F558 File Offset: 0x0001D758
		public Task<DeleteSurveyQueueItemResp> DeleteSurveyQueueItemAsync(DeleteSurveyQueueItemReq request)
		{
			return this.WrapServiceMethod<Task<DeleteSurveyQueueItemResp>>(() => this.Proxy.DeleteSurveyQueueItemAsync(request));
		}

		// Token: 0x06000C97 RID: 3223 RVA: 0x0001F590 File Offset: 0x0001D790
		public Task<LoadLookupSurveyStatusesResp> LoadLookupSurveyStatusesAsync(LoadLookupSurveyStatusesReq request)
		{
			return this.WrapServiceMethod<Task<LoadLookupSurveyStatusesResp>>(() => this.Proxy.LoadLookupSurveyStatusesAsync(request));
		}

		// Token: 0x06000C98 RID: 3224 RVA: 0x0001F5C8 File Offset: 0x0001D7C8
		public Task<LoadSurveyQueueItemFormDataItemsResp> LoadSurveyQueueItemFormDataItemsAsync(LoadSurveyQueueItemFormDataItemsReq request)
		{
			return this.WrapServiceMethod<Task<LoadSurveyQueueItemFormDataItemsResp>>(() => this.Proxy.LoadSurveyQueueItemFormDataItemsAsync(request));
		}

		// Token: 0x06000C99 RID: 3225 RVA: 0x0001F600 File Offset: 0x0001D800
		public Task<LoadAllowedSurveysResp> LoadAllowedSurveysAsync(LoadAllowedSurveysReq request)
		{
			return this.WrapServiceMethod<Task<LoadAllowedSurveysResp>>(() => this.Proxy.LoadAllowedSurveysAsync(request));
		}

		// Token: 0x06000C9A RID: 3226 RVA: 0x0001F638 File Offset: 0x0001D838
		public Task<UpdateSurveyQueueItemStaffNoteAndStatusResp> UpdateSurveyQueueItemStaffNoteAndStatusAsync(UpdateSurveyQueueItemStaffNoteAndStatusReq request)
		{
			return this.WrapServiceMethod<Task<UpdateSurveyQueueItemStaffNoteAndStatusResp>>(() => this.Proxy.UpdateSurveyQueueItemStaffNoteAndStatusAsync(request));
		}

		// Token: 0x06000C9B RID: 3227 RVA: 0x0001F670 File Offset: 0x0001D870
		public Task<UpdateSurveyQueueItemStaffNoteResp> UpdateSurveyQueueItemStaffNoteAsync(UpdateSurveyQueueItemStaffNoteReq request)
		{
			return this.WrapServiceMethod<Task<UpdateSurveyQueueItemStaffNoteResp>>(() => this.Proxy.UpdateSurveyQueueItemStaffNoteAsync(request));
		}

		// Token: 0x06000C9C RID: 3228 RVA: 0x0001F6A8 File Offset: 0x0001D8A8
		public Task<UpdateSurveyQueueItemStatusResp> UpdateSurveyQueueItemStatusAsync(UpdateSurveyQueueItemStatusReq request)
		{
			return this.WrapServiceMethod<Task<UpdateSurveyQueueItemStatusResp>>(() => this.Proxy.UpdateSurveyQueueItemStatusAsync(request));
		}
	}
}
