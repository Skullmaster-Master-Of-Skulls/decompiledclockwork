using System;
using System.Threading;
using System.Threading.Tasks;

namespace System.Linq.Parallel
{
	// Token: 0x020001EE RID: 494
	internal abstract class QueryTask
	{
		// Token: 0x06000FEC RID: 4076 RVA: 0x0003847B File Offset: 0x0003667B
		protected QueryTask(int taskIndex, QueryTaskGroupState groupState)
		{
			this.m_taskIndex = taskIndex;
			this.m_groupState = groupState;
		}

		// Token: 0x06000FED RID: 4077 RVA: 0x00038491 File Offset: 0x00036691
		private static void RunTaskSynchronously(object o)
		{
			((QueryTask)o).BaseWork(null);
		}

		// Token: 0x06000FEE RID: 4078 RVA: 0x000384A0 File Offset: 0x000366A0
		internal Task RunSynchronously(TaskScheduler taskScheduler)
		{
			Task task = new Task(QueryTask.s_runTaskSynchronouslyDelegate, this, TaskCreationOptions.AttachedToParent);
			task.RunSynchronously(taskScheduler);
			return task;
		}

		// Token: 0x06000FEF RID: 4079 RVA: 0x000384C4 File Offset: 0x000366C4
		internal Task RunAsynchronously(TaskScheduler taskScheduler)
		{
			return Task.Factory.StartNew(QueryTask.s_baseWorkDelegate, this, default(CancellationToken), TaskCreationOptions.PreferFairness | TaskCreationOptions.AttachedToParent, taskScheduler);
		}

		// Token: 0x06000FF0 RID: 4080 RVA: 0x000384EC File Offset: 0x000366EC
		private void BaseWork(object unused)
		{
			PlinqEtwProvider.Log.ParallelQueryFork(this.m_groupState.QueryId);
			try
			{
				this.Work();
			}
			finally
			{
				PlinqEtwProvider.Log.ParallelQueryJoin(this.m_groupState.QueryId);
			}
		}

		// Token: 0x06000FF1 RID: 4081
		protected abstract void Work();

		// Token: 0x04000912 RID: 2322
		protected int m_taskIndex;

		// Token: 0x04000913 RID: 2323
		protected QueryTaskGroupState m_groupState;

		// Token: 0x04000914 RID: 2324
		private static Action<object> s_runTaskSynchronouslyDelegate = new Action<object>(QueryTask.RunTaskSynchronously);

		// Token: 0x04000915 RID: 2325
		private static Action<object> s_baseWorkDelegate = delegate(object o)
		{
			((QueryTask)o).BaseWork(null);
		};
	}
}
