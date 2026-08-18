using System;

namespace TechnoPro.Common.Public.Entities.Inventory
{
	// Token: 0x02000313 RID: 787
	public class InventoryGroup : BusinessBase<int>
	{
		// Token: 0x17000A26 RID: 2598
		// (get) Token: 0x06001889 RID: 6281 RVA: 0x0001D604 File Offset: 0x0001B804
		// (set) Token: 0x0600188A RID: 6282 RVA: 0x0000E258 File Offset: 0x0000C458
		public int ProductGroupId
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

		// Token: 0x17000A27 RID: 2599
		// (get) Token: 0x0600188B RID: 6283 RVA: 0x0001D61C File Offset: 0x0001B81C
		// (set) Token: 0x0600188C RID: 6284 RVA: 0x0001D624 File Offset: 0x0001B824
		public string Name { get; set; }

		// Token: 0x17000A28 RID: 2600
		// (get) Token: 0x0600188D RID: 6285 RVA: 0x0001D62D File Offset: 0x0001B82D
		// (set) Token: 0x0600188E RID: 6286 RVA: 0x0001D635 File Offset: 0x0001B835
		public string Notes { get; set; }
	}
}
