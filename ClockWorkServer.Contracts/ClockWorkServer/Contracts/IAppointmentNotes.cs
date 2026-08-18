using System;
using System.ServiceModel;
using TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms;
using TechnoPro.ClockWorkServer.Contracts.Faults;
using TechnoPro.Common.Public;
using TechnoPro.Common.WCF.Faults;
using WCFExtrasPlus.Soap;
using WCFExtrasPlus.Wsdl.Documentation;

namespace TechnoPro.ClockWorkServer.Contracts
{
	// Token: 0x02000042 RID: 66
	[ServiceContract(Name = "AppointmentNotesService", Namespace = "http://tpro.ca")]
	[SoapHeaders]
	[XmlComments]
	public interface IAppointmentNotes : IService
	{
		// Token: 0x060001FF RID: 511
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadAllAppointmentIdsWithNotesResp LoadAllAppointmentIdsWithNotes(LoadAllAppointmentIdsWithNotesReq Request);

		// Token: 0x06000200 RID: 512
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		GetAppointmentNotesSummaryHtmlResp GetAppointmentNotesSummaryHtml(GetAppointmentNotesSummaryHtmlReq Request);

		// Token: 0x06000201 RID: 513
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		void SaveAppointmentNotesToFirstRtfInFirstFormAttachedToAppointmentType(SaveAppointmentNotesToFirstRtfInFirstFormAttachedToAppointmentTypeReq Request);

		// Token: 0x06000202 RID: 514
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadAppointmentNotesRtfFromFirstRtfInFirstFormAttachedToAppointmentTypeResp LoadAppointmentNotesRtfFromFirstRtfInFirstFormAttachedToAppointmentType(LoadAppointmentNotesRtfFromFirstRtfInFirstFormAttachedToAppointmentTypeReq Request);

		// Token: 0x06000203 RID: 515
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadNotesAppointmentsForStudentNoAttendeesResp LoadNotesAppointmentsForStudentNoAttendees(LoadNotesAppointmentsForStudentNoAttendeesReq Request);

		// Token: 0x06000204 RID: 516
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadNotesAppointmentByAppointmentIdResp LoadNotesAppointmentByAppointmentId(LoadNotesAppointmentByAppointmentIdReq Request);

		// Token: 0x06000205 RID: 517
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadNotesAppointmentExtendedInfosResp LoadNotesAppointmentExtendedInfos(LoadNotesAppointmentExtendedInfosReq Request);
	}
}
