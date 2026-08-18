using System;
using System.Collections.Generic;
using TechnoPro.ClockWorkServer.Contracts.DTO.Surveys;
using TechnoPro.Common.ClientManager.ICore.Surveys;
using TechnoPro.Common.Public;
using TechnoPro.Common.Web.Security.Proxy;

namespace TechnoPro.Common.Rest.ClientManager.Core.Surveys
{
	// Token: 0x0200000F RID: 15
	public class SurveyRestClientManager : BearerTokenRestProxy<ISurveyClientManager>, ISurveyClientManager, IWebService
	{
		// Token: 0x0600007B RID: 123 RVA: 0x00003477 File Offset: 0x00001677
		public SurveyRestClientManager(string serviceAddress, string token = null) : base(serviceAddress, token)
		{
		}

		// Token: 0x0600007C RID: 124 RVA: 0x00003481 File Offset: 0x00001681
		public SurveyRestClientManager(string serviceAddress, string serviceAddressSuffix, string token = null) : base(serviceAddress, serviceAddressSuffix, token)
		{
		}

		// Token: 0x0600007D RID: 125 RVA: 0x0000348C File Offset: 0x0000168C
		public IList<SurveyDTO> GetAllSurveys()
		{
			return base.GetMany<SurveyDTO>("survey", true);
		}

		// Token: 0x0600007E RID: 126 RVA: 0x0000349A File Offset: 0x0000169A
		public IList<SurveyDTO> GetActiveSurveys()
		{
			return base.GetMany<SurveyDTO>("survey/active", true);
		}

		// Token: 0x0600007F RID: 127 RVA: 0x000034A8 File Offset: 0x000016A8
		public SurveyDTO GetSurvey(int SurveyId)
		{
			return base.Get<SurveyDTO>(string.Format("survey/surveyid/{0}", SurveyId), true);
		}

		// Token: 0x06000080 RID: 128 RVA: 0x000034C1 File Offset: 0x000016C1
		public void UpdateSurvey(SurveyDTO Survey)
		{
			base.Put<SurveyDTO>(Survey, "survey");
		}

		// Token: 0x06000081 RID: 129 RVA: 0x000034CF File Offset: 0x000016CF
		public int CreateNewSurvey(SurveyDTO Survey)
		{
			return base.Post<SurveyDTO, int>(Survey, "survey");
		}

		// Token: 0x06000082 RID: 130 RVA: 0x000034DD File Offset: 0x000016DD
		public void DeleteSurvey(int SurveyId)
		{
			base.Delete(string.Format("survey/surveyid/{0}", SurveyId));
		}

		// Token: 0x06000083 RID: 131 RVA: 0x000034F5 File Offset: 0x000016F5
		public void DisableSurvey(int SurveyId)
		{
			base.Post<int>(SurveyId, "survey/disable");
		}

		// Token: 0x06000084 RID: 132 RVA: 0x00003503 File Offset: 0x00001703
		public void EnableSurvey(int SurveyId)
		{
			base.Post<int>(SurveyId, "survey/enable");
		}
	}
}
