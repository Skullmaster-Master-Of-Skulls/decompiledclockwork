using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.RequiredSessionForm;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x0200011D RID: 285
	public class RequiredSessionFormReusableClientProxy : WCFTokenBasedReusableClientProxy<IRequiredSessionForm>, IRequiredSessionForm, IService
	{
		// Token: 0x06000B7F RID: 2943 RVA: 0x0001D16A File Offset: 0x0001B36A
		public RequiredSessionFormReusableClientProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x06000B80 RID: 2944 RVA: 0x0001D175 File Offset: 0x0001B375
		public RequiredSessionFormReusableClientProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x06000B81 RID: 2945 RVA: 0x0001D184 File Offset: 0x0001B384
		public LoadInfoPmIdForCurrentSessionResp LoadInfoPmIdForCurrentSession(LoadInfoPmIdForCurrentSessionReq Request)
		{
			return this.WrapServiceMethod<LoadInfoPmIdForCurrentSessionResp>(() => this.Proxy.LoadInfoPmIdForCurrentSession(Request));
		}

		// Token: 0x06000B82 RID: 2946 RVA: 0x0001D1BC File Offset: 0x0001B3BC
		public LoadInfoPmIdForSessionResp LoadInfoPmIdForSession(LoadInfoPmIdForSessionReq Request)
		{
			return this.WrapServiceMethod<LoadInfoPmIdForSessionResp>(() => this.Proxy.LoadInfoPmIdForSession(Request));
		}
	}
}
