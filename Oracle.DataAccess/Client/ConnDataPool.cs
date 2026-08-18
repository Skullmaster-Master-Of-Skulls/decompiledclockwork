using System;
using System.Collections;

namespace Oracle.DataAccess.Client
{
	// Token: 0x020000E1 RID: 225
	internal class ConnDataPool
	{
		// Token: 0x0600083F RID: 2111 RVA: 0x00050AC8 File Offset: 0x0004FAC8
		public ConnDataPool(Hashtable val, ulong lut)
		{
			this.m_LastUsedTime = lut;
			this.m_ConnPool = val;
		}

		// Token: 0x040006FC RID: 1788
		public ulong m_LastUsedTime;

		// Token: 0x040006FD RID: 1789
		public Hashtable m_ConnPool;
	}
}
