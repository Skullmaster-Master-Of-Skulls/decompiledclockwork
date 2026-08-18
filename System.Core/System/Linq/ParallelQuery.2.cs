using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Parallel;

namespace System.Linq
{
	// Token: 0x02000171 RID: 369
	[__DynamicallyInvokable]
	public class ParallelQuery<TSource> : ParallelQuery, IEnumerable<!0>, IEnumerable
	{
		// Token: 0x06000DAC RID: 3500 RVA: 0x00030A5D File Offset: 0x0002EC5D
		internal ParallelQuery(QuerySettings settings) : base(settings)
		{
		}

		// Token: 0x06000DAD RID: 3501 RVA: 0x00030A66 File Offset: 0x0002EC66
		internal sealed override ParallelQuery<TCastTo> Cast<TCastTo>()
		{
			return from elem in this
			select (TCastTo)((object)elem);
		}

		// Token: 0x06000DAE RID: 3502 RVA: 0x00030A90 File Offset: 0x0002EC90
		internal sealed override ParallelQuery<TCastTo> OfType<TCastTo>()
		{
			return from elem in this
			where elem is TCastTo
			select (TCastTo)((object)elem);
		}

		// Token: 0x06000DAF RID: 3503 RVA: 0x00030AE6 File Offset: 0x0002ECE6
		internal override IEnumerator GetEnumeratorUntyped()
		{
			return ((IEnumerable<TSource>)this).GetEnumerator();
		}

		// Token: 0x06000DB0 RID: 3504 RVA: 0x00030AEE File Offset: 0x0002ECEE
		[__DynamicallyInvokable]
		public virtual IEnumerator<TSource> GetEnumerator()
		{
			throw new NotSupportedException();
		}
	}
}
