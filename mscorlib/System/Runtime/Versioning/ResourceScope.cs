using System;

namespace System.Runtime.Versioning
{
	// Token: 0x0200094C RID: 2380
	[Flags]
	public enum ResourceScope
	{
		// Token: 0x04002CDF RID: 11487
		None = 0,
		// Token: 0x04002CE0 RID: 11488
		Machine = 1,
		// Token: 0x04002CE1 RID: 11489
		Process = 2,
		// Token: 0x04002CE2 RID: 11490
		AppDomain = 4,
		// Token: 0x04002CE3 RID: 11491
		Library = 8,
		// Token: 0x04002CE4 RID: 11492
		Private = 16,
		// Token: 0x04002CE5 RID: 11493
		Assembly = 32
	}
}
