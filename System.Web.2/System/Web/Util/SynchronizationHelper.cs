using System;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;
using System.Threading;
using System.Threading.Tasks;

namespace System.Web.Util
{
	// Token: 0x020001E2 RID: 482
	internal sealed class SynchronizationHelper
	{
		// Token: 0x060017B3 RID: 6067 RVA: 0x0004A4A4 File Offset: 0x000486A4
		public SynchronizationHelper(ISyncContext syncContext)
		{
			this._syncContext = syncContext;
			this._appVerifierCallback = AppVerifier.GetSyncContextCheckDelegate(syncContext);
		}

		// Token: 0x1700070D RID: 1805
		// (get) Token: 0x060017B4 RID: 6068 RVA: 0x0004A4E0 File Offset: 0x000486E0
		// (set) Token: 0x060017B5 RID: 6069 RVA: 0x0004A4E8 File Offset: 0x000486E8
		public ExceptionDispatchInfo Error { get; set; }

		// Token: 0x1700070E RID: 1806
		// (get) Token: 0x060017B6 RID: 6070 RVA: 0x0004A4F1 File Offset: 0x000486F1
		// (set) Token: 0x060017B7 RID: 6071 RVA: 0x0004A500 File Offset: 0x00048700
		private Thread CurrentThread
		{
			get
			{
				return Interlocked.CompareExchange<Thread>(ref this._currentThread, null, null);
			}
			set
			{
				Interlocked.Exchange<Thread>(ref this._currentThread, value);
			}
		}

		// Token: 0x1700070F RID: 1807
		// (get) Token: 0x060017B8 RID: 6072 RVA: 0x0004A50F File Offset: 0x0004870F
		public int PendingCount
		{
			get
			{
				return this.ChangeOperationCount(0);
			}
		}

		// Token: 0x060017B9 RID: 6073 RVA: 0x0004A518 File Offset: 0x00048718
		public int ChangeOperationCount(int addend)
		{
			int num = Interlocked.Add(ref this._operationsInFlight, addend);
			if (num == 0)
			{
				Task task = Interlocked.Exchange<Task>(ref this._completionTask, null);
				if (task != null)
				{
					task.Start();
				}
			}
			return num;
		}

		// Token: 0x060017BA RID: 6074 RVA: 0x0004A54C File Offset: 0x0004874C
		private void CheckForRequestStateIfRequired(bool checkForReEntry)
		{
			if (this._appVerifierCallback != null)
			{
				this._appVerifierCallback(checkForReEntry);
			}
		}

		// Token: 0x060017BB RID: 6075 RVA: 0x0004A562 File Offset: 0x00048762
		private static Task CreateInitialTask()
		{
			return Task.FromResult<object>(null);
		}

		// Token: 0x060017BC RID: 6076 RVA: 0x0004A56C File Offset: 0x0004876C
		public IDisposable EnterSynchronousControl()
		{
			if (this.CurrentThread == Thread.CurrentThread)
			{
				return DisposableAction.Empty;
			}
			TaskCompletionSource<object> tcs = new TaskCompletionSource<object>();
			object lockObj = this._lockObj;
			Task lastScheduledTask;
			lock (lockObj)
			{
				lastScheduledTask = this._lastScheduledTask;
				this._lastScheduledTask = tcs.Task;
			}
			if (!lastScheduledTask.IsCompleted)
			{
				lastScheduledTask.ContinueWith(delegate(Task _)
				{
				}, TaskContinuationOptions.ExecuteSynchronously).Wait();
			}
			this.CurrentThread = Thread.CurrentThread;
			return new DisposableAction(delegate()
			{
				this.CurrentThread = null;
				tcs.TrySetResult(null);
			});
		}

		// Token: 0x060017BD RID: 6077 RVA: 0x0004A63C File Offset: 0x0004883C
		public void QueueAsynchronous(Action action)
		{
			this.CheckForRequestStateIfRequired(true);
			this.ChangeOperationCount(1);
			object lockObj = this._lockObj;
			lock (lockObj)
			{
				Task lastScheduledTask = this._lastScheduledTask.ContinueWith(delegate(Task _)
				{
					this.SafeWrapCallback(action);
				}, TaskScheduler.Default);
				this._lastScheduledTask = lastScheduledTask;
			}
		}

