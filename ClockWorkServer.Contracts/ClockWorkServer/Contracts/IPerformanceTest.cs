using System;
using System.ServiceModel;
using TechnoPro.ClockWorkServer.Contracts.DTO.PerformanceTesting;
using TechnoPro.ClockWorkServer.Contracts.Faults;
using TechnoPro.Common.Public;
using TechnoPro.Common.WCF.Faults;
using WCFExtrasPlus.Soap;
using WCFExtrasPlus.Wsdl.Documentation;

namespace TechnoPro.ClockWorkServer.Contracts
{
	// Token: 0x02000077 RID: 119
	[ServiceContract(Name = "PerformanceTestService", Namespace = "http://tpro.ca")]
	[SoapHeaders]
	[XmlComments]
	public interface IPerformanceTest : IService
	{
		// Token: 0x06000374 RID: 884
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		SearchForPersonPerformanceTestResp SearchForPersonPerformanceTest(SearchForPersonPerformanceTestReq Request);

		// Token: 0x06000375 RID: 885
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadAppointmentsPerformanceTestResp LoadAppointmentsPerformanceTest(LoadAppointmentsPerformanceTestReq Request);
	}
}
