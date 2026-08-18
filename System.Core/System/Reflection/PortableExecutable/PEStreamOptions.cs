using System;

namespace System.Reflection.PortableExecutable
{
	// Token: 0x0200004E RID: 78
	[Flags]
	internal enum PEStreamOptions
	{
		// Token: 0x040002E4 RID: 740
		Default = 0,
		// Token: 0x040002E5 RID: 741
		LeaveOpen = 1,
		// Token: 0x040002E6 RID: 742
		PrefetchMetadata = 2,
		// Token: 0x040002E7 RID: 743
		PrefetchEntireImage = 4,
		// Token: 0x040002E8 RID: 744
		IsLoadedImage = 8
	}
}
