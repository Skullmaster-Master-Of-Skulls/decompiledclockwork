using System;
using System.Collections.Generic;
using System.Data.Entity.Util;
using System.Threading;

namespace System.Data.Common.QueryCache
{
	// Token: 0x020003DE RID: 990
	internal class QueryCacheManager : IDisposable
	{
		// Token: 0x06003531 RID: 13617 RVA: 0x000CF270 File Offset: 0x000CD470
		internal static QueryCacheManager Create()
		{
			return new QueryCacheManager(AppSettings.QueryCacheSize, 0.8f, 60000);
		}

		// Token: 0x06003532 RID: 13618 RVA: 0x000CF288 File Offset: 0x000CD488
		private QueryCacheManager(int maximumSize, float loadFactor, int recycleMillis)
		{
			this._maxNumberOfEntries = maximumSize;
			this._sweepingTriggerHighMark = (int)((float)this._maxNumberOfEntries * loadFactor);
			this._evictionTimer = new QueryCacheManager.EvictionTimer(this, recycleMillis);
		}

		// Token: 0x06003533 RID: 13619 RVA: 0x000CF2D8 File Offset: 0x000CD4D8
		internal bool TryLookupAndAdd(QueryCacheEntry inQueryCacheEntry, out QueryCacheEntry outQueryCacheEntry)
		{
			outQueryCacheEntry = null;
			object cacheDataLock = this._cacheDataLock;
			bool result;
			lock (cacheDataLock)
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

		// Token: 0x06003534 RID: 13620 RVA: 0x000CF36C File Offset: 0x000CD56C
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

		// Token: 0x06003535 RID: 13621 RVA: 0x000CF3A8 File Offset: 0x000CD5A8
		internal void Clear()
		{
			object cacheDataLock = this._cacheDataLock;
			lock (cacheDataLock)
			{
				this._cacheData.Clear();
			}
		}

		// Token: 0x06003536 RID: 13622 RVA: 0x000CF3F0 File Offset: 0x000CD5F0
		private bool TryInternalCacheLookup(QueryCacheKey queryCacheKey, out QueryCacheEntry queryCacheEntry)
		{
			queryCacheEntry = null;
			bool flag = false;
			object cacheDataLock = this._cacheDataLock;
			lock (cacheDataLock)
			{
				flag = this._cacheData.TryGetValue(queryCacheKey, out queryCacheEntry);
			}
			if (flag)
			{
				queryCacheEntry.QueryCacheKey.UpdateHit();
			}
			return flag;
		}

		// Token: 0x06003537 RID: 13623 RVA: 0x000CF450 File Offset: 0x000CD650
		private static void CacheRecyclerHandler(object state)
		{
			((QueryCacheManager)state).SweepCache();
		}

		// Token: 0x06003538 RID: 13624 RVA: 0x000CF460 File Offset: 0x000CD660
		private void SweepCache()
		{
			if (!this._evictionTimer.Suspend())
			{
				return;
			}
			bool flag = false;
			object cacheDataLock = this._cacheDataLock;
			lock (cacheDataLock)
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
							if (num2 > QueryCacheManager.AgingMaxIndex)
							{
								num2 = QueryCacheManager.AgingMaxIndex;
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

		// Token: 0x06003539 RID: 13625 RVA: 0x000CF5A8 File Offset: 0x000CD7A8
		public void Dispose()
		{
			GC.SuppressFinalize(this);
			if (this._evictionTimer.Stop())
			{
				this.Clear();
			}
		}

		// Token: 0x04001789 RID: 6025
		private const float DefaultHighMarkPercentageFactor = 0.8f;

		// Token: 0x0400178A RID: 6026
		private const int DefaultRecyclerPeriodInMilliseconds = 60000;

		// Token: 0x0400178B RID: 6027
		private readonly object _cacheDataLock = new object();

		// Token: 0x0400178C RID: 6028
		private readonly Dictionary<QueryCacheKey, QueryCacheEntry> _cacheData = new Dictionary<QueryCacheKey, QueryCacheEntry>(32);

		// Token: 0x0400178D RID: 6029
		private readonly int _maxNumberOfEntries;

		// Token: 0x0400178E RID: 6030
		private readonly int _sweepingTriggerHighMark;

		// Token: 0x0400178F RID: 6031
		private readonly QueryCacheManager.EvictionTimer _evictionTimer;

		// Token: 0x04001790 RID: 6032
		private static readonly int[] _agingFactor = new int[]
		{
			1,
			1,
			2,
			4,
			8,
			16
		};

		// Token: 0x04001791 RID: 6033
		private static readonly int AgingMaxIndex = QueryCacheManager._agingFactor.Length - 1;

		// Token: 0x020006A0 RID: 1696
		private sealed class EvictionTimer
		{
			// Token: 0x06004578 RID: 17784 RVA: 0x000F9F42 File Offset: 0x000F8142
			internal EvictionTimer(QueryCacheManager cacheManager, int recyclePeriod)
			{
				this._cacheManager = cacheManager;
				this._period = recyclePeriod;
			}

			// Token: 0x06004579 RID: 17785 RVA: 0x000F9F64 File Offset: 0x000F8164
			internal void Start()
			{
				object sync = this._sync;
				lock (sync)
				{
					if (this._timer == null)
					{
						this._timer = new Timer(new TimerCallback(QueryCacheManager.CacheRecyclerHandler), this._cacheManager, this._period, this._period);
					}
				}
			}

			// Token: 0x0600457A RID: 17786 RVA: 0x000F9FD0 File Offset: 0x000F81D0
			internal bool Stop()
			{
				object sync = this._sync;
				bool result;
				lock (sync)
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

			// Token: 0x0600457B RID: 17787 RVA: 0x000FA02C File Offset: 0x000F822C
			internal bool Suspend()
			{
				object sync = this._sync;
				bool result;
				lock (sync)
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

			// Token: 0x0600457C RID: 17788 RVA: 0x000FA084 File Offset: 0x000F8284
			internal void Resume()
			{
				object sync = this._sync;
				lock (sync)
				{
					if (this._timer != null)
					{
						this._timer.Change(this._period, this._period);
					}
				}
			}

			// Token: 0x0400200F RID: 8207
			private readonly object _sync = new object();

			// Token: 0x04002010 RID: 8208
			private readonly int _period;

			// Token: 0x04002011 RID: 8209
			private readonly QueryCacheManager _cacheManager;

			// Token: 0x04002012 RID: 8210
			private Timer _timer;
		}
	}
}
