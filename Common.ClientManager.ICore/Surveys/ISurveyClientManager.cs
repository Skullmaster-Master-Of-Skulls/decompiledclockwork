using System;
using System.Collections.Generic;
using TechnoPro.ClockWorkServer.Contracts.DTO.Surveys;
using TechnoPro.Common.Public;

namespace TechnoPro.Common.ClientManager.ICore.Surveys
{
	// Token: 0x02000010 RID: 16
	public interface ISurveyClientManager : IWebService
	{
		// Token: 0x06000065 RID: 101
		IList<SurveyDTO> GetAllSurveys();

		// Token: 0x06000066 RID: 102
		IList<SurveyDTO> GetActiveSurveys();

		// Token: 0x06000067 RID: 103
		SurveyDTO GetSurvey(int SurveyId);

		// Token: 0x06000068 RID: 104
		void UpdateSurvey(SurveyDTO Survey);

		// Token: 0x06000069 RID: 105
		int CreateNewSurvey(SurveyDTO Survey);

		// Token: 0x0600006A RID: 106
		void DeleteSurvey(int SurveyId);

		// Token: 0x0600006B RID: 107
		void DisableSurvey(int SurveyId);

		// Token: 0x0600006C RID: 108
		void EnableSurvey(int SurveyId);
	}
}
