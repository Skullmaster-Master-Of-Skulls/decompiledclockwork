using System;
using TechnoPro.ClockWorkServer.Client.ServiceFactory;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.AlertTrigger;
using TechnoPro.Common.ClientManager.ICore;
using TechnoPro.Common.ClientManager.ICore.AlertTrigger;
using TechnoPro.Common.Public;
using TechnoPro.Common.Unity.IoC;

namespace TechnoPro.Common.ClientManager.Core.AlertTrigger
{
	// Token: 0x020000A5 RID: 165
	public class AlertTriggerClientManager : IAlertTriggerClientManager, IWebService
	{
		// Token: 0x06000659 RID: 1625 RVA: 0x0001BD28 File Offset: 0x00019F28
		public AlertTriggerForUserSetDTO CheckForTriggerAlerts(int StudentPersonId)
		{
			CheckForTriggerAlertsReq checkForTriggerAlertsReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<CheckForTriggerAlertsReq>();
			checkForTriggerAlertsReq.StudentPersonId = StudentPersonId;
			return ClientServiceFactory.GetClientInstance<IAlertTrigger>().CheckForTriggerAlerts(checkForTriggerAlertsReq).AlertTriggerForUserSet;
		}

		// Token: 0x0600065A RID: 1626 RVA: 0x0001BD60 File Offset: 0x00019F60
		public bool AllowedToBookAppointmentForStudent(int StudentPersonId)
		{
			AllowedToBookAppointmentForStudentReq allowedToBookAppointmentForStudentReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<AllowedToBookAppointmentForStudentReq>();
			allowedToBookAppointmentForStudentReq.StudentPersonId = StudentPersonId;
			return ClientServiceFactory.GetClientInstance<IAlertTrigger>().AllowedToBookAppointmentForStudent(allowedToBookAppointmentForStudentReq).IsAllowedToBookAppointments;
		}
	}
}
