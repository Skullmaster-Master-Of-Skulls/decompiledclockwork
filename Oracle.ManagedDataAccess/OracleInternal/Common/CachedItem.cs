using System;

namespace OracleInternal.Common
{
	// Token: 0x020000A3 RID: 163
	internal class CachedItem<T> : IComparable
	{
		// Token: 0x060006D1 RID: 1745 RVA: 0x0003F7DC File Offset: 0x0003D9DC
		internal CachedItem(T value, long lastUsedCount)
		{
			this.m_value = value;
			this.m_lastUsedCount = lastUsedCount;
		}

		// Token: 0x060006D2 RID: 1746 RVA: 0x0003F7F4 File Offset: 0x0003D9F4
		int IComparable.CompareTo(object ci)
		{
			return this.m_lastUsedCount.CompareTo(((CachedItem<T>)ci).m_lastUsedCount);
		}

		// Token: 0x04000930 RID: 2352
		internal T m_value;

		// Token: 0x04000931 RID: 2353
		internal long m_lastUsedCount;
	}
}
