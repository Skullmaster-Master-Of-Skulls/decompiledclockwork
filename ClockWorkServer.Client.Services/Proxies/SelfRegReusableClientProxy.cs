using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.StudentAccommodationRequests;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x0200013F RID: 319
	public class SelfRegReusableClientProxy : WCFTokenBasedReusableClientProxy<ISelfReg>, ISelfReg, IService
	{
		// Token: 0x06000C4C RID: 3148 RVA: 0x0001EBBA File Offset: 0x0001CDBA
		public SelfRegReusableClientProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x06000C4D RID: 3149 RVA: 0x0001EBC5 File Offset: 0x0001CDC5
		public SelfRegReusableClientProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x06000C4E RID: 3150 RVA: 0x0001EBD4 File Offset: 0x0001CDD4
		public ProcessSelfRegRequestResp ProcessSelfRegRequest(ProcessSelfRegRequestReq Request)
		{
			return this.WrapServiceMethod<ProcessSelfRegRequestResp>(() => this.Proxy.ProcessSelfRegRequest(Request));
		}

		// Token: 0x06000C4F RID: 3151 RVA: 0x0001EC0C File Offset: 0x0001CE0C
		public GetCoursesAllowedBySelfRegCustomLogicRulesToViewLoaForResp GetCoursesAllowedBySelfRegCustomLogicRulesToViewLoaFor(GetCoursesAllowedBySelfRegCustomLogicRulesToViewLoaForReq Request)
		{
			return this.WrapServiceMethod<GetCoursesAllowedBySelfRegCustomLogicRulesToViewLoaForResp>(() => this.Proxy.GetCoursesAllowedBySelfRegCustomLogicRulesToViewLoaFor(Request));
		}
	}
}
