using System;
using System.Collections.Generic;
using System.Threading;

namespace System.Linq.Parallel
{
	// Token: 0x0200019C RID: 412
	internal sealed class ZipQueryOperator<TLeftInput, TRightInput, TOutput> : QueryOperator<TOutput>
	{
		// Token: 0x06000E69 RID: 3689 RVA: 0x00033757 File Offset: 0x00031957
		internal ZipQueryOperator(ParallelQuery<TLeftInput> leftChildSource, IEnumerable<TRightInput> rightChildSource, Func<TLeftInput, TRightInput, TOutput> resultSelector) : this(QueryOperator<TLeftInput>.AsQueryOperator(leftChildSource), QueryOperator<TRightInput>.AsQueryOperator(rightChildSource), resultSelector)
		{
		}

		// Token: 0x06000E6A RID: 3690 RVA: 0x0003376C File Offset: 0x0003196C
		private ZipQueryOperator(QueryOperator<TLeftInput> left, QueryOperator<TRightInput> right, Func<TLeftInput, TRightInput, TOutput> resultSelector) : base(left.SpecifiedQuerySettings.Merge(right.SpecifiedQuerySettings))
		{
			this.m_leftChild = left;
			this.m_rightChild = right;
			this.m_resultSelector = resultSelector;
			this.m_outputOrdered = (this.m_leftChild.OutputOrdered || this.m_rightChild.OutputOrdered);
			OrdinalIndexState ordinalIndexState = this.m_leftChild.OrdinalIndexState;
			OrdinalIndexState ordinalIndexState2 = this.m_rightChild.OrdinalIndexState;
			this.m_prematureMergeLeft = (ordinalIndexState > OrdinalIndexState.Indexible);
			this.m_prematureMergeRight = (ordinalIndexState2 > OrdinalIndexState.Indexible);
			this.m_limitsParallelism = ((this.m_prematureMergeLeft && ordinalIndexState != OrdinalIndexState.Shuffled) || (this.m_prematureMergeRight && ordinalIndexState2 != OrdinalIndexState.Shuffled));
		}

		// Token: 0x06000E6B RID: 3691 RVA: 0x0003381C File Offset: 0x00031A1C
		internal override QueryResults<TOutput> Open(QuerySettings settings, bool preferStriping)
		{
			QueryResults<TLeftInput> queryResults = this.m_leftChild.Open(settings, preferStriping);
			QueryResults<TRightInput> queryResults2 = this.m_rightChild.Open(settings, preferStriping);
			int value = settings.DegreeOfParallelism.Value;
			if (this.m_prematureMergeLeft)
			{
				PartitionedStreamMerger<TLeftInput> partitionedStreamMerger = new PartitionedStreamMerger<TLeftInput>(false, ParallelMergeOptions.FullyBuffered, settings.TaskScheduler, this.m_leftChild.OutputOrdered, settings.CancellationState, settings.QueryId);
				queryResults.GivePartitionedStream(partitionedStreamMerger);
				queryResults = new ListQueryResults<TLeftInput>(partitionedStreamMerger.MergeExecutor.GetResultsAsArray(), value, preferStriping);
			}
			if (this.m_prematureMergeRight)
			{
				PartitionedStreamMerger<TRightInput> partitionedStreamMerger2 = new PartitionedStreamMerger<TRightInput>(false, ParallelMergeOptions.FullyBuffered, settings.TaskScheduler, this.m_rightChild.OutputOrdered, settings.CancellationState, settings.QueryId);
				queryResults2.GivePartitionedStream(partitionedStreamMerger2);
				queryResults2 = new ListQueryResults<TRightInput>(partitionedStreamMerger2.MergeExecutor.GetResultsAsArray(), value, preferStriping);
			}
			return new ZipQueryOperator<TLeftInput, TRightInput, TOutput>.ZipQueryOperatorResults(queryResults, queryResults2, this.m_resultSelector, value, preferStriping);
		}

