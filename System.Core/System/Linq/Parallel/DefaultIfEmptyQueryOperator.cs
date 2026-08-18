using System;
using System.Collections.Generic;
using System.Threading;

namespace System.Linq.Parallel
{
	// Token: 0x020001CE RID: 462
	internal sealed class DefaultIfEmptyQueryOperator<TSource> : UnaryQueryOperator<TSource, TSource>
	{
		// Token: 0x06000F4D RID: 3917 RVA: 0x00036186 File Offset: 0x00034386
		internal DefaultIfEmptyQueryOperator(IEnumerable<TSource> child, TSource defaultValue) : base(child)
		{
			this.m_defaultValue = defaultValue;
			base.SetOrdinalIndexState(base.Child.OrdinalIndexState.Worse(OrdinalIndexState.Correct));
		}

		// Token: 0x06000F4E RID: 3918 RVA: 0x000361B0 File Offset: 0x000343B0
		internal override QueryResults<TSource> Open(QuerySettings settings, bool preferStriping)
		{
			QueryResults<TSource> childQueryResults = base.Child.Open(settings, preferStriping);
			return new UnaryQueryOperator<TSource, TSource>.UnaryQueryOperatorResults(childQueryResults, this, settings, preferStriping);
		}

		// Token: 0x06000F4F RID: 3919 RVA: 0x000361D4 File Offset: 0x000343D4
		internal override void WrapPartitionedStream<TKey>(PartitionedStream<TSource, TKey> inputStream, IPartitionedStreamRecipient<TSource> recipient, bool preferStriping, QuerySettings settings)
		{
			int partitionCount = inputStream.PartitionCount;
			Shared<int> sharedEmptyCount = new Shared<int>(0);
			CountdownEvent sharedLatch = new CountdownEvent(partitionCount - 1);
			PartitionedStream<TSource, TKey> partitionedStream = new PartitionedStream<TSource, TKey>(partitionCount, inputStream.KeyComparer, this.OrdinalIndexState);
			for (int i = 0; i < partitionCount; i++)
			{
				partitionedStream[i] = new DefaultIfEmptyQueryOperator<TSource>.DefaultIfEmptyQueryOperatorEnumerator<TKey>(inputStream[i], this.m_defaultValue, i, partitionCount, sharedEmptyCount, sharedLatch, settings.CancellationState.MergedCancellationToken);
			}
			recipient.Receive<TKey>(partitionedStream);
		}

		// Token: 0x06000F50 RID: 3920 RVA: 0x0003624E File Offset: 0x0003444E
		internal override IEnumerable<TSource> AsSequentialQuery(CancellationToken token)
		{
			return base.Child.AsSequentialQuery(token).DefaultIfEmpty(this.m_defaultValue);
		}

		// Token: 0x170002B0 RID: 688
		// (get) Token: 0x06000F51 RID: 3921 RVA: 0x00036267 File Offset: 0x00034467
		internal override bool LimitsParallelism
		{
			get
			{
				return false;
			}
		}

		// Token: 0x040008C1 RID: 2241
		private readonly TSource m_defaultValue;

		// Token: 0x020003EF RID: 1007
		private class DefaultIfEmptyQueryOperatorEnumerator<TKey> : QueryOperatorEnumerator<TSource, TKey>
		{
			// Token: 0x06001E14 RID: 7700 RVA: 0x0006B9CC File Offset: 0x00069BCC
			internal DefaultIfEmptyQueryOperatorEnumerator(QueryOperatorEnumerator<TSource, TKey> source, TSource defaultValue, int partitionIndex, int partitionCount, Shared<int> sharedEmptyCount, CountdownEvent sharedLatch, CancellationToken cancelToken)
			{
				this.m_source = source;
				this.m_defaultValue = defaultValue;
				this.m_partitionIndex = partitionIndex;
				this.m_partitionCount = partitionCount;
				this.m_sharedEmptyCount = sharedEmptyCount;
				this.m_sharedLatch = sharedLatch;
				this.m_cancelToken = cancelToken;
			}

			// Token: 0x06001E15 RID: 7701 RVA: 0x0006BA0C File Offset: 0x00069C0C
			internal override bool MoveNext(ref TSource currentElement, ref TKey currentKey)
			{
				bool flag = this.m_source.MoveNext(ref currentElement, ref currentKey);
				if (!this.m_lookedForEmpty)
				{
					this.m_lookedForEmpty = true;
					if (!flag)
					{
						if (this.m_partitionIndex == 0)
						{
							this.m_sharedLatch.Wait(this.m_cancelToken);
							this.m_sharedLatch.Dispose();
							if (this.m_sharedEmptyCount.Value == this.m_partitionCount - 1)
							{
								currentElement = this.m_defaultValue;
								currentKey = default(TKey);
								return true;
							}
							return false;
						}
						else
						{
							Interlocked.Increment(ref this.m_sharedEmptyCount.Value);
						}
					}
					if (this.m_partitionIndex != 0)
					{
						this.m_sharedLatch.Signal();
					}
				}
				return flag;
			}

			// Token: 0x06001E16 RID: 7702 RVA: 0x0006BAAF File Offset: 0x00069CAF
			protected override void Dispose(bool disposing)
			{
				this.m_source.Dispose();
			}

			// Token: 0x040011C3 RID: 4547
			private QueryOperatorEnumerator<TSource, TKey> m_source;

			// Token: 0x040011C4 RID: 4548
			private bool m_lookedForEmpty;

			// Token: 0x040011C5 RID: 4549
			private int m_partitionIndex;

			// Token: 0x040011C6 RID: 4550
			private int m_partitionCount;

			// Token: 0x040011C7 RID: 4551
			private TSource m_defaultValue;

			// Token: 0x040011C8 RID: 4552
			private Shared<int> m_sharedEmptyCount;

			// Token: 0x040011C9 RID: 4553
			private CountdownEvent m_sharedLatch;

			// Token: 0x040011CA RID: 4554
			private CancellationToken m_cancelToken;
		}
	}
}
