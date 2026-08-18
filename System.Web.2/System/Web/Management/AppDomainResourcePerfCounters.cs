using System;
using System.Threading;

namespace System.Web.Management
{
	// Token: 0x0200016C RID: 364
	internal class AppDomainResourcePerfCounters
	{
		// Token: 0x06001458 RID: 5208 RVA: 0x0003C37C File Offset: 0x0003A57C
		internal static void Init()
		{
			if (AppDomainResourcePerfCounters._fInit)
			{
				return;
			}
			object initLock = AppDomainResourcePerfCounters._InitLock;
			lock (initLock)
			{
				if (!AppDomainResourcePerfCounters._fInit)
				{
					if (AppDomain.MonitoringIsEnabled)
					{
						PerfCounters.SetCounter(AppPerfCounter.APP_CPU_USED_BASE, 100);
						AppDomainResourcePerfCounters._Timer = new Timer(new TimerCallback(new AppDomainResourcePerfCounters().TimerCallback), null, 5000U, 5000U);
					}
					AppDomainResourcePerfCounters._fInit = true;
				}
			}
		}

		// Token: 0x06001459 RID: 5209 RVA: 0x0003C404 File Offset: 0x0003A604
		internal static void Stop()
		{
			if (AppDomainResourcePerfCounters._Timer == null)
			{
				return;
			}
			AppDomainResourcePerfCounters._StopRequested = true;
			object initLock = AppDomainResourcePerfCounters._InitLock;
			lock (initLock)
			{
				if (AppDomainResourcePerfCounters._Timer != null)
				{
					((IDisposable)AppDomainResourcePerfCounters._Timer).Dispose();
					AppDomainResourcePerfCounters._Timer = null;
				}
				goto IL_48;
			}
			IL_41:
			Thread.Sleep(100);
			IL_48:
			if (AppDomainResourcePerfCounters._inProgressLock == 0)
			{
				return;
			}
			goto IL_41;
		}

		// Token: 0x0600145A RID: 5210 RVA: 0x0003C470 File Offset: 0x0003A670
		private AppDomainResourcePerfCounters()
		{
			this._TotalCPUTime = AppDomain.CurrentDomain.MonitoringTotalProcessorTime;
			this._LastCollectTime = DateTime.UtcNow;
		}

		// Token: 0x0600145B RID: 5211 RVA: 0x0003C494 File Offset: 0x0003A694
		private void TimerCallback(object state)
		{
			if (AppDomainResourcePerfCounters._StopRequested || !AppDomain.MonitoringIsEnabled || Interlocked.Exchange(ref AppDomainResourcePerfCounters._inProgressLock, 1) != 0)
			{
				return;
			}
			try
			{
				this.SetPerfCounters();
			}
			catch
			{
			}
			finally
			{
				Interlocked.Exchange(ref AppDomainResourcePerfCounters._inProgressLock, 0);
			}
		}

		// Token: 0x0600145C RID: 5212 RVA: 0x0003C4F4 File Offset: 0x0003A6F4
		private void SetPerfCounters()
		{
			long val = AppDomain.CurrentDomain.MonitoringSurvivedMemorySize / 1024L;
			this._MemUsageLastReported = (int)Math.Min(2147483647L, Math.Max(0L, val));
			PerfCounters.SetCounter(AppPerfCounter.APP_MEMORY_USED, this._MemUsageLastReported);
			DateTime utcNow = DateTime.UtcNow;
			TimeSpan monitoringTotalProcessorTime = AppDomain.CurrentDomain.MonitoringTotalProcessorTime;
			double totalMilliseconds = (utcNow - this._LastCollectTime).TotalMilliseconds;
			double totalMilliseconds2 = (monitoringTotalProcessorTime - this._TotalCPUTime).TotalMilliseconds;
			int val2 = (int)(totalMilliseconds2 * 100.0 / totalMilliseconds);
			this._CPUUsageLastReported = Math.Min(100, Math.Max(0, val2));
			PerfCounters.SetCounter(AppPerfCounter.APP_CPU_USED, this._CPUUsageLastReported);
			this._TotalCPUTime = monitoringTotalProcessorTime;
			this._LastCollectTime = utcNow;
		}

		// Token: 0x0400152A RID: 5418
		private const uint NUM_SECONDS_TO_POLL = 5U;

		// Token: 0x0400152B RID: 5419
		private static bool _fInit = false;

		// Token: 0x0400152C RID: 5420
		private static object _InitLock = new object();

		// Token: 0x0400152D RID: 5421
		private static Timer _Timer = null;

		// Token: 0x0400152E RID: 5422
		private static int _inProgressLock = 0;

		// Token: 0x0400152F RID: 5423
		private static bool _StopRequested = false;

		// Token: 0x04001530 RID: 5424
		private int _MemUsageLastReported;

		// Token: 0x04001531 RID: 5425
		private int _CPUUsageLastReported;

		// Token: 0x04001532 RID: 5426
		private TimeSpan _TotalCPUTime;

		// Token: 0x04001533 RID: 5427
		private DateTime _LastCollectTime;
	}
}
