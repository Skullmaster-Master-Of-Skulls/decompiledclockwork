using System;
using System.Collections.Generic;
using System.Data.Entity.Internal;
using System.Diagnostics.CodeAnalysis;
using System.Threading;

namespace System.Data.Entity.Core.Common.QueryCache
{
	// Token: 0x020002FB RID: 763
	internal class QueryCacheManager : IDisposable
	{
		// Token: 0x06001AD6 RID: 6870 RVA: 0x00085F28 File Offset: 0x00084128
		internal static QueryCacheManager Create()
		{
			QueryCacheConfig queryCache = AppConfig.DefaultInstance.QueryCache;
			int queryCacheSize = queryCache.GetQueryCacheSize();
			int recycleMillis = queryCache.GetCleaningIntervalInSeconds() * 1000;
			return new QueryCacheManager(queryCacheSize, 0.8f, recycleMillis);
		}

		// Token: 0x06001AD7 RID: 6871 RVA: 0x00085F60 File Offset: 0x00084160
		private QueryCacheManager(int maximumSize, float loadFactor, int recycleMillis)
		{
			this._maxNumberOfEntries = maximumSize;
			this._sweepingTriggerHighMark = (int)((float)this._maxNumberOfEntries * loadFactor);
			this._evictionTimer = new QueryCacheManager.EvictionTimer(this, recycleMillis);
		}

		// Token: 0x06001AD8 RID: 6872 RVA: 0x00085FB0 File Offset: 0x000841B0
		internal bool TryLookupAndAdd(QueryCacheEntry inQueryCacheEntry, out QueryCacheEntry outQueryCacheEntry)
		{
			outQueryCacheEntry = null;
			bool result;
			lock (this._cacheDataLock)
			{
				if (!this._cacheData.TryGetValue(inQueryCacheEntry.QueryCacheKey, out outQueryCacheEntry))
				{
					this._cacheData.Add(inQueryCacheEntry.QueryCacheKey, inQueryCacheEntry);
					if (this._cacheData.Count > this._sweepingTriggerHighMark)
					{
						this._evictionTimer.Start();
					}
					result = false;
				}
				else
				{
					outQueryCacheEntry.QueryCacheKey.UpdateHit();
					result = true;
				}
			}
			return result;
		}

		// Token: 0x06001AD9 RID: 6873 RVA: 0x00086044 File Offset: 0x00084244
		internal bool TryCacheLookup<TK, TE>(TK key, out TE value) where TK : QueryCacheKey
		{
			value = default(TE);
			QueryCacheEntry queryCacheEntry = null;
			bool flag = this.TryInternalCacheLookup(key, out queryCacheEntry);
			if (flag)
			{
				value = (TE)((object)queryCacheEntry.GetTarget());
			}
			return flag;
		}

		// Token: 0x06001ADA RID: 6874 RVA: 0x00086080 File Offset: 0x00084280
		internal void Clear()
		{
			lock (this._cacheDataLock)
			{
				this._cacheData.Clear();
			}
		}

		// Token: 0x06001ADB RID: 6875 RVA: 0x000860C8 File Offset: 0x000842C8
		private bool TryInternalCacheLookup(QueryCacheKey queryCacheKey, out QueryCacheEntry queryCacheEntry)
		{
			queryCacheEntry = null;
			bool flag = false;
			lock (this._cacheDataLock)
			{
				flag = this._cacheData.TryGetValue(queryCacheKey, out queryCacheEntry);
			}
			if (flag)
			{
				queryCacheEntry.QueryCacheKey.UpdateHit();
			}
			return flag;
		}

		// Token: 0x06001ADC RID: 6876 RVA: 0x00086128 File Offset: 0x00084328
		private static void CacheRecyclerHandler(object state)
		{
			((QueryCacheManager)state).SweepCache();
		}

