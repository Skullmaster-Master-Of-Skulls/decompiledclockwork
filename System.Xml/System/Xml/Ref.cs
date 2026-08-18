using System;
using System.Runtime.CompilerServices;

namespace System.Xml
{
	// Token: 0x02000027 RID: 39
	internal abstract class Ref
	{
		// Token: 0x060000C5 RID: 197 RVA: 0x000046C1 File Offset: 0x000036C1
		public static bool Equal(string strA, string strB)
		{
			return strA == strB;
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x000046C7 File Offset: 0x000036C7
		internal static int CombineHash(int h1, int h2)
		{
			return (h1 << 5) + h1 ^ h2;
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x000046D0 File Offset: 0x000036D0
		internal static int CombineHashRef(int h, object obj)
		{
			return Ref.CombineHash(h, (obj != null) ? RuntimeHelpers.GetHashCode(obj) : 0);
		}
	}
}
