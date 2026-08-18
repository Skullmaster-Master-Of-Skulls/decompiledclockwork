using System;

namespace System.Reflection.Metadata.Ecma335
{
	// Token: 0x0200006C RID: 108
	internal enum HeapSizeFlag : byte
	{
		// Token: 0x040003AF RID: 943
		StringHeapLarge = 1,
		// Token: 0x040003B0 RID: 944
		GuidHeapLarge,
		// Token: 0x040003B1 RID: 945
		BlobHeapLarge = 4,
		// Token: 0x040003B2 RID: 946
		EncDeltas = 32,
		// Token: 0x040003B3 RID: 947
		DeletedMarks = 128
	}
}
