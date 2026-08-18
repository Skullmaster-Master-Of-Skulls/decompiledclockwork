using System;
using System.Collections.Generic;
using System.Threading;

namespace System.Linq.Parallel
{
	// Token: 0x020001E1 RID: 481
	internal sealed class SelectQueryOperator<TInput, TOutput> : UnaryQueryOperator<TInput, TOutput>
	{
		// Token: 0x06000FAA RID: 4010 RVA: 0x0003774A File Offset: 0x0003594A
		internal SelectQueryOperator(IEnumerable<TInput> child, Func<TInput, TOutput> selector) : base(child)
		{
			this.m_selector = selector;
			base.SetOrdinalIndexState(base.Child.OrdinalIndexState);
		}

		// Token: 0x06000FAB RID: 4011 RVA: 0x0003776C File Offset: 0x0003596C
		internal override void WrapPartitionedStream<TKey>(PartitionedStream<TInput, TKey> inputStream, IPartitionedStreamRecipient<TOutput> recipient, bool preferStriping, QuerySettings settings)
		{
			PartitionedStream<TOutput, TKey> partitionedStream = new PartitionedStream<TOutput, TKey>(inputStream.PartitionCount, inputStream.KeyComparer, this.OrdinalIndexState);
			for (int i = 0; i < inputStream.PartitionCount; i++)
			{
				partitionedStream[i] = new SelectQueryOperator<TInput, TOutput>.SelectQueryOperatorEnumerator<TKey>(inputStream[i], this.m_selector);
			}
			recipient.Receive<TKey>(partitionedStream);
		}

		// Token: 0x06000FAC RID: 4012 RVA: 0x000377C4 File Offset: 0x000359C4
		internal override QueryResults<TOutput> Open(QuerySettings settings, bool preferStriping)
		{
			QueryResults<TInput> childQueryResults = base.Child.Open(settings, preferStriping);
			return SelectQueryOperator<TInput, TOutput>.SelectQueryOperatorResults.NewResults(childQueryResults, this, settings, preferStriping);
		}

		// Token: 0x06000FAD RID: 4013 RVA: 0x000377E8 File Offset: 0x000359E8
		internal override IEnumerable<TOutput> AsSequentialQuery(CancellationToken token)
		{
			return base.Child.AsSequentialQuery(token).Select(this.m_selector);
		}

		// Token: 0x170002BD RID: 701
		// (get) Token: 0x06000FAE RID: 4014 RVA: 0x00037801 File Offset: 0x00035A01
		internal override bool LimitsParallelism
		{
			get
			{
				return false;
			}
		}

		// Token: 0x040008EA RID: 2282
		private Func<TInput, TOutput> m_selector;

		// Token: 0x02000403 RID: 1027
		private class SelectQueryOperatorEnumerator<TKey> : QueryOperatorEnumerator<TOutput, TKey>
		{
			// Token: 0x06001E4D RID: 7757 RVA: 0x0006C863 File Offset: 0x0006AA63
			internal SelectQueryOperatorEnumerator(QueryOperatorEnumerator<TInput, TKey> source, Func<TInput, TOutput> selector)
			{
				this.m_source = source;
				this.m_selector = selector;
			}

			// Token: 0x06001E4E RID: 7758 RVA: 0x0006C87C File Offset: 0x0006AA7C
			internal override bool MoveNext(ref TOutput currentElement, ref TKey currentKey)
			{
				TInput arg = default(TInput);
				if (this.m_source.MoveNext(ref arg, ref currentKey))
				{
					currentElement = this.m_selector(arg);
					return true;
				}
				return false;
			}

			// Token: 0x06001E4F RID: 7759 RVA: 0x0006C8B6 File Offset: 0x0006AAB6
			protected override void Dispose(bool disposing)
			{
				this.m_source.Dispose();
			}

			// Token: 0x04001214 RID: 4628
			private readonly QueryOperatorEnumerator<TInput, TKey> m_source;

			// Token: 0x04001215 RID: 4629
			private readonly Func<TInput, TOutput> m_selector;
		}

		// Token: 0x02000404 RID: 1028
		private class SelectQueryOperatorResults : UnaryQueryOperator<TInput, TOutput>.UnaryQueryOperatorResults
		{
			// Token: 0x06001E50 RID: 7760 RVA: 0x0006C8C3 File Offset: 0x0006AAC3
			public static QueryResults<TOutput> NewResults(QueryResults<TInput> childQueryResults, SelectQueryOperator<TInput, TOutput> op, QuerySettings settings, bool preferStriping)
			{
				if (childQueryResults.IsIndexible)
				{
					return new SelectQueryOperator<TInput, TOutput>.SelectQueryOperatorResults(childQueryResults, op, settings, preferStriping);
				}
				return new UnaryQueryOperator<TInput, TOutput>.UnaryQueryOperatorResults(childQueryResults, op, settings, preferStriping);
			}

			// Token: 0x06001E51 RID: 7761 RVA: 0x0006C8E0 File Offset: 0x0006AAE0
			private SelectQueryOperatorResults(QueryResults<TInput> childQueryResults, SelectQueryOperator<TInput, TOutput> op, QuerySettings settings, bool preferStriping) : base(childQueryResults, op, settings, preferStriping)
			{
				this.m_selector = op.m_selector;
				this.m_childCount = this.m_childQueryResults.ElementsCount;
			}

			// Token: 0x17000574 RID: 1396
			// (get) Token: 0x06001E52 RID: 7762 RVA: 0x0006C90A File Offset: 0x0006AB0A
			internal override bool IsIndexible
			{
				get
				{
					return true;
				}
			}

			// Token: 0x17000575 RID: 1397
			// (get) Token: 0x06001E53 RID: 7763 RVA: 0x0006C90D File Offset: 0x0006AB0D
			internal override int ElementsCount
			{
				get
				{
					return this.m_childCount;
				}
			}

			// Token: 0x06001E54 RID: 7764 RVA: 0x0006C915 File Offset: 0x0006AB15
			internal override TOutput GetElement(int index)
			{
				return this.m_selector(this.m_childQueryResults.GetElement(index));
			}

			// Token: 0x04001216 RID: 4630
			private Func<TInput, TOutput> m_selector;

			// Token: 0x04001217 RID: 4631
			private int m_childCount;
		}
	}
}
