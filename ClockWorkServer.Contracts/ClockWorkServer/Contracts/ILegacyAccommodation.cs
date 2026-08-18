using System;
using System.ServiceModel;
using TechnoPro.ClockWorkServer.Contracts.DTO.Legacy.Accommodation;
using TechnoPro.ClockWorkServer.Contracts.Faults;
using TechnoPro.Common.Public;
using TechnoPro.Common.WCF.Faults;
using WCFExtrasPlus.Soap;
using WCFExtrasPlus.Wsdl.Documentation;

namespace TechnoPro.ClockWorkServer.Contracts
{
	// Token: 0x0200005D RID: 93
	[ServiceContract(Name = "LegacyAccommodationService", Namespace = "http://tpro.ca")]
	[SoapHeaders]
	[XmlComments]
	public interface ILegacyAccommodation : IService
	{
		// Token: 0x060002D2 RID: 722
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LogLoaIssuedDateResp LogLoaIssuedDate(LogLoaIssuedDateReq Request);
	}
}
