using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.Surveys;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x02000147 RID: 327
	public class SurveyReusableClientProxy : WCFTokenBasedReusableClientProxy<ISurvey>, ISurvey, IService
	{
		// Token: 0x06000C7E RID: 3198 RVA: 0x0001F222 File Offset: 0x0001D422
		public SurveyReusableClientProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x06000C7F RID: 3199 RVA: 0x0001F22D File Offset: 0x0001D42D
		public SurveyReusableClientProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x06000C80 RID: 3200 RVA: 0x0001F23C File Offset: 0x0001D43C
		public GetActiveSurveysResp GetActiveSurveys(GetActiveSurveysReq Request)
		{
			return this.WrapServiceMethod<GetActiveSurveysResp>(() => this.Proxy.GetActiveSurveys(Request));
		}

		// Token: 0x06000C81 RID: 3201 RVA: 0x0001F274 File Offset: 0x0001D474
		public GetAllSurveysResp GetAllSurveys(GetAllSurveysReq Request)
		{
			return this.WrapServiceMethod<GetAllSurveysResp>(() => this.Proxy.GetAllSurveys(Request));
		}

		// Token: 0x06000C82 RID: 3202 RVA: 0x0001F2AC File Offset: 0x0001D4AC
		public GetSurveyResp GetSurvey(GetSurveyReq Request)
		{
			return this.WrapServiceMethod<GetSurveyResp>(() => this.Proxy.GetSurvey(Request));
		}

		// Token: 0x06000C83 RID: 3203 RVA: 0x0001F2E4 File Offset: 0x0001D4E4
		public void DeleteSurvey(DeleteSurveyReq Request)
		{
			this.WrapServiceMethod(delegate()
			{
				this.Proxy.DeleteSurvey(Request);
			});
		}

		// Token: 0x06000C84 RID: 3204 RVA: 0x0001F31C File Offset: 0x0001D51C
		public void UpdateSurvey(UpdateSurveyReq request)
		{
			this.WrapServiceMethod(delegate()
			{
				this.Proxy.UpdateSurvey(request);
			});
		}

		// Token: 0x06000C85 RID: 3205 RVA: 0x0001F354 File Offset: 0x0001D554
		public CreateNewSurveyResp CreateNewSurvey(CreateNewSurveyReq request)
		{
			return this.WrapServiceMethod<CreateNewSurveyResp>(() => this.Proxy.CreateNewSurvey(request));
		}

		// Token: 0x06000C86 RID: 3206 RVA: 0x0001F38C File Offset: 0x0001D58C
		public void DisableSurvey(DisableSurveyReq Request)
		{
			this.WrapServiceMethod(delegate()
			{
				this.Proxy.DisableSurvey(Request);
			});
		}

		// Token: 0x06000C87 RID: 3207 RVA: 0x0001F3C4 File Offset: 0x0001D5C4
		public void EnableSurvey(EnableSurveyReq Request)
		{
			this.WrapServiceMethod(delegate()
			{
				this.Proxy.EnableSurvey(Request);
			});
		}
	}
}
