using System;

namespace System.Xml
{
	// Token: 0x02000068 RID: 104
	internal static class Bits
	{
		// Token: 0x06000397 RID: 919 RVA: 0x0000E690 File Offset: 0x0000C890
		public static int Count(uint num)
		{
			num = (num & Bits.MASK_0101010101010101) + (num >> 1 & Bits.MASK_0101010101010101);
			num = (num & Bits.MASK_0011001100110011) + (num >> 2 & Bits.MASK_0011001100110011);
			num = (num & Bits.MASK_0000111100001111) + (num >> 4 & Bits.MASK_0000111100001111);
			num = (num & Bits.MASK_0000000011111111) + (num >> 8 & Bits.MASK_0000000011111111);
			num = (num & Bits.MASK_1111111111111111) + (num >> 16);
			return (int)num;
		}

		// Token: 0x06000398 RID: 920 RVA: 0x0000E6F8 File Offset: 0x0000C8F8
		public static bool ExactlyOne(uint num)
		{
			return num != 0U && (num & num - 1U) == 0U;
		}

		// Token: 0x06000399 RID: 921 RVA: 0x0000E707 File Offset: 0x0000C907
		public static bool MoreThanOne(uint num)
		{
			return (num & num - 1U) > 0U;
		}

		// Token: 0x0600039A RID: 922 RVA: 0x0000E711 File Offset: 0x0000C911
		public static uint ClearLeast(uint num)
		{
			return num & num - 1U;
		}

		// Token: 0x0600039B RID: 923 RVA: 0x0000E718 File Offset: 0x0000C918
		public static int LeastPosition(uint num)
		{
			if (num == 0U)
			{
				return 0;
			}
			return Bits.Count(num ^ num - 1U);
		}

		// Token: 0x040001A9 RID: 425
		private static readonly uint MASK_0101010101010101 = 1431655765U;

		// Token: 0x040001AA RID: 426
		private static readonly uint MASK_0011001100110011 = 858993459U;

		// Token: 0x040001AB RID: 427
		private static readonly uint MASK_0000111100001111 = 252645135U;

		// Token: 0x040001AC RID: 428
		private static readonly uint MASK_0000000011111111 = 16711935U;

		// Token: 0x040001AD RID: 429
		private static readonly uint MASK_1111111111111111 = 65535U;
	}
}
