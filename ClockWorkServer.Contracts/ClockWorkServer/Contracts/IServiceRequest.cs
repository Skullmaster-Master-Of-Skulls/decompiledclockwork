using System;
using System.ServiceModel;
using TechnoPro.ClockWorkServer.Contracts.DTO.ServiceProvider.ContractParameters;
using TechnoPro.ClockWorkServer.Contracts.Faults;
using TechnoPro.Common.Public;
using TechnoPro.Common.WCF.Faults;
using WCFExtrasPlus.Soap;
using WCFExtrasPlus.Wsdl.Documentation;

namespace TechnoPro.ClockWorkServer.Contracts
{
	// Token: 0x02000082 RID: 130
	[ServiceContract(Name = "ServiceRequestService", Namespace = "http://tpro.ca")]
	[SoapHeaders]
	[XmlComments]
	public interface IServiceRequest : IService
	{
		// Token: 0x0600039C RID: 924
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadRequestByIdResp LoadRequestById(LoadRequestByIdReq Request);

		// Token: 0x0600039D RID: 925
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadRequestByStudentAndProviderTypeResp LoadRequestByStudentAndProviderType(LoadRequestByStudentAndProviderTypeReq Request);

		// Token: 0x0600039E RID: 926
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadRequestsResp LoadRequests(LoadRequestsReq Request);

		// Token: 0x0600039F RID: 927
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		CreateRequestResp CreateRequest(CreateRequestReq Request);

		// Token: 0x060003A0 RID: 928
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		UpdateRequestResp UpdateRequest(UpdateRequestReq Request);

		// Token: 0x060003A1 RID: 929
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		DeleteRequestResp DeleteRequest(DeleteRequestReq Request);

		// Token: 0x060003A2 RID: 930
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		CreateRequestCourseResp CreateRequestCourse(CreateRequestCourseReq Request);

		// Token: 0x060003A3 RID: 931
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		DeleteRequestCourseResp DeleteRequestCourse(DeleteRequestCourseReq Request);

		// Token: 0x060003A4 RID: 932
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		UpdateRequestCourseResp UpdateRequestCourse(UpdateRequestCourseReq Request);

		// Token: 0x060003A5 RID: 933
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		CreateRequestEventResp CreateRequestEvent(CreateRequestEventReq Request);

		// Token: 0x060003A6 RID: 934
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		DeleteRequestEventResp DeleteRequestEvent(DeleteRequestEventReq Request);

		// Token: 0x060003A7 RID: 935
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		UpdateRequestEventResp UpdateRequestEvent(UpdateRequestEventReq Request);

		// Token: 0x060003A8 RID: 936
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		AssignOrUnassignRequestCourseResp AssignOrUnassignRequestCourse(AssignOrUnassignRequestCourseReq Request);

		// Token: 0x060003A9 RID: 937
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		AssignOrUnassignRequestEventResp AssignOrUnassignRequestEvent(AssignOrUnassignRequestEventReq Request);

		// Token: 0x060003AA RID: 938
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		MergeDuplicateRequestsForTwoStudentsResp MergeDuplicateRequestsForTwoStudents(MergeDuplicateRequestsForTwoStudentsReq Request);
	}
}
