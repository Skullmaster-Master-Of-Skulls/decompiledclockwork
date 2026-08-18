using System;
using System.Diagnostics;
using System.Runtime;

namespace System.ServiceModel.Diagnostics
{
	// Token: 0x02000A78 RID: 2680
	internal sealed class EndpointPerformanceCounters : EndpointPerformanceCountersBase
	{
		// Token: 0x17001933 RID: 6451
		// (get) Token: 0x060069BD RID: 27069 RVA: 0x00189F87 File Offset: 0x00188187
		// (set) Token: 0x060069BE RID: 27070 RVA: 0x00189F8F File Offset: 0x0018818F
		internal PerformanceCounter[] Counters { get; set; }

		// Token: 0x060069BF RID: 27071 RVA: 0x00189F98 File Offset: 0x00188198
		internal EndpointPerformanceCounters(string service, string contract, string uri) : base(service, contract, uri)
		{
			this.Counters = new PerformanceCounter[19];
			for (int i = 0; i < 19; i++)
			{
				PerformanceCounter endpointPerformanceCounter = PerformanceCounters.GetEndpointPerformanceCounter(EndpointPerformanceCountersBase.perfCounterNames[i], this.instanceName);
				if (endpointPerformanceCounter == null)
				{
					break;
				}
				try
				{
					endpointPerformanceCounter.RawValue = 0L;
					this.Counters[i] = endpointPerformanceCounter;
				}
				catch (Exception exception)
				{
					if (Fx.IsFatal(exception))
					{
						throw;
					}
					if (DiagnosticUtility.ShouldTraceError)
					{
						TraceUtility.TraceEvent(TraceEventType.Error, 524344, SR.GetString("TraceCodePerformanceCounterFailedToLoad"), null, exception);
					}
					break;
				}
			}
		}

		// Token: 0x060069C0 RID: 27072 RVA: 0x0018A02C File Offset: 0x0018822C
		private void Increment(int counter)
		{
			this.Increment(this.Counters, counter);
		}

		// Token: 0x060069C1 RID: 27073 RVA: 0x0018A03B File Offset: 0x0018823B
		private void IncrementBy(int counter, long time)
		{
			this.IncrementBy(this.Counters, counter, time);
		}

		// Token: 0x060069C2 RID: 27074 RVA: 0x0018A04B File Offset: 0x0018824B
		private void Decrement(int counter)
		{
			this.Decrement(this.Counters, counter);
		}

		// Token: 0x060069C3 RID: 27075 RVA: 0x0018A05A File Offset: 0x0018825A
		internal override void MethodCalled()
		{
			this.Increment(0);
			this.Increment(1);
			this.Increment(2);
		}

		// Token: 0x060069C4 RID: 27076 RVA: 0x0018A071 File Offset: 0x00188271
		internal override void MethodReturnedSuccess()
		{
			this.Decrement(2);
		}

		// Token: 0x060069C5 RID: 27077 RVA: 0x0018A07A File Offset: 0x0018827A
		internal override void MethodReturnedError()
		{
			this.Increment(3);
			this.Increment(4);
			this.Decrement(2);
		}

		// Token: 0x060069C6 RID: 27078 RVA: 0x0018A091 File Offset: 0x00188291
		internal override void MethodReturnedFault()
		{
			this.Increment(5);
			this.Increment(6);
			this.Decrement(2);
		}

		// Token: 0x060069C7 RID: 27079 RVA: 0x0018A0A8 File Offset: 0x001882A8
		internal override void SaveCallDuration(long time)
		{
			this.IncrementBy(7, time);
			this.Increment(8);
		}

		// Token: 0x060069C8 RID: 27080 RVA: 0x0018A0B9 File Offset: 0x001882B9
		internal override void AuthenticationFailed()
		{
			this.Increment(9);
			this.Increment(10);
		}

		// Token: 0x060069C9 RID: 27081 RVA: 0x0018A0CB File Offset: 0x001882CB
		internal override void AuthorizationFailed()
		{
			this.Increment(11);
			this.Increment(12);
		}

		// Token: 0x060069CA RID: 27082 RVA: 0x0018A0DD File Offset: 0x001882DD
		internal override void SessionFaulted()
		{
			this.Increment(13);
			this.Increment(14);
		}

		// Token: 0x060069CB RID: 27083 RVA: 0x0018A0EF File Offset: 0x001882EF
		internal override void MessageDropped()
		{
			this.Increment(15);
			this.Increment(16);
		}

		// Token: 0x060069CC RID: 27084 RVA: 0x0018A101 File Offset: 0x00188301
		internal override void TxFlowed()
		{
			this.Increment(17);
			this.Increment(18);
		}

		// Token: 0x17001934 RID: 6452
		// (get) Token: 0x060069CD RID: 27085 RVA: 0x0018A113 File Offset: 0x00188313
		internal override bool Initialized
		{
			get
			{
				return this.Counters != null;
			}
		}

		// Token: 0x060069CE RID: 27086 RVA: 0x0018A120 File Offset: 0x00188320
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
