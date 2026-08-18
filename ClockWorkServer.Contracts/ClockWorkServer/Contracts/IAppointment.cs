using System;
using System.ServiceModel;
using System.Threading.Tasks;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsCalendar.AppParameters;
using TechnoPro.ClockWorkServer.Contracts.Faults;
using TechnoPro.Common.Public;
using TechnoPro.Common.WCF.Faults;
using WCFExtrasPlus.Soap;
using WCFExtrasPlus.Wsdl.Documentation;

namespace TechnoPro.ClockWorkServer.Contracts
{
	// Token: 0x02000013 RID: 19
	[ServiceContract(Name = "AppointmentService", Namespace = "http://tpro.ca")]
	[SoapHeaders]
	[XmlComments]
	public interface IAppointment : IService
	{
		// Token: 0x060000B8 RID: 184
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadDeletedAppointmentByIdResp LoadDeletedAppointmentById(LoadDeletedAppointmentByIdReq request);

		// Token: 0x060000B9 RID: 185
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadAppointmentsResp LoadAppointments(LoadAppointmentsReq request);

		// Token: 0x060000BA RID: 186
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		void CancelAppointment(CancelAppointmentReq request);

		// Token: 0x060000BB RID: 187
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		void UnCancelAppointment(UnCancelAppointmentReq request);

		// Token: 0x060000BC RID: 188
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		void UnMarkAppointmentTentative(UnMarkAppointmentTentativeReq request);

		// Token: 0x060000BD RID: 189
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		void MarkAppointmentTentative(MarkAppointmentTentativeReq request);

		// Token: 0x060000BE RID: 190
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadAppointmentByIdResp LoadAppointmentById(LoadAppointmentByIdReq request);

		// Token: 0x060000BF RID: 191
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadAppointmentExtendedInfoResp LoadAppointmentExtendedInfo(LoadAppointmentExtendedInfoReq request);

		// Token: 0x060000C0 RID: 192
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		void DeleteAppointment(DeleteAppointmentReq request);

		// Token: 0x060000C1 RID: 193
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		UpdateAppointmentResp UpdateAppointment(UpdateAppointmentReq Request);

		// Token: 0x060000C2 RID: 194
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		CreateAppointmentResp CreateAppointment(CreateAppointmentReq Request);

		// Token: 0x060000C3 RID: 195
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		void UpdateCalendarAppointmentParts(UpdateCalendarAppointmentPartsReq Request);

		// Token: 0x060000C4 RID: 196
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		void InsertOrUpdateAppointmentMemo(InsertOrUpdateAppointmentMemoReq Request);

		// Token: 0x060000C5 RID: 197
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		FreeTimeSearchResp FreeTimeSearch(FreeTimeSearchReq Request);

		// Token: 0x060000C6 RID: 198
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadAppointmentsAndAvailabilityResp LoadAppointmentsAndAvailability(LoadAppointmentsAndAvailabilityReq Request);

		// Token: 0x060000C7 RID: 199
		[OperationContract(Name = "LoadAppointmentsAndAvailabilityAsync")]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		Task<LoadAppointmentsAndAvailabilityResp> LoadAppointmentsAndAvailabilityAsync(LoadAppointmentsAndAvailabilityReq Request);

		// Token: 0x060000C8 RID: 200
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadBasicAppointmentInformationByUserAndDateRangeResp LoadBasicAppointmentInformationByUserAndDateRange(LoadBasicAppointmentInformationByUserAndDateRangeReq Request);

		// Token: 0x060000C9 RID: 201
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		UpdateAppointmentExternalIdResp UpdateAppointmentExternalId(UpdateAppointmentExternalIdReq request);

		// Token: 0x060000CA RID: 202
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		GetNumberOfAppointmentsWithAppTypeResp GetNumberOfAppointmentsWithAppType(GetNumberOfAppointmentsWithAppTypeReq Request);

		// Token: 0x060000CB RID: 203
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		SwapAppointmentTypeForAllAppointmentsResp SwapAppointmentTypeForAllAppointments(SwapAppointmentTypeForAllAppointmentsReq Request);

		// Token: 0x060000CC RID: 204
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadAppointmentsWithSpecialPermissionsResp LoadAppointmentsWithSpecialPermissions(LoadAppointmentsWithSpecialPermissionsReq Request);

		// Token: 0x060000CD RID: 205
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadAppointmentWithSpecialPermissionsResp LoadAppointmentWithSpecialPermissions(LoadAppointmentWithSpecialPermissionsReq Request);

		// Token: 0x060000CE RID: 206
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		UpdateAppointmentDateAndTimeResp UpdateAppointmentDateAndTime(UpdateAppointmentDateAndTimeReq Request);

		// Token: 0x060000CF RID: 207
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		CancelAttendeeAppointmentResp CancelAttendeeAppointment(CancelAttendeeAppointmentReq Request);
	}
}
