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
	// Token: 0x02000023 RID: 35
	[ServiceContract(Name = "AppointmentCancelInfoService", Namespace = "http://tpro.ca")]
	[SoapHeaders]
	[XmlComments]
	public interface IAppointmentCancelInfo : IService
	{
		// Token: 0x0600013C RID: 316
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadCancelInfoByAppointmentIdResp LoadCancelInfoByAppointmentId(LoadCancelInfoByAppointmentIdReq Request);

		// Token: 0x0600013D RID: 317
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		void DeleteCancelInfo(DeleteCancelInfoReq Request);

		// Token: 0x0600013E RID: 318
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		void InsertOrUpdateAppointmentCancelInfo(InsertOrUpdateAppointmentCancelInfoReq Request);
	}
}
