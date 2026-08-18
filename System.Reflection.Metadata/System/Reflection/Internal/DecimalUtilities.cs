using System;

namespace System.Reflection.Internal
{
	// Token: 0x02000150 RID: 336
	internal static class DecimalUtilities
	{
		// Token: 0x06000A98 RID: 2712 RVA: 0x0001E726 File Offset: 0x0001C926
		public static int GetScale(this decimal value)
		{
			return (int)((byte)(decimal.GetBits(value)[3] >> 16));
		}

		// Token: 0x06000A99 RID: 2713 RVA: 0x0001E734 File Offset: 0x0001C934
		public static void GetBits(this decimal value, out bool isNegative, out byte scale, out uint low, out uint mid, out uint high)
		{
			int[] bits = decimal.GetBits(value);
			low = (uint)bits[0];
			mid = (uint)bits[1];
			high = (uint)bits[2];
			scale = (byte)(bits[3] >> 16);
			isNegative = (((long)bits[3] & (long)((ulong)int.MinValue)) != 0L);
		}
	}
}
