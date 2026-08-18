using System;
using TechnoPro.Common.Public.Entities.People;

namespace TechnoPro.Common.Public.Entities.Inventory
{
	// Token: 0x0200031B RID: 795
	public class InventoryLoanGroup : BusinessBase<int>
	{
		// Token: 0x17000A39 RID: 2617
		// (get) Token: 0x060018B4 RID: 6324 RVA: 0x0001D768 File Offset: 0x0001B968
		// (set) Token: 0x060018B5 RID: 6325 RVA: 0x0000E258 File Offset: 0x0000C458
		public int LoanGroupId
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

		// Token: 0x17000A3A RID: 2618
		// (get) Token: 0x060018B6 RID: 6326 RVA: 0x0001D780 File Offset: 0x0001B980
		// (set) Token: 0x060018B7 RID: 6327 RVA: 0x0001D788 File Offset: 0x0001B988
		public DateTime LoanedDate { get; set; }

		// Token: 0x17000A3B RID: 2619
		// (get) Token: 0x060018B8 RID: 6328 RVA: 0x0001D791 File Offset: 0x0001B991
		// (set) Token: 0x060018B9 RID: 6329 RVA: 0x0001D799 File Offset: 0x0001B999
		public DateTime DueDate { get; set; }

		// Token: 0x17000A3C RID: 2620
		// (get) Token: 0x060018BA RID: 6330 RVA: 0x0001D7A2 File Offset: 0x0001B9A2
		// (set) Token: 0x060018BB RID: 6331 RVA: 0x0001D7AA File Offset: 0x0001B9AA
		public string LoanNotes { get; set; }

		// Token: 0x17000A3D RID: 2621
		// (get) Token: 0x060018BC RID: 6332 RVA: 0x0001D7B3 File Offset: 0x0001B9B3
		// (set) Token: 0x060018BD RID: 6333 RVA: 0x0001D7BB File Offset: 0x0001B9BB
		public PersonBase LoanedTo { get; set; }

		// Token: 0x17000A3E RID: 2622
		// (get) Token: 0x060018BE RID: 6334 RVA: 0x0001D7C4 File Offset: 0x0001B9C4
		// (set) Token: 0x060018BF RID: 6335 RVA: 0x0001D7CC File Offset: 0x0001B9CC
		public PersonBase WhoLoaned { get; set; }

		// Token: 0x17000A3F RID: 2623
		// (get) Token: 0x060018C0 RID: 6336 RVA: 0x0001D7D5 File Offset: 0x0001B9D5
		// (set) Token: 0x060018C1 RID: 6337 RVA: 0x0001D7DD File Offset: 0x0001B9DD
		public InventoryLocation Location { get; set; }
	}
}
