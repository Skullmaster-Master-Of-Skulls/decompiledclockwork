using System;
using System.ServiceModel;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsList;
using TechnoPro.ClockWorkServer.Contracts.Faults;
using TechnoPro.Common.Public;
using TechnoPro.Common.WCF.Faults;
using WCFExtrasPlus.Soap;
using WCFExtrasPlus.Wsdl.Documentation;

namespace TechnoPro.ClockWorkServer.Contracts
{
	// Token: 0x02000012 RID: 18
	[ServiceContract(Name = "ListAppointmentService", Namespace = "http://tpro.ca")]
	[SoapHeaders]
	[XmlComments]
	public interface IListAppointment : IService
	{
		// Token: 0x06000097 RID: 151
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		CreateListAppointmentResp CreateListAppointment(CreateListAppointmentReq Request);

		// Token: 0x06000098 RID: 152
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		UpdateListAppointmentResp UpdateListAppointment(UpdateListAppointmentReq Request);

		// Token: 0x06000099 RID: 153
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		CancelListAppointmentResp CancelListAppointment(CancelListAppointmentReq Request);

		// Token: 0x0600009A RID: 154
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		UnCancelListAppointmentResp UnCancelListAppointment(UnCancelListAppointmentReq Request);

		// Token: 0x0600009B RID: 155
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		MarkListAppointmentAsTentativeResp MarkListAppointmentAsTentative(MarkListAppointmentAsTentativeReq Request);

		// Token: 0x0600009C RID: 156
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		UnMarkListAppointmentAsTentativeResp UnMarkListAppointmentAsTentative(UnMarkListAppointmentAsTentativeReq Request);

		// Token: 0x0600009D RID: 157
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		DeleteListAppointmentResp DeleteListAppointment(DeleteListAppointmentReq Request);

		// Token: 0x0600009E RID: 158
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		FreeTimeSearchResp FreeTimeSearch(FreeTimeSearchReq Request);

		// Token: 0x0600009F RID: 159
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadOverlappingAvailabilitiesResp LoadOverlappingAvailabilities(LoadOverlappingAvailabilitiesReq Request);

		// Token: 0x060000A0 RID: 160
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadClosedDaysResp LoadClosedDays(LoadClosedDaysReq Request);

		// Token: 0x060000A1 RID: 161
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		IsDayClosedResp IsDayClosed(IsDayClosedReq Request);

		// Token: 0x060000A2 RID: 162
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		void CreateClosedDay(CreateClosedDayReq Request);

		// Token: 0x060000A3 RID: 163
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		void DeleteClosedDay(DeleteClosedDayReq Request);

		// Token: 0x060000A4 RID: 164
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		void CreateAvailabilities(CreateAvailabilitiesReq Request);

		// Token: 0x060000A5 RID: 165
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		void DeleteAvailability(DeleteAvailabilityReq Request);

		// Token: 0x060000A6 RID: 166
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		void UpdateAvailability(UpdateAvailabilityReq Request);

		// Token: 0x060000A7 RID: 167
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		PrintMedicalCalendarResp PrintMedicalCalendar(PrintMedicalCalendarReq Request);

		// Token: 0x060000A8 RID: 168
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadAvailabilityResp LoadAvailability(LoadAvailabilityReq Request);

		// Token: 0x060000A9 RID: 169
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadAppointmentsResp LoadAppointments(LoadAppointmentsReq Request);

		// Token: 0x060000AA RID: 170
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadAppointmentsWithAvailabilityResp LoadAppointmentsWithAvailability(LoadAppointmentsWithAvailabilityReq Request);

		// Token: 0x060000AB RID: 171
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadAppointmentByIdResp LoadAppointmentById(LoadAppointmentByIdReq Request);

		// Token: 0x060000AC RID: 172
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		void MarkIn(MarkInReq Request);

		// Token: 0x060000AD RID: 173
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		void MarkNoShow(MarkNoShowReq Request);

		// Token: 0x060000AE RID: 174
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		void MarkConfirmed(MarkConfirmedReq Request);

		// Token: 0x060000AF RID: 175
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadSingleDayAvailabilityStatusesByUserResp LoadSingleDayAvailabilityStatusesByUser(LoadSingleDayAvailabilityStatusesByUserReq Request);

		// Token: 0x060000B0 RID: 176
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadAvailabilityByIdResp LoadAvailabilityById(LoadAvailabilityByIdReq Request);

		// Token: 0x060000B1 RID: 177
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadDistinctUsersAStudentHasHadAtLeastOneAppointmentWithResp LoadDistinctUsersAStudentHasHadAtLeastOneAppointmentWith(LoadDistinctUsersAStudentHasHadAtLeastOneAppointmentWithReq Request);

		// Token: 0x060000B2 RID: 178
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		void InsertOrUpdateAppointmentMemo(InsertOrUpdateAppointmentMemoReq Request);

		// Token: 0x060000B3 RID: 179
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		void FixAvailabilityAppointmentMappings(FixAvailabilityAppointmentMappingsReq Request);

		// Token: 0x060000B4 RID: 180
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadAvailability2MarkersResp LoadAvailability2Markers(LoadAvailability2MarkersReq Request);

		// Token: 0x060000B5 RID: 181
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		CreateAvailability2MarkerResp CreateAvailability2Marker(CreateAvailability2MarkerReq Request);

		// Token: 0x060000B6 RID: 182
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		void DeleteAvailability2Marker(DeleteAvailability2MarkerReq Request);

		// Token: 0x060000B7 RID: 183
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		void UpdateAvailability2Marker(UpdateAvailability2MarkerReq Request);
	}
}
