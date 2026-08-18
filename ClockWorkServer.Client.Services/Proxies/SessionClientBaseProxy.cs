using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x020000DE RID: 222
	internal class SessionClientBaseProxy : ClientBase<TechnoPro.ClockWorkServer.Contracts.ISession>, TechnoPro.ClockWorkServer.Contracts.ISession, IService
	{
		// Token: 0x060008A9 RID: 2217 RVA: 0x0001688D File Offset: 0x00014A8D
		public SessionClientBaseProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x060008AA RID: 2218 RVA: 0x00016898 File Offset: 0x00014A98
		public SessionClientBaseProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x060008AB RID: 2219 RVA: 0x000168A4 File Offset: 0x00014AA4
		public AddSessionResp AddSession(AddSessionReq Req)
		{
			return base.Channel.AddSession(Req);
		}

		// Token: 0x060008AC RID: 2220 RVA: 0x000168C4 File Offset: 0x00014AC4
		public SubtractSessionResp SubtractSession(SubtractSessionReq Req)
		{
			return base.Channel.SubtractSession(Req);
		}

		// Token: 0x060008AD RID: 2221 RVA: 0x000168E4 File Offset: 0x00014AE4
		public GoToTodaysSessionResp GoToTodaysSession(GoToTodaysSessionReq Req)
		{
			return base.Channel.GoToTodaysSession(Req);
		}

		// Token: 0x060008AE RID: 2222 RVA: 0x00016904 File Offset: 0x00014B04
		public GotoSessionResp GotoSession(GotoSessionReq Req)
		{
			return base.Channel.GotoSession(Req);
		}

		// Token: 0x060008AF RID: 2223 RVA: 0x00016924 File Offset: 0x00014B24
		public GetCurrentAcademicTermResp GetCurrentAcademicTerm(GetCurrentAcademicTermReq Req)
		{
			return base.Channel.GetCurrentAcademicTerm(Req);
		}

		// Token: 0x060008B0 RID: 2224 RVA: 0x00016944 File Offset: 0x00014B44
		public CopySessionResp CopySession(CopySessionReq Req)
		{
			return base.Channel.CopySession(Req);
		}

		// Token: 0x060008B1 RID: 2225 RVA: 0x00016964 File Offset: 0x00014B64
		public LoadAcademicTermsResp LoadAcademicTerms(LoadAcademicTermsReq Req)
		{
			return base.Channel.LoadAcademicTerms(Req);
		}

		// Token: 0x060008B2 RID: 2226 RVA: 0x00016984 File Offset: 0x00014B84
		public GetCurrentSessionResp GetCurrentSession(GetCurrentSessionReq Req)
		{
			return base.Channel.GetCurrentSession(Req);
		}

		// Token: 0x060008B3 RID: 2227 RVA: 0x000169A4 File Offset: 0x00014BA4
		public GetSessionByDateResp GetSessionByDate(GetSessionByDateReq request)
		{
			return base.Channel.GetSessionByDate(request);
		}

		// Token: 0x060008B4 RID: 2228 RVA: 0x000169C2 File Offset: 0x00014BC2
		public void SetSessionChooserDefaultValue(SetSessionChooserDefaultValueReq Request)
		{
			base.Channel.SetSessionChooserDefaultValue(Request);
		}

		// Token: 0x060008B5 RID: 2229 RVA: 0x000169D4 File Offset: 0x00014BD4
		public GetSessionChooserDefaultValueResp GetSessionChooserDefaultValue()
		{
			return base.Channel.GetSessionChooserDefaultValue();
		}
	}
}
