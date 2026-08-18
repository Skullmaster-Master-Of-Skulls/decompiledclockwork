using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using TechnoPro.Common.ICore.Surveys;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.DynamicForms;
using TechnoPro.Common.Public.Entities.Surveys;

namespace TechnoPro.Common.Core.Surveys
{
	// Token: 0x02000039 RID: 57
	public class SurveyQueueManager : ISurveyQueueManager, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x17000065 RID: 101
		// (get) Token: 0x0600024A RID: 586 RVA: 0x0000C36E File Offset: 0x0000A56E
		// (set) Token: 0x0600024B RID: 587 RVA: 0x0000C376 File Offset: 0x0000A576
		public OperationContext OpContext { get; set; }

		// Token: 0x0600024C RID: 588 RVA: 0x0000C37F File Offset: 0x0000A57F
		public SurveyQueueManager(OperationContext opContext)
		{
			this.OpContext = opContext;
		}

		// Token: 0x0600024D RID: 589 RVA: 0x0000C394 File Offset: 0x0000A594
		[DebuggerStepThrough]
		public Task<IList<SurveyStatus>> LoadLookupSurveyStatusesAsync()
		{
			SurveyQueueManager.<LoadLookupSurveyStatusesAsync>d__5 <LoadLookupSurveyStatusesAsync>d__ = new SurveyQueueManager.<LoadLookupSurveyStatusesAsync>d__5();
			<LoadLookupSurveyStatusesAsync>d__.<>t__builder = AsyncTaskMethodBuilder<IList<SurveyStatus>>.Create();
			<LoadLookupSurveyStatusesAsync>d__.<>4__this = this;
			<LoadLookupSurveyStatusesAsync>d__.<>1__state = -1;
			<LoadLookupSurveyStatusesAsync>d__.<>t__builder.Start<SurveyQueueManager.<LoadLookupSurveyStatusesAsync>d__5>(ref <LoadLookupSurveyStatusesAsync>d__);
			return <LoadLookupSurveyStatusesAsync>d__.<>t__builder.Task;
		}

		// Token: 0x0600024E RID: 590 RVA: 0x0000C3D8 File Offset: 0x0000A5D8
		[DebuggerStepThrough]
		public Task<IList<SurveyQueueItem>> LoadSurveyQueueItemsAsync(int surveyId, DateTime startDate, DateTime? endDate, int filterByAssignedCounsellorPid, params eSurveyStatusType[] surveyTypesToExclude)
		{
			SurveyQueueManager.<LoadSurveyQueueItemsAsync>d__6 <LoadSurveyQueueItemsAsync>d__ = new SurveyQueueManager.<LoadSurveyQueueItemsAsync>d__6();
			<LoadSurveyQueueItemsAsync>d__.<>t__builder = AsyncTaskMethodBuilder<IList<SurveyQueueItem>>.Create();
			<LoadSurveyQueueItemsAsync>d__.<>4__this = this;
			<LoadSurveyQueueItemsAsync>d__.surveyId = surveyId;
			<LoadSurveyQueueItemsAsync>d__.startDate = startDate;
			<LoadSurveyQueueItemsAsync>d__.endDate = endDate;
			<LoadSurveyQueueItemsAsync>d__.filterByAssignedCounsellorPid = filterByAssignedCounsellorPid;
			<LoadSurveyQueueItemsAsync>d__.surveyTypesToExclude = surveyTypesToExclude;
			<LoadSurveyQueueItemsAsync>d__.<>1__state = -1;
			<LoadSurveyQueueItemsAsync>d__.<>t__builder.Start<SurveyQueueManager.<LoadSurveyQueueItemsAsync>d__6>(ref <LoadSurveyQueueItemsAsync>d__);
			return <LoadSurveyQueueItemsAsync>d__.<>t__builder.Task;
		}

