using System;
using System.Collections.Generic;
using TechnoPro.ClockWorkServer.Client.ServiceFactory;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.Surveys;
using TechnoPro.Common.ClientManager.ICore;
using TechnoPro.Common.ClientManager.ICore.Surveys;
using TechnoPro.Common.Public;
using TechnoPro.Common.Unity.IoC;

namespace TechnoPro.Common.ClientManager.Core.Surveys
{
	// Token: 0x02000013 RID: 19
	public class SurveyClientManager : ISurveyClientManager, IWebService
	{
		// Token: 0x06000097 RID: 151 RVA: 0x000045B4 File Offset: 0x000027B4
		public IList<SurveyDTO> GetAllSurveys()
		{
			GetAllSurveysReq request = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<GetAllSurveysReq>();
			return ClientServiceFactory.GetClientInstance<ISurvey>().GetAllSurveys(request).Surveys;
		}

		// Token: 0x06000098 RID: 152 RVA: 0x000045E4 File Offset: 0x000027E4
		public IList<SurveyDTO> GetActiveSurveys()
		{
			GetActiveSurveysReq request = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<GetActiveSurveysReq>();
			return ClientServiceFactory.GetClientInstance<ISurvey>().GetActiveSurveys(request).Surveys;
		}

		// Token: 0x06000099 RID: 153 RVA: 0x00004614 File Offset: 0x00002814
		public SurveyDTO GetSurvey(int SurveyId)
		{
			GetSurveyReq getSurveyReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<GetSurveyReq>();
			getSurveyReq.SurveyId = SurveyId;
			return ClientServiceFactory.GetClientInstance<ISurvey>().GetSurvey(getSurveyReq).Survey;
		}

		// Token: 0x0600009A RID: 154 RVA: 0x0000464C File Offset: 0x0000284C
		public void DeleteSurvey(int SurveyId)
		{
			DeleteSurveyReq deleteSurveyReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<DeleteSurveyReq>();
			deleteSurveyReq.SurveyId = SurveyId;
			ClientServiceFactory.GetClientInstance<ISurvey>().DeleteSurvey(deleteSurveyReq);
		}

		// Token: 0x0600009B RID: 155 RVA: 0x0000467C File Offset: 0x0000287C
		public void UpdateSurvey(SurveyDTO Survey)
		{
			UpdateSurveyReq updateSurveyReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<UpdateSurveyReq>();
			updateSurveyReq.Survey = Survey;
			ClientServiceFactory.GetClientInstance<ISurvey>().UpdateSurvey(updateSurveyReq);
		}

		// Token: 0x0600009C RID: 156 RVA: 0x000046AC File Offset: 0x000028AC
		public int CreateNewSurvey(SurveyDTO Survey)
		{
			CreateNewSurveyReq createNewSurveyReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<CreateNewSurveyReq>();
			createNewSurveyReq.Survey = Survey;
			return ClientServiceFactory.GetClientInstance<ISurvey>().CreateNewSurvey(createNewSurveyReq).SurveyId;
		}

		// Token: 0x0600009D RID: 157 RVA: 0x000046E4 File Offset: 0x000028E4
		public void DisableSurvey(int SurveyId)
		{
			DisableSurveyReq disableSurveyReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<DisableSurveyReq>();
			disableSurveyReq.SurveyId = SurveyId;
			ClientServiceFactory.GetClientInstance<ISurvey>().DisableSurvey(disableSurveyReq);
		}

		// Token: 0x0600009E RID: 158 RVA: 0x00004714 File Offset: 0x00002914
		public void EnableSurvey(int SurveyId)
		{
			EnableSurveyReq enableSurveyReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<EnableSurveyReq>();
			enableSurveyReq.SurveyId = SurveyId;
			ClientServiceFactory.GetClientInstance<ISurvey>().EnableSurvey(enableSurveyReq);
		}
	}
}
