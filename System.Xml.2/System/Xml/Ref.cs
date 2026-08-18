using System;
using System.Runtime.CompilerServices;

namespace System.Xml
{
	// Token: 0x02000077 RID: 119
	internal static class Ref
	{
		// Token: 0x060003EC RID: 1004 RVA: 0x0000F454 File Offset: 0x0000D654
		public static bool Equal(string strA, string strB)
		{
			return strA == strB;
		}

		// Token: 0x060003ED RID: 1005 RVA: 0x0000F45A File Offset: 0x0000D65A
		public new static void Equals(object objA, object objB)
		{
		}

		// Token: 0x060003EE RID: 1006 RVA: 0x0000F45C File Offset: 0x0000D65C
		internal static int CombineHash(int h1, int h2)
		{
			return (h1 << 5) + h1 ^ h2;
		}

		// Token: 0x060003EF RID: 1007 RVA: 0x0000F465 File Offset: 0x0000D665
		internal static int CombineHashRef(int h, object obj)
		{
			return Ref.CombineHash(h, (obj != null) ? RuntimeHelpers.GetHashCode(obj) : 0);
		}
	}
}
