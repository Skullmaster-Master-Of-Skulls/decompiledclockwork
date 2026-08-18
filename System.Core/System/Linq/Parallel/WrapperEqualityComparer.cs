using System;
using System.Collections.Generic;

namespace System.Linq.Parallel
{
	// Token: 0x0200020A RID: 522
	internal struct WrapperEqualityComparer<T> : IEqualityComparer<Wrapper<T>>
	{
		// Token: 0x0600106F RID: 4207 RVA: 0x0003A1C1 File Offset: 0x000383C1
		internal WrapperEqualityComparer(IEqualityComparer<T> comparer)
		{
			if (comparer == null)
			{
				this.m_comparer = EqualityComparer<T>.Default;
				return;
			}
			this.m_comparer = comparer;
		}

		// Token: 0x06001070 RID: 4208 RVA: 0x0003A1D9 File Offset: 0x000383D9
		public bool Equals(Wrapper<T> x, Wrapper<T> y)
		{
			return this.m_comparer.Equals(x.Value, y.Value);
		}

		// Token: 0x06001071 RID: 4209 RVA: 0x0003A1F2 File Offset: 0x000383F2
		public int GetHashCode(Wrapper<T> x)
		{
			return this.m_comparer.GetHashCode(x.Value);
		}

		// Token: 0x04000956 RID: 2390
		private IEqualityComparer<T> m_comparer;
	}
}
