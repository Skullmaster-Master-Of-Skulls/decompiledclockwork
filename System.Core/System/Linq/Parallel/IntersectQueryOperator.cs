using System;
using System.Collections.Generic;
using System.Threading;

namespace System.Linq.Parallel
{
	// Token: 0x02000199 RID: 409
	internal sealed class IntersectQueryOperator<TInputOutput> : BinaryQueryOperator<TInputOutput, TInputOutput, TInputOutput>
	{
		// Token: 0x06000E56 RID: 3670 RVA: 0x00033165 File Offset: 0x00031365
		internal IntersectQueryOperator(ParallelQuery<TInputOutput> left, ParallelQuery<TInputOutput> right, IEqualityComparer<TInputOutput> comparer) : base(left, right)
		{
			this.m_comparer = comparer;
			this.m_outputOrdered = base.LeftChild.OutputOrdered;
			base.SetOrdinalIndex(OrdinalIndexState.Shuffled);
		}

		// Token: 0x06000E57 RID: 3671 RVA: 0x00033190 File Offset: 0x00031390
		internal override QueryResults<TInputOutput> Open(QuerySettings settings, bool preferStriping)
		{
			QueryResults<TInputOutput> leftChildQueryResults = base.LeftChild.Open(settings, false);
			QueryResults<TInputOutput> rightChildQueryResults = base.RightChild.Open(settings, false);
			return new BinaryQueryOperator<TInputOutput, TInputOutput, TInputOutput>.BinaryQueryOperatorResults(leftChildQueryResults, rightChildQueryResults, this, settings, false);
		}

		// Token: 0x06000E58 RID: 3672 RVA: 0x000331C4 File Offset: 0x000313C4
		public override void WrapPartitionedStream<TLeftKey, TRightKey>(PartitionedStream<TInputOutput, TLeftKey> leftPartitionedStream, PartitionedStream<TInputOutput, TRightKey> rightPartitionedStream, IPartitionedStreamRecipient<TInputOutput> outputRecipient, bool preferStriping, QuerySettings settings)
		{
			if (base.OutputOrdered)
			{
				this.WrapPartitionedStreamHelper<TLeftKey, TRightKey>(ExchangeUtilities.HashRepartitionOrdered<TInputOutput, NoKeyMemoizationRequired, TLeftKey>(leftPartitionedStream, null, null, this.m_comparer, settings.CancellationState.MergedCancellationToken), rightPartitionedStream, outputRecipient, settings.CancellationState.MergedCancellationToken);
				return;
			}
			this.WrapPartitionedStreamHelper<int, TRightKey>(ExchangeUtilities.HashRepartition<TInputOutput, NoKeyMemoizationRequired, TLeftKey>(leftPartitionedStream, null, null, this.m_comparer, settings.CancellationState.MergedCancellationToken), rightPartitionedStream, outputRecipient, settings.CancellationState.MergedCancellationToken);
		}

		// Token: 0x06000E59 RID: 3673 RVA: 0x00033238 File Offset: 0x00031438
		private void WrapPartitionedStreamHelper<TLeftKey, TRightKey>(PartitionedStream<Pair<TInputOutput, NoKeyMemoizationRequired>, TLeftKey> leftHashStream, PartitionedStream<TInputOutput, TRightKey> rightPartitionedStream, IPartitionedStreamRecipient<TInputOutput> outputRecipient, CancellationToken cancellationToken)
		{
			int partitionCount = leftHashStream.PartitionCount;
			PartitionedStream<Pair<TInputOutput, NoKeyMemoizationRequired>, int> partitionedStream = ExchangeUtilities.HashRepartition<TInputOutput, NoKeyMemoizationRequired, TRightKey>(rightPartitionedStream, null, null, this.m_comparer, cancellationToken);
			PartitionedStream<TInputOutput, TLeftKey> partitionedStream2 = new PartitionedStream<TInputOutput, TLeftKey>(partitionCount, leftHashStream.KeyComparer, OrdinalIndexState.Shuffled);
			for (int i = 0; i < partitionCount; i++)
			{
				if (base.OutputOrdered)
				{
					partitionedStream2[i] = new IntersectQueryOperator<TInputOutput>.OrderedIntersectQueryOperatorEnumerator<TLeftKey>(leftHashStream[i], partitionedStream[i], this.m_comparer, leftHashStream.KeyComparer, cancellationToken);
				}
				else
				{
					partitionedStream2[i] = (QueryOperatorEnumerator<TInputOutput, TLeftKey>)new IntersectQueryOperator<TInputOutput>.IntersectQueryOperatorEnumerator<TLeftKey>(leftHashStream[i], partitionedStream[i], this.m_comparer, cancellationToken);
				}
			}
			outputRecipient.Receive<TLeftKey>(partitionedStream2);
		}

