using System;
using System.Threading;
using System.Web.Configuration;
using System.Web.Hosting;

namespace System.Web.Caching
{
	// Token: 0x02000881 RID: 2177
	internal sealed class CacheSizeMonitor
	{
		// Token: 0x06006684 RID: 26244 RVA: 0x001693F8 File Offset: 0x001675F8
		internal CacheSizeMonitor(SRefMultiple sizedRef)
		{
			this._sizedRef = sizedRef;
			this._gen2Count = GC.CollectionCount(2);
			this._cacheSizeSamples = new long[2];
			this._cacheSizeSampleTimes = new DateTime[2];
			this._pressureHigh = 99;
			this._pressureMiddle = 98;
			this._pressureLow = 97;
			this.InitHistory();
		}

		// Token: 0x17001CAD RID: 7341
		// (get) Token: 0x06006685 RID: 26245 RVA: 0x00169466 File Offset: 0x00167666
		internal static int PollInterval
		{
			get
			{
				return CacheSizeMonitor.s_pollInterval;
			}
		}

		// Token: 0x17001CAE RID: 7342
		// (get) Token: 0x06006686 RID: 26246 RVA: 0x0016946D File Offset: 0x0016766D
		internal int PressureLast
		{
			get
			{
				return this._pressureHist[this._i0];
			}
		}

		// Token: 0x17001CAF RID: 7343
		// (get) Token: 0x06006687 RID: 26247 RVA: 0x0016947C File Offset: 0x0016767C
		internal int PressureAvg
		{
			get
			{
				return this._pressureAvg;
			}
		}

		// Token: 0x17001CB0 RID: 7344
		// (get) Token: 0x06006688 RID: 26248 RVA: 0x00169484 File Offset: 0x00167684
		internal int PressureHigh
		{
			get
			{
				return this._pressureHigh;
			}
		}

		// Token: 0x17001CB1 RID: 7345
		// (get) Token: 0x06006689 RID: 26249 RVA: 0x0016948C File Offset: 0x0016768C
		internal int PressureLow
		{
			get
			{
				return this._pressureLow;
			}
		}

		// Token: 0x17001CB2 RID: 7346
		// (get) Token: 0x0600668A RID: 26250 RVA: 0x00169494 File Offset: 0x00167694
		internal int PressureMiddle
		{
			get
			{
				return this._pressureMiddle;
			}
		}

		// Token: 0x0600668B RID: 26251 RVA: 0x0016949C File Offset: 0x0016769C
		internal bool IsAboveHighPressure()
		{
			return this.PressureLast >= this.PressureHigh;
		}

		// Token: 0x0600668C RID: 26252 RVA: 0x001694AF File Offset: 0x001676AF
		internal bool IsAboveMediumPressure()
		{
			return this.PressureLast > this.PressureMiddle;
		}

		// Token: 0x0600668D RID: 26253 RVA: 0x001694C0 File Offset: 0x001676C0
		private void InitHistory()
		{
			int currentPressure = this.GetCurrentPressure();
			this._pressureHist = new int[6];
			for (int i = 0; i < 6; i++)
			{
				this._pressureHist[i] = currentPressure;
				this._pressureTotal += currentPressure;
			}
			this._pressureAvg = currentPressure;
		}

		// Token: 0x0600668E RID: 26254 RVA: 0x0016950C File Offset: 0x0016770C
		internal void Update()
		{
			int currentPressure = this.GetCurrentPressure();
			this._i0 = (this._i0 + 1) % 6;
			this._pressureTotal -= this._pressureHist[this._i0];
			this._pressureTotal += currentPressure;
			this._pressureHist[this._i0] = currentPressure;
			this._pressureAvg = this._pressureTotal / 6;
		}

		// Token: 0x0600668F RID: 26255 RVA: 0x00169574 File Offset: 0x00167774
		internal void SetTrimStats(long trimDurationTicks, long totalCountBeforeTrim, long trimCount)
		{
			this._lastTrimDurationTicks = trimDurationTicks;
			int num = GC.CollectionCount(2);
			if (num != this._lastTrimGen2Count)
			{
				this._lastTrimTime = DateTime.UtcNow;
				this._totalCountBeforeTrim = totalCountBeforeTrim;
				this._lastTrimCount = trimCount;
			}
			else
			{
				this._lastTrimCount += trimCount;
			}
			this._lastTrimGen2Count = num;
			this._lastTrimPercent = (int)(this._lastTrimCount * 100L / this._totalCountBeforeTrim);
		}

		// Token: 0x06006690 RID: 26256 RVA: 0x001695E0 File Offset: 0x001677E0
		internal void Dispose()
		{
			SRefMultiple sizedRef = this._sizedRef;
			if (sizedRef != null && Interlocked.CompareExchange<SRefMultiple>(ref this._sizedRef, null, sizedRef) == sizedRef)
			{
				sizedRef.Dispose();
			}
		}

