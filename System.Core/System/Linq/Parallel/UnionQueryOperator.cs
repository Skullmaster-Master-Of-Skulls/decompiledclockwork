using System;
using System.Collections.Generic;
using System.Threading;

namespace System.Linq.Parallel
{
	// Token: 0x0200019B RID: 411
	internal sealed class UnionQueryOperator<TInputOutput> : BinaryQueryOperator<TInputOutput, TInputOutput, TInputOutput>
	{
		// Token: 0x06000E62 RID: 3682 RVA: 0x000334F1 File Offset: 0x000316F1
		internal UnionQueryOperator(ParallelQuery<TInputOutput> left, ParallelQuery<TInputOutput> right, IEqualityComparer<TInputOutput> comparer) : base(left, right)
		{
			this.m_comparer = comparer;
			this.m_outputOrdered = (base.LeftChild.OutputOrdered || base.RightChild.OutputOrdered);
		}

		// Token: 0x06000E63 RID: 3683 RVA: 0x00033524 File Offset: 0x00031724
		internal override QueryResults<TInputOutput> Open(QuerySettings settings, bool preferStriping)
		{
			QueryResults<TInputOutput> leftChildQueryResults = base.LeftChild.Open(settings, false);
			QueryResults<TInputOutput> rightChildQueryResults = base.RightChild.Open(settings, false);
			return new BinaryQueryOperator<TInputOutput, TInputOutput, TInputOutput>.BinaryQueryOperatorResults(leftChildQueryResults, rightChildQueryResults, this, settings, false);
		}

		// Token: 0x06000E64 RID: 3684 RVA: 0x00033558 File Offset: 0x00031758
		public override void WrapPartitionedStream<TLeftKey, TRightKey>(PartitionedStream<TInputOutput, TLeftKey> leftStream, PartitionedStream<TInputOutput, TRightKey> rightStream, IPartitionedStreamRecipient<TInputOutput> outputRecipient, bool preferStriping, QuerySettings settings)
		{
			int partitionCount = leftStream.PartitionCount;
			if (base.LeftChild.OutputOrdered)
			{
				PartitionedStream<Pair<TInputOutput, NoKeyMemoizationRequired>, TLeftKey> leftHashStream = ExchangeUtilities.HashRepartitionOrdered<TInputOutput, NoKeyMemoizationRequired, TLeftKey>(leftStream, null, null, this.m_comparer, settings.CancellationState.MergedCancellationToken);
				this.WrapPartitionedStreamFixedLeftType<TLeftKey, TRightKey>(leftHashStream, rightStream, outputRecipient, partitionCount, settings.CancellationState.MergedCancellationToken);
				return;
			}
			PartitionedStream<Pair<TInputOutput, NoKeyMemoizationRequired>, int> leftHashStream2 = ExchangeUtilities.HashRepartition<TInputOutput, NoKeyMemoizationRequired, TLeftKey>(leftStream, null, null, this.m_comparer, settings.CancellationState.MergedCancellationToken);
			this.WrapPartitionedStreamFixedLeftType<int, TRightKey>(leftHashStream2, rightStream, outputRecipient, partitionCount, settings.CancellationState.MergedCancellationToken);
		}

		// Token: 0x06000E65 RID: 3685 RVA: 0x000335DC File Offset: 0x000317DC
		private void WrapPartitionedStreamFixedLeftType<TLeftKey, TRightKey>(PartitionedStream<Pair<TInputOutput, NoKeyMemoizationRequired>, TLeftKey> leftHashStream, PartitionedStream<TInputOutput, TRightKey> rightStream, IPartitionedStreamRecipient<TInputOutput> outputRecipient, int partitionCount, CancellationToken cancellationToken)
		{
			if (base.RightChild.OutputOrdered)
			{
				PartitionedStream<Pair<TInputOutput, NoKeyMemoizationRequired>, TRightKey> rightHashStream = ExchangeUtilities.HashRepartitionOrdered<TInputOutput, NoKeyMemoizationRequired, TRightKey>(rightStream, null, null, this.m_comparer, cancellationToken);
				this.WrapPartitionedStreamFixedBothTypes<TLeftKey, TRightKey>(leftHashStream, rightHashStream, outputRecipient, partitionCount, cancellationToken);
				return;
			}
			PartitionedStream<Pair<TInputOutput, NoKeyMemoizationRequired>, int> rightHashStream2 = ExchangeUtilities.HashRepartition<TInputOutput, NoKeyMemoizationRequired, TRightKey>(rightStream, null, null, this.m_comparer, cancellationToken);
			this.WrapPartitionedStreamFixedBothTypes<TLeftKey, int>(leftHashStream, rightHashStream2, outputRecipient, partitionCount, cancellationToken);
		}

