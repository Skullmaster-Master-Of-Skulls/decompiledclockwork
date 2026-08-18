using System;
using System.Threading;

namespace System.Runtime
{
	// Token: 0x0200002E RID: 46
	internal struct TimeoutHelper
	{
		// Token: 0x0600017A RID: 378 RVA: 0x00006A46 File Offset: 0x00004C46
		public TimeoutHelper(TimeSpan timeout)
		{
			this.originalTimeout = timeout;
			this.deadline = DateTime.MaxValue;
			this.deadlineSet = (timeout == TimeSpan.MaxValue);
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x0600017B RID: 379 RVA: 0x00006A6B File Offset: 0x00004C6B
		public TimeSpan OriginalTimeout
		{
			get
			{
				return this.originalTimeout;
			}
		}

		// Token: 0x0600017C RID: 380 RVA: 0x00006A73 File Offset: 0x00004C73
		public static bool IsTooLarge(TimeSpan timeout)
		{
			return timeout > TimeoutHelper.MaxWait && timeout != TimeSpan.MaxValue;
		}

		// Token: 0x0600017D RID: 381 RVA: 0x00006A8F File Offset: 0x00004C8F
		public static TimeSpan FromMilliseconds(int milliseconds)
		{
			if (milliseconds == -1)
			{
				return TimeSpan.MaxValue;
			}
			return TimeSpan.FromMilliseconds((double)milliseconds);
		}

		// Token: 0x0600017E RID: 382 RVA: 0x00006AA4 File Offset: 0x00004CA4
		public static int ToMilliseconds(TimeSpan timeout)
		{
			if (timeout == TimeSpan.MaxValue)
			{
				return -1;
			}
			long num = Ticks.FromTimeSpan(timeout);
			if (num / 10000L > 2147483647L)
			{
				return int.MaxValue;
			}
			return Ticks.ToMilliseconds(num);
		}

		// Token: 0x0600017F RID: 383 RVA: 0x00006AE3 File Offset: 0x00004CE3
		public static TimeSpan Min(TimeSpan val1, TimeSpan val2)
		{
			if (val1 > val2)
			{
				return val2;
			}
			return val1;
		}

		// Token: 0x06000180 RID: 384 RVA: 0x00006AF1 File Offset: 0x00004CF1
		public static TimeSpan Add(TimeSpan timeout1, TimeSpan timeout2)
		{
			return Ticks.ToTimeSpan(Ticks.Add(Ticks.FromTimeSpan(timeout1), Ticks.FromTimeSpan(timeout2)));
		}

		// Token: 0x06000181 RID: 385 RVA: 0x00006B0C File Offset: 0x00004D0C
		public static DateTime Add(DateTime time, TimeSpan timeout)
		{
			if (timeout >= TimeSpan.Zero && DateTime.MaxValue - time <= timeout)
			{
				return DateTime.MaxValue;
			}
			if (timeout <= TimeSpan.Zero && DateTime.MinValue - time >= timeout)
			{
				return DateTime.MinValue;
			}
			return time + timeout;
		}

		// Token: 0x06000182 RID: 386 RVA: 0x00006B6C File Offset: 0x00004D6C
		public static DateTime Subtract(DateTime time, TimeSpan timeout)
		{
			return TimeoutHelper.Add(time, TimeSpan.Zero - timeout);
		}

		// Token: 0x06000183 RID: 387 RVA: 0x00006B7F File Offset: 0x00004D7F
		public static TimeSpan Divide(TimeSpan timeout, int factor)
		{
			if (timeout == TimeSpan.MaxValue)
			{
				return TimeSpan.MaxValue;
			}
			return Ticks.ToTimeSpan(Ticks.FromTimeSpan(timeout) / (long)factor + 1L);
		}

		// Token: 0x06000184 RID: 388 RVA: 0x00006BA8 File Offset: 0x00004DA8
		public TimeSpan RemainingTime()
		{
			if (!this.deadlineSet)
			{
				this.SetDeadline();
				return this.originalTimeout;
			}
			if (this.deadline == DateTime.MaxValue)
			{
				return TimeSpan.MaxValue;
			}
			TimeSpan timeSpan = this.deadline - DateTime.UtcNow;
			if (timeSpan <= TimeSpan.Zero)
			{
				return TimeSpan.Zero;
			}
			return timeSpan;
		}

		// Token: 0x06000185 RID: 389 RVA: 0x00006C07 File Offset: 0x00004E07
		public TimeSpan ElapsedTime()
		{
			return this.originalTimeout - this.RemainingTime();
		}

		// Token: 0x06000186 RID: 390 RVA: 0x00006C1A File Offset: 0x00004E1A
		private void SetDeadline()
		{
			this.deadline = DateTime.UtcNow + this.originalTimeout;
			this.deadlineSet = true;
		}

		// Token: 0x06000187 RID: 391 RVA: 0x00006C39 File Offset: 0x00004E39
		public static void ThrowIfNegativeArgument(TimeSpan timeout)
		{
			TimeoutHelper.ThrowIfNegativeArgument(timeout, "timeout");
		}

		// Token: 0x06000188 RID: 392 RVA: 0x00006C46 File Offset: 0x00004E46
		public static void ThrowIfNegativeArgument(TimeSpan timeout, string argumentName)
		{
			if (timeout < TimeSpan.Zero)
			{
				throw Fx.Exception.ArgumentOutOfRange(argumentName, timeout, InternalSR.TimeoutMustBeNonNegative(argumentName, timeout));
			}
		}

		// Token: 0x06000189 RID: 393 RVA: 0x00006C73 File Offset: 0x00004E73
		public static void ThrowIfNonPositiveArgument(TimeSpan timeout)
		{
			TimeoutHelper.ThrowIfNonPositiveArgument(timeout, "timeout");
		}

		// Token: 0x0600018A RID: 394 RVA: 0x00006C80 File Offset: 0x00004E80
		public static void ThrowIfNonPositiveArgument(TimeSpan timeout, string argumentName)
		{
			if (timeout <= TimeSpan.Zero)
			{
				throw Fx.Exception.ArgumentOutOfRange(argumentName, timeout, InternalSR.TimeoutMustBePositive(argumentName, timeout));
			}
		}

		// Token: 0x0600018B RID: 395 RVA: 0x00006CAD File Offset: 0x00004EAD
		public static bool WaitOne(WaitHandle waitHandle, TimeSpan timeout)
		{
			TimeoutHelper.ThrowIfNegativeArgument(timeout);
			if (timeout == TimeSpan.MaxValue)
			{
				waitHandle.WaitOne();
				return true;
			}
			return waitHandle.WaitOne(timeout, false);
		}

		// Token: 0x040000AA RID: 170
		private DateTime deadline;

		// Token: 0x040000AB RID: 171
		private bool deadlineSet;

		// Token: 0x040000AC RID: 172
		private TimeSpan originalTimeout;

		// Token: 0x040000AD RID: 173
		public static readonly TimeSpan MaxWait = TimeSpan.FromMilliseconds(2147483647.0);
	}
}
