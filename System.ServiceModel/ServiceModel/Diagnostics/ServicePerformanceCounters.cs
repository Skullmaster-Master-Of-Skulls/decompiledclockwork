using System;
using System.Diagnostics;
using System.Runtime;

namespace System.ServiceModel.Diagnostics
{
	// Token: 0x02000A9B RID: 2715
	internal sealed class ServicePerformanceCounters : ServicePerformanceCountersBase
	{
		// Token: 0x17001978 RID: 6520
		// (get) Token: 0x06006B62 RID: 27490 RVA: 0x0018FDF7 File Offset: 0x0018DFF7
		// (set) Token: 0x06006B63 RID: 27491 RVA: 0x0018FDFF File Offset: 0x0018DFFF
		internal PerformanceCounter[] Counters { get; set; }

		// Token: 0x06006B64 RID: 27492 RVA: 0x0018FE08 File Offset: 0x0018E008
		internal ServicePerformanceCounters(ServiceHostBase serviceHost) : base(serviceHost)
		{
			this.Counters = new PerformanceCounter[39];
			for (int i = 0; i < 39; i++)
			{
				PerformanceCounter servicePerformanceCounter = PerformanceCounters.GetServicePerformanceCounter(ServicePerformanceCountersBase.perfCounterNames[i], this.InstanceName);
				if (servicePerformanceCounter == null)
				{
					break;
				}
				try
				{
					servicePerformanceCounter.RawValue = 0L;
					this.Counters[i] = servicePerformanceCounter;
				}
				catch (Exception exception)
				{
					if (Fx.IsFatal(exception))
					{
						throw;
					}
					if (DiagnosticUtility.ShouldTraceError)
					{
						TraceUtility.TraceEvent(TraceEventType.Error, 524347, SR.GetString("TraceCodePerformanceCountersFailedForService"), null, exception);
					}
					break;
				}
			}
		}

		// Token: 0x06006B65 RID: 27493 RVA: 0x0018FE9C File Offset: 0x0018E09C
		private void Increment(int counter)
		{
			this.Increment(this.Counters, counter);
		}

		// Token: 0x06006B66 RID: 27494 RVA: 0x0018FEAB File Offset: 0x0018E0AB
		private void IncrementBy(int counter, long time)
		{
			this.IncrementBy(this.Counters, counter, time);
		}

		// Token: 0x06006B67 RID: 27495 RVA: 0x0018FEBB File Offset: 0x0018E0BB
		private void Decrement(int counter)
		{
			this.Decrement(this.Counters, counter);
		}

		// Token: 0x06006B68 RID: 27496 RVA: 0x0018FECA File Offset: 0x0018E0CA
		private void Set(int counter, long denominator)
		{
			this.Set(this.Counters, counter, denominator);
		}

		// Token: 0x06006B69 RID: 27497 RVA: 0x0018FEDA File Offset: 0x0018E0DA
		internal override void MethodCalled()
		{
			this.Increment(0);
			this.Increment(1);
			this.Increment(2);
		}

		// Token: 0x06006B6A RID: 27498 RVA: 0x0018FEF1 File Offset: 0x0018E0F1
		internal override void MethodReturnedSuccess()
		{
			this.Decrement(2);
		}

		// Token: 0x06006B6B RID: 27499 RVA: 0x0018FEFA File Offset: 0x0018E0FA
		internal override void MethodReturnedError()
		{
			this.Increment(3);
			this.Increment(4);
			this.Decrement(2);
		}

		// Token: 0x06006B6C RID: 27500 RVA: 0x0018FF11 File Offset: 0x0018E111
		internal override void MethodReturnedFault()
		{
			this.Increment(5);
			this.Increment(6);
			this.Decrement(2);
		}

		// Token: 0x06006B6D RID: 27501 RVA: 0x0018FF28 File Offset: 0x0018E128
		internal override void SaveCallDuration(long time)
		{
			this.IncrementBy(7, time);
			this.Increment(8);
		}

		// Token: 0x06006B6E RID: 27502 RVA: 0x0018FF39 File Offset: 0x0018E139
		internal override void AuthenticationFailed()
		{
			this.Increment(9);
			this.Increment(10);
		}

		// Token: 0x06006B6F RID: 27503 RVA: 0x0018FF4B File Offset: 0x0018E14B
		internal override void AuthorizationFailed()
		{
			this.Increment(11);
			this.Increment(12);
		}

