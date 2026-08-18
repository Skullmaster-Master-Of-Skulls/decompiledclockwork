using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.Surveys;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x02000148 RID: 328
	internal class SurveyClientBaseProxy : ClientBase<ISurvey>, ISurvey, IService
	{
		// Token: 0x06000C88 RID: 3208 RVA: 0x0001F3F9 File Offset: 0x0001D5F9
		public SurveyClientBaseProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x06000C89 RID: 3209 RVA: 0x0001F404 File Offset: 0x0001D604
		public SurveyClientBaseProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x06000C8A RID: 3210 RVA: 0x0001F410 File Offset: 0x0001D610
		public GetActiveSurveysResp GetActiveSurveys(GetActiveSurveysReq Request)
		{
			return base.Channel.GetActiveSurveys(Request);
		}

		// Token: 0x06000C8B RID: 3211 RVA: 0x0001F430 File Offset: 0x0001D630
		public GetAllSurveysResp GetAllSurveys(GetAllSurveysReq Request)
		{
			return base.Channel.GetAllSurveys(Request);
		}

		// Token: 0x06000C8C RID: 3212 RVA: 0x0001F450 File Offset: 0x0001D650
		public GetSurveyResp GetSurvey(GetSurveyReq Request)
		{
			return base.Channel.GetSurvey(Request);
		}

		// Token: 0x06000C8D RID: 3213 RVA: 0x0001F46E File Offset: 0x0001D66E
		public void DeleteSurvey(DeleteSurveyReq Request)
		{
			base.Channel.DeleteSurvey(Request);
		}

		// Token: 0x06000C8E RID: 3214 RVA: 0x0001F47E File Offset: 0x0001D67E
		public void UpdateSurvey(UpdateSurveyReq request)
		{
			base.Channel.UpdateSurvey(request);
		}

		// Token: 0x06000C8F RID: 3215 RVA: 0x0001F490 File Offset: 0x0001D690
		public CreateNewSurveyResp CreateNewSurvey(CreateNewSurveyReq request)
		{
			return base.Channel.CreateNewSurvey(request);
		}

		// Token: 0x06000C90 RID: 3216 RVA: 0x0001F4AE File Offset: 0x0001D6AE
		public void DisableSurvey(DisableSurveyReq Request)
		{
			base.Channel.DisableSurvey(Request);
		}

		// Token: 0x06000C91 RID: 3217 RVA: 0x0001F4BE File Offset: 0x0001D6BE
		public void EnableSurvey(EnableSurveyReq Request)
		{
			base.Channel.EnableSurvey(Request);
		}
	}
}
