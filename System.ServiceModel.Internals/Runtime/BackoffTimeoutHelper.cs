using System;
using System.Threading;

namespace System.Runtime
{
	// Token: 0x0200000D RID: 13
	internal sealed class BackoffTimeoutHelper
	{
		// Token: 0x0600004D RID: 77 RVA: 0x00002F3D File Offset: 0x0000113D
		internal BackoffTimeoutHelper(TimeSpan timeout) : this(timeout, BackoffTimeoutHelper.defaultMaxWaitTime)
		{
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00002F4B File Offset: 0x0000114B
		internal BackoffTimeoutHelper(TimeSpan timeout, TimeSpan maxWaitTime) : this(timeout, maxWaitTime, BackoffTimeoutHelper.defaultInitialWaitTime)
		{
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00002F5A File Offset: 0x0000115A
		internal BackoffTimeoutHelper(TimeSpan timeout, TimeSpan maxWaitTime, TimeSpan initialWaitTime)
		{
			this.random = new Random(this.GetHashCode());
			this.maxWaitTime = maxWaitTime;
			this.originalTimeout = timeout;
			this.Reset(timeout, initialWaitTime);
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000050 RID: 80 RVA: 0x00002F89 File Offset: 0x00001189
		public TimeSpan OriginalTimeout
		{
			get
			{
				return this.originalTimeout;
			}
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00002F91 File Offset: 0x00001191
		private void Reset(TimeSpan timeout, TimeSpan initialWaitTime)
		{
			if (timeout == TimeSpan.MaxValue)
			{
				this.deadline = DateTime.MaxValue;
			}
			else
			{
				this.deadline = DateTime.UtcNow + timeout;
			}
			this.waitTime = initialWaitTime;
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00002FC5 File Offset: 0x000011C5
		public bool IsExpired()
		{
			return !(this.deadline == DateTime.MaxValue) && DateTime.UtcNow >= this.deadline;
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00002FEC File Offset: 0x000011EC
		public void WaitAndBackoff(Action<object> callback, object state)
		{
			if (this.backoffCallback != callback || this.backoffState != state)
			{
				if (this.backoffTimer != null)
				{
					this.backoffTimer.Cancel();
				}
				this.backoffCallback = callback;
				this.backoffState = state;
				this.backoffTimer = new IOThreadTimer(callback, state, false, BackoffTimeoutHelper.maxSkewMilliseconds);
			}
			TimeSpan timeFromNow = this.WaitTimeWithDrift();
			this.Backoff();
			this.backoffTimer.Set(timeFromNow);
		}

		// Token: 0x06000054 RID: 84 RVA: 0x0000305E File Offset: 0x0000125E
		public void WaitAndBackoff()
		{
			Thread.Sleep(this.WaitTimeWithDrift());
			this.Backoff();
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00003074 File Offset: 0x00001274
		private TimeSpan WaitTimeWithDrift()
		{
			return Ticks.ToTimeSpan(Math.Max(Ticks.FromTimeSpan(BackoffTimeoutHelper.defaultInitialWaitTime), Ticks.Add(Ticks.FromTimeSpan(this.waitTime), (long)((ulong)this.random.Next() % (ulong)(2L * BackoffTimeoutHelper.maxDriftTicks + 1L) - (ulong)BackoffTimeoutHelper.maxDriftTicks))));
		}

		// Token: 0x06000056 RID: 86 RVA: 0x000030C4 File Offset: 0x000012C4
		private void Backoff()
		{
			if (this.waitTime.Ticks >= this.maxWaitTime.Ticks / 2L)
			{
				this.waitTime = this.maxWaitTime;
			}
			else
			{
				this.waitTime = TimeSpan.FromTicks(this.waitTime.Ticks * 2L);
			}
			if (this.deadline != DateTime.MaxValue)
			{
				TimeSpan t = this.deadline - DateTime.UtcNow;
				if (this.waitTime > t)
				{
					this.waitTime = t;
					if (this.waitTime < TimeSpan.Zero)
					{
						this.waitTime = TimeSpan.Zero;
					}
				}
			}
		}

		// Token: 0x04000026 RID: 38
		private static readonly int maxSkewMilliseconds = (int)(IOThreadTimer.SystemTimeResolutionTicks / 10000L);

		// Token: 0x04000027 RID: 39
		private static readonly long maxDriftTicks = IOThreadTimer.SystemTimeResolutionTicks * 2L;

		// Token: 0x04000028 RID: 40
		private static readonly TimeSpan defaultInitialWaitTime = TimeSpan.FromMilliseconds(1.0);

		// Token: 0x04000029 RID: 41
		private static readonly TimeSpan defaultMaxWaitTime = TimeSpan.FromMinutes(1.0);

		// Token: 0x0400002A RID: 42
		private DateTime deadline;

		// Token: 0x0400002B RID: 43
		private TimeSpan maxWaitTime;

		// Token: 0x0400002C RID: 44
		private TimeSpan waitTime;

		// Token: 0x0400002D RID: 45
		private IOThreadTimer backoffTimer;

		// Token: 0x0400002E RID: 46
		private Action<object> backoffCallback;

		// Token: 0x0400002F RID: 47
		private object backoffState;

		// Token: 0x04000030 RID: 48
		private Random random;

		// Token: 0x04000031 RID: 49
		private TimeSpan originalTimeout;
	}
}
