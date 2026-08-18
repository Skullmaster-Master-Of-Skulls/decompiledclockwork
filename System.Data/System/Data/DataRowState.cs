using System;

namespace System.Data
{
	// Token: 0x02000090 RID: 144
	[Flags]
	public enum DataRowState
	{
		// Token: 0x04000789 RID: 1929
		Detached = 1,
		// Token: 0x0400078A RID: 1930
		Unchanged = 2,
		// Token: 0x0400078B RID: 1931
		Added = 4,
		// Token: 0x0400078C RID: 1932
		Deleted = 8,
		// Token: 0x0400078D RID: 1933
		Modified = 16
	}
}
