using System;

namespace TechnoPro.Common.Public.Entities.AlertTrigger
{
	// Token: 0x020005A7 RID: 1447
	public interface IAlertTriggerDefinitionBase : IAlertTriggerDefinitionCommon
	{
		// Token: 0x06002EEE RID: 12014
		T Clone<T>() where T : IAlertTriggerDefinitionCommon;
	}
}
