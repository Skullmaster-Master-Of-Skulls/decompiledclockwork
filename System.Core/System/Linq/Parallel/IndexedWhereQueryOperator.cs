using System;
using System.Collections.Generic;
using System.Threading;

namespace System.Linq.Parallel
{
	// Token: 0x020001DD RID: 477
	internal sealed class IndexedWhereQueryOperator<TInputOutput> : UnaryQueryOperator<TInputOutput, TInputOutput>
	{
		// Token: 0x06000F91 RID: 3985 RVA: 0x0003713F File Offset: 0x0003533F
		internal IndexedWhereQueryOperator(IEnumerable<TInputOutput> child, Func<TInputOutput, int, bool> predicate) : base(child)
		{
			this.m_predicate = predicate;
			this.m_outputOrdered = true;
			this.InitOrdinalIndexState();
		}

		// Token: 0x06000F92 RID: 3986 RVA: 0x0003715C File Offset: 0x0003535C
		private void InitOrdinalIndexState()
		{
			OrdinalIndexState ordinalIndexState = base.Child.OrdinalIndexState;
			if (ordinalIndexState.IsWorseThan(OrdinalIndexState.Correct))
			{
				this.m_prematureMerge = true;
				this.m_limitsParallelism = (ordinalIndexState != OrdinalIndexState.Shuffled);
			}
			base.SetOrdinalIndexState(OrdinalIndexState.Increasing);
		}

		// Token: 0x06000F93 RID: 3987 RVA: 0x0003719C File Offset: 0x0003539C
		internal override QueryResults<TInputOutput> Open(QuerySettings settings, bool preferStriping)
		{
			QueryResults<TInputOutput> childQueryResults = base.Child.Open(settings, preferStriping);
			return new UnaryQueryOperator<TInputOutput, TInputOutput>.UnaryQueryOperatorResults(childQueryResults, this, settings, preferStriping);
		}

		// Token: 0x06000F94 RID: 3988 RVA: 0x000371C0 File Offset: 0x000353C0
		internal override void WrapPartitionedStream<TKey>(PartitionedStream<TInputOutput, TKey> inputStream, IPartitionedStreamRecipient<TInputOutput> recipient, bool preferStriping, QuerySettings settings)
		{
			int partitionCount = inputStream.PartitionCount;
			PartitionedStream<TInputOutput, int> partitionedStream;
			if (this.m_prematureMerge)
			{
				ListQueryResults<TInputOutput> listQueryResults = QueryOperator<TInputOutput>.ExecuteAndCollectResults<TKey>(inputStream, partitionCount, base.Child.OutputOrdered, preferStriping, settings);
				partitionedStream = listQueryResults.GetPartitionedStream();
			}
			else
			{
				partitionedStream = (PartitionedStream<TInputOutput, int>)inputStream;
			}
			PartitionedStream<TInputOutput, int> partitionedStream2 = new PartitionedStream<TInputOutput, int>(partitionCount, Util.GetDefaultComparer<int>(), this.OrdinalIndexState);
			for (int i = 0; i < partitionCount; i++)
			{
				partitionedStream2[i] = new IndexedWhereQueryOperator<TInputOutput>.IndexedWhereQueryOperatorEnumerator(partitionedStream[i], this.m_predicate, settings.CancellationState.MergedCancellationToken);
			}
			recipient.Receive<int>(partitionedStream2);
		}

		// Token: 0x06000F95 RID: 3989 RVA: 0x00037254 File Offset: 0x00035454
		internal override IEnumerable<TInputOutput> AsSequentialQuery(CancellationToken token)
		{
			IEnumerable<TInputOutput> source = CancellableEnumerable.Wrap<TInputOutput>(base.Child.AsSequentialQuery(token), token);
			return source.Where(this.m_predicate);
		}

		// Token: 0x170002B9 RID: 697
		// (get) Token: 0x06000F96 RID: 3990 RVA: 0x00037280 File Offset: 0x00035480
		internal override bool LimitsParallelism
		{
			get
			{
				return this.m_limitsParallelism;
			}
		}

		// Token: 0x040008E0 RID: 2272
		private Func<TInputOutput, int, bool> m_predicate;

		// Token: 0x040008E1 RID: 2273
		private bool m_prematureMerge;

		// Token: 0x040008E2 RID: 2274
		private bool m_limitsParallelism;

		// Token: 0x020003FC RID: 1020
		private class IndexedWhereQueryOperatorEnumerator : QueryOperatorEnumerator<TInputOutput, int>
		{
			// Token: 0x06001E38 RID: 7736 RVA: 0x0006C14A File Offset: 0x0006A34A
			internal IndexedWhereQueryOperatorEnumerator(QueryOperatorEnumerator<TInputOutput, int> source, Func<TInputOutput, int, bool> predicate, CancellationToken cancellationToken)
			{
				this.m_source = source;
				this.m_predicate = predicate;
				this.m_cancellationToken = cancellationToken;
			}

			// Token: 0x06001E39 RID: 7737 RVA: 0x0006C168 File Offset: 0x0006A368
			internal override bool MoveNext(ref TInputOutput currentElement, ref int currentKey)
			{
				if (this.m_outputLoopCount == null)
				{
					this.m_outputLoopCount = new Shared<int>(0);
				}
				while (this.m_source.MoveNext(ref currentElement, ref currentKey))
				{
					Shared<int> outputLoopCount = this.m_outputLoopCount;
					int value = outputLoopCount.Value;
					outputLoopCount.Value = value + 1;
					if ((value & 63) == 0)
					{
						CancellationState.ThrowIfCanceled(this.m_cancellationToken);
					}
					if (this.m_predicate(currentElement, currentKey))
					{
						return true;
					}
				}
				return false;
			}

			// Token: 0x06001E3A RID: 7738 RVA: 0x0006C1D8 File Offset: 0x0006A3D8
			protected override void Dispose(bool disposing)
			{
				this.m_source.Dispose();
			}

			// Token: 0x040011F5 RID: 4597
			private readonly QueryOperatorEnumerator<TInputOutput, int> m_source;

			// Token: 0x040011F6 RID: 4598
			private readonly Func<TInputOutput, int, bool> m_predicate;

			// Token: 0x040011F7 RID: 4599
			private CancellationToken m_cancellationToken;

			// Token: 0x040011F8 RID: 4600
			private Shared<int> m_outputLoopCount;
		}
	}
}
