using System;
using System.Collections.Generic;
using System.Threading;

namespace System.Linq.Parallel
{
	// Token: 0x020001DC RID: 476
	internal sealed class IndexedSelectQueryOperator<TInput, TOutput> : UnaryQueryOperator<TInput, TOutput>
	{
		// Token: 0x06000F8B RID: 3979 RVA: 0x00037013 File Offset: 0x00035213
		internal IndexedSelectQueryOperator(IEnumerable<TInput> child, Func<TInput, int, TOutput> selector) : base(child)
		{
			this.m_selector = selector;
			this.m_outputOrdered = true;
			this.InitOrdinalIndexState();
		}

		// Token: 0x06000F8C RID: 3980 RVA: 0x00037030 File Offset: 0x00035230
		private void InitOrdinalIndexState()
		{
			OrdinalIndexState ordinalIndexState = base.Child.OrdinalIndexState;
			OrdinalIndexState ordinalIndexState2 = ordinalIndexState;
			if (ordinalIndexState.IsWorseThan(OrdinalIndexState.Correct))
			{
				this.m_prematureMerge = true;
				this.m_limitsParallelism = (ordinalIndexState != OrdinalIndexState.Shuffled);
				ordinalIndexState2 = OrdinalIndexState.Correct;
			}
			base.SetOrdinalIndexState(ordinalIndexState2);
		}

		// Token: 0x06000F8D RID: 3981 RVA: 0x00037074 File Offset: 0x00035274
		internal override QueryResults<TOutput> Open(QuerySettings settings, bool preferStriping)
		{
			QueryResults<TInput> childQueryResults = base.Child.Open(settings, preferStriping);
			return IndexedSelectQueryOperator<TInput, TOutput>.IndexedSelectQueryOperatorResults.NewResults(childQueryResults, this, settings, preferStriping);
		}

		// Token: 0x06000F8E RID: 3982 RVA: 0x00037098 File Offset: 0x00035298
		internal override void WrapPartitionedStream<TKey>(PartitionedStream<TInput, TKey> inputStream, IPartitionedStreamRecipient<TOutput> recipient, bool preferStriping, QuerySettings settings)
		{
			int partitionCount = inputStream.PartitionCount;
			PartitionedStream<TInput, int> partitionedStream;
			if (this.m_prematureMerge)
			{
				ListQueryResults<TInput> listQueryResults = QueryOperator<TInput>.ExecuteAndCollectResults<TKey>(inputStream, partitionCount, base.Child.OutputOrdered, preferStriping, settings);
				partitionedStream = listQueryResults.GetPartitionedStream();
			}
			else
			{
				partitionedStream = (PartitionedStream<TInput, int>)inputStream;
			}
			PartitionedStream<TOutput, int> partitionedStream2 = new PartitionedStream<TOutput, int>(partitionCount, Util.GetDefaultComparer<int>(), this.OrdinalIndexState);
			for (int i = 0; i < partitionCount; i++)
			{
				partitionedStream2[i] = new IndexedSelectQueryOperator<TInput, TOutput>.IndexedSelectQueryOperatorEnumerator(partitionedStream[i], this.m_selector);
			}
			recipient.Receive<int>(partitionedStream2);
		}

		// Token: 0x170002B8 RID: 696
		// (get) Token: 0x06000F8F RID: 3983 RVA: 0x0003711E File Offset: 0x0003531E
		internal override bool LimitsParallelism
		{
			get
			{
				return this.m_limitsParallelism;
			}
		}

		// Token: 0x06000F90 RID: 3984 RVA: 0x00037126 File Offset: 0x00035326
		internal override IEnumerable<TOutput> AsSequentialQuery(CancellationToken token)
		{
			return base.Child.AsSequentialQuery(token).Select(this.m_selector);
		}

		// Token: 0x040008DD RID: 2269
		private readonly Func<TInput, int, TOutput> m_selector;

		// Token: 0x040008DE RID: 2270
		private bool m_prematureMerge;

