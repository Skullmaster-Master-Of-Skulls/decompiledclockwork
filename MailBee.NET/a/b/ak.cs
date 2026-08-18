using System;

namespace a.b
{
	// Token: 0x02000327 RID: 807
	internal static class ak
	{
		// Token: 0x06001D26 RID: 7462 RVA: 0x0007E614 File Offset: 0x0007D614
		public static int a(int A_0, int A_1)
		{
			if (A_0 > 0)
			{
				return A_0 >> A_1;
			}
			return (int)((uint)A_0 >> A_1);
		}

		// Token: 0x06001D27 RID: 7463 RVA: 0x0007E627 File Offset: 0x0007D627
		public static long a(long A_0, int A_1)
		{
			if (A_0 > 0L)
			{
				return A_0 >> A_1;
			}
			return (long)((ulong)A_0 >> A_1);
		}

		// Token: 0x06001D28 RID: 7464 RVA: 0x0007E63B File Offset: 0x0007D63B
		public static short a(short A_0, int A_1)
		{
			if (A_0 > 0)
			{
				return (short)(A_0 >> A_1);
			}
			return (short)((ushort)A_0 >> A_1);
		}

		// Token: 0x06001D29 RID: 7465 RVA: 0x0007E651 File Offset: 0x0007D651
		public static sbyte a(sbyte A_0, int A_1)
		{
			if (A_0 > 0)
			{
				return (sbyte)(A_0 >> A_1);
			}
			return (sbyte)((byte)A_0 >> A_1);
		}
	}
}
