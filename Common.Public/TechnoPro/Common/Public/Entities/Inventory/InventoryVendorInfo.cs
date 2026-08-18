using System;

namespace TechnoPro.Common.Public.Entities.Inventory
{
	// Token: 0x02000311 RID: 785
	public class InventoryVendorInfo : BusinessBase<string>
	{
		// Token: 0x17000A1E RID: 2590
		// (get) Token: 0x06001877 RID: 6263 RVA: 0x0001D56C File Offset: 0x0001B76C
		// (set) Token: 0x06001878 RID: 6264 RVA: 0x0000E9FC File Offset: 0x0000CBFC
		public string VendorName
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

		// Token: 0x17000A1F RID: 2591
		// (get) Token: 0x06001879 RID: 6265 RVA: 0x0001D584 File Offset: 0x0001B784
		// (set) Token: 0x0600187A RID: 6266 RVA: 0x0001D58C File Offset: 0x0001B78C
		public DateTime? PurchaseDate { get; set; }

		// Token: 0x17000A20 RID: 2592
		// (get) Token: 0x0600187B RID: 6267 RVA: 0x0001D595 File Offset: 0x0001B795
		// (set) Token: 0x0600187C RID: 6268 RVA: 0x0001D59D File Offset: 0x0001B79D
		public double PurchaseAmount { get; set; }

		// Token: 0x17000A21 RID: 2593
		// (get) Token: 0x0600187D RID: 6269 RVA: 0x0001D5A6 File Offset: 0x0001B7A6
		// (set) Token: 0x0600187E RID: 6270 RVA: 0x0001D5AE File Offset: 0x0001B7AE
		public DateTime? WarrantyExpDate { get; set; }

		// Token: 0x17000A22 RID: 2594
		// (get) Token: 0x0600187F RID: 6271 RVA: 0x0001D5B7 File Offset: 0x0001B7B7
		// (set) Token: 0x06001880 RID: 6272 RVA: 0x0001D5BF File Offset: 0x0001B7BF
		public string PurchaseInfo { get; set; }
	}
}
