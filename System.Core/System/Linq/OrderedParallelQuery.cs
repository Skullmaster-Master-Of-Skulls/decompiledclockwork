using System;
using System.Collections.Generic;
using System.Linq.Parallel;

namespace System.Linq
{
	// Token: 0x0200016F RID: 367
	[__DynamicallyInvokable]
	public class OrderedParallelQuery<TSource> : ParallelQuery<TSource>
	{
		// Token: 0x06000DA2 RID: 3490 RVA: 0x000309F2 File Offset: 0x0002EBF2
		internal OrderedParallelQuery(QueryOperator<TSource> sortOp) : base(sortOp.SpecifiedQuerySettings)
		{
			this.m_sortOp = sortOp;
		}

		// Token: 0x1700026D RID: 621
		// (get) Token: 0x06000DA3 RID: 3491 RVA: 0x00030A07 File Offset: 0x0002EC07
		internal QueryOperator<TSource> SortOperator
		{
			get
			{
				return this.m_sortOp;
			}
		}

		// Token: 0x1700026E RID: 622
		// (get) Token: 0x06000DA4 RID: 3492 RVA: 0x00030A0F File Offset: 0x0002EC0F
		internal IOrderedEnumerable<TSource> OrderedEnumerable
		{
			get
			{
				return (IOrderedEnumerable<TSource>)this.m_sortOp;
			}
		}

		// Token: 0x06000DA5 RID: 3493 RVA: 0x00030A1C File Offset: 0x0002EC1C
		[__DynamicallyInvokable]
		public override IEnumerator<TSource> GetEnumerator()
		{
			return this.m_sortOp.GetEnumerator();
		}

		// Token: 0x040007B0 RID: 1968
		private QueryOperator<TSource> m_sortOp;
	}
}
