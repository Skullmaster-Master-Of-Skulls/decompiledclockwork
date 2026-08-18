using System;

namespace TechnoPro.Common.Public.Entities.AlertTrigger.AlertTriggerDefinitions
{
	// Token: 0x020005AA RID: 1450
	[AlertDef(eAlertTriggerType.ExistingInfo, "ei_", "AlertTriggerFunctionExistingInfo")]
	[Serializable]
	public class AlertTriggerDefinitionExistingInfoBase : AlertTriggerDefinitionBase
	{
		// Token: 0x06002F01 RID: 12033 RVA: 0x00033768 File Offset: 0x00031968
		public AlertTriggerDefinitionExistingInfoBase()
		{
		}

		// Token: 0x06002F02 RID: 12034 RVA: 0x00033A0B File Offset: 0x00031C0B
		public AlertTriggerDefinitionExistingInfoBase(int controlId, int screenNum)
		{
			this.SetValues(controlId, screenNum);
		}

		// Token: 0x06002F03 RID: 12035 RVA: 0x00033A1E File Offset: 0x00031C1E
		public void SetValues(int controlId, int screenNum)
		{
			this.ControlId = controlId;
			this.ScreenNum = screenNum;
		}

		// Token: 0x170013BE RID: 5054
		// (get) Token: 0x06002F04 RID: 12036 RVA: 0x00033A31 File Offset: 0x00031C31
		// (set) Token: 0x06002F05 RID: 12037 RVA: 0x00033A39 File Offset: 0x00031C39
		public virtual int ControlId { get; private set; }

		// Token: 0x170013BF RID: 5055
		// (get) Token: 0x06002F06 RID: 12038 RVA: 0x00033A42 File Offset: 0x00031C42
		// (set) Token: 0x06002F07 RID: 12039 RVA: 0x00033A4A File Offset: 0x00031C4A
		public virtual int ScreenNum { get; private set; }

		// Token: 0x170013C0 RID: 5056
		// (get) Token: 0x06002F08 RID: 12040 RVA: 0x00033A53 File Offset: 0x00031C53
		// (set) Token: 0x06002F09 RID: 12041 RVA: 0x00033A5B File Offset: 0x00031C5B
		public virtual string PreferredFormTypeCode { get; set; }
	}
}
