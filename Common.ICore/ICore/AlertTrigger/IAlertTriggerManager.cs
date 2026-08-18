using System;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.AlertTrigger;

namespace TechnoPro.Common.ICore.AlertTrigger
{
	// Token: 0x020000F8 RID: 248
	public interface IAlertTriggerManager : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x06000813 RID: 2067
		IAlertTriggerDefinition[] GetAlertTriggersForCurrentUser();

		// Token: 0x06000814 RID: 2068
		void ClearAlertTriggersForCurrentUser();

		// Token: 0x06000815 RID: 2069
		AlertTriggerForUserSet CheckForTriggerAlerts(int studentPersonId);

		// Token: 0x06000816 RID: 2070
		bool AllowedToBookAppointmentForStudent(int studentPersonId);
	}
}
