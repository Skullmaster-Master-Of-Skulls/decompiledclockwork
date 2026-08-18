using System;

namespace TechnoPro.Common.Public.Entities.Inventory
{
	// Token: 0x0200031E RID: 798
	public class InventoryReservation : BusinessBase<int>
	{
		// Token: 0x17000A4D RID: 2637
		// (get) Token: 0x060018E0 RID: 6368 RVA: 0x0001D9EC File Offset: 0x0001BBEC
		// (set) Token: 0x060018E1 RID: 6369 RVA: 0x0000E258 File Offset: 0x0000C458
		public int ReservationId
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

		// Token: 0x17000A4E RID: 2638
		// (get) Token: 0x060018E2 RID: 6370 RVA: 0x0001DA04 File Offset: 0x0001BC04
		// (set) Token: 0x060018E3 RID: 6371 RVA: 0x0001DA0C File Offset: 0x0001BC0C
		public InventoryProduct ReservedProduct { get; set; }

		// Token: 0x17000A4F RID: 2639
		// (get) Token: 0x060018E4 RID: 6372 RVA: 0x0001DA15 File Offset: 0x0001BC15
		// (set) Token: 0x060018E5 RID: 6373 RVA: 0x0001DA1D File Offset: 0x0001BC1D
		public InventoryReservationGroup Group { get; set; }
	}
}
