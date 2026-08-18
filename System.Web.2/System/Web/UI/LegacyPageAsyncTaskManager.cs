using System;
using System.Collections;
using System.Threading;

namespace System.Web.UI
{
	// Token: 0x020002D7 RID: 727
	internal class LegacyPageAsyncTaskManager
	{
		// Token: 0x060021CD RID: 8653 RVA: 0x0006E4F0 File Offset: 0x0006C6F0
		internal LegacyPageAsyncTaskManager(Page page)
		{
			this._page = page;
			this._app = page.Context.ApplicationInstance;
			this._tasks = new ArrayList();
			this._resumeTasksCallback = new WaitCallback(this.ResumeTasksThreadpoolThread);
		}

		// Token: 0x17000972 RID: 2418
		// (get) Token: 0x060021CE RID: 8654 RVA: 0x0006E52D File Offset: 0x0006C72D
		internal HttpApplication Application
		{
			get
			{
				return this._app;
			}
		}

		// Token: 0x060021CF RID: 8655 RVA: 0x0006E535 File Offset: 0x0006C735
		internal void AddTask(LegacyPageAsyncTask task)
		{
			this._tasks.Add(task);
		}

		// Token: 0x17000973 RID: 2419
		// (get) Token: 0x060021D0 RID: 8656 RVA: 0x0006E544 File Offset: 0x0006C744
		internal bool AnyTasksRemain
		{
			get
			{
				for (int i = 0; i < this._tasks.Count; i++)
				{
					LegacyPageAsyncTask legacyPageAsyncTask = (LegacyPageAsyncTask)this._tasks[i];
					if (!legacyPageAsyncTask.Started)
					{
						return true;
					}
				}
				return false;
			}
		}

		// Token: 0x17000974 RID: 2420
		// (get) Token: 0x060021D1 RID: 8657 RVA: 0x0006E584 File Offset: 0x0006C784
		internal bool FailedToStartTasks
		{
			get
			{
				return this._failedToStart;
			}
		}

		// Token: 0x17000975 RID: 2421
		// (get) Token: 0x060021D2 RID: 8658 RVA: 0x0006E58C File Offset: 0x0006C78C
		internal bool TaskExecutionInProgress
		{
			get
			{
				return this._inProgress;
			}
		}

		// Token: 0x17000976 RID: 2422
		// (get) Token: 0x060021D3 RID: 8659 RVA: 0x0006E598 File Offset: 0x0006C798
		private Exception AnyTaskError
		{
			get
			{
				for (int i = 0; i < this._tasks.Count; i++)
				{
					LegacyPageAsyncTask legacyPageAsyncTask = (LegacyPageAsyncTask)this._tasks[i];
					if (legacyPageAsyncTask.Error != null)
					{
						return legacyPageAsyncTask.Error;
					}
				}
				return null;
			}
		}

		// Token: 0x17000977 RID: 2423
		// (get) Token: 0x060021D4 RID: 8660 RVA: 0x0006E5DD File Offset: 0x0006C7DD
		private bool TimeoutEndReached
		{
			get
			{
				if (!this._timeoutEndReached && DateTime.UtcNow >= this._timeoutEnd)
				{
					this._timeoutEndReached = true;
				}
				return this._timeoutEndReached;
			}
		}

		// Token: 0x060021D5 RID: 8661 RVA: 0x0006E60C File Offset: 0x0006C80C
		private void WaitForAllStartedTasks(bool syncCaller, bool forceTimeout)
		{
			for (int i = 0; i < this._tasks.Count; i++)
			{
				LegacyPageAsyncTask legacyPageAsyncTask = (LegacyPageAsyncTask)this._tasks[i];
				if (legacyPageAsyncTask.Started && !legacyPageAsyncTask.Completed)
				{
					if (!forceTimeout && !this.TimeoutEndReached)
					{
						DateTime utcNow = DateTime.UtcNow;
						if (utcNow < this._timeoutEnd)
						{
							WaitHandle asyncWaitHandle = legacyPageAsyncTask.AsyncResult.AsyncWaitHandle;
							if (asyncWaitHandle != null)
							{
								bool flag = asyncWaitHandle.WaitOne(this._timeoutEnd - utcNow, false);
								if (flag && legacyPageAsyncTask.Completed)
								{
									goto IL_AA;
								}
							}
						}
					}
					bool flag2 = false;
					while (!legacyPageAsyncTask.Completed)
					{
						if (forceTimeout || (!flag2 && this.TimeoutEndReached))
						{
							legacyPageAsyncTask.ForceTimeout(syncCaller);
							flag2 = true;
						}
						else
						{
							Thread.Sleep(50);
						}
					}
				}
				IL_AA:;
			}
		}

