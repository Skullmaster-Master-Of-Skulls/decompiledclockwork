using System;
using System.Collections.Generic;
using TechnoPro.ClockWorkServer.Common.Services.Impl.Adapters;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.Surveys;
using TechnoPro.Common.Core.Mappers.Surveys;
using TechnoPro.Common.Core.Surveys;
using TechnoPro.Common.ICore.Surveys;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities.Surveys;

namespace TechnoPro.ClockWorkServer.Common.Services.Impl
{
	// Token: 0x02000092 RID: 146
	public class SurveyServiceManager : ISurvey, IService
	{
		// Token: 0x06000532 RID: 1330 RVA: 0x00018458 File Offset: 0x00016658
		public int CheckConnectivity()
		{
			return 1;
		}

		// Token: 0x06000533 RID: 1331 RVA: 0x0001846C File Offset: 0x0001666C
		public GetAllSurveysResp GetAllSurveys(GetAllSurveysReq request)
		{
			ISurveyManager surveyManager = new SurveyManager(request.GetOperationContext());
			List<Survey> allSurveys = surveyManager.GetAllSurveys();
			List<SurveyDTO> surveys = allSurveys.ConvertAll<SurveyDTO>((Survey s) => s.ToDTO());
			return new GetAllSurveysResp
			{
				Surveys = surveys
			};
		}

		// Token: 0x06000534 RID: 1332 RVA: 0x000184C4 File Offset: 0x000166C4
		public GetActiveSurveysResp GetActiveSurveys(GetActiveSurveysReq request)
		{
			ISurveyManager surveyManager = new SurveyManager(request.GetOperationContext());
			List<Survey> activeSurveys = surveyManager.GetActiveSurveys();
			List<SurveyDTO> surveys = activeSurveys.ConvertAll<SurveyDTO>((Survey s) => s.ToDTO());
			return new GetActiveSurveysResp
			{
				Surveys = surveys
			};
		}

		// Token: 0x06000535 RID: 1333 RVA: 0x0001851C File Offset: 0x0001671C
		public GetSurveyResp GetSurvey(GetSurveyReq request)
		{
			ISurveyManager surveyManager = new SurveyManager(request.GetOperationContext());
			Survey survey = surveyManager.GetSurvey(request.SurveyId);
			return new GetSurveyResp
			{
				Survey = survey.ToDTO()
			};
		}

		// Token: 0x06000536 RID: 1334 RVA: 0x0001855C File Offset: 0x0001675C
		public void DeleteSurvey(DeleteSurveyReq Request)
		{
			ISurveyManager surveyManager = new SurveyManager(Request.GetOperationContext());
			surveyManager.DeleteSurvey(Request.SurveyId);
		}

		// Token: 0x06000537 RID: 1335 RVA: 0x00018584 File Offset: 0x00016784
		public void UpdateSurvey(UpdateSurveyReq request)
		{
			ISurveyManager surveyManager = new SurveyManager(request.GetOperationContext());
			surveyManager.UpdateSurvey(request.Survey.ToDomainObject());
		}

		// Token: 0x06000538 RID: 1336 RVA: 0x000185B0 File Offset: 0x000167B0
		public CreateNewSurveyResp CreateNewSurvey(CreateNewSurveyReq request)
		{
			ISurveyManager surveyManager = new SurveyManager(request.GetOperationContext());
			int surveyId = surveyManager.CreateSurvey(request.Survey.ToDomainObject());
			return new CreateNewSurveyResp
			{
				SurveyId = surveyId
			};
		}

		// Token: 0x06000539 RID: 1337 RVA: 0x000185F0 File Offset: 0x000167F0
		public void DisableSurvey(DisableSurveyReq Request)
		{
			ISurveyManager surveyManager = new SurveyManager(Request.GetOperationContext());
			surveyManager.DisableSurvey(Request.SurveyId);
		}

		// Token: 0x0600053A RID: 1338 RVA: 0x00018618 File Offset: 0x00016818
		public void EnableSurvey(EnableSurveyReq Request)
		{
			ISurveyManager surveyManager = new SurveyManager(Request.GetOperationContext());
			surveyManager.EnableSurvey(Request.SurveyId);
		}
	}
}
