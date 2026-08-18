using System;
using System.Collections.Generic;
using System.Threading;

namespace System.Linq.Parallel
{
	// Token: 0x0200019A RID: 410
	internal sealed class JoinQueryOperator<TLeftInput, TRightInput, TKey, TOutput> : BinaryQueryOperator<TLeftInput, TRightInput, TOutput>
	{
		// Token: 0x06000E5C RID: 3676 RVA: 0x0003331C File Offset: 0x0003151C
		internal JoinQueryOperator(ParallelQuery<TLeftInput> left, ParallelQuery<TRightInput> right, Func<TLeftInput, TKey> leftKeySelector, Func<TRightInput, TKey> rightKeySelector, Func<TLeftInput, TRightInput, TOutput> resultSelector, IEqualityComparer<TKey> keyComparer) : base(left, right)
		{
			this.m_leftKeySelector = leftKeySelector;
			this.m_rightKeySelector = rightKeySelector;
			this.m_resultSelector = resultSelector;
			this.m_keyComparer = keyComparer;
			this.m_outputOrdered = base.LeftChild.OutputOrdered;
			base.SetOrdinalIndex(OrdinalIndexState.Shuffled);
		}

		// Token: 0x06000E5D RID: 3677 RVA: 0x00033368 File Offset: 0x00031568
		public override void WrapPartitionedStream<TLeftKey, TRightKey>(PartitionedStream<TLeftInput, TLeftKey> leftStream, PartitionedStream<TRightInput, TRightKey> rightStream, IPartitionedStreamRecipient<TOutput> outputRecipient, bool preferStriping, QuerySettings settings)
		{
			if (base.LeftChild.OutputOrdered)
			{
				this.WrapPartitionedStreamHelper<TLeftKey, TRightKey>(ExchangeUtilities.HashRepartitionOrdered<TLeftInput, TKey, TLeftKey>(leftStream, this.m_leftKeySelector, this.m_keyComparer, null, settings.CancellationState.MergedCancellationToken), rightStream, outputRecipient, settings.CancellationState.MergedCancellationToken);
				return;
			}
			this.WrapPartitionedStreamHelper<int, TRightKey>(ExchangeUtilities.HashRepartition<TLeftInput, TKey, TLeftKey>(leftStream, this.m_leftKeySelector, this.m_keyComparer, null, settings.CancellationState.MergedCancellationToken), rightStream, outputRecipient, settings.CancellationState.MergedCancellationToken);
		}

		// Token: 0x06000E5E RID: 3678 RVA: 0x000333EC File Offset: 0x000315EC
		private void WrapPartitionedStreamHelper<TLeftKey, TRightKey>(PartitionedStream<Pair<TLeftInput, TKey>, TLeftKey> leftHashStream, PartitionedStream<TRightInput, TRightKey> rightPartitionedStream, IPartitionedStreamRecipient<TOutput> outputRecipient, CancellationToken cancellationToken)
		{
			int partitionCount = leftHashStream.PartitionCount;
			PartitionedStream<Pair<TRightInput, TKey>, int> partitionedStream = ExchangeUtilities.HashRepartition<TRightInput, TKey, TRightKey>(rightPartitionedStream, this.m_rightKeySelector, this.m_keyComparer, null, cancellationToken);
			PartitionedStream<TOutput, TLeftKey> partitionedStream2 = new PartitionedStream<TOutput, TLeftKey>(partitionCount, leftHashStream.KeyComparer, this.OrdinalIndexState);
			for (int i = 0; i < partitionCount; i++)
			{
				partitionedStream2[i] = new HashJoinQueryOperatorEnumerator<TLeftInput, TLeftKey, TRightInput, TKey, TOutput>(leftHashStream[i], partitionedStream[i], this.m_resultSelector, null, this.m_keyComparer, cancellationToken);
			}
			outputRecipient.Receive<TLeftKey>(partitionedStream2);
		}

		// Token: 0x06000E5F RID: 3679 RVA: 0x00033468 File Offset: 0x00031668
		internal override QueryResults<TOutput> Open(QuerySettings settings, bool preferStriping)
		{
			QueryResults<TLeftInput> leftChildQueryResults = base.LeftChild.Open(settings, false);
			QueryResults<TRightInput> rightChildQueryResults = base.RightChild.Open(settings, false);
			return new BinaryQueryOperator<TLeftInput, TRightInput, TOutput>.BinaryQueryOperatorResults(leftChildQueryResults, rightChildQueryResults, this, settings, false);
		}

		// Token: 0x06000E60 RID: 3680 RVA: 0x0003349C File Offset: 0x0003169C
		internal override IEnumerable<TOutput> AsSequentialQuery(CancellationToken token)
		{
			IEnumerable<TLeftInput> outer = CancellableEnumerable.Wrap<TLeftInput>(base.LeftChild.AsSequentialQuery(token), token);
			IEnumerable<TRightInput> inner = CancellableEnumerable.Wrap<TRightInput>(base.RightChild.AsSequentialQuery(token), token);
			return outer.Join(inner, this.m_leftKeySelector, this.m_rightKeySelector, this.m_resultSelector, this.m_keyComparer);
		}

		// Token: 0x1700028D RID: 653
		// (get) Token: 0x06000E61 RID: 3681 RVA: 0x000334EE File Offset: 0x000316EE
		internal override bool LimitsParallelism
		{
			get
			{
				return false;
			}
		}

		// Token: 0x04000879 RID: 2169
		private readonly Func<TLeftInput, TKey> m_leftKeySelector;

		// Token: 0x0400087A RID: 2170
		private readonly Func<TRightInput, TKey> m_rightKeySelector;

		// Token: 0x0400087B RID: 2171
		private readonly Func<TLeftInput, TRightInput, TOutput> m_resultSelector;

		// Token: 0x0400087C RID: 2172
		private readonly IEqualityComparer<TKey> m_keyComparer;
	}
}
