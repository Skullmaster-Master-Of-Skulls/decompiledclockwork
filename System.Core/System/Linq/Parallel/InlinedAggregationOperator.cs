using System;
using System.Collections.Generic;
using System.Threading;

namespace System.Linq.Parallel
{
	// Token: 0x020001AB RID: 427
	internal abstract class InlinedAggregationOperator<TSource, TIntermediate, TResult> : UnaryQueryOperator<TSource, TIntermediate>
	{
		// Token: 0x06000EA1 RID: 3745 RVA: 0x000342DF File Offset: 0x000324DF
		internal InlinedAggregationOperator(IEnumerable<TSource> child) : base(child)
		{
		}

		// Token: 0x06000EA2 RID: 3746 RVA: 0x000342E8 File Offset: 0x000324E8
		internal TResult Aggregate()
		{
			Exception ex = null;
			TResult result;
			try
			{
				result = this.InternalAggregate(ref ex);
			}
			catch (ThreadAbortException)
			{
				throw;
			}
			catch (Exception ex2)
			{
				if (ex2 is AggregateException)
				{
					throw;
				}
				OperationCanceledException ex3 = ex2 as OperationCanceledException;
				if (ex3 != null && ex3.CancellationToken == base.SpecifiedQuerySettings.CancellationState.ExternalCancellationToken && base.SpecifiedQuerySettings.CancellationState.ExternalCancellationToken.IsCancellationRequested)
				{
					throw;
				}
				throw new AggregateException(new Exception[]
				{
					ex2
				});
			}
			if (ex != null)
			{
				throw ex;
			}
			return result;
		}

		// Token: 0x06000EA3 RID: 3747
		protected abstract TResult InternalAggregate(ref Exception singularExceptionToThrow);

		// Token: 0x06000EA4 RID: 3748 RVA: 0x0003438C File Offset: 0x0003258C
		internal override QueryResults<TIntermediate> Open(QuerySettings settings, bool preferStriping)
		{
			QueryResults<TSource> childQueryResults = base.Child.Open(settings, preferStriping);
			return new UnaryQueryOperator<TSource, TIntermediate>.UnaryQueryOperatorResults(childQueryResults, this, settings, preferStriping);
		}

		// Token: 0x06000EA5 RID: 3749 RVA: 0x000343B0 File Offset: 0x000325B0
		internal override void WrapPartitionedStream<TKey>(PartitionedStream<TSource, TKey> inputStream, IPartitionedStreamRecipient<TIntermediate> recipient, bool preferStriping, QuerySettings settings)
		{
			int partitionCount = inputStream.PartitionCount;
			PartitionedStream<TIntermediate, int> partitionedStream = new PartitionedStream<TIntermediate, int>(partitionCount, Util.GetDefaultComparer<int>(), OrdinalIndexState.Correct);
			for (int i = 0; i < partitionCount; i++)
			{
				partitionedStream[i] = this.CreateEnumerator<TKey>(i, partitionCount, inputStream[i], null, settings.CancellationState.MergedCancellationToken);
			}
			recipient.Receive<int>(partitionedStream);
		}

		// Token: 0x06000EA6 RID: 3750
		protected abstract QueryOperatorEnumerator<TIntermediate, int> CreateEnumerator<TKey>(int index, int count, QueryOperatorEnumerator<TSource, TKey> source, object sharedData, CancellationToken cancellationToken);

		// Token: 0x06000EA7 RID: 3751 RVA: 0x00034407 File Offset: 0x00032607
		internal override IEnumerable<TIntermediate> AsSequentialQuery(CancellationToken token)
		{
			throw new NotSupportedException();
		}

		// Token: 0x17000297 RID: 663
		// (get) Token: 0x06000EA8 RID: 3752 RVA: 0x0003440E File Offset: 0x0003260E
		internal override bool LimitsParallelism
		{
			get
			{
				return false;
			}
		}
	}
}
