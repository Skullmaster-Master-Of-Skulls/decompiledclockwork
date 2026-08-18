using System;
using System.Threading.Tasks;

namespace System.Linq.Parallel
{
	// Token: 0x020001F1 RID: 497
	internal static class SpoolingTask
	{
		// Token: 0x06000FFC RID: 4092 RVA: 0x00038738 File Offset: 0x00036938
		internal static void SpoolStopAndGo<TInputOutput, TIgnoreKey>(QueryTaskGroupState groupState, PartitionedStream<TInputOutput, TIgnoreKey> partitions, SynchronousChannel<TInputOutput>[] channels, TaskScheduler taskScheduler)
		{
			Task task = new Task(delegate()
			{
				int num = partitions.PartitionCount - 1;
				for (int i = 0; i < num; i++)
				{
					QueryTask queryTask = new StopAndGoSpoolingTask<TInputOutput, TIgnoreKey>(i, groupState, partitions[i], channels[i]);
					queryTask.RunAsynchronously(taskScheduler);
				}
				QueryTask queryTask2 = new StopAndGoSpoolingTask<TInputOutput, TIgnoreKey>(num, groupState, partitions[num], channels[num]);
				queryTask2.RunSynchronously(taskScheduler);
			});
			groupState.QueryBegin(task);
			task.RunSynchronously(taskScheduler);
			groupState.QueryEnd(false);
		}

		// Token: 0x06000FFD RID: 4093 RVA: 0x000387A0 File Offset: 0x000369A0
		internal static void SpoolPipeline<TInputOutput, TIgnoreKey>(QueryTaskGroupState groupState, PartitionedStream<TInputOutput, TIgnoreKey> partitions, AsynchronousChannel<TInputOutput>[] channels, TaskScheduler taskScheduler)
		{
			Task task = new Task(delegate()
			{
				for (int i = 0; i < partitions.PartitionCount; i++)
				{
					QueryTask queryTask = new PipelineSpoolingTask<TInputOutput, TIgnoreKey>(i, groupState, partitions[i], channels[i]);
					queryTask.RunAsynchronously(taskScheduler);
				}
			});
			groupState.QueryBegin(task);
			task.Start(taskScheduler);
		}

		// Token: 0x06000FFE RID: 4094 RVA: 0x000387FC File Offset: 0x000369FC
		internal static void SpoolForAll<TInputOutput, TIgnoreKey>(QueryTaskGroupState groupState, PartitionedStream<TInputOutput, TIgnoreKey> partitions, TaskScheduler taskScheduler)
		{
			Task task = new Task(delegate()
			{
				int num = partitions.PartitionCount - 1;
				for (int i = 0; i < num; i++)
				{
					QueryTask queryTask = new ForAllSpoolingTask<TInputOutput, TIgnoreKey>(i, groupState, partitions[i]);
					queryTask.RunAsynchronously(taskScheduler);
				}
				QueryTask queryTask2 = new ForAllSpoolingTask<TInputOutput, TIgnoreKey>(num, groupState, partitions[num]);
				queryTask2.RunSynchronously(taskScheduler);
			});
			groupState.QueryBegin(task);
			task.RunSynchronously(taskScheduler);
			groupState.QueryEnd(false);
		}
	}
}
