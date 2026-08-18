using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.RequiredSessionForm;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x0200011E RID: 286
	internal class RequiredSessionFormClientBaseProxy : ClientBase<IRequiredSessionForm>, IRequiredSessionForm, IService
	{
		// Token: 0x06000B83 RID: 2947 RVA: 0x0001D1F4 File Offset: 0x0001B3F4
		public RequiredSessionFormClientBaseProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x06000B84 RID: 2948 RVA: 0x0001D1FF File Offset: 0x0001B3FF
		public RequiredSessionFormClientBaseProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x06000B85 RID: 2949 RVA: 0x0001D20C File Offset: 0x0001B40C
		public LoadInfoPmIdForCurrentSessionResp LoadInfoPmIdForCurrentSession(LoadInfoPmIdForCurrentSessionReq Request)
		{
			return base.Channel.LoadInfoPmIdForCurrentSession(Request);
		}

		// Token: 0x06000B86 RID: 2950 RVA: 0x0001D22C File Offset: 0x0001B42C
		public LoadInfoPmIdForSessionResp LoadInfoPmIdForSession(LoadInfoPmIdForSessionReq Request)
		{
			return base.Channel.LoadInfoPmIdForSession(Request);
		}
	}
}
