using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.ClockWorkAudit;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x02000060 RID: 96
	internal class ClockWorkAuditClientBaseProxy : ClientBase<IClockWorkAudit>, IClockWorkAudit, IService
	{
		// Token: 0x06000444 RID: 1092 RVA: 0x0000C400 File Offset: 0x0000A600
		public ClockWorkAuditClientBaseProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x06000445 RID: 1093 RVA: 0x0000C40B File Offset: 0x0000A60B
		public ClockWorkAuditClientBaseProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x06000446 RID: 1094 RVA: 0x0000C418 File Offset: 0x0000A618
		public ExecuteAuditResp ExecuteAudit(ExecuteAuditReq Request)
		{
			return base.Channel.ExecuteAudit(Request);
		}
	}
}
