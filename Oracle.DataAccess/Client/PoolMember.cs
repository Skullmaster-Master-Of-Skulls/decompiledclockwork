using System;

namespace Oracle.DataAccess.Client
{
	// Token: 0x020000E0 RID: 224
	internal class PoolMember
	{
		// Token: 0x0600083E RID: 2110 RVA: 0x00050AB2 File Offset: 0x0004FAB2
		public PoolMember(object val, ulong lut)
		{
			this.m_LastUsedTime = lut;
			this.m_Value = val;
		}

		// Token: 0x040006FA RID: 1786
		public ulong m_LastUsedTime;

		// Token: 0x040006FB RID: 1787
		public object m_Value;
	}
}
