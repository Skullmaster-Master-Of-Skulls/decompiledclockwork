using System;

namespace System.Net
{
	// Token: 0x020004EB RID: 1259
	[Flags]
	internal enum ThreadKinds
	{
		// Token: 0x040026B1 RID: 9905
		Unknown = 0,
		// Token: 0x040026B2 RID: 9906
		User = 1,
		// Token: 0x040026B3 RID: 9907
		System = 2,
		// Token: 0x040026B4 RID: 9908
		Sync = 4,
		// Token: 0x040026B5 RID: 9909
		Async = 8,
		// Token: 0x040026B6 RID: 9910
		Timer = 16,
		// Token: 0x040026B7 RID: 9911
		CompletionPort = 32,
		// Token: 0x040026B8 RID: 9912
		Worker = 64,
		// Token: 0x040026B9 RID: 9913
		Finalization = 128,
		// Token: 0x040026BA RID: 9914
		Other = 256,
		// Token: 0x040026BB RID: 9915
		OwnerMask = 3,
		// Token: 0x040026BC RID: 9916
		SyncMask = 12,
		// Token: 0x040026BD RID: 9917
		SourceMask = 496,
		// Token: 0x040026BE RID: 9918
		SafeSources = 352,
		// Token: 0x040026BF RID: 9919
		ThreadPool = 96
	}
}
