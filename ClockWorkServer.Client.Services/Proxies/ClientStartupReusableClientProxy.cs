using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.Startup;
using TechnoPro.ClockWorkServer.Contracts.DTO.Updates;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x02000137 RID: 311
	public class ClientStartupReusableClientProxy : WCFReusableClientProxy<IClientStartup>, IClientStartup, IService, IConnectivity
	{
		// Token: 0x06000C2F RID: 3119 RVA: 0x0001E8F2 File Offset: 0x0001CAF2
		public ClientStartupReusableClientProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x06000C30 RID: 3120 RVA: 0x0001E8FD File Offset: 0x0001CAFD
		public ClientStartupReusableClientProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x06000C31 RID: 3121 RVA: 0x0001E90C File Offset: 0x0001CB0C
		public UpdateRequiredResponse IsUpdateRequired(UpdateRequiredRequest request)
		{
			return this.WrapServiceMethod<UpdateRequiredResponse>(() => this.Proxy.IsUpdateRequired(request));
		}

		// Token: 0x06000C32 RID: 3122 RVA: 0x0001E944 File Offset: 0x0001CB44
		public GetClockWorkServerCertificateResp GetClockWorkServerCertificate(GetClockWorkServerCertificateReq request)
		{
			return this.WrapServiceMethod<GetClockWorkServerCertificateResp>(() => this.Proxy.GetClockWorkServerCertificate(request));
		}

		// Token: 0x06000C33 RID: 3123 RVA: 0x0001E97C File Offset: 0x0001CB7C
		public override int CheckConnectivity()
		{
			return this.WrapServiceMethod<int>(() => base.Proxy.CheckConnectivity());
		}
	}
}
