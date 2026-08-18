using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Threading.Tasks;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.Surveys;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x0200014A RID: 330
	internal class SurveyQueueClientBaseProxy : ClientBase<ISurveyQueue>, ISurveyQueue, IService
	{
		// Token: 0x06000C9D RID: 3229 RVA: 0x0001F6E0 File Offset: 0x0001D8E0
		public SurveyQueueClientBaseProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x06000C9E RID: 3230 RVA: 0x0001F6EB File Offset: 0x0001D8EB
		public SurveyQueueClientBaseProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x06000C9F RID: 3231 RVA: 0x0001F6F8 File Offset: 0x0001D8F8
		public Task<DeleteSurveyQueueItemResp> DeleteSurveyQueueItemAsync(DeleteSurveyQueueItemReq request)
		{
			return base.Channel.DeleteSurveyQueueItemAsync(request);
		}

		// Token: 0x06000CA0 RID: 3232 RVA: 0x0001F718 File Offset: 0x0001D918
		public Task<LoadLookupSurveyStatusesResp> LoadLookupSurveyStatusesAsync(LoadLookupSurveyStatusesReq request)
		{
			return base.Channel.LoadLookupSurveyStatusesAsync(request);
		}

		// Token: 0x06000CA1 RID: 3233 RVA: 0x0001F738 File Offset: 0x0001D938
		public Task<LoadSurveyQueueItemResp> LoadSurveyQueueItemAsync(LoadSurveyQueueItemReq request)
		{
			return base.Channel.LoadSurveyQueueItemAsync(request);
		}

		// Token: 0x06000CA2 RID: 3234 RVA: 0x0001F758 File Offset: 0x0001D958
		public Task<LoadSurveyQueueItemFormDataItemsResp> LoadSurveyQueueItemFormDataItemsAsync(LoadSurveyQueueItemFormDataItemsReq request)
		{
			return base.Channel.LoadSurveyQueueItemFormDataItemsAsync(request);
		}

		// Token: 0x06000CA3 RID: 3235 RVA: 0x0001F778 File Offset: 0x0001D978
		public Task<LoadSurveyQueueItemsResp> LoadSurveyQueueItemsAsync(LoadSurveyQueueItemsReq request)
		{
			return base.Channel.LoadSurveyQueueItemsAsync(request);
		}

		// Token: 0x06000CA4 RID: 3236 RVA: 0x0001F798 File Offset: 0x0001D998
		public Task<UpdateSurveyQueueItemStaffNoteAndStatusResp> UpdateSurveyQueueItemStaffNoteAndStatusAsync(UpdateSurveyQueueItemStaffNoteAndStatusReq request)
		{
			return base.Channel.UpdateSurveyQueueItemStaffNoteAndStatusAsync(request);
		}

		// Token: 0x06000CA5 RID: 3237 RVA: 0x0001F7B8 File Offset: 0x0001D9B8
		public Task<UpdateSurveyQueueItemStaffNoteResp> UpdateSurveyQueueItemStaffNoteAsync(UpdateSurveyQueueItemStaffNoteReq request)
		{
			return base.Channel.UpdateSurveyQueueItemStaffNoteAsync(request);
		}

		// Token: 0x06000CA6 RID: 3238 RVA: 0x0001F7D8 File Offset: 0x0001D9D8
		public Task<UpdateSurveyQueueItemStatusResp> UpdateSurveyQueueItemStatusAsync(UpdateSurveyQueueItemStatusReq request)
		{
			return base.Channel.UpdateSurveyQueueItemStatusAsync(request);
		}

		// Token: 0x06000CA7 RID: 3239 RVA: 0x0001F7F8 File Offset: 0x0001D9F8
		public Task<LoadAllowedSurveysResp> LoadAllowedSurveysAsync(LoadAllowedSurveysReq request)
		{
			return base.Channel.LoadAllowedSurveysAsync(request);
		}
	}
}
