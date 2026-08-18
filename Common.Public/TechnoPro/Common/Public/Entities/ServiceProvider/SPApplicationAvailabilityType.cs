using System;

namespace TechnoPro.Common.Public.Entities.ServiceProvider
{
	// Token: 0x020001E5 RID: 485
	public class SPApplicationAvailabilityType : BusinessBase<int>
	{
		// Token: 0x1700058F RID: 1423
		// (get) Token: 0x06000DE5 RID: 3557 RVA: 0x000160F0 File Offset: 0x000142F0
		// (set) Token: 0x06000DE6 RID: 3558 RVA: 0x0000E258 File Offset: 0x0000C458
		public virtual int SPApplicationAvailabilityTypeId
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

		// Token: 0x17000590 RID: 1424
		// (get) Token: 0x06000DE7 RID: 3559 RVA: 0x00016108 File Offset: 0x00014308
		// (set) Token: 0x06000DE8 RID: 3560 RVA: 0x00016110 File Offset: 0x00014310
		public string Title { get; set; }

		// Token: 0x17000591 RID: 1425
		// (get) Token: 0x06000DE9 RID: 3561 RVA: 0x00016119 File Offset: 0x00014319
		// (set) Token: 0x06000DEA RID: 3562 RVA: 0x00016121 File Offset: 0x00014321
		public string Description { get; set; }

		// Token: 0x17000592 RID: 1426
		// (get) Token: 0x06000DEB RID: 3563 RVA: 0x0001612A File Offset: 0x0001432A
		// (set) Token: 0x06000DEC RID: 3564 RVA: 0x00016132 File Offset: 0x00014332
		public bool IsActive { get; set; }

		// Token: 0x17000593 RID: 1427
		// (get) Token: 0x06000DED RID: 3565 RVA: 0x0001613B File Offset: 0x0001433B
		// (set) Token: 0x06000DEE RID: 3566 RVA: 0x00016143 File Offset: 0x00014343
		public bool IsVisible { get; set; }
	}
}
