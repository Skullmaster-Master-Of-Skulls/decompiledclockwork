using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime;
using System.ServiceModel.Diagnostics;
using System.Threading;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x0200059B RID: 1435
	internal sealed class QuotaThrottle
	{
		// Token: 0x0600379D RID: 14237 RVA: 0x000D6856 File Offset: 0x000D4A56
		internal QuotaThrottle(WaitCallback release, object mutex)
		{
			this.limit = int.MaxValue;
			this.mutex = mutex;
			this.release = release;
			this.waiters = new Queue<object>();
		}

		// Token: 0x17000D41 RID: 3393
		// (get) Token: 0x0600379E RID: 14238 RVA: 0x000D688D File Offset: 0x000D4A8D
		private bool IsEnabled
		{
			get
			{
				return this.limit != int.MaxValue;
			}
		}

		// Token: 0x17000D42 RID: 3394
		// (set) Token: 0x0600379F RID: 14239 RVA: 0x000D689F File Offset: 0x000D4A9F
		internal string Owner
		{
			set
			{
				this.owner = value;
			}
		}

		// Token: 0x17000D43 RID: 3395
		// (get) Token: 0x060037A0 RID: 14240 RVA: 0x000D68A8 File Offset: 0x000D4AA8
		internal int Limit
		{
			get
			{
				return this.limit;
			}
		}

		// Token: 0x060037A1 RID: 14241 RVA: 0x000D68B0 File Offset: 0x000D4AB0
		internal bool Acquire(object o)
		{
			object obj = this.mutex;
			bool result;
			lock (obj)
			{
				if (this.IsEnabled)
				{
					if (this.limit > 0)
					{
						this.limit--;
						if (this.limit == 0 && DiagnosticUtility.ShouldTraceWarning && !this.didTraceThrottleLimit)
						{
							this.didTraceThrottleLimit = true;
							TraceUtility.TraceEvent(TraceEventType.Warning, 524328, SR.GetString("TraceCodeManualFlowThrottleLimitReached", new object[]
							{
								this.propertyName,
								this.owner
							}));
						}
						result = true;
					}
					else
					{
						this.waiters.Enqueue(o);
						result = false;
					}
				}
				else
				{
					result = true;
				}
			}
			return result;
		}

		// Token: 0x060037A2 RID: 14242 RVA: 0x000D696C File Offset: 0x000D4B6C
		internal int IncrementLimit(int incrementBy)
		{
			if (incrementBy < 0)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("incrementBy", incrementBy, SR.GetString("ValueMustBeNonNegative")));
			}
			object[] array = null;
			object obj = this.mutex;
			checked
			{
				int result;
				lock (obj)
				{
					if (this.IsEnabled)
					{
						this.limit += incrementBy;
						array = this.LimitChanged();
					}
					result = this.limit;
				}
				if (array != null)
				{
					this.Release(array);
				}
				return result;
			}
		}

		// Token: 0x060037A3 RID: 14243 RVA: 0x000D6A00 File Offset: 0x000D4C00
		private object[] LimitChanged()
		{
			object[] array = null;
			if (this.IsEnabled)
			{
				if (this.waiters.Count > 0 && this.limit > 0)
				{
					if (this.limit < this.waiters.Count)
					{
						array = new object[this.limit];
						for (int i = 0; i < this.limit; i++)
						{
							array[i] = this.waiters.Dequeue();
						}
						this.limit = 0;
					}
					else
					{
						array = this.waiters.ToArray();
						this.waiters.Clear();
						this.waiters.TrimExcess();
						this.limit -= array.Length;
					}
				}
				this.didTraceThrottleLimit = false;
			}
			else
			{
				array = this.waiters.ToArray();
				this.waiters.Clear();
				this.waiters.TrimExcess();
			}
			return array;
		}

		// Token: 0x060037A4 RID: 14244 RVA: 0x000D6ADC File Offset: 0x000D4CDC
		internal void SetLimit(int messageLimit)
		{
			if (messageLimit < 0)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("messageLimit", messageLimit, SR.GetString("ValueMustBeNonNegative")));
			}
			object[] array = null;
			object obj = this.mutex;
			lock (obj)
			{
				this.limit = messageLimit;
				array = this.LimitChanged();
			}
			if (array != null)
			{
				this.Release(array);
			}
		}

		// Token: 0x060037A5 RID: 14245 RVA: 0x000D6B5C File Offset: 0x000D4D5C
		private void ReleaseAsync(object state)
		{
			this.release(state);
		}

		// Token: 0x060037A6 RID: 14246 RVA: 0x000D6B6C File Offset: 0x000D4D6C
		internal void Release(object[] released)
		{
			for (int i = 0; i < released.Length; i++)
			{
				ActionItem.Schedule(new Action<object>(this.ReleaseAsync), released[i]);
			}
		}

		// Token: 0x0400295E RID: 10590
		private int limit;

		// Token: 0x0400295F RID: 10591
		private object mutex;

		// Token: 0x04002960 RID: 10592
		private WaitCallback release;

		// Token: 0x04002961 RID: 10593
		private Queue<object> waiters;

		// Token: 0x04002962 RID: 10594
		private bool didTraceThrottleLimit;

		// Token: 0x04002963 RID: 10595
		private string propertyName = "ManualFlowControlLimit";

		// Token: 0x04002964 RID: 10596
		private string owner;
	}
}
