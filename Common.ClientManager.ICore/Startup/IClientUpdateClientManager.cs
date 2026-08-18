using System;
using TechnoPro.ClockWorkServer.Contracts.DTO.Updates;
using TechnoPro.Common.Public;

namespace TechnoPro.Common.ClientManager.ICore.Startup
{
	// Token: 0x02000017 RID: 23
	public interface IClientUpdateClientManager : IWebService
	{
		// Token: 0x0600008D RID: 141
		GetClientUpdateResp GetClientUpdate(GetClientUpdateReq req);
	}
}
