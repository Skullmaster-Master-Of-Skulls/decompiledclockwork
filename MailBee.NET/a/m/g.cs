using System;
using System.Collections;

namespace a.m
{
	// Token: 0x02000217 RID: 535
	internal class g : IComparer
	{
		// Token: 0x0600115C RID: 4444 RVA: 0x0004D0F4 File Offset: 0x0004C0F4
		private static bool a(double A_0)
		{
			if (A_0 > 0.0)
			{
				return A_0 < 1E-07;
			}
			return A_0 > -1E-07;
		}

		// Token: 0x0600115D RID: 4445 RVA: 0x0004D11C File Offset: 0x0004C11C
		public int Compare(object x, object y)
		{
			a a = (a)x;
			a a2 = (a)y;
			if (!g.a(a.d - a2.d))
			{
				if (a.d < a2.d)
				{
					return 1;
				}
				if (a.d > a2.d)
				{
					return -1;
				}
			}
			if (!g.a(a.c - a2.c))
			{
				if (a.c < a2.c)
				{
					return 1;
				}
				if (a.c > a2.c)
				{
					return -1;
				}
			}
			if (a.b < a2.b)
			{
				return 1;
			}
			if (a.b > a2.b)
			{
				return -1;
			}
			if (a.a.Length < a2.a.Length)
			{
				return 1;
			}
			if (a.a.Length > a2.a.Length)
			{
				return -1;
			}
			return 0;
		}
	}
}
