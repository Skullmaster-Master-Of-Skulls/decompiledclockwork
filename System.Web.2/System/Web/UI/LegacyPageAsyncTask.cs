using System;
using System.Threading;

namespace System.Web.UI
{
	// Token: 0x020002D6 RID: 726
	internal sealed class LegacyPageAsyncTask
	{
		// Token: 0x060021BD RID: 8637 RVA: 0x0006E1E5 File Offset: 0x0006C3E5
		internal LegacyPageAsyncTask(BeginEventHandler beginHandler, EndEventHandler endHandler, EndEventHandler timeoutHandler, object state, bool executeInParallel)
		{
			this._beginHandler = beginHandler;
			this._endHandler = endHandler;
			this._timeoutHandler = timeoutHandler;
			this._state = state;
			this._executeInParallel = executeInParallel;
		}

		// Token: 0x17000968 RID: 2408
		// (get) Token: 0x060021BE RID: 8638 RVA: 0x0006E212 File Offset: 0x0006C412
		public BeginEventHandler BeginHandler
		{
			get
			{
				return this._beginHandler;
			}
		}

		// Token: 0x17000969 RID: 2409
		// (get) Token: 0x060021BF RID: 8639 RVA: 0x0006E21A File Offset: 0x0006C41A
		public EndEventHandler EndHandler
		{
			get
			{
				return this._endHandler;
			}
		}

		// Token: 0x1700096A RID: 2410
		// (get) Token: 0x060021C0 RID: 8640 RVA: 0x0006E222 File Offset: 0x0006C422
		public EndEventHandler TimeoutHandler
		{
			get
			{
				return this._timeoutHandler;
			}
		}

		// Token: 0x1700096B RID: 2411
		// (get) Token: 0x060021C1 RID: 8641 RVA: 0x0006E22A File Offset: 0x0006C42A
		public object State
		{
			get
			{
				return this._state;
			}
		}

		// Token: 0x1700096C RID: 2412
		// (get) Token: 0x060021C2 RID: 8642 RVA: 0x0006E232 File Offset: 0x0006C432
		public bool ExecuteInParallel
		{
			get
			{
				return this._executeInParallel;
			}
		}

		// Token: 0x1700096D RID: 2413
		// (get) Token: 0x060021C3 RID: 8643 RVA: 0x0006E23A File Offset: 0x0006C43A
		internal bool Started
		{
			get
			{
				return this._started;
			}
		}

		// Token: 0x1700096E RID: 2414
		// (get) Token: 0x060021C4 RID: 8644 RVA: 0x0006E242 File Offset: 0x0006C442
		internal bool CompletedSynchronously
		{
			get
			{
				return this._completedSynchronously;
			}
		}

		// Token: 0x1700096F RID: 2415
		// (get) Token: 0x060021C5 RID: 8645 RVA: 0x0006E24A File Offset: 0x0006C44A
		internal bool Completed
		{
			get
			{
				return this._completed;
			}
		}

		// Token: 0x17000970 RID: 2416
		// (get) Token: 0x060021C6 RID: 8646 RVA: 0x0006E252 File Offset: 0x0006C452
		internal IAsyncResult AsyncResult
		{
			get
			{
				return this._asyncResult;
			}
		}

		// Token: 0x17000971 RID: 2417
		// (get) Token: 0x060021C7 RID: 8647 RVA: 0x0006E25A File Offset: 0x0006C45A
		internal Exception Error
		{
			get
			{
				return this._error;
			}
		}

		// Token: 0x060021C8 RID: 8648 RVA: 0x0006E264 File Offset: 0x0006C464
		internal void Start(LegacyPageAsyncTaskManager manager, object source, EventArgs args)
		{
			this._taskManager = manager;
			this._completionCallback = new AsyncCallback(this.OnAsyncTaskCompletion);
			this._started = true;
			try
			{
				IAsyncResult asyncResult = this._beginHandler(source, args, this._completionCallback, this._state);
				if (asyncResult == null)
				{
					throw new InvalidOperationException(SR.GetString("Async_null_asyncresult"));
				}
				if (this._asyncResult == null)
				{
					this._asyncResult = asyncResult;
				}
			}
			catch (Exception error)
			{
				this._error = error;
				this._completed = true;
				this._completedSynchronously = true;
				this._taskManager.TaskCompleted(true);
			}
		}

