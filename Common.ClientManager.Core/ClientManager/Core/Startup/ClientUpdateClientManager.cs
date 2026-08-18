using System;
using TechnoPro.ClockWorkServer.Client.ServiceFactory;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.Updates;
using TechnoPro.Common.ClientManager.ICore;
using TechnoPro.Common.ClientManager.ICore.Startup;
using TechnoPro.Common.Public;
using TechnoPro.Common.Unity.IoC;

namespace TechnoPro.Common.ClientManager.Core.Startup
{
	// Token: 0x0200001A RID: 26
	public class ClientUpdateClientManager : IClientUpdateClientManager, IWebService
	{
		// Token: 0x060000C8 RID: 200 RVA: 0x00005108 File Offset: 0x00003308
		public GetClientUpdateResp GetClientUpdate(GetClientUpdateReq req)
		{
			ObjectFactory.Resolve<IRequestBuilderClientManager>().UpdateRequest<GetClientUpdateReq>(req);
			IClientUpdate clientInstance = ClientServiceFactory.GetClientInstance<IClientUpdate>(true, true);
			GetClientUpdateResp result;
			if (clientInstance == null)
			{
				(result = new GetClientUpdateResp()).File = null;
			}
			else
			{
				result = clientInstance.GetClientUpdate(req);
			}
			return result;
		}
	}
}
