using System;

namespace System.Reflection.PortableExecutable
{
	// Token: 0x0200002A RID: 42
	[Flags]
	public enum PEStreamOptions
	{
		// Token: 0x0400017A RID: 378
		Default = 0,
		// Token: 0x0400017B RID: 379
		LeaveOpen = 1,
		// Token: 0x0400017C RID: 380
		PrefetchMetadata = 2,
		// Token: 0x0400017D RID: 381
		PrefetchEntireImage = 4
	}
}