		// Token: 0x040008DF RID: 2271
		private bool m_limitsParallelism;

		// Token: 0x020003FA RID: 1018
		private class IndexedSelectQueryOperatorEnumerator : QueryOperatorEnumerator<TOutput, int>
		{
			// Token: 0x06001E30 RID: 7728 RVA: 0x0006C078 File Offset: 0x0006A278
			internal IndexedSelectQueryOperatorEnumerator(QueryOperatorEnumerator<TInput, int> source, Func<TInput, int, TOutput> selector)
			{
				this.m_source = source;
				this.m_selector = selector;
			}

			// Token: 0x06001E31 RID: 7729 RVA: 0x0006C090 File Offset: 0x0006A290
			internal override bool MoveNext(ref TOutput currentElement, ref int currentKey)
			{
				TInput arg = default(TInput);
				if (this.m_source.MoveNext(ref arg, ref currentKey))
				{
					currentElement = this.m_selector(arg, currentKey);
					return true;
				}
				return false;
			}

			// Token: 0x06001E32 RID: 7730 RVA: 0x0006C0CC File Offset: 0x0006A2CC
			protected override void Dispose(bool disposing)
			{
				this.m_source.Dispose();
			}

			// Token: 0x040011F1 RID: 4593
			private readonly QueryOperatorEnumerator<TInput, int> m_source;

			// Token: 0x040011F2 RID: 4594
			private readonly Func<TInput, int, TOutput> m_selector;
		}

		// Token: 0x020003FB RID: 1019
		private class IndexedSelectQueryOperatorResults : UnaryQueryOperator<TInput, TOutput>.UnaryQueryOperatorResults
		{
			// Token: 0x06001E33 RID: 7731 RVA: 0x0006C0D9 File Offset: 0x0006A2D9
			public static QueryResults<TOutput> NewResults(QueryResults<TInput> childQueryResults, IndexedSelectQueryOperator<TInput, TOutput> op, QuerySettings settings, bool preferStriping)
			{
				if (childQueryResults.IsIndexible)
				{
					return new IndexedSelectQueryOperator<TInput, TOutput>.IndexedSelectQueryOperatorResults(childQueryResults, op, settings, preferStriping);
				}
				return new UnaryQueryOperator<TInput, TOutput>.UnaryQueryOperatorResults(childQueryResults, op, settings, preferStriping);
			}

			// Token: 0x06001E34 RID: 7732 RVA: 0x0006C0F6 File Offset: 0x0006A2F6
			private IndexedSelectQueryOperatorResults(QueryResults<TInput> childQueryResults, IndexedSelectQueryOperator<TInput, TOutput> op, QuerySettings settings, bool preferStriping) : base(childQueryResults, op, settings, preferStriping)
			{
				this.m_selectOp = op;
				this.m_childCount = this.m_childQueryResults.ElementsCount;
			}

			// Token: 0x17000570 RID: 1392
			// (get) Token: 0x06001E35 RID: 7733 RVA: 0x0006C11B File Offset: 0x0006A31B
			internal override int ElementsCount
			{
				get
				{
					return this.m_childQueryResults.ElementsCount;
				}
			}

			// Token: 0x17000571 RID: 1393
			// (get) Token: 0x06001E36 RID: 7734 RVA: 0x0006C128 File Offset: 0x0006A328
			internal override bool IsIndexible
			{
				get
				{
					return true;
				}
			}

			// Token: 0x06001E37 RID: 7735 RVA: 0x0006C12B File Offset: 0x0006A32B
			internal override TOutput GetElement(int index)
			{
				return this.m_selectOp.m_selector(this.m_childQueryResults.GetElement(index), index);
			}

			// Token: 0x040011F3 RID: 4595
			private IndexedSelectQueryOperator<TInput, TOutput> m_selectOp;

			// Token: 0x040011F4 RID: 4596
			private int m_childCount;
		}
	}
}
