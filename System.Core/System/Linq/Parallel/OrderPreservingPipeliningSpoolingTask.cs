using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace System.Linq.Parallel
{
	// Token: 0x020001EC RID: 492
	internal class OrderPreservingPipeliningSpoolingTask<TOutput, TKey> : SpoolingTaskBase
	{
		// Token: 0x06000FE6 RID: 4070 RVA: 0x00038160 File Offset: 0x00036360
		internal OrderPreservingPipeliningSpoolingTask(QueryOperatorEnumerator<TOutput, TKey> partition, QueryTaskGroupState taskGroupState, bool[] consumerWaiting, bool[] producerWaiting, bool[] producerDone, int partitionIndex, Queue<Pair<TKey, TOutput>>[] buffers, object bufferLock, TaskScheduler taskScheduler, bool autoBuffered) : base(partitionIndex, taskGroupState)
		{
			this.m_partition = partition;
			this.m_taskGroupState = taskGroupState;
			this.m_producerDone = producerDone;
			this.m_consumerWaiting = consumerWaiting;
			this.m_producerWaiting = producerWaiting;
			this.m_partitionIndex = partitionIndex;
			this.m_buffers = buffers;
			this.m_bufferLock = bufferLock;
			this.m_taskScheduler = taskScheduler;
			this.m_autoBuffered = autoBuffered;
		}

		// Token: 0x06000FE7 RID: 4071 RVA: 0x000381C4 File Offset: 0x000363C4
		protected override void SpoolingWork()
		{
			TOutput second = default(TOutput);
			TKey first = default(TKey);
			int num = this.m_autoBuffered ? 16 : 1;
			Pair<TKey, TOutput>[] array = new Pair<TKey, TOutput>[num];
			QueryOperatorEnumerator<TOutput, TKey> partition = this.m_partition;
			CancellationToken mergedCancellationToken = this.m_taskGroupState.CancellationState.MergedCancellationToken;
			int num2;
			do
			{
				num2 = 0;
				while (num2 < num && partition.MoveNext(ref second, ref first))
				{
					array[num2] = new Pair<TKey, TOutput>(first, second);
					num2++;
				}
				if (num2 == 0)
				{
					break;
				}
				object bufferLock = this.m_bufferLock;
				lock (bufferLock)
				{
					if (mergedCancellationToken.IsCancellationRequested)
					{
						break;
					}
					for (int i = 0; i < num2; i++)
					{
						this.m_buffers[this.m_partitionIndex].Enqueue(array[i]);
					}
					if (this.m_consumerWaiting[this.m_partitionIndex])
					{
						Monitor.Pulse(this.m_bufferLock);
						this.m_consumerWaiting[this.m_partitionIndex] = false;
					}
					if (this.m_buffers[this.m_partitionIndex].Count >= 8192)
					{
						this.m_producerWaiting[this.m_partitionIndex] = true;
						Monitor.Wait(this.m_bufferLock);
					}
				}
			}
			while (num2 == num);
		}

		// Token: 0x06000FE8 RID: 4072 RVA: 0x00038314 File Offset: 0x00036514
		public static void Spool(QueryTaskGroupState groupState, PartitionedStream<TOutput, TKey> partitions, bool[] consumerWaiting, bool[] producerWaiting, bool[] producerDone, Queue<Pair<TKey, TOutput>>[] buffers, object[] bufferLocks, TaskScheduler taskScheduler, bool autoBuffered)
		{
			int degreeOfParallelism = partitions.PartitionCount;
			for (int i = 0; i < degreeOfParallelism; i++)
			{
				buffers[i] = new Queue<Pair<TKey, TOutput>>(128);
				bufferLocks[i] = new object();
			}
			Task task = new Task(delegate()
			{
				for (int j = 0; j < degreeOfParallelism; j++)
				{
					QueryTask queryTask = new OrderPreservingPipeliningSpoolingTask<TOutput, TKey>(partitions[j], groupState, consumerWaiting, producerWaiting, producerDone, j, buffers, bufferLocks[j], taskScheduler, autoBuffered);
					queryTask.RunAsynchronously(taskScheduler);
				}
			});
			groupState.QueryBegin(task);
			task.Start(taskScheduler);
		}

		// Token: 0x06000FE9 RID: 4073 RVA: 0x000383D8 File Offset: 0x000365D8
		protected override void SpoolingFinally()
		{
			object bufferLock = this.m_bufferLock;
			lock (bufferLock)
			{
				this.m_producerDone[this.m_partitionIndex] = true;
				if (this.m_consumerWaiting[this.m_partitionIndex])
				{
					Monitor.Pulse(this.m_bufferLock);
					this.m_consumerWaiting[this.m_partitionIndex] = false;
				}
			}
			base.SpoolingFinally();
			this.m_partition.Dispose();
		}

		// Token: 0x04000907 RID: 2311
		private readonly QueryTaskGroupState m_taskGroupState;

		// Token: 0x04000908 RID: 2312
		private readonly TaskScheduler m_taskScheduler;

		// Token: 0x04000909 RID: 2313
		private readonly QueryOperatorEnumerator<TOutput, TKey> m_partition;

		// Token: 0x0400090A RID: 2314
		private readonly bool[] m_consumerWaiting;

		// Token: 0x0400090B RID: 2315
		private readonly bool[] m_producerWaiting;

		// Token: 0x0400090C RID: 2316
		private readonly bool[] m_producerDone;

		// Token: 0x0400090D RID: 2317
		private readonly int m_partitionIndex;

		// Token: 0x0400090E RID: 2318
		private readonly Queue<Pair<TKey, TOutput>>[] m_buffers;

		// Token: 0x0400090F RID: 2319
		private readonly object m_bufferLock;

		// Token: 0x04000910 RID: 2320
		private readonly bool m_autoBuffered;

		// Token: 0x04000911 RID: 2321
		private const int PRODUCER_BUFFER_AUTO_SIZE = 16;
	}
}