		// Token: 0x06006691 RID: 26257 RVA: 0x00169610 File Offset: 0x00167810
		internal void ReadConfig(CacheSection cacheSection)
		{
			long privateBytesLimit = cacheSection.PrivateBytesLimit;
			this._memoryLimit = AspNetMemoryMonitor.ConfiguredProcessMemoryLimit;
			if (privateBytesLimit == 0L && this._memoryLimit == 0L)
			{
				this._memoryLimit = AspNetMemoryMonitor.ProcessPrivateBytesLimit;
			}
			else if (privateBytesLimit != 0L && this._memoryLimit != 0L)
			{
				this._memoryLimit = Math.Min(this._memoryLimit, privateBytesLimit);
			}
			else if (privateBytesLimit != 0L)
			{
				this._memoryLimit = privateBytesLimit;
			}
			if (this._memoryLimit > 0L)
			{
				if (CacheSizeMonitor.s_pid == 0U)
				{
					CacheSizeMonitor.s_pid = (uint)SafeNativeMethods.GetCurrentProcessId();
				}
				this._pressureHigh = 100;
				this._pressureMiddle = 90;
				this._pressureLow = 80;
			}
			CacheSizeMonitor.s_pollInterval = (int)Math.Min(cacheSection.PrivateBytesPollTime.TotalMilliseconds, 2147483647.0);
			PerfCounters.SetCounter(AppPerfCounter.CACHE_PERCENT_PROC_MEM_LIMIT_USED_BASE, (int)(this._memoryLimit >> 10));
		}

		// Token: 0x06006692 RID: 26258 RVA: 0x001696D8 File Offset: 0x001678D8
		private int GetCurrentPressure()
		{
			int num = GC.CollectionCount(2);
			SRefMultiple sizedRef = this._sizedRef;
			if (num != this._gen2Count && sizedRef != null)
			{
				this._gen2Count = num;
				this._idx ^= 1;
				this._cacheSizeSampleTimes[this._idx] = DateTime.UtcNow;
				this._cacheSizeSamples[this._idx] = sizedRef.ApproximateSize;
			}
			if (this._memoryLimit <= 0L)
			{
				return 0;
			}
			long num2 = this._cacheSizeSamples[this._idx];
			if (num2 > this._memoryLimit)
			{
				num2 = this._memoryLimit;
			}
			PerfCounters.SetCounter(AppPerfCounter.CACHE_PERCENT_PROC_MEM_LIMIT_USED, (int)(num2 >> 10));
			return (int)(num2 * 100L / this._memoryLimit);
		}

		// Token: 0x06006693 RID: 26259 RVA: 0x00169784 File Offset: 0x00167984
		internal int GetPercentToTrim()
		{
			int num = GC.CollectionCount(2);
			int result = 0;
			if (num != this._lastTrimGen2Count && this.IsAboveHighPressure())
			{
				long num2 = this._cacheSizeSamples[this._idx];
				if (num2 > this._memoryLimit)
				{
					result = Math.Min(100, (int)((num2 - this._memoryLimit) * 100L / num2));
				}
			}
			return result;
		}

		// Token: 0x06006694 RID: 26260 RVA: 0x001697DA File Offset: 0x001679DA
		internal bool HasLimit()
		{
			return this._memoryLimit != 0L;
		}

		// Token: 0x040034CF RID: 13519
		private const int SAMPLE_COUNT = 2;

		// Token: 0x040034D0 RID: 13520
		private const int HISTORY_COUNT = 6;

		// Token: 0x040034D1 RID: 13521
		private const int MEGABYTE_SHIFT = 20;

		// Token: 0x040034D2 RID: 13522
		private const int KILOBYTE_SHIFT = 10;

		// Token: 0x040034D3 RID: 13523
		private static uint s_pid;

		// Token: 0x040034D4 RID: 13524
		private static int s_pollInterval;

		// Token: 0x040034D5 RID: 13525
		private long[] _cacheSizeSamples;

		// Token: 0x040034D6 RID: 13526
		private DateTime[] _cacheSizeSampleTimes;

		// Token: 0x040034D7 RID: 13527
		private int _idx;

		// Token: 0x040034D8 RID: 13528
		private SRefMultiple _sizedRef;

		// Token: 0x040034D9 RID: 13529
		private int _gen2Count;

		// Token: 0x040034DA RID: 13530
		private long _memoryLimit;

		// Token: 0x040034DB RID: 13531
		private int _pressureHigh;

		// Token: 0x040034DC RID: 13532
		private int _pressureMiddle;

		// Token: 0x040034DD RID: 13533
		private int _pressureLow;

		// Token: 0x040034DE RID: 13534
		private int _i0;

		// Token: 0x040034DF RID: 13535
		private int[] _pressureHist;

		// Token: 0x040034E0 RID: 13536
		private int _pressureTotal;

		// Token: 0x040034E1 RID: 13537
		private int _pressureAvg;

		// Token: 0x040034E2 RID: 13538
		private DateTime _lastTrimTime = DateTime.MinValue;

		// Token: 0x040034E3 RID: 13539
		private long _lastTrimDurationTicks;

		// Token: 0x040034E4 RID: 13540
		private int _lastTrimPercent;

		// Token: 0x040034E5 RID: 13541
		private long _totalCountBeforeTrim;

		// Token: 0x040034E6 RID: 13542
		private long _lastTrimCount;

		// Token: 0x040034E7 RID: 13543
		private int _lastTrimGen2Count = -1;
	}
}
