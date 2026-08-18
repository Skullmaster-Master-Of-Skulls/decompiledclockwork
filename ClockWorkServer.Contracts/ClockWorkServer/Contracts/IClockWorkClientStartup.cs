using System;
using System.ServiceModel;
using TechnoPro.ClockWorkServer.Contracts.DTO.Startup;
using TechnoPro.ClockWorkServer.Contracts.Faults;
using TechnoPro.Common.Public;
using TechnoPro.Common.WCF.Faults;
using WCFExtrasPlus.Soap;
using WCFExtrasPlus.Wsdl.Documentation;

namespace TechnoPro.ClockWorkServer.Contracts
{
	// Token: 0x02000088 RID: 136
	[ServiceContract(Name = "ClockWorkClientStartupService", Namespace = "http://tpro.ca")]
	[SoapHeaders]
	[XmlComments]
	public interface IClockWorkClientStartup : IService
	{
		// Token: 0x060003BD RID: 957
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		GetClockWorkClientStartupResp GetClockWorkClientStartup(GetClockWorkClientStartupReq Request);
	}
}
