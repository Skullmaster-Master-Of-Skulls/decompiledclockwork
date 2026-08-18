using System;
using System.Collections.Generic;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Surveys;

namespace TechnoPro.Common.ICore.Surveys
{
	// Token: 0x0200002C RID: 44
	public interface ISurveyManager : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x0600013D RID: 317
		List<Survey> GetAllSurveys();

		// Token: 0x0600013E RID: 318
		List<Survey> GetActiveSurveys();

		// Token: 0x0600013F RID: 319
		Survey GetSurvey(int SurveyId);

		// Token: 0x06000140 RID: 320
		void UpdateSurvey(Survey Survey);

		// Token: 0x06000141 RID: 321
		int CreateSurvey(Survey Survey);

		// Token: 0x06000142 RID: 322
		void DeleteSurvey(int SurveyId);

		// Token: 0x06000143 RID: 323
		void DisableSurvey(int SurveyId);

		// Token: 0x06000144 RID: 324
		void EnableSurvey(int SurveyId);
	}
}
