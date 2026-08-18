using System;

namespace System.Security.Cryptography
{
	// Token: 0x02000106 RID: 262
	[Flags]
	public enum CngExportPolicies
	{
		// Token: 0x04000684 RID: 1668
		None = 0,
		// Token: 0x04000685 RID: 1669
		AllowExport = 1,
		// Token: 0x04000686 RID: 1670
		AllowPlaintextExport = 2,
		// Token: 0x04000687 RID: 1671
		AllowArchiving = 4,
		// Token: 0x04000688 RID: 1672
		AllowPlaintextArchiving = 8
	}
}