		// Token: 0x06000E66 RID: 3686 RVA: 0x00033634 File Offset: 0x00031834
		private void WrapPartitionedStreamFixedBothTypes<TLeftKey, TRightKey>(PartitionedStream<Pair<TInputOutput, NoKeyMemoizationRequired>, TLeftKey> leftHashStream, PartitionedStream<Pair<TInputOutput, NoKeyMemoizationRequired>, TRightKey> rightHashStream, IPartitionedStreamRecipient<TInputOutput> outputRecipient, int partitionCount, CancellationToken cancellationToken)
		{
			if (base.LeftChild.OutputOrdered || base.RightChild.OutputOrdered)
			{
				IComparer<ConcatKey<TLeftKey, TRightKey>> keyComparer = ConcatKey<TLeftKey, TRightKey>.MakeComparer(leftHashStream.KeyComparer, rightHashStream.KeyComparer);
				PartitionedStream<TInputOutput, ConcatKey<TLeftKey, TRightKey>> partitionedStream = new PartitionedStream<TInputOutput, ConcatKey<TLeftKey, TRightKey>>(partitionCount, keyComparer, OrdinalIndexState.Shuffled);
				for (int i = 0; i < partitionCount; i++)
				{
					partitionedStream[i] = new UnionQueryOperator<TInputOutput>.OrderedUnionQueryOperatorEnumerator<TLeftKey, TRightKey>(leftHashStream[i], rightHashStream[i], base.LeftChild.OutputOrdered, base.RightChild.OutputOrdered, this.m_comparer, keyComparer, cancellationToken);
				}
				outputRecipient.Receive<ConcatKey<TLeftKey, TRightKey>>(partitionedStream);
				return;
			}
			PartitionedStream<TInputOutput, int> partitionedStream2 = new PartitionedStream<TInputOutput, int>(partitionCount, Util.GetDefaultComparer<int>(), OrdinalIndexState.Shuffled);
			for (int j = 0; j < partitionCount; j++)
			{
				partitionedStream2[j] = new UnionQueryOperator<TInputOutput>.UnionQueryOperatorEnumerator<TLeftKey, TRightKey>(leftHashStream[j], rightHashStream[j], j, this.m_comparer, cancellationToken);
			}
			outputRecipient.Receive<int>(partitionedStream2);
		}

		// Token: 0x06000E67 RID: 3687 RVA: 0x00033714 File Offset: 0x00031914
		internal override IEnumerable<TInputOutput> AsSequentialQuery(CancellationToken token)
		{
			IEnumerable<TInputOutput> first = CancellableEnumerable.Wrap<TInputOutput>(base.LeftChild.AsSequentialQuery(token), token);
			IEnumerable<TInputOutput> second = CancellableEnumerable.Wrap<TInputOutput>(base.RightChild.AsSequentialQuery(token), token);
			return first.Union(second, this.m_comparer);
		}

		// Token: 0x1700028E RID: 654
		// (get) Token: 0x06000E68 RID: 3688 RVA: 0x00033754 File Offset: 0x00031954
		internal override bool LimitsParallelism
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0400087D RID: 2173
		private readonly IEqualityComparer<TInputOutput> m_comparer;

		// Token: 0x020003C3 RID: 963
		private class UnionQueryOperatorEnumerator<TLeftKey, TRightKey> : QueryOperatorEnumerator<TInputOutput, int>
		{
			// Token: 0x06001D81 RID: 7553 RVA: 0x0006968A File Offset: 0x0006788A
			internal UnionQueryOperatorEnumerator(QueryOperatorEnumerator<Pair<TInputOutput, NoKeyMemoizationRequired>, TLeftKey> leftSource, QueryOperatorEnumerator<Pair<TInputOutput, NoKeyMemoizationRequired>, TRightKey> rightSource, int partitionIndex, IEqualityComparer<TInputOutput> comparer, CancellationToken cancellationToken)
			{
				this.m_leftSource = leftSource;
				this.m_rightSource = rightSource;
				this.m_partitionIndex = partitionIndex;
				this.m_comparer = comparer;
				this.m_cancellationToken = cancellationToken;
			}

