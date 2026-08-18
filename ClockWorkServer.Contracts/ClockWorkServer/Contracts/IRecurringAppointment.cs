using System;
using System.ServiceModel;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsRecurring;
using TechnoPro.ClockWorkServer.Contracts.Faults;
using TechnoPro.Common.Public;
using TechnoPro.Common.WCF.Faults;
using WCFExtrasPlus.Soap;
using WCFExtrasPlus.Wsdl.Documentation;

namespace TechnoPro.ClockWorkServer.Contracts
{
	// Token: 0x02000015 RID: 21
	[ServiceContract(Name = "RecurringAppointmentService", Namespace = "http://tpro.ca")]
	[SoapHeaders]
	[XmlComments]
	public interface IRecurringAppointment : IService
	{
		// Token: 0x060000D6 RID: 214
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadCurrentRecurringAppointmentsSetResp LoadCurrentRecurringAppointmentsSet(LoadCurrentRecurringAppointmentsSetReq Request);

		// Token: 0x060000D7 RID: 215
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		void UpdateRecurringAppointmentGroupInformationAndDates(UpdateRecurringAppointmentGroupInformationAndDatesReq Request);

		// Token: 0x060000D8 RID: 216
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		UpdateRecurringAppointmentInstancesResp UpdateRecurringAppointmentInstances(UpdateRecurringAppointmentInstancesReq Request);

		// Token: 0x060000D9 RID: 217
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadAppointmentsInARecurringSetWithPermissionsToEditForASpecificUserResp LoadAppointmentsInARecurringSetWithPermissionsToEditForASpecificUser(LoadAppointmentsInARecurringSetWithPermissionsToEditForASpecificUserReq Request);

		// Token: 0x060000DA RID: 218
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		IsUserAllowedToEditAllAppointmentsInARecurringSetResp IsUserAllowedToEditAllAppointmentsInARecurringSet(IsUserAllowedToEditAllAppointmentsInARecurringSetReq Request);

		// Token: 0x060000DB RID: 219
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		UpdateRecurringAppointmentAttendeesResp UpdateRecurringAppointmentAttendees(UpdateRecurringAppointmentAttendeesReq Request);

		// Token: 0x060000DC RID: 220
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		UpdateRecurringWorkshopAppointmentInstancesResp UpdateRecurringWorkshopAppointmentInstances(UpdateRecurringWorkshopAppointmentInstancesReq Request);
	}
}
