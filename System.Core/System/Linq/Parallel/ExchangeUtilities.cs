using System;
using System.Collections.Generic;
using System.Threading;

namespace System.Linq.Parallel
{
	// Token: 0x020001F8 RID: 504
	internal static class ExchangeUtilities
	{
		// Token: 0x06001012 RID: 4114 RVA: 0x00038B9C File Offset: 0x00036D9C
		internal static PartitionedStream<T, int> PartitionDataSource<T>(IEnumerable<T> source, int partitionCount, bool useStriping)
		{
			IParallelPartitionable<T> parallelPartitionable = source as IParallelPartitionable<T>;
			PartitionedStream<T, int> result;
			if (parallelPartitionable != null)
			{
				QueryOperatorEnumerator<T, int>[] partitions = parallelPartitionable.GetPartitions(partitionCount);
				if (partitions == null)
				{
					throw new InvalidOperationException(SR.GetString("ParallelPartitionable_NullReturn"));
				}
				if (partitions.Length != partitionCount)
				{
					throw new InvalidOperationException(SR.GetString("ParallelPartitionable_IncorretElementCount"));
				}
				PartitionedStream<T, int> partitionedStream = new PartitionedStream<T, int>(partitionCount, Util.GetDefaultComparer<int>(), OrdinalIndexState.Correct);
				for (int i = 0; i < partitionCount; i++)
				{
					QueryOperatorEnumerator<T, int> queryOperatorEnumerator = partitions[i];
					if (queryOperatorEnumerator == null)
					{
						throw new InvalidOperationException(SR.GetString("ParallelPartitionable_NullElement"));
					}
					partitionedStream[i] = queryOperatorEnumerator;
				}
				result = partitionedStream;
			}
			else
			{
				result = new PartitionedDataSource<T>(source, partitionCount, useStriping);
			}
			return result;
		}

		// Token: 0x06001013 RID: 4115 RVA: 0x00038C33 File Offset: 0x00036E33
		internal static PartitionedStream<Pair<TElement, THashKey>, int> HashRepartition<TElement, THashKey, TIgnoreKey>(PartitionedStream<TElement, TIgnoreKey> source, Func<TElement, THashKey> keySelector, IEqualityComparer<THashKey> keyComparer, IEqualityComparer<TElement> elementComparer, CancellationToken cancellationToken)
		{
			return new UnorderedHashRepartitionStream<TElement, THashKey, TIgnoreKey>(source, keySelector, keyComparer, elementComparer, cancellationToken);
		}

		// Token: 0x06001014 RID: 4116 RVA: 0x00038C40 File Offset: 0x00036E40
		internal static PartitionedStream<Pair<TElement, THashKey>, TOrderKey> HashRepartitionOrdered<TElement, THashKey, TOrderKey>(PartitionedStream<TElement, TOrderKey> source, Func<TElement, THashKey> keySelector, IEqualityComparer<THashKey> keyComparer, IEqualityComparer<TElement> elementComparer, CancellationToken cancellationToken)
		{
			return new OrderedHashRepartitionStream<TElement, THashKey, TOrderKey>(source, keySelector, keyComparer, elementComparer, cancellationToken);
		}

		// Token: 0x06001015 RID: 4117 RVA: 0x00038C4D File Offset: 0x00036E4D
		internal static OrdinalIndexState Worse(this OrdinalIndexState state1, OrdinalIndexState state2)
		{
			if (state1 <= state2)
			{
				return state2;
			}
			return state1;
		}

		// Token: 0x06001016 RID: 4118 RVA: 0x00038C56 File Offset: 0x00036E56
		internal static bool IsWorseThan(this OrdinalIndexState state1, OrdinalIndexState state2)
		{
			return state1 > state2;
		}
	}
}
