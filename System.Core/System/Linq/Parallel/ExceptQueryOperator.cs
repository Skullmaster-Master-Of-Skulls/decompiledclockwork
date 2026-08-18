using System;
using System.Collections.Generic;
using System.Threading;

namespace System.Linq.Parallel
{
	// Token: 0x02000196 RID: 406
	internal sealed class ExceptQueryOperator<TInputOutput> : BinaryQueryOperator<TInputOutput, TInputOutput, TInputOutput>
	{
		// Token: 0x06000E47 RID: 3655 RVA: 0x00032ACD File Offset: 0x00030CCD
		internal ExceptQueryOperator(ParallelQuery<TInputOutput> left, ParallelQuery<TInputOutput> right, IEqualityComparer<TInputOutput> comparer) : base(left, right)
		{
			this.m_comparer = comparer;
			this.m_outputOrdered = base.LeftChild.OutputOrdered;
			base.SetOrdinalIndex(OrdinalIndexState.Shuffled);
		}

		// Token: 0x06000E48 RID: 3656 RVA: 0x00032AF8 File Offset: 0x00030CF8
		internal override QueryResults<TInputOutput> Open(QuerySettings settings, bool preferStriping)
		{
			QueryResults<TInputOutput> leftChildQueryResults = base.LeftChild.Open(settings, false);
			QueryResults<TInputOutput> rightChildQueryResults = base.RightChild.Open(settings, false);
			return new BinaryQueryOperator<TInputOutput, TInputOutput, TInputOutput>.BinaryQueryOperatorResults(leftChildQueryResults, rightChildQueryResults, this, settings, false);
		}

		// Token: 0x06000E49 RID: 3657 RVA: 0x00032B2C File Offset: 0x00030D2C
		public override void WrapPartitionedStream<TLeftKey, TRightKey>(PartitionedStream<TInputOutput, TLeftKey> leftStream, PartitionedStream<TInputOutput, TRightKey> rightStream, IPartitionedStreamRecipient<TInputOutput> outputRecipient, bool preferStriping, QuerySettings settings)
		{
			if (base.OutputOrdered)
			{
				this.WrapPartitionedStreamHelper<TLeftKey, TRightKey>(ExchangeUtilities.HashRepartitionOrdered<TInputOutput, NoKeyMemoizationRequired, TLeftKey>(leftStream, null, null, this.m_comparer, settings.CancellationState.MergedCancellationToken), rightStream, outputRecipient, settings.CancellationState.MergedCancellationToken);
				return;
			}
			this.WrapPartitionedStreamHelper<int, TRightKey>(ExchangeUtilities.HashRepartition<TInputOutput, NoKeyMemoizationRequired, TLeftKey>(leftStream, null, null, this.m_comparer, settings.CancellationState.MergedCancellationToken), rightStream, outputRecipient, settings.CancellationState.MergedCancellationToken);
		}

		// Token: 0x06000E4A RID: 3658 RVA: 0x00032BA0 File Offset: 0x00030DA0
		private void WrapPartitionedStreamHelper<TLeftKey, TRightKey>(PartitionedStream<Pair<TInputOutput, NoKeyMemoizationRequired>, TLeftKey> leftHashStream, PartitionedStream<TInputOutput, TRightKey> rightPartitionedStream, IPartitionedStreamRecipient<TInputOutput> outputRecipient, CancellationToken cancellationToken)
		{
			int partitionCount = leftHashStream.PartitionCount;
			PartitionedStream<Pair<TInputOutput, NoKeyMemoizationRequired>, int> partitionedStream = ExchangeUtilities.HashRepartition<TInputOutput, NoKeyMemoizationRequired, TRightKey>(rightPartitionedStream, null, null, this.m_comparer, cancellationToken);
			PartitionedStream<TInputOutput, TLeftKey> partitionedStream2 = new PartitionedStream<TInputOutput, TLeftKey>(partitionCount, leftHashStream.KeyComparer, OrdinalIndexState.Shuffled);
			for (int i = 0; i < partitionCount; i++)
			{
				if (base.OutputOrdered)
				{
					partitionedStream2[i] = new ExceptQueryOperator<TInputOutput>.OrderedExceptQueryOperatorEnumerator<TLeftKey>(leftHashStream[i], partitionedStream[i], this.m_comparer, leftHashStream.KeyComparer, cancellationToken);
				}
				else
				{
					partitionedStream2[i] = (QueryOperatorEnumerator<TInputOutput, TLeftKey>)new ExceptQueryOperator<TInputOutput>.ExceptQueryOperatorEnumerator<TLeftKey>(leftHashStream[i], partitionedStream[i], this.m_comparer, cancellationToken);
				}
			}
			outputRecipient.Receive<TLeftKey>(partitionedStream2);
		}

