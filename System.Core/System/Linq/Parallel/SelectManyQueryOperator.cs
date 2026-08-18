using System;
using System.Collections.Generic;
using System.Threading;

namespace System.Linq.Parallel
{
	// Token: 0x020001E0 RID: 480
	internal sealed class SelectManyQueryOperator<TLeftInput, TRightInput, TOutput> : UnaryQueryOperator<TLeftInput, TOutput>
	{
		// Token: 0x06000FA2 RID: 4002 RVA: 0x00037471 File Offset: 0x00035671
		internal SelectManyQueryOperator(IEnumerable<TLeftInput> leftChild, Func<TLeftInput, IEnumerable<TRightInput>> rightChildSelector, Func<TLeftInput, int, IEnumerable<TRightInput>> indexedRightChildSelector, Func<TLeftInput, TRightInput, TOutput> resultSelector) : base(leftChild)
		{
			this.m_rightChildSelector = rightChildSelector;
			this.m_indexedRightChildSelector = indexedRightChildSelector;
			this.m_resultSelector = resultSelector;
			this.m_outputOrdered = (base.Child.OutputOrdered || indexedRightChildSelector != null);
			this.InitOrderIndex();
		}

		// Token: 0x06000FA3 RID: 4003 RVA: 0x000374B0 File Offset: 0x000356B0
		private void InitOrderIndex()
		{
			OrdinalIndexState ordinalIndexState = base.Child.OrdinalIndexState;
			if (this.m_indexedRightChildSelector != null)
			{
				this.m_prematureMerge = ordinalIndexState.IsWorseThan(OrdinalIndexState.Correct);
				this.m_limitsParallelism = (this.m_prematureMerge && ordinalIndexState != OrdinalIndexState.Shuffled);
			}
			else if (base.OutputOrdered)
			{
				this.m_prematureMerge = ordinalIndexState.IsWorseThan(OrdinalIndexState.Increasing);
			}
			base.SetOrdinalIndexState(OrdinalIndexState.Increasing);
		}

		// Token: 0x06000FA4 RID: 4004 RVA: 0x00037514 File Offset: 0x00035714
		internal override void WrapPartitionedStream<TLeftKey>(PartitionedStream<TLeftInput, TLeftKey> inputStream, IPartitionedStreamRecipient<TOutput> recipient, bool preferStriping, QuerySettings settings)
		{
			int partitionCount = inputStream.PartitionCount;
			if (this.m_indexedRightChildSelector != null)
			{
				PartitionedStream<TLeftInput, int> inputStream2;
				if (this.m_prematureMerge)
				{
					ListQueryResults<TLeftInput> listQueryResults = QueryOperator<TLeftInput>.ExecuteAndCollectResults<TLeftKey>(inputStream, partitionCount, base.OutputOrdered, preferStriping, settings);
					inputStream2 = listQueryResults.GetPartitionedStream();
				}
				else
				{
					inputStream2 = (PartitionedStream<TLeftInput, int>)inputStream;
				}
				this.WrapPartitionedStreamIndexed(inputStream2, recipient, settings);
				return;
			}
			if (this.m_prematureMerge)
			{
				PartitionedStream<TLeftInput, int> partitionedStream = QueryOperator<TLeftInput>.ExecuteAndCollectResults<TLeftKey>(inputStream, partitionCount, base.OutputOrdered, preferStriping, settings).GetPartitionedStream();
				this.WrapPartitionedStreamNotIndexed<int>(partitionedStream, recipient, settings);
				return;
			}
			this.WrapPartitionedStreamNotIndexed<TLeftKey>(inputStream, recipient, settings);
		}

		// Token: 0x06000FA5 RID: 4005 RVA: 0x00037598 File Offset: 0x00035798
		private void WrapPartitionedStreamNotIndexed<TLeftKey>(PartitionedStream<TLeftInput, TLeftKey> inputStream, IPartitionedStreamRecipient<TOutput> recipient, QuerySettings settings)
		{
			int partitionCount = inputStream.PartitionCount;
			PairComparer<TLeftKey, int> keyComparer = new PairComparer<TLeftKey, int>(inputStream.KeyComparer, Util.GetDefaultComparer<int>());
			PartitionedStream<TOutput, Pair<TLeftKey, int>> partitionedStream = new PartitionedStream<TOutput, Pair<TLeftKey, int>>(partitionCount, keyComparer, this.OrdinalIndexState);
			for (int i = 0; i < partitionCount; i++)
			{
				partitionedStream[i] = new SelectManyQueryOperator<TLeftInput, TRightInput, TOutput>.SelectManyQueryOperatorEnumerator<TLeftKey>(inputStream[i], this, settings.CancellationState.MergedCancellationToken);
			}
			recipient.Receive<Pair<TLeftKey, int>>(partitionedStream);
		}

