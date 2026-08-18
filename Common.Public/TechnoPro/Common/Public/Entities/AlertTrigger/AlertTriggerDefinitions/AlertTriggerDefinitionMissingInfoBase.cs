using System;

namespace TechnoPro.Common.Public.Entities.AlertTrigger.AlertTriggerDefinitions
{
	// Token: 0x020005AE RID: 1454
	[AlertDef(eAlertTriggerType.MissingInfo, "mi_", "AlertTriggerFunctionMissingInfo")]
	[Serializable]
	public class AlertTriggerDefinitionMissingInfoBase : AlertTriggerDefinitionBase
	{
		// Token: 0x06002F15 RID: 12053 RVA: 0x00033768 File Offset: 0x00031968
		public AlertTriggerDefinitionMissingInfoBase()
		{
		}

		// Token: 0x06002F16 RID: 12054 RVA: 0x00033B0E File Offset: 0x00031D0E
		public AlertTriggerDefinitionMissingInfoBase(int cid, int screenNum)
		{
			this.SetValues(cid, screenNum);
		}

		// Token: 0x06002F17 RID: 12055 RVA: 0x00033B21 File Offset: 0x00031D21
		public void SetValues(int cid, int screenNum)
		{
			this.ControlId = cid;
			this.ScreenNum = screenNum;
		}

		// Token: 0x170013C6 RID: 5062
		// (get) Token: 0x06002F18 RID: 12056 RVA: 0x00033B34 File Offset: 0x00031D34
		// (set) Token: 0x06002F19 RID: 12057 RVA: 0x00033B3C File Offset: 0x00031D3C
		public virtual int ControlId { get; private set; }

		// Token: 0x170013C7 RID: 5063
		// (get) Token: 0x06002F1A RID: 12058 RVA: 0x00033B45 File Offset: 0x00031D45
		// (set) Token: 0x06002F1B RID: 12059 RVA: 0x00033B4D File Offset: 0x00031D4D
		public virtual int ScreenNum { get; private set; }
	}
}
