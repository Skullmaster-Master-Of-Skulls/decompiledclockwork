using System;
using System.Collections.Generic;
using System.Threading;

namespace System.Linq.Parallel
{
	// Token: 0x020001E6 RID: 486
	internal sealed class TakeOrSkipQueryOperator<TResult> : UnaryQueryOperator<TResult, TResult>
	{
		// Token: 0x06000FC3 RID: 4035 RVA: 0x00037AC8 File Offset: 0x00035CC8
		internal TakeOrSkipQueryOperator(IEnumerable<TResult> child, int count, bool take) : base(child)
		{
			this.m_count = count;
			this.m_take = take;
			base.SetOrdinalIndexState(this.OutputOrdinalIndexState());
		}

		// Token: 0x06000FC4 RID: 4036 RVA: 0x00037AEC File Offset: 0x00035CEC
		private OrdinalIndexState OutputOrdinalIndexState()
		{
			OrdinalIndexState ordinalIndexState = base.Child.OrdinalIndexState;
			if (ordinalIndexState == OrdinalIndexState.Indexible)
			{
				return OrdinalIndexState.Indexible;
			}
			if (ordinalIndexState.IsWorseThan(OrdinalIndexState.Increasing))
			{
				this.m_prematureMerge = true;
				ordinalIndexState = OrdinalIndexState.Correct;
			}
			if (!this.m_take && ordinalIndexState == OrdinalIndexState.Correct)
			{
				ordinalIndexState = OrdinalIndexState.Increasing;
			}
			return ordinalIndexState;
		}

		// Token: 0x06000FC5 RID: 4037 RVA: 0x00037B2C File Offset: 0x00035D2C
		internal override void WrapPartitionedStream<TKey>(PartitionedStream<TResult, TKey> inputStream, IPartitionedStreamRecipient<TResult> recipient, bool preferStriping, QuerySettings settings)
		{
			if (this.m_prematureMerge)
			{
				ListQueryResults<TResult> listQueryResults = QueryOperator<TResult>.ExecuteAndCollectResults<TKey>(inputStream, inputStream.PartitionCount, base.Child.OutputOrdered, preferStriping, settings);
				PartitionedStream<TResult, int> partitionedStream = listQueryResults.GetPartitionedStream();
				this.WrapHelper<int>(partitionedStream, recipient, settings);
				return;
			}
			this.WrapHelper<TKey>(inputStream, recipient, settings);
		}

		// Token: 0x06000FC6 RID: 4038 RVA: 0x00037B78 File Offset: 0x00035D78
		private void WrapHelper<TKey>(PartitionedStream<TResult, TKey> inputStream, IPartitionedStreamRecipient<TResult> recipient, QuerySettings settings)
		{
			int partitionCount = inputStream.PartitionCount;
			FixedMaxHeap<TKey> sharedIndices = new FixedMaxHeap<TKey>(this.m_count, inputStream.KeyComparer);
			CountdownEvent sharedBarrier = new CountdownEvent(partitionCount);
			PartitionedStream<TResult, TKey> partitionedStream = new PartitionedStream<TResult, TKey>(partitionCount, inputStream.KeyComparer, this.OrdinalIndexState);
			for (int i = 0; i < partitionCount; i++)
			{
				partitionedStream[i] = new TakeOrSkipQueryOperator<TResult>.TakeOrSkipQueryOperatorEnumerator<TKey>(inputStream[i], this.m_take, sharedIndices, sharedBarrier, settings.CancellationState.MergedCancellationToken, inputStream.KeyComparer);
			}
			recipient.Receive<TKey>(partitionedStream);
		}

		// Token: 0x06000FC7 RID: 4039 RVA: 0x00037C00 File Offset: 0x00035E00
		internal override QueryResults<TResult> Open(QuerySettings settings, bool preferStriping)
		{
			QueryResults<TResult> childQueryResults = base.Child.Open(settings, true);
			return TakeOrSkipQueryOperator<TResult>.TakeOrSkipQueryOperatorResults.NewResults(childQueryResults, this, settings, preferStriping);
		}

