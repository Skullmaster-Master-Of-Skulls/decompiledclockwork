using System;

namespace System.Reflection.Metadata.Ecma335
{
	// Token: 0x020000D8 RID: 216
	internal static class HeapHandleType
	{
		// Token: 0x0600087B RID: 2171 RVA: 0x00017327 File Offset: 0x00015527
		internal static bool IsValidHeapOffset(uint offset)
		{
			return (offset & 3758096384U) == 0U;
		}

		// Token: 0x0400061C RID: 1564
		internal const int OffsetBitCount = 29;

		// Token: 0x0400061D RID: 1565
		internal const uint OffsetMask = 536870911U;

		// Token: 0x0400061E RID: 1566
		internal const uint VirtualBit = 2147483648U;
	}
}
