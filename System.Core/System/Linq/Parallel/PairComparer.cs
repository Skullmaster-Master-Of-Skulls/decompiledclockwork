using System;
using System.Collections.Generic;

namespace System.Linq.Parallel
{
	// Token: 0x02000201 RID: 513
	internal class PairComparer<T, U> : IComparer<Pair<T, U>>
	{
		// Token: 0x06001049 RID: 4169 RVA: 0x0003965C File Offset: 0x0003785C
		public PairComparer(IComparer<T> comparer1, IComparer<U> comparer2)
		{
			this.m_comparer1 = comparer1;
			this.m_comparer2 = comparer2;
		}

		// Token: 0x0600104A RID: 4170 RVA: 0x00039674 File Offset: 0x00037874
		public int Compare(Pair<T, U> x, Pair<T, U> y)
		{
			int num = this.m_comparer1.Compare(x.First, y.First);
			if (num != 0)
			{
				return num;
			}
			return this.m_comparer2.Compare(x.Second, y.Second);
		}

		// Token: 0x0400093A RID: 2362
		private IComparer<T> m_comparer1;

		// Token: 0x0400093B RID: 2363
		private IComparer<U> m_comparer2;
	}
}