		// Token: 0x06000E4B RID: 3659 RVA: 0x00032C40 File Offset: 0x00030E40
		internal override IEnumerable<TInputOutput> AsSequentialQuery(CancellationToken token)
		{
			IEnumerable<TInputOutput> first = CancellableEnumerable.Wrap<TInputOutput>(base.LeftChild.AsSequentialQuery(token), token);
			IEnumerable<TInputOutput> second = CancellableEnumerable.Wrap<TInputOutput>(base.RightChild.AsSequentialQuery(token), token);
			return first.Except(second, this.m_comparer);
		}

		// Token: 0x1700028A RID: 650
		// (get) Token: 0x06000E4C RID: 3660 RVA: 0x00032C80 File Offset: 0x00030E80
		internal override bool LimitsParallelism
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0400086C RID: 2156
		private readonly IEqualityComparer<TInputOutput> m_comparer;

		// Token: 0x020003BE RID: 958
		private class ExceptQueryOperatorEnumerator<TLeftKey> : QueryOperatorEnumerator<TInputOutput, int>
		{
			// Token: 0x06001D74 RID: 7540 RVA: 0x000690E8 File Offset: 0x000672E8
			internal ExceptQueryOperatorEnumerator(QueryOperatorEnumerator<Pair<TInputOutput, NoKeyMemoizationRequired>, TLeftKey> leftSource, QueryOperatorEnumerator<Pair<TInputOutput, NoKeyMemoizationRequired>, int> rightSource, IEqualityComparer<TInputOutput> comparer, CancellationToken cancellationToken)
			{
				this.m_leftSource = leftSource;
				this.m_rightSource = rightSource;
				this.m_comparer = comparer;
				this.m_cancellationToken = cancellationToken;
			}

			// Token: 0x06001D75 RID: 7541 RVA: 0x00069110 File Offset: 0x00067310
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
					if (this.m_hashLookup.Add(pair2.First))
					{
						currentElement = pair2.First;
						return true;
					}
				}
				return false;
			}

			// Token: 0x06001D76 RID: 7542 RVA: 0x000691FC File Offset: 0x000673FC
			protected override void Dispose(bool disposing)
			{
				this.m_leftSource.Dispose();
				this.m_rightSource.Dispose();
			}

			// Token: 0x0400113F RID: 4415
			private QueryOperatorEnumerator<Pair<TInputOutput, NoKeyMemoizationRequired>, TLeftKey> m_leftSource;

			// Token: 0x04001140 RID: 4416
			private QueryOperatorEnumerator<Pair<TInputOutput, NoKeyMemoizationRequired>, int> m_rightSource;

			// Token: 0x04001141 RID: 4417
			private IEqualityComparer<TInputOutput> m_comparer;

			// Token: 0x04001142 RID: 4418
			private Set<TInputOutput> m_hashLookup;

			// Token: 0x04001143 RID: 4419
			private CancellationToken m_cancellationToken;

