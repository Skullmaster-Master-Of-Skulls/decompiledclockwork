using System;

namespace TechnoPro.Common.Public.Entities.Inventory
{
	// Token: 0x0200030E RID: 782
	public class InventoryCategory : BusinessBase<string>
	{
		// Token: 0x17000A08 RID: 2568
		// (get) Token: 0x06001845 RID: 6213 RVA: 0x0001D340 File Offset: 0x0001B540
		// (set) Token: 0x06001846 RID: 6214 RVA: 0x0000E9FC File Offset: 0x0000CBFC
		public string CategoryName
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

		// Token: 0x17000A09 RID: 2569
		// (get) Token: 0x06001847 RID: 6215 RVA: 0x0001D358 File Offset: 0x0001B558
		// (set) Token: 0x06001848 RID: 6216 RVA: 0x0001D360 File Offset: 0x0001B560
		public int DynamicFormId { get; set; }

		// Token: 0x17000A0A RID: 2570
		// (get) Token: 0x06001849 RID: 6217 RVA: 0x0001D369 File Offset: 0x0001B569
		// (set) Token: 0x0600184A RID: 6218 RVA: 0x0001D371 File Offset: 0x0001B571
		public int CatalogId { get; set; }
	}
}
