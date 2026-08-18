using System;
using System.Collections.Generic;

namespace System.Linq.Parallel
{
	// Token: 0x0200017C RID: 380
	internal class ParallelEnumerableWrapper<T> : ParallelQuery<T>
	{
		// Token: 0x06000DE6 RID: 3558 RVA: 0x0003125E File Offset: 0x0002F45E
		internal ParallelEnumerableWrapper(IEnumerable<T> wrappedEnumerable) : base(QuerySettings.Empty)
		{
			this.m_wrappedEnumerable = wrappedEnumerable;
		}

		// Token: 0x1700027A RID: 634
		// (get) Token: 0x06000DE7 RID: 3559 RVA: 0x00031272 File Offset: 0x0002F472
		internal IEnumerable<T> WrappedEnumerable
		{
			get
			{
				return this.m_wrappedEnumerable;
			}
		}

		// Token: 0x06000DE8 RID: 3560 RVA: 0x0003127A File Offset: 0x0002F47A
		public override IEnumerator<T> GetEnumerator()
		{
			return this.m_wrappedEnumerable.GetEnumerator();
		}

		// Token: 0x04000816 RID: 2070
		private readonly IEnumerable<T> m_wrappedEnumerable;
	}
}
