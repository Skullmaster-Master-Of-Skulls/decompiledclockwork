using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace System.Linq.Parallel
{
	// Token: 0x02000186 RID: 390
	internal class OrderPreservingMergeHelper<TInputOutput, TKey> : IMergeHelper<!0>
	{
		// Token: 0x06000E0E RID: 3598 RVA: 0x00031ADB File Offset: 0x0002FCDB
		internal OrderPreservingMergeHelper(PartitionedStream<TInputOutput, TKey> partitions, TaskScheduler taskScheduler, CancellationState cancellationState, int queryId)
		{
			this.m_taskGroupState = new QueryTaskGroupState(cancellationState, queryId);
			this.m_partitions = partitions;
			this.m_results = new Shared<TInputOutput[]>(null);
			this.m_taskScheduler = taskScheduler;
		}

		// Token: 0x06000E0F RID: 3599 RVA: 0x00031B0B File Offset: 0x0002FD0B
		void IMergeHelper<!0>.Execute()
		{
			OrderPreservingSpoolingTask<TInputOutput, TKey>.Spool(this.m_taskGroupState, this.m_partitions, this.m_results, this.m_taskScheduler);
		}

		// Token: 0x06000E10 RID: 3600 RVA: 0x00031B2A File Offset: 0x0002FD2A
		IEnumerator<TInputOutput> IMergeHelper<!0>.GetEnumerator()
		{
			return this.m_results.Value.GetEnumerator();
		}

		// Token: 0x06000E11 RID: 3601 RVA: 0x00031B3C File Offset: 0x0002FD3C
		public TInputOutput[] GetResultsAsArray()
		{
			return this.m_results.Value;
		}

		// Token: 0x04000831 RID: 2097
		private QueryTaskGroupState m_taskGroupState;

		// Token: 0x04000832 RID: 2098
		private PartitionedStream<TInputOutput, TKey> m_partitions;

		// Token: 0x04000833 RID: 2099
		private Shared<TInputOutput[]> m_results;

		// Token: 0x04000834 RID: 2100
		private TaskScheduler m_taskScheduler;
	}
}