		// Token: 0x170002C4 RID: 708
		// (get) Token: 0x06000FC8 RID: 4040 RVA: 0x00037C24 File Offset: 0x00035E24
		internal override bool LimitsParallelism
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000FC9 RID: 4041 RVA: 0x00037C28 File Offset: 0x00035E28
		internal override IEnumerable<TResult> AsSequentialQuery(CancellationToken token)
		{
			if (this.m_take)
			{
				return base.Child.AsSequentialQuery(token).Take(this.m_count);
			}
			IEnumerable<TResult> source = CancellableEnumerable.Wrap<TResult>(base.Child.AsSequentialQuery(token), token);
			return source.Skip(this.m_count);
		}

		// Token: 0x040008F5 RID: 2293
		private readonly int m_count;

		// Token: 0x040008F6 RID: 2294
		private readonly bool m_take;

		// Token: 0x040008F7 RID: 2295
		private bool m_prematureMerge;

		// Token: 0x02000408 RID: 1032
		private class TakeOrSkipQueryOperatorEnumerator<TKey> : QueryOperatorEnumerator<TResult, TKey>
		{
			// Token: 0x06001E5C RID: 7772 RVA: 0x0006CA70 File Offset: 0x0006AC70
			internal TakeOrSkipQueryOperatorEnumerator(QueryOperatorEnumerator<TResult, TKey> source, bool take, FixedMaxHeap<TKey> sharedIndices, CountdownEvent sharedBarrier, CancellationToken cancellationToken, IComparer<TKey> keyComparer)
			{
				this.m_source = source;
				this.m_count = sharedIndices.Size;
				this.m_take = take;
				this.m_sharedIndices = sharedIndices;
				this.m_sharedBarrier = sharedBarrier;
				this.m_cancellationToken = cancellationToken;
				this.m_keyComparer = keyComparer;
			}

			// Token: 0x06001E5D RID: 7773 RVA: 0x0006CABC File Offset: 0x0006ACBC
			internal override bool MoveNext(ref TResult currentElement, ref TKey currentKey)
			{
				if (this.m_buffer == null && this.m_count > 0)
				{
					List<Pair<TResult, TKey>> list = new List<Pair<TResult, TKey>>();
					TResult first = default(TResult);
					TKey tkey = default(TKey);
					int num = 0;
					while (list.Count < this.m_count && this.m_source.MoveNext(ref first, ref tkey))
					{
						if ((num++ & 63) == 0)
						{
							CancellationState.ThrowIfCanceled(this.m_cancellationToken);
						}
						list.Add(new Pair<TResult, TKey>(first, tkey));
						FixedMaxHeap<TKey> sharedIndices = this.m_sharedIndices;
						lock (sharedIndices)
						{
							if (!this.m_sharedIndices.Insert(tkey))
							{
								break;
							}
						}
					}
					this.m_sharedBarrier.Signal();
					this.m_sharedBarrier.Wait(this.m_cancellationToken);
					this.m_buffer = list;
					this.m_bufferIndex = new Shared<int>(-1);
				}
				if (!this.m_take)
				{
					TKey y = default(TKey);
					if (this.m_count > 0)
					{
						if (this.m_sharedIndices.Count < this.m_count)
						{
							return false;
						}
						y = this.m_sharedIndices.MaxValue;
						if (this.m_bufferIndex.Value < this.m_buffer.Count - 1)
						{
							this.m_bufferIndex.Value++;
							while (this.m_bufferIndex.Value < this.m_buffer.Count)
							{
								if (this.m_keyComparer.Compare(this.m_buffer[this.m_bufferIndex.Value].Second, y) > 0)
								{
									currentElement = this.m_buffer[this.m_bufferIndex.Value].First;
									currentKey = this.m_buffer[this.m_bufferIndex.Value].Second;
									return true;
								}
								this.m_bufferIndex.Value++;
							}
						}
					}
					return this.m_source.MoveNext(ref currentElement, ref currentKey);
				}
				if (this.m_count == 0 || this.m_bufferIndex.Value >= this.m_buffer.Count - 1)
				{
					return false;
				}
				this.m_bufferIndex.Value++;
				currentElement = this.m_buffer[this.m_bufferIndex.Value].First;
				currentKey = this.m_buffer[this.m_bufferIndex.Value].Second;
				return this.m_sharedIndices.Count == 0 || this.m_keyComparer.Compare(this.m_buffer[this.m_bufferIndex.Value].Second, this.m_sharedIndices.MaxValue) <= 0;
			}

