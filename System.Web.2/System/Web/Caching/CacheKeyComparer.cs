using System;
using System.Collections;

namespace System.Web.Caching
{
	// Token: 0x02000871 RID: 2161
	internal sealed class CacheKeyComparer : IEqualityComparer
	{
		// Token: 0x060065D3 RID: 26067 RVA: 0x00166AAB File Offset: 0x00164CAB
		internal static CacheKeyComparer GetInstance()
		{
			if (CacheKeyComparer.s_comparerInstance == null)
			{
				CacheKeyComparer.s_comparerInstance = new CacheKeyComparer();
			}
			return CacheKeyComparer.s_comparerInstance;
		}

		// Token: 0x060065D4 RID: 26068 RVA: 0x000030B5 File Offset: 0x000012B5
		private CacheKeyComparer()
		{
		}

		// Token: 0x060065D5 RID: 26069 RVA: 0x00166AC3 File Offset: 0x00164CC3
		bool IEqualityComparer.Equals(object x, object y)
		{
			return this.Compare(x, y) == 0;
		}

		// Token: 0x060065D6 RID: 26070 RVA: 0x00166AD0 File Offset: 0x00164CD0
		private int Compare(object x, object y)
		{
			CacheKey cacheKey = (CacheKey)x;
			CacheKey cacheKey2 = (CacheKey)y;
			if (cacheKey.IsPublic)
			{
				if (cacheKey2.IsPublic)
				{
					return string.Compare(cacheKey.Key, cacheKey2.Key, StringComparison.Ordinal);
				}
				return 1;
			}
			else
			{
				if (!cacheKey2.IsPublic)
				{
					return string.Compare(cacheKey.Key, cacheKey2.Key, StringComparison.Ordinal);
				}
				return -1;
			}
		}

		// Token: 0x060065D7 RID: 26071 RVA: 0x00166B2C File Offset: 0x00164D2C
		int IEqualityComparer.GetHashCode(object obj)
		{
			CacheKey cacheKey = (CacheKey)obj;
			return cacheKey.GetHashCode();
		}

		// Token: 0x04003477 RID: 13431
		private static CacheKeyComparer s_comparerInstance;
	}
}
