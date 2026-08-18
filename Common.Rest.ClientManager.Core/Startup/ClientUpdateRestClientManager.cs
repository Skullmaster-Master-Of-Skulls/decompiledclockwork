using System;
using TechnoPro.ClockWorkServer.Contracts.DTO.Updates;
using TechnoPro.Common.ClientManager.ICore.Startup;
using TechnoPro.Common.Public;
using TechnoPro.Common.Web.Security.Proxy;

namespace TechnoPro.Common.Rest.ClientManager.Core.Startup
{
	// Token: 0x02000014 RID: 20
	public class ClientUpdateRestClientManager : AnonymousRestProxy<IClientUpdateClientManager>, IClientUpdateClientManager, IWebService
	{
		// Token: 0x060000A0 RID: 160 RVA: 0x000038F4 File Offset: 0x00001AF4
		public ClientUpdateRestClientManager(string serviceAddress, string clientId) : base(serviceAddress, clientId)
		{
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x000038FE File Offset: 0x00001AFE
		public ClientUpdateRestClientManager(string serviceAddress, string serviceAddressSuffix, string clientId) : base(serviceAddress, serviceAddressSuffix, clientId)
		{
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x00003909 File Offset: 0x00001B09
		public GetClientUpdateResp GetClientUpdate(GetClientUpdateReq req)
		{
			return base.Post<GetClientUpdateReq, GetClientUpdateResp>(req, "clientupdate/getupdate");
		}
	}
}
