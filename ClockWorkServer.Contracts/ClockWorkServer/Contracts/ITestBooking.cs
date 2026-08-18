using System;
using System.ServiceModel;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking;
using TechnoPro.ClockWorkServer.Contracts.Faults;
using TechnoPro.Common.Public;
using TechnoPro.Common.WCF.Faults;
using WCFExtrasPlus.Soap;
using WCFExtrasPlus.Wsdl.Documentation;

namespace TechnoPro.ClockWorkServer.Contracts
{
	// Token: 0x020000A3 RID: 163
	[ServiceContract(Name = "TestBookingService", Namespace = "http://tpro.ca")]
	[SoapHeaders]
	[XmlComments]
	public interface ITestBooking : IService
	{
		// Token: 0x060004C2 RID: 1218
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadTestsResp LoadTests(LoadTestsReq request);

		// Token: 0x060004C3 RID: 1219
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		void UpdateTest(UpdateTestReq request);

		// Token: 0x060004C4 RID: 1220
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadTestAccommodationsResp LoadTestAccommodations(LoadTestAccommodationsReq request);

		// Token: 0x060004C5 RID: 1221
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadTestByAppointmentIdResp LoadTestByAppointmentId(LoadTestByAppointmentIdReq request);

		// Token: 0x060004C6 RID: 1222
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadTestBookingMailMergeInfoByDateResp LoadTestBookingMailMergeInfoByDate(LoadTestBookingMailMergeInfoByDateReq Request);

		// Token: 0x060004C7 RID: 1223
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		void DeleteTest(DeleteTestReq Request);

		// Token: 0x060004C8 RID: 1224
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadTestsByExamIdResp LoadTestsByExamId(LoadTestsByExamIdReq Request);

		// Token: 0x060004C9 RID: 1225
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadTestsByAppointmentIdsResp LoadTestsByAppointmentIds(LoadTestsByAppointmentIdsReq Request);

		// Token: 0x060004CA RID: 1226
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadBasicTestsByAppointmentIdsResp LoadBasicTestsByAppointmentIds(LoadBasicTestsByAppointmentIdsReq Request);

		// Token: 0x060004CB RID: 1227
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadAllExamStatusesResp LoadAllExamStatuses(LoadAllExamStatusesReq Request);

		// Token: 0x060004CC RID: 1228
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadAccommodationsByTestResp LoadAccommodationsByTest(LoadAccommodationsByTestReq Request);

		// Token: 0x060004CD RID: 1229
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadTestAndAllowedAccommodationsResp LoadTestAndAllowedAccommodations(LoadTestAndAllowedAccommodationsReq Request);

		// Token: 0x060004CE RID: 1230
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadTestForEditByAppointmentIdResp LoadTestForEditByAppointmentId(LoadTestForEditByAppointmentIdReq Request);

		// Token: 0x060004CF RID: 1231
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		UpdateTestAccommodationsResp UpdateTestAccommodations(UpdateTestAccommodationsReq Request);

		// Token: 0x060004D0 RID: 1232
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		CreateTestResp CreateTest(CreateTestReq Request);

		// Token: 0x060004D1 RID: 1233
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadTestsByStudentResp LoadTestsByStudent(LoadTestsByStudentReq Request);

		// Token: 0x060004D2 RID: 1234
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadStudentsWritingExamResp LoadStudentsWritingExam(LoadStudentsWritingExamReq Request);

		// Token: 0x060004D3 RID: 1235
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadInstructorAcknowledgedStudentResp LoadInstructorAcknowledgedStudent(LoadInstructorAcknowledgedStudentReq Request);
	}
}
