using System;
using TechnoPro.ClockWorkServer.Common.Services.Impl.Adapters;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.AlertTrigger;
using TechnoPro.Common.Core.AlertTrigger;
using TechnoPro.Common.Core.Mappers.AlertTrigger;
using TechnoPro.Common.ICore.AlertTrigger;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities.AlertTrigger;

namespace TechnoPro.ClockWorkServer.Common.Services.Impl
{
	// Token: 0x02000002 RID: 2
	public class AlertTriggerServiceManager : IAlertTrigger, IService
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		public CheckForTriggerAlertsResp CheckForTriggerAlerts(CheckForTriggerAlertsReq Request)
		{
			IAlertTriggerManager alertTriggerManager = new AlertTriggerManager(Request.GetOperationContext());
			CheckForTriggerAlertsResp checkForTriggerAlertsResp = new CheckForTriggerAlertsResp();
			AlertTriggerForUserSet alertTriggerForUserSet = alertTriggerManager.CheckForTriggerAlerts(Request.StudentPersonId);
			checkForTriggerAlertsResp.AlertTriggerForUserSet = ((alertTriggerForUserSet != null) ? alertTriggerForUserSet.ToDTO() : null);
			return checkForTriggerAlertsResp;
		}

		// Token: 0x06000002 RID: 2 RVA: 0x00002094 File Offset: 0x00000294
		public AllowedToBookAppointmentForStudentResp AllowedToBookAppointmentForStudent(AllowedToBookAppointmentForStudentReq Request)
		{
			IAlertTriggerManager alertTriggerManager = new AlertTriggerManager(Request.GetOperationContext());
			return new AllowedToBookAppointmentForStudentResp
			{
				IsAllowedToBookAppointments = alertTriggerManager.AllowedToBookAppointmentForStudent(Request.StudentPersonId)
			};
		}
	}
}
