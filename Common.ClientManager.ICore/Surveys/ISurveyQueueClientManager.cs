using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms;
using TechnoPro.ClockWorkServer.Contracts.DTO.Surveys;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities.Surveys;

namespace TechnoPro.Common.ClientManager.ICore.Surveys
{
	// Token: 0x02000011 RID: 17
	public interface ISurveyQueueClientManager : IWebService
	{
		// Token: 0x0600006D RID: 109
		Task<IList<SurveyStatusDTO>> LoadLookupSurveyStatusesAsync();

		// Token: 0x0600006E RID: 110
		Task<IList<SurveyQueueItemDTO>> LoadSurveyQueueItemsAsync(int surveyId, DateTime startDate, DateTime? endDate, int filterByAssignedCounsellorPid, params eSurveyStatusType[] surveyTypesToExclude);

		// Token: 0x0600006F RID: 111
		Task<SurveyQueueItemDTO> UpdateSurveyQueueItemStaffNoteAndStatusAsync(int peopleSurveyId, int? newPeopleSurveyStatusId, string newStaffNote);

		// Token: 0x06000070 RID: 112
		Task<SurveyQueueItemDTO> UpdateSurveyQueueItemStaffNoteAsync(int peopleSurveyId, string newStaffNote);

		// Token: 0x06000071 RID: 113
		Task<SurveyQueueItemDTO> UpdateSurveyQueueItemStatusAsync(int peopleSurveyId, int? newPeopleSurveyStatusId);

		// Token: 0x06000072 RID: 114
		Task<IList<SurveyDTO>> LoadAllowedSurveysAsync();

		// Token: 0x06000073 RID: 115
		Task<bool> DeleteSurveyQueueItemAsync(int peopleSurveyId);

		// Token: 0x06000074 RID: 116
		Task<IList<DynamicDataDTO>> LoadSurveyQueueItemFormDataItemsAsync(int peopleSurveyId);

		// Token: 0x06000075 RID: 117
		Task<SurveyQueueItemDTO> LoadSurveyQueueItemAsync(int peopleSurveyId);
	}
}