			// Token: 0x04001144 RID: 4420
			private Shared<int> m_outputLoopCount;
		}

		// Token: 0x020003BF RID: 959
		private class OrderedExceptQueryOperatorEnumerator<TLeftKey> : QueryOperatorEnumerator<TInputOutput, TLeftKey>
		{
			// Token: 0x06001D77 RID: 7543 RVA: 0x00069214 File Offset: 0x00067414
			internal OrderedExceptQueryOperatorEnumerator(QueryOperatorEnumerator<Pair<TInputOutput, NoKeyMemoizationRequired>, TLeftKey> leftSource, QueryOperatorEnumerator<Pair<TInputOutput, NoKeyMemoizationRequired>, int> rightSource, IEqualityComparer<TInputOutput> comparer, IComparer<TLeftKey> leftKeyComparer, CancellationToken cancellationToken)
			{
				this.m_leftSource = leftSource;
				this.m_rightSource = rightSource;
				this.m_comparer = comparer;
				this.m_leftKeyComparer = leftKeyComparer;
				this.m_cancellationToken = cancellationToken;
			}

			// Token: 0x06001D78 RID: 7544 RVA: 0x00069244 File Offset: 0x00067444
			internal override bool MoveNext(ref TInputOutput currentElement, ref TLeftKey currentKey)
			{
				if (this.m_outputEnumerator == null)
				{
					Set<TInputOutput> set = new Set<TInputOutput>(this.m_comparer);
					Pair<TInputOutput, NoKeyMemoizationRequired> pair = default(Pair<TInputOutput, NoKeyMemoizationRequired>);
					int num = 0;
					int num2 = 0;
					while (this.m_rightSource.MoveNext(ref pair, ref num))
					{
						if ((num2++ & 63) == 0)
						{
							CancellationState.ThrowIfCanceled(this.m_cancellationToken);
						}
						set.Add(pair.First);
					}
					Dictionary<Wrapper<TInputOutput>, Pair<TInputOutput, TLeftKey>> dictionary = new Dictionary<Wrapper<TInputOutput>, Pair<TInputOutput, TLeftKey>>(new WrapperEqualityComparer<TInputOutput>(this.m_comparer));
					Pair<TInputOutput, NoKeyMemoizationRequired> pair2 = default(Pair<TInputOutput, NoKeyMemoizationRequired>);
					TLeftKey tleftKey = default(TLeftKey);
					while (this.m_leftSource.MoveNext(ref pair2, ref tleftKey))
					{
						if ((num2++ & 63) == 0)
						{
							CancellationState.ThrowIfCanceled(this.m_cancellationToken);
						}
						if (!set.Contains(pair2.First))
						{
							Wrapper<TInputOutput> key = new Wrapper<TInputOutput>(pair2.First);
							Pair<TInputOutput, TLeftKey> pair3;
							if (!dictionary.TryGetValue(key, out pair3) || this.m_leftKeyComparer.Compare(tleftKey, pair3.Second) < 0)
							{
								dictionary[key] = new Pair<TInputOutput, TLeftKey>(pair2.First, tleftKey);
							}
						}
					}
					this.m_outputEnumerator = dictionary.GetEnumerator();
				}
				if (this.m_outputEnumerator.MoveNext())
				{
					KeyValuePair<Wrapper<TInputOutput>, Pair<TInputOutput, TLeftKey>> keyValuePair = this.m_outputEnumerator.Current;
					Pair<TInputOutput, TLeftKey> value = keyValuePair.Value;
					currentElement = value.First;
					currentKey = value.Second;
					return true;
				}
				return false;
			}

			// Token: 0x06001D79 RID: 7545 RVA: 0x000693A3 File Offset: 0x000675A3
			protected override void Dispose(bool disposing)
			{
				this.m_leftSource.Dispose();
				this.m_rightSource.Dispose();
			}

			// Token: 0x04001145 RID: 4421
			private QueryOperatorEnumerator<Pair<TInputOutput, NoKeyMemoizationRequired>, TLeftKey> m_leftSource;

			// Token: 0x04001146 RID: 4422
			private QueryOperatorEnumerator<Pair<TInputOutput, NoKeyMemoizationRequired>, int> m_rightSource;

			// Token: 0x04001147 RID: 4423
			private IEqualityComparer<TInputOutput> m_comparer;

			// Token: 0x04001148 RID: 4424
			private IComparer<TLeftKey> m_leftKeyComparer;

			// Token: 0x04001149 RID: 4425
			private IEnumerator<KeyValuePair<Wrapper<TInputOutput>, Pair<TInputOutput, TLeftKey>>> m_outputEnumerator;

			// Token: 0x0400114A RID: 4426
			private CancellationToken m_cancellationToken;
		}
	}
}
