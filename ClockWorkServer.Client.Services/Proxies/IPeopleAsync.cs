using System;
using System.ServiceModel;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;
using TechnoPro.Common.Public;
using WCFExtrasPlus.Soap;
using WCFExtrasPlus.Wsdl.Documentation;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x02000107 RID: 263
	[ServiceContract(Name = "PeopleService", Namespace = "http://tpro.ca")]
	[SoapHeaders]
	[XmlComments]
	public interface IPeopleAsync : IPeople, IService
	{
		// Token: 0x06000A2B RID: 2603
		[OperationContract(AsyncPattern = true)]
		IAsyncResult BeginFindUserGroupObjectBySearchString(FindUserGroupObjectBySearchStringReq req, AsyncCallback callback, object asyncState);

		// Token: 0x06000A2C RID: 2604
		FindUserGroupObjectBySearchStringResp EndFindUserGroupObjectBySearchString(IAsyncResult result);

		// Token: 0x06000A2D RID: 2605
		void Close();
	}
}
