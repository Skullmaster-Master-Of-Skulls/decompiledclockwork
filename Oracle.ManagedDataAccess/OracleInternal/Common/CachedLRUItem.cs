using System;

namespace OracleInternal.Common
{
	// Token: 0x020000A4 RID: 164
	internal class CachedLRUItem<T, U>
	{
		// Token: 0x060006D3 RID: 1747 RVA: 0x0003F80C File Offset: 0x0003DA0C
		internal CachedLRUItem(T value, U pos)
		{
			this.m_value = value;
			this.m_pos = pos;
		}

		// Token: 0x04000932 RID: 2354
		internal T m_value;

		// Token: 0x04000933 RID: 2355
		internal U m_pos;
	}
}
