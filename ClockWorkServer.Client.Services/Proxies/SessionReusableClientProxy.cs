using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x020000DD RID: 221
	public class SessionReusableClientProxy : WCFTokenBasedReusableClientProxy<TechnoPro.ClockWorkServer.Contracts.ISession>, TechnoPro.ClockWorkServer.Contracts.ISession, IService
	{
		// Token: 0x0600089B RID: 2203 RVA: 0x00016612 File Offset: 0x00014812
		public SessionReusableClientProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x0600089C RID: 2204 RVA: 0x0001661D File Offset: 0x0001481D
		public SessionReusableClientProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x0600089D RID: 2205 RVA: 0x0001662C File Offset: 0x0001482C
		public AddSessionResp AddSession(AddSessionReq Req)
		{
			return this.WrapServiceMethod<AddSessionResp>(() => this.Proxy.AddSession(Req));
		}

		// Token: 0x0600089E RID: 2206 RVA: 0x00016664 File Offset: 0x00014864
		public SubtractSessionResp SubtractSession(SubtractSessionReq Req)
		{
			return this.WrapServiceMethod<SubtractSessionResp>(() => this.Proxy.SubtractSession(Req));
		}

		// Token: 0x0600089F RID: 2207 RVA: 0x0001669C File Offset: 0x0001489C
		public GoToTodaysSessionResp GoToTodaysSession(GoToTodaysSessionReq Req)
		{
			return this.WrapServiceMethod<GoToTodaysSessionResp>(() => this.Proxy.GoToTodaysSession(Req));
		}

		// Token: 0x060008A0 RID: 2208 RVA: 0x000166D4 File Offset: 0x000148D4
		public GotoSessionResp GotoSession(GotoSessionReq Req)
		{
			return this.WrapServiceMethod<GotoSessionResp>(() => this.Proxy.GotoSession(Req));
		}

		// Token: 0x060008A1 RID: 2209 RVA: 0x0001670C File Offset: 0x0001490C
		public GetCurrentAcademicTermResp GetCurrentAcademicTerm(GetCurrentAcademicTermReq Req)
		{
			return this.WrapServiceMethod<GetCurrentAcademicTermResp>(() => this.Proxy.GetCurrentAcademicTerm(Req));
		}

		// Token: 0x060008A2 RID: 2210 RVA: 0x00016744 File Offset: 0x00014944
		public CopySessionResp CopySession(CopySessionReq Req)
		{
			return this.WrapServiceMethod<CopySessionResp>(() => this.Proxy.CopySession(Req));
		}

		// Token: 0x060008A3 RID: 2211 RVA: 0x0001677C File Offset: 0x0001497C
		public LoadAcademicTermsResp LoadAcademicTerms(LoadAcademicTermsReq Req)
		{
			return this.WrapServiceMethod<LoadAcademicTermsResp>(() => this.Proxy.LoadAcademicTerms(Req));
		}

		// Token: 0x060008A4 RID: 2212 RVA: 0x000167B4 File Offset: 0x000149B4
		public GetCurrentSessionResp GetCurrentSession(GetCurrentSessionReq Req)
		{
			return this.WrapServiceMethod<GetCurrentSessionResp>(() => this.Proxy.GetCurrentSession(Req));
		}

		// Token: 0x060008A5 RID: 2213 RVA: 0x000167EC File Offset: 0x000149EC
		public GetSessionByDateResp GetSessionByDate(GetSessionByDateReq request)
		{
			return this.WrapServiceMethod<GetSessionByDateResp>(() => this.Proxy.GetSessionByDate(request));
		}

		// Token: 0x060008A6 RID: 2214 RVA: 0x00016824 File Offset: 0x00014A24
		public void SetSessionChooserDefaultValue(SetSessionChooserDefaultValueReq Request)
		{
			this.WrapServiceMethod(delegate()
			{
				this.Proxy.SetSessionChooserDefaultValue(Request);
			});
		}

		// Token: 0x060008A7 RID: 2215 RVA: 0x0001685C File Offset: 0x00014A5C
		public GetSessionChooserDefaultValueResp GetSessionChooserDefaultValue()
		{
			return this.WrapServiceMethod<GetSessionChooserDefaultValueResp>(() => base.Proxy.GetSessionChooserDefaultValue());
		}
	}
}
