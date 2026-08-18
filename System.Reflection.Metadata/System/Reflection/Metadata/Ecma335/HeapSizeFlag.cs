using System;

namespace System.Reflection.Metadata.Ecma335
{
	// Token: 0x020000B9 RID: 185
	internal enum HeapSizeFlag : byte
	{
		// Token: 0x040004EE RID: 1262
		StringHeapLarge = 1,
		// Token: 0x040004EF RID: 1263
		GuidHeapLarge,
		// Token: 0x040004F0 RID: 1264
		BlobHeapLarge = 4,
		// Token: 0x040004F1 RID: 1265
		EnCDeltas = 32,
		// Token: 0x040004F2 RID: 1266
		DeletedMarks = 128
	}
}
