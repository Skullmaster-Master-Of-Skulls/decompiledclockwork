using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.ClockWorkAudit;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x0200005F RID: 95
	public class ClockWorkAuditReusableClientProxy : WCFTokenBasedReusableClientProxy<IClockWorkAudit>, IClockWorkAudit, IService
	{
		// Token: 0x06000441 RID: 1089 RVA: 0x0000C3AE File Offset: 0x0000A5AE
		public ClockWorkAuditReusableClientProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x06000442 RID: 1090 RVA: 0x0000C3B9 File Offset: 0x0000A5B9
		public ClockWorkAuditReusableClientProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x06000443 RID: 1091 RVA: 0x0000C3C8 File Offset: 0x0000A5C8
		public ExecuteAuditResp ExecuteAudit(ExecuteAuditReq Request)
		{
			return this.WrapServiceMethod<ExecuteAuditResp>(() => this.Proxy.ExecuteAudit(Request));
		}
	}
}
