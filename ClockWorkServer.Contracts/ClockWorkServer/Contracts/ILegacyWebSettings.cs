using System;
using System.ServiceModel;
using TechnoPro.ClockWorkServer.Contracts.DTO.Legacy.WebSettings;
using TechnoPro.ClockWorkServer.Contracts.Faults;
using TechnoPro.Common.Public;
using TechnoPro.Common.WCF.Faults;
using WCFExtrasPlus.Soap;
using WCFExtrasPlus.Wsdl.Documentation;

namespace TechnoPro.ClockWorkServer.Contracts
{
	// Token: 0x02000062 RID: 98
	[ServiceContract(Name = "LegacyWebSettingsService", Namespace = "http://tpro.ca")]
	[SoapHeaders]
	[XmlComments]
	public interface ILegacyWebSettings : IService
	{
		// Token: 0x060002DF RID: 735
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		GetWebSettingValueResp GetWebSettingValue(GetWebSettingValueReq Request);
	}
}
