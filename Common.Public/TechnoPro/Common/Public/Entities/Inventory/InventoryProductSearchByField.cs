using System;

namespace TechnoPro.Common.Public.Entities.Inventory
{
	// Token: 0x02000316 RID: 790
	[Flags]
	[Serializable]
	public enum InventoryProductSearchByField
	{
		// Token: 0x04001447 RID: 5191
		None = 0,
		// Token: 0x04001448 RID: 5192
		ProductName = 1,
		// Token: 0x04001449 RID: 5193
		SerialNumber = 2,
		// Token: 0x0400144A RID: 5194
		BarCode = 4,
		// Token: 0x0400144B RID: 5195
		CategoryName = 8,
		// Token: 0x0400144C RID: 5196
		GroupName = 16,
		// Token: 0x0400144D RID: 5197
		All = 31
	}
}