		// Token: 0x06000FA6 RID: 4006 RVA: 0x00037600 File Offset: 0x00035800
		private void WrapPartitionedStreamIndexed(PartitionedStream<TLeftInput, int> inputStream, IPartitionedStreamRecipient<TOutput> recipient, QuerySettings settings)
		{
			PairComparer<int, int> keyComparer = new PairComparer<int, int>(inputStream.KeyComparer, Util.GetDefaultComparer<int>());
			PartitionedStream<TOutput, Pair<int, int>> partitionedStream = new PartitionedStream<TOutput, Pair<int, int>>(inputStream.PartitionCount, keyComparer, this.OrdinalIndexState);
			for (int i = 0; i < inputStream.PartitionCount; i++)
			{
				partitionedStream[i] = new SelectManyQueryOperator<TLeftInput, TRightInput, TOutput>.IndexedSelectManyQueryOperatorEnumerator(inputStream[i], this, settings.CancellationState.MergedCancellationToken);
			}
			recipient.Receive<Pair<int, int>>(partitionedStream);
		}

		// Token: 0x06000FA7 RID: 4007 RVA: 0x0003766C File Offset: 0x0003586C
		internal override QueryResults<TOutput> Open(QuerySettings settings, bool preferStriping)
		{
			QueryResults<TLeftInput> childQueryResults = base.Child.Open(settings, preferStriping);
			return new UnaryQueryOperator<TLeftInput, TOutput>.UnaryQueryOperatorResults(childQueryResults, this, settings, preferStriping);
		}

		// Token: 0x06000FA8 RID: 4008 RVA: 0x00037690 File Offset: 0x00035890
		internal override IEnumerable<TOutput> AsSequentialQuery(CancellationToken token)
		{
			if (this.m_rightChildSelector != null)
			{
				if (this.m_resultSelector != null)
				{
					return CancellableEnumerable.Wrap<TLeftInput>(base.Child.AsSequentialQuery(token), token).SelectMany(this.m_rightChildSelector, this.m_resultSelector);
				}
				return (IEnumerable<TOutput>)CancellableEnumerable.Wrap<TLeftInput>(base.Child.AsSequentialQuery(token), token).SelectMany(this.m_rightChildSelector);
			}
			else
			{
				if (this.m_resultSelector != null)
				{
					return CancellableEnumerable.Wrap<TLeftInput>(base.Child.AsSequentialQuery(token), token).SelectMany(this.m_indexedRightChildSelector, this.m_resultSelector);
				}
				return (IEnumerable<TOutput>)CancellableEnumerable.Wrap<TLeftInput>(base.Child.AsSequentialQuery(token), token).SelectMany(this.m_indexedRightChildSelector);
			}
		}

		// Token: 0x170002BC RID: 700
		// (get) Token: 0x06000FA9 RID: 4009 RVA: 0x00037742 File Offset: 0x00035942
		internal override bool LimitsParallelism
		{
			get
			{
				return this.m_limitsParallelism;
			}
		}

		// Token: 0x040008E5 RID: 2277
		private readonly Func<TLeftInput, IEnumerable<TRightInput>> m_rightChildSelector;

		// Token: 0x040008E6 RID: 2278
		private readonly Func<TLeftInput, int, IEnumerable<TRightInput>> m_indexedRightChildSelector;

		// Token: 0x040008E7 RID: 2279
		private readonly Func<TLeftInput, TRightInput, TOutput> m_resultSelector;

		// Token: 0x040008E8 RID: 2280
		private bool m_prematureMerge;

		// Token: 0x040008E9 RID: 2281
		private bool m_limitsParallelism;

		// Token: 0x02000401 RID: 1025
		private class IndexedSelectManyQueryOperatorEnumerator : QueryOperatorEnumerator<TOutput, Pair<int, int>>
		{
			// Token: 0x06001E47 RID: 7751 RVA: 0x0006C517 File Offset: 0x0006A717
			internal IndexedSelectManyQueryOperatorEnumerator(QueryOperatorEnumerator<TLeftInput, int> leftSource, SelectManyQueryOperator<TLeftInput, TRightInput, TOutput> selectManyOperator, CancellationToken cancellationToken)
			{
				this.m_leftSource = leftSource;
				this.m_selectManyOperator = selectManyOperator;
				this.m_cancellationToken = cancellationToken;
			}

