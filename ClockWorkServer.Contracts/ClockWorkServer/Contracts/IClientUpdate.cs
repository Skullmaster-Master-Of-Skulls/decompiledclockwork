using System;
using System.ServiceModel;
using TechnoPro.ClockWorkServer.Contracts.DTO;
using TechnoPro.ClockWorkServer.Contracts.DTO.Updates;
using TechnoPro.ClockWorkServer.Contracts.Faults;
using TechnoPro.Common.Public;
using TechnoPro.Common.WCF.Attributes;
using TechnoPro.Common.WCF.Faults;
using WCFExtrasPlus.Soap;
using WCFExtrasPlus.Wsdl.Documentation;

namespace TechnoPro.ClockWorkServer.Contracts
{
	// Token: 0x02000087 RID: 135
	[ServiceContract(Name = "ClientUpdateService", Namespace = "http://tpro.ca")]
	[SoapHeaders]
	[XmlComments]
	[XtraSizeService]
	public interface IClientUpdate : IService
	{
		// Token: 0x060003BC RID: 956
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidHashCredentialsFault))]
		[SoapHeader("clientDetails", typeof(ClientParametersDTO), Direction = SoapHeaderDirection.In)]
		GetClientUpdateResp GetClientUpdate(GetClientUpdateReq updateReq);
	}
}
