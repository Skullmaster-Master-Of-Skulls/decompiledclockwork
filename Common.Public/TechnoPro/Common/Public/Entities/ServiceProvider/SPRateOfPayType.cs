using System;

namespace TechnoPro.Common.Public.Entities.ServiceProvider
{
	// Token: 0x020001EA RID: 490
	public class SPRateOfPayType : BusinessBase<int>
	{
		// Token: 0x170005B5 RID: 1461
		// (get) Token: 0x06000E36 RID: 3638 RVA: 0x0001639C File Offset: 0x0001459C
		// (set) Token: 0x06000E37 RID: 3639 RVA: 0x0000E258 File Offset: 0x0000C458
		public virtual int SPRateOfPayTypeId
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

		// Token: 0x170005B6 RID: 1462
		// (get) Token: 0x06000E38 RID: 3640 RVA: 0x000163B4 File Offset: 0x000145B4
		// (set) Token: 0x06000E39 RID: 3641 RVA: 0x000163BC File Offset: 0x000145BC
		public string Title { get; set; }

		// Token: 0x170005B7 RID: 1463
		// (get) Token: 0x06000E3A RID: 3642 RVA: 0x000163C5 File Offset: 0x000145C5
		// (set) Token: 0x06000E3B RID: 3643 RVA: 0x000163CD File Offset: 0x000145CD
		public string Description { get; set; }

		// Token: 0x170005B8 RID: 1464
		// (get) Token: 0x06000E3C RID: 3644 RVA: 0x000163D6 File Offset: 0x000145D6
		// (set) Token: 0x06000E3D RID: 3645 RVA: 0x000163DE File Offset: 0x000145DE
		public bool IsOneTimePayment { get; set; }

		// Token: 0x170005B9 RID: 1465
		// (get) Token: 0x06000E3E RID: 3646 RVA: 0x000163E7 File Offset: 0x000145E7
		// (set) Token: 0x06000E3F RID: 3647 RVA: 0x000163EF File Offset: 0x000145EF
		public bool IsHourlyRate { get; set; }

		// Token: 0x170005BA RID: 1466
		// (get) Token: 0x06000E40 RID: 3648 RVA: 0x000163F8 File Offset: 0x000145F8
		// (set) Token: 0x06000E41 RID: 3649 RVA: 0x00016400 File Offset: 0x00014600
		public bool IsActive { get; set; }
	}
}