			// Token: 0x06001E48 RID: 7752 RVA: 0x0006C534 File Offset: 0x0006A734
			internal override bool MoveNext(ref TOutput currentElement, ref Pair<int, int> currentKey)
			{
				for (;;)
				{
					if (this.m_currentRightSource == null)
					{
						this.m_mutables = new SelectManyQueryOperator<TLeftInput, TRightInput, TOutput>.IndexedSelectManyQueryOperatorEnumerator.Mutables();
						SelectManyQueryOperator<TLeftInput, TRightInput, TOutput>.IndexedSelectManyQueryOperatorEnumerator.Mutables mutables = this.m_mutables;
						int lhsCount = mutables.m_lhsCount;
						mutables.m_lhsCount = lhsCount + 1;
						if ((lhsCount & 63) == 0)
						{
							CancellationState.ThrowIfCanceled(this.m_cancellationToken);
						}
						if (!this.m_leftSource.MoveNext(ref this.m_mutables.m_currentLeftElement, ref this.m_mutables.m_currentLeftSourceIndex))
						{
							break;
						}
						IEnumerable<TRightInput> enumerable = this.m_selectManyOperator.m_indexedRightChildSelector(this.m_mutables.m_currentLeftElement, this.m_mutables.m_currentLeftSourceIndex);
						this.m_currentRightSource = enumerable.GetEnumerator();
						if (this.m_selectManyOperator.m_resultSelector == null)
						{
							this.m_currentRightSourceAsOutput = (IEnumerator<TOutput>)this.m_currentRightSource;
						}
					}
					if (this.m_currentRightSource.MoveNext())
					{
						goto Block_4;
					}
					this.m_currentRightSource.Dispose();
					this.m_currentRightSource = null;
					this.m_currentRightSourceAsOutput = null;
				}
				return false;
				Block_4:
				this.m_mutables.m_currentRightSourceIndex++;
				if (this.m_selectManyOperator.m_resultSelector != null)
				{
					currentElement = this.m_selectManyOperator.m_resultSelector(this.m_mutables.m_currentLeftElement, this.m_currentRightSource.Current);
				}
				else
				{
					currentElement = this.m_currentRightSourceAsOutput.Current;
				}
				currentKey = new Pair<int, int>(this.m_mutables.m_currentLeftSourceIndex, this.m_mutables.m_currentRightSourceIndex);
				return true;
			}

			// Token: 0x06001E49 RID: 7753 RVA: 0x0006C6A2 File Offset: 0x0006A8A2
			protected override void Dispose(bool disposing)
			{
				this.m_leftSource.Dispose();
				if (this.m_currentRightSource != null)
				{
					this.m_currentRightSource.Dispose();
				}
			}

			// Token: 0x04001208 RID: 4616
			private readonly QueryOperatorEnumerator<TLeftInput, int> m_leftSource;

			// Token: 0x04001209 RID: 4617
			private readonly SelectManyQueryOperator<TLeftInput, TRightInput, TOutput> m_selectManyOperator;

			// Token: 0x0400120A RID: 4618
			private IEnumerator<TRightInput> m_currentRightSource;

			// Token: 0x0400120B RID: 4619
			private IEnumerator<TOutput> m_currentRightSourceAsOutput;

			// Token: 0x0400120C RID: 4620
			private SelectManyQueryOperator<TLeftInput, TRightInput, TOutput>.IndexedSelectManyQueryOperatorEnumerator.Mutables m_mutables;

			// Token: 0x0400120D RID: 4621
			private readonly CancellationToken m_cancellationToken;

			// Token: 0x02000495 RID: 1173
			private class Mutables
			{
				// Token: 0x040013F5 RID: 5109
				internal int m_currentRightSourceIndex = -1;

				// Token: 0x040013F6 RID: 5110
				internal TLeftInput m_currentLeftElement;

				// Token: 0x040013F7 RID: 5111
				internal int m_currentLeftSourceIndex;

				// Token: 0x040013F8 RID: 5112
				internal int m_lhsCount;
			}
		}

