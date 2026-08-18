using System;
using System.Diagnostics;
using System.Runtime;

namespace System.ServiceModel.Diagnostics
{
	// Token: 0x02000A9D RID: 2717
	internal class DefaultPerformanceCounters : PerformanceCountersBase
	{
		// Token: 0x1700197B RID: 6523
		// (get) Token: 0x06006B9C RID: 27548 RVA: 0x001907E0 File Offset: 0x0018E9E0
		// (set) Token: 0x06006B9D RID: 27549 RVA: 0x001907E8 File Offset: 0x0018E9E8
		internal PerformanceCounter[] Counters { get; set; }

		// Token: 0x1700197C RID: 6524
		// (get) Token: 0x06006B9E RID: 27550 RVA: 0x001907F1 File Offset: 0x0018E9F1
		internal override string InstanceName
		{
			get
			{
				return this.instanceName;
			}
		}

		// Token: 0x1700197D RID: 6525
		// (get) Token: 0x06006B9F RID: 27551 RVA: 0x001907F9 File Offset: 0x0018E9F9
		internal override string[] CounterNames
		{
			get
			{
				return this.perfCounterNames;
			}
		}

		// Token: 0x1700197E RID: 6526
		// (get) Token: 0x06006BA0 RID: 27552 RVA: 0x00190801 File Offset: 0x0018EA01
		internal override int PerfCounterStart
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x1700197F RID: 6527
		// (get) Token: 0x06006BA1 RID: 27553 RVA: 0x00190804 File Offset: 0x0018EA04
		internal override int PerfCounterEnd
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x06006BA2 RID: 27554 RVA: 0x00190807 File Offset: 0x0018EA07
		internal static string CreateFriendlyInstanceName(ServiceHostBase serviceHost)
		{
			return "_WCF_Admin";
		}

		// Token: 0x06006BA3 RID: 27555 RVA: 0x00190810 File Offset: 0x0018EA10
		internal DefaultPerformanceCounters(ServiceHostBase serviceHost)
		{
			this.instanceName = DefaultPerformanceCounters.CreateFriendlyInstanceName(serviceHost);
			this.Counters = new PerformanceCounter[1];
			for (int i = 0; i < 1; i++)
			{
				try
				{
					PerformanceCounter defaultPerformanceCounter = PerformanceCounters.GetDefaultPerformanceCounter(this.perfCounterNames[i], this.instanceName);
					if (defaultPerformanceCounter == null)
					{
						break;
					}
					this.Counters[i] = defaultPerformanceCounter;
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

		// Token: 0x17001980 RID: 6528
		// (get) Token: 0x06006BA4 RID: 27556 RVA: 0x001908C0 File Offset: 0x0018EAC0
		internal override bool Initialized
		{
			get
			{
				return this.Counters != null;
			}
		}

		// Token: 0x06006BA5 RID: 27557 RVA: 0x001908CC File Offset: 0x0018EACC
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

		// Token: 0x04003CF1 RID: 15601
		private string instanceName;

		// Token: 0x04003CF2 RID: 15602
		private string[] perfCounterNames = new string[]
		{
			"Instances"
		};

		// Token: 0x04003CF3 RID: 15603
		private const int maxCounterLength = 64;

		// Token: 0x04003CF4 RID: 15604
		private const int hashLength = 2;

		// Token: 0x02000EC2 RID: 3778
		private enum PerfCounters
		{
			// Token: 0x04004CA7 RID: 19623
			Instances,
			// Token: 0x04004CA8 RID: 19624
			TotalCounters
		}

		// Token: 0x02000EC3 RID: 3779
		[Flags]
		private enum truncOptions : uint
		{
			// Token: 0x04004CAA RID: 19626
			NoBits = 0U,
			// Token: 0x04004CAB RID: 19627
			service32 = 1U,
			// Token: 0x04004CAC RID: 19628
			uri31 = 4U
		}
	}
}
