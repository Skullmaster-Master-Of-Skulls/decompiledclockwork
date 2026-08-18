using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Web.Configuration;
using System.Web.Util;

namespace System.Web.Hosting
{
	// Token: 0x0200078C RID: 1932
	internal class LowPhysicalMemoryMonitor
	{
		// Token: 0x17001B11 RID: 6929
		// (get) Token: 0x06005C78 RID: 23672 RVA: 0x0013FFF1 File Offset: 0x0013E1F1
		internal int PressureLast
		{
			get
			{
				return this._pressureHist[this._i0];
			}
		}

		// Token: 0x17001B12 RID: 6930
		// (get) Token: 0x06005C79 RID: 23673 RVA: 0x00140000 File Offset: 0x0013E200
		internal int PressureHigh
		{
			get
			{
				return this._pressureHigh;
			}
		}

		// Token: 0x17001B13 RID: 6931
		// (get) Token: 0x06005C7A RID: 23674 RVA: 0x00140008 File Offset: 0x0013E208
		internal int PressureLow
		{
			get
			{
				return this._pressureLow;
			}
		}

		// Token: 0x06005C7B RID: 23675 RVA: 0x00140010 File Offset: 0x0013E210
		internal LowPhysicalMemoryMonitor()
		{
			long s_totalPhysical = AspNetMemoryMonitor.s_totalPhysical;
			if (s_totalPhysical >= 4294967296L)
			{
				this._pressureHigh = 99;
			}
			else if (s_totalPhysical >= (long)((ulong)-2147483648))
			{
				this._pressureHigh = 98;
			}
			else if (s_totalPhysical >= 1073741824L)
			{
				this._pressureHigh = 97;
			}
			else if (s_totalPhysical >= 805306368L)
			{
				this._pressureHigh = 96;
			}
			else
			{
				this._pressureHigh = 95;
			}
			this._pressureLow = this._pressureHigh - 9;
			this.InitHistory();
			this._appManager = ApplicationManager.GetApplicationManager();
			this._observers = new List<IObserver<LowPhysicalMemoryInfo>>();
			CacheSection cacheSection = null;
			RuntimeConfig appLKGConfig = RuntimeConfig.GetAppLKGConfig();
			try
			{
				RuntimeConfig appConfig = RuntimeConfig.GetAppConfig();
				cacheSection = appConfig.Cache;
			}
			catch (Exception)
			{
				cacheSection = appLKGConfig.Cache;
			}
			this.ReadConfig(cacheSection);
			PerfCounters.SetCounter(AppPerfCounter.CACHE_PERCENT_MACH_MEM_LIMIT_USED_BASE, this._pressureHigh);
			this._timer = new Timer(new TimerCallback(this.MonitorThread), null, this._currentPollInterval, this._currentPollInterval);
		}

		// Token: 0x06005C7C RID: 23676 RVA: 0x00140128 File Offset: 0x0013E328
		private void InitHistory()
		{
			int currentPressure = this.GetCurrentPressure();
			this._pressureHist = new int[6];
			for (int i = 0; i < 6; i++)
			{
				this._pressureHist[i] = currentPressure;
			}
		}

		// Token: 0x06005C7D RID: 23677 RVA: 0x00140160 File Offset: 0x0013E360
		private void ReadConfig(CacheSection cacheSection)
		{
			if (cacheSection == null)
			{
				return;
			}
			LowPhysicalMemoryMonitor.s_configuredPollInterval = (int)Math.Min(cacheSection.PrivateBytesPollTime.TotalMilliseconds, 2147483647.0);
			int percentagePhysicalMemoryUsedLimit = cacheSection.PercentagePhysicalMemoryUsedLimit;
			if (percentagePhysicalMemoryUsedLimit == 0)
			{
				return;
			}
			this._pressureHigh = Math.Max(3, percentagePhysicalMemoryUsedLimit);
			this._pressureLow = Math.Max(1, this._pressureHigh - 9);
		}

		// Token: 0x06005C7E RID: 23678 RVA: 0x001401C0 File Offset: 0x0013E3C0
		private void Update()
		{
			int currentPressure = this.GetCurrentPressure();
			this._i0 = (this._i0 + 1) % 6;
			this._pressureHist[this._i0] = currentPressure;
		}

		// Token: 0x06005C7F RID: 23679 RVA: 0x001401F4 File Offset: 0x0013E3F4
		private int GetCurrentPressure()
		{
			UnsafeNativeMethods.MEMORYSTATUSEX memorystatusex = default(UnsafeNativeMethods.MEMORYSTATUSEX);
			memorystatusex.Init();
			if (UnsafeNativeMethods.GlobalMemoryStatusEx(ref memorystatusex) == 0)
			{
				return 0;
			}
			int dwMemoryLoad = memorystatusex.dwMemoryLoad;
			if (this._pressureHigh != 0)
			{
				PerfCounters.SetCounter(AppPerfCounter.CACHE_PERCENT_MACH_MEM_LIMIT_USED, dwMemoryLoad);
			}
			return dwMemoryLoad;
		}

