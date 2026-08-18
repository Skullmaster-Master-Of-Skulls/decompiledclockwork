using System;
using System.Collections.Generic;
using System.Threading;

namespace System.Linq.Parallel
{
	// Token: 0x020001DE RID: 478
	internal sealed class LastQueryOperator<TSource> : UnaryQueryOperator<TSource, TSource>
	{
		// Token: 0x06000F97 RID: 3991 RVA: 0x00037288 File Offset: 0x00035488
		internal LastQueryOperator(IEnumerable<TSource> child, Func<TSource, bool> predicate) : base(child)
		{
			this.m_predicate = predicate;
			this.m_prematureMergeNeeded = base.Child.OrdinalIndexState.IsWorseThan(OrdinalIndexState.Increasing);
		}

		// Token: 0x06000F98 RID: 3992 RVA: 0x000372B0 File Offset: 0x000354B0
		internal override QueryResults<TSource> Open(QuerySettings settings, bool preferStriping)
		{
			QueryResults<TSource> childQueryResults = base.Child.Open(settings, false);
			return new UnaryQueryOperator<TSource, TSource>.UnaryQueryOperatorResults(childQueryResults, this, settings, preferStriping);
		}

		// Token: 0x06000F99 RID: 3993 RVA: 0x000372D4 File Offset: 0x000354D4
		internal override void WrapPartitionedStream<TKey>(PartitionedStream<TSource, TKey> inputStream, IPartitionedStreamRecipient<TSource> recipient, bool preferStriping, QuerySettings settings)
		{
			if (this.m_prematureMergeNeeded)
			{
				PartitionedStream<TSource, int> partitionedStream = QueryOperator<TSource>.ExecuteAndCollectResults<TKey>(inputStream, inputStream.PartitionCount, base.Child.OutputOrdered, preferStriping, settings).GetPartitionedStream();
				this.WrapHelper<int>(partitionedStream, recipient, settings);
				return;
			}
			this.WrapHelper<TKey>(inputStream, recipient, settings);
		}

		// Token: 0x06000F9A RID: 3994 RVA: 0x00037320 File Offset: 0x00035520
		private void WrapHelper<TKey>(PartitionedStream<TSource, TKey> inputStream, IPartitionedStreamRecipient<TSource> recipient, QuerySettings settings)
		{
			int partitionCount = inputStream.PartitionCount;
			LastQueryOperator<TSource>.LastQueryOperatorState<TKey> operatorState = new LastQueryOperator<TSource>.LastQueryOperatorState<TKey>();
			CountdownEvent sharedBarrier = new CountdownEvent(partitionCount);
			PartitionedStream<TSource, int> partitionedStream = new PartitionedStream<TSource, int>(partitionCount, Util.GetDefaultComparer<int>(), OrdinalIndexState.Shuffled);
			for (int i = 0; i < partitionCount; i++)
			{
				partitionedStream[i] = new LastQueryOperator<TSource>.LastQueryOperatorEnumerator<TKey>(inputStream[i], this.m_predicate, operatorState, sharedBarrier, settings.CancellationState.MergedCancellationToken, inputStream.KeyComparer, i);
			}
			recipient.Receive<int>(partitionedStream);
		}

		// Token: 0x06000F9B RID: 3995 RVA: 0x00037396 File Offset: 0x00035596
		internal override IEnumerable<TSource> AsSequentialQuery(CancellationToken token)
		{
			throw new NotSupportedException();
		}

		// Token: 0x170002BA RID: 698
		// (get) Token: 0x06000F9C RID: 3996 RVA: 0x0003739D File Offset: 0x0003559D
		internal override bool LimitsParallelism
		{
			get
			{
				return false;
			}
		}

		// Token: 0x040008E3 RID: 2275
		private readonly Func<TSource, bool> m_predicate;

		// Token: 0x040008E4 RID: 2276
		private readonly bool m_prematureMergeNeeded;

		// Token: 0x020003FD RID: 1021
		private class LastQueryOperatorEnumerator<TKey> : QueryOperatorEnumerator<TSource, int>
		{
			// Token: 0x06001E3B RID: 7739 RVA: 0x0006C1E5 File Offset: 0x0006A3E5
			internal LastQueryOperatorEnumerator(QueryOperatorEnumerator<TSource, TKey> source, Func<TSource, bool> predicate, LastQueryOperator<TSource>.LastQueryOperatorState<TKey> operatorState, CountdownEvent sharedBarrier, CancellationToken cancelToken, IComparer<TKey> keyComparer, int partitionId)
			{
				this.m_source = source;
				this.m_predicate = predicate;
				this.m_operatorState = operatorState;
				this.m_sharedBarrier = sharedBarrier;
				this.m_cancellationToken = cancelToken;
				this.m_keyComparer = keyComparer;
				this.m_partitionId = partitionId;
			}

			// Token: 0x06001E3C RID: 7740 RVA: 0x0006C224 File Offset: 0x0006A424
			internal override bool MoveNext(ref TSource currentElement, ref int currentKey)
			{
				if (this.m_alreadySearched)
				{
					return false;
				}
				TSource tsource = default(TSource);
				TKey tkey = default(TKey);
				bool flag = false;
				try
				{
					int num = 0;
					TSource tsource2 = default(TSource);
					TKey tkey2 = default(TKey);
					while (this.m_source.MoveNext(ref tsource2, ref tkey2))
					{
						if ((num & 63) == 0)
						{
							CancellationState.ThrowIfCanceled(this.m_cancellationToken);
						}
						if (this.m_predicate == null || this.m_predicate(tsource2))
						{
							tsource = tsource2;
							tkey = tkey2;
							flag = true;
						}
						num++;
					}
					if (flag)
					{
						LastQueryOperator<TSource>.LastQueryOperatorState<TKey> operatorState = this.m_operatorState;
						lock (operatorState)
						{
							if (this.m_operatorState.m_partitionId == -1 || this.m_keyComparer.Compare(tkey, this.m_operatorState.m_key) > 0)
							{
								this.m_operatorState.m_partitionId = this.m_partitionId;
								this.m_operatorState.m_key = tkey;
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
					if (this.m_operatorState.m_partitionId == this.m_partitionId)
					{
						currentElement = tsource;
						currentKey = 0;
						return true;
					}
				}
				return false;
			}

			// Token: 0x06001E3D RID: 7741 RVA: 0x0006C388 File Offset: 0x0006A588
			protected override void Dispose(bool disposing)
			{
				this.m_source.Dispose();
			}

			// Token: 0x040011F9 RID: 4601
			private QueryOperatorEnumerator<TSource, TKey> m_source;

			// Token: 0x040011FA RID: 4602
			private Func<TSource, bool> m_predicate;

			// Token: 0x040011FB RID: 4603
			private bool m_alreadySearched;

			// Token: 0x040011FC RID: 4604
			private int m_partitionId;

			// Token: 0x040011FD RID: 4605
			private LastQueryOperator<TSource>.LastQueryOperatorState<TKey> m_operatorState;

			// Token: 0x040011FE RID: 4606
			private CountdownEvent m_sharedBarrier;

			// Token: 0x040011FF RID: 4607
			private CancellationToken m_cancellationToken;

			// Token: 0x04001200 RID: 4608
			private IComparer<TKey> m_keyComparer;
		}

		// Token: 0x020003FE RID: 1022
		private class LastQueryOperatorState<TKey>
		{
			// Token: 0x04001201 RID: 4609
			internal TKey m_key;

			// Token: 0x04001202 RID: 4610
			internal int m_partitionId = -1;
		}
	}
}
