using System;
using System.Collections.Generic;
using System.Security;
using System.Threading;

namespace System.Runtime
{
	// Token: 0x0200000C RID: 12
	internal class AsyncWaitHandle
	{
		// Token: 0x06000046 RID: 70 RVA: 0x00002BA4 File Offset: 0x00000DA4
		public AsyncWaitHandle() : this(EventResetMode.AutoReset)
		{
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00002BAD File Offset: 0x00000DAD
		public AsyncWaitHandle(EventResetMode resetMode)
		{
			this.resetMode = resetMode;
			this.syncObject = new object();
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00002BC8 File Offset: 0x00000DC8
		public bool WaitAsync(Action<object, TimeoutException> callback, object state, TimeSpan timeout)
		{
			if (!this.isSignaled || (this.isSignaled && this.resetMode == EventResetMode.AutoReset))
			{
				object obj = this.syncObject;
				lock (obj)
				{
					if (this.isSignaled && this.resetMode == EventResetMode.AutoReset)
					{
						this.isSignaled = false;
					}
					else if (!this.isSignaled)
					{
						AsyncWaitHandle.AsyncWaiter asyncWaiter = new AsyncWaitHandle.AsyncWaiter(this, callback, state);
						if (this.asyncWaiters == null)
						{
							this.asyncWaiters = new List<AsyncWaitHandle.AsyncWaiter>();
						}
						this.asyncWaiters.Add(asyncWaiter);
						if (timeout != TimeSpan.MaxValue)
						{
							if (AsyncWaitHandle.timerCompleteCallback == null)
							{
								AsyncWaitHandle.timerCompleteCallback = new Action<object>(AsyncWaitHandle.OnTimerComplete);
							}
							asyncWaiter.SetTimer(AsyncWaitHandle.timerCompleteCallback, asyncWaiter, timeout);
						}
						return false;
					}
				}
				return true;
			}
			return true;
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00002CA4 File Offset: 0x00000EA4
		private static void OnTimerComplete(object state)
		{
			AsyncWaitHandle.AsyncWaiter asyncWaiter = (AsyncWaitHandle.AsyncWaiter)state;
			AsyncWaitHandle parent = asyncWaiter.Parent;
			bool flag = false;
			object obj = parent.syncObject;
			lock (obj)
			{
				if (parent.asyncWaiters != null && parent.asyncWaiters.Remove(asyncWaiter))
				{
					asyncWaiter.TimedOut = true;
					flag = true;
				}
			}
			asyncWaiter.CancelTimer();
			if (flag)
			{
				asyncWaiter.Call();
			}
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00002D20 File Offset: 0x00000F20
		public bool Wait(TimeSpan timeout)
		{
			if (!this.isSignaled || (this.isSignaled && this.resetMode == EventResetMode.AutoReset))
			{
				object obj = this.syncObject;
				lock (obj)
				{
					if (this.isSignaled && this.resetMode == EventResetMode.AutoReset)
					{
						this.isSignaled = false;
					}
					else if (!this.isSignaled)
					{
						bool flag2 = false;
						try
						{
							try
							{
							}
							finally
							{
								this.syncWaiterCount++;
								flag2 = true;
							}
							if (timeout == TimeSpan.MaxValue)
							{
								if (!Monitor.Wait(this.syncObject, -1))
								{
									return false;
								}
							}
							else if (!Monitor.Wait(this.syncObject, timeout))
							{
								return false;
							}
						}
						finally
						{
							if (flag2)
							{
								this.syncWaiterCount--;
							}
						}
					}
				}
				return true;
			}
			return true;
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00002E10 File Offset: 0x00001010
		public void Set()
		{
			List<AsyncWaitHandle.AsyncWaiter> list = null;
			AsyncWaitHandle.AsyncWaiter asyncWaiter = null;
			if (!this.isSignaled)
			{
				object obj = this.syncObject;
				lock (obj)
				{
					if (!this.isSignaled)
					{
						if (this.resetMode == EventResetMode.ManualReset)
						{
							this.isSignaled = true;
							Monitor.PulseAll(this.syncObject);
							list = this.asyncWaiters;
							this.asyncWaiters = null;
						}
						else if (this.syncWaiterCount > 0)
						{
							Monitor.Pulse(this.syncObject);
						}
						else if (this.asyncWaiters != null && this.asyncWaiters.Count > 0)
						{
							asyncWaiter = this.asyncWaiters[0];
							this.asyncWaiters.RemoveAt(0);
						}
						else
						{
							this.isSignaled = true;
						}
					}
				}
			}
			if (list != null)
			{
				foreach (AsyncWaitHandle.AsyncWaiter asyncWaiter2 in list)
				{
					asyncWaiter2.CancelTimer();
					asyncWaiter2.Call();
				}
			}
			if (asyncWaiter != null)
			{
				asyncWaiter.CancelTimer();
				asyncWaiter.Call();
			}
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00002F34 File Offset: 0x00001134
		public void Reset()
		{
			this.isSignaled = false;
		}

		// Token: 0x04000020 RID: 32
		private static Action<object> timerCompleteCallback;

		// Token: 0x04000021 RID: 33
		private List<AsyncWaitHandle.AsyncWaiter> asyncWaiters;

		// Token: 0x04000022 RID: 34
		private bool isSignaled;

		// Token: 0x04000023 RID: 35
		private EventResetMode resetMode;

		// Token: 0x04000024 RID: 36
		private object syncObject;

		// Token: 0x04000025 RID: 37
		private int syncWaiterCount;

		// Token: 0x0200005C RID: 92
		private class AsyncWaiter : ActionItem
		{
			// Token: 0x0600037B RID: 891 RVA: 0x0001192C File Offset: 0x0000FB2C
			[SecuritySafeCritical]
			public AsyncWaiter(AsyncWaitHandle parent, Action<object, TimeoutException> callback, object state)
			{
				this.Parent = parent;
				this.callback = callback;
				this.state = state;
			}

			// Token: 0x17000096 RID: 150
			// (get) Token: 0x0600037C RID: 892 RVA: 0x00011949 File Offset: 0x0000FB49
			// (set) Token: 0x0600037D RID: 893 RVA: 0x00011951 File Offset: 0x0000FB51
			public AsyncWaitHandle Parent { get; private set; }

			// Token: 0x17000097 RID: 151
			// (get) Token: 0x0600037E RID: 894 RVA: 0x0001195A File Offset: 0x0000FB5A
			// (set) Token: 0x0600037F RID: 895 RVA: 0x00011962 File Offset: 0x0000FB62
			public bool TimedOut { get; set; }

			// Token: 0x06000380 RID: 896 RVA: 0x0001196B File Offset: 0x0000FB6B
			[SecuritySafeCritical]
			public void Call()
			{
				base.Schedule();
			}

			// Token: 0x06000381 RID: 897 RVA: 0x00011973 File Offset: 0x0000FB73
			[SecurityCritical]
			protected override void Invoke()
			{
				this.callback(this.state, this.TimedOut ? new TimeoutException(InternalSR.TimeoutOnOperation(this.originalTimeout)) : null);
			}

			// Token: 0x06000382 RID: 898 RVA: 0x000119A6 File Offset: 0x0000FBA6
			public void SetTimer(Action<object> callback, object state, TimeSpan timeout)
			{
				if (this.timer != null)
				{
					throw Fx.Exception.AsError(new InvalidOperationException(InternalSR.MustCancelOldTimer));
				}
				this.originalTimeout = timeout;
				this.timer = new IOThreadTimer(callback, state, false);
				this.timer.Set(timeout);
			}

			// Token: 0x06000383 RID: 899 RVA: 0x000119E6 File Offset: 0x0000FBE6
			public void CancelTimer()
			{
				if (this.timer != null)
				{
					this.timer.Cancel();
					this.timer = null;
				}
			}

			// Token: 0x040001D4 RID: 468
			[SecurityCritical]
			private Action<object, TimeoutException> callback;

			// Token: 0x040001D5 RID: 469
			[SecurityCritical]
			private object state;

			// Token: 0x040001D6 RID: 470
			private IOThreadTimer timer;

			// Token: 0x040001D7 RID: 471
			private TimeSpan originalTimeout;
		}
	}
}