			// Token: 0x06001D82 RID: 7554 RVA: 0x000696B8 File Offset: 0x000678B8
			internal override bool MoveNext(ref TInputOutput currentElement, ref int currentKey)
			{
				if (this.m_hashLookup == null)
				{
					this.m_hashLookup = new Set<TInputOutput>(this.m_comparer);
					this.m_outputLoopCount = new Shared<int>(0);
				}
				if (this.m_leftSource != null)
				{
					TLeftKey tleftKey = default(TLeftKey);
					Pair<TInputOutput, NoKeyMemoizationRequired> pair = default(Pair<TInputOutput, NoKeyMemoizationRequired>);
					int num = 0;
					while (this.m_leftSource.MoveNext(ref pair, ref tleftKey))
					{
						if ((num++ & 63) == 0)
						{
							CancellationState.ThrowIfCanceled(this.m_cancellationToken);
						}
						if (this.m_hashLookup.Add(pair.First))
						{
							currentElement = pair.First;
							return true;
						}
					}
					this.m_leftSource.Dispose();
					this.m_leftSource = null;
				}
				if (this.m_rightSource != null)
				{
					TRightKey trightKey = default(TRightKey);
					Pair<TInputOutput, NoKeyMemoizationRequired> pair2 = default(Pair<TInputOutput, NoKeyMemoizationRequired>);
					while (this.m_rightSource.MoveNext(ref pair2, ref trightKey))
					{
						Shared<int> outputLoopCount = this.m_outputLoopCount;
						int value = outputLoopCount.Value;
						outputLoopCount.Value = value + 1;
						if ((value & 63) == 0)
						{
							CancellationState.ThrowIfCanceled(this.m_cancellationToken);
						}
						if (this.m_hashLookup.Add(pair2.First))
						{
							currentElement = pair2.First;
							return true;
						}
					}
					this.m_rightSource.Dispose();
					this.m_rightSource = null;
				}
				return false;
			}

			// Token: 0x06001D83 RID: 7555 RVA: 0x000697EE File Offset: 0x000679EE
			protected override void Dispose(bool disposing)
			{
				if (this.m_leftSource != null)
				{
					this.m_leftSource.Dispose();
				}
				if (this.m_rightSource != null)
				{
					this.m_rightSource.Dispose();
				}
			}

			// Token: 0x0400115D RID: 4445
			private QueryOperatorEnumerator<Pair<TInputOutput, NoKeyMemoizationRequired>, TLeftKey> m_leftSource;

			// Token: 0x0400115E RID: 4446
			private QueryOperatorEnumerator<Pair<TInputOutput, NoKeyMemoizationRequired>, TRightKey> m_rightSource;

			// Token: 0x0400115F RID: 4447
			private readonly int m_partitionIndex;

			// Token: 0x04001160 RID: 4448
			private Set<TInputOutput> m_hashLookup;

			// Token: 0x04001161 RID: 4449
			private CancellationToken m_cancellationToken;

			// Token: 0x04001162 RID: 4450
			private Shared<int> m_outputLoopCount;

			// Token: 0x04001163 RID: 4451
			private readonly IEqualityComparer<TInputOutput> m_comparer;
		}

		// Token: 0x020003C4 RID: 964
		private class OrderedUnionQueryOperatorEnumerator<TLeftKey, TRightKey> : QueryOperatorEnumerator<TInputOutput, ConcatKey<TLeftKey, TRightKey>>
		{
			// Token: 0x06001D84 RID: 7556 RVA: 0x00069818 File Offset: 0x00067A18
			internal OrderedUnionQueryOperatorEnumerator(QueryOperatorEnumerator<Pair<TInputOutput, NoKeyMemoizationRequired>, TLeftKey> leftSource, QueryOperatorEnumerator<Pair<TInputOutput, NoKeyMemoizationRequired>, TRightKey> rightSource, bool leftOrdered, bool rightOrdered, IEqualityComparer<TInputOutput> comparer, IComparer<ConcatKey<TLeftKey, TRightKey>> keyComparer, CancellationToken cancellationToken)
			{
				this.m_leftSource = leftSource;
				this.m_rightSource = rightSource;
				this.m_keyComparer = keyComparer;
				this.m_leftOrdered = leftOrdered;
				this.m_rightOrdered = rightOrdered;
				this.m_comparer = comparer;
				if (this.m_comparer == null)
				{
					this.m_comparer = EqualityComparer<TInputOutput>.Default;
				}
				this.m_cancellationToken = cancellationToken;
			}