		// Token: 0x0600024F RID: 591 RVA: 0x0000C444 File Offset: 0x0000A644
		[DebuggerStepThrough]
		public Task<bool> DeleteSurveyQueueItemAsync(int peopleSurveyId)
		{
			SurveyQueueManager.<DeleteSurveyQueueItemAsync>d__7 <DeleteSurveyQueueItemAsync>d__ = new SurveyQueueManager.<DeleteSurveyQueueItemAsync>d__7();
			<DeleteSurveyQueueItemAsync>d__.<>t__builder = AsyncTaskMethodBuilder<bool>.Create();
			<DeleteSurveyQueueItemAsync>d__.<>4__this = this;
			<DeleteSurveyQueueItemAsync>d__.peopleSurveyId = peopleSurveyId;
			<DeleteSurveyQueueItemAsync>d__.<>1__state = -1;
			<DeleteSurveyQueueItemAsync>d__.<>t__builder.Start<SurveyQueueManager.<DeleteSurveyQueueItemAsync>d__7>(ref <DeleteSurveyQueueItemAsync>d__);
			return <DeleteSurveyQueueItemAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000250 RID: 592 RVA: 0x0000C490 File Offset: 0x0000A690
		[DebuggerStepThrough]
		public Task<IList<DynamicData>> LoadSurveyQueueItemFormDataItemsAsync(int peopleSurveyId)
		{
			SurveyQueueManager.<LoadSurveyQueueItemFormDataItemsAsync>d__8 <LoadSurveyQueueItemFormDataItemsAsync>d__ = new SurveyQueueManager.<LoadSurveyQueueItemFormDataItemsAsync>d__8();
			<LoadSurveyQueueItemFormDataItemsAsync>d__.<>t__builder = AsyncTaskMethodBuilder<IList<DynamicData>>.Create();
			<LoadSurveyQueueItemFormDataItemsAsync>d__.<>4__this = this;
			<LoadSurveyQueueItemFormDataItemsAsync>d__.peopleSurveyId = peopleSurveyId;
			<LoadSurveyQueueItemFormDataItemsAsync>d__.<>1__state = -1;
			<LoadSurveyQueueItemFormDataItemsAsync>d__.<>t__builder.Start<SurveyQueueManager.<LoadSurveyQueueItemFormDataItemsAsync>d__8>(ref <LoadSurveyQueueItemFormDataItemsAsync>d__);
			return <LoadSurveyQueueItemFormDataItemsAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000251 RID: 593 RVA: 0x0000C4DC File Offset: 0x0000A6DC
		[DebuggerStepThrough]
		public Task<SurveyQueueItem> LoadSurveyQueueItemAsync(int peopleSurveyId)
		{
			SurveyQueueManager.<LoadSurveyQueueItemAsync>d__9 <LoadSurveyQueueItemAsync>d__ = new SurveyQueueManager.<LoadSurveyQueueItemAsync>d__9();
			<LoadSurveyQueueItemAsync>d__.<>t__builder = AsyncTaskMethodBuilder<SurveyQueueItem>.Create();
			<LoadSurveyQueueItemAsync>d__.<>4__this = this;
			<LoadSurveyQueueItemAsync>d__.peopleSurveyId = peopleSurveyId;
			<LoadSurveyQueueItemAsync>d__.<>1__state = -1;
			<LoadSurveyQueueItemAsync>d__.<>t__builder.Start<SurveyQueueManager.<LoadSurveyQueueItemAsync>d__9>(ref <LoadSurveyQueueItemAsync>d__);
			return <LoadSurveyQueueItemAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000252 RID: 594 RVA: 0x0000C528 File Offset: 0x0000A728
		[DebuggerStepThrough]
		public Task<IList<Survey>> LoadAllowedSurveysAsync()
		{
			SurveyQueueManager.<LoadAllowedSurveysAsync>d__10 <LoadAllowedSurveysAsync>d__ = new SurveyQueueManager.<LoadAllowedSurveysAsync>d__10();
			<LoadAllowedSurveysAsync>d__.<>t__builder = AsyncTaskMethodBuilder<IList<Survey>>.Create();
			<LoadAllowedSurveysAsync>d__.<>4__this = this;
			<LoadAllowedSurveysAsync>d__.<>1__state = -1;
			<LoadAllowedSurveysAsync>d__.<>t__builder.Start<SurveyQueueManager.<LoadAllowedSurveysAsync>d__10>(ref <LoadAllowedSurveysAsync>d__);
			return <LoadAllowedSurveysAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000253 RID: 595 RVA: 0x0000C56C File Offset: 0x0000A76C
		[DebuggerStepThrough]
		public Task<SurveyQueueItem> UpdateSurveyQueueItemStaffNoteAndStatusAsync(int peopleSurveyId, int? newPeopleSurveyStatusId, string newStaffNote)
		{
			SurveyQueueManager.<UpdateSurveyQueueItemStaffNoteAndStatusAsync>d__11 <UpdateSurveyQueueItemStaffNoteAndStatusAsync>d__ = new SurveyQueueManager.<UpdateSurveyQueueItemStaffNoteAndStatusAsync>d__11();
			<UpdateSurveyQueueItemStaffNoteAndStatusAsync>d__.<>t__builder = AsyncTaskMethodBuilder<SurveyQueueItem>.Create();
			<UpdateSurveyQueueItemStaffNoteAndStatusAsync>d__.<>4__this = this;
			<UpdateSurveyQueueItemStaffNoteAndStatusAsync>d__.peopleSurveyId = peopleSurveyId;
			<UpdateSurveyQueueItemStaffNoteAndStatusAsync>d__.newPeopleSurveyStatusId = newPeopleSurveyStatusId;
			<UpdateSurveyQueueItemStaffNoteAndStatusAsync>d__.newStaffNote = newStaffNote;
			<UpdateSurveyQueueItemStaffNoteAndStatusAsync>d__.<>1__state = -1;
			<UpdateSurveyQueueItemStaffNoteAndStatusAsync>d__.<>t__builder.Start<SurveyQueueManager.<UpdateSurveyQueueItemStaffNoteAndStatusAsync>d__11>(ref <UpdateSurveyQueueItemStaffNoteAndStatusAsync>d__);
			return <UpdateSurveyQueueItemStaffNoteAndStatusAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000254 RID: 596 RVA: 0x0000C5C8 File Offset: 0x0000A7C8
		[DebuggerStepThrough]
		public Task<SurveyQueueItem> UpdateSurveyQueueItemStaffNoteAsync(int peopleSurveyId, string newStaffNote)
		{
			SurveyQueueManager.<UpdateSurveyQueueItemStaffNoteAsync>d__12 <UpdateSurveyQueueItemStaffNoteAsync>d__ = new SurveyQueueManager.<UpdateSurveyQueueItemStaffNoteAsync>d__12();
			<UpdateSurveyQueueItemStaffNoteAsync>d__.<>t__builder = AsyncTaskMethodBuilder<SurveyQueueItem>.Create();
			<UpdateSurveyQueueItemStaffNoteAsync>d__.<>4__this = this;
			<UpdateSurveyQueueItemStaffNoteAsync>d__.peopleSurveyId = peopleSurveyId;
			<UpdateSurveyQueueItemStaffNoteAsync>d__.newStaffNote = newStaffNote;
			<UpdateSurveyQueueItemStaffNoteAsync>d__.<>1__state = -1;
			<UpdateSurveyQueueItemStaffNoteAsync>d__.<>t__builder.Start<SurveyQueueManager.<UpdateSurveyQueueItemStaffNoteAsync>d__12>(ref <UpdateSurveyQueueItemStaffNoteAsync>d__);
			return <UpdateSurveyQueueItemStaffNoteAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000255 RID: 597 RVA: 0x0000C61C File Offset: 0x0000A81C
		[DebuggerStepThrough]
		public Task<SurveyQueueItem> UpdateSurveyQueueItemStatusAsync(int peopleSurveyId, int? newPeopleSurveyStatusId)
		{
			SurveyQueueManager.<UpdateSurveyQueueItemStatusAsync>d__13 <UpdateSurveyQueueItemStatusAsync>d__ = new SurveyQueueManager.<UpdateSurveyQueueItemStatusAsync>d__13();
			<UpdateSurveyQueueItemStatusAsync>d__.<>t__builder = AsyncTaskMethodBuilder<SurveyQueueItem>.Create();
			<UpdateSurveyQueueItemStatusAsync>d__.<>4__this = this;
			<UpdateSurveyQueueItemStatusAsync>d__.peopleSurveyId = peopleSurveyId;
			<UpdateSurveyQueueItemStatusAsync>d__.newPeopleSurveyStatusId = newPeopleSurveyStatusId;
			<UpdateSurveyQueueItemStatusAsync>d__.<>1__state = -1;
			<UpdateSurveyQueueItemStatusAsync>d__.<>t__builder.Start<SurveyQueueManager.<UpdateSurveyQueueItemStatusAsync>d__13>(ref <UpdateSurveyQueueItemStatusAsync>d__);
			return <UpdateSurveyQueueItemStatusAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000256 RID: 598 RVA: 0x0000C670 File Offset: 0x0000A870
		[DebuggerStepThrough]
		private Task<SurveyQueueItem> UpdateSurveyQueueItemAsync(int peopleSurveyId, Func<Task<SurveyQueueItem>> updateItem)
		{
			SurveyQueueManager.<UpdateSurveyQueueItemAsync>d__14 <UpdateSurveyQueueItemAsync>d__ = new SurveyQueueManager.<UpdateSurveyQueueItemAsync>d__14();
			<UpdateSurveyQueueItemAsync>d__.<>t__builder = AsyncTaskMethodBuilder<SurveyQueueItem>.Create();
			<UpdateSurveyQueueItemAsync>d__.<>4__this = this;
			<UpdateSurveyQueueItemAsync>d__.peopleSurveyId = peopleSurveyId;
			<UpdateSurveyQueueItemAsync>d__.updateItem = updateItem;
			<UpdateSurveyQueueItemAsync>d__.<>1__state = -1;
			<UpdateSurveyQueueItemAsync>d__.<>t__builder.Start<SurveyQueueManager.<UpdateSurveyQueueItemAsync>d__14>(ref <UpdateSurveyQueueItemAsync>d__);
			return <UpdateSurveyQueueItemAsync>d__.<>t__builder.Task;
		}
	}
}