		// Token: 0x02000402 RID: 1026
		private class SelectManyQueryOperatorEnumerator<TLeftKey> : QueryOperatorEnumerator<TOutput, Pair<TLeftKey, int>>
		{
			// Token: 0x06001E4A RID: 7754 RVA: 0x0006C6C2 File Offset: 0x0006A8C2
			internal SelectManyQueryOperatorEnumerator(QueryOperatorEnumerator<TLeftInput, TLeftKey> leftSource, SelectManyQueryOperator<TLeftInput, TRightInput, TOutput> selectManyOperator, CancellationToken cancellationToken)
			{
				this.m_leftSource = leftSource;
				this.m_selectManyOperator = selectManyOperator;
				this.m_cancellationToken = cancellationToken;
			}

			// Token: 0x06001E4B RID: 7755 RVA: 0x0006C6E0 File Offset: 0x0006A8E0
			internal override bool MoveNext(ref TOutput currentElement, ref Pair<TLeftKey, int> currentKey)
			{
				for (;;)
				{
					if (this.m_currentRightSource == null)
					{
						this.m_mutables = new SelectManyQueryOperator<TLeftInput, TRightInput, TOutput>.SelectManyQueryOperatorEnumerator<TLeftKey>.Mutables();
						SelectManyQueryOperator<TLeftInput, TRightInput, TOutput>.SelectManyQueryOperatorEnumerator<TLeftKey>.Mutables mutables = this.m_mutables;
						int lhsCount = mutables.m_lhsCount;
						mutables.m_lhsCount = lhsCount + 1;
						if ((lhsCount & 63) == 0)
						{
							CancellationState.ThrowIfCanceled(this.m_cancellationToken);
						}
						if (!this.m_leftSource.MoveNext(ref this.m_mutables.m_currentLeftElement, ref this.m_mutables.m_currentLeftKey))
						{
							break;
						}
						IEnumerable<TRightInput> enumerable = this.m_selectManyOperator.m_rightChildSelector(this.m_mutables.m_currentLeftElement);
						this.m_currentRightSource = enumerable.GetEnumerator();
						if (this.m_selectManyOperator.m_resultSelector == null)
						{
							this.m_currentRightSourceAsOutput = (IEnumerator<TOutput>)this.m_currentRightSource;
						}
					}
					if (this.m_currentRightSource.MoveNext())
					{
						goto Block_4;
					}
					this.m_currentRightSource.Dispose();
					this.m_currentRightSource = null;
					this.m_currentRightSourceAsOutput = null;
				}
				return false;
				Block_4:
				this.m_mutables.m_currentRightSourceIndex++;
				if (this.m_selectManyOperator.m_resultSelector != null)
				{
					currentElement = this.m_selectManyOperator.m_resultSelector(this.m_mutables.m_currentLeftElement, this.m_currentRightSource.Current);
				}
				else
				{
					currentElement = this.m_currentRightSourceAsOutput.Current;
				}
				currentKey = new Pair<TLeftKey, int>(this.m_mutables.m_currentLeftKey, this.m_mutables.m_currentRightSourceIndex);
				return true;
			}

			// Token: 0x06001E4C RID: 7756 RVA: 0x0006C843 File Offset: 0x0006AA43
			protected override void Dispose(bool disposing)
			{
				this.m_leftSource.Dispose();
				if (this.m_currentRightSource != null)
				{
					this.m_currentRightSource.Dispose();
				}
			}

			// Token: 0x0400120E RID: 4622
			private readonly QueryOperatorEnumerator<TLeftInput, TLeftKey> m_leftSource;

			// Token: 0x0400120F RID: 4623
			private readonly SelectManyQueryOperator<TLeftInput, TRightInput, TOutput> m_selectManyOperator;

			// Token: 0x04001210 RID: 4624
			private IEnumerator<TRightInput> m_currentRightSource;

			// Token: 0x04001211 RID: 4625
			private IEnumerator<TOutput> m_currentRightSourceAsOutput;

			// Token: 0x04001212 RID: 4626
			private SelectManyQueryOperator<TLeftInput, TRightInput, TOutput>.SelectManyQueryOperatorEnumerator<TLeftKey>.Mutables m_mutables;

			// Token: 0x04001213 RID: 4627
			private readonly CancellationToken m_cancellationToken;

			// Token: 0x02000496 RID: 1174
			private class Mutables
			{
				// Token: 0x040013F9 RID: 5113
				internal int m_currentRightSourceIndex = -1;

				// Token: 0x040013FA RID: 5114
				internal TLeftInput m_currentLeftElement;

				// Token: 0x040013FB RID: 5115
				internal TLeftKey m_currentLeftKey;

				// Token: 0x040013FC RID: 5116
				internal int m_lhsCount;
			}
		}
	}
}
