using System;

namespace a.b
{
	// Token: 0x02000245 RID: 581
	internal class fv
	{
		// Token: 0x0600137B RID: 4987 RVA: 0x00058598 File Offset: 0x00057598
		public static bool a(byte[] A_0, byte[] A_1)
		{
			if (A_0.Length != A_1.Length)
			{
				return false;
			}
			for (int i = 0; i < A_0.Length; i++)
			{
				if (A_0[i] != A_1[i])
				{
					return false;
				}
			}
			return true;
		}
	}
}
