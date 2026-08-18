using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.Updates;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x0200013A RID: 314
	internal class ClientUpdateClientBaseProxy : ClientBase<IClientUpdate>, IClientUpdate, IService
	{
		// Token: 0x06000C3D RID: 3133 RVA: 0x0001EA74 File Offset: 0x0001CC74
		public ClientUpdateClientBaseProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x06000C3E RID: 3134 RVA: 0x0001EA7F File Offset: 0x0001CC7F
		public ClientUpdateClientBaseProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x06000C3F RID: 3135 RVA: 0x0001EA8C File Offset: 0x0001CC8C
		public GetClientUpdateResp GetClientUpdate(GetClientUpdateReq updateReq)
		{
			return base.Channel.GetClientUpdate(updateReq);
		}
	}
}
