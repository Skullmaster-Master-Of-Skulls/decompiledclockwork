using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms;
using TechnoPro.ClockWorkServer.Contracts.DTO.Surveys;
using TechnoPro.Common.ClientManager.ICore.Surveys;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities.Surveys;

namespace TechnoPro.Common.ClientManager.Core.Surveys
{
	// Token: 0x02000014 RID: 20
	public class SurveyQueueClientManager : ISurveyQueueClientManager, IWebService
	{
		// Token: 0x060000A0 RID: 160 RVA: 0x00004744 File Offset: 0x00002944
		[DebuggerStepThrough]
		public Task<bool> DeleteSurveyQueueItemAsync(int peopleSurveyId)
		{
			SurveyQueueClientManager.<DeleteSurveyQueueItemAsync>d__0 <DeleteSurveyQueueItemAsync>d__ = new SurveyQueueClientManager.<DeleteSurveyQueueItemAsync>d__0();
			<DeleteSurveyQueueItemAsync>d__.<>t__builder = AsyncTaskMethodBuilder<bool>.Create();
			<DeleteSurveyQueueItemAsync>d__.<>4__this = this;
			<DeleteSurveyQueueItemAsync>d__.peopleSurveyId = peopleSurveyId;
			<DeleteSurveyQueueItemAsync>d__.<>1__state = -1;
			<DeleteSurveyQueueItemAsync>d__.<>t__builder.Start<SurveyQueueClientManager.<DeleteSurveyQueueItemAsync>d__0>(ref <DeleteSurveyQueueItemAsync>d__);
			return <DeleteSurveyQueueItemAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x00004790 File Offset: 0x00002990
		[DebuggerStepThrough]
		public Task<IList<SurveyStatusDTO>> LoadLookupSurveyStatusesAsync()
		{
			SurveyQueueClientManager.<LoadLookupSurveyStatusesAsync>d__1 <LoadLookupSurveyStatusesAsync>d__ = new SurveyQueueClientManager.<LoadLookupSurveyStatusesAsync>d__1();
			<LoadLookupSurveyStatusesAsync>d__.<>t__builder = AsyncTaskMethodBuilder<IList<SurveyStatusDTO>>.Create();
			<LoadLookupSurveyStatusesAsync>d__.<>4__this = this;
			<LoadLookupSurveyStatusesAsync>d__.<>1__state = -1;
			<LoadLookupSurveyStatusesAsync>d__.<>t__builder.Start<SurveyQueueClientManager.<LoadLookupSurveyStatusesAsync>d__1>(ref <LoadLookupSurveyStatusesAsync>d__);
			return <LoadLookupSurveyStatusesAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x000047D4 File Offset: 0x000029D4
		[DebuggerStepThrough]
		public Task<SurveyQueueItemDTO> LoadSurveyQueueItemAsync(int peopleSurveyId)
		{
			SurveyQueueClientManager.<LoadSurveyQueueItemAsync>d__2 <LoadSurveyQueueItemAsync>d__ = new SurveyQueueClientManager.<LoadSurveyQueueItemAsync>d__2();
			<LoadSurveyQueueItemAsync>d__.<>t__builder = AsyncTaskMethodBuilder<SurveyQueueItemDTO>.Create();
			<LoadSurveyQueueItemAsync>d__.<>4__this = this;
			<LoadSurveyQueueItemAsync>d__.peopleSurveyId = peopleSurveyId;
			<LoadSurveyQueueItemAsync>d__.<>1__state = -1;
			<LoadSurveyQueueItemAsync>d__.<>t__builder.Start<SurveyQueueClientManager.<LoadSurveyQueueItemAsync>d__2>(ref <LoadSurveyQueueItemAsync>d__);
			return <LoadSurveyQueueItemAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x00004820 File Offset: 0x00002A20
		[DebuggerStepThrough]
		public Task<IList<DynamicDataDTO>> LoadSurveyQueueItemFormDataItemsAsync(int peopleSurveyId)
		{
			SurveyQueueClientManager.<LoadSurveyQueueItemFormDataItemsAsync>d__3 <LoadSurveyQueueItemFormDataItemsAsync>d__ = new SurveyQueueClientManager.<LoadSurveyQueueItemFormDataItemsAsync>d__3();
			<LoadSurveyQueueItemFormDataItemsAsync>d__.<>t__builder = AsyncTaskMethodBuilder<IList<DynamicDataDTO>>.Create();
			<LoadSurveyQueueItemFormDataItemsAsync>d__.<>4__this = this;
			<LoadSurveyQueueItemFormDataItemsAsync>d__.peopleSurveyId = peopleSurveyId;
			<LoadSurveyQueueItemFormDataItemsAsync>d__.<>1__state = -1;
			<LoadSurveyQueueItemFormDataItemsAsync>d__.<>t__builder.Start<SurveyQueueClientManager.<LoadSurveyQueueItemFormDataItemsAsync>d__3>(ref <LoadSurveyQueueItemFormDataItemsAsync>d__);
			return <LoadSurveyQueueItemFormDataItemsAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x0000486C File Offset: 0x00002A6C
		[DebuggerStepThrough]
		public Task<IList<SurveyQueueItemDTO>> LoadSurveyQueueItemsAsync(int surveyId, DateTime startDate, DateTime? endDate, int filterByAssignedCounsellorPid, params eSurveyStatusType[] surveyTypesToExclude)
		{
			SurveyQueueClientManager.<LoadSurveyQueueItemsAsync>d__4 <LoadSurveyQueueItemsAsync>d__ = new SurveyQueueClientManager.<LoadSurveyQueueItemsAsync>d__4();
			<LoadSurveyQueueItemsAsync>d__.<>t__builder = AsyncTaskMethodBuilder<IList<SurveyQueueItemDTO>>.Create();
			<LoadSurveyQueueItemsAsync>d__.<>4__this = this;
			<LoadSurveyQueueItemsAsync>d__.surveyId = surveyId;
			<LoadSurveyQueueItemsAsync>d__.startDate = startDate;
			<LoadSurveyQueueItemsAsync>d__.endDate = endDate;
			<LoadSurveyQueueItemsAsync>d__.filterByAssignedCounsellorPid = filterByAssignedCounsellorPid;
			<LoadSurveyQueueItemsAsync>d__.surveyTypesToExclude = surveyTypesToExclude;
			<LoadSurveyQueueItemsAsync>d__.<>1__state = -1;
			<LoadSurveyQueueItemsAsync>d__.<>t__builder.Start<SurveyQueueClientManager.<LoadSurveyQueueItemsAsync>d__4>(ref <LoadSurveyQueueItemsAsync>d__);
			return <LoadSurveyQueueItemsAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x000048D8 File Offset: 0x00002AD8
		[DebuggerStepThrough]
		public Task<SurveyQueueItemDTO> UpdateSurveyQueueItemStaffNoteAndStatusAsync(int peopleSurveyId, int? newPeopleSurveyStatusId, string newStaffNote)
		{
			SurveyQueueClientManager.<UpdateSurveyQueueItemStaffNoteAndStatusAsync>d__5 <UpdateSurveyQueueItemStaffNoteAndStatusAsync>d__ = new SurveyQueueClientManager.<UpdateSurveyQueueItemStaffNoteAndStatusAsync>d__5();
			<UpdateSurveyQueueItemStaffNoteAndStatusAsync>d__.<>t__builder = AsyncTaskMethodBuilder<SurveyQueueItemDTO>.Create();
			<UpdateSurveyQueueItemStaffNoteAndStatusAsync>d__.<>4__this = this;
			<UpdateSurveyQueueItemStaffNoteAndStatusAsync>d__.peopleSurveyId = peopleSurveyId;
			<UpdateSurveyQueueItemStaffNoteAndStatusAsync>d__.newPeopleSurveyStatusId = newPeopleSurveyStatusId;
			<UpdateSurveyQueueItemStaffNoteAndStatusAsync>d__.newStaffNote = newStaffNote;
			<UpdateSurveyQueueItemStaffNoteAndStatusAsync>d__.<>1__state = -1;
			<UpdateSurveyQueueItemStaffNoteAndStatusAsync>d__.<>t__builder.Start<SurveyQueueClientManager.<UpdateSurveyQueueItemStaffNoteAndStatusAsync>d__5>(ref <UpdateSurveyQueueItemStaffNoteAndStatusAsync>d__);
			return <UpdateSurveyQueueItemStaffNoteAndStatusAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x00004934 File Offset: 0x00002B34
		[DebuggerStepThrough]
		public Task<SurveyQueueItemDTO> UpdateSurveyQueueItemStaffNoteAsync(int peopleSurveyId, string newStaffNote)
		{
			SurveyQueueClientManager.<UpdateSurveyQueueItemStaffNoteAsync>d__6 <UpdateSurveyQueueItemStaffNoteAsync>d__ = new SurveyQueueClientManager.<UpdateSurveyQueueItemStaffNoteAsync>d__6();
			<UpdateSurveyQueueItemStaffNoteAsync>d__.<>t__builder = AsyncTaskMethodBuilder<SurveyQueueItemDTO>.Create();
			<UpdateSurveyQueueItemStaffNoteAsync>d__.<>4__this = this;
			<UpdateSurveyQueueItemStaffNoteAsync>d__.peopleSurveyId = peopleSurveyId;
			<UpdateSurveyQueueItemStaffNoteAsync>d__.newStaffNote = newStaffNote;
			<UpdateSurveyQueueItemStaffNoteAsync>d__.<>1__state = -1;
			<UpdateSurveyQueueItemStaffNoteAsync>d__.<>t__builder.Start<SurveyQueueClientManager.<UpdateSurveyQueueItemStaffNoteAsync>d__6>(ref <UpdateSurveyQueueItemStaffNoteAsync>d__);
			return <UpdateSurveyQueueItemStaffNoteAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x00004988 File Offset: 0x00002B88
		[DebuggerStepThrough]
		public Task<SurveyQueueItemDTO> UpdateSurveyQueueItemStatusAsync(int peopleSurveyId, int? newPeopleSurveyStatusId)
		{
			SurveyQueueClientManager.<UpdateSurveyQueueItemStatusAsync>d__7 <UpdateSurveyQueueItemStatusAsync>d__ = new SurveyQueueClientManager.<UpdateSurveyQueueItemStatusAsync>d__7();
			<UpdateSurveyQueueItemStatusAsync>d__.<>t__builder = AsyncTaskMethodBuilder<SurveyQueueItemDTO>.Create();
			<UpdateSurveyQueueItemStatusAsync>d__.<>4__this = this;
			<UpdateSurveyQueueItemStatusAsync>d__.peopleSurveyId = peopleSurveyId;
			<UpdateSurveyQueueItemStatusAsync>d__.newPeopleSurveyStatusId = newPeopleSurveyStatusId;
			<UpdateSurveyQueueItemStatusAsync>d__.<>1__state = -1;
			<UpdateSurveyQueueItemStatusAsync>d__.<>t__builder.Start<SurveyQueueClientManager.<UpdateSurveyQueueItemStatusAsync>d__7>(ref <UpdateSurveyQueueItemStatusAsync>d__);
			return <UpdateSurveyQueueItemStatusAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x000049DC File Offset: 0x00002BDC
		[DebuggerStepThrough]
		public Task<IList<SurveyDTO>> LoadAllowedSurveysAsync()
		{
			SurveyQueueClientManager.<LoadAllowedSurveysAsync>d__8 <LoadAllowedSurveysAsync>d__ = new SurveyQueueClientManager.<LoadAllowedSurveysAsync>d__8();
			<LoadAllowedSurveysAsync>d__.<>t__builder = AsyncTaskMethodBuilder<IList<SurveyDTO>>.Create();
			<LoadAllowedSurveysAsync>d__.<>4__this = this;
			<LoadAllowedSurveysAsync>d__.<>1__state = -1;
			<LoadAllowedSurveysAsync>d__.<>t__builder.Start<SurveyQueueClientManager.<LoadAllowedSurveysAsync>d__8>(ref <LoadAllowedSurveysAsync>d__);
			return <LoadAllowedSurveysAsync>d__.<>t__builder.Task;
		}
	}
}
