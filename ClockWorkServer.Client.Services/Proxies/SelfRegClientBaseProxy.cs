using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.StudentAccommodationRequests;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x02000140 RID: 320
	internal class SelfRegClientBaseProxy : ClientBase<ISelfReg>, ISelfReg, IService
	{
		// Token: 0x06000C50 RID: 3152 RVA: 0x0001EC44 File Offset: 0x0001CE44
		public SelfRegClientBaseProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x06000C51 RID: 3153 RVA: 0x0001EC4F File Offset: 0x0001CE4F
		public SelfRegClientBaseProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x06000C52 RID: 3154 RVA: 0x0001EC5C File Offset: 0x0001CE5C
		public ProcessSelfRegRequestResp ProcessSelfRegRequest(ProcessSelfRegRequestReq Request)
		{
			return base.Channel.ProcessSelfRegRequest(Request);
		}

		// Token: 0x06000C53 RID: 3155 RVA: 0x0001EC7C File Offset: 0x0001CE7C
		public GetCoursesAllowedBySelfRegCustomLogicRulesToViewLoaForResp GetCoursesAllowedBySelfRegCustomLogicRulesToViewLoaFor(GetCoursesAllowedBySelfRegCustomLogicRulesToViewLoaForReq Request)
		{
			return base.Channel.GetCoursesAllowedBySelfRegCustomLogicRulesToViewLoaFor(Request);
		}
	}
}
