using System;

namespace TechnoPro.Common.Public.Entities.Inventory
{
	// Token: 0x02000310 RID: 784
	public class InventoryProductAccessory : BusinessBase<string>
	{
		// Token: 0x17000A1C RID: 2588
		// (get) Token: 0x06001872 RID: 6258 RVA: 0x0001D540 File Offset: 0x0001B740
		// (set) Token: 0x06001873 RID: 6259 RVA: 0x0000E9FC File Offset: 0x0000CBFC
		public string Name
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

		// Token: 0x17000A1D RID: 2589
		// (get) Token: 0x06001874 RID: 6260 RVA: 0x0001D558 File Offset: 0x0001B758
		// (set) Token: 0x06001875 RID: 6261 RVA: 0x0001D560 File Offset: 0x0001B760
		public string Description { get; set; }
	}
}
