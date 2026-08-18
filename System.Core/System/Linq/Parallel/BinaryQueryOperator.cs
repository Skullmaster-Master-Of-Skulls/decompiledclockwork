using System;
using System.Collections.Generic;

namespace System.Linq.Parallel
{
	// Token: 0x0200019D RID: 413
	internal abstract class BinaryQueryOperator<TLeftInput, TRightInput, TOutput> : QueryOperator<TOutput>
	{
		// Token: 0x06000E6F RID: 3695 RVA: 0x00033920 File Offset: 0x00031B20
		internal BinaryQueryOperator(ParallelQuery<TLeftInput> leftChild, ParallelQuery<TRightInput> rightChild) : this(QueryOperator<TLeftInput>.AsQueryOperator(leftChild), QueryOperator<TRightInput>.AsQueryOperator(rightChild))
		{
		}

		// Token: 0x06000E70 RID: 3696 RVA: 0x00033934 File Offset: 0x00031B34
		internal BinaryQueryOperator(QueryOperator<TLeftInput> leftChild, QueryOperator<TRightInput> rightChild) : base(false, leftChild.SpecifiedQuerySettings.Merge(rightChild.SpecifiedQuerySettings))
		{
			this.m_leftChild = leftChild;
			this.m_rightChild = rightChild;
		}

		// Token: 0x17000291 RID: 657
		// (get) Token: 0x06000E71 RID: 3697 RVA: 0x00033971 File Offset: 0x00031B71
		internal QueryOperator<TLeftInput> LeftChild
		{
			get
			{
				return this.m_leftChild;
			}
		}

		// Token: 0x17000292 RID: 658
		// (get) Token: 0x06000E72 RID: 3698 RVA: 0x00033979 File Offset: 0x00031B79
		internal QueryOperator<TRightInput> RightChild
		{
			get
			{
				return this.m_rightChild;
			}
		}

		// Token: 0x17000293 RID: 659
		// (get) Token: 0x06000E73 RID: 3699 RVA: 0x00033981 File Offset: 0x00031B81
		internal sealed override OrdinalIndexState OrdinalIndexState
		{
			get
			{
				return this.m_indexState;
			}
		}

		// Token: 0x06000E74 RID: 3700 RVA: 0x00033989 File Offset: 0x00031B89
		protected void SetOrdinalIndex(OrdinalIndexState indexState)
		{
			this.m_indexState = indexState;
		}

		// Token: 0x06000E75 RID: 3701
		public abstract void WrapPartitionedStream<TLeftKey, TRightKey>(PartitionedStream<TLeftInput, TLeftKey> leftPartitionedStream, PartitionedStream<TRightInput, TRightKey> rightPartitionedStream, IPartitionedStreamRecipient<TOutput> outputRecipient, bool preferStriping, QuerySettings settings);

		// Token: 0x04000884 RID: 2180
		private readonly QueryOperator<TLeftInput> m_leftChild;

		// Token: 0x04000885 RID: 2181
		private readonly QueryOperator<TRightInput> m_rightChild;

		// Token: 0x04000886 RID: 2182
		private OrdinalIndexState m_indexState = OrdinalIndexState.Shuffled;

		// Token: 0x020003C7 RID: 967
		internal class BinaryQueryOperatorResults : QueryResults<TOutput>
		{
			// Token: 0x06001D96 RID: 7574 RVA: 0x00069D2B File Offset: 0x00067F2B
			internal BinaryQueryOperatorResults(QueryResults<TLeftInput> leftChildQueryResults, QueryResults<TRightInput> rightChildQueryResults, BinaryQueryOperator<TLeftInput, TRightInput, TOutput> op, QuerySettings settings, bool preferStriping)
			{
				this.m_leftChildQueryResults = leftChildQueryResults;
				this.m_rightChildQueryResults = rightChildQueryResults;
				this.m_op = op;
				this.m_settings = settings;
				this.m_preferStriping = preferStriping;
			}

			// Token: 0x06001D97 RID: 7575 RVA: 0x00069D58 File Offset: 0x00067F58
			internal override void GivePartitionedStream(IPartitionedStreamRecipient<TOutput> recipient)
			{
				if (this.m_settings.ExecutionMode.Value == ParallelExecutionMode.Default && this.m_op.LimitsParallelism)
				{
					IEnumerable<TOutput> source = this.m_op.AsSequentialQuery(this.m_settings.CancellationState.ExternalCancellationToken);
					PartitionedStream<TOutput, int> partitionedStream = ExchangeUtilities.PartitionDataSource<TOutput>(source, this.m_settings.DegreeOfParallelism.Value, this.m_preferStriping);
					recipient.Receive<int>(partitionedStream);
					return;
				}
				if (this.IsIndexible)
				{
					PartitionedStream<TOutput, int> partitionedStream2 = ExchangeUtilities.PartitionDataSource<TOutput>(this, this.m_settings.DegreeOfParallelism.Value, this.m_preferStriping);
					recipient.Receive<int>(partitionedStream2);
					return;
				}
				this.m_leftChildQueryResults.GivePartitionedStream(new BinaryQueryOperator<TLeftInput, TRightInput, TOutput>.BinaryQueryOperatorResults.LeftChildResultsRecipient(recipient, this, this.m_preferStriping, this.m_settings));
			}

