using System;
using System.ComponentModel;

namespace System.Data
{
	// Token: 0x02000114 RID: 276
	[Flags]
	[Obsolete("PropertyAttributes has been deprecated.  http://go.microsoft.com/fwlink/?linkid=14202")]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public enum PropertyAttributes
	{
		// Token: 0x04000597 RID: 1431
		NotSupported = 0,
		// Token: 0x04000598 RID: 1432
		Required = 1,
		// Token: 0x04000599 RID: 1433
		Optional = 2,
		// Token: 0x0400059A RID: 1434
		Read = 512,
		// Token: 0x0400059B RID: 1435
		Write = 1024
	}
}
