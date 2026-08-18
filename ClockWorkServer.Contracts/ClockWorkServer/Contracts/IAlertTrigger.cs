using System;
using System.ServiceModel;
using TechnoPro.ClockWorkServer.Contracts.DTO.AlertTrigger;
using TechnoPro.ClockWorkServer.Contracts.Faults;
using TechnoPro.Common.Public;
using TechnoPro.Common.WCF.Faults;
using WCFExtrasPlus.Soap;
using WCFExtrasPlus.Wsdl.Documentation;

namespace TechnoPro.ClockWorkServer.Contracts
{
	// Token: 0x02000005 RID: 5
	[ServiceContract(Name = "AlertTriggerService", Namespace = "http://tpro.ca")]
	[SoapHeaders]
	[XmlComments]
	public interface IAlertTrigger : IService
	{
		// Token: 0x06000002 RID: 2
		[OperationContract(Name = "CheckForTriggerAlerts")]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		CheckForTriggerAlertsResp CheckForTriggerAlerts(CheckForTriggerAlertsReq Request);

		// Token: 0x06000003 RID: 3
		[OperationContract(Name = "AllowedToBookAppointmentForStudent")]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		AllowedToBookAppointmentForStudentResp AllowedToBookAppointmentForStudent(AllowedToBookAppointmentForStudentReq Request);
	}
}
