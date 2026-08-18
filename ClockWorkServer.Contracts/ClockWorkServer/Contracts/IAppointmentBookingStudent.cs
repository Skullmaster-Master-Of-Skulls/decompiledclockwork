using System;
using System.ServiceModel;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsCalendar.AppParameters;
using TechnoPro.ClockWorkServer.Contracts.Faults;
using TechnoPro.Common.Public;
using TechnoPro.Common.WCF.Faults;
using WCFExtrasPlus.Soap;
using WCFExtrasPlus.Wsdl.Documentation;

namespace TechnoPro.ClockWorkServer.Contracts
{
	// Token: 0x02000010 RID: 16
	[ServiceContract(Name = "AppointmentBookingStudentService", Namespace = "http://tpro.ca")]
	[SoapHeaders]
	[XmlComments]
	public interface IAppointmentBookingStudent : IService
	{
		// Token: 0x0600008D RID: 141
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		ValidateBookStudentAppointmentResp ValidateBookStudentAppointment(ValidateBookStudentAppointmentReq Request);

		// Token: 0x0600008E RID: 142
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		TryToBookStudentAppointmentResp TryToBookStudentAppointment(TryToBookStudentAppointmentReq Request);

		// Token: 0x0600008F RID: 143
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		IsStudentBannedFromOnlineAppointmentBookingResp IsStudentBannedFromOnlineAppointmentBooking(IsStudentBannedFromOnlineAppointmentBookingReq Request);

		// Token: 0x06000090 RID: 144
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		MarkStudentBannedFromOnlineAppointmentBookingResp MarkStudentBannedFromOnlineAppointmentBooking(MarkStudentBannedFromOnlineAppointmentBookingReq Request);

		// Token: 0x06000091 RID: 145
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadAvailabilityForChannelResp LoadAvailabilityForChannel(LoadAvailabilityForChannelReq Request);

		// Token: 0x06000092 RID: 146
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		GetActiveChannelsForStudentResp GetActiveChannelsForStudent(GetActiveChannelsForStudentReq Request);
	}
}
