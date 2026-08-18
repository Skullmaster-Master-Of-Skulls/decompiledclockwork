using System;
using System.Threading.Tasks;

namespace System.Linq.Parallel
{
	// Token: 0x020001EB RID: 491
	internal class OrderPreservingSpoolingTask<TInputOutput, TKey> : SpoolingTaskBase
	{
		// Token: 0x06000FE3 RID: 4067 RVA: 0x0003805D File Offset: 0x0003625D
		private OrderPreservingSpoolingTask(int taskIndex, QueryTaskGroupState groupState, Shared<TInputOutput[]> results, SortHelper<TInputOutput> sortHelper) : base(taskIndex, groupState)
		{
			this.m_results = results;
			this.m_sortHelper = sortHelper;
		}

		// Token: 0x06000FE4 RID: 4068 RVA: 0x00038078 File Offset: 0x00036278
		internal static void Spool(QueryTaskGroupState groupState, PartitionedStream<TInputOutput, TKey> partitions, Shared<TInputOutput[]> results, TaskScheduler taskScheduler)
		{
			int maxToRunInParallel = partitions.PartitionCount - 1;
			SortHelper<TInputOutput, TKey>[] sortHelpers = SortHelper<TInputOutput, TKey>.GenerateSortHelpers(partitions, groupState);
			Task task = new Task(delegate()
			{
				for (int j = 0; j < maxToRunInParallel; j++)
				{
					QueryTask queryTask = new OrderPreservingSpoolingTask<TInputOutput, TKey>(j, groupState, results, sortHelpers[j]);
					queryTask.RunAsynchronously(taskScheduler);
				}
				QueryTask queryTask2 = new OrderPreservingSpoolingTask<TInputOutput, TKey>(maxToRunInParallel, groupState, results, sortHelpers[maxToRunInParallel]);
				queryTask2.RunSynchronously(taskScheduler);
			});
			groupState.QueryBegin(task);
			task.RunSynchronously(taskScheduler);
			for (int i = 0; i < sortHelpers.Length; i++)
			{
				sortHelpers[i].Dispose();
			}
			groupState.QueryEnd(false);
		}

		// Token: 0x06000FE5 RID: 4069 RVA: 0x00038118 File Offset: 0x00036318
		protected override void SpoolingWork()
		{
			TInputOutput[] value = this.m_sortHelper.Sort();
			if (!this.m_groupState.CancellationState.MergedCancellationToken.IsCancellationRequested && this.m_taskIndex == 0)
			{
				this.m_results.Value = value;
			}
		}

		// Token: 0x04000905 RID: 2309
		private Shared<TInputOutput[]> m_results;

		// Token: 0x04000906 RID: 2310
		private SortHelper<TInputOutput> m_sortHelper;
	}
}
