using System;
using System.ServiceModel;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking.Parameters;
using TechnoPro.ClockWorkServer.Contracts.Faults;
using TechnoPro.Common.Public;
using TechnoPro.Common.WCF.Faults;
using WCFExtrasPlus.Soap;
using WCFExtrasPlus.Wsdl.Documentation;

namespace TechnoPro.ClockWorkServer.Contracts
{
	// Token: 0x0200001D RID: 29
	[ServiceContract(Name = "TestExamBrowserService", Namespace = "http://tpro.ca")]
	[SoapHeaders]
	[XmlComments]
	public interface ITestExamBrowser : IService
	{
		// Token: 0x06000119 RID: 281
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadTestExamRowsResp LoadTestExamRows(LoadTestExamRowsReq Request);

		// Token: 0x0600011A RID: 282
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadTestExamRowResp LoadTestExamRow(LoadTestExamRowReq Request);
	}
}
