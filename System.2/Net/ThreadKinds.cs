using System;

namespace System.Net
{
	// Token: 0x020001C2 RID: 450
	[Flags]
	internal enum ThreadKinds
	{
		// Token: 0x0400146E RID: 5230
		Unknown = 0,
		// Token: 0x0400146F RID: 5231
		User = 1,
		// Token: 0x04001470 RID: 5232
		System = 2,
		// Token: 0x04001471 RID: 5233
		Sync = 4,
		// Token: 0x04001472 RID: 5234
		Async = 8,
		// Token: 0x04001473 RID: 5235
		Timer = 16,
		// Token: 0x04001474 RID: 5236
		CompletionPort = 32,
		// Token: 0x04001475 RID: 5237
		Worker = 64,
		// Token: 0x04001476 RID: 5238
		Finalization = 128,
		// Token: 0x04001477 RID: 5239
		Other = 256,
		// Token: 0x04001478 RID: 5240
		OwnerMask = 3,
		// Token: 0x04001479 RID: 5241
		SyncMask = 12,
		// Token: 0x0400147A RID: 5242
		SourceMask = 496,
		// Token: 0x0400147B RID: 5243
		SafeSources = 352,
		// Token: 0x0400147C RID: 5244
		ThreadPool = 96
	}
}
