using System;
using System.Diagnostics;
using System.Runtime;

namespace System.ServiceModel.Diagnostics
{
	// Token: 0x02000A87 RID: 2695
	internal sealed class OperationPerformanceCounters : OperationPerformanceCountersBase
	{
		// Token: 0x17001959 RID: 6489
		// (get) Token: 0x06006A59 RID: 27225 RVA: 0x0018C7FF File Offset: 0x0018A9FF
		// (set) Token: 0x06006A5A RID: 27226 RVA: 0x0018C807 File Offset: 0x0018AA07
		internal PerformanceCounter[] Counters { get; set; }

		// Token: 0x06006A5B RID: 27227 RVA: 0x0018C810 File Offset: 0x0018AA10
		internal OperationPerformanceCounters(string service, string contract, string operationName, string uri) : base(service, contract, operationName, uri)
		{
			this.Counters = new PerformanceCounter[15];
			for (int i = 0; i < 15; i++)
			{
				PerformanceCounter operationPerformanceCounter = PerformanceCounters.GetOperationPerformanceCounter(OperationPerformanceCountersBase.perfCounterNames[i], this.instanceName);
				if (operationPerformanceCounter == null)
				{
					break;
				}
				try
				{
					operationPerformanceCounter.RawValue = 0L;
					this.Counters[i] = operationPerformanceCounter;
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

		// Token: 0x06006A5C RID: 27228 RVA: 0x0018C8A8 File Offset: 0x0018AAA8
		private void Increment(int counter)
		{
			this.Increment(this.Counters, counter);
		}

		// Token: 0x06006A5D RID: 27229 RVA: 0x0018C8B7 File Offset: 0x0018AAB7
		private void IncrementBy(int counter, long time)
		{
			this.IncrementBy(this.Counters, counter, time);
		}

		// Token: 0x06006A5E RID: 27230 RVA: 0x0018C8C7 File Offset: 0x0018AAC7
		private void Decrement(int counter)
		{
			this.Decrement(this.Counters, counter);
		}

		// Token: 0x06006A5F RID: 27231 RVA: 0x0018C8D6 File Offset: 0x0018AAD6
		internal override void MethodCalled()
		{
			this.Increment(0);
			this.Increment(1);
			this.Increment(2);
		}

		// Token: 0x06006A60 RID: 27232 RVA: 0x0018C8ED File Offset: 0x0018AAED
		internal override void MethodReturnedSuccess()
		{
			this.Decrement(2);
		}

		// Token: 0x06006A61 RID: 27233 RVA: 0x0018C8F6 File Offset: 0x0018AAF6
		internal override void MethodReturnedError()
		{
			this.Increment(3);
			this.Increment(4);
			this.Decrement(2);
		}

		// Token: 0x06006A62 RID: 27234 RVA: 0x0018C90D File Offset: 0x0018AB0D
		internal override void MethodReturnedFault()
		{
			this.Increment(5);
			this.Increment(6);
			this.Decrement(2);
		}

		// Token: 0x06006A63 RID: 27235 RVA: 0x0018C924 File Offset: 0x0018AB24
		internal override void SaveCallDuration(long time)
		{
			this.IncrementBy(7, time);
			this.Increment(8);
		}

		// Token: 0x06006A64 RID: 27236 RVA: 0x0018C935 File Offset: 0x0018AB35
		internal override void AuthenticationFailed()
		{
			this.Increment(9);
			this.Increment(10);
		}

		// Token: 0x06006A65 RID: 27237 RVA: 0x0018C947 File Offset: 0x0018AB47
		internal override void AuthorizationFailed()
		{
			this.Increment(11);
			this.Increment(12);
		}

		// Token: 0x06006A66 RID: 27238 RVA: 0x0018C959 File Offset: 0x0018AB59
		internal override void TxFlowed()
		{
			this.Increment(13);
			this.Increment(14);
		}

		// Token: 0x1700195A RID: 6490
		// (get) Token: 0x06006A67 RID: 27239 RVA: 0x0018C96B File Offset: 0x0018AB6B
		internal override bool Initialized
		{
			get
			{
				return this.Counters != null;
			}
		}

		// Token: 0x06006A68 RID: 27240 RVA: 0x0018C978 File Offset: 0x0018AB78
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
