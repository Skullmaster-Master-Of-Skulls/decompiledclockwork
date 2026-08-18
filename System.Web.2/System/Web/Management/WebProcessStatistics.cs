using System;
using System.Diagnostics;
using System.Globalization;
using System.Web.Hosting;

namespace System.Web.Management
{
	// Token: 0x0200019C RID: 412
	public class WebProcessStatistics
	{
		// Token: 0x060015CD RID: 5581 RVA: 0x000431F0 File Offset: 0x000413F0
		static WebProcessStatistics()
		{
			try
			{
				WebProcessStatistics.s_startTime = Process.GetCurrentProcess().StartTime;
			}
			catch
			{
				WebProcessStatistics.s_getCurrentProcFailed = true;
			}
		}

		// Token: 0x060015CE RID: 5582 RVA: 0x00043258 File Offset: 0x00041458
		private void Update()
		{
			DateTime now = DateTime.Now;
			if (now - WebProcessStatistics.s_lastUpdated < WebProcessStatistics.TS_ONE_SECOND)
			{
				return;
			}
			object obj = WebProcessStatistics.s_lockObject;
			lock (obj)
			{
				if (!(now - WebProcessStatistics.s_lastUpdated < WebProcessStatistics.TS_ONE_SECOND))
				{
					if (!WebProcessStatistics.s_getCurrentProcFailed)
					{
						Process currentProcess = Process.GetCurrentProcess();
						WebProcessStatistics.s_threadCount = currentProcess.Threads.Count;
						WebProcessStatistics.s_workingSet = currentProcess.WorkingSet64;
						WebProcessStatistics.s_peakWorkingSet = currentProcess.PeakWorkingSet64;
					}
					WebProcessStatistics.s_managedHeapSize = GC.GetTotalMemory(false);
					WebProcessStatistics.s_appdomainCount = HostingEnvironment.AppDomainsCount;
					WebProcessStatistics.s_requestsExecuting = PerfCounters.GetGlobalCounter(GlobalPerfCounter.REQUESTS_CURRENT);
					WebProcessStatistics.s_requestsQueued = PerfCounters.GetGlobalCounter(GlobalPerfCounter.REQUESTS_QUEUED);
					WebProcessStatistics.s_requestsRejected = PerfCounters.GetGlobalCounter(GlobalPerfCounter.REQUESTS_REJECTED);
					WebProcessStatistics.s_lastUpdated = now;
				}
			}
		}

		// Token: 0x1700066D RID: 1645
		// (get) Token: 0x060015CF RID: 5583 RVA: 0x00043338 File Offset: 0x00041538
		public DateTime ProcessStartTime
		{
			get
			{
				this.Update();
				return WebProcessStatistics.s_startTime;
			}
		}

		// Token: 0x1700066E RID: 1646
		// (get) Token: 0x060015D0 RID: 5584 RVA: 0x00043345 File Offset: 0x00041545
		public int ThreadCount
		{
			get
			{
				this.Update();
				return WebProcessStatistics.s_threadCount;
			}
		}

		// Token: 0x1700066F RID: 1647
		// (get) Token: 0x060015D1 RID: 5585 RVA: 0x00043352 File Offset: 0x00041552
		public long WorkingSet
		{
			get
			{
				this.Update();
				return WebProcessStatistics.s_workingSet;
			}
		}

		// Token: 0x17000670 RID: 1648
		// (get) Token: 0x060015D2 RID: 5586 RVA: 0x0004335F File Offset: 0x0004155F
		public long PeakWorkingSet
		{
			get
			{
				this.Update();
				return WebProcessStatistics.s_peakWorkingSet;
			}
		}

		// Token: 0x17000671 RID: 1649
		// (get) Token: 0x060015D3 RID: 5587 RVA: 0x0004336C File Offset: 0x0004156C
		public long ManagedHeapSize
		{
			get
			{
				this.Update();
				return WebProcessStatistics.s_managedHeapSize;
			}
		}

		// Token: 0x17000672 RID: 1650
		// (get) Token: 0x060015D4 RID: 5588 RVA: 0x00043379 File Offset: 0x00041579
		public int AppDomainCount
		{
			get
			{
				this.Update();
				return WebProcessStatistics.s_appdomainCount;
			}
		}

