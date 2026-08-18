using System;
using System.ServiceModel;
using TechnoPro.ClockWorkServer.Contracts.DTO.Veteran;
using TechnoPro.ClockWorkServer.Contracts.Faults;
using TechnoPro.Common.Public;
using TechnoPro.Common.WCF.Faults;
using WCFExtrasPlus.Soap;
using WCFExtrasPlus.Wsdl.Documentation;

namespace TechnoPro.ClockWorkServer.Contracts
{
	// Token: 0x020000A5 RID: 165
	[ServiceContract(Name = "VeteranService", Namespace = "http://tpro.ca")]
	[SoapHeaders]
	[XmlComments]
	public interface IVeteran : IService
	{
		// Token: 0x060004D8 RID: 1240
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadChangeInBenefitRequestsResp LoadChangeInBenefitRequests(LoadChangeInBenefitRequestsReq Request);
	}
}
