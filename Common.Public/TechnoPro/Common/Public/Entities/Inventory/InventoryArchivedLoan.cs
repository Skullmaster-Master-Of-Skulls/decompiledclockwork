using System;
using TechnoPro.Common.Public.Entities.People;

namespace TechnoPro.Common.Public.Entities.Inventory
{
	// Token: 0x0200031C RID: 796
	public class InventoryArchivedLoan : BusinessBase<int>
	{
		// Token: 0x17000A40 RID: 2624
		// (get) Token: 0x060018C3 RID: 6339 RVA: 0x0001D7E8 File Offset: 0x0001B9E8
		// (set) Token: 0x060018C4 RID: 6340 RVA: 0x0000E258 File Offset: 0x0000C458
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

		// Token: 0x17000A41 RID: 2625
		// (get) Token: 0x060018C5 RID: 6341 RVA: 0x0001D800 File Offset: 0x0001BA00
		// (set) Token: 0x060018C6 RID: 6342 RVA: 0x0001D808 File Offset: 0x0001BA08
		public InventoryLoanGroup Group { get; set; }

		// Token: 0x17000A42 RID: 2626
		// (get) Token: 0x060018C7 RID: 6343 RVA: 0x0001D811 File Offset: 0x0001BA11
		// (set) Token: 0x060018C8 RID: 6344 RVA: 0x0001D819 File Offset: 0x0001BA19
		public InventoryProductSnapshot LoanedProduct { get; set; }

		// Token: 0x17000A43 RID: 2627
		// (get) Token: 0x060018C9 RID: 6345 RVA: 0x0001D822 File Offset: 0x0001BA22
		// (set) Token: 0x060018CA RID: 6346 RVA: 0x0001D82A File Offset: 0x0001BA2A
		public PersonBase WhoReturned { get; set; }

		// Token: 0x17000A44 RID: 2628
		// (get) Token: 0x060018CB RID: 6347 RVA: 0x0001D833 File Offset: 0x0001BA33
		// (set) Token: 0x060018CC RID: 6348 RVA: 0x0001D83B File Offset: 0x0001BA3B
		public string ReturnedNotes { get; set; }

		// Token: 0x17000A45 RID: 2629
		// (get) Token: 0x060018CD RID: 6349 RVA: 0x0001D844 File Offset: 0x0001BA44
		// (set) Token: 0x060018CE RID: 6350 RVA: 0x0001D84C File Offset: 0x0001BA4C
		public DateTime ReturnedDate { get; set; }

		// Token: 0x17000A46 RID: 2630
		// (get) Token: 0x060018CF RID: 6351 RVA: 0x0001D855 File Offset: 0x0001BA55
		// (set) Token: 0x060018D0 RID: 6352 RVA: 0x0001D85D File Offset: 0x0001BA5D
		public InventoryLoanStatus ReturnedStatus { get; set; }
	}
}
