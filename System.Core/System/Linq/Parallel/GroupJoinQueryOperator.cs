using System;
using System.Collections.Generic;
using System.Threading;

namespace System.Linq.Parallel
{
	// Token: 0x02000197 RID: 407
	internal sealed class GroupJoinQueryOperator<TLeftInput, TRightInput, TKey, TOutput> : BinaryQueryOperator<TLeftInput, TRightInput, TOutput>
	{
		// Token: 0x06000E4D RID: 3661 RVA: 0x00032C84 File Offset: 0x00030E84
		internal GroupJoinQueryOperator(ParallelQuery<TLeftInput> left, ParallelQuery<TRightInput> right, Func<TLeftInput, TKey> leftKeySelector, Func<TRightInput, TKey> rightKeySelector, Func<TLeftInput, IEnumerable<TRightInput>, TOutput> resultSelector, IEqualityComparer<TKey> keyComparer) : base(left, right)
		{
			this.m_leftKeySelector = leftKeySelector;
			this.m_rightKeySelector = rightKeySelector;
			this.m_resultSelector = resultSelector;
			this.m_keyComparer = keyComparer;
			this.m_outputOrdered = base.LeftChild.OutputOrdered;
			base.SetOrdinalIndex(OrdinalIndexState.Shuffled);
		}

		// Token: 0x06000E4E RID: 3662 RVA: 0x00032CD0 File Offset: 0x00030ED0
		internal override QueryResults<TOutput> Open(QuerySettings settings, bool preferStriping)
		{
			QueryResults<TLeftInput> leftChildQueryResults = base.LeftChild.Open(settings, false);
			QueryResults<TRightInput> rightChildQueryResults = base.RightChild.Open(settings, false);
			return new BinaryQueryOperator<TLeftInput, TRightInput, TOutput>.BinaryQueryOperatorResults(leftChildQueryResults, rightChildQueryResults, this, settings, false);
		}

		// Token: 0x06000E4F RID: 3663 RVA: 0x00032D04 File Offset: 0x00030F04
		public override void WrapPartitionedStream<TLeftKey, TRightKey>(PartitionedStream<TLeftInput, TLeftKey> leftStream, PartitionedStream<TRightInput, TRightKey> rightStream, IPartitionedStreamRecipient<TOutput> outputRecipient, bool preferStriping, QuerySettings settings)
		{
			int partitionCount = leftStream.PartitionCount;
			if (base.LeftChild.OutputOrdered)
			{
				this.WrapPartitionedStreamHelper<TLeftKey, TRightKey>(ExchangeUtilities.HashRepartitionOrdered<TLeftInput, TKey, TLeftKey>(leftStream, this.m_leftKeySelector, this.m_keyComparer, null, settings.CancellationState.MergedCancellationToken), rightStream, outputRecipient, partitionCount, settings.CancellationState.MergedCancellationToken);
				return;
			}
			this.WrapPartitionedStreamHelper<int, TRightKey>(ExchangeUtilities.HashRepartition<TLeftInput, TKey, TLeftKey>(leftStream, this.m_leftKeySelector, this.m_keyComparer, null, settings.CancellationState.MergedCancellationToken), rightStream, outputRecipient, partitionCount, settings.CancellationState.MergedCancellationToken);
		}

		// Token: 0x06000E50 RID: 3664 RVA: 0x00032D90 File Offset: 0x00030F90
		private void WrapPartitionedStreamHelper<TLeftKey, TRightKey>(PartitionedStream<Pair<TLeftInput, TKey>, TLeftKey> leftHashStream, PartitionedStream<TRightInput, TRightKey> rightPartitionedStream, IPartitionedStreamRecipient<TOutput> outputRecipient, int partitionCount, CancellationToken cancellationToken)
		{
			PartitionedStream<Pair<TRightInput, TKey>, int> partitionedStream = ExchangeUtilities.HashRepartition<TRightInput, TKey, TRightKey>(rightPartitionedStream, this.m_rightKeySelector, this.m_keyComparer, null, cancellationToken);
			PartitionedStream<TOutput, TLeftKey> partitionedStream2 = new PartitionedStream<TOutput, TLeftKey>(partitionCount, leftHashStream.KeyComparer, this.OrdinalIndexState);
			for (int i = 0; i < partitionCount; i++)
			{
				partitionedStream2[i] = new HashJoinQueryOperatorEnumerator<TLeftInput, TLeftKey, TRightInput, TKey, TOutput>(leftHashStream[i], partitionedStream[i], null, this.m_resultSelector, this.m_keyComparer, cancellationToken);
			}
			outputRecipient.Receive<TLeftKey>(partitionedStream2);
		}

		// Token: 0x06000E51 RID: 3665 RVA: 0x00032E04 File Offset: 0x00031004
		internal override IEnumerable<TOutput> AsSequentialQuery(CancellationToken token)
		{
			IEnumerable<TLeftInput> outer = CancellableEnumerable.Wrap<TLeftInput>(base.LeftChild.AsSequentialQuery(token), token);
			IEnumerable<TRightInput> inner = CancellableEnumerable.Wrap<TRightInput>(base.RightChild.AsSequentialQuery(token), token);
			return outer.GroupJoin(inner, this.m_leftKeySelector, this.m_rightKeySelector, this.m_resultSelector, this.m_keyComparer);
		}

		// Token: 0x1700028B RID: 651
		// (get) Token: 0x06000E52 RID: 3666 RVA: 0x00032E56 File Offset: 0x00031056
		internal override bool LimitsParallelism
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0400086D RID: 2157
		private readonly Func<TLeftInput, TKey> m_leftKeySelector;

		// Token: 0x0400086E RID: 2158
		private readonly Func<TRightInput, TKey> m_rightKeySelector;

		// Token: 0x0400086F RID: 2159
		private readonly Func<TLeftInput, IEnumerable<TRightInput>, TOutput> m_resultSelector;

		// Token: 0x04000870 RID: 2160
		private readonly IEqualityComparer<TKey> m_keyComparer;
	}
}
