using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace System.Linq.Parallel
{
	// Token: 0x02000185 RID: 389
	internal class MergeExecutor<TInputOutput> : IEnumerable<!0>, IEnumerable
	{
		// Token: 0x06000E06 RID: 3590 RVA: 0x000319A5 File Offset: 0x0002FBA5
		private MergeExecutor()
		{
		}

		// Token: 0x06000E07 RID: 3591 RVA: 0x000319B0 File Offset: 0x0002FBB0
		internal static MergeExecutor<TInputOutput> Execute<TKey>(PartitionedStream<TInputOutput, TKey> partitions, bool ignoreOutput, ParallelMergeOptions options, TaskScheduler taskScheduler, bool isOrdered, CancellationState cancellationState, int queryId)
		{
			MergeExecutor<TInputOutput> mergeExecutor = new MergeExecutor<TInputOutput>();
			if (isOrdered && !ignoreOutput)
			{
				if (options != ParallelMergeOptions.FullyBuffered && !partitions.OrdinalIndexState.IsWorseThan(OrdinalIndexState.Increasing))
				{
					bool autoBuffered = options == ParallelMergeOptions.AutoBuffered;
					if (partitions.PartitionCount > 1)
					{
						mergeExecutor.m_mergeHelper = new OrderPreservingPipeliningMergeHelper<TInputOutput, TKey>(partitions, taskScheduler, cancellationState, autoBuffered, queryId, partitions.KeyComparer);
					}
					else
					{
						mergeExecutor.m_mergeHelper = new DefaultMergeHelper<TInputOutput, TKey>(partitions, false, options, taskScheduler, cancellationState, queryId);
					}
				}
				else
				{
					mergeExecutor.m_mergeHelper = new OrderPreservingMergeHelper<TInputOutput, TKey>(partitions, taskScheduler, cancellationState, queryId);
				}
			}
			else
			{
				mergeExecutor.m_mergeHelper = new DefaultMergeHelper<TInputOutput, TKey>(partitions, ignoreOutput, options, taskScheduler, cancellationState, queryId);
			}
			mergeExecutor.Execute();
			return mergeExecutor;
		}

		// Token: 0x06000E08 RID: 3592 RVA: 0x00031A46 File Offset: 0x0002FC46
		private void Execute()
		{
			this.m_mergeHelper.Execute();
		}

		// Token: 0x06000E09 RID: 3593 RVA: 0x00031A53 File Offset: 0x0002FC53
		IEnumerator IEnumerable.GetEnumerator()
		{
			return ((IEnumerable<TInputOutput>)this).GetEnumerator();
		}

		// Token: 0x06000E0A RID: 3594 RVA: 0x00031A5B File Offset: 0x0002FC5B
		public IEnumerator<TInputOutput> GetEnumerator()
		{
			return this.m_mergeHelper.GetEnumerator();
		}

		// Token: 0x06000E0B RID: 3595 RVA: 0x00031A68 File Offset: 0x0002FC68
		internal TInputOutput[] GetResultsAsArray()
		{
			return this.m_mergeHelper.GetResultsAsArray();
		}

		// Token: 0x06000E0C RID: 3596 RVA: 0x00031A78 File Offset: 0x0002FC78
		internal static AsynchronousChannel<TInputOutput>[] MakeAsynchronousChannels(int partitionCount, ParallelMergeOptions options, IntValueEvent consumerEvent, CancellationToken cancellationToken)
		{
			AsynchronousChannel<TInputOutput>[] array = new AsynchronousChannel<TInputOutput>[partitionCount];
			int chunkSize = 0;
			if (options == ParallelMergeOptions.NotBuffered)
			{
				chunkSize = 1;
			}
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = new AsynchronousChannel<TInputOutput>(i, chunkSize, cancellationToken, consumerEvent);
			}
			return array;
		}

		// Token: 0x06000E0D RID: 3597 RVA: 0x00031AB0 File Offset: 0x0002FCB0
		internal static SynchronousChannel<TInputOutput>[] MakeSynchronousChannels(int partitionCount)
		{
			SynchronousChannel<TInputOutput>[] array = new SynchronousChannel<TInputOutput>[partitionCount];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = new SynchronousChannel<TInputOutput>();
			}
			return array;
		}

		// Token: 0x04000830 RID: 2096
		private IMergeHelper<TInputOutput> m_mergeHelper;
	}
}
