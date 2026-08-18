using System;
using System.ServiceModel;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestExamViews;
using TechnoPro.ClockWorkServer.Contracts.Faults;
using TechnoPro.Common.Public;
using TechnoPro.Common.WCF.Faults;
using WCFExtrasPlus.Soap;
using WCFExtrasPlus.Wsdl.Documentation;

namespace TechnoPro.ClockWorkServer.Contracts
{
	// Token: 0x02000020 RID: 32
	[ServiceContract(Name = "FinalExamsViewService", Namespace = "http://tpro.ca")]
	[SoapHeaders]
	[XmlComments]
	public interface IFinalExamsView : IService
	{
		// Token: 0x06000127 RID: 295
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadFinalExamsLightResp LoadFinalExamsLight(LoadFinalExamsLightReq Request);

		// Token: 0x06000128 RID: 296
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadUnbookedFinalExamsResp LoadUnbookedFinalExams(LoadUnbookedFinalExamsReq Request);
	}
}
