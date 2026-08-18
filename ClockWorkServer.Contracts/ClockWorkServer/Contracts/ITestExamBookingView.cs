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
	// Token: 0x0200001C RID: 28
	[ServiceContract(Name = "TestExamBookingViewService", Namespace = "http://tpro.ca")]
	[SoapHeaders]
	[XmlComments]
	public interface ITestExamBookingView : IService
	{
		// Token: 0x0600010B RID: 267
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadTestsFullResp LoadTestsFull(LoadTestsFullReq Request);

		// Token: 0x0600010C RID: 268
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadTestsSmallResp LoadTestsSmall(LoadTestsSmallReq Request);

		// Token: 0x0600010D RID: 269
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadClassTestDefinitionsSmallResp LoadClassTestDefinitionsSmall(LoadClassTestDefinitionsSmallReq Request);

		// Token: 0x0600010E RID: 270
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadUnbookedStudentsSmallResp LoadUnbookedStudentsSmall(LoadUnbookedStudentsSmallReq Request);

		// Token: 0x0600010F RID: 271
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadTestFullByAppIdResp LoadTestFullByAppId(LoadTestFullByAppIdReq request);

		// Token: 0x06000110 RID: 272
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadTestSmallByAppIdResp LoadTestSmallByAppId(LoadTestSmallByAppIdReq request);

		// Token: 0x06000111 RID: 273
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadClassTestDefinitionSmallByExamIdResp LoadClassTestDefinitionSmallByExamId(LoadClassTestDefinitionSmallByExamIdReq request);

		// Token: 0x06000112 RID: 274
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadTestsFullByExamIdResp LoadTestsFullByExamId(LoadTestsFullByExamIdReq Request);

		// Token: 0x06000113 RID: 275
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadTestsSmallByExamIdResp LoadTestsSmallByExamId(LoadTestsSmallByExamIdReq Request);

		// Token: 0x06000114 RID: 276
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadTestsFullByAppointmentIdsResp LoadTestsFullByAppointmentIds(LoadTestsFullByAppointmentIdsReq Request);

		// Token: 0x06000115 RID: 277
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadTestsSmallByAppointmentIdsResp LoadTestsSmallByAppointmentIds(LoadTestsSmallByAppointmentIdsReq Request);

		// Token: 0x06000116 RID: 278
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		void SaveTestExamBookingLayoutToCentralizedSetting(SaveTestExamBookingLayoutToCentralizedSettingReq Request);

		// Token: 0x06000117 RID: 279
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		void ClearTestExamBookingLayoutInCentralizedSetting(ClearTestExamBookingLayoutInCentralizedSettingReq Request);

		// Token: 0x06000118 RID: 280
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadUnbookedTestExamStudentsResp LoadUnbookedTestExamStudents(LoadUnbookedTestExamStudentsReq Request);
	}
}
