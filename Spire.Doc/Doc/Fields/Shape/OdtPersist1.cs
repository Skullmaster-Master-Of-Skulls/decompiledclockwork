using System;

namespace Spire.Doc.Fields.Shape
{
	// Token: 0x0200004B RID: 75
	[Flags]
	internal enum OdtPersist1
	{
		// Token: 0x04000364 RID: 868
		None = 0,
		// Token: 0x04000365 RID: 869
		Reserved1 = 1,
		// Token: 0x04000366 RID: 870
		DefHandler = 2,
		// Token: 0x04000367 RID: 871
		Reserved2 = 4,
		// Token: 0x04000368 RID: 872
		Reserved3 = 8,
		// Token: 0x04000369 RID: 873
		Link = 16,
		// Token: 0x0400036A RID: 874
		Reserved4 = 32,
		// Token: 0x0400036B RID: 875
		Icon = 64,
		// Token: 0x0400036C RID: 876
		Ole1 = 128,
		// Token: 0x0400036D RID: 877
		Manual = 256,
		// Token: 0x0400036E RID: 878
		RecomposeOnResize = 512,
		// Token: 0x0400036F RID: 879
		Reserved5 = 1024,
		// Token: 0x04000370 RID: 880
		Reserved6 = 2048,
		// Token: 0x04000371 RID: 881
		Ocx = 4096,
		// Token: 0x04000372 RID: 882
		Stream = 8192,
		// Token: 0x04000373 RID: 883
		Reserved7 = 16384,
		// Token: 0x04000374 RID: 884
		SupportIViewObject = 32768
	}
}
