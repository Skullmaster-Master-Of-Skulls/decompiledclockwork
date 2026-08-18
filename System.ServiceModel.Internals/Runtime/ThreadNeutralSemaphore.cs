using System;
using System.Collections.Generic;
using System.Threading;

namespace System.Runtime
{
	// Token: 0x0200002C RID: 44
	internal class ThreadNeutralSemaphore
	{
		// Token: 0x06000165 RID: 357 RVA: 0x00006507 File Offset: 0x00004707
		public ThreadNeutralSemaphore(int maxCount) : this(maxCount, null)
		{
		}

		// Token: 0x06000166 RID: 358 RVA: 0x00006511 File Offset: 0x00004711
		public ThreadNeutralSemaphore(int maxCount, Func<Exception> abortedExceptionGenerator)
		{
			this.maxCount = maxCount;
			this.abortedExceptionGenerator = abortedExceptionGenerator;
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x06000167 RID: 359 RVA: 0x00006532 File Offset: 0x00004732
		private static Action<object, TimeoutException> EnteredAsyncCallback
		{
			get
			{
				if (ThreadNeutralSemaphore.enteredAsyncCallback == null)
				{
					ThreadNeutralSemaphore.enteredAsyncCallback = new Action<object, TimeoutException>(ThreadNeutralSemaphore.OnEnteredAsync);
				}
				return ThreadNeutralSemaphore.enteredAsyncCallback;
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x06000168 RID: 360 RVA: 0x00006551 File Offset: 0x00004751
		private Queue<AsyncWaitHandle> Waiters
		{
			get
			{
				if (this.waiters == null)
				{
					this.waiters = new Queue<AsyncWaitHandle>();
				}
				return this.waiters;
			}
		}

		// Token: 0x06000169 RID: 361 RVA: 0x0000656C File Offset: 0x0000476C
		public bool EnterAsync(TimeSpan timeout, FastAsyncCallback callback, object state)
		{
			AsyncWaitHandle asyncWaitHandle = null;
			object thisLock = this.ThisLock;
			lock (thisLock)
			{
				if (this.aborted)
				{
					throw Fx.Exception.AsError(this.CreateObjectAbortedException());
				}
				if (this.count < this.maxCount)
				{
					this.count++;
					return true;
				}
				asyncWaitHandle = new AsyncWaitHandle();
				this.Waiters.Enqueue(asyncWaitHandle);
			}
			return asyncWaitHandle.WaitAsync(ThreadNeutralSemaphore.EnteredAsyncCallback, new ThreadNeutralSemaphore.EnterAsyncData(this, asyncWaitHandle, callback, state), timeout);
		}

		// Token: 0x0600016A RID: 362 RVA: 0x0000660C File Offset: 0x0000480C
		private static void OnEnteredAsync(object state, TimeoutException exception)
		{
			ThreadNeutralSemaphore.EnterAsyncData enterAsyncData = (ThreadNeutralSemaphore.EnterAsyncData)state;
			ThreadNeutralSemaphore semaphore = enterAsyncData.Semaphore;
			Exception asyncException = exception;
			if (exception != null && !semaphore.RemoveWaiter(enterAsyncData.Waiter))
			{
				asyncException = null;
			}
			if (semaphore.aborted)
			{
				asyncException = semaphore.CreateObjectAbortedException();
			}
			enterAsyncData.Callback(enterAsyncData.State, asyncException);
		}

		// Token: 0x0600016B RID: 363 RVA: 0x00006660 File Offset: 0x00004860
		public bool TryEnter()
		{
			object thisLock = this.ThisLock;
			bool result;
			lock (thisLock)
			{
				if (this.count < this.maxCount)
				{
					this.count++;
					result = true;
				}
				else
				{
					result = false;
				}
			}
			return result;
		}

		// Token: 0x0600016C RID: 364 RVA: 0x000066C0 File Offset: 0x000048C0
		public void Enter(TimeSpan timeout)
		{
			if (!this.TryEnter(timeout))
			{
				throw Fx.Exception.AsError(ThreadNeutralSemaphore.CreateEnterTimedOutException(timeout));
			}
		}

		// Token: 0x0600016D RID: 365 RVA: 0x000066DC File Offset: 0x000048DC
		public bool TryEnter(TimeSpan timeout)
		{
			AsyncWaitHandle asyncWaitHandle = this.EnterCore();
			if (asyncWaitHandle == null)
			{
				return true;
			}
			bool flag = !asyncWaitHandle.Wait(timeout);
			if (this.aborted)
			{
				throw Fx.Exception.AsError(this.CreateObjectAbortedException());
			}
			if (flag && !this.RemoveWaiter(asyncWaitHandle))
			{
				flag = false;
			}
			return !flag;
		}

		// Token: 0x0600016E RID: 366 RVA: 0x0000672B File Offset: 0x0000492B
		internal static TimeoutException CreateEnterTimedOutException(TimeSpan timeout)
		{
			return new TimeoutException(InternalSR.LockTimeoutExceptionMessage(timeout));
		}

		// Token: 0x0600016F RID: 367 RVA: 0x0000673D File Offset: 0x0000493D
		private Exception CreateObjectAbortedException()
		{
			if (this.abortedExceptionGenerator != null)
			{
				return this.abortedExceptionGenerator();
			}
			return new OperationCanceledException(InternalSR.ThreadNeutralSemaphoreAborted);
		}

		// Token: 0x06000170 RID: 368 RVA: 0x00006760 File Offset: 0x00004960
		private bool RemoveWaiter(AsyncWaitHandle waiter)
		{
			bool result = false;
			object thisLock = this.ThisLock;
			lock (thisLock)
			{
				for (int i = this.Waiters.Count; i > 0; i--)
				{
					AsyncWaitHandle asyncWaitHandle = this.Waiters.Dequeue();
					if (asyncWaitHandle == waiter)
					{
						result = true;
					}
					else
					{
						this.Waiters.Enqueue(asyncWaitHandle);
					}
				}
			}
			return result;
		}

		// Token: 0x06000171 RID: 369 RVA: 0x000067D8 File Offset: 0x000049D8
		private AsyncWaitHandle EnterCore()
		{
			object thisLock = this.ThisLock;
			AsyncWaitHandle asyncWaitHandle;
			lock (thisLock)
			{
				if (this.aborted)
				{
					throw Fx.Exception.AsError(this.CreateObjectAbortedException());
				}
				if (this.count < this.maxCount)
				{
					this.count++;
					return null;
				}
				asyncWaitHandle = new AsyncWaitHandle();
				this.Waiters.Enqueue(asyncWaitHandle);
			}
			return asyncWaitHandle;
		}

		// Token: 0x06000172 RID: 370 RVA: 0x00006860 File Offset: 0x00004A60
		public int Exit()
		{
			int result = -1;
			object thisLock = this.ThisLock;
			AsyncWaitHandle asyncWaitHandle;
			lock (thisLock)
			{
				if (this.aborted)
				{
					return result;
				}
				if (this.count == 0)
				{
					string invalidSemaphoreExit = InternalSR.InvalidSemaphoreExit;
					throw Fx.Exception.AsError(new SynchronizationLockException(invalidSemaphoreExit));
				}
				if (this.waiters == null || this.waiters.Count == 0)
				{
					this.count--;
					return this.count;
				}
				asyncWaitHandle = this.waiters.Dequeue();
				result = this.count;
			}
			asyncWaitHandle.Set();
			return result;
		}

		// Token: 0x06000173 RID: 371 RVA: 0x00006914 File Offset: 0x00004B14
		public void Abort()
		{
			object thisLock = this.ThisLock;
			lock (thisLock)
			{
				if (!this.aborted)
				{
					this.aborted = true;
					if (this.waiters != null)
					{
						while (this.waiters.Count > 0)
						{
							AsyncWaitHandle asyncWaitHandle = this.waiters.Dequeue();
							asyncWaitHandle.Set();
						}
					}
				}
			}
		}

		// Token: 0x040000A3 RID: 163
		private static Action<object, TimeoutException> enteredAsyncCallback;

		// Token: 0x040000A4 RID: 164
		private bool aborted;

		// Token: 0x040000A5 RID: 165
		private Func<Exception> abortedExceptionGenerator;

		// Token: 0x040000A6 RID: 166
		private int count;

		// Token: 0x040000A7 RID: 167
		private int maxCount;

		// Token: 0x040000A8 RID: 168
		private object ThisLock = new object();

		// Token: 0x040000A9 RID: 169
		private Queue<AsyncWaitHandle> waiters;

		// Token: 0x02000087 RID: 135
		private class EnterAsyncData
		{
			// Token: 0x06000414 RID: 1044 RVA: 0x0001349E File Offset: 0x0001169E
			public EnterAsyncData(ThreadNeutralSemaphore semaphore, AsyncWaitHandle waiter, FastAsyncCallback callback, object state)
			{
				this.Waiter = waiter;
				this.Semaphore = semaphore;
				this.Callback = callback;
				this.State = state;
			}

			// Token: 0x170000B3 RID: 179
			// (get) Token: 0x06000415 RID: 1045 RVA: 0x000134C3 File Offset: 0x000116C3
			// (set) Token: 0x06000416 RID: 1046 RVA: 0x000134CB File Offset: 0x000116CB
			public ThreadNeutralSemaphore Semaphore { get; set; }

			// Token: 0x170000B4 RID: 180
			// (get) Token: 0x06000417 RID: 1047 RVA: 0x000134D4 File Offset: 0x000116D4
			// (set) Token: 0x06000418 RID: 1048 RVA: 0x000134DC File Offset: 0x000116DC
			public AsyncWaitHandle Waiter { get; set; }

			// Token: 0x170000B5 RID: 181
			// (get) Token: 0x06000419 RID: 1049 RVA: 0x000134E5 File Offset: 0x000116E5
			// (set) Token: 0x0600041A RID: 1050 RVA: 0x000134ED File Offset: 0x000116ED
			public FastAsyncCallback Callback { get; set; }

			// Token: 0x170000B6 RID: 182
			// (get) Token: 0x0600041B RID: 1051 RVA: 0x000134F6 File Offset: 0x000116F6
			// (set) Token: 0x0600041C RID: 1052 RVA: 0x000134FE File Offset: 0x000116FE
			public object State { get; set; }
		}
	}
}
