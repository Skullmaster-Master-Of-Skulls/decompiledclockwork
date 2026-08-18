using System;
using System.Collections;
using System.Collections.Generic;

namespace System.Linq.Parallel
{
	// Token: 0x0200017B RID: 379
	internal class ParallelEnumerableWrapper : ParallelQuery<object>
	{
		// Token: 0x06000DE3 RID: 3555 RVA: 0x0003122B File Offset: 0x0002F42B
		internal ParallelEnumerableWrapper(IEnumerable source) : base(QuerySettings.Empty)
		{
			this.m_source = source;
		}

		// Token: 0x06000DE4 RID: 3556 RVA: 0x0003123F File Offset: 0x0002F43F
		internal override IEnumerator GetEnumeratorUntyped()
		{
			return this.m_source.GetEnumerator();
		}

		// Token: 0x06000DE5 RID: 3557 RVA: 0x0003124C File Offset: 0x0002F44C
		public override IEnumerator<object> GetEnumerator()
		{
			return new EnumerableWrapperWeakToStrong(this.m_source).GetEnumerator();
		}

		// Token: 0x04000815 RID: 2069
		private readonly IEnumerable m_source;
	}
}
