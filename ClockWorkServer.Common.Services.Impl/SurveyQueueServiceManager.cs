using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.Surveys;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Common.Services.Impl
{
	// Token: 0x02000091 RID: 145
	public class SurveyQueueServiceManager : ISurveyQueue, IService
	{
		// Token: 0x06000528 RID: 1320 RVA: 0x000181AC File Offset: 0x000163AC
		[DebuggerStepThrough]
		public Task<DeleteSurveyQueueItemResp> DeleteSurveyQueueItemAsync(DeleteSurveyQueueItemReq request)
		{
			SurveyQueueServiceManager.<DeleteSurveyQueueItemAsync>d__0 <DeleteSurveyQueueItemAsync>d__ = new SurveyQueueServiceManager.<DeleteSurveyQueueItemAsync>d__0();
			<DeleteSurveyQueueItemAsync>d__.<>t__builder = AsyncTaskMethodBuilder<DeleteSurveyQueueItemResp>.Create();
			<DeleteSurveyQueueItemAsync>d__.<>4__this = this;
			<DeleteSurveyQueueItemAsync>d__.request = request;
			<DeleteSurveyQueueItemAsync>d__.<>1__state = -1;
			<DeleteSurveyQueueItemAsync>d__.<>t__builder.Start<SurveyQueueServiceManager.<DeleteSurveyQueueItemAsync>d__0>(ref <DeleteSurveyQueueItemAsync>d__);
			return <DeleteSurveyQueueItemAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000529 RID: 1321 RVA: 0x000181F8 File Offset: 0x000163F8
		[DebuggerStepThrough]
		public Task<LoadLookupSurveyStatusesResp> LoadLookupSurveyStatusesAsync(LoadLookupSurveyStatusesReq request)
		{
			SurveyQueueServiceManager.<LoadLookupSurveyStatusesAsync>d__1 <LoadLookupSurveyStatusesAsync>d__ = new SurveyQueueServiceManager.<LoadLookupSurveyStatusesAsync>d__1();
			<LoadLookupSurveyStatusesAsync>d__.<>t__builder = AsyncTaskMethodBuilder<LoadLookupSurveyStatusesResp>.Create();
			<LoadLookupSurveyStatusesAsync>d__.<>4__this = this;
			<LoadLookupSurveyStatusesAsync>d__.request = request;
			<LoadLookupSurveyStatusesAsync>d__.<>1__state = -1;
			<LoadLookupSurveyStatusesAsync>d__.<>t__builder.Start<SurveyQueueServiceManager.<LoadLookupSurveyStatusesAsync>d__1>(ref <LoadLookupSurveyStatusesAsync>d__);
			return <LoadLookupSurveyStatusesAsync>d__.<>t__builder.Task;
		}

		// Token: 0x0600052A RID: 1322 RVA: 0x00018244 File Offset: 0x00016444
		[DebuggerStepThrough]
		public Task<LoadSurveyQueueItemResp> LoadSurveyQueueItemAsync(LoadSurveyQueueItemReq request)
		{
			SurveyQueueServiceManager.<LoadSurveyQueueItemAsync>d__2 <LoadSurveyQueueItemAsync>d__ = new SurveyQueueServiceManager.<LoadSurveyQueueItemAsync>d__2();
			<LoadSurveyQueueItemAsync>d__.<>t__builder = AsyncTaskMethodBuilder<LoadSurveyQueueItemResp>.Create();
			<LoadSurveyQueueItemAsync>d__.<>4__this = this;
			<LoadSurveyQueueItemAsync>d__.request = request;
			<LoadSurveyQueueItemAsync>d__.<>1__state = -1;
			<LoadSurveyQueueItemAsync>d__.<>t__builder.Start<SurveyQueueServiceManager.<LoadSurveyQueueItemAsync>d__2>(ref <LoadSurveyQueueItemAsync>d__);
			return <LoadSurveyQueueItemAsync>d__.<>t__builder.Task;
		}

		// Token: 0x0600052B RID: 1323 RVA: 0x00018290 File Offset: 0x00016490
		[DebuggerStepThrough]
		public Task<LoadSurveyQueueItemFormDataItemsResp> LoadSurveyQueueItemFormDataItemsAsync(LoadSurveyQueueItemFormDataItemsReq request)
		{
			SurveyQueueServiceManager.<LoadSurveyQueueItemFormDataItemsAsync>d__3 <LoadSurveyQueueItemFormDataItemsAsync>d__ = new SurveyQueueServiceManager.<LoadSurveyQueueItemFormDataItemsAsync>d__3();
			<LoadSurveyQueueItemFormDataItemsAsync>d__.<>t__builder = AsyncTaskMethodBuilder<LoadSurveyQueueItemFormDataItemsResp>.Create();
			<LoadSurveyQueueItemFormDataItemsAsync>d__.<>4__this = this;
			<LoadSurveyQueueItemFormDataItemsAsync>d__.request = request;
			<LoadSurveyQueueItemFormDataItemsAsync>d__.<>1__state = -1;
			<LoadSurveyQueueItemFormDataItemsAsync>d__.<>t__builder.Start<SurveyQueueServiceManager.<LoadSurveyQueueItemFormDataItemsAsync>d__3>(ref <LoadSurveyQueueItemFormDataItemsAsync>d__);
			return <LoadSurveyQueueItemFormDataItemsAsync>d__.<>t__builder.Task;
		}

		// Token: 0x0600052C RID: 1324 RVA: 0x000182DC File Offset: 0x000164DC
		[DebuggerStepThrough]
		public Task<LoadSurveyQueueItemsResp> LoadSurveyQueueItemsAsync(LoadSurveyQueueItemsReq request)
		{
			SurveyQueueServiceManager.<LoadSurveyQueueItemsAsync>d__4 <LoadSurveyQueueItemsAsync>d__ = new SurveyQueueServiceManager.<LoadSurveyQueueItemsAsync>d__4();
			<LoadSurveyQueueItemsAsync>d__.<>t__builder = AsyncTaskMethodBuilder<LoadSurveyQueueItemsResp>.Create();
			<LoadSurveyQueueItemsAsync>d__.<>4__this = this;
			<LoadSurveyQueueItemsAsync>d__.request = request;
			<LoadSurveyQueueItemsAsync>d__.<>1__state = -1;
			<LoadSurveyQueueItemsAsync>d__.<>t__builder.Start<SurveyQueueServiceManager.<LoadSurveyQueueItemsAsync>d__4>(ref <LoadSurveyQueueItemsAsync>d__);
			return <LoadSurveyQueueItemsAsync>d__.<>t__builder.Task;
		}

		// Token: 0x0600052D RID: 1325 RVA: 0x00018328 File Offset: 0x00016528
		[DebuggerStepThrough]
		public Task<UpdateSurveyQueueItemStaffNoteAndStatusResp> UpdateSurveyQueueItemStaffNoteAndStatusAsync(UpdateSurveyQueueItemStaffNoteAndStatusReq request)
		{
			SurveyQueueServiceManager.<UpdateSurveyQueueItemStaffNoteAndStatusAsync>d__5 <UpdateSurveyQueueItemStaffNoteAndStatusAsync>d__ = new SurveyQueueServiceManager.<UpdateSurveyQueueItemStaffNoteAndStatusAsync>d__5();
			<UpdateSurveyQueueItemStaffNoteAndStatusAsync>d__.<>t__builder = AsyncTaskMethodBuilder<UpdateSurveyQueueItemStaffNoteAndStatusResp>.Create();
			<UpdateSurveyQueueItemStaffNoteAndStatusAsync>d__.<>4__this = this;
			<UpdateSurveyQueueItemStaffNoteAndStatusAsync>d__.request = request;
			<UpdateSurveyQueueItemStaffNoteAndStatusAsync>d__.<>1__state = -1;
			<UpdateSurveyQueueItemStaffNoteAndStatusAsync>d__.<>t__builder.Start<SurveyQueueServiceManager.<UpdateSurveyQueueItemStaffNoteAndStatusAsync>d__5>(ref <UpdateSurveyQueueItemStaffNoteAndStatusAsync>d__);
			return <UpdateSurveyQueueItemStaffNoteAndStatusAsync>d__.<>t__builder.Task;
		}

		// Token: 0x0600052E RID: 1326 RVA: 0x00018374 File Offset: 0x00016574
		[DebuggerStepThrough]
		public Task<UpdateSurveyQueueItemStaffNoteResp> UpdateSurveyQueueItemStaffNoteAsync(UpdateSurveyQueueItemStaffNoteReq request)
		{
			SurveyQueueServiceManager.<UpdateSurveyQueueItemStaffNoteAsync>d__6 <UpdateSurveyQueueItemStaffNoteAsync>d__ = new SurveyQueueServiceManager.<UpdateSurveyQueueItemStaffNoteAsync>d__6();
			<UpdateSurveyQueueItemStaffNoteAsync>d__.<>t__builder = AsyncTaskMethodBuilder<UpdateSurveyQueueItemStaffNoteResp>.Create();
			<UpdateSurveyQueueItemStaffNoteAsync>d__.<>4__this = this;
			<UpdateSurveyQueueItemStaffNoteAsync>d__.request = request;
			<UpdateSurveyQueueItemStaffNoteAsync>d__.<>1__state = -1;
			<UpdateSurveyQueueItemStaffNoteAsync>d__.<>t__builder.Start<SurveyQueueServiceManager.<UpdateSurveyQueueItemStaffNoteAsync>d__6>(ref <UpdateSurveyQueueItemStaffNoteAsync>d__);
			return <UpdateSurveyQueueItemStaffNoteAsync>d__.<>t__builder.Task;
		}

		// Token: 0x0600052F RID: 1327 RVA: 0x000183C0 File Offset: 0x000165C0
		[DebuggerStepThrough]
		public Task<UpdateSurveyQueueItemStatusResp> UpdateSurveyQueueItemStatusAsync(UpdateSurveyQueueItemStatusReq request)
		{
			SurveyQueueServiceManager.<UpdateSurveyQueueItemStatusAsync>d__7 <UpdateSurveyQueueItemStatusAsync>d__ = new SurveyQueueServiceManager.<UpdateSurveyQueueItemStatusAsync>d__7();
			<UpdateSurveyQueueItemStatusAsync>d__.<>t__builder = AsyncTaskMethodBuilder<UpdateSurveyQueueItemStatusResp>.Create();
			<UpdateSurveyQueueItemStatusAsync>d__.<>4__this = this;
			<UpdateSurveyQueueItemStatusAsync>d__.request = request;
			<UpdateSurveyQueueItemStatusAsync>d__.<>1__state = -1;
			<UpdateSurveyQueueItemStatusAsync>d__.<>t__builder.Start<SurveyQueueServiceManager.<UpdateSurveyQueueItemStatusAsync>d__7>(ref <UpdateSurveyQueueItemStatusAsync>d__);
			return <UpdateSurveyQueueItemStatusAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000530 RID: 1328 RVA: 0x0001840C File Offset: 0x0001660C
		[DebuggerStepThrough]
		public Task<LoadAllowedSurveysResp> LoadAllowedSurveysAsync(LoadAllowedSurveysReq request)
		{
			SurveyQueueServiceManager.<LoadAllowedSurveysAsync>d__8 <LoadAllowedSurveysAsync>d__ = new SurveyQueueServiceManager.<LoadAllowedSurveysAsync>d__8();
			<LoadAllowedSurveysAsync>d__.<>t__builder = AsyncTaskMethodBuilder<LoadAllowedSurveysResp>.Create();
			<LoadAllowedSurveysAsync>d__.<>4__this = this;
			<LoadAllowedSurveysAsync>d__.request = request;
			<LoadAllowedSurveysAsync>d__.<>1__state = -1;
			<LoadAllowedSurveysAsync>d__.<>t__builder.Start<SurveyQueueServiceManager.<LoadAllowedSurveysAsync>d__8>(ref <LoadAllowedSurveysAsync>d__);
			return <LoadAllowedSurveysAsync>d__.<>t__builder.Task;
		}
	}
}
