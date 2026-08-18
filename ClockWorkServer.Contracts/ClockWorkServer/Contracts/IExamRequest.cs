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
	// Token: 0x02000019 RID: 25
	[ServiceContract(Name = "ExamRequestService", Namespace = "http://tpro.ca")]
	[SoapHeaders]
	[XmlComments]
	public interface IExamRequest : IService
	{
		// Token: 0x060000F7 RID: 247
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadRequestsByDateRangeResp LoadRequestsByDateRange(LoadRequestsByDateRangeReq Request);

		// Token: 0x060000F8 RID: 248
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		CreateExamRequestResp CreateExamRequest(CreateExamRequestReq Request);

		// Token: 0x060000F9 RID: 249
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		void DeleteExamRequest(DeleteExamRequestReq Request);

		// Token: 0x060000FA RID: 250
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadRequestsByCourseResp LoadRequestsByCourse(LoadRequestsByCourseReq Request);

		// Token: 0x060000FB RID: 251
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadStudentsRegisteredInCourseWithStudentListWhoSubmittedExamRequestsResp LoadStudentsRegisteredInCourseWithStudentListWhoSubmittedExamRequests(LoadStudentsRegisteredInCourseWithStudentListWhoSubmittedExamRequestsReq Request);
	}
}
