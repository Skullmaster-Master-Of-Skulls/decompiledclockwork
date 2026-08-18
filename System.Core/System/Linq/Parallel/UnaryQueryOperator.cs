using System;
using System.Collections.Generic;

namespace System.Linq.Parallel
{
	// Token: 0x020001E9 RID: 489
	internal abstract class UnaryQueryOperator<TInput, TOutput> : QueryOperator<TOutput>
	{
		// Token: 0x06000FD6 RID: 4054 RVA: 0x00037F77 File Offset: 0x00036177
		internal UnaryQueryOperator(IEnumerable<TInput> child) : this(QueryOperator<TInput>.AsQueryOperator(child))
		{
		}

		// Token: 0x06000FD7 RID: 4055 RVA: 0x00037F85 File Offset: 0x00036185
		internal UnaryQueryOperator(IEnumerable<TInput> child, bool outputOrdered) : this(QueryOperator<TInput>.AsQueryOperator(child), outputOrdered)
		{
		}

		// Token: 0x06000FD8 RID: 4056 RVA: 0x00037F94 File Offset: 0x00036194
		private UnaryQueryOperator(QueryOperator<TInput> child) : this(child, child.OutputOrdered, child.SpecifiedQuerySettings)
		{
		}

		// Token: 0x06000FD9 RID: 4057 RVA: 0x00037FA9 File Offset: 0x000361A9
		internal UnaryQueryOperator(QueryOperator<TInput> child, bool outputOrdered) : this(child, outputOrdered, child.SpecifiedQuerySettings)
		{
		}

		// Token: 0x06000FDA RID: 4058 RVA: 0x00037FB9 File Offset: 0x000361B9
		private UnaryQueryOperator(QueryOperator<TInput> child, bool outputOrdered, QuerySettings settings) : base(outputOrdered, settings)
		{
			this.m_child = child;
		}

		// Token: 0x170002C7 RID: 711
		// (get) Token: 0x06000FDB RID: 4059 RVA: 0x00037FD1 File Offset: 0x000361D1
		internal QueryOperator<TInput> Child
		{
			get
			{
				return this.m_child;
			}
		}

		// Token: 0x170002C8 RID: 712
		// (get) Token: 0x06000FDC RID: 4060 RVA: 0x00037FD9 File Offset: 0x000361D9
		internal sealed override OrdinalIndexState OrdinalIndexState
		{
			get
			{
				return this.m_indexState;
			}
		}

		// Token: 0x06000FDD RID: 4061 RVA: 0x00037FE1 File Offset: 0x000361E1
		protected void SetOrdinalIndexState(OrdinalIndexState indexState)
		{
			this.m_indexState = indexState;
		}

		// Token: 0x06000FDE RID: 4062
		internal abstract void WrapPartitionedStream<TKey>(PartitionedStream<TInput, TKey> inputStream, IPartitionedStreamRecipient<TOutput> recipient, bool preferStriping, QuerySettings settings);

		// Token: 0x040008FE RID: 2302
		private readonly QueryOperator<TInput> m_child;

		// Token: 0x040008FF RID: 2303
		private OrdinalIndexState m_indexState = OrdinalIndexState.Shuffled;

		// Token: 0x0200040D RID: 1037
		internal class UnaryQueryOperatorResults : QueryResults<TOutput>
		{
			// Token: 0x06001E6B RID: 7787 RVA: 0x0006D357 File Offset: 0x0006B557
			internal UnaryQueryOperatorResults(QueryResults<TInput> childQueryResults, UnaryQueryOperator<TInput, TOutput> op, QuerySettings settings, bool preferStriping)
			{
				this.m_childQueryResults = childQueryResults;
				this.m_op = op;
				this.m_settings = settings;
				this.m_preferStriping = preferStriping;
			}

			// Token: 0x06001E6C RID: 7788 RVA: 0x0006D37C File Offset: 0x0006B57C
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
				this.m_childQueryResults.GivePartitionedStream(new UnaryQueryOperator<TInput, TOutput>.UnaryQueryOperatorResults.ChildResultsRecipient(recipient, this.m_op, this.m_preferStriping, this.m_settings));
			}

			// Token: 0x0400123F RID: 4671
			protected QueryResults<TInput> m_childQueryResults;

			// Token: 0x04001240 RID: 4672
			private UnaryQueryOperator<TInput, TOutput> m_op;

			// Token: 0x04001241 RID: 4673
			private QuerySettings m_settings;

			// Token: 0x04001242 RID: 4674
			private bool m_preferStriping;

			// Token: 0x02000497 RID: 1175
			private class ChildResultsRecipient : IPartitionedStreamRecipient<TInput>
			{
				// Token: 0x06002063 RID: 8291 RVA: 0x00070917 File Offset: 0x0006EB17
				internal ChildResultsRecipient(IPartitionedStreamRecipient<TOutput> outputRecipient, UnaryQueryOperator<TInput, TOutput> op, bool preferStriping, QuerySettings settings)
				{
					this.m_outputRecipient = outputRecipient;
					this.m_op = op;
					this.m_preferStriping = preferStriping;
					this.m_settings = settings;
				}

				// Token: 0x06002064 RID: 8292 RVA: 0x0007093C File Offset: 0x0006EB3C
				public void Receive<TKey>(PartitionedStream<TInput, TKey> inputStream)
				{
					this.m_op.WrapPartitionedStream<TKey>(inputStream, this.m_outputRecipient, this.m_preferStriping, this.m_settings);
				}

				// Token: 0x040013FD RID: 5117
				private IPartitionedStreamRecipient<TOutput> m_outputRecipient;

				// Token: 0x040013FE RID: 5118
				private UnaryQueryOperator<TInput, TOutput> m_op;

				// Token: 0x040013FF RID: 5119
				private bool m_preferStriping;

				// Token: 0x04001400 RID: 5120
				private QuerySettings m_settings;
			}
		}
	}
}
