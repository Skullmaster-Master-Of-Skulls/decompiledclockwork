using System;
using TechnoPro.Common.Public.Entities.People;

namespace TechnoPro.Common.Public.Entities.Inventory
{
	// Token: 0x02000319 RID: 793
	public class InventoryReturnedLoan : InventoryLoan
	{
		// Token: 0x17000A32 RID: 2610
		// (get) Token: 0x060018A4 RID: 6308 RVA: 0x0001D6DE File Offset: 0x0001B8DE
		// (set) Token: 0x060018A5 RID: 6309 RVA: 0x0001D6E6 File Offset: 0x0001B8E6
		public PersonBase WhoReturned { get; set; }

		// Token: 0x17000A33 RID: 2611
		// (get) Token: 0x060018A6 RID: 6310 RVA: 0x0001D6EF File Offset: 0x0001B8EF
		// (set) Token: 0x060018A7 RID: 6311 RVA: 0x0001D6F7 File Offset: 0x0001B8F7
		public string ReturnedNotes { get; set; }

		// Token: 0x17000A34 RID: 2612
		// (get) Token: 0x060018A8 RID: 6312 RVA: 0x0001D700 File Offset: 0x0001B900
		// (set) Token: 0x060018A9 RID: 6313 RVA: 0x0001D708 File Offset: 0x0001B908
		public DateTime ReturnedDate { get; set; }

		// Token: 0x17000A35 RID: 2613
		// (get) Token: 0x060018AA RID: 6314 RVA: 0x0001D711 File Offset: 0x0001B911
		// (set) Token: 0x060018AB RID: 6315 RVA: 0x0001D719 File Offset: 0x0001B919
		public InventoryLoanStatus ReturnedStatus { get; set; }
	}
}
