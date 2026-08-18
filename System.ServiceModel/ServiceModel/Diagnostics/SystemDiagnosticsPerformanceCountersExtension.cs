using System;
using System.Diagnostics;
using System.Runtime;

namespace System.ServiceModel.Diagnostics
{
	// Token: 0x02000AA0 RID: 2720
	internal static class SystemDiagnosticsPerformanceCountersExtension
	{
		// Token: 0x06006BB8 RID: 27576 RVA: 0x00190C30 File Offset: 0x0018EE30
		internal static void Increment(this PerformanceCountersBase thisPtr, PerformanceCounter[] counters, int counterIndex)
		{
			PerformanceCounter performanceCounter = null;
			try
			{
				if (counters != null)
				{
					performanceCounter = counters[counterIndex];
					if (performanceCounter != null)
					{
						performanceCounter.Increment();
					}
				}
			}
			catch (Exception exception)
			{
				if (Fx.IsFatal(exception))
				{
					throw;
				}
				PerformanceCounters.TracePerformanceCounterUpdateFailure(thisPtr.InstanceName, thisPtr.CounterNames[counterIndex]);
				if (counters != null)
				{
					counters[counterIndex] = null;
					PerformanceCounters.ReleasePerformanceCounter(ref performanceCounter);
				}
			}
		}

		// Token: 0x06006BB9 RID: 27577 RVA: 0x00190C90 File Offset: 0x0018EE90
		internal static void IncrementBy(this PerformanceCountersBase thisPtr, PerformanceCounter[] counters, int counterIndex, long time)
		{
			PerformanceCounter performanceCounter = null;
			try
			{
				if (counters != null)
				{
					performanceCounter = counters[counterIndex];
					if (performanceCounter != null)
					{
						performanceCounter.IncrementBy(time);
					}
				}
			}
			catch (Exception exception)
			{
				if (Fx.IsFatal(exception))
				{
					throw;
				}
				PerformanceCounters.TracePerformanceCounterUpdateFailure(thisPtr.InstanceName, thisPtr.CounterNames[counterIndex]);
				if (counters != null)
				{
					counters[counterIndex] = null;
					PerformanceCounters.ReleasePerformanceCounter(ref performanceCounter);
				}
			}
		}

		// Token: 0x06006BBA RID: 27578 RVA: 0x00190CF4 File Offset: 0x0018EEF4
		internal static void Set(this PerformanceCountersBase thisPtr, PerformanceCounter[] counters, int counterIndex, long value)
		{
			PerformanceCounter performanceCounter = null;
			try
			{
				if (counters != null)
				{
					performanceCounter = counters[counterIndex];
					if (performanceCounter != null)
					{
						performanceCounter.RawValue = value;
					}
				}
			}
			catch (Exception exception)
			{
				if (Fx.IsFatal(exception))
				{
					throw;
				}
				PerformanceCounters.TracePerformanceCounterUpdateFailure(thisPtr.InstanceName, thisPtr.CounterNames[counterIndex]);
				counters[counterIndex] = null;
				PerformanceCounters.ReleasePerformanceCounter(ref performanceCounter);
			}
		}

		// Token: 0x06006BBB RID: 27579 RVA: 0x00190D54 File Offset: 0x0018EF54
		internal static void Decrement(this PerformanceCountersBase thisPtr, PerformanceCounter[] counters, int counterIndex)
		{
			PerformanceCounter performanceCounter = null;
			try
			{
				if (counters != null)
				{
					performanceCounter = counters[counterIndex];
					if (performanceCounter != null)
					{
						performanceCounter.Decrement();
					}
				}
			}
			catch (Exception exception)
			{
				if (Fx.IsFatal(exception))
				{
					throw;
				}
				PerformanceCounters.TracePerformanceCounterUpdateFailure(thisPtr.InstanceName, thisPtr.CounterNames[counterIndex]);
				if (counters != null)
				{
					counters[counterIndex] = null;
					PerformanceCounters.ReleasePerformanceCounter(ref performanceCounter);
				}
			}
		}
	}
}
