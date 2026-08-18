using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x020000CF RID: 207
	public class AcademicTermReusableClientProxy : WCFTokenBasedReusableClientProxy<IAcademicTerm>, IAcademicTerm, IService
	{
		// Token: 0x06000807 RID: 2055 RVA: 0x000150B6 File Offset: 0x000132B6
		public AcademicTermReusableClientProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x06000808 RID: 2056 RVA: 0x000150C1 File Offset: 0x000132C1
		public AcademicTermReusableClientProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x06000809 RID: 2057 RVA: 0x000150D0 File Offset: 0x000132D0
		public GetCurrentAcademicTermResp GetCurrentAcademicTerm(GetCurrentAcademicTermReq request)
		{
			return this.WrapServiceMethod<GetCurrentAcademicTermResp>(() => this.Proxy.GetCurrentAcademicTerm(request));
		}

		// Token: 0x0600080A RID: 2058 RVA: 0x00015108 File Offset: 0x00013308
		public LoadAcademicTermsResp LoadAcademicTerms(LoadAcademicTermsReq request)
		{
			return this.WrapServiceMethod<LoadAcademicTermsResp>(() => this.Proxy.LoadAcademicTerms(request));
		}

		// Token: 0x0600080B RID: 2059 RVA: 0x00015140 File Offset: 0x00013340
		public GetAcademicTermResp GetAcademicTerm(GetAcademicTermReq request)
		{
			return this.WrapServiceMethod<GetAcademicTermResp>(() => this.Proxy.GetAcademicTerm(request));
		}

		// Token: 0x0600080C RID: 2060 RVA: 0x00015178 File Offset: 0x00013378
		public ChangeCurrentAcademicTermsResp ChangeCurrentAcademicTerms(ChangeCurrentAcademicTermsReq request)
		{
			return this.WrapServiceMethod<ChangeCurrentAcademicTermsResp>(() => this.Proxy.ChangeCurrentAcademicTerms(request));
		}

		// Token: 0x0600080D RID: 2061 RVA: 0x000151B0 File Offset: 0x000133B0
		public ValidateAcademicTermListResp ValidateAcademicTermList(ValidateAcademicTermListReq request)
		{
			return this.WrapServiceMethod<ValidateAcademicTermListResp>(() => this.Proxy.ValidateAcademicTermList(request));
		}
	}
}
