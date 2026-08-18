using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.DynamicForms;
using TechnoPro.Common.Public.Entities.Surveys;

namespace TechnoPro.Common.ICore.Surveys
{
	// Token: 0x0200002B RID: 43
	public interface ISurveyQueueManager : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x06000134 RID: 308
		Task<IList<SurveyStatus>> LoadLookupSurveyStatusesAsync();

		// Token: 0x06000135 RID: 309
		Task<IList<SurveyQueueItem>> LoadSurveyQueueItemsAsync(int surveyId, DateTime startDate, DateTime? endDate, int filterByAssignedCounsellorPid, params eSurveyStatusType[] surveyTypesToExclude);

		// Token: 0x06000136 RID: 310
		Task<SurveyQueueItem> UpdateSurveyQueueItemStaffNoteAndStatusAsync(int peopleSurveyId, int? newPeopleSurveyStatusId, string newStaffNote);

		// Token: 0x06000137 RID: 311
		Task<SurveyQueueItem> UpdateSurveyQueueItemStaffNoteAsync(int peopleSurveyId, string newStaffNote);

		// Token: 0x06000138 RID: 312
		Task<SurveyQueueItem> UpdateSurveyQueueItemStatusAsync(int peopleSurveyId, int? newPeopleSurveyStatusId);

		// Token: 0x06000139 RID: 313
		Task<bool> DeleteSurveyQueueItemAsync(int peopleSurveyId);

		// Token: 0x0600013A RID: 314
		Task<IList<DynamicData>> LoadSurveyQueueItemFormDataItemsAsync(int peopleSurveyId);

		// Token: 0x0600013B RID: 315
		Task<SurveyQueueItem> LoadSurveyQueueItemAsync(int peopleSurveyId);

		// Token: 0x0600013C RID: 316
		Task<IList<Survey>> LoadAllowedSurveysAsync();
	}
}
