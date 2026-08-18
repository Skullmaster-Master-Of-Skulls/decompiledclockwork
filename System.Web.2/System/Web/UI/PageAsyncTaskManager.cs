using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace System.Web.UI
{
	// Token: 0x0200023B RID: 571
	internal sealed class PageAsyncTaskManager
	{
		// Token: 0x06001AC2 RID: 6850 RVA: 0x00053F3B File Offset: 0x0005213B
		public void EnqueueTask(IPageAsyncTask task)
		{
			if (this._executeTasksAsyncHasCompleted)
			{
				throw new InvalidOperationException(SR.GetString("PageAsyncManager_CannotEnqueue"));
			}
			this._registeredTasks.Enqueue(task);
		}

		// Token: 0x06001AC3 RID: 6851 RVA: 0x00053F64 File Offset: 0x00052164
		public Task ExecuteTasksAsync(object sender, EventArgs e, CancellationToken cancellationToken, AspNetSynchronizationContextBase syncContext, IRequestCompletedNotifier requestCompletedNotifier)
		{
			PageAsyncTaskManager.<ExecuteTasksAsync>d__3 <ExecuteTasksAsync>d__;
			<ExecuteTasksAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<ExecuteTasksAsync>d__.<>4__this = this;
			<ExecuteTasksAsync>d__.sender = sender;
			<ExecuteTasksAsync>d__.e = e;
			<ExecuteTasksAsync>d__.cancellationToken = cancellationToken;
			<ExecuteTasksAsync>d__.syncContext = syncContext;
			<ExecuteTasksAsync>d__.requestCompletedNotifier = requestCompletedNotifier;
			<ExecuteTasksAsync>d__.<>1__state = -1;
			<ExecuteTasksAsync>d__.<>t__builder.Start<PageAsyncTaskManager.<ExecuteTasksAsync>d__3>(ref <ExecuteTasksAsync>d__);
			return <ExecuteTasksAsync>d__.<>t__builder.Task;
		}

		// Token: 0x04001858 RID: 6232
		private bool _executeTasksAsyncHasCompleted;

		// Token: 0x04001859 RID: 6233
		private readonly Queue<IPageAsyncTask> _registeredTasks = new Queue<IPageAsyncTask>();
	}
}
