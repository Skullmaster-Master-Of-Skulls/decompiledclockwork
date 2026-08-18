using System;
using System.Text;

namespace a
{
	// Token: 0x020000EF RID: 239
	internal class v
	{
		// Token: 0x060007EE RID: 2030 RVA: 0x00025379 File Offset: 0x00024379
		protected v()
		{
		}

		// Token: 0x060007EF RID: 2031 RVA: 0x00025384 File Offset: 0x00024384
		public static string a(byte[] A_0, Encoding A_1)
		{
			if (A_0 == null || A_1 == null)
			{
				throw new ArgumentNullException();
			}
			int num = w.b(A_0, 0, A_0.Length);
			if (num < 0)
			{
				throw new l(121, A_1.GetString(A_0, 0, A_0.Length));
			}
			return A_1.GetString(A_0, 0, num);
		}

		// Token: 0x0400054D RID: 1357
		protected static readonly char[] a = new char[]
		{
			' ',
			'\t'
		};
	}
}
