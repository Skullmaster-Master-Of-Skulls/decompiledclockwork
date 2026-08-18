using System;
using System.Collections.Generic;
using System.Threading;

namespace System.Linq.Parallel
{
	// Token: 0x020001D3 RID: 467
	internal sealed class GroupByQueryOperator<TSource, TGroupKey, TElement> : UnaryQueryOperator<TSource, IGrouping<TGroupKey, TElement>>
	{
		// Token: 0x06000F6A RID: 3946 RVA: 0x00036803 File Offset: 0x00034A03
		internal GroupByQueryOperator(IEnumerable<TSource> child, Func<TSource, TGroupKey> keySelector, Func<TSource, TElement> elementSelector, IEqualityComparer<TGroupKey> keyComparer) : base(child)
		{
			this.m_keySelector = keySelector;
			this.m_elementSelector = elementSelector;
			this.m_keyComparer = keyComparer;
			base.SetOrdinalIndexState(OrdinalIndexState.Shuffled);
		}

		// Token: 0x06000F6B RID: 3947 RVA: 0x0003682C File Offset: 0x00034A2C
		internal override void WrapPartitionedStream<TKey>(PartitionedStream<TSource, TKey> inputStream, IPartitionedStreamRecipient<IGrouping<TGroupKey, TElement>> recipient, bool preferStriping, QuerySettings settings)
		{
			if (base.Child.OutputOrdered)
			{
				this.WrapPartitionedStreamHelperOrdered<TKey>(ExchangeUtilities.HashRepartitionOrdered<TSource, TGroupKey, TKey>(inputStream, this.m_keySelector, this.m_keyComparer, null, settings.CancellationState.MergedCancellationToken), recipient, settings.CancellationState.MergedCancellationToken);
				return;
			}
			this.WrapPartitionedStreamHelper<TKey, int>(ExchangeUtilities.HashRepartition<TSource, TGroupKey, TKey>(inputStream, this.m_keySelector, this.m_keyComparer, null, settings.CancellationState.MergedCancellationToken), recipient, settings.CancellationState.MergedCancellationToken);
		}

		// Token: 0x06000F6C RID: 3948 RVA: 0x000368AC File Offset: 0x00034AAC
		private void WrapPartitionedStreamHelper<TIgnoreKey, TKey>(PartitionedStream<Pair<TSource, TGroupKey>, TKey> hashStream, IPartitionedStreamRecipient<IGrouping<TGroupKey, TElement>> recipient, CancellationToken cancellationToken)
		{
			int partitionCount = hashStream.PartitionCount;
			PartitionedStream<IGrouping<TGroupKey, TElement>, TKey> partitionedStream = new PartitionedStream<IGrouping<TGroupKey, TElement>, TKey>(partitionCount, hashStream.KeyComparer, OrdinalIndexState.Shuffled);
			for (int i = 0; i < partitionCount; i++)
			{
				if (this.m_elementSelector == null)
				{
					GroupByIdentityQueryOperatorEnumerator<TSource, TGroupKey, TKey> groupByIdentityQueryOperatorEnumerator = new GroupByIdentityQueryOperatorEnumerator<TSource, TGroupKey, TKey>(hashStream[i], this.m_keyComparer, cancellationToken);
					partitionedStream[i] = (QueryOperatorEnumerator<IGrouping<TGroupKey, TElement>, TKey>)groupByIdentityQueryOperatorEnumerator;
				}
				else
				{
					partitionedStream[i] = new GroupByElementSelectorQueryOperatorEnumerator<TSource, TGroupKey, TElement, TKey>(hashStream[i], this.m_keyComparer, this.m_elementSelector, cancellationToken);
				}
			}
			recipient.Receive<TKey>(partitionedStream);
		}

		// Token: 0x06000F6D RID: 3949 RVA: 0x0003692C File Offset: 0x00034B2C
		private void WrapPartitionedStreamHelperOrdered<TKey>(PartitionedStream<Pair<TSource, TGroupKey>, TKey> hashStream, IPartitionedStreamRecipient<IGrouping<TGroupKey, TElement>> recipient, CancellationToken cancellationToken)
		{
			int partitionCount = hashStream.PartitionCount;
			PartitionedStream<IGrouping<TGroupKey, TElement>, TKey> partitionedStream = new PartitionedStream<IGrouping<TGroupKey, TElement>, TKey>(partitionCount, hashStream.KeyComparer, OrdinalIndexState.Shuffled);
			IComparer<TKey> keyComparer = hashStream.KeyComparer;
			for (int i = 0; i < partitionCount; i++)
			{
				if (this.m_elementSelector == null)
				{
					OrderedGroupByIdentityQueryOperatorEnumerator<TSource, TGroupKey, TKey> orderedGroupByIdentityQueryOperatorEnumerator = new OrderedGroupByIdentityQueryOperatorEnumerator<TSource, TGroupKey, TKey>(hashStream[i], this.m_keySelector, this.m_keyComparer, keyComparer, cancellationToken);
					partitionedStream[i] = (QueryOperatorEnumerator<IGrouping<TGroupKey, TElement>, TKey>)orderedGroupByIdentityQueryOperatorEnumerator;
				}
				else
				{
					partitionedStream[i] = new OrderedGroupByElementSelectorQueryOperatorEnumerator<TSource, TGroupKey, TElement, TKey>(hashStream[i], this.m_keySelector, this.m_elementSelector, this.m_keyComparer, keyComparer, cancellationToken);
				}
			}
			recipient.Receive<TKey>(partitionedStream);
		}

		// Token: 0x06000F6E RID: 3950 RVA: 0x000369C4 File Offset: 0x00034BC4
		internal override QueryResults<IGrouping<TGroupKey, TElement>> Open(QuerySettings settings, bool preferStriping)
		{
			QueryResults<TSource> childQueryResults = base.Child.Open(settings, false);
			return new UnaryQueryOperator<TSource, IGrouping<TGroupKey, TElement>>.UnaryQueryOperatorResults(childQueryResults, this, settings, false);
		}

		// Token: 0x06000F6F RID: 3951 RVA: 0x000369E8 File Offset: 0x00034BE8
		internal override IEnumerable<IGrouping<TGroupKey, TElement>> AsSequentialQuery(CancellationToken token)
		{
			IEnumerable<TSource> source = CancellableEnumerable.Wrap<TSource>(base.Child.AsSequentialQuery(token), token);
			if (this.m_elementSelector == null)
			{
				return (IEnumerable<IGrouping<TGroupKey, TElement>>)source.GroupBy(this.m_keySelector, this.m_keyComparer);
			}
			return source.GroupBy(this.m_keySelector, this.m_elementSelector, this.m_keyComparer);
		}

		// Token: 0x170002B5 RID: 693
		// (get) Token: 0x06000F70 RID: 3952 RVA: 0x00036A40 File Offset: 0x00034C40
		internal override bool LimitsParallelism
		{
			get
			{
				return false;
			}
		}

		// Token: 0x040008C9 RID: 2249
		private readonly Func<TSource, TGroupKey> m_keySelector;

		// Token: 0x040008CA RID: 2250
		private readonly Func<TSource, TElement> m_elementSelector;

		// Token: 0x040008CB RID: 2251
		private readonly IEqualityComparer<TGroupKey> m_keyComparer;
	}
}
