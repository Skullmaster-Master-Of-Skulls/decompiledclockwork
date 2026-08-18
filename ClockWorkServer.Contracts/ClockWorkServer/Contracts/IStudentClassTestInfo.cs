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
	// Token: 0x0200001B RID: 27
	[ServiceContract(Name = "StudentClassTestInfoService", Namespace = "http://tpro.ca")]
	[SoapHeaders]
	[XmlComments]
	public interface IStudentClassTestInfo : IService
	{
		// Token: 0x06000105 RID: 261
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		void UpdateExamStatus(UpdateExamStatusReq Request);

		// Token: 0x06000106 RID: 262
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		void UpdateBookingNote(UpdateBookingNoteReq Request);

		// Token: 0x06000107 RID: 263
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		void UpdatePrivateNote(UpdatePrivateNoteReq Request);

		// Token: 0x06000108 RID: 264
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		void UpdateBookingAndPrivateNote(UpdateBookingAndPrivateNoteReq Request);

		// Token: 0x06000109 RID: 265
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		void UpdateStudentReportedClassDateAndTimeToMatchExamClassDateAndTime(UpdateStudentReportedClassDateAndTimeToMatchExamClassDateAndTimeReq Request);

		// Token: 0x0600010A RID: 266
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadClassTestByAppointmentIdResp LoadClassTestByAppointmentId(LoadClassTestByAppointmentIdReq Request);
	}
}
