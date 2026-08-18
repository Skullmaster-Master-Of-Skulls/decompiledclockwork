using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.Startup;
using TechnoPro.ClockWorkServer.Contracts.DTO.Updates;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x02000138 RID: 312
	internal class ClientStartupClientBaseProxy : ClientBase<IClientStartup>, IClientStartup, IService, IConnectivity
	{
		// Token: 0x06000C35 RID: 3125 RVA: 0x0001E9AD File Offset: 0x0001CBAD
		public ClientStartupClientBaseProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x06000C36 RID: 3126 RVA: 0x0001E9B8 File Offset: 0x0001CBB8
		public ClientStartupClientBaseProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x06000C37 RID: 3127 RVA: 0x0001E9C4 File Offset: 0x0001CBC4
		public int CheckConnectivity()
		{
			return base.Channel.CheckConnectivity();
		}

		// Token: 0x06000C38 RID: 3128 RVA: 0x0001E9E4 File Offset: 0x0001CBE4
		public UpdateRequiredResponse IsUpdateRequired(UpdateRequiredRequest request)
		{
			return base.Channel.IsUpdateRequired(request);
		}

		// Token: 0x06000C39 RID: 3129 RVA: 0x0001EA04 File Offset: 0x0001CC04
		public GetClockWorkServerCertificateResp GetClockWorkServerCertificate(GetClockWorkServerCertificateReq request)
		{
			return base.Channel.GetClockWorkServerCertificate(request);
		}
	}
}