		// Token: 0x1700028C RID: 652
		// (get) Token: 0x06000E5A RID: 3674 RVA: 0x000332D7 File Offset: 0x000314D7
		internal override bool LimitsParallelism
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000E5B RID: 3675 RVA: 0x000332DC File Offset: 0x000314DC
		internal override IEnumerable<TInputOutput> AsSequentialQuery(CancellationToken token)
		{
			IEnumerable<TInputOutput> first = CancellableEnumerable.Wrap<TInputOutput>(base.LeftChild.AsSequentialQuery(token), token);
			IEnumerable<TInputOutput> second = CancellableEnumerable.Wrap<TInputOutput>(base.RightChild.AsSequentialQuery(token), token);
			return first.Intersect(second, this.m_comparer);
		}

		// Token: 0x04000878 RID: 2168
		private readonly IEqualityComparer<TInputOutput> m_comparer;

		// Token: 0x020003C1 RID: 961
		private class IntersectQueryOperatorEnumerator<TLeftKey> : QueryOperatorEnumerator<TInputOutput, int>
		{
			// Token: 0x06001D7B RID: 7547 RVA: 0x000693C3 File Offset: 0x000675C3
			internal IntersectQueryOperatorEnumerator(QueryOperatorEnumerator<Pair<TInputOutput, NoKeyMemoizationRequired>, TLeftKey> leftSource, QueryOperatorEnumerator<Pair<TInputOutput, NoKeyMemoizationRequired>, int> rightSource, IEqualityComparer<TInputOutput> comparer, CancellationToken cancellationToken)
			{
				this.m_leftSource = leftSource;
				this.m_rightSource = rightSource;
				this.m_comparer = comparer;
				this.m_cancellationToken = cancellationToken;
			}

			// Token: 0x06001D7C RID: 7548 RVA: 0x000693E8 File Offset: 0x000675E8
			internal override bool MoveNext(ref TInputOutput currentElement, ref int currentKey)
			{
				if (this.m_hashLookup == null)
				{
					this.m_outputLoopCount = new Shared<int>(0);
					this.m_hashLookup = new Set<TInputOutput>(this.m_comparer);
					Pair<TInputOutput, NoKeyMemoizationRequired> pair = default(Pair<TInputOutput, NoKeyMemoizationRequired>);
					int num = 0;
					int num2 = 0;
					while (this.m_rightSource.MoveNext(ref pair, ref num))
					{
						if ((num2++ & 63) == 0)
						{
							CancellationState.ThrowIfCanceled(this.m_cancellationToken);
						}
						this.m_hashLookup.Add(pair.First);
					}
				}
				Pair<TInputOutput, NoKeyMemoizationRequired> pair2 = default(Pair<TInputOutput, NoKeyMemoizationRequired>);
				TLeftKey tleftKey = default(TLeftKey);
				while (this.m_leftSource.MoveNext(ref pair2, ref tleftKey))
				{
					Shared<int> outputLoopCount = this.m_outputLoopCount;
					int value = outputLoopCount.Value;
					outputLoopCount.Value = value + 1;
					if ((value & 63) == 0)
					{
						CancellationState.ThrowIfCanceled(this.m_cancellationToken);
					}
					if (this.m_hashLookup.Contains(pair2.First))
					{
						this.m_hashLookup.Remove(pair2.First);
						currentElement = pair2.First;
						return true;
					}
				}
				return false;
			}

			// Token: 0x06001D7D RID: 7549 RVA: 0x000694E7 File Offset: 0x000676E7
			protected override void Dispose(bool disposing)
			{
				this.m_leftSource.Dispose();
				this.m_rightSource.Dispose();
			}

			// Token: 0x04001151 RID: 4433
			private QueryOperatorEnumerator<Pair<TInputOutput, NoKeyMemoizationRequired>, TLeftKey> m_leftSource;

			// Token: 0x04001152 RID: 4434
			private QueryOperatorEnumerator<Pair<TInputOutput, NoKeyMemoizationRequired>, int> m_rightSource;

			// Token: 0x04001153 RID: 4435
			private IEqualityComparer<TInputOutput> m_comparer;

			// Token: 0x04001154 RID: 4436
			private Set<TInputOutput> m_hashLookup;