		// Token: 0x060021D6 RID: 8662 RVA: 0x0006E6D8 File Offset: 0x0006C8D8
		internal void RegisterHandlersForPagePreRenderCompleteAsync()
		{
			this._page.AddOnPreRenderCompleteAsync(new BeginEventHandler(this.BeginExecuteAsyncTasks), new EndEventHandler(this.EndExecuteAsyncTasks));
		}

		// Token: 0x060021D7 RID: 8663 RVA: 0x0006E6FD File Offset: 0x0006C8FD
		private IAsyncResult BeginExecuteAsyncTasks(object sender, EventArgs e, AsyncCallback cb, object extraData)
		{
			return this.ExecuteTasks(cb, extraData);
		}

		// Token: 0x060021D8 RID: 8664 RVA: 0x0006E708 File Offset: 0x0006C908
		private void EndExecuteAsyncTasks(IAsyncResult ar)
		{
			this._asyncResult.End();
		}

		// Token: 0x060021D9 RID: 8665 RVA: 0x0006E718 File Offset: 0x0006C918
		internal HttpAsyncResult ExecuteTasks(AsyncCallback callback, object extraData)
		{
			this._failedToStart = false;
			this._timeoutEnd = DateTime.UtcNow + this._page.AsyncTimeout;
			this._timeoutEndReached = false;
			this._tasksStarted = 0;
			this._tasksCompleted = 0;
			this._asyncResult = new HttpAsyncResult(callback, extraData);
			bool flag = callback == null;
			if (flag)
			{
				try
				{
				}
				finally
				{
					try
					{
						this._app.Context.SyncContext.DisassociateFromCurrentThread();
						this._app.Context.SyncContext.AssociateWithCurrentThread();
					}
					catch (SynchronizationLockException)
					{
						this._failedToStart = true;
						throw new InvalidOperationException(SR.GetString("Async_tasks_wrong_thread"));
					}
				}
			}
			this._inProgress = true;
			try
			{
				this.ResumeTasks(flag, true);
			}
			finally
			{
				if (flag)
				{
					this._inProgress = false;
				}
			}
			return this._asyncResult;
		}

		// Token: 0x060021DA RID: 8666 RVA: 0x0006E80C File Offset: 0x0006CA0C
		private void ResumeTasks(bool waitUntilDone, bool onCallerThread)
		{
			Interlocked.Increment(ref this._tasksStarted);
			try
			{
				if (onCallerThread)
				{
					this.ResumeTasksPossiblyUnderLock(waitUntilDone);
				}
				else
				{
					using (this._app.Context.SyncContext.AcquireThreadLock())
					{
						ThreadContext threadContext = null;
						try
						{
							threadContext = this._app.OnThreadEnter();
							this.ResumeTasksPossiblyUnderLock(waitUntilDone);
						}
						finally
						{
							if (threadContext != null)
							{
								threadContext.DisassociateFromCurrentThread();
							}
						}
					}
				}
			}
			finally
			{
				this.TaskCompleted(onCallerThread);
			}
		}

		// Token: 0x060021DB RID: 8667 RVA: 0x0006E8A4 File Offset: 0x0006CAA4
		private void ResumeTasksPossiblyUnderLock(bool waitUntilDone)
		{
			while (this.AnyTasksRemain)
			{
				bool flag = false;
				bool flag2 = false;
				bool flag3 = false;
				for (int i = 0; i < this._tasks.Count; i++)
				{
					LegacyPageAsyncTask legacyPageAsyncTask = (LegacyPageAsyncTask)this._tasks[i];
					if (!legacyPageAsyncTask.Started && (!flag3 || legacyPageAsyncTask.ExecuteInParallel))
					{
						flag = true;
						Interlocked.Increment(ref this._tasksStarted);
						legacyPageAsyncTask.Start(this, this._page, EventArgs.Empty);
						if (!legacyPageAsyncTask.CompletedSynchronously)
						{
							flag2 = true;
							if (!legacyPageAsyncTask.ExecuteInParallel)
							{
								break;
							}
							flag3 = true;
						}
					}
				}
				if (!flag)
				{
					break;
				}
				if (!this.TimeoutEndReached && flag2 && !waitUntilDone)
				{
					this.StartTimerIfNeeeded();
					return;
				}
				bool flag4 = true;
				try
				{
					try
					{
					}
					finally
					{
						this._app.Context.SyncContext.DisassociateFromCurrentThread();
						flag4 = false;
					}
					this.WaitForAllStartedTasks(true, false);
				}
				finally
				{
					if (!flag4)
					{
						this._app.Context.SyncContext.AssociateWithCurrentThread();
					}
				}
			}
		}

