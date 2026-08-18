using System;

namespace TechnoPro.Common.Public.Entities.Inventory
{
	// Token: 0x02000317 RID: 791
	[Flags]
	[Serializable]
	public enum eInventoryProductSnapshotReason
	{
		// Token: 0x0400144F RID: 5199
		None = 0,
		// Token: 0x04001450 RID: 5200
		Product_Deleted = 1,
		// Token: 0x04001451 RID: 5201
		Product_Created = 2,
		// Token: 0x04001452 RID: 5202
		Location_Changed = 4,
		// Token: 0x04001453 RID: 5203
		Properties_Changed = 8,
		// Token: 0x04001454 RID: 5204
		Returned_Loan = 16,
		// Token: 0x04001455 RID: 5205
		Product_Loaned = 32,
		// Token: 0x04001456 RID: 5206
		All = 63
	}
}
