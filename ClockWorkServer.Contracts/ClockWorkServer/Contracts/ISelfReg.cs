using System;
using System.ServiceModel;
using TechnoPro.ClockWorkServer.Contracts.DTO.StudentAccommodationRequests;
using TechnoPro.ClockWorkServer.Contracts.Faults;
using TechnoPro.Common.Public;
using TechnoPro.Common.WCF.Faults;
using WCFExtrasPlus.Soap;
using WCFExtrasPlus.Wsdl.Documentation;

namespace TechnoPro.ClockWorkServer.Contracts
{
	// Token: 0x0200008C RID: 140
	[ServiceContract(Name = "SelfRegService", Namespace = "http://tpro.ca")]
	[SoapHeaders]
	[XmlComments]
	public interface ISelfReg : IService
	{
		// Token: 0x060003D3 RID: 979
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		ProcessSelfRegRequestResp ProcessSelfRegRequest(ProcessSelfRegRequestReq Request);

		// Token: 0x060003D4 RID: 980
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		GetCoursesAllowedBySelfRegCustomLogicRulesToViewLoaForResp GetCoursesAllowedBySelfRegCustomLogicRulesToViewLoaFor(GetCoursesAllowedBySelfRegCustomLogicRulesToViewLoaForReq Request);
	}
}