		// Token: 0x060017BE RID: 6078 RVA: 0x0004A6C0 File Offset: 0x000488C0
		public void QueueAsynchronousAsync(Func<object, Task> func, object state)
		{
			SynchronizationHelper.<>c__DisplayClass23_0 CS$<>8__locals1 = new SynchronizationHelper.<>c__DisplayClass23_0();
			CS$<>8__locals1.<>4__this = this;
			CS$<>8__locals1.func = func;
			CS$<>8__locals1.state = state;
			this.CheckForRequestStateIfRequired(true);
			this.ChangeOperationCount(1);
			object lockObj = this._lockObj;
			lock (lockObj)
			{
				Task lastScheduledTaskAsync = this._lastScheduledTaskAsync.ContinueWith<Task>(delegate(Task _)
				{
					SynchronizationHelper.<>c__DisplayClass23_0.<<QueueAsynchronousAsync>b__0>d <<QueueAsynchronousAsync>b__0>d;
					<<QueueAsynchronousAsync>b__0>d.<>t__builder = AsyncTaskMethodBuilder.Create();
					<<QueueAsynchronousAsync>b__0>d.<>4__this = CS$<>8__locals1;
					<<QueueAsynchronousAsync>b__0>d.<>1__state = -1;
					<<QueueAsynchronousAsync>b__0>d.<>t__builder.Start<SynchronizationHelper.<>c__DisplayClass23_0.<<QueueAsynchronousAsync>b__0>d>(ref <<QueueAsynchronousAsync>b__0>d);
					return <<QueueAsynchronousAsync>b__0>d.<>t__builder.Task;
				}).Unwrap();
				this._lastScheduledTaskAsync = lastScheduledTaskAsync;
			}
		}

		// Token: 0x060017BF RID: 6079 RVA: 0x0004A748 File Offset: 0x00048948
		public void QueueSynchronous(Action action)
		{
			this.CheckForRequestStateIfRequired(false);
			if (this.CurrentThread == Thread.CurrentThread)
			{
				action();
				return;
			}
			this.ChangeOperationCount(1);
			using (this.EnterSynchronousControl())
			{
				this.SafeWrapCallback(action);
			}
		}

		// Token: 0x060017C0 RID: 6080 RVA: 0x0004A7A4 File Offset: 0x000489A4
		private void SafeWrapCallback(Action action)
		{
			try
			{
				this.CurrentThread = Thread.CurrentThread;
				ISyncContextLock syncContextLock = null;
				try
				{
					syncContextLock = ((this._syncContext != null) ? this._syncContext.Enter() : null);
					try
					{
						action();
					}
					catch (Exception source)
					{
						this.Error = ExceptionDispatchInfo.Capture(source);
					}
				}
				finally
				{
					if (syncContextLock != null)
					{
						syncContextLock.Leave();
					}
				}
			}
			finally
			{
				this.CurrentThread = null;
				this.ChangeOperationCount(-1);
			}
		}

		// Token: 0x060017C1 RID: 6081 RVA: 0x0004A830 File Offset: 0x00048A30
		private Task SafeWrapCallbackAsync(Func<object, Task> func, object state)
		{
			SynchronizationHelper.<SafeWrapCallbackAsync>d__26 <SafeWrapCallbackAsync>d__;
			<SafeWrapCallbackAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<SafeWrapCallbackAsync>d__.<>4__this = this;
			<SafeWrapCallbackAsync>d__.func = func;
			<SafeWrapCallbackAsync>d__.state = state;
			<SafeWrapCallbackAsync>d__.<>1__state = -1;
			<SafeWrapCallbackAsync>d__.<>t__builder.Start<SynchronizationHelper.<SafeWrapCallbackAsync>d__26>(ref <SafeWrapCallbackAsync>d__);
			return <SafeWrapCallbackAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060017C2 RID: 6082 RVA: 0x0004A884 File Offset: 0x00048A84
		public bool TrySetCompletionContinuation(Action continuation)
		{
			int num = this.ChangeOperationCount(1);
			bool flag = num > 1;
			if (flag)
			{
				Interlocked.Exchange<Task>(ref this._completionTask, new Task(continuation));
			}
			this.ChangeOperationCount(-1);
			return flag;
		}

		// Token: 0x04001727 RID: 5927
		private Task _completionTask;

		// Token: 0x04001728 RID: 5928
		private Thread _currentThread;

		// Token: 0x04001729 RID: 5929
		private Task _lastScheduledTask = SynchronizationHelper.CreateInitialTask();

		// Token: 0x0400172A RID: 5930
		private Task _lastScheduledTaskAsync = SynchronizationHelper.CreateInitialTask();

		// Token: 0x0400172B RID: 5931
		private readonly object _lockObj = new object();

		// Token: 0x0400172C RID: 5932
		private int _operationsInFlight;

		// Token: 0x0400172D RID: 5933
		private readonly ISyncContext _syncContext;

		// Token: 0x0400172E RID: 5934
		private readonly Action<bool> _appVerifierCallback;
	}
}
