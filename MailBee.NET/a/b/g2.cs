using System;

namespace a.b
{
	// Token: 0x02000328 RID: 808
	internal class g2
	{
		// Token: 0x06001D2A RID: 7466 RVA: 0x0007E667 File Offset: 0x0007D667
		private g2()
		{
		}

		// Token: 0x06001D2B RID: 7467 RVA: 0x0007E670 File Offset: 0x0007D670
		public static bool a(byte[] A_0, int A_1)
		{
			if (A_0 == null || A_0.Length - A_1 < g2.a.Length)
			{
				return false;
			}
			for (int i = 0; i < g2.a.Length; i++)
			{
				if (g2.a[i] != A_0[i + A_1])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0400137C RID: 4988
		private static byte[] a = new byte[]
		{
			137,
			80,
			78,
			71,
			13,
			10,
			26,
			10
		};
	}
}
