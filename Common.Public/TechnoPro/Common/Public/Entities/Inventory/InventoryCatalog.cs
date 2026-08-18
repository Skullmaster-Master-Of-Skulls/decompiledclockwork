using System;
using System.Collections.Generic;
using TechnoPro.Common.Public.Entities.People;

namespace TechnoPro.Common.Public.Entities.Inventory
{
	// Token: 0x0200030D RID: 781
	public class InventoryCatalog : BusinessBase<int>
	{
		// Token: 0x17000A02 RID: 2562
		// (get) Token: 0x06001838 RID: 6200 RVA: 0x0001D2D0 File Offset: 0x0001B4D0
		// (set) Token: 0x06001839 RID: 6201 RVA: 0x0000E258 File Offset: 0x0000C458
		public int InventoryCatalogId
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

		// Token: 0x17000A03 RID: 2563
		// (get) Token: 0x0600183A RID: 6202 RVA: 0x0001D2E8 File Offset: 0x0001B4E8
		// (set) Token: 0x0600183B RID: 6203 RVA: 0x0001D2F0 File Offset: 0x0001B4F0
		public IList<InventoryCategory> Categories { get; set; }

		// Token: 0x17000A04 RID: 2564
		// (get) Token: 0x0600183C RID: 6204 RVA: 0x0001D2F9 File Offset: 0x0001B4F9
		// (set) Token: 0x0600183D RID: 6205 RVA: 0x0001D301 File Offset: 0x0001B501
		public string Name { get; set; }

		// Token: 0x17000A05 RID: 2565
		// (get) Token: 0x0600183E RID: 6206 RVA: 0x0001D30A File Offset: 0x0001B50A
		// (set) Token: 0x0600183F RID: 6207 RVA: 0x0001D312 File Offset: 0x0001B512
		public string Description { get; set; }

		// Token: 0x17000A06 RID: 2566
		// (get) Token: 0x06001840 RID: 6208 RVA: 0x0001D31B File Offset: 0x0001B51B
		// (set) Token: 0x06001841 RID: 6209 RVA: 0x0001D323 File Offset: 0x0001B523
		public PersonBase WhoCreated { get; set; }

		// Token: 0x17000A07 RID: 2567
		// (get) Token: 0x06001842 RID: 6210 RVA: 0x0001D32C File Offset: 0x0001B52C
		// (set) Token: 0x06001843 RID: 6211 RVA: 0x0001D334 File Offset: 0x0001B534
		public DateTime CreationDate { get; set; }
	}
}
