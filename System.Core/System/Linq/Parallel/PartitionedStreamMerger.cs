using System;
using System.Threading.Tasks;

namespace System.Linq.Parallel
{
	// Token: 0x0200019E RID: 414
	internal class PartitionedStreamMerger<TOutput> : IPartitionedStreamRecipient<TOutput>
	{
		// Token: 0x17000294 RID: 660
		// (get) Token: 0x06000E76 RID: 3702 RVA: 0x00033992 File Offset: 0x00031B92
		internal MergeExecutor<TOutput> MergeExecutor
		{
			get
			{
				return this.m_mergeExecutor;
			}
		}

		// Token: 0x06000E77 RID: 3703 RVA: 0x0003399A File Offset: 0x00031B9A
		internal PartitionedStreamMerger(bool forEffectMerge, ParallelMergeOptions mergeOptions, TaskScheduler taskScheduler, bool outputOrdered, CancellationState cancellationState, int queryId)
		{
			this.m_forEffectMerge = forEffectMerge;
			this.m_mergeOptions = mergeOptions;
			this.m_isOrdered = outputOrdered;
			this.m_taskScheduler = taskScheduler;
			this.m_cancellationState = cancellationState;
			this.m_queryId = queryId;
		}

		// Token: 0x06000E78 RID: 3704 RVA: 0x000339CF File Offset: 0x00031BCF
		public void Receive<TKey>(PartitionedStream<TOutput, TKey> partitionedStream)
		{
			this.m_mergeExecutor = MergeExecutor<TOutput>.Execute<TKey>(partitionedStream, this.m_forEffectMerge, this.m_mergeOptions, this.m_taskScheduler, this.m_isOrdered, this.m_cancellationState, this.m_queryId);
		}

		// Token: 0x04000887 RID: 2183
		private bool m_forEffectMerge;

		// Token: 0x04000888 RID: 2184
		private ParallelMergeOptions m_mergeOptions;

		// Token: 0x04000889 RID: 2185
		private bool m_isOrdered;

		// Token: 0x0400088A RID: 2186
		private MergeExecutor<TOutput> m_mergeExecutor;

		// Token: 0x0400088B RID: 2187
		private TaskScheduler m_taskScheduler;

		// Token: 0x0400088C RID: 2188
		private int m_queryId;

		// Token: 0x0400088D RID: 2189
		private CancellationState m_cancellationState;
	}
}
