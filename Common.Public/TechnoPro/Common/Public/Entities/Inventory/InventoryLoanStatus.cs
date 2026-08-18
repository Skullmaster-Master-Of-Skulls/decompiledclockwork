using System;

namespace TechnoPro.Common.Public.Entities.Inventory
{
	// Token: 0x0200031A RID: 794
	public class InventoryLoanStatus : BusinessBase<int>
	{
		// Token: 0x17000A36 RID: 2614
		// (get) Token: 0x060018AD RID: 6317 RVA: 0x0001D72C File Offset: 0x0001B92C
		// (set) Token: 0x060018AE RID: 6318 RVA: 0x0000E258 File Offset: 0x0000C458
		public int LoanStatusId
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

		// Token: 0x17000A37 RID: 2615
		// (get) Token: 0x060018AF RID: 6319 RVA: 0x0001D744 File Offset: 0x0001B944
		// (set) Token: 0x060018B0 RID: 6320 RVA: 0x0001D74C File Offset: 0x0001B94C
		public string Name { get; set; }

		// Token: 0x17000A38 RID: 2616
		// (get) Token: 0x060018B1 RID: 6321 RVA: 0x0001D755 File Offset: 0x0001B955
		// (set) Token: 0x060018B2 RID: 6322 RVA: 0x0001D75D File Offset: 0x0001B95D
		public string Description { get; set; }
	}
}
