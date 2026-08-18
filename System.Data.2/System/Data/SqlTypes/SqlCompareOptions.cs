using System;

namespace System.Data.SqlTypes
{
	// Token: 0x02000167 RID: 359
	[Flags]
	[Serializable]
	public enum SqlCompareOptions
	{
		// Token: 0x04000E25 RID: 3621
		None = 0,
		// Token: 0x04000E26 RID: 3622
		IgnoreCase = 1,
		// Token: 0x04000E27 RID: 3623
		IgnoreNonSpace = 2,
		// Token: 0x04000E28 RID: 3624
		IgnoreKanaType = 8,
		// Token: 0x04000E29 RID: 3625
		IgnoreWidth = 16,
		// Token: 0x04000E2A RID: 3626
		BinarySort = 32768,
		// Token: 0x04000E2B RID: 3627
		BinarySort2 = 16384
	}
}
