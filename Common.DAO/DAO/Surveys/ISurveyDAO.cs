using System;
using System.Collections.Generic;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Surveys;

namespace TechnoPro.Common.DAO.Surveys
{
	// Token: 0x02000026 RID: 38
	public interface ISurveyDAO : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x06000096 RID: 150
		List<Survey> GetAllSurveys();

		// Token: 0x06000097 RID: 151
		List<Survey> GetActiveSurveys();

		// Token: 0x06000098 RID: 152
		Survey GetSurvey(int SurveyId);

		// Token: 0x06000099 RID: 153
		int CreateNewSurvey(Survey Survey);

		// Token: 0x0600009A RID: 154
		void UpdateSurvey(Survey Survey);

		// Token: 0x0600009B RID: 155
		void DeleteSurvey(int SurveyId);

		// Token: 0x0600009C RID: 156
		void DisableSurvey(int SurveyId);

		// Token: 0x0600009D RID: 157
		void EnableSurvey(int SurveyId);
	}
}
