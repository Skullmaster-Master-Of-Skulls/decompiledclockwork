using System;
using System.Diagnostics.CodeAnalysis;

namespace System.Data.Entity
{
	// Token: 0x020006B1 RID: 1713
	[Flags]
	[SuppressMessage("Microsoft.Naming", "CA1714:FlagsEnumsShouldHavePluralNames")]
	public enum EntityState
	{
		// Token: 0x0400192B RID: 6443
		Detached = 1,
		// Token: 0x0400192C RID: 6444
		Unchanged = 2,
		// Token: 0x0400192D RID: 6445
		Added = 4,
		// Token: 0x0400192E RID: 6446
		Deleted = 8,
		// Token: 0x0400192F RID: 6447
		Modified = 16
	}
}
