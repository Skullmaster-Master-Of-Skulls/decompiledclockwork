using System;

namespace System.Data.SqlTypes
{
	// Token: 0x02000356 RID: 854
	[Flags]
	[Serializable]
	public enum SqlCompareOptions
	{
		// Token: 0x04001D3C RID: 7484
		None = 0,
		// Token: 0x04001D3D RID: 7485
		IgnoreCase = 1,
		// Token: 0x04001D3E RID: 7486
		IgnoreNonSpace = 2,
		// Token: 0x04001D3F RID: 7487
		IgnoreKanaType = 8,
		// Token: 0x04001D40 RID: 7488
		IgnoreWidth = 16,
		// Token: 0x04001D41 RID: 7489
		BinarySort = 32768,
		// Token: 0x04001D42 RID: 7490
		BinarySort2 = 16384
	}
}
