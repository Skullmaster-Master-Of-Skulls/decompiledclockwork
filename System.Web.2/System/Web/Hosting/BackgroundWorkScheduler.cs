using System;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Util;

namespace System.Web.Hosting
{
	// Token: 0x02000784 RID: 1924
	internal sealed class BackgroundWorkScheduler : IRegisteredObject
	{
		// Token: 0x06005C46 RID: 23622 RVA: 0x0013F5F2 File Offset: 0x0013D7F2
		internal BackgroundWorkScheduler(Action<BackgroundWorkScheduler> unregisterCallback, Action<AppDomain, Exception> logCallback, Action workItemCompleteCallback = null)
		{
			this._unregisterCallback = unregisterCallback;
			this._logCallback = logCallback;
			this._workItemCompleteCallback = workItemCompleteCallback;
		}

		// Token: 0x06005C47 RID: 23623 RVA: 0x0013F61B File Offset: 0x0013D81B
		private void FinalShutdown()
		{
			this._unregisterCallback(this);
		}

		// Token: 0x06005C48 RID: 23624 RVA: 0x0013F62C File Offset: 0x0013D82C
		private void RunWorkItemImpl(Func<CancellationToken, Task> workItem)
		{
			BackgroundWorkScheduler.<RunWorkItemImpl>d__7 <RunWorkItemImpl>d__;
			<RunWorkItemImpl>d__.<>t__builder = AsyncVoidMethodBuilder.Create();
			<RunWorkItemImpl>d__.<>4__this = this;
			<RunWorkItemImpl>d__.workItem = workItem;
			<RunWorkItemImpl>d__.<>1__state = -1;
			<RunWorkItemImpl>d__.<>t__builder.Start<BackgroundWorkScheduler.<RunWorkItemImpl>d__7>(ref <RunWorkItemImpl>d__);
		}

		// Token: 0x06005C49 RID: 23625 RVA: 0x0013F66B File Offset: 0x0013D86B
		public void ScheduleWorkItem(Func<CancellationToken, Task> workItem)
		{
			if (this._cancellationTokenHelper.IsCancellationRequested)
			{
				return;
			}
			ThreadPool.UnsafeQueueUserWorkItem(delegate(object state)
			{
				lock (this)
				{
					if (this._cancellationTokenHelper.IsCancellationRequested)
					{
						return;
					}
					this._numExecutingWorkItems++;
				}
				this.RunWorkItemImpl((Func<CancellationToken, Task>)state);
			}, workItem);
		}

		// Token: 0x06005C4A RID: 23626 RVA: 0x0013F690 File Offset: 0x0013D890
		public void Stop(bool immediate)
		{
			int numExecutingWorkItems;
			lock (this)
			{
				this._cancellationTokenHelper.Cancel();
				numExecutingWorkItems = this._numExecutingWorkItems;
			}
			if (numExecutingWorkItems == 0)
			{
				this.FinalShutdown();
			}
		}

		// Token: 0x06005C4B RID: 23627 RVA: 0x0013F6E0 File Offset: 0x0013D8E0
		private void WorkItemComplete()
		{
			int num2;
			bool isCancellationRequested;
			lock (this)
			{
				int num = this._numExecutingWorkItems - 1;
				this._numExecutingWorkItems = num;
				num2 = num;
				isCancellationRequested = this._cancellationTokenHelper.IsCancellationRequested;
			}
			if (this._workItemCompleteCallback != null)
			{
				this._workItemCompleteCallback();
			}
			if (num2 == 0 && isCancellationRequested)
			{
				this.FinalShutdown();
			}
		}

		// Token: 0x0400308D RID: 12429
		private readonly CancellationTokenHelper _cancellationTokenHelper = new CancellationTokenHelper(false);

		// Token: 0x0400308E RID: 12430
		private int _numExecutingWorkItems;

		// Token: 0x0400308F RID: 12431
		private readonly Action<BackgroundWorkScheduler> _unregisterCallback;

		// Token: 0x04003090 RID: 12432
		private readonly Action<AppDomain, Exception> _logCallback;

		// Token: 0x04003091 RID: 12433
		private readonly Action _workItemCompleteCallback;
	}
}
