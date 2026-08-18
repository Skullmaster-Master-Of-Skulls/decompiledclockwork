using System;

namespace TechnoPro.Common.Public.Entities.AlertTrigger
{
	// Token: 0x020005A2 RID: 1442
	public class AlertTriggerForUserSet : BusinessBase<int>
	{
		// Token: 0x170013AC RID: 5036
		// (get) Token: 0x06002ED6 RID: 11990 RVA: 0x000338BC File Offset: 0x00031ABC
		// (set) Token: 0x06002ED7 RID: 11991 RVA: 0x0000E258 File Offset: 0x0000C458
		public virtual int StudentPersonId
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

		// Token: 0x170013AD RID: 5037
		// (get) Token: 0x06002ED8 RID: 11992 RVA: 0x000338D4 File Offset: 0x00031AD4
		// (set) Token: 0x06002ED9 RID: 11993 RVA: 0x000338DC File Offset: 0x00031ADC
		public AlertTriggerForUserGroup[] AlertTriggerGroups { get; set; }
	}
}
