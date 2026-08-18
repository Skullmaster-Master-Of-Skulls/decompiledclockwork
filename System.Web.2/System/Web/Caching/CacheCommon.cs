using System;
using System.Diagnostics;
using System.Threading;
using System.Web.Configuration;
using System.Web.Util;

namespace System.Web.Caching
{
	// Token: 0x0200086F RID: 2159
	internal class CacheCommon
	{
		// Token: 0x060065B2 RID: 26034 RVA: 0x001664F0 File Offset: 0x001646F0
		internal CacheCommon()
		{
			this._srefMultiple = new SRefMultiple();
			this._cacheSizeMonitor = new CacheSizeMonitor(this._srefMultiple);
			this._enableMemoryCollection = true;
			this._enableExpiration = true;
		}

		// Token: 0x060065B3 RID: 26035 RVA: 0x00166543 File Offset: 0x00164743
		internal void Dispose(bool disposing)
		{
			if (disposing && Interlocked.Exchange(ref this._disposed, 1) == 0)
			{
				this.EnableCacheMemoryTimer(false);
				this._cacheSizeMonitor.Dispose();
			}
		}

		// Token: 0x060065B4 RID: 26036 RVA: 0x00166568 File Offset: 0x00164768
		internal void AddSRefTarget(object o)
		{
			this._srefMultiple.AddSRefTarget(o);
		}

		// Token: 0x060065B5 RID: 26037 RVA: 0x00166576 File Offset: 0x00164776
		internal void SetCacheInternal(CacheInternal cacheInternal)
		{
			this._cacheInternal = cacheInternal;
		}

		// Token: 0x060065B6 RID: 26038 RVA: 0x00166580 File Offset: 0x00164780
		internal void ReadCacheInternalConfig(CacheSection cacheSection)
		{
			if (this._internalConfigRead)
			{
				return;
			}
			lock (this)
			{
				if (!this._internalConfigRead)
				{
					this._internalConfigRead = true;
					if (cacheSection != null)
					{
						this._enableMemoryCollection = !cacheSection.DisableMemoryCollection;
						this._enableExpiration = !cacheSection.DisableExpiration;
						this._cacheSizeMonitor.ReadConfig(cacheSection);
						this._currentPollInterval = CacheSizeMonitor.PollInterval;
						this.ResetFromConfigSettings();
					}
				}
			}
		}

		// Token: 0x060065B7 RID: 26039 RVA: 0x00166610 File Offset: 0x00164810
		internal void ResetFromConfigSettings()
		{
			this.EnableCacheMemoryTimer(this._enableMemoryCollection);
			this._cacheInternal.EnableExpirationTimer(this._enableExpiration);
		}

		// Token: 0x060065B8 RID: 26040 RVA: 0x00166630 File Offset: 0x00164830
		internal void EnableCacheMemoryTimer(bool enable)
		{
			object timerLock = this._timerLock;
			lock (timerLock)
			{
				if (enable)
				{
					if (this._timerHandleRef == null)
					{
						Timer t = new Timer(new TimerCallback(this.CacheManagerTimerCallback), null, this._currentPollInterval, this._currentPollInterval);
						this._timerHandleRef = new DisposableGCHandleRef<Timer>(t);
					}
					else
					{
						this._timerHandleRef.Target.Change(this._currentPollInterval, this._currentPollInterval);
					}
				}
				else
				{
					DisposableGCHandleRef<Timer> timerHandleRef = this._timerHandleRef;
					if (timerHandleRef != null && Interlocked.CompareExchange<DisposableGCHandleRef<Timer>>(ref this._timerHandleRef, null, timerHandleRef) == timerHandleRef)
					{
						timerHandleRef.Dispose();
					}
				}
			}
			if (!enable)
			{
				while (this._inCacheManagerThread != 0)
				{
					Thread.Sleep(100);
				}
			}
		}

