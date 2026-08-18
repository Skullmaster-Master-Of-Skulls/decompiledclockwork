using System;

namespace System.Reflection.Metadata.Ecma335
{
	// Token: 0x02000072 RID: 114
	internal static class HeapHandleType
	{
		// Token: 0x060002FB RID: 763 RVA: 0x00007A5F File Offset: 0x00005C5F
		internal static bool IsValidHeapOffset(uint offset)
		{
			return (offset & 3758096384U) == 0U;
		}

		// Token: 0x04000404 RID: 1028
		internal const int OffsetBitCount = 29;

		// Token: 0x04000405 RID: 1029
		internal const uint OffsetMask = 536870911U;

		// Token: 0x04000406 RID: 1030
		internal const uint VirtualBit = 2147483648U;
	}
}
