using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Threading.Tasks;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.ClockWorkServerConnection;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x02000066 RID: 102
	internal class ClockWorkServerDiscoveryClientBaseProxy : ClientBase<IClockWorkServerDiscovery>, IClockWorkServerDiscovery, IService
	{
		// Token: 0x06000465 RID: 1125 RVA: 0x0000C7E8 File Offset: 0x0000A9E8
		public ClockWorkServerDiscoveryClientBaseProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x06000466 RID: 1126 RVA: 0x0000C7F3 File Offset: 0x0000A9F3
		public ClockWorkServerDiscoveryClientBaseProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x06000467 RID: 1127 RVA: 0x0000C800 File Offset: 0x0000AA00
		public GetClockWorkServerConnectionInfoResp GetClockWorkServerConnectionInfo(GetClockWorkServerConnectionInfoReq request)
		{
			return base.Channel.GetClockWorkServerConnectionInfo(request);
		}

		// Token: 0x06000468 RID: 1128 RVA: 0x0000C820 File Offset: 0x0000AA20
		public GetClockWorkServerInfoResp GetClockWorkServerInfo(GetClockWorkServerInfoReq request)
		{
			return base.Channel.GetClockWorkServerInfo(request);
		}

		// Token: 0x06000469 RID: 1129 RVA: 0x0000C840 File Offset: 0x0000AA40
		public Task<GetClockWorkServerInfoResp> GetClockWorkServerInfoAsync(GetClockWorkServerInfoReq request)
		{
			return base.Channel.GetClockWorkServerInfoAsync(request);
		}
	}
}
