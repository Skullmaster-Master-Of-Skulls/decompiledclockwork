using System;
using System.Collections.Generic;

namespace System.Linq.Parallel
{
	// Token: 0x02000191 RID: 401
	internal class PartitionedStream<TElement, TKey>
	{
		// Token: 0x06000E2C RID: 3628 RVA: 0x000326F8 File Offset: 0x000308F8
		internal PartitionedStream(int partitionCount, IComparer<TKey> keyComparer, OrdinalIndexState indexState)
		{
			this.m_partitions = new QueryOperatorEnumerator<TElement, TKey>[partitionCount];
			this.m_keyComparer = keyComparer;
			this.m_indexState = indexState;
		}

		// Token: 0x1700027F RID: 639
		internal QueryOperatorEnumerator<TElement, TKey> this[int index]
		{
			get
			{
				return this.m_partitions[index];
			}
			set
			{
				this.m_partitions[index] = value;
			}
		}

		// Token: 0x17000280 RID: 640
		// (get) Token: 0x06000E2F RID: 3631 RVA: 0x0003272F File Offset: 0x0003092F
		public int PartitionCount
		{
			get
			{
				return this.m_partitions.Length;
			}
		}

		// Token: 0x17000281 RID: 641
		// (get) Token: 0x06000E30 RID: 3632 RVA: 0x00032739 File Offset: 0x00030939
		internal IComparer<TKey> KeyComparer
		{
			get
			{
				return this.m_keyComparer;
			}
		}

		// Token: 0x17000282 RID: 642
		// (get) Token: 0x06000E31 RID: 3633 RVA: 0x00032741 File Offset: 0x00030941
		internal OrdinalIndexState OrdinalIndexState
		{
			get
			{
				return this.m_indexState;
			}
		}

		// Token: 0x04000860 RID: 2144
		protected QueryOperatorEnumerator<TElement, TKey>[] m_partitions;

		// Token: 0x04000861 RID: 2145
		private readonly IComparer<TKey> m_keyComparer;

		// Token: 0x04000862 RID: 2146
		private readonly OrdinalIndexState m_indexState;
	}
}
