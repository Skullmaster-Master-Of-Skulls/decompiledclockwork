using System;
using System.Runtime;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000946 RID: 2374
	internal class WaitAsyncResult : AsyncResult
	{
		// Token: 0x06005B41 RID: 23361 RVA: 0x0014E926 File Offset: 0x0014CB26
		public WaitAsyncResult(TimeSpan timeout, bool throwTimeoutException, AsyncCallback callback, object state) : base(callback, state)
		{
			this.timeout = timeout;
			this.throwTimeoutException = throwTimeoutException;
		}

		// Token: 0x06005B42 RID: 23362 RVA: 0x0014E94C File Offset: 0x0014CB4C
		public void Begin()
		{
			object obj = this.thisLock;
			lock (obj)
			{
				if (!this.completed)
				{
					if (this.timeout != TimeSpan.MaxValue)
					{
						this.timer = new IOThreadTimer(new Action<object>(this.OnTimerElapsed), null, true);
						this.timer.Set(this.timeout);
					}
				}
			}
		}

		// Token: 0x06005B43 RID: 23363 RVA: 0x0014E9D0 File Offset: 0x0014CBD0
		public static bool End(IAsyncResult result)
		{
			return !AsyncResult.End<WaitAsyncResult>(result).timedOut;
		}

		// Token: 0x06005B44 RID: 23364 RVA: 0x0014E9E0 File Offset: 0x0014CBE0
		protected virtual string GetTimeoutString(TimeSpan timeout)
		{
			return SR.GetString("TimeoutOnOperation", new object[]
			{
				timeout
			});
		}

		// Token: 0x06005B45 RID: 23365 RVA: 0x0014E9FB File Offset: 0x0014CBFB
		public void OnAborted(CommunicationObject communicationObject)
		{
			if (this.ShouldComplete(false))
			{
				base.Complete(false, communicationObject.CreateClosedException());
			}
		}

		// Token: 0x06005B46 RID: 23366 RVA: 0x0014EA13 File Offset: 0x0014CC13
		public void OnFaulted(CommunicationObject communicationObject)
		{
			if (this.ShouldComplete(false))
			{
				base.Complete(false, communicationObject.GetTerminalException());
			}
		}

		// Token: 0x06005B47 RID: 23367 RVA: 0x0014EA2B File Offset: 0x0014CC2B
		public void OnSignaled()
		{
			if (this.ShouldComplete(false))
			{
				base.Complete(false);
			}
		}

		// Token: 0x06005B48 RID: 23368 RVA: 0x0014EA3D File Offset: 0x0014CC3D
		protected virtual void OnTimerElapsed(object state)
		{
			if (this.ShouldComplete(true))
			{
				if (this.throwTimeoutException)
				{
					base.Complete(false, new TimeoutException(this.GetTimeoutString(this.timeout)));
					return;
				}
				base.Complete(false);
			}
		}

		// Token: 0x06005B49 RID: 23369 RVA: 0x0014EA70 File Offset: 0x0014CC70
		private bool ShouldComplete(bool timedOut)
		{
			object obj = this.thisLock;
			lock (obj)
			{
				if (!this.completed)
				{
					this.completed = true;
					this.timedOut = timedOut;
					if (!timedOut && this.timer != null)
					{
						this.timer.Cancel();
					}
					return true;
				}
			}
			return false;
		}

		// Token: 0x040036E3 RID: 14051
		private bool completed;

		// Token: 0x040036E4 RID: 14052
		private bool throwTimeoutException;

		// Token: 0x040036E5 RID: 14053
		private bool timedOut;

		// Token: 0x040036E6 RID: 14054
		private TimeSpan timeout;

		// Token: 0x040036E7 RID: 14055
		private IOThreadTimer timer;

		// Token: 0x040036E8 RID: 14056
		private object thisLock = new object();

		// Token: 0x02000DCC RID: 3532
		// (Invoke) Token: 0x06008014 RID: 32788
		public delegate void AbortHandler(CommunicationObject communicationObject);

		// Token: 0x02000DCD RID: 3533
		// (Invoke) Token: 0x06008018 RID: 32792
		public delegate void SignaledHandler();
	}
}