		// Token: 0x060021C9 RID: 8649 RVA: 0x0006E304 File Offset: 0x0006C504
		private void OnAsyncTaskCompletion(IAsyncResult ar)
		{
			if (this._asyncResult == null)
			{
				this._asyncResult = ar;
			}
			this.CompleteTask(false);
		}

		// Token: 0x060021CA RID: 8650 RVA: 0x0006E31C File Offset: 0x0006C51C
		internal void ForceTimeout(bool syncCaller)
		{
			this.CompleteTask(true, syncCaller);
		}

		// Token: 0x060021CB RID: 8651 RVA: 0x0006E326 File Offset: 0x0006C526
		private void CompleteTask(bool timedOut)
		{
			this.CompleteTask(timedOut, false);
		}

		// Token: 0x060021CC RID: 8652 RVA: 0x0006E330 File Offset: 0x0006C530
		private void CompleteTask(bool timedOut, bool syncTimeoutCaller)
		{
			if (Interlocked.Exchange(ref this._completionMethodLock, 1) != 0)
			{
				return;
			}
			bool flag = false;
			bool flag2;
			if (timedOut)
			{
				flag2 = !syncTimeoutCaller;
			}
			else
			{
				this._completedSynchronously = this._asyncResult.CompletedSynchronously;
				flag2 = !this._completedSynchronously;
			}
			HttpApplication application = this._taskManager.Application;
			try
			{
				if (flag2)
				{
					using (application.Context.SyncContext.AcquireThreadLock())
					{
						ThreadContext threadContext = null;
						try
						{
							threadContext = application.OnThreadEnter();
							if (timedOut)
							{
								if (this._timeoutHandler != null)
								{
									this._timeoutHandler(this._asyncResult);
								}
							}
							else
							{
								this._endHandler(this._asyncResult);
							}
						}
						finally
						{
							if (threadContext != null)
							{
								threadContext.DisassociateFromCurrentThread();
							}
						}
						goto IL_D9;
					}
				}
				if (timedOut)
				{
					if (this._timeoutHandler != null)
					{
						this._timeoutHandler(this._asyncResult);
					}
				}
				else
				{
					this._endHandler(this._asyncResult);
				}
				IL_D9:;
			}
			catch (ThreadAbortException ex)
			{
				this._error = ex;
				HttpApplication.CancelModuleException ex2 = ex.ExceptionState as HttpApplication.CancelModuleException;
				if (ex2 != null && !ex2.Timeout)
				{
					using (application.Context.SyncContext.AcquireThreadLock())
					{
						if (!application.IsRequestCompleted)
						{
							flag = true;
							application.CompleteRequest();
						}
					}
					this._error = null;
				}
				Thread.ResetAbort();
			}
			catch (Exception error)
			{
				this._error = error;
			}
			this._completed = true;
			this._taskManager.TaskCompleted(this._completedSynchronously);
			if (flag)
			{
				this._taskManager.CompleteAllTasksNow(false);
			}
		}

		// Token: 0x04001BE4 RID: 7140
		private BeginEventHandler _beginHandler;

		// Token: 0x04001BE5 RID: 7141
		private EndEventHandler _endHandler;

		// Token: 0x04001BE6 RID: 7142
		private EndEventHandler _timeoutHandler;

		// Token: 0x04001BE7 RID: 7143
		private object _state;

		// Token: 0x04001BE8 RID: 7144
		private bool _executeInParallel;

		// Token: 0x04001BE9 RID: 7145
		private LegacyPageAsyncTaskManager _taskManager;

		// Token: 0x04001BEA RID: 7146
		private int _completionMethodLock;

		// Token: 0x04001BEB RID: 7147
		private bool _started;

		// Token: 0x04001BEC RID: 7148
		private bool _completed;

		// Token: 0x04001BED RID: 7149
		private bool _completedSynchronously;

		// Token: 0x04001BEE RID: 7150
		private AsyncCallback _completionCallback;

		// Token: 0x04001BEF RID: 7151
		private IAsyncResult _asyncResult;

		// Token: 0x04001BF0 RID: 7152
		private Exception _error;
	}
}
