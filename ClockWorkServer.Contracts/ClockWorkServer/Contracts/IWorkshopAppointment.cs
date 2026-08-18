using System;
using System.ServiceModel;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsWorkshops;
using TechnoPro.ClockWorkServer.Contracts.Faults;
using TechnoPro.Common.Public;
using TechnoPro.Common.WCF.Faults;
using WCFExtrasPlus.Soap;
using WCFExtrasPlus.Wsdl.Documentation;

namespace TechnoPro.ClockWorkServer.Contracts
{
	// Token: 0x02000021 RID: 33
	[ServiceContract(Name = "WorkshopService", Namespace = "http://tpro.ca")]
	[SoapHeaders]
	[XmlComments]
	public interface IWorkshopAppointment : IService
	{
		// Token: 0x06000129 RID: 297
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadWorkshopAppointmentsWithNoWorkshopByAppTypeResp LoadWorkshopAppointmentsWithNoWorkshopId(LoadWorkshopAppointmentsWithNoWorkshopByAppTypeReq Request);

		// Token: 0x0600012A RID: 298
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		CancelWorkshopAppointmentResp CancelWorkshopAppointment(CancelWorkshopAppointmentReq Request);

		// Token: 0x0600012B RID: 299
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		UncancelWorkshopAppointmentResp UncancelWorkshopAppointment(UncancelWorkshopAppointmentReq Request);

		// Token: 0x0600012C RID: 300
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadWorkshopAppointmentsByWorkshopIdResp LoadWorkshopAppointmentsByWorkshopId(LoadWorkshopAppointmentsByWorkshopIdReq Request);

		// Token: 0x0600012D RID: 301
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadWorkshopAppointmentResp LoadWorkshopAppointment(LoadWorkshopAppointmentReq Request);

		// Token: 0x0600012E RID: 302
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		DeleteWorkshopAppointmentResp DeleteWorkshopAppointment(DeleteWorkshopAppointmentReq request);

		// Token: 0x0600012F RID: 303
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		CreateWorkshopAppointmentResp CreateWorkshopAppointment(CreateWorkshopAppointmentReq Request);

		// Token: 0x06000130 RID: 304
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		UpdateWorkshopAppointmentResp UpdateWorkshopAppointment(UpdateWorkshopAppointmentReq Request);

		// Token: 0x06000131 RID: 305
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		void UpdateWorkshopAppointmentParts(UpdateWorkshopAppointmentPartsReq Request);

		// Token: 0x06000132 RID: 306
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		void InsertOrUpdateAppointmentMemo(InsertOrUpdateAppointmentMemoReq Request);

		// Token: 0x06000133 RID: 307
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		void UpdateAppointmentWorkshopId(UpdateAppointmentWorkshopIdReq Request);
	}
}
