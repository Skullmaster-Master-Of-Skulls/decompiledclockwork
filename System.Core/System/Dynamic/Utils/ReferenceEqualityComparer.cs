using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace System.Dynamic.Utils
{
	// Token: 0x020000D8 RID: 216
	internal sealed class ReferenceEqualityComparer<T> : IEqualityComparer<T>
	{
		// Token: 0x0600069F RID: 1695 RVA: 0x00015B81 File Offset: 0x00013D81
		private ReferenceEqualityComparer()
		{
		}

		// Token: 0x060006A0 RID: 1696 RVA: 0x00015B89 File Offset: 0x00013D89
		public bool Equals(T x, T y)
		{
			return x == y;
		}

		// Token: 0x060006A1 RID: 1697 RVA: 0x00015B99 File Offset: 0x00013D99
		public int GetHashCode(T obj)
		{
			return RuntimeHelpers.GetHashCode(obj);
		}

		// Token: 0x040005C6 RID: 1478
		internal static readonly ReferenceEqualityComparer<T> Instance = new ReferenceEqualityComparer<T>();
	}
}
