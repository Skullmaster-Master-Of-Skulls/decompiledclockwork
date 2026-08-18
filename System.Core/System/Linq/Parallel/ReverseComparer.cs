using System;
using System.Collections.Generic;

namespace System.Linq.Parallel
{
	// Token: 0x02000203 RID: 515
	internal class ReverseComparer<T> : IComparer<T>
	{
		// Token: 0x06001056 RID: 4182 RVA: 0x000397F0 File Offset: 0x000379F0
		internal ReverseComparer(IComparer<T> comparer)
		{
			this.m_comparer = comparer;
		}

		// Token: 0x06001057 RID: 4183 RVA: 0x000397FF File Offset: 0x000379FF
		public int Compare(T x, T y)
		{
			return -this.m_comparer.Compare(x, y);
		}

		// Token: 0x04000944 RID: 2372
		private IComparer<T> m_comparer;
	}
}
