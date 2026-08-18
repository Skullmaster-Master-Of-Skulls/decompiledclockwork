using System;

namespace TechnoPro.Common.Public.Entities.People
{
	// Token: 0x02000257 RID: 599
	public class BasicPerson : BusinessBase<int>
	{
		// Token: 0x17000774 RID: 1908
		// (get) Token: 0x06001217 RID: 4631 RVA: 0x000187D8 File Offset: 0x000169D8
		// (set) Token: 0x06001218 RID: 4632 RVA: 0x0000E258 File Offset: 0x0000C458
		public virtual int PersonId
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

		// Token: 0x17000775 RID: 1909
		// (get) Token: 0x06001219 RID: 4633 RVA: 0x000187F0 File Offset: 0x000169F0
		// (set) Token: 0x0600121A RID: 4634 RVA: 0x000187F8 File Offset: 0x000169F8
		public string FirstName { get; set; }

		// Token: 0x17000776 RID: 1910
		// (get) Token: 0x0600121B RID: 4635 RVA: 0x00018801 File Offset: 0x00016A01
		// (set) Token: 0x0600121C RID: 4636 RVA: 0x00018809 File Offset: 0x00016A09
		public string MiddleName { get; set; }

		// Token: 0x17000777 RID: 1911
		// (get) Token: 0x0600121D RID: 4637 RVA: 0x00018812 File Offset: 0x00016A12
		// (set) Token: 0x0600121E RID: 4638 RVA: 0x0001881A File Offset: 0x00016A1A
		public string LastName { get; set; }

		// Token: 0x17000778 RID: 1912
		// (get) Token: 0x0600121F RID: 4639 RVA: 0x00018823 File Offset: 0x00016A23
		// (set) Token: 0x06001220 RID: 4640 RVA: 0x0001882B File Offset: 0x00016A2B
		public string StudentNumber { get; set; }
	}
}