			// Token: 0x0400117A RID: 4474
			protected QueryResults<TLeftInput> m_leftChildQueryResults;

			// Token: 0x0400117B RID: 4475
			protected QueryResults<TRightInput> m_rightChildQueryResults;

			// Token: 0x0400117C RID: 4476
			private BinaryQueryOperator<TLeftInput, TRightInput, TOutput> m_op;

			// Token: 0x0400117D RID: 4477
			private QuerySettings m_settings;

			// Token: 0x0400117E RID: 4478
			private bool m_preferStriping;

			// Token: 0x02000493 RID: 1171
			private class LeftChildResultsRecipient : IPartitionedStreamRecipient<TLeftInput>
			{
				// Token: 0x0600205D RID: 8285 RVA: 0x0007083F File Offset: 0x0006EA3F
				internal LeftChildResultsRecipient(IPartitionedStreamRecipient<TOutput> outputRecipient, BinaryQueryOperator<TLeftInput, TRightInput, TOutput>.BinaryQueryOperatorResults results, bool preferStriping, QuerySettings settings)
				{
					this.m_outputRecipient = outputRecipient;
					this.m_results = results;
					this.m_preferStriping = preferStriping;
					this.m_settings = settings;
				}

				// Token: 0x0600205E RID: 8286 RVA: 0x00070864 File Offset: 0x0006EA64
				public void Receive<TLeftKey>(PartitionedStream<TLeftInput, TLeftKey> source)
				{
					BinaryQueryOperator<TLeftInput, TRightInput, TOutput>.BinaryQueryOperatorResults.RightChildResultsRecipient<TLeftKey> recipient = new BinaryQueryOperator<TLeftInput, TRightInput, TOutput>.BinaryQueryOperatorResults.RightChildResultsRecipient<TLeftKey>(this.m_outputRecipient, this.m_results.m_op, source, this.m_preferStriping, this.m_settings);
					this.m_results.m_rightChildQueryResults.GivePartitionedStream(recipient);
				}

				// Token: 0x040013EC RID: 5100
				private IPartitionedStreamRecipient<TOutput> m_outputRecipient;

				// Token: 0x040013ED RID: 5101
				private BinaryQueryOperator<TLeftInput, TRightInput, TOutput>.BinaryQueryOperatorResults m_results;

				// Token: 0x040013EE RID: 5102
				private bool m_preferStriping;

				// Token: 0x040013EF RID: 5103
				private QuerySettings m_settings;
			}

			// Token: 0x02000494 RID: 1172
			private class RightChildResultsRecipient<TLeftKey> : IPartitionedStreamRecipient<TRightInput>
			{
				// Token: 0x0600205F RID: 8287 RVA: 0x000708A6 File Offset: 0x0006EAA6
				internal RightChildResultsRecipient(IPartitionedStreamRecipient<TOutput> outputRecipient, BinaryQueryOperator<TLeftInput, TRightInput, TOutput> op, PartitionedStream<TLeftInput, TLeftKey> leftPartitionedStream, bool preferStriping, QuerySettings settings)
				{
					this.m_outputRecipient = outputRecipient;
					this.m_op = op;
					this.m_preferStriping = preferStriping;
					this.m_leftPartitionedStream = leftPartitionedStream;
					this.m_settings = settings;
				}

				// Token: 0x06002060 RID: 8288 RVA: 0x000708D3 File Offset: 0x0006EAD3
				public void Receive<TRightKey>(PartitionedStream<TRightInput, TRightKey> rightPartitionedStream)
				{
					this.m_op.WrapPartitionedStream<TLeftKey, TRightKey>(this.m_leftPartitionedStream, rightPartitionedStream, this.m_outputRecipient, this.m_preferStriping, this.m_settings);
				}

				// Token: 0x040013F0 RID: 5104
				private IPartitionedStreamRecipient<TOutput> m_outputRecipient;

				// Token: 0x040013F1 RID: 5105
				private PartitionedStream<TLeftInput, TLeftKey> m_leftPartitionedStream;

				// Token: 0x040013F2 RID: 5106
				private BinaryQueryOperator<TLeftInput, TRightInput, TOutput> m_op;

				// Token: 0x040013F3 RID: 5107
				private bool m_preferStriping;

				// Token: 0x040013F4 RID: 5108
				private QuerySettings m_settings;
			}
		}
	}
}