		// Token: 0x06006B70 RID: 27504 RVA: 0x0018FF5D File Offset: 0x0018E15D
		internal override void ServiceInstanceCreated()
		{
			this.Increment(13);
			this.Increment(14);
		}

		// Token: 0x06006B71 RID: 27505 RVA: 0x0018FF6F File Offset: 0x0018E16F
		internal override void ServiceInstanceRemoved()
		{
			this.Decrement(13);
		}

		// Token: 0x06006B72 RID: 27506 RVA: 0x0018FF79 File Offset: 0x0018E179
		internal override void SessionFaulted()
		{
			this.Increment(15);
			this.Increment(16);
		}

		// Token: 0x06006B73 RID: 27507 RVA: 0x0018FF8B File Offset: 0x0018E18B
		internal override void MessageDropped()
		{
			this.Increment(17);
			this.Increment(18);
		}

		// Token: 0x06006B74 RID: 27508 RVA: 0x0018FF9D File Offset: 0x0018E19D
		internal override void TxCommitted(long count)
		{
			this.IncrementBy(21, count);
			this.IncrementBy(22, count);
		}

		// Token: 0x06006B75 RID: 27509 RVA: 0x0018FFB1 File Offset: 0x0018E1B1
		internal override void TxInDoubt(long count)
		{
			this.IncrementBy(25, count);
			this.IncrementBy(26, count);
		}

		// Token: 0x06006B76 RID: 27510 RVA: 0x0018FFC5 File Offset: 0x0018E1C5
		internal override void TxAborted(long count)
		{
			this.IncrementBy(23, count);
			this.IncrementBy(24, count);
		}

		// Token: 0x06006B77 RID: 27511 RVA: 0x0018FFD9 File Offset: 0x0018E1D9
		internal override void TxFlowed()
		{
			this.Increment(19);
			this.Increment(20);
		}

		// Token: 0x06006B78 RID: 27512 RVA: 0x0018FFEB File Offset: 0x0018E1EB
		internal override void MsmqDroppedMessage()
		{
			this.Increment(31);
			this.Increment(32);
		}

		// Token: 0x06006B79 RID: 27513 RVA: 0x0018FFFD File Offset: 0x0018E1FD
		internal override void MsmqPoisonMessage()
		{
			this.Increment(27);
			this.Increment(28);
		}

		// Token: 0x06006B7A RID: 27514 RVA: 0x0019000F File Offset: 0x0018E20F
		internal override void MsmqRejectedMessage()
		{
			this.Increment(29);
			this.Increment(30);
		}

		// Token: 0x06006B7B RID: 27515 RVA: 0x00190021 File Offset: 0x0018E221
		internal override void IncrementThrottlePercent(int counterIndex)
		{
			this.Increment(counterIndex);
		}

		// Token: 0x06006B7C RID: 27516 RVA: 0x0019002A File Offset: 0x0018E22A
		internal override void SetThrottleBase(int counterIndex, long denominator)
		{
			this.Set(counterIndex, denominator);
		}

		// Token: 0x06006B7D RID: 27517 RVA: 0x00190034 File Offset: 0x0018E234
		internal override void DecrementThrottlePercent(int counterIndex)
		{
			this.Decrement(counterIndex);
		}

		// Token: 0x17001979 RID: 6521
		// (get) Token: 0x06006B7E RID: 27518 RVA: 0x0019003D File Offset: 0x0018E23D
		internal override bool Initialized
		{
			get
			{
				return this.Counters != null;
			}
		}

		// Token: 0x06006B7F RID: 27519 RVA: 0x00190048 File Offset: 0x0018E248
		protected override void Dispose(bool disposing)
		{
			try
			{
				if (disposing && PerformanceCounters.PerformanceCountersEnabled && this.Counters != null)
				{
					for (int i = this.PerfCounterStart; i < this.PerfCounterEnd; i++)
					{
						PerformanceCounter performanceCounter = this.Counters[i];
						if (performanceCounter != null)
						{
							PerformanceCounters.ReleasePerformanceCounter(ref performanceCounter);
						}
						this.Counters[i] = null;
					}
					this.Counters = null;
				}
			}
			finally
			{
				base.Dispose(disposing);
			}
		}
	}
}