			// Token: 0x06001E5E RID: 7774 RVA: 0x0006CDAC File Offset: 0x0006AFAC
			protected override void Dispose(bool disposing)
			{
				this.m_source.Dispose();
			}

			// Token: 0x04001222 RID: 4642
			private readonly QueryOperatorEnumerator<TResult, TKey> m_source;

			// Token: 0x04001223 RID: 4643
			private readonly int m_count;

			// Token: 0x04001224 RID: 4644
			private readonly bool m_take;

			// Token: 0x04001225 RID: 4645
			private readonly IComparer<TKey> m_keyComparer;

			// Token: 0x04001226 RID: 4646
			private readonly FixedMaxHeap<TKey> m_sharedIndices;

			// Token: 0x04001227 RID: 4647
			private readonly CountdownEvent m_sharedBarrier;

			// Token: 0x04001228 RID: 4648
			private readonly CancellationToken m_cancellationToken;

			// Token: 0x04001229 RID: 4649
			private List<Pair<TResult, TKey>> m_buffer;

			// Token: 0x0400122A RID: 4650
			private Shared<int> m_bufferIndex;
		}

		// Token: 0x02000409 RID: 1033
		private class TakeOrSkipQueryOperatorResults : UnaryQueryOperator<TResult, TResult>.UnaryQueryOperatorResults
		{
			// Token: 0x06001E5F RID: 7775 RVA: 0x0006CDB9 File Offset: 0x0006AFB9
			public static QueryResults<TResult> NewResults(QueryResults<TResult> childQueryResults, TakeOrSkipQueryOperator<TResult> op, QuerySettings settings, bool preferStriping)
			{
				if (childQueryResults.IsIndexible)
				{
					return new TakeOrSkipQueryOperator<TResult>.TakeOrSkipQueryOperatorResults(childQueryResults, op, settings, preferStriping);
				}
				return new UnaryQueryOperator<TResult, TResult>.UnaryQueryOperatorResults(childQueryResults, op, settings, preferStriping);
			}

			// Token: 0x06001E60 RID: 7776 RVA: 0x0006CDD6 File Offset: 0x0006AFD6
			private TakeOrSkipQueryOperatorResults(QueryResults<TResult> childQueryResults, TakeOrSkipQueryOperator<TResult> takeOrSkipOp, QuerySettings settings, bool preferStriping) : base(childQueryResults, takeOrSkipOp, settings, preferStriping)
			{
				this.m_takeOrSkipOp = takeOrSkipOp;
				this.m_childCount = this.m_childQueryResults.ElementsCount;
			}

			// Token: 0x17000576 RID: 1398
			// (get) Token: 0x06001E61 RID: 7777 RVA: 0x0006CDFB File Offset: 0x0006AFFB
			internal override bool IsIndexible
			{
				get
				{
					return this.m_childCount >= 0;
				}
			}

			// Token: 0x17000577 RID: 1399
			// (get) Token: 0x06001E62 RID: 7778 RVA: 0x0006CE09 File Offset: 0x0006B009
			internal override int ElementsCount
			{
				get
				{
					if (this.m_takeOrSkipOp.m_take)
					{
						return Math.Min(this.m_childCount, this.m_takeOrSkipOp.m_count);
					}
					return Math.Max(this.m_childCount - this.m_takeOrSkipOp.m_count, 0);
				}
			}

			// Token: 0x06001E63 RID: 7779 RVA: 0x0006CE47 File Offset: 0x0006B047
			internal override TResult GetElement(int index)
			{
				if (this.m_takeOrSkipOp.m_take)
				{
					return this.m_childQueryResults.GetElement(index);
				}
				return this.m_childQueryResults.GetElement(this.m_takeOrSkipOp.m_count + index);
			}

			// Token: 0x0400122B RID: 4651
			private TakeOrSkipQueryOperator<TResult> m_takeOrSkipOp;

			// Token: 0x0400122C RID: 4652
			private int m_childCount;
		}
	}
}
