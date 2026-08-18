using System;
using System.Collections.Generic;
using System.Threading;

namespace System.Linq.Parallel
{
	// Token: 0x020001D1 RID: 465
	internal sealed class FirstQueryOperator<TSource> : UnaryQueryOperator<TSource, TSource>
	{
		// Token: 0x06000F5E RID: 3934 RVA: 0x000365F0 File Offset: 0x000347F0
		internal FirstQueryOperator(IEnumerable<TSource> child, Func<TSource, bool> predicate) : base(child)
		{
			this.m_predicate = predicate;
			this.m_prematureMergeNeeded = base.Child.OrdinalIndexState.IsWorseThan(OrdinalIndexState.Increasing);
		}

		// Token: 0x06000F5F RID: 3935 RVA: 0x00036618 File Offset: 0x00034818
		internal override QueryResults<TSource> Open(QuerySettings settings, bool preferStriping)
		{
			QueryResults<TSource> childQueryResults = base.Child.Open(settings, false);
			return new UnaryQueryOperator<TSource, TSource>.UnaryQueryOperatorResults(childQueryResults, this, settings, preferStriping);
		}

		// Token: 0x06000F60 RID: 3936 RVA: 0x0003663C File Offset: 0x0003483C
		internal override void WrapPartitionedStream<TKey>(PartitionedStream<TSource, TKey> inputStream, IPartitionedStreamRecipient<TSource> recipient, bool preferStriping, QuerySettings settings)
		{
			if (this.m_prematureMergeNeeded)
			{
				ListQueryResults<TSource> listQueryResults = QueryOperator<TSource>.ExecuteAndCollectResults<TKey>(inputStream, inputStream.PartitionCount, base.Child.OutputOrdered, preferStriping, settings);
				this.WrapHelper<int>(listQueryResults.GetPartitionedStream(), recipient, settings);
				return;
			}
			this.WrapHelper<TKey>(inputStream, recipient, settings);
		}

		// Token: 0x06000F61 RID: 3937 RVA: 0x00036688 File Offset: 0x00034888
		private void WrapHelper<TKey>(PartitionedStream<TSource, TKey> inputStream, IPartitionedStreamRecipient<TSource> recipient, QuerySettings settings)
		{
			int partitionCount = inputStream.PartitionCount;
			FirstQueryOperator<TSource>.FirstQueryOperatorState<TKey> operatorState = new FirstQueryOperator<TSource>.FirstQueryOperatorState<TKey>();
			CountdownEvent sharedBarrier = new CountdownEvent(partitionCount);
			PartitionedStream<TSource, int> partitionedStream = new PartitionedStream<TSource, int>(partitionCount, Util.GetDefaultComparer<int>(), OrdinalIndexState.Shuffled);
			for (int i = 0; i < partitionCount; i++)
			{
				partitionedStream[i] = new FirstQueryOperator<TSource>.FirstQueryOperatorEnumerator<TKey>(inputStream[i], this.m_predicate, operatorState, sharedBarrier, settings.CancellationState.MergedCancellationToken, inputStream.KeyComparer, i);
			}
			recipient.Receive<int>(partitionedStream);
		}

		// Token: 0x06000F62 RID: 3938 RVA: 0x000366FE File Offset: 0x000348FE
		internal override IEnumerable<TSource> AsSequentialQuery(CancellationToken token)
		{
			throw new NotSupportedException();
		}

		// Token: 0x170002B3 RID: 691
		// (get) Token: 0x06000F63 RID: 3939 RVA: 0x00036705 File Offset: 0x00034905
		internal override bool LimitsParallelism
		{
			get
			{
				return false;
			}
		}

		// Token: 0x040008C6 RID: 2246
		private readonly Func<TSource, bool> m_predicate;

		// Token: 0x040008C7 RID: 2247
		private readonly bool m_prematureMergeNeeded;

