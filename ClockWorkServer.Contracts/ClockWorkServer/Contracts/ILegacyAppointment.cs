using System;
using System.ServiceModel;
using TechnoPro.ClockWorkServer.Contracts.DTO.Legacy.Appointment;
using TechnoPro.ClockWorkServer.Contracts.Faults;
using TechnoPro.Common.Public;
using TechnoPro.Common.WCF.Faults;
using WCFExtrasPlus.Soap;
using WCFExtrasPlus.Wsdl.Documentation;

namespace TechnoPro.ClockWorkServer.Contracts
{
	// Token: 0x0200005F RID: 95
	[ServiceContract(Name = "LegacyAppointmentService", Namespace = "http://tpro.ca")]
	[SoapHeaders]
	[XmlComments]
	public interface ILegacyAppointment : IService
	{
		// Token: 0x060002D3 RID: 723
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadAsAppointmentModifiedHistoryResp LoadAsAppointmentModifiedHistory(LoadAsAppointmentModifiedHistoryReq Request);
	}
}
