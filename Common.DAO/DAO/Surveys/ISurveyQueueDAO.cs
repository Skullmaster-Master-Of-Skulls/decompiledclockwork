using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Surveys;

namespace TechnoPro.Common.DAO.Surveys
{
	// Token: 0x02000027 RID: 39
	public interface ISurveyQueueDAO : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x0600009E RID: 158
		Task<IList<SurveyStatus>> LoadLookupSurveyStatusesAsync();

		// Token: 0x0600009F RID: 159
		Task<IList<SurveyQueueItem>> LoadSurveyQueueItemsAsync(int surveyId, DateTime startDate, DateTime? endDate, int filterByAssignedCounsellorPid, params int[] statusIdsToExclude);

		// Token: 0x060000A0 RID: 160
		Task<SurveyQueueItem> UpdateSurveyQueueItemStaffNoteAndStatusAsync(int peopleSurveyId, int? newPeopleSurveyStatusId, string newStaffNote);

		// Token: 0x060000A1 RID: 161
		Task<SurveyQueueItem> UpdateSurveyQueueItemStaffNoteAsync(int peopleSurveyId, string newStaffNote);

		// Token: 0x060000A2 RID: 162
		Task<SurveyQueueItem> UpdateSurveyQueueItemStatusAsync(int peopleSurveyId, int? newPeopleSurveyStatusId);

		// Token: 0x060000A3 RID: 163
		Task<bool> DeleteSurveyQueueItemAsync(int peopleSurveyId);

		// Token: 0x060000A4 RID: 164
		Task<SurveyQueueItem> LoadSurveyQueueItemAsync(int peopleSurveyId);

		// Token: 0x060000A5 RID: 165
		Task<int?> LoadSurveyIdByPeopleSurveyId(int peopleSurveyId);
	}
}