		// Token: 0x060021DC RID: 8668 RVA: 0x0006E9B8 File Offset: 0x0006CBB8
		private void ResumeTasksThreadpoolThread(object data)
		{
			this.ResumeTasks(false, false);
		}

		// Token: 0x060021DD RID: 8669 RVA: 0x0006E9C4 File Offset: 0x0006CBC4
		internal void TaskCompleted(bool onCallerThread)
		{
			int num = Interlocked.Increment(ref this._tasksCompleted);
			if (num < this._tasksStarted)
			{
				return;
			}
			if (!this.AnyTasksRemain)
			{
				this._inProgress = false;
				this._asyncResult.Complete(onCallerThread, null, this.AnyTaskError);
				return;
			}
			if (Thread.CurrentThread.IsThreadPoolThread)
			{
				this.ResumeTasks(false, onCallerThread);
				return;
			}
			ThreadPool.QueueUserWorkItem(this._resumeTasksCallback);
		}

		// Token: 0x060021DE RID: 8670 RVA: 0x0006EA30 File Offset: 0x0006CC30
		private void StartTimerIfNeeeded()
		{
			if (this._timeoutTimer != null)
			{
				return;
			}
			DateTime utcNow = DateTime.UtcNow;
			if (utcNow >= this._timeoutEnd)
			{
				return;
			}
			double totalMilliseconds = (this._timeoutEnd - utcNow).TotalMilliseconds;
			if (totalMilliseconds >= 2147483647.0)
			{
				return;
			}
			this._timeoutTimer = new Timer(new TimerCallback(this.TimeoutTimerCallback), null, (int)totalMilliseconds, -1);
		}

		// Token: 0x060021DF RID: 8671 RVA: 0x0006EA98 File Offset: 0x0006CC98
		internal void DisposeTimer()
		{
			Timer timeoutTimer = this._timeoutTimer;
			if (timeoutTimer != null && Interlocked.CompareExchange<Timer>(ref this._timeoutTimer, null, timeoutTimer) == timeoutTimer)
			{
				timeoutTimer.Dispose();
			}
		}

		// Token: 0x060021E0 RID: 8672 RVA: 0x0006EAC5 File Offset: 0x0006CCC5
		private void TimeoutTimerCallback(object state)
		{
			this.DisposeTimer();
			this.WaitForAllStartedTasks(false, false);
		}

		// Token: 0x060021E1 RID: 8673 RVA: 0x0006EAD5 File Offset: 0x0006CCD5
		internal void CompleteAllTasksNow(bool syncCaller)
		{
			this.WaitForAllStartedTasks(syncCaller, true);
		}

		// Token: 0x04001BF1 RID: 7153
		private Page _page;

		// Token: 0x04001BF2 RID: 7154
		private HttpApplication _app;

		// Token: 0x04001BF3 RID: 7155
		private HttpAsyncResult _asyncResult;

		// Token: 0x04001BF4 RID: 7156
		private bool _failedToStart;

		// Token: 0x04001BF5 RID: 7157
		private ArrayList _tasks;

		// Token: 0x04001BF6 RID: 7158
		private DateTime _timeoutEnd;

		// Token: 0x04001BF7 RID: 7159
		private volatile bool _timeoutEndReached;

		// Token: 0x04001BF8 RID: 7160
		private volatile bool _inProgress;

		// Token: 0x04001BF9 RID: 7161
		private int _tasksStarted;

		// Token: 0x04001BFA RID: 7162
		private int _tasksCompleted;

		// Token: 0x04001BFB RID: 7163
		private WaitCallback _resumeTasksCallback;

		// Token: 0x04001BFC RID: 7164
		private Timer _timeoutTimer;
	}
}
