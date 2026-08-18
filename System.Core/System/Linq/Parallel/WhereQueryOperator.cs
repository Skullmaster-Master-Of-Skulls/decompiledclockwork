using System;
using System.Collections.Generic;
using System.Threading;

namespace System.Linq.Parallel
{
	// Token: 0x020001E8 RID: 488
	internal sealed class WhereQueryOperator<TInputOutput> : UnaryQueryOperator<TInputOutput, TInputOutput>
	{
		// Token: 0x06000FD1 RID: 4049 RVA: 0x00037E98 File Offset: 0x00036098
		internal WhereQueryOperator(IEnumerable<TInputOutput> child, Func<TInputOutput, bool> predicate) : base(child)
		{
			base.SetOrdinalIndexState(base.Child.OrdinalIndexState.Worse(OrdinalIndexState.Increasing));
			this.m_predicate = predicate;
		}

		// Token: 0x06000FD2 RID: 4050 RVA: 0x00037EC0 File Offset: 0x000360C0
		internal override void WrapPartitionedStream<TKey>(PartitionedStream<TInputOutput, TKey> inputStream, IPartitionedStreamRecipient<TInputOutput> recipient, bool preferStriping, QuerySettings settings)
		{
			PartitionedStream<TInputOutput, TKey> partitionedStream = new PartitionedStream<TInputOutput, TKey>(inputStream.PartitionCount, inputStream.KeyComparer, this.OrdinalIndexState);
			for (int i = 0; i < inputStream.PartitionCount; i++)
			{
				partitionedStream[i] = new WhereQueryOperator<TInputOutput>.WhereQueryOperatorEnumerator<TKey>(inputStream[i], this.m_predicate, settings.CancellationState.MergedCancellationToken);
			}
			recipient.Receive<TKey>(partitionedStream);
		}

		// Token: 0x06000FD3 RID: 4051 RVA: 0x00037F24 File Offset: 0x00036124
		internal override QueryResults<TInputOutput> Open(QuerySettings settings, bool preferStriping)
		{
			QueryResults<TInputOutput> childQueryResults = base.Child.Open(settings, preferStriping);
			return new UnaryQueryOperator<TInputOutput, TInputOutput>.UnaryQueryOperatorResults(childQueryResults, this, settings, preferStriping);
		}

		// Token: 0x06000FD4 RID: 4052 RVA: 0x00037F48 File Offset: 0x00036148
		internal override IEnumerable<TInputOutput> AsSequentialQuery(CancellationToken token)
		{
			IEnumerable<TInputOutput> source = CancellableEnumerable.Wrap<TInputOutput>(base.Child.AsSequentialQuery(token), token);
			return source.Where(this.m_predicate);
		}

		// Token: 0x170002C6 RID: 710
		// (get) Token: 0x06000FD5 RID: 4053 RVA: 0x00037F74 File Offset: 0x00036174
		internal override bool LimitsParallelism
		{
			get
			{
				return false;
			}
		}

		// Token: 0x040008FD RID: 2301
		private Func<TInputOutput, bool> m_predicate;

		// Token: 0x0200040C RID: 1036
		private class WhereQueryOperatorEnumerator<TKey> : QueryOperatorEnumerator<TInputOutput, TKey>
		{
			// Token: 0x06001E68 RID: 7784 RVA: 0x0006D2BD File Offset: 0x0006B4BD
			internal WhereQueryOperatorEnumerator(QueryOperatorEnumerator<TInputOutput, TKey> source, Func<TInputOutput, bool> predicate, CancellationToken cancellationToken)
			{
				this.m_source = source;
				this.m_predicate = predicate;
				this.m_cancellationToken = cancellationToken;
			}

			// Token: 0x06001E69 RID: 7785 RVA: 0x0006D2DC File Offset: 0x0006B4DC
			internal override bool MoveNext(ref TInputOutput currentElement, ref TKey currentKey)
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
					if (this.m_predicate(currentElement))
					{
						return true;
					}
				}
				return false;
			}

			// Token: 0x06001E6A RID: 7786 RVA: 0x0006D34A File Offset: 0x0006B54A
			protected override void Dispose(bool disposing)
			{
				this.m_source.Dispose();
			}

			// Token: 0x0400123B RID: 4667
			private readonly QueryOperatorEnumerator<TInputOutput, TKey> m_source;

			// Token: 0x0400123C RID: 4668
			private readonly Func<TInputOutput, bool> m_predicate;

			// Token: 0x0400123D RID: 4669
			private CancellationToken m_cancellationToken;

			// Token: 0x0400123E RID: 4670
			private Shared<int> m_outputLoopCount;
		}
	}
}