		// Token: 0x060065B9 RID: 26041 RVA: 0x001666F8 File Offset: 0x001648F8
		private void AdjustTimer()
		{
			object timerLock = this._timerLock;
			lock (timerLock)
			{
				if (this._timerHandleRef != null)
				{
					if (this._cacheSizeMonitor.IsAboveHighPressure())
					{
						if (this._currentPollInterval > 5000)
						{
							this._currentPollInterval = 5000;
							this._timerHandleRef.Target.Change(this._currentPollInterval, this._currentPollInterval);
						}
					}
					else if (this._cacheSizeMonitor.PressureLast > this._cacheSizeMonitor.PressureLow / 2)
					{
						int num = Math.Min(CacheSizeMonitor.PollInterval, 30000);
						if (this._currentPollInterval != num)
						{
							this._currentPollInterval = num;
							this._timerHandleRef.Target.Change(this._currentPollInterval, this._currentPollInterval);
						}
					}
					else if (this._currentPollInterval != CacheSizeMonitor.PollInterval)
					{
						this._currentPollInterval = CacheSizeMonitor.PollInterval;
						this._timerHandleRef.Target.Change(this._currentPollInterval, this._currentPollInterval);
					}
				}
			}
		}

		// Token: 0x060065BA RID: 26042 RVA: 0x00166814 File Offset: 0x00164A14
		private void CacheManagerTimerCallback(object state)
		{
			this.CacheManagerThread(0);
		}

		// Token: 0x060065BB RID: 26043 RVA: 0x00166820 File Offset: 0x00164A20
		internal long CacheManagerThread(int minPercent)
		{
			if (Interlocked.Exchange(ref this._inCacheManagerThread, 1) != 0)
			{
				return 0L;
			}
			long result;
			try
			{
				if (this._timerHandleRef == null)
				{
					result = 0L;
				}
				else
				{
					this._cacheSizeMonitor.Update();
					this.AdjustTimer();
					int num = Math.Max(minPercent, this._cacheSizeMonitor.GetPercentToTrim());
					long totalCount = this._cacheInternal.TotalCount;
					Stopwatch stopwatch = Stopwatch.StartNew();
					long num2 = this._cacheInternal.TrimIfNecessary(num);
					stopwatch.Stop();
					if (num > 0 && num2 > 0L)
					{
						this._cacheSizeMonitor.SetTrimStats(stopwatch.Elapsed.Ticks, totalCount, num2);
					}
					result = num2;
				}
			}
			finally
			{
				Interlocked.Exchange(ref this._inCacheManagerThread, 0);
			}
			return result;
		}

		// Token: 0x0400344B RID: 13387
		internal const int MEMORYSTATUS_INTERVAL_5_SECONDS = 5000;

		// Token: 0x0400344C RID: 13388
		internal const int MEMORYSTATUS_INTERVAL_30_SECONDS = 30000;

		// Token: 0x0400344D RID: 13389
		internal CacheInternal _cacheInternal;

		// Token: 0x0400344E RID: 13390
		protected internal CacheSizeMonitor _cacheSizeMonitor;

		// Token: 0x0400344F RID: 13391
		private object _timerLock = new object();

		// Token: 0x04003450 RID: 13392
		private DisposableGCHandleRef<Timer> _timerHandleRef;

		// Token: 0x04003451 RID: 13393
		private int _currentPollInterval = 30000;

		// Token: 0x04003452 RID: 13394
		internal int _inCacheManagerThread;

		// Token: 0x04003453 RID: 13395
		internal bool _enableMemoryCollection;

		// Token: 0x04003454 RID: 13396
		internal bool _enableExpiration;

		// Token: 0x04003455 RID: 13397
		internal bool _internalConfigRead;

		// Token: 0x04003456 RID: 13398
		internal SRefMultiple _srefMultiple;

		// Token: 0x04003457 RID: 13399
		private int _disposed;
	}
}
