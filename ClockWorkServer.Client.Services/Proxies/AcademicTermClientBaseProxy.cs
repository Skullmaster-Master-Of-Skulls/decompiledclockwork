using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x020000D0 RID: 208
	internal class AcademicTermClientBaseProxy : ClientBase<IAcademicTerm>, IAcademicTerm, IService
	{
		// Token: 0x0600080E RID: 2062 RVA: 0x000151E8 File Offset: 0x000133E8
		public AcademicTermClientBaseProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x0600080F RID: 2063 RVA: 0x000151F3 File Offset: 0x000133F3
		public AcademicTermClientBaseProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x06000810 RID: 2064 RVA: 0x00015200 File Offset: 0x00013400
		public GetCurrentAcademicTermResp GetCurrentAcademicTerm(GetCurrentAcademicTermReq request)
		{
			return base.Channel.GetCurrentAcademicTerm(request);
		}

		// Token: 0x06000811 RID: 2065 RVA: 0x00015220 File Offset: 0x00013420
		public LoadAcademicTermsResp LoadAcademicTerms(LoadAcademicTermsReq request)
		{
			return base.Channel.LoadAcademicTerms(request);
		}

		// Token: 0x06000812 RID: 2066 RVA: 0x00015240 File Offset: 0x00013440
		public GetAcademicTermResp GetAcademicTerm(GetAcademicTermReq request)
		{
			return base.Channel.GetAcademicTerm(request);
		}

		// Token: 0x06000813 RID: 2067 RVA: 0x00015260 File Offset: 0x00013460
		public ChangeCurrentAcademicTermsResp ChangeCurrentAcademicTerms(ChangeCurrentAcademicTermsReq request)
		{
			return base.Channel.ChangeCurrentAcademicTerms(request);
		}

		// Token: 0x06000814 RID: 2068 RVA: 0x00015280 File Offset: 0x00013480
		public ValidateAcademicTermListResp ValidateAcademicTermList(ValidateAcademicTermListReq request)
		{
			return base.Channel.ValidateAcademicTermList(request);
		}
	}
}
