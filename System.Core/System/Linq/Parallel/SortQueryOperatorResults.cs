using System;

namespace System.Linq.Parallel
{
	// Token: 0x020001E4 RID: 484
	internal class SortQueryOperatorResults<TInputOutput, TSortKey> : QueryResults<TInputOutput>
	{
		// Token: 0x06000FBC RID: 4028 RVA: 0x00037A0D File Offset: 0x00035C0D
		internal SortQueryOperatorResults(QueryResults<TInputOutput> childQueryResults, SortQueryOperator<TInputOutput, TSortKey> op, QuerySettings settings, bool preferStriping)
		{
			this.m_childQueryResults = childQueryResults;
			this.m_op = op;
			this.m_settings = settings;
			this.m_preferStriping = preferStriping;
		}

		// Token: 0x170002C2 RID: 706
		// (get) Token: 0x06000FBD RID: 4029 RVA: 0x00037A32 File Offset: 0x00035C32
		internal override bool IsIndexible
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000FBE RID: 4030 RVA: 0x00037A35 File Offset: 0x00035C35
		internal override void GivePartitionedStream(IPartitionedStreamRecipient<TInputOutput> recipient)
		{
			this.m_childQueryResults.GivePartitionedStream(new SortQueryOperatorResults<TInputOutput, TSortKey>.ChildResultsRecipient(recipient, this.m_op, this.m_settings));
		}

		// Token: 0x040008EE RID: 2286
		protected QueryResults<TInputOutput> m_childQueryResults;

		// Token: 0x040008EF RID: 2287
		private SortQueryOperator<TInputOutput, TSortKey> m_op;

		// Token: 0x040008F0 RID: 2288
		private QuerySettings m_settings;

		// Token: 0x040008F1 RID: 2289
		private bool m_preferStriping;

		// Token: 0x02000407 RID: 1031
		private class ChildResultsRecipient : IPartitionedStreamRecipient<TInputOutput>
		{
			// Token: 0x06001E5A RID: 7770 RVA: 0x0006CA36 File Offset: 0x0006AC36
			internal ChildResultsRecipient(IPartitionedStreamRecipient<TInputOutput> outputRecipient, SortQueryOperator<TInputOutput, TSortKey> op, QuerySettings settings)
			{
				this.m_outputRecipient = outputRecipient;
				this.m_op = op;
				this.m_settings = settings;
			}

			// Token: 0x06001E5B RID: 7771 RVA: 0x0006CA53 File Offset: 0x0006AC53
			public void Receive<TKey>(PartitionedStream<TInputOutput, TKey> childPartitionedStream)
			{
				this.m_op.WrapPartitionedStream<TKey>(childPartitionedStream, this.m_outputRecipient, false, this.m_settings);
			}

			// Token: 0x0400121F RID: 4639
			private IPartitionedStreamRecipient<TInputOutput> m_outputRecipient;

			// Token: 0x04001220 RID: 4640
			private SortQueryOperator<TInputOutput, TSortKey> m_op;

			// Token: 0x04001221 RID: 4641
			private QuerySettings m_settings;
		}
	}
}
