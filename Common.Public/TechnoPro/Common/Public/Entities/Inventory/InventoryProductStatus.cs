using System;

namespace TechnoPro.Common.Public.Entities.Inventory
{
	// Token: 0x02000312 RID: 786
	public class InventoryProductStatus : BusinessBase<int>
	{
		// Token: 0x17000A23 RID: 2595
		// (get) Token: 0x06001882 RID: 6274 RVA: 0x0001D5C8 File Offset: 0x0001B7C8
		// (set) Token: 0x06001883 RID: 6275 RVA: 0x0000E258 File Offset: 0x0000C458
		public int ProductStatusId
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

		// Token: 0x17000A24 RID: 2596
		// (get) Token: 0x06001884 RID: 6276 RVA: 0x0001D5E0 File Offset: 0x0001B7E0
		// (set) Token: 0x06001885 RID: 6277 RVA: 0x0001D5E8 File Offset: 0x0001B7E8
		public string Name { get; set; }

		// Token: 0x17000A25 RID: 2597
		// (get) Token: 0x06001886 RID: 6278 RVA: 0x0001D5F1 File Offset: 0x0001B7F1
		// (set) Token: 0x06001887 RID: 6279 RVA: 0x0001D5F9 File Offset: 0x0001B7F9
		public string Description { get; set; }
	}
}
