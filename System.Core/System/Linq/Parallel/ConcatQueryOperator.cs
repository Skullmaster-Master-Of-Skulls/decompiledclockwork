using System;
using System.Collections.Generic;
using System.Threading;

namespace System.Linq.Parallel
{
	// Token: 0x020001CB RID: 459
	internal sealed class ConcatQueryOperator<TSource> : BinaryQueryOperator<TSource, TSource, TSource>
	{
		// Token: 0x06000F3C RID: 3900 RVA: 0x00035E14 File Offset: 0x00034014
		internal ConcatQueryOperator(ParallelQuery<TSource> firstChild, ParallelQuery<TSource> secondChild) : base(firstChild, secondChild)
		{
			this.m_outputOrdered = (base.LeftChild.OutputOrdered || base.RightChild.OutputOrdered);
			this.m_prematureMergeLeft = base.LeftChild.OrdinalIndexState.IsWorseThan(OrdinalIndexState.Increasing);
			this.m_prematureMergeRight = base.RightChild.OrdinalIndexState.IsWorseThan(OrdinalIndexState.Increasing);
			if (base.LeftChild.OrdinalIndexState == OrdinalIndexState.Indexible && base.RightChild.OrdinalIndexState == OrdinalIndexState.Indexible)
			{
				base.SetOrdinalIndex(OrdinalIndexState.Indexible);
				return;
			}
			base.SetOrdinalIndex(OrdinalIndexState.Increasing.Worse(base.LeftChild.OrdinalIndexState.Worse(base.RightChild.OrdinalIndexState)));
		}

		// Token: 0x06000F3D RID: 3901 RVA: 0x00035EC4 File Offset: 0x000340C4
		internal override QueryResults<TSource> Open(QuerySettings settings, bool preferStriping)
		{
			QueryResults<TSource> leftChildQueryResults = base.LeftChild.Open(settings, preferStriping);
			QueryResults<TSource> rightChildQueryResults = base.RightChild.Open(settings, preferStriping);
			return ConcatQueryOperator<TSource>.ConcatQueryOperatorResults.NewResults(leftChildQueryResults, rightChildQueryResults, this, settings, preferStriping);
		}

		// Token: 0x06000F3E RID: 3902 RVA: 0x00035EF8 File Offset: 0x000340F8
		public override void WrapPartitionedStream<TLeftKey, TRightKey>(PartitionedStream<TSource, TLeftKey> leftStream, PartitionedStream<TSource, TRightKey> rightStream, IPartitionedStreamRecipient<TSource> outputRecipient, bool preferStriping, QuerySettings settings)
		{
			if (this.m_prematureMergeLeft)
			{
				ListQueryResults<TSource> listQueryResults = QueryOperator<TSource>.ExecuteAndCollectResults<TLeftKey>(leftStream, leftStream.PartitionCount, base.LeftChild.OutputOrdered, preferStriping, settings);
				PartitionedStream<TSource, int> partitionedStream = listQueryResults.GetPartitionedStream();
				this.WrapHelper<int, TRightKey>(partitionedStream, rightStream, outputRecipient, settings, preferStriping);
				return;
			}
			this.WrapHelper<TLeftKey, TRightKey>(leftStream, rightStream, outputRecipient, settings, preferStriping);
		}

		// Token: 0x06000F3F RID: 3903 RVA: 0x00035F4C File Offset: 0x0003414C
		private void WrapHelper<TLeftKey, TRightKey>(PartitionedStream<TSource, TLeftKey> leftStreamInc, PartitionedStream<TSource, TRightKey> rightStream, IPartitionedStreamRecipient<TSource> outputRecipient, QuerySettings settings, bool preferStriping)
		{
			if (this.m_prematureMergeRight)
			{
				ListQueryResults<TSource> listQueryResults = QueryOperator<TSource>.ExecuteAndCollectResults<TRightKey>(rightStream, leftStreamInc.PartitionCount, base.LeftChild.OutputOrdered, preferStriping, settings);
				PartitionedStream<TSource, int> partitionedStream = listQueryResults.GetPartitionedStream();
				this.WrapHelper2<TLeftKey, int>(leftStreamInc, partitionedStream, outputRecipient);
				return;
			}
			this.WrapHelper2<TLeftKey, TRightKey>(leftStreamInc, rightStream, outputRecipient);
		}

		// Token: 0x06000F40 RID: 3904 RVA: 0x00035F98 File Offset: 0x00034198
		private void WrapHelper2<TLeftKey, TRightKey>(PartitionedStream<TSource, TLeftKey> leftStreamInc, PartitionedStream<TSource, TRightKey> rightStreamInc, IPartitionedStreamRecipient<TSource> outputRecipient)
		{
			int partitionCount = leftStreamInc.PartitionCount;
			IComparer<ConcatKey<TLeftKey, TRightKey>> keyComparer = ConcatKey<TLeftKey, TRightKey>.MakeComparer(leftStreamInc.KeyComparer, rightStreamInc.KeyComparer);
			PartitionedStream<TSource, ConcatKey<TLeftKey, TRightKey>> partitionedStream = new PartitionedStream<TSource, ConcatKey<TLeftKey, TRightKey>>(partitionCount, keyComparer, this.OrdinalIndexState);
			for (int i = 0; i < partitionCount; i++)
			{
				partitionedStream[i] = new ConcatQueryOperator<TSource>.ConcatQueryOperatorEnumerator<TLeftKey, TRightKey>(leftStreamInc[i], rightStreamInc[i]);
			}
			outputRecipient.Receive<ConcatKey<TLeftKey, TRightKey>>(partitionedStream);
		}

