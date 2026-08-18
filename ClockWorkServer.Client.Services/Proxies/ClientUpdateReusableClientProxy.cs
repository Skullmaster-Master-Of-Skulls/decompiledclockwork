using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.Updates;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x02000139 RID: 313
	public class ClientUpdateReusableClientProxy : WCFReusableClientProxy<IClientUpdate>, IClientUpdate, IService
	{
		// Token: 0x06000C3A RID: 3130 RVA: 0x0001EA22 File Offset: 0x0001CC22
		public ClientUpdateReusableClientProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x06000C3B RID: 3131 RVA: 0x0001EA2D File Offset: 0x0001CC2D
		public ClientUpdateReusableClientProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x06000C3C RID: 3132 RVA: 0x0001EA3C File Offset: 0x0001CC3C
		public GetClientUpdateResp GetClientUpdate(GetClientUpdateReq updateReq)
		{
			return this.WrapServiceMethod<GetClientUpdateResp>(() => this.Proxy.GetClientUpdate(updateReq));
		}
	}
}
