using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace System.Linq.Parallel
{
	// Token: 0x02000182 RID: 386
	internal class DefaultMergeHelper<TInputOutput, TIgnoreKey> : IMergeHelper<TInputOutput>
	{
		// Token: 0x06000DF9 RID: 3577 RVA: 0x00031750 File Offset: 0x0002F950
		internal DefaultMergeHelper(PartitionedStream<TInputOutput, TIgnoreKey> partitions, bool ignoreOutput, ParallelMergeOptions options, TaskScheduler taskScheduler, CancellationState cancellationState, int queryId)
		{
			this.m_taskGroupState = new QueryTaskGroupState(cancellationState, queryId);
			this.m_partitions = partitions;
			this.m_taskScheduler = taskScheduler;
			this.m_ignoreOutput = ignoreOutput;
			IntValueEvent consumerEvent = new IntValueEvent();
			if (!ignoreOutput)
			{
				if (options != ParallelMergeOptions.FullyBuffered)
				{
					if (partitions.PartitionCount > 1)
					{
						this.m_asyncChannels = MergeExecutor<TInputOutput>.MakeAsynchronousChannels(partitions.PartitionCount, options, consumerEvent, cancellationState.MergedCancellationToken);
						this.m_channelEnumerator = new AsynchronousChannelMergeEnumerator<TInputOutput>(this.m_taskGroupState, this.m_asyncChannels, consumerEvent);
						return;
					}
					this.m_channelEnumerator = ExceptionAggregator.WrapQueryEnumerator<TInputOutput, TIgnoreKey>(partitions[0], this.m_taskGroupState.CancellationState).GetEnumerator();
					return;
				}
				else
				{
					this.m_syncChannels = MergeExecutor<TInputOutput>.MakeSynchronousChannels(partitions.PartitionCount);
					this.m_channelEnumerator = new SynchronousChannelMergeEnumerator<TInputOutput>(this.m_taskGroupState, this.m_syncChannels);
				}
			}
		}

		// Token: 0x06000DFA RID: 3578 RVA: 0x00031820 File Offset: 0x0002FA20
		void IMergeHelper<!0>.Execute()
		{
			if (this.m_asyncChannels != null)
			{
				SpoolingTask.SpoolPipeline<TInputOutput, TIgnoreKey>(this.m_taskGroupState, this.m_partitions, this.m_asyncChannels, this.m_taskScheduler);
				return;
			}
			if (this.m_syncChannels != null)
			{
				SpoolingTask.SpoolStopAndGo<TInputOutput, TIgnoreKey>(this.m_taskGroupState, this.m_partitions, this.m_syncChannels, this.m_taskScheduler);
				return;
			}
			if (this.m_ignoreOutput)
			{
				SpoolingTask.SpoolForAll<TInputOutput, TIgnoreKey>(this.m_taskGroupState, this.m_partitions, this.m_taskScheduler);
			}
		}

		// Token: 0x06000DFB RID: 3579 RVA: 0x00031898 File Offset: 0x0002FA98
		IEnumerator<TInputOutput> IMergeHelper<!0>.GetEnumerator()
		{
			return this.m_channelEnumerator;
		}

		// Token: 0x06000DFC RID: 3580 RVA: 0x000318A0 File Offset: 0x0002FAA0
		public TInputOutput[] GetResultsAsArray()
		{
			if (this.m_syncChannels != null)
			{
				int num = 0;
				for (int i = 0; i < this.m_syncChannels.Length; i++)
				{
					num += this.m_syncChannels[i].Count;
				}
				TInputOutput[] array = new TInputOutput[num];
				int num2 = 0;
				for (int j = 0; j < this.m_syncChannels.Length; j++)
				{
					this.m_syncChannels[j].CopyTo(array, num2);
					num2 += this.m_syncChannels[j].Count;
				}
				return array;
			}
			List<TInputOutput> list = new List<TInputOutput>();
			foreach (TInputOutput item in ((IMergeHelper<TInputOutput>)this))
			{
				list.Add(item);
			}
			return list.ToArray();
		}

		// Token: 0x04000828 RID: 2088
		private QueryTaskGroupState m_taskGroupState;

		// Token: 0x04000829 RID: 2089
		private PartitionedStream<TInputOutput, TIgnoreKey> m_partitions;

		// Token: 0x0400082A RID: 2090
		private AsynchronousChannel<TInputOutput>[] m_asyncChannels;

		// Token: 0x0400082B RID: 2091
		private SynchronousChannel<TInputOutput>[] m_syncChannels;

		// Token: 0x0400082C RID: 2092
		private IEnumerator<TInputOutput> m_channelEnumerator;

		// Token: 0x0400082D RID: 2093
		private TaskScheduler m_taskScheduler;

		// Token: 0x0400082E RID: 2094
		private bool m_ignoreOutput;
	}
}
