using System;
using System.Collections.Generic;
using System.Threading;

namespace System.Linq.Parallel
{
	// Token: 0x020001C9 RID: 457
	internal class QueryExecutionOption<TSource> : QueryOperator<TSource>
	{
		// Token: 0x06000F31 RID: 3889 RVA: 0x00035C9B File Offset: 0x00033E9B
		internal QueryExecutionOption(QueryOperator<TSource> source, QuerySettings settings) : base(source.OutputOrdered, settings.Merge(source.SpecifiedQuerySettings))
		{
			this.m_child = source;
			this.m_indexState = this.m_child.OrdinalIndexState;
		}

		// Token: 0x06000F32 RID: 3890 RVA: 0x00035CCE File Offset: 0x00033ECE
		internal override QueryResults<TSource> Open(QuerySettings settings, bool preferStriping)
		{
			return this.m_child.Open(settings, preferStriping);
		}

		// Token: 0x06000F33 RID: 3891 RVA: 0x00035CDD File Offset: 0x00033EDD
		internal override IEnumerable<TSource> AsSequentialQuery(CancellationToken token)
		{
			return this.m_child.AsSequentialQuery(token);
		}

		// Token: 0x170002AB RID: 683
		// (get) Token: 0x06000F34 RID: 3892 RVA: 0x00035CEB File Offset: 0x00033EEB
		internal override OrdinalIndexState OrdinalIndexState
		{
			get
			{
				return this.m_indexState;
			}
		}

		// Token: 0x170002AC RID: 684
		// (get) Token: 0x06000F35 RID: 3893 RVA: 0x00035CF3 File Offset: 0x00033EF3
		internal override bool LimitsParallelism
		{
			get
			{
				return this.m_child.LimitsParallelism;
			}
		}

		// Token: 0x040008B6 RID: 2230
		private QueryOperator<TSource> m_child;

		// Token: 0x040008B7 RID: 2231
		private OrdinalIndexState m_indexState;
	}
}
