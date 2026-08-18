using System;
using System.ServiceModel;
using TechnoPro.ClockWorkServer.Contracts.DTO.Appointments.BaseAppParameters;
using TechnoPro.ClockWorkServer.Contracts.Faults;
using TechnoPro.Common.Public;
using TechnoPro.Common.WCF.Faults;
using WCFExtrasPlus.Soap;
using WCFExtrasPlus.Wsdl.Documentation;

namespace TechnoPro.ClockWorkServer.Contracts
{
	// Token: 0x02000028 RID: 40
	[ServiceContract(Name = "AppointmentAttendeeService", Namespace = "http://tpro.ca")]
	[SoapHeaders]
	[XmlComments]
	public interface IAppointmentAttendee : IService
	{
		// Token: 0x06000168 RID: 360
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		void UpdateAttendeeNoShow(UpdateAttendeeNoShowReq request);

		// Token: 0x06000169 RID: 361
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadAttendeesByAppointmentIdResp LoadAttendeesByAppointmentId(LoadAttendeesByAppointmentIdReq Request);

		// Token: 0x0600016A RID: 362
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadAttendeeByIdResp LoadAttendeeById(LoadAttendeeByIdReq Request);

		// Token: 0x0600016B RID: 363
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadAttendeeByAttendeeIdResp LoadAttendeeByAttendeeId(LoadAttendeeByAttendeeIdReq Request);

		// Token: 0x0600016C RID: 364
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[FaultContract(typeof(InvalidOperationFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		void DeleteAttendee(DeleteAttendeeReq Request);

		// Token: 0x0600016D RID: 365
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[FaultContract(typeof(InvalidOperationFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		void DeleteAttendeeByAttendeeId(DeleteAttendeeByAttendeeIdReq Request);

		// Token: 0x0600016E RID: 366
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		InsertOrUpdateAppointmentAttendeeResp InsertOrUpdateAppointmentAttendee(InsertOrUpdateAppointmentAttendeeReq Request);

		// Token: 0x0600016F RID: 367
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		void InsertOrUpdateAppointmentAttendees(InsertOrUpdateAppointmentAttendeesReq Request);

		// Token: 0x06000170 RID: 368
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		void RemoveAttendeesNotInList(RemoveAttendeesNotInListReq Request);

		// Token: 0x06000171 RID: 369
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		void UpdateNoShowValue(UpdateNoShowValueReq Request);

		// Token: 0x06000172 RID: 370
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		void UpdateNoShowValueByAttendeeId(UpdateNoShowValueByAttendeeIdReq Request);

		// Token: 0x06000173 RID: 371
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		void UpdateMiscCodeValue(UpdateMiscCodeValueReq Request);

		// Token: 0x06000174 RID: 372
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		void UpdateMiscCodeValueByAttendeeId(UpdateMiscCodeValueByAttendeeIdReq Request);

		// Token: 0x06000175 RID: 373
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		void SwapAttendee(SwapAttendeeReq Request);

		// Token: 0x06000176 RID: 374
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		IsAttendeeDoubleBookedResp IsAttendeeDoubleBooked(IsAttendeeDoubleBookedReq Request);

		// Token: 0x06000177 RID: 375
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		GetDoubleBookedAttendeesResp GetDoubleBookedAttendees(GetDoubleBookedAttendeesReq Request);

		// Token: 0x06000178 RID: 376
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		TryToRemoveAttendeesResp TryToRemoveAttendees(TryToRemoveAttendeesReq request);

		// Token: 0x06000179 RID: 377
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadAttendeesByAppointmentIdsResp LoadAttendeesByAppointmentIds(LoadAttendeesByAppointmentIdsReq Request);
	}
}
