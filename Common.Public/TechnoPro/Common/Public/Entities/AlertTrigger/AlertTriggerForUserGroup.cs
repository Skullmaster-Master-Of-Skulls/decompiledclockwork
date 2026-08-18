using System;

namespace TechnoPro.Common.Public.Entities.AlertTrigger
{
	// Token: 0x020005A3 RID: 1443
	public class AlertTriggerForUserGroup : BusinessBase<eAlertTriggerType>
	{
		// Token: 0x170013AE RID: 5038
		// (get) Token: 0x06002EDB RID: 11995 RVA: 0x000338E8 File Offset: 0x00031AE8
		// (set) Token: 0x06002EDC RID: 11996 RVA: 0x00033900 File Offset: 0x00031B00
		public virtual eAlertTriggerType TriggerType
		{
			get
			{
				return this.Id;
			}
			set
			{
				this.Id = value;
			}
		}

		// Token: 0x170013AF RID: 5039
		// (get) Token: 0x06002EDD RID: 11997 RVA: 0x0003390B File Offset: 0x00031B0B
		// (set) Token: 0x06002EDE RID: 11998 RVA: 0x00033913 File Offset: 0x00031B13
		public AlertTriggerForUser[] Triggers { get; set; }
	}
}
