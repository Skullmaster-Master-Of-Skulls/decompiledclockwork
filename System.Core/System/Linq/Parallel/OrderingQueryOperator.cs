using System;
using System.Collections.Generic;
using System.Threading;

namespace System.Linq.Parallel
{
	// Token: 0x020001C8 RID: 456
	internal sealed class OrderingQueryOperator<TSource> : QueryOperator<TSource>
	{
		// Token: 0x06000F2B RID: 3883 RVA: 0x00035C09 File Offset: 0x00033E09
		public OrderingQueryOperator(QueryOperator<TSource> child, bool orderOn) : base(orderOn, child.SpecifiedQuerySettings)
		{
			this.m_child = child;
			this.m_ordinalIndexState = this.m_child.OrdinalIndexState;
			this.m_orderOn = orderOn;
		}

		// Token: 0x06000F2C RID: 3884 RVA: 0x00035C37 File Offset: 0x00033E37
		internal override QueryResults<TSource> Open(QuerySettings settings, bool preferStriping)
		{
			return this.m_child.Open(settings, preferStriping);
		}

		// Token: 0x06000F2D RID: 3885 RVA: 0x00035C48 File Offset: 0x00033E48
		internal override IEnumerator<TSource> GetEnumerator(ParallelMergeOptions? mergeOptions, bool suppressOrderPreservation)
		{
			ScanQueryOperator<TSource> scanQueryOperator = this.m_child as ScanQueryOperator<TSource>;
			if (scanQueryOperator != null)
			{
				return scanQueryOperator.Data.GetEnumerator();
			}
			return base.GetEnumerator(mergeOptions, suppressOrderPreservation);
		}

		// Token: 0x06000F2E RID: 3886 RVA: 0x00035C78 File Offset: 0x00033E78
		internal override IEnumerable<TSource> AsSequentialQuery(CancellationToken token)
		{
			return this.m_child.AsSequentialQuery(token);
		}

		// Token: 0x170002A9 RID: 681
		// (get) Token: 0x06000F2F RID: 3887 RVA: 0x00035C86 File Offset: 0x00033E86
		internal override bool LimitsParallelism
		{
			get
			{
				return this.m_child.LimitsParallelism;
			}
		}

		// Token: 0x170002AA RID: 682
		// (get) Token: 0x06000F30 RID: 3888 RVA: 0x00035C93 File Offset: 0x00033E93
		internal override OrdinalIndexState OrdinalIndexState
		{
			get
			{
				return this.m_ordinalIndexState;
			}
		}

		// Token: 0x040008B3 RID: 2227
		private bool m_orderOn;

		// Token: 0x040008B4 RID: 2228
		private QueryOperator<TSource> m_child;

		// Token: 0x040008B5 RID: 2229
		private OrdinalIndexState m_ordinalIndexState;
	}
}