		// Token: 0x06000E6C RID: 3692 RVA: 0x000338FE File Offset: 0x00031AFE
		internal override IEnumerable<TOutput> AsSequentialQuery(CancellationToken token)
		{
			using (IEnumerator<TLeftInput> leftEnumerator = this.m_leftChild.AsSequentialQuery(token).GetEnumerator())
			{
				using (IEnumerator<TRightInput> rightEnumerator = this.m_rightChild.AsSequentialQuery(token).GetEnumerator())
				{
					while (leftEnumerator.MoveNext() && rightEnumerator.MoveNext())
					{
						yield return this.m_resultSelector(leftEnumerator.Current, rightEnumerator.Current);
					}
				}
				IEnumerator<TRightInput> rightEnumerator = null;
			}
			IEnumerator<TLeftInput> leftEnumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x1700028F RID: 655
		// (get) Token: 0x06000E6D RID: 3693 RVA: 0x00033915 File Offset: 0x00031B15
		internal override OrdinalIndexState OrdinalIndexState
		{
			get
			{
				return OrdinalIndexState.Indexible;
			}
		}

		// Token: 0x17000290 RID: 656
		// (get) Token: 0x06000E6E RID: 3694 RVA: 0x00033918 File Offset: 0x00031B18
		internal override bool LimitsParallelism
		{
			get
			{
				return this.m_limitsParallelism;
			}
		}

		// Token: 0x0400087E RID: 2174
		private readonly Func<TLeftInput, TRightInput, TOutput> m_resultSelector;

		// Token: 0x0400087F RID: 2175
		private readonly QueryOperator<TLeftInput> m_leftChild;

		// Token: 0x04000880 RID: 2176
		private readonly QueryOperator<TRightInput> m_rightChild;

		// Token: 0x04000881 RID: 2177
		private readonly bool m_prematureMergeLeft;

		// Token: 0x04000882 RID: 2178
		private readonly bool m_prematureMergeRight;

		// Token: 0x04000883 RID: 2179
		private readonly bool m_limitsParallelism;

		// Token: 0x020003C5 RID: 965
		internal class ZipQueryOperatorResults : QueryResults<TOutput>
		{
			// Token: 0x06001D87 RID: 7559 RVA: 0x00069A4C File Offset: 0x00067C4C
			internal ZipQueryOperatorResults(QueryResults<TLeftInput> leftChildResults, QueryResults<TRightInput> rightChildResults, Func<TLeftInput, TRightInput, TOutput> resultSelector, int partitionCount, bool preferStriping)
			{
				this.m_leftChildResults = leftChildResults;
				this.m_rightChildResults = rightChildResults;
				this.m_resultSelector = resultSelector;
				this.m_partitionCount = partitionCount;
				this.m_preferStriping = preferStriping;
				this.m_count = Math.Min(this.m_leftChildResults.Count, this.m_rightChildResults.Count);
			}

			// Token: 0x17000564 RID: 1380
			// (get) Token: 0x06001D88 RID: 7560 RVA: 0x00069AA5 File Offset: 0x00067CA5
			internal override int ElementsCount
			{
				get
				{
					return this.m_count;
				}
			}

			// Token: 0x17000565 RID: 1381
			// (get) Token: 0x06001D89 RID: 7561 RVA: 0x00069AAD File Offset: 0x00067CAD
			internal override bool IsIndexible
			{
				get
				{
					return true;
				}
			}

			// Token: 0x06001D8A RID: 7562 RVA: 0x00069AB0 File Offset: 0x00067CB0
			internal override TOutput GetElement(int index)
			{
				return this.m_resultSelector(this.m_leftChildResults.GetElement(index), this.m_rightChildResults.GetElement(index));
			}

			// Token: 0x06001D8B RID: 7563 RVA: 0x00069AD8 File Offset: 0x00067CD8
			internal override void GivePartitionedStream(IPartitionedStreamRecipient<TOutput> recipient)
			{
				PartitionedStream<TOutput, int> partitionedStream = ExchangeUtilities.PartitionDataSource<TOutput>(this, this.m_partitionCount, this.m_preferStriping);
				recipient.Receive<int>(partitionedStream);
			}

			// Token: 0x0400116C RID: 4460
			private readonly QueryResults<TLeftInput> m_leftChildResults;

			// Token: 0x0400116D RID: 4461
			private readonly QueryResults<TRightInput> m_rightChildResults;

			// Token: 0x0400116E RID: 4462
			private readonly Func<TLeftInput, TRightInput, TOutput> m_resultSelector;

			// Token: 0x0400116F RID: 4463
			private readonly int m_count;

			// Token: 0x04001170 RID: 4464
			private readonly int m_partitionCount;

			// Token: 0x04001171 RID: 4465
			private readonly bool m_preferStriping;
		}
	}
}