		// Token: 0x06005C80 RID: 23680 RVA: 0x00140234 File Offset: 0x0013E434
		internal void AdjustTimer(bool disable = false)
		{
			object timerLock = this._timerLock;
			lock (timerLock)
			{
				if (this._timer != null)
				{
					if (disable)
					{
						this._currentPollInterval = -1;
						this._timer.Change(-1, -1);
					}
					else
					{
						int num = LowPhysicalMemoryMonitor.s_configuredPollInterval;
						if (this.PressureLast >= this.PressureHigh)
						{
							num = Math.Min(num, 5000);
						}
						else if (this.PressureLast > this.PressureLow / 2)
						{
							num = Math.Min(num, 30000);
						}
						if (num != this._currentPollInterval)
						{
							this._currentPollInterval = num;
							this._timer.Change(this._currentPollInterval, this._currentPollInterval);
						}
					}
				}
			}
		}

		// Token: 0x06005C81 RID: 23681 RVA: 0x001402FC File Offset: 0x0013E4FC
		internal void MonitorThread(object state)
		{
			if (Interlocked.Exchange(ref this._inMonitorThread, 1) != 0)
			{
				return;
			}
			try
			{
				if (this._timer != null)
				{
					this.Update();
					this.AdjustTimer(false);
					if (this.PressureLast >= this.PressureHigh)
					{
						long num = HttpRuntime.Cache.InternalCache.ItemCount + HttpRuntime.Cache.ObjectCache.ItemCount;
						Stopwatch stopwatch = Stopwatch.StartNew();
						bool flag = this.RaiseLowMemoryEvent(this.PressureLast, this.PressureHigh);
						stopwatch.Stop();
						long num2 = Math.Max(0L, num - HttpRuntime.Cache.InternalCache.ItemCount - HttpRuntime.Cache.ObjectCache.ItemCount);
						if (flag && !this._appManager.ShutdownInProgress)
						{
							Stopwatch stopwatch2 = Stopwatch.StartNew();
							GC.Collect();
							stopwatch2.Stop();
						}
					}
				}
			}
			finally
			{
				Interlocked.Exchange(ref this._inMonitorThread, 0);
			}
		}

		// Token: 0x06005C82 RID: 23682 RVA: 0x001403F4 File Offset: 0x0013E5F4
		private bool RaiseLowMemoryEvent(int current, int limit)
		{
			LowPhysicalMemoryInfo lowPhysicalMemoryInfo = new LowPhysicalMemoryInfo(current, limit);
			List<IObserver<LowPhysicalMemoryInfo>> observers = this._observers;
			IObserver<LowPhysicalMemoryInfo>[] array;
			lock (observers)
			{
				array = this._observers.ToArray();
			}
			foreach (IObserver<LowPhysicalMemoryInfo> observer in array)
			{
				try
				{
					observer.OnNext(lowPhysicalMemoryInfo);
				}
				catch (Exception e)
				{
					Misc.ReportUnhandledException(e, new string[]
					{
						SR.GetString("Unhandled_Monitor_Exception", new object[]
						{
							"RaiseLowMemoryEvent",
							"LowMemoryMonitor"
						})
					});
				}
			}
			return lowPhysicalMemoryInfo.RequestGC;
		}

		// Token: 0x06005C83 RID: 23683 RVA: 0x001404B0 File Offset: 0x0013E6B0
		internal void Subscribe(IObserver<LowPhysicalMemoryInfo> observer)
		{
			if (this._observers != null && observer != null)
			{
				List<IObserver<LowPhysicalMemoryInfo>> observers = this._observers;
				lock (observers)
				{
					if (this._observers != null && observer != null)
					{
						this._observers.Add(observer);
					}
				}
			}
		}

		// Token: 0x06005C84 RID: 23684 RVA: 0x0014050C File Offset: 0x0013E70C
		internal void Unsubscribe(IObserver<LowPhysicalMemoryInfo> observer)
		{
			if (this._observers != null && observer != null)
			{
				List<IObserver<LowPhysicalMemoryInfo>> observers = this._observers;
				lock (observers)
				{
					if (this._observers != null && observer != null)
					{
						this._observers.Remove(observer);
					}
				}
			}
		}

		// Token: 0x06005C85 RID: 23685 RVA: 0x0014056C File Offset: 0x0013E76C
		public void Start()
		{
			this.AdjustTimer(false);
		}

		// Token: 0x06005C86 RID: 23686 RVA: 0x00140575 File Offset: 0x0013E775
		public void Stop()
		{
			this.AdjustTimer(true);
		}

		// Token: 0x040030BB RID: 12475
		private const int HISTORY_COUNT = 6;

		// Token: 0x040030BC RID: 12476
		private static int s_configuredPollInterval = int.MaxValue;

		// Token: 0x040030BD RID: 12477
		private int _pressureHigh;

		// Token: 0x040030BE RID: 12478
		private int _pressureLow;

		// Token: 0x040030BF RID: 12479
		private int[] _pressureHist;

		// Token: 0x040030C0 RID: 12480
		private int _i0;

		// Token: 0x040030C1 RID: 12481
		private object _timerLock = new object();

		// Token: 0x040030C2 RID: 12482
		private Timer _timer;

		// Token: 0x040030C3 RID: 12483
		private int _currentPollInterval = -1;

		// Token: 0x040030C4 RID: 12484
		private int _inMonitorThread;

		// Token: 0x040030C5 RID: 12485
		private ApplicationManager _appManager;

		// Token: 0x040030C6 RID: 12486
		private List<IObserver<LowPhysicalMemoryInfo>> _observers;
	}
}