		// Token: 0x06001ADD RID: 6877 RVA: 0x00086138 File Offset: 0x00084338
		private void SweepCache()
		{
			if (!this._evictionTimer.Suspend())
			{
				return;
			}
			bool flag = false;
			lock (this._cacheDataLock)
			{
				if (this._cacheData.Count > this._sweepingTriggerHighMark)
				{
					uint num = 0U;
					List<QueryCacheKey> list = new List<QueryCacheKey>(this._cacheData.Count);
					list.AddRange(this._cacheData.Keys);
					for (int i = 0; i < list.Count; i++)
					{
						if (list[i].HitCount == 0U)
						{
							this._cacheData.Remove(list[i]);
							num += 1U;
						}
						else
						{
							int num2 = list[i].AgingIndex + 1;
							if (num2 > QueryCacheManager._agingMaxIndex)
							{
								num2 = QueryCacheManager._agingMaxIndex;
							}
							list[i].AgingIndex = num2;
							list[i].HitCount = list[i].HitCount >> QueryCacheManager._agingFactor[num2];
						}
					}
				}
				else
				{
					this._evictionTimer.Stop();
					flag = true;
				}
			}
			if (!flag)
			{
				this._evictionTimer.Resume();
			}
		}

		// Token: 0x06001ADE RID: 6878 RVA: 0x0008626C File Offset: 0x0008446C
		public void Dispose()
		{
			GC.SuppressFinalize(this);
			if (this._evictionTimer.Stop())
			{
				this.Clear();
			}
		}

		// Token: 0x04000960 RID: 2400
		private readonly object _cacheDataLock = new object();

		// Token: 0x04000961 RID: 2401
		private readonly Dictionary<QueryCacheKey, QueryCacheEntry> _cacheData = new Dictionary<QueryCacheKey, QueryCacheEntry>(32);

		// Token: 0x04000962 RID: 2402
		private readonly int _maxNumberOfEntries;

		// Token: 0x04000963 RID: 2403
		private readonly int _sweepingTriggerHighMark;

		// Token: 0x04000964 RID: 2404
		private readonly QueryCacheManager.EvictionTimer _evictionTimer;

		// Token: 0x04000965 RID: 2405
		private static readonly int[] _agingFactor = new int[]
		{
			1,
			1,
			2,
			4,
			8,
			16
		};

		// Token: 0x04000966 RID: 2406
		private static readonly int _agingMaxIndex = QueryCacheManager._agingFactor.Length - 1;

		// Token: 0x020002FC RID: 764
		[SuppressMessage("Microsoft.Design", "CA1001:TypesThatOwnDisposableFieldsShouldBeDisposable")]
		private sealed class EvictionTimer
		{
			// Token: 0x06001AE0 RID: 6880 RVA: 0x000862C6 File Offset: 0x000844C6
			internal EvictionTimer(QueryCacheManager cacheManager, int recyclePeriod)
			{
				this._cacheManager = cacheManager;
				this._period = recyclePeriod;
			}

			// Token: 0x06001AE1 RID: 6881 RVA: 0x000862E8 File Offset: 0x000844E8
			internal void Start()
			{
				lock (this._sync)
				{
					if (this._timer == null)
					{
						this._timer = new Timer(new TimerCallback(QueryCacheManager.CacheRecyclerHandler), this._cacheManager, this._period, this._period);
					}
				}
			}

			// Token: 0x06001AE2 RID: 6882 RVA: 0x00086354 File Offset: 0x00084554
			internal bool Stop()
			{
				bool result;
				lock (this._sync)
				{
					if (this._timer != null)
					{
						this._timer.Dispose();
						this._timer = null;
						result = true;
					}
					else
					{
						result = false;
					}
				}
				return result;
			}

			// Token: 0x06001AE3 RID: 6883 RVA: 0x000863B0 File Offset: 0x000845B0
			internal bool Suspend()
			{
				bool result;
				lock (this._sync)
				{
					if (this._timer != null)
					{
						this._timer.Change(-1, -1);
						result = true;
					}
					else
					{
						result = false;
					}
				}
				return result;
			}

			// Token: 0x06001AE4 RID: 6884 RVA: 0x00086408 File Offset: 0x00084608
			internal void Resume()
			{
				lock (this._sync)
				{
					if (this._timer != null)
					{
						this._timer.Change(this._period, this._period);
					}
				}
			}

			// Token: 0x04000967 RID: 2407
			private readonly object _sync = new object();

			// Token: 0x04000968 RID: 2408
			private readonly int _period;

			// Token: 0x04000969 RID: 2409
			private readonly QueryCacheManager _cacheManager;

			// Token: 0x0400096A RID: 2410
			private Timer _timer;
		}
	}
}
