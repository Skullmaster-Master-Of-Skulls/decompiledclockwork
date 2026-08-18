using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.ServiceModel.Diagnostics;
using System.ServiceModel.Diagnostics.Application;
using System.Threading;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x02000565 RID: 1381
	internal sealed class FlowThrottle
	{
		// Token: 0x060035AB RID: 13739 RVA: 0x000D0F30 File Offset: 0x000CF130
		internal FlowThrottle(WaitCallback release, int capacity, string propertyName, string configName)
		{
			if (capacity <= 0)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SFxThrottleLimitMustBeGreaterThanZero0")));
			}
			this.count = 0;
			this.capacity = capacity;
			this.mutex = new object();
			this.release = release;
			this.waiters = new Queue<object>();
			this.propertyName = propertyName;
			this.configName = configName;
			this.warningRestoreLimit = (int)Math.Floor(0.7 * (double)capacity);
		}

		// Token: 0x17000CD2 RID: 3282
		// (get) Token: 0x060035AC RID: 13740 RVA: 0x000D0FB3 File Offset: 0x000CF1B3
		// (set) Token: 0x060035AD RID: 13741 RVA: 0x000D0FBB File Offset: 0x000CF1BB
		internal int Capacity
		{
			get
			{
				return this.capacity;
			}
			set
			{
				if (value <= 0)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SFxThrottleLimitMustBeGreaterThanZero0")));
				}
				this.capacity = value;
			}
		}

		// Token: 0x17000CD3 RID: 3283
		// (get) Token: 0x060035AE RID: 13742 RVA: 0x000D0FE2 File Offset: 0x000CF1E2
		internal int Count
		{
			get
			{
				return this.count;
			}
		}

		// Token: 0x060035AF RID: 13743 RVA: 0x000D0FEC File Offset: 0x000CF1EC
		internal bool Acquire(object o)
		{
			bool flag = true;
			object obj = this.mutex;
			bool result;
			lock (obj)
			{
				if (this.count < this.capacity)
				{
					this.count++;
				}
				else
				{
					if (this.waiters.Count == 0)
					{
						if (TD.MessageThrottleExceededIsEnabled() && !this.warningIssued)
						{
							TD.MessageThrottleExceeded(this.propertyName, (long)this.capacity);
							this.warningIssued = true;
						}
						if (DiagnosticUtility.ShouldTraceWarning)
						{
							string @string;
							if (this.propertyName != null)
							{
								@string = SR.GetString("TraceCodeServiceThrottleLimitReached", new object[]
								{
									this.propertyName,
									this.capacity,
									this.configName
								});
							}
							else
							{
								@string = SR.GetString("TraceCodeServiceThrottleLimitReachedInternal", new object[]
								{
									this.capacity
								});
							}
							TraceUtility.TraceEvent(TraceEventType.Warning, 524337, @string);
						}
					}
					this.waiters.Enqueue(o);
					flag = false;
				}
				if (this.acquired != null)
				{
					this.acquired();
				}
				if (this.ratio != null)
				{
					this.ratio(this.count);
				}
				result = flag;
			}
			return result;
		}

		// Token: 0x060035B0 RID: 13744 RVA: 0x000D113C File Offset: 0x000CF33C
		internal void Release()
		{
			object obj = null;
			object obj2 = this.mutex;
			lock (obj2)
			{
				if (this.waiters.Count > 0)
				{
					obj = this.waiters.Dequeue();
					if (this.waiters.Count == 0)
					{
						this.waiters.TrimExcess();
					}
				}
				else
				{
					this.count--;
					if (this.count < this.warningRestoreLimit)
					{
						if (TD.MessageThrottleAtSeventyPercentIsEnabled() && this.warningIssued)
						{
							TD.MessageThrottleAtSeventyPercent(this.propertyName, (long)this.capacity);
						}
						this.warningIssued = false;
					}
				}
			}
			if (obj != null)
			{
				this.release(obj);
			}
			if (this.released != null)
			{
				this.released();
			}
			if (this.ratio != null)
			{
				this.ratio(this.count);
			}
		}

		// Token: 0x060035B1 RID: 13745 RVA: 0x000D122C File Offset: 0x000CF42C
		internal void SetReleased(Action action)
		{
			this.released = action;
		}

		// Token: 0x060035B2 RID: 13746 RVA: 0x000D1235 File Offset: 0x000CF435
		internal void SetAcquired(Action action)
		{
			this.acquired = action;
		}

		// Token: 0x060035B3 RID: 13747 RVA: 0x000D123E File Offset: 0x000CF43E
		internal void SetRatio(Action<int> action)
		{
			this.ratio = action;
		}

		// Token: 0x04002890 RID: 10384
		private int capacity;

		// Token: 0x04002891 RID: 10385
		private int count;

		// Token: 0x04002892 RID: 10386
		private bool warningIssued;

		// Token: 0x04002893 RID: 10387
		private int warningRestoreLimit;

		// Token: 0x04002894 RID: 10388
		private object mutex;

		// Token: 0x04002895 RID: 10389
		private WaitCallback release;

		// Token: 0x04002896 RID: 10390
		private Queue<object> waiters;

		// Token: 0x04002897 RID: 10391
		private string propertyName;

		// Token: 0x04002898 RID: 10392
		private string configName;

		// Token: 0x04002899 RID: 10393
		private Action acquired;

		// Token: 0x0400289A RID: 10394
		private Action released;

		// Token: 0x0400289B RID: 10395
		private Action<int> ratio;
	}
}
