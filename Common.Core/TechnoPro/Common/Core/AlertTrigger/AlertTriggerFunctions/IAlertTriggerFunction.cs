using System;
using TechnoPro.Common.Public.Entities.AlertTrigger;

namespace TechnoPro.Common.Core.AlertTrigger.AlertTriggerFunctions
{
	// Token: 0x0200016C RID: 364
	public interface IAlertTriggerFunction
	{
		// Token: 0x06001033 RID: 4147
		IAlertTriggerDefinition ConvertAlertTriggerDefBaseToAlertTriggerDef(IAlertTriggerDefinitionBase baseTrigger);

		// Token: 0x06001034 RID: 4148
		AlertTriggerForUser[] CheckForTriggerAlerts(IAlertTriggerDefinition[] triggers, int studentPersonId);
	}
}
