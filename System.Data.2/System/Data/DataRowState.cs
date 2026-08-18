using System;

namespace System.Data
{
	// Token: 0x020000C5 RID: 197
	[Flags]
	public enum DataRowState
	{
		// Token: 0x04000369 RID: 873
		Detached = 1,
		// Token: 0x0400036A RID: 874
		Unchanged = 2,
		// Token: 0x0400036B RID: 875
		Added = 4,
		// Token: 0x0400036C RID: 876
		Deleted = 8,
		// Token: 0x0400036D RID: 877
		Modified = 16
	}
}
