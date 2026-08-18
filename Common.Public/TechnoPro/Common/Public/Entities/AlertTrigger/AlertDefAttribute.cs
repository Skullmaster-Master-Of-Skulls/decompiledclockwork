using System;

namespace TechnoPro.Common.Public.Entities.AlertTrigger
{
	// Token: 0x020005A9 RID: 1449
	public class AlertDefAttribute : Attribute
	{
		// Token: 0x06002EF9 RID: 12025 RVA: 0x0000EC26 File Offset: 0x0000CE26
		public AlertDefAttribute()
		{
		}

		// Token: 0x06002EFA RID: 12026 RVA: 0x000339B6 File Offset: 0x00031BB6
		public AlertDefAttribute(eAlertTriggerType triggerType, string code, string functionClassName)
		{
			this.TriggerType = triggerType;
			this.Code = code;
			this.AlertTriggerFunctionClassName = functionClassName;
		}

		// Token: 0x170013BB RID: 5051
		// (get) Token: 0x06002EFB RID: 12027 RVA: 0x000339D8 File Offset: 0x00031BD8
		// (set) Token: 0x06002EFC RID: 12028 RVA: 0x000339E0 File Offset: 0x00031BE0
		public string Code { get; private set; }

		// Token: 0x170013BC RID: 5052
		// (get) Token: 0x06002EFD RID: 12029 RVA: 0x000339E9 File Offset: 0x00031BE9
		// (set) Token: 0x06002EFE RID: 12030 RVA: 0x000339F1 File Offset: 0x00031BF1
		public eAlertTriggerType TriggerType { get; private set; }

		// Token: 0x170013BD RID: 5053
		// (get) Token: 0x06002EFF RID: 12031 RVA: 0x000339FA File Offset: 0x00031BFA
		// (set) Token: 0x06002F00 RID: 12032 RVA: 0x00033A02 File Offset: 0x00031C02
		public string AlertTriggerFunctionClassName { get; private set; }
	}
}
