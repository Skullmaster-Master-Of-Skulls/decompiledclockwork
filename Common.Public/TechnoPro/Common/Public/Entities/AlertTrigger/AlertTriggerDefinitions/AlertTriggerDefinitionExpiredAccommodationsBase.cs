using System;

namespace TechnoPro.Common.Public.Entities.AlertTrigger.AlertTriggerDefinitions
{
	// Token: 0x020005AC RID: 1452
	[AlertDef(eAlertTriggerType.ExpiredAccommodations, "ae_", "AlertTriggerFunctionExpiredAccommodations")]
	[Serializable]
	public class AlertTriggerDefinitionExpiredAccommodationsBase : AlertTriggerDefinitionBase
	{
		// Token: 0x170013C4 RID: 5060
		// (get) Token: 0x06002F0F RID: 12047 RVA: 0x00033ADA File Offset: 0x00031CDA
		// (set) Token: 0x06002F10 RID: 12048 RVA: 0x00033AE2 File Offset: 0x00031CE2
		public int NumberOfDaysEarlyToWarn { get; set; }

		// Token: 0x170013C5 RID: 5061
		// (get) Token: 0x06002F11 RID: 12049 RVA: 0x00033AEB File Offset: 0x00031CEB
		// (set) Token: 0x06002F12 RID: 12050 RVA: 0x00033AF3 File Offset: 0x00031CF3
		public bool ShouldWarnIfExpiryDateIsEmpty { get; set; }
	}
}
