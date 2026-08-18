using System;
using System.Collections.Generic;

namespace OracleInternal.ConnectionPool
{
	// Token: 0x020000DA RID: 218
	internal class CPComparer : IComparer<object>
	{
		// Token: 0x060008B0 RID: 2224 RVA: 0x0005DCBC File Offset: 0x0005BEBC
		public int Compare(object x, object y)
		{
			int count = ((OraclePool)x).m_cpQueuePR.Count;
			int count2 = ((OraclePool)y).m_cpQueuePR.Count;
			if (count < count2)
			{
				return 1;
			}
			if (count == count2)
			{
				return 0;
			}
			return -1;
		}

		// Token: 0x04000B9D RID: 2973
		public static CPComparer s_cpComparer = new CPComparer();
	}
}