		// Token: 0x06000F41 RID: 3905 RVA: 0x00035FF9 File Offset: 0x000341F9
		internal override IEnumerable<TSource> AsSequentialQuery(CancellationToken token)
		{
			return base.LeftChild.AsSequentialQuery(token).Concat(base.RightChild.AsSequentialQuery(token));
		}

		// Token: 0x170002AE RID: 686
		// (get) Token: 0x06000F42 RID: 3906 RVA: 0x00036018 File Offset: 0x00034218
		internal override bool LimitsParallelism
		{
			get
			{
				return false;
			}
		}

		// Token: 0x040008BA RID: 2234
		private readonly bool m_prematureMergeLeft;

		// Token: 0x040008BB RID: 2235
		private readonly bool m_prematureMergeRight;

		// Token: 0x020003EB RID: 1003
		private class ConcatQueryOperatorEnumerator<TLeftKey, TRightKey> : QueryOperatorEnumerator<TSource, ConcatKey<TLeftKey, TRightKey>>
		{
			// Token: 0x06001E07 RID: 7687 RVA: 0x0006B741 File Offset: 0x00069941
			internal ConcatQueryOperatorEnumerator(QueryOperatorEnumerator<TSource, TLeftKey> firstSource, QueryOperatorEnumerator<TSource, TRightKey> secondSource)
			{
				this.m_firstSource = firstSource;
				this.m_secondSource = secondSource;
			}

			// Token: 0x06001E08 RID: 7688 RVA: 0x0006B758 File Offset: 0x00069958
			internal override bool MoveNext(ref TSource currentElement, ref ConcatKey<TLeftKey, TRightKey> currentKey)
			{
				if (!this.m_begunSecond)
				{
					TLeftKey leftKey = default(TLeftKey);
					if (this.m_firstSource.MoveNext(ref currentElement, ref leftKey))
					{
						currentKey = ConcatKey<TLeftKey, TRightKey>.MakeLeft(leftKey);
						return true;
					}
					this.m_begunSecond = true;
				}
				TRightKey rightKey = default(TRightKey);
				if (this.m_secondSource.MoveNext(ref currentElement, ref rightKey))
				{
					currentKey = ConcatKey<TLeftKey, TRightKey>.MakeRight(rightKey);
					return true;
				}
				return false;
			}

			// Token: 0x06001E09 RID: 7689 RVA: 0x0006B7C1 File Offset: 0x000699C1
			protected override void Dispose(bool disposing)
			{
				this.m_firstSource.Dispose();
				this.m_secondSource.Dispose();
			}

			// Token: 0x040011B5 RID: 4533
			private QueryOperatorEnumerator<TSource, TLeftKey> m_firstSource;

			// Token: 0x040011B6 RID: 4534
			private QueryOperatorEnumerator<TSource, TRightKey> m_secondSource;

			// Token: 0x040011B7 RID: 4535
			private bool m_begunSecond;
		}

		// Token: 0x020003EC RID: 1004
		private class ConcatQueryOperatorResults : BinaryQueryOperator<TSource, TSource, TSource>.BinaryQueryOperatorResults
		{
			// Token: 0x06001E0A RID: 7690 RVA: 0x0006B7D9 File Offset: 0x000699D9
			public static QueryResults<TSource> NewResults(QueryResults<TSource> leftChildQueryResults, QueryResults<TSource> rightChildQueryResults, ConcatQueryOperator<TSource> op, QuerySettings settings, bool preferStriping)
			{
				if (leftChildQueryResults.IsIndexible && rightChildQueryResults.IsIndexible)
				{
					return new ConcatQueryOperator<TSource>.ConcatQueryOperatorResults(leftChildQueryResults, rightChildQueryResults, op, settings, preferStriping);
				}
				return new BinaryQueryOperator<TSource, TSource, TSource>.BinaryQueryOperatorResults(leftChildQueryResults, rightChildQueryResults, op, settings, preferStriping);
			}

			// Token: 0x06001E0B RID: 7691 RVA: 0x0006B802 File Offset: 0x00069A02
			private ConcatQueryOperatorResults(QueryResults<TSource> leftChildQueryResults, QueryResults<TSource> rightChildQueryResults, ConcatQueryOperator<TSource> concatOp, QuerySettings settings, bool preferStriping) : base(leftChildQueryResults, rightChildQueryResults, concatOp, settings, preferStriping)
			{
				this.m_concatOp = concatOp;
				this.m_leftChildCount = leftChildQueryResults.ElementsCount;
				this.m_rightChildCount = rightChildQueryResults.ElementsCount;
			}

			// Token: 0x1700056C RID: 1388
			// (get) Token: 0x06001E0C RID: 7692 RVA: 0x0006B830 File Offset: 0x00069A30
			internal override bool IsIndexible
			{
				get
				{
					return true;
				}
			}

			// Token: 0x1700056D RID: 1389
			// (get) Token: 0x06001E0D RID: 7693 RVA: 0x0006B833 File Offset: 0x00069A33
			internal override int ElementsCount
			{
				get
				{
					return this.m_leftChildCount + this.m_rightChildCount;
				}
			}

			// Token: 0x06001E0E RID: 7694 RVA: 0x0006B842 File Offset: 0x00069A42
			internal override TSource GetElement(int index)
			{
				if (index < this.m_leftChildCount)
				{
					return this.m_leftChildQueryResults.GetElement(index);
				}
				return this.m_rightChildQueryResults.GetElement(index - this.m_leftChildCount);
			}

			// Token: 0x040011B8 RID: 4536
			private ConcatQueryOperator<TSource> m_concatOp;

			// Token: 0x040011B9 RID: 4537
			private int m_leftChildCount;

			// Token: 0x040011BA RID: 4538
			private int m_rightChildCount;
		}
	}
}
