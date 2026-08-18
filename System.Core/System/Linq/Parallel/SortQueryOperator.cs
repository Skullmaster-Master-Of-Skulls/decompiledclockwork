using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;

namespace System.Linq.Parallel
{
	// Token: 0x020001E3 RID: 483
	internal sealed class SortQueryOperator<TInputOutput, TSortKey> : UnaryQueryOperator<TInputOutput, TInputOutput>, IOrderedEnumerable<TInputOutput>, IEnumerable<!0>, IEnumerable
	{
		// Token: 0x06000FB4 RID: 4020 RVA: 0x00037898 File Offset: 0x00035A98
		internal SortQueryOperator(IEnumerable<TInputOutput> source, Func<TInputOutput, TSortKey> keySelector, IComparer<TSortKey> comparer, bool descending) : base(source, true)
		{
			this.m_keySelector = keySelector;
			if (comparer == null)
			{
				this.m_comparer = Util.GetDefaultComparer<TSortKey>();
			}
			else
			{
				this.m_comparer = comparer;
			}
			if (descending)
			{
				this.m_comparer = new ReverseComparer<TSortKey>(this.m_comparer);
			}
			base.SetOrdinalIndexState(OrdinalIndexState.Shuffled);
		}

		// Token: 0x06000FB5 RID: 4021 RVA: 0x000378E8 File Offset: 0x00035AE8
		IOrderedEnumerable<TInputOutput> IOrderedEnumerable<!0>.CreateOrderedEnumerable<TKey2>(Func<TInputOutput, TKey2> key2Selector, IComparer<TKey2> key2Comparer, bool descending)
		{
			key2Comparer = (key2Comparer ?? Util.GetDefaultComparer<TKey2>());
			if (descending)
			{
				key2Comparer = new ReverseComparer<TKey2>(key2Comparer);
			}
			IComparer<Pair<TSortKey, TKey2>> comparer = new PairComparer<TSortKey, TKey2>(this.m_comparer, key2Comparer);
			Func<TInputOutput, Pair<TSortKey, TKey2>> keySelector = (TInputOutput elem) => new Pair<TSortKey, TKey2>(this.m_keySelector(elem), key2Selector(elem));
			return new SortQueryOperator<TInputOutput, Pair<TSortKey, TKey2>>(base.Child, keySelector, comparer, false);
		}

		// Token: 0x170002BF RID: 703
		// (get) Token: 0x06000FB6 RID: 4022 RVA: 0x00037948 File Offset: 0x00035B48
		internal Func<TInputOutput, TSortKey> KeySelector
		{
			get
			{
				return this.m_keySelector;
			}
		}

		// Token: 0x170002C0 RID: 704
		// (get) Token: 0x06000FB7 RID: 4023 RVA: 0x00037950 File Offset: 0x00035B50
		internal IComparer<TSortKey> KeyComparer
		{
			get
			{
				return this.m_comparer;
			}
		}

		// Token: 0x06000FB8 RID: 4024 RVA: 0x00037958 File Offset: 0x00035B58
		internal override QueryResults<TInputOutput> Open(QuerySettings settings, bool preferStriping)
		{
			QueryResults<TInputOutput> childQueryResults = base.Child.Open(settings, false);
			return new SortQueryOperatorResults<TInputOutput, TSortKey>(childQueryResults, this, settings, preferStriping);
		}

		// Token: 0x06000FB9 RID: 4025 RVA: 0x0003797C File Offset: 0x00035B7C
		internal override void WrapPartitionedStream<TKey>(PartitionedStream<TInputOutput, TKey> inputStream, IPartitionedStreamRecipient<TInputOutput> recipient, bool preferStriping, QuerySettings settings)
		{
			PartitionedStream<TInputOutput, TSortKey> partitionedStream = new PartitionedStream<TInputOutput, TSortKey>(inputStream.PartitionCount, this.m_comparer, this.OrdinalIndexState);
			for (int i = 0; i < partitionedStream.PartitionCount; i++)
			{
				partitionedStream[i] = new SortQueryOperatorEnumerator<TInputOutput, TKey, TSortKey>(inputStream[i], this.m_keySelector, this.m_comparer);
			}
			recipient.Receive<TSortKey>(partitionedStream);
		}

		// Token: 0x06000FBA RID: 4026 RVA: 0x000379D8 File Offset: 0x00035BD8
		internal override IEnumerable<TInputOutput> AsSequentialQuery(CancellationToken token)
		{
			IEnumerable<TInputOutput> source = CancellableEnumerable.Wrap<TInputOutput>(base.Child.AsSequentialQuery(token), token);
			return source.OrderBy(this.m_keySelector, this.m_comparer);
		}

		// Token: 0x170002C1 RID: 705
		// (get) Token: 0x06000FBB RID: 4027 RVA: 0x00037A0A File Offset: 0x00035C0A
		internal override bool LimitsParallelism
		{
			get
			{
				return false;
			}
		}

		// Token: 0x040008EC RID: 2284
		private readonly Func<TInputOutput, TSortKey> m_keySelector;

		// Token: 0x040008ED RID: 2285
		private readonly IComparer<TSortKey> m_comparer;
	}
}