		// Token: 0x17000673 RID: 1651
		// (get) Token: 0x060015D5 RID: 5589 RVA: 0x00043386 File Offset: 0x00041586
		public int RequestsExecuting
		{
			get
			{
				this.Update();
				return WebProcessStatistics.s_requestsExecuting;
			}
		}

		// Token: 0x17000674 RID: 1652
		// (get) Token: 0x060015D6 RID: 5590 RVA: 0x00043393 File Offset: 0x00041593
		public int RequestsQueued
		{
			get
			{
				this.Update();
				return WebProcessStatistics.s_requestsQueued;
			}
		}

		// Token: 0x17000675 RID: 1653
		// (get) Token: 0x060015D7 RID: 5591 RVA: 0x000433A0 File Offset: 0x000415A0
		public int RequestsRejected
		{
			get
			{
				this.Update();
				return WebProcessStatistics.s_requestsRejected;
			}
		}

		// Token: 0x060015D8 RID: 5592 RVA: 0x000433B0 File Offset: 0x000415B0
		public virtual void FormatToString(WebEventFormatter formatter)
		{
			formatter.AppendLine(WebBaseEvent.FormatResourceStringWithCache("Webevent_event_process_start_time", this.ProcessStartTime.ToString(CultureInfo.InstalledUICulture)));
			formatter.AppendLine(WebBaseEvent.FormatResourceStringWithCache("Webevent_event_thread_count", this.ThreadCount.ToString(CultureInfo.InstalledUICulture)));
			formatter.AppendLine(WebBaseEvent.FormatResourceStringWithCache("Webevent_event_working_set", this.WorkingSet.ToString(CultureInfo.InstalledUICulture)));
			formatter.AppendLine(WebBaseEvent.FormatResourceStringWithCache("Webevent_event_peak_working_set", this.PeakWorkingSet.ToString(CultureInfo.InstalledUICulture)));
			formatter.AppendLine(WebBaseEvent.FormatResourceStringWithCache("Webevent_event_managed_heap_size", this.ManagedHeapSize.ToString(CultureInfo.InstalledUICulture)));
			formatter.AppendLine(WebBaseEvent.FormatResourceStringWithCache("Webevent_event_application_domain_count", this.AppDomainCount.ToString(CultureInfo.InstalledUICulture)));
			formatter.AppendLine(WebBaseEvent.FormatResourceStringWithCache("Webevent_event_requests_executing", this.RequestsExecuting.ToString(CultureInfo.InstalledUICulture)));
			formatter.AppendLine(WebBaseEvent.FormatResourceStringWithCache("Webevent_event_request_queued", this.RequestsQueued.ToString(CultureInfo.InstalledUICulture)));
			formatter.AppendLine(WebBaseEvent.FormatResourceStringWithCache("Webevent_event_request_rejected", this.RequestsRejected.ToString(CultureInfo.InstalledUICulture)));
		}

		// Token: 0x04001656 RID: 5718
		private static DateTime s_startTime = DateTime.MinValue;

		// Token: 0x04001657 RID: 5719
		private static DateTime s_lastUpdated = DateTime.MinValue;

		// Token: 0x04001658 RID: 5720
		private static int s_threadCount;

		// Token: 0x04001659 RID: 5721
		private static long s_workingSet;

		// Token: 0x0400165A RID: 5722
		private static long s_peakWorkingSet;

		// Token: 0x0400165B RID: 5723
		private static long s_managedHeapSize;

		// Token: 0x0400165C RID: 5724
		private static int s_appdomainCount;

		// Token: 0x0400165D RID: 5725
		private static int s_requestsExecuting;

		// Token: 0x0400165E RID: 5726
		private static int s_requestsQueued;

		// Token: 0x0400165F RID: 5727
		private static int s_requestsRejected;

		// Token: 0x04001660 RID: 5728
		private static bool s_getCurrentProcFailed = false;

		// Token: 0x04001661 RID: 5729
		private static object s_lockObject = new object();

		// Token: 0x04001662 RID: 5730
		private static TimeSpan TS_ONE_SECOND = new TimeSpan(0, 0, 1);
	}
}
