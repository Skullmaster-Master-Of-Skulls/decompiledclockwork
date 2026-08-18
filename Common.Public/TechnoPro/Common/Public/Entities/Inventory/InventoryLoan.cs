using System;

namespace TechnoPro.Common.Public.Entities.Inventory
{
	// Token: 0x02000318 RID: 792
	public class InventoryLoan : BusinessBase<int>
	{
		// Token: 0x17000A2F RID: 2607
		// (get) Token: 0x0600189D RID: 6301 RVA: 0x0001D6A4 File Offset: 0x0001B8A4
		// (set) Token: 0x0600189E RID: 6302 RVA: 0x0000E258 File Offset: 0x0000C458
		public int LoanId
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

		// Token: 0x17000A30 RID: 2608
		// (get) Token: 0x0600189F RID: 6303 RVA: 0x0001D6BC File Offset: 0x0001B8BC
		// (set) Token: 0x060018A0 RID: 6304 RVA: 0x0001D6C4 File Offset: 0x0001B8C4
		public InventoryProduct LoanedProduct { get; set; }

		// Token: 0x17000A31 RID: 2609
		// (get) Token: 0x060018A1 RID: 6305 RVA: 0x0001D6CD File Offset: 0x0001B8CD
		// (set) Token: 0x060018A2 RID: 6306 RVA: 0x0001D6D5 File Offset: 0x0001B8D5
		public InventoryLoanGroup Group { get; set; }
	}
}