		// Token: 0x020003F3 RID: 1011
		private class FirstQueryOperatorEnumerator<TKey> : QueryOperatorEnumerator<TSource, int>
		{
			// Token: 0x06001E20 RID: 7712 RVA: 0x0006BD40 File Offset: 0x00069F40
			internal FirstQueryOperatorEnumerator(QueryOperatorEnumerator<TSource, TKey> source, Func<TSource, bool> predicate, FirstQueryOperator<TSource>.FirstQueryOperatorState<TKey> operatorState, CountdownEvent sharedBarrier, CancellationToken cancellationToken, IComparer<TKey> keyComparer, int partitionId)
			{
				this.m_source = source;
				this.m_predicate = predicate;
				this.m_operatorState = operatorState;
				this.m_sharedBarrier = sharedBarrier;
				this.m_cancellationToken = cancellationToken;
				this.m_keyComparer = keyComparer;
				this.m_partitionId = partitionId;
			}

			// Token: 0x06001E21 RID: 7713 RVA: 0x0006BD80 File Offset: 0x00069F80
			internal override bool MoveNext(ref TSource currentElement, ref int currentKey)
			{
				if (this.m_alreadySearched)
				{
					return false;
				}
				TSource tsource = default(TSource);
				TKey tkey = default(TKey);
				try
				{
					TSource tsource2 = default(TSource);
					TKey tkey2 = default(TKey);
					int num = 0;
					while (this.m_source.MoveNext(ref tsource2, ref tkey2))
					{
						if ((num++ & 63) == 0)
						{
							CancellationState.ThrowIfCanceled(this.m_cancellationToken);
						}
						if (this.m_predicate == null || this.m_predicate(tsource2))
						{
							tsource = tsource2;
							tkey = tkey2;
							FirstQueryOperator<TSource>.FirstQueryOperatorState<TKey> operatorState = this.m_operatorState;
							lock (operatorState)
							{
								if (this.m_operatorState.m_partitionId == -1 || this.m_keyComparer.Compare(tkey, this.m_operatorState.m_key) < 0)
								{
									this.m_operatorState.m_key = tkey;
									this.m_operatorState.m_partitionId = this.m_partitionId;
								}
								break;
							}
						}
					}
				}
				finally
				{
					this.m_sharedBarrier.Signal();
				}
				this.m_alreadySearched = true;
				if (this.m_partitionId == this.m_operatorState.m_partitionId)
				{
					this.m_sharedBarrier.Wait(this.m_cancellationToken);
					if (this.m_partitionId == this.m_operatorState.m_partitionId)
					{
						currentElement = tsource;
						currentKey = 0;
						return true;
					}
				}
				return false;
			}

			// Token: 0x06001E22 RID: 7714 RVA: 0x0006BEE4 File Offset: 0x0006A0E4
			protected override void Dispose(bool disposing)
			{
				this.m_source.Dispose();
			}

			// Token: 0x040011D8 RID: 4568
			private QueryOperatorEnumerator<TSource, TKey> m_source;

			// Token: 0x040011D9 RID: 4569
			private Func<TSource, bool> m_predicate;

			// Token: 0x040011DA RID: 4570
			private bool m_alreadySearched;

			// Token: 0x040011DB RID: 4571
			private int m_partitionId;

			// Token: 0x040011DC RID: 4572
			private FirstQueryOperator<TSource>.FirstQueryOperatorState<TKey> m_operatorState;

			// Token: 0x040011DD RID: 4573
			private CountdownEvent m_sharedBarrier;

			// Token: 0x040011DE RID: 4574
			private CancellationToken m_cancellationToken;

			// Token: 0x040011DF RID: 4575
			private IComparer<TKey> m_keyComparer;
		}

		// Token: 0x020003F4 RID: 1012
		private class FirstQueryOperatorState<TKey>
		{
			// Token: 0x040011E0 RID: 4576
			internal TKey m_key;

			// Token: 0x040011E1 RID: 4577
			internal int m_partitionId = -1;
		}
	}
}