			// Token: 0x06001D85 RID: 7557 RVA: 0x00069874 File Offset: 0x00067A74
			internal override bool MoveNext(ref TInputOutput currentElement, ref ConcatKey<TLeftKey, TRightKey> currentKey)
			{
				if (this.m_outputEnumerator == null)
				{
					IEqualityComparer<Wrapper<TInputOutput>> comparer = new WrapperEqualityComparer<TInputOutput>(this.m_comparer);
					Dictionary<Wrapper<TInputOutput>, Pair<TInputOutput, ConcatKey<TLeftKey, TRightKey>>> dictionary = new Dictionary<Wrapper<TInputOutput>, Pair<TInputOutput, ConcatKey<TLeftKey, TRightKey>>>(comparer);
					Pair<TInputOutput, NoKeyMemoizationRequired> pair = default(Pair<TInputOutput, NoKeyMemoizationRequired>);
					TLeftKey tleftKey = default(TLeftKey);
					int num = 0;
					while (this.m_leftSource.MoveNext(ref pair, ref tleftKey))
					{
						if ((num++ & 63) == 0)
						{
							CancellationState.ThrowIfCanceled(this.m_cancellationToken);
						}
						ConcatKey<TLeftKey, TRightKey> concatKey = ConcatKey<TLeftKey, TRightKey>.MakeLeft(this.m_leftOrdered ? tleftKey : default(TLeftKey));
						Wrapper<TInputOutput> key = new Wrapper<TInputOutput>(pair.First);
						Pair<TInputOutput, ConcatKey<TLeftKey, TRightKey>> pair2;
						if (!dictionary.TryGetValue(key, out pair2) || this.m_keyComparer.Compare(concatKey, pair2.Second) < 0)
						{
							dictionary[key] = new Pair<TInputOutput, ConcatKey<TLeftKey, TRightKey>>(pair.First, concatKey);
						}
					}
					TRightKey trightKey = default(TRightKey);
					while (this.m_rightSource.MoveNext(ref pair, ref trightKey))
					{
						if ((num++ & 63) == 0)
						{
							CancellationState.ThrowIfCanceled(this.m_cancellationToken);
						}
						ConcatKey<TLeftKey, TRightKey> concatKey2 = ConcatKey<TLeftKey, TRightKey>.MakeRight(this.m_rightOrdered ? trightKey : default(TRightKey));
						Wrapper<TInputOutput> key2 = new Wrapper<TInputOutput>(pair.First);
						Pair<TInputOutput, ConcatKey<TLeftKey, TRightKey>> pair3;
						if (!dictionary.TryGetValue(key2, out pair3) || this.m_keyComparer.Compare(concatKey2, pair3.Second) < 0)
						{
							dictionary[key2] = new Pair<TInputOutput, ConcatKey<TLeftKey, TRightKey>>(pair.First, concatKey2);
						}
					}
					this.m_outputEnumerator = dictionary.GetEnumerator();
				}
				if (this.m_outputEnumerator.MoveNext())
				{
					KeyValuePair<Wrapper<TInputOutput>, Pair<TInputOutput, ConcatKey<TLeftKey, TRightKey>>> keyValuePair = this.m_outputEnumerator.Current;
					Pair<TInputOutput, ConcatKey<TLeftKey, TRightKey>> value = keyValuePair.Value;
					currentElement = value.First;
					currentKey = value.Second;
					return true;
				}
				return false;
			}

			// Token: 0x06001D86 RID: 7558 RVA: 0x00069A31 File Offset: 0x00067C31
			protected override void Dispose(bool disposing)
			{
				this.m_leftSource.Dispose();
				this.m_rightSource.Dispose();
			}

			// Token: 0x04001164 RID: 4452
			private QueryOperatorEnumerator<Pair<TInputOutput, NoKeyMemoizationRequired>, TLeftKey> m_leftSource;

			// Token: 0x04001165 RID: 4453
			private QueryOperatorEnumerator<Pair<TInputOutput, NoKeyMemoizationRequired>, TRightKey> m_rightSource;

			// Token: 0x04001166 RID: 4454
			private IComparer<ConcatKey<TLeftKey, TRightKey>> m_keyComparer;

			// Token: 0x04001167 RID: 4455
			private IEnumerator<KeyValuePair<Wrapper<TInputOutput>, Pair<TInputOutput, ConcatKey<TLeftKey, TRightKey>>>> m_outputEnumerator;

			// Token: 0x04001168 RID: 4456
			private bool m_leftOrdered;

			// Token: 0x04001169 RID: 4457
			private bool m_rightOrdered;

			// Token: 0x0400116A RID: 4458
			private IEqualityComparer<TInputOutput> m_comparer;

			// Token: 0x0400116B RID: 4459
			private CancellationToken m_cancellationToken;
		}
	}
}
