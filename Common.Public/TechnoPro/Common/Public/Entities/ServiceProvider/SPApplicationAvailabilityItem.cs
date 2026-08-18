using System;

namespace TechnoPro.Common.Public.Entities.ServiceProvider
{
	// Token: 0x020001E4 RID: 484
	public class SPApplicationAvailabilityItem : BusinessBase<int>
	{
		// Token: 0x17000589 RID: 1417
		// (get) Token: 0x06000DD8 RID: 3544 RVA: 0x00016080 File Offset: 0x00014280
		// (set) Token: 0x06000DD9 RID: 3545 RVA: 0x0000E258 File Offset: 0x0000C458
		public virtual int SPApplicationAvailabilityitemId
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

		// Token: 0x1700058A RID: 1418
		// (get) Token: 0x06000DDA RID: 3546 RVA: 0x00016098 File Offset: 0x00014298
		// (set) Token: 0x06000DDB RID: 3547 RVA: 0x000160A0 File Offset: 0x000142A0
		public SPApplication Application { get; set; }

		// Token: 0x1700058B RID: 1419
		// (get) Token: 0x06000DDC RID: 3548 RVA: 0x000160A9 File Offset: 0x000142A9
		// (set) Token: 0x06000DDD RID: 3549 RVA: 0x000160B1 File Offset: 0x000142B1
		public DateTime StartDateTime { get; set; }

		// Token: 0x1700058C RID: 1420
		// (get) Token: 0x06000DDE RID: 3550 RVA: 0x000160BA File Offset: 0x000142BA
		// (set) Token: 0x06000DDF RID: 3551 RVA: 0x000160C2 File Offset: 0x000142C2
		public DateTime EndDateTime { get; set; }

		// Token: 0x1700058D RID: 1421
		// (get) Token: 0x06000DE0 RID: 3552 RVA: 0x000160CB File Offset: 0x000142CB
		// (set) Token: 0x06000DE1 RID: 3553 RVA: 0x000160D3 File Offset: 0x000142D3
		public string Note { get; set; }

		// Token: 0x1700058E RID: 1422
		// (get) Token: 0x06000DE2 RID: 3554 RVA: 0x000160DC File Offset: 0x000142DC
		// (set) Token: 0x06000DE3 RID: 3555 RVA: 0x000160E4 File Offset: 0x000142E4
		public string Location { get; set; }
	}
}
