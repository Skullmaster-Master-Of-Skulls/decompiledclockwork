using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Threading.Tasks;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.ClockWorkServerConnection;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x02000065 RID: 101
	public class ClockWorkServerDiscoveryReusableClientProxy : WCFReusableClientProxy<IClockWorkServerDiscovery>, IClockWorkServerDiscovery, IService
	{
		// Token: 0x06000460 RID: 1120 RVA: 0x0000C719 File Offset: 0x0000A919
		public ClockWorkServerDiscoveryReusableClientProxy(string endpoint) : base(endpoint)
		{
			base.IncludeProxyHeader = false;
		}

		// Token: 0x06000461 RID: 1121 RVA: 0x0000C72C File Offset: 0x0000A92C
		public ClockWorkServerDiscoveryReusableClientProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
			base.IncludeProxyHeader = false;
		}

		// Token: 0x06000462 RID: 1122 RVA: 0x0000C740 File Offset: 0x0000A940
		public GetClockWorkServerConnectionInfoResp GetClockWorkServerConnectionInfo(GetClockWorkServerConnectionInfoReq request)
		{
			return this.WrapServiceMethod<GetClockWorkServerConnectionInfoResp>(() => this.Proxy.GetClockWorkServerConnectionInfo(request));
		}

		// Token: 0x06000463 RID: 1123 RVA: 0x0000C778 File Offset: 0x0000A978
		public GetClockWorkServerInfoResp GetClockWorkServerInfo(GetClockWorkServerInfoReq request)
		{
			return this.WrapServiceMethod<GetClockWorkServerInfoResp>(() => this.Proxy.GetClockWorkServerInfo(request));
		}

		// Token: 0x06000464 RID: 1124 RVA: 0x0000C7B0 File Offset: 0x0000A9B0
		public Task<GetClockWorkServerInfoResp> GetClockWorkServerInfoAsync(GetClockWorkServerInfoReq request)
		{
			return this.WrapServiceMethod<Task<GetClockWorkServerInfoResp>>(() => this.Proxy.GetClockWorkServerInfoAsync(request));
		}
	}
}
