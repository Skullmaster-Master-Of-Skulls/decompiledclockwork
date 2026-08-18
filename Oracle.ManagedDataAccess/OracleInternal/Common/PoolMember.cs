using System;
using System.Collections.Generic;

namespace OracleInternal.Common
{
	// Token: 0x020000A0 RID: 160
	internal class PoolMember<valType>
	{
		// Token: 0x060006C3 RID: 1731 RVA: 0x0003E7E0 File Offset: 0x0003C9E0
		public PoolMember(valType val, int maxSubCacheSize, ulong lut)
		{
			this.m_list = new List<valType>(Math.Min(maxSubCacheSize, 10));
			this.m_list.Add(val);
			this.m_LastUsedTime = lut;
		}

		// Token: 0x04000912 RID: 2322
		public ulong m_LastUsedTime;

		// Token: 0x04000913 RID: 2323
		public List<valType> m_list;
	}
}
