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
	// Token: 0x0200008D RID: 141
	[ServiceContract(Name = "StudentAccommodationReqService", Namespace = "http://tpro.ca")]
	[SoapHeaders]
	[XmlComments]
	public interface IStudentAccommodationReq : IService
	{
		// Token: 0x060003D5 RID: 981
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadCourseRegistrationsWithRequestByStudentAndDateResp LoadCourseRegistrationsWithRequestByStudentAndDate(LoadCourseRegistrationsWithRequestByStudentAndDateReq Request);

		// Token: 0x060003D6 RID: 982
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		AddRequestResp AddRequest(AddRequestReq Request);

		// Token: 0x060003D7 RID: 983
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadRequestsByStudentAndDateResp LoadRequestsByStudentAndDate(LoadRequestsByStudentAndDateReq Request);

		// Token: 0x060003D8 RID: 984
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadRequestByIdResp LoadRequestById(LoadRequestByIdReq Request);

		// Token: 0x060003D9 RID: 985
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		void DeleteRequest(DeleteRequestReq Request);

		// Token: 0x060003DA RID: 986
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		void UpdateRequest(UpdateRequestReq Request);

		// Token: 0x060003DB RID: 987
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadCourseRegistrationsWithRequestByStatusResp LoadCourseRegistrationsWithRequestByStatus(LoadCourseRegistrationsWithRequestByStatusReq Request);

		// Token: 0x060003DC RID: 988
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		void UpdateRequestStatus(UpdateRequestStatusReq Request);

		// Token: 0x060003DD RID: 989
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadStudentCourseAccommodationRequestHistoryResp LoadStudentCourseAccommodationRequestHistory(LoadStudentCourseAccommodationRequestHistoryReq Request);
	}
}
