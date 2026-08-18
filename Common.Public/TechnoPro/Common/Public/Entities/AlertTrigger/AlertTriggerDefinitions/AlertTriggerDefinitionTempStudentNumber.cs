using System;

namespace TechnoPro.Common.Public.Entities.AlertTrigger.AlertTriggerDefinitions
{
	// Token: 0x020005B3 RID: 1459
	[AlertDef(eAlertTriggerType.TempStudentNumber, "ts_", "AlertTriggerFunctionTempStudentNumber")]
	[Serializable]
	public class AlertTriggerDefinitionTempStudentNumber : AlertTriggerDefinitionTempStudentNumberBase, IAlertTriggerDefinition, IAlertTriggerDefinitionCommon
	{
	}
}