			// Token: 0x04001155 RID: 4437
			private CancellationToken m_cancellationToken;

			// Token: 0x04001156 RID: 4438
			private Shared<int> m_outputLoopCount;
		}

		// Token: 0x020003C2 RID: 962
		private class OrderedIntersectQueryOperatorEnumerator<TLeftKey> : QueryOperatorEnumerator<TInputOutput, TLeftKey>
		{
			// Token: 0x06001D7E RID: 7550 RVA: 0x000694FF File Offset: 0x000676FF
			internal OrderedIntersectQueryOperatorEnumerator(QueryOperatorEnumerator<Pair<TInputOutput, NoKeyMemoizationRequired>, TLeftKey> leftSource, QueryOperatorEnumerator<Pair<TInputOutput, NoKeyMemoizationRequired>, int> rightSource, IEqualityComparer<TInputOutput> comparer, IComparer<TLeftKey> leftKeyComparer, CancellationToken cancellationToken)
			{
				this.m_leftSource = leftSource;
				this.m_rightSource = rightSource;
				this.m_comparer = new WrapperEqualityComparer<TInputOutput>(comparer);
				this.m_leftKeyComparer = leftKeyComparer;
				this.m_cancellationToken = cancellationToken;
			}

			// Token: 0x06001D7F RID: 7551 RVA: 0x00069538 File Offset: 0x00067738
			internal override bool MoveNext(ref TInputOutput currentElement, ref TLeftKey currentKey)
			{
				int num = 0;
				if (this.m_hashLookup == null)
				{
					this.m_hashLookup = new Dictionary<Wrapper<TInputOutput>, Pair<TInputOutput, TLeftKey>>(this.m_comparer);
					Pair<TInputOutput, NoKeyMemoizationRequired> pair = default(Pair<TInputOutput, NoKeyMemoizationRequired>);
					TLeftKey tleftKey = default(TLeftKey);
					while (this.m_leftSource.MoveNext(ref pair, ref tleftKey))
					{
						if ((num++ & 63) == 0)
						{
							CancellationState.ThrowIfCanceled(this.m_cancellationToken);
						}
						Wrapper<TInputOutput> key = new Wrapper<TInputOutput>(pair.First);
						Pair<TInputOutput, TLeftKey> pair2;
						if (!this.m_hashLookup.TryGetValue(key, out pair2) || this.m_leftKeyComparer.Compare(tleftKey, pair2.Second) < 0)
						{
							this.m_hashLookup[key] = new Pair<TInputOutput, TLeftKey>(pair.First, tleftKey);
						}
					}
				}
				Pair<TInputOutput, NoKeyMemoizationRequired> pair3 = default(Pair<TInputOutput, NoKeyMemoizationRequired>);
				int num2 = 0;
				while (this.m_rightSource.MoveNext(ref pair3, ref num2))
				{
					if ((num++ & 63) == 0)
					{
						CancellationState.ThrowIfCanceled(this.m_cancellationToken);
					}
					Wrapper<TInputOutput> key2 = new Wrapper<TInputOutput>(pair3.First);
					Pair<TInputOutput, TLeftKey> pair4;
					if (this.m_hashLookup.TryGetValue(key2, out pair4))
					{
						currentElement = pair4.First;
						currentKey = pair4.Second;
						this.m_hashLookup.Remove(new Wrapper<TInputOutput>(pair4.First));
						return true;
					}
				}
				return false;
			}

			// Token: 0x06001D80 RID: 7552 RVA: 0x00069672 File Offset: 0x00067872
			protected override void Dispose(bool disposing)
			{
				this.m_leftSource.Dispose();
				this.m_rightSource.Dispose();
			}

			// Token: 0x04001157 RID: 4439
			private QueryOperatorEnumerator<Pair<TInputOutput, NoKeyMemoizationRequired>, TLeftKey> m_leftSource;

			// Token: 0x04001158 RID: 4440
			private QueryOperatorEnumerator<Pair<TInputOutput, NoKeyMemoizationRequired>, int> m_rightSource;

			// Token: 0x04001159 RID: 4441
			private IEqualityComparer<Wrapper<TInputOutput>> m_comparer;

			// Token: 0x0400115A RID: 4442
			private IComparer<TLeftKey> m_leftKeyComparer;

			// Token: 0x0400115B RID: 4443
			private Dictionary<Wrapper<TInputOutput>, Pair<TInputOutput, TLeftKey>> m_hashLookup;

			// Token: 0x0400115C RID: 4444
			private CancellationToken m_cancellationToken;
		}
	}
}
