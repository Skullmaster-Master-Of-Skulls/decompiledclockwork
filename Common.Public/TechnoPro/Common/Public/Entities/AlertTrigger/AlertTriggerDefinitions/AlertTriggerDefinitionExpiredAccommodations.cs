using System;

namespace TechnoPro.Common.Public.Entities.AlertTrigger.AlertTriggerDefinitions
{
	// Token: 0x020005AD RID: 1453
	[AlertDef(eAlertTriggerType.ExpiredAccommodations, "ae_", "AlertTriggerFunctionExpiredAccommodations")]
	[Serializable]
	public class AlertTriggerDefinitionExpiredAccommodations : AlertTriggerDefinitionExpiredAccommodationsBase, IAlertTriggerDefinition, IAlertTriggerDefinitionCommon
	{
	}
}
