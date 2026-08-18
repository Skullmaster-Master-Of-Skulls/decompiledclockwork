using System;
using TechnoPro.ClockWorkServer.Contracts.DTO.AlertTrigger;
using TechnoPro.Common.Public;

namespace TechnoPro.Common.ClientManager.ICore.AlertTrigger
{
	// Token: 0x020000A1 RID: 161
	public interface IAlertTriggerClientManager : IWebService
	{
		// Token: 0x06000537 RID: 1335
		AlertTriggerForUserSetDTO CheckForTriggerAlerts(int StudentPersonId);

		// Token: 0x06000538 RID: 1336
		bool AllowedToBookAppointmentForStudent(int StudentPersonId);
	}
}
