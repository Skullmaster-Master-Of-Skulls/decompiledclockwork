using System;
using System.Collections;
using System.Threading;
using System.Web.Hosting;

namespace System.Web.Caching
{
	// Token: 0x02000872 RID: 2162
	internal sealed class CacheSingle : CacheInternal
	{
		// Token: 0x060065D8 RID: 26072 RVA: 0x00166B48 File Offset: 0x00164D48
		internal CacheSingle(CacheCommon cacheCommon, CacheMultiple cacheMultiple, int iSubCache) : base(cacheCommon)
		{
			this._cacheMultiple = cacheMultiple;
			this._iSubCache = iSubCache;
			this._entries = new Hashtable(CacheKeyComparer.GetInstance());
			this._expires = new CacheExpires(this);
			this._usage = new CacheUsage(this);
			this._lock = new object();
			this._insertBlock = new ManualResetEvent(true);
			cacheCommon.AddSRefTarget(new
			{
				this._entries,
				this._expires,
				this._usage
			});
		}

		// Token: 0x060065D9 RID: 26073 RVA: 0x00166BC8 File Offset: 0x00164DC8
		protected override void Dispose(bool disposing)
		{
			if (disposing && Interlocked.Exchange(ref this._disposed, 1) == 0)
			{
				if (this._expires != null)
				{
					this._expires.EnableExpirationTimer(false);
				}
				CacheEntry[] array = null;
				object @lock = this._lock;
				lock (@lock)
				{
					array = new CacheEntry[this._entries.Count];
					int num = 0;
					foreach (object obj in this._entries)
					{
						DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
						array[num++] = (CacheEntry)dictionaryEntry.Value;
					}
				}
				foreach (CacheEntry cacheKey in array)
				{
					base.Remove(cacheKey, CacheItemRemovedReason.Removed);
				}
				this._insertBlock.Set();
				this.ReleaseInsertBlock();
			}
			base.Dispose(disposing);
		}

		// Token: 0x060065DA RID: 26074 RVA: 0x00166CDC File Offset: 0x00164EDC
		private ManualResetEvent UseInsertBlock()
		{
			while (this._disposed != 1)
			{
				int insertBlockCalls = this._insertBlockCalls;
				if (insertBlockCalls < 0)
				{
					return null;
				}
				if (Interlocked.CompareExchange(ref this._insertBlockCalls, insertBlockCalls + 1, insertBlockCalls) == insertBlockCalls)
				{
					return this._insertBlock;
				}
			}
			return null;
		}

		// Token: 0x060065DB RID: 26075 RVA: 0x00166D1C File Offset: 0x00164F1C
		private void ReleaseInsertBlock()
		{
			if (Interlocked.Decrement(ref this._insertBlockCalls) < 0)
			{
				ManualResetEvent insertBlock = this._insertBlock;
				this._insertBlock = null;
				insertBlock.Close();
			}
		}

		// Token: 0x060065DC RID: 26076 RVA: 0x00166D4C File Offset: 0x00164F4C
		private void SetInsertBlock()
		{
			ManualResetEvent manualResetEvent = null;
			try
			{
				manualResetEvent = this.UseInsertBlock();
				if (manualResetEvent != null)
				{
					manualResetEvent.Set();
				}
			}
			finally
			{
				if (manualResetEvent != null)
				{
					this.ReleaseInsertBlock();
				}
			}
		}

		// Token: 0x060065DD RID: 26077 RVA: 0x00166D88 File Offset: 0x00164F88
		private void ResetInsertBlock()
		{
			ManualResetEvent manualResetEvent = null;
			try
			{
				manualResetEvent = this.UseInsertBlock();
				if (manualResetEvent != null)
				{
					manualResetEvent.Reset();
				}
			}
			finally
			{
				if (manualResetEvent != null)
				{
					this.ReleaseInsertBlock();
				}
			}
		}

		// Token: 0x060065DE RID: 26078 RVA: 0x00166DC4 File Offset: 0x00164FC4
		private bool WaitInsertBlock()
		{
			bool result = false;
			ManualResetEvent manualResetEvent = null;
			try
			{
				manualResetEvent = this.UseInsertBlock();
				if (manualResetEvent != null)
				{
					result = manualResetEvent.WaitOne(CacheSingle.INSERT_BLOCK_WAIT, false);
				}
			}
			finally
			{
				if (manualResetEvent != null)
				{
					this.ReleaseInsertBlock();
				}
			}
			return result;
		}

		// Token: 0x060065DF RID: 26079 RVA: 0x00166E0C File Offset: 0x0016500C
		internal void BlockInsertIfNeeded()
		{
			if (this._cacheCommon._cacheSizeMonitor.IsAboveHighPressure())
			{
				this._useInsertBlock = true;
				this.ResetInsertBlock();
			}
		}

		// Token: 0x060065E0 RID: 26080 RVA: 0x00166E2D File Offset: 0x0016502D
		internal void UnblockInsert()
		{
			if (this._useInsertBlock)
			{
				this._useInsertBlock = false;
				this.SetInsertBlock();
			}
		}

		// Token: 0x17001C88 RID: 7304
		// (get) Token: 0x060065E1 RID: 26081 RVA: 0x00166E44 File Offset: 0x00165044
		internal override int PublicCount
		{
			get
			{
				return this._publicCount;
			}
		}

		// Token: 0x17001C89 RID: 7305
		// (get) Token: 0x060065E2 RID: 26082 RVA: 0x00166E4C File Offset: 0x0016504C
		internal override long TotalCount
		{
			get
			{
				return (long)this._totalCount;
			}
		}

		// Token: 0x060065E3 RID: 26083 RVA: 0x00166E58 File Offset: 0x00165058
		internal override IDictionaryEnumerator CreateEnumerator(bool getPrivateItems = false, CacheGetOptions options = CacheGetOptions.None)
		{
			Hashtable hashtable = new Hashtable(getPrivateItems ? (this._totalCount - this._publicCount) : this._publicCount);
			DateTime utcNow = DateTime.UtcNow;
			object @lock = this._lock;
			lock (@lock)
			{
				foreach (object obj in this._entries)
				{
					CacheEntry cacheEntry = (CacheEntry)((DictionaryEntry)obj).Value;
					if (cacheEntry.IsPublic == !getPrivateItems && cacheEntry.State == CacheEntry.EntryState.AddedToCache && (!this._cacheCommon._enableExpiration || utcNow <= cacheEntry.UtcExpires))
					{
						if (options == CacheGetOptions.ReturnCacheEntry)
						{
							hashtable[cacheEntry.Key] = cacheEntry;
						}
						else
						{
							hashtable[cacheEntry.Key] = cacheEntry.Value;
						}
					}
				}
			}
			return hashtable.GetEnumerator();
		}

		// Token: 0x060065E4 RID: 26084 RVA: 0x00166F70 File Offset: 0x00165170
		internal override CacheEntry UpdateCache(CacheKey cacheKey, CacheEntry newEntry, bool replace, CacheItemRemovedReason removedReason, out object valueOld)
		{
			CacheEntry cacheEntry = null;
			CacheEntry cacheEntry2 = null;
			CacheDependency cacheDependency = null;
			bool flag = false;
			bool flag2 = false;
			DateTime dateTime = DateTime.MinValue;
			CacheEntry.EntryState entryState = CacheEntry.EntryState.NotInCache;
			bool flag3 = false;
			CacheItemRemovedReason reason = CacheItemRemovedReason.Removed;
			valueOld = null;
			bool flag4 = !replace && newEntry == null;
			bool flag5 = !replace && newEntry != null;
			DateTime utcNow;
			for (;;)
			{
				if (flag)
				{
					this.UpdateCache(cacheKey, null, true, CacheItemRemovedReason.Expired, out valueOld);
					flag = false;
				}
				cacheEntry = null;
				utcNow = DateTime.UtcNow;
				if (this._useInsertBlock && newEntry != null && newEntry.HasUsage())
				{
					bool flag6 = this.WaitInsertBlock();
				}
				bool flag7 = false;
				if (!flag4)
				{
					Monitor.Enter(this._lock, ref flag7);
				}
				try
				{
					cacheEntry = (CacheEntry)this._entries[cacheKey];
					if (cacheEntry != null)
					{
						entryState = cacheEntry.State;
						bool flag8 = this._cacheCommon._enableExpiration && cacheEntry.UtcExpires < utcNow;
						if (flag8)
						{
							if (flag4)
							{
								if (entryState == CacheEntry.EntryState.AddedToCache)
								{
									flag = true;
									continue;
								}
								cacheEntry = null;
							}
							else
							{
								replace = true;
								removedReason = CacheItemRemovedReason.Expired;
							}
						}
						else
						{
							flag2 = (this._cacheCommon._enableExpiration && cacheEntry.SlidingExpiration > TimeSpan.Zero);
						}
					}
					if (!flag4)
					{
						if (replace && cacheEntry != null)
						{
							bool flag9 = entryState != CacheEntry.EntryState.AddingToCache;
							if (flag9)
							{
								cacheEntry2 = cacheEntry;
								cacheEntry2.State = CacheEntry.EntryState.RemovingFromCache;
								this._entries.Remove(cacheEntry2);
							}
							else if (newEntry == null)
							{
								cacheEntry = null;
							}
						}
						if (newEntry != null)
						{
							bool flag10 = true;
							if (cacheEntry != null && cacheEntry2 == null)
							{
								flag10 = false;
								reason = CacheItemRemovedReason.Removed;
							}
							if (flag10)
							{
								cacheDependency = newEntry.Dependency;
								if (cacheDependency != null && cacheDependency.HasChanged)
								{
									flag10 = false;
									reason = CacheItemRemovedReason.DependencyChanged;
								}
							}
							if (flag10)
							{
								newEntry.State = CacheEntry.EntryState.AddingToCache;
								this._entries.Add(newEntry, newEntry);
								if (flag5)
								{
									cacheEntry = null;
								}
								else
								{
									cacheEntry = newEntry;
								}
							}
							else
							{
								if (!flag5)
								{
									cacheEntry = null;
									flag3 = true;
								}
								else
								{
									flag3 = (cacheEntry == null);
								}
								if (!flag3)
								{
									newEntry = null;
								}
							}
						}
					}
				}
				finally
				{
					if (flag7)
					{
						Monitor.Exit(this._lock);
					}
				}
				break;
			}
			if (flag4)
			{
				if (cacheEntry != null)
				{
					if (flag2)
					{
						dateTime = utcNow + cacheEntry.SlidingExpiration;
						if (dateTime - cacheEntry.UtcExpires >= CacheExpires.MIN_UPDATE_DELTA || dateTime < cacheEntry.UtcExpires)
						{
							this._expires.UtcUpdate(cacheEntry, dateTime);
						}
					}
					this.UtcUpdateUsageRecursive(cacheEntry, utcNow);
				}
				if (cacheKey.IsPublic)
				{
					PerfCounters.IncrementCounter(AppPerfCounter.API_CACHE_RATIO_BASE);
					if (cacheEntry != null)
					{
						PerfCounters.IncrementCounter(AppPerfCounter.API_CACHE_HITS);
					}
					else
					{
						PerfCounters.IncrementCounter(AppPerfCounter.API_CACHE_MISSES);
					}
				}
				PerfCounters.IncrementCounter(AppPerfCounter.TOTAL_CACHE_RATIO_BASE);
				if (cacheEntry != null)
				{
					PerfCounters.IncrementCounter(AppPerfCounter.TOTAL_CACHE_HITS);
				}
				else
				{
					PerfCounters.IncrementCounter(AppPerfCounter.TOTAL_CACHE_MISSES);
				}
			}
			else
			{
				int num = 0;
				int num2 = 0;
				int num3 = 0;
				int num4 = 0;
				if (cacheEntry2 != null)
				{
					if (cacheEntry2.InExpires())
					{
						this._expires.Remove(cacheEntry2);
					}
					if (cacheEntry2.InUsage())
					{
						this._usage.Remove(cacheEntry2);
					}
					cacheEntry2.State = CacheEntry.EntryState.RemovedFromCache;
					valueOld = cacheEntry2.Value;
					num--;
					num3++;
					if (cacheEntry2.IsPublic)
					{
						num2--;
						num4++;
					}
				}
				if (newEntry != null)
				{
					if (flag3)
					{
						newEntry.State = CacheEntry.EntryState.RemovedFromCache;
						newEntry.Close(reason);
						newEntry = null;
					}
					else
					{
						if (this._cacheCommon._enableExpiration && newEntry.HasExpiration())
						{
							this._expires.Add(newEntry);
						}
						if (this._cacheCommon._enableMemoryCollection && newEntry.HasUsage() && (!newEntry.HasExpiration() || newEntry.SlidingExpiration > TimeSpan.Zero || newEntry.UtcExpires - utcNow >= CacheUsage.MIN_LIFETIME_FOR_USAGE))
						{
							this._usage.Add(newEntry);
						}
						newEntry.State = CacheEntry.EntryState.AddedToCache;
						num++;
						num3++;
						if (newEntry.IsPublic)
						{
							num2++;
							num4++;
						}
					}
				}
				if (cacheEntry2 != null)
				{
					cacheEntry2.Close(removedReason);
				}
				if (newEntry != null)
				{
					newEntry.MonitorDependencyChanges();
					if (cacheDependency != null && cacheDependency.HasChanged)
					{
						base.Remove(newEntry, CacheItemRemovedReason.DependencyChanged);
					}
				}
				if (num == 1)
				{
					Interlocked.Increment(ref this._totalCount);
					PerfCounters.IncrementCounter(AppPerfCounter.TOTAL_CACHE_ENTRIES);
				}
				else if (num == -1)
				{
					Interlocked.Decrement(ref this._totalCount);
					PerfCounters.DecrementCounter(AppPerfCounter.TOTAL_CACHE_ENTRIES);
				}
				if (num2 == 1)
				{
					Interlocked.Increment(ref this._publicCount);
					PerfCounters.IncrementCounter(AppPerfCounter.API_CACHE_ENTRIES);
				}
				else if (num2 == -1)
				{
					Interlocked.Decrement(ref this._publicCount);
					PerfCounters.DecrementCounter(AppPerfCounter.API_CACHE_ENTRIES);
				}
				if (num3 > 0)
				{
					PerfCounters.IncrementCounterEx(AppPerfCounter.TOTAL_CACHE_TURNOVER_RATE, num3);
				}
				if (num4 > 0)
				{
					PerfCounters.IncrementCounterEx(AppPerfCounter.API_CACHE_TURNOVER_RATE, num4);
				}
			}
			return cacheEntry;
		}

		// Token: 0x060065E5 RID: 26085 RVA: 0x001673C0 File Offset: 0x001655C0
		private void UtcUpdateUsageRecursive(CacheEntry cacheEntry, DateTime utcNow)
		{
			if (cacheEntry != null && (utcNow - cacheEntry.UtcLastUsageUpdate > CacheUsage.CORRELATED_REQUEST_TIMEOUT || utcNow < cacheEntry.UtcLastUsageUpdate))
			{
				cacheEntry.UtcLastUsageUpdate = utcNow;
				if (cacheEntry.InUsage())
				{
					CacheSingle cacheSingle;
					if (this._cacheMultiple == null)
					{
						cacheSingle = this;
					}
					else
					{
						cacheSingle = this._cacheMultiple.GetCacheSingle(cacheEntry.Key.GetHashCode());
					}
					cacheSingle._usage.Update(cacheEntry);
				}
				CacheDependency dependency = cacheEntry.Dependency;
				if (dependency != null)
				{
					dependency.KeepDependenciesAlive();
				}
			}
		}

		// Token: 0x060065E6 RID: 26086 RVA: 0x00167444 File Offset: 0x00165644
		internal override long TrimIfNecessary(int percent)
		{
			if (!this._cacheCommon._enableMemoryCollection)
			{
				return 0L;
			}
			int num = 0;
			if (percent > 0)
			{
				num = (int)((long)this._totalCount * (long)percent / 100L);
			}
			int num2 = this._totalCount - 1073741823;
			if (num < num2)
			{
				num = num2;
			}
			int num3 = this._totalCount - 10;
			if (num > num3)
			{
				num = num3;
			}
			if (num <= 0 || HostingEnvironment.ShutdownInitiated)
			{
				return 0L;
			}
			int delta = 0;
			int delta2 = 0;
			int num4 = 0;
			int num5 = 0;
			int totalCount = this._totalCount;
			try
			{
				num5 = this._expires.FlushExpiredItems(true);
				if (num5 < num)
				{
					num4 = this._usage.FlushUnderUsedItems(num - num5, ref delta2, ref delta);
					num5 += num4;
				}
				if (num4 > 0)
				{
					PerfCounters.IncrementCounterEx(AppPerfCounter.CACHE_TOTAL_TRIMS, num4);
					PerfCounters.IncrementCounterEx(AppPerfCounter.CACHE_API_TRIMS, delta2);
					PerfCounters.IncrementCounterEx(AppPerfCounter.CACHE_OUTPUT_TRIMS, delta);
				}
			}
			catch
			{
			}
			return (long)num5;
		}

		// Token: 0x060065E7 RID: 26087 RVA: 0x00167524 File Offset: 0x00165724
		internal override void EnableExpirationTimer(bool enable)
		{
			if (this._expires != null)
			{
				this._expires.EnableExpirationTimer(enable);
			}
		}

		// Token: 0x04003478 RID: 13432
		private static readonly TimeSpan INSERT_BLOCK_WAIT = new TimeSpan(0, 0, 10);

		// Token: 0x04003479 RID: 13433
		private const int MAX_COUNT = 1073741823;

		// Token: 0x0400347A RID: 13434
		private const int MIN_COUNT = 10;

		// Token: 0x0400347B RID: 13435
		private Hashtable _entries;

		// Token: 0x0400347C RID: 13436
		private CacheExpires _expires;

		// Token: 0x0400347D RID: 13437
		private CacheUsage _usage;

		// Token: 0x0400347E RID: 13438
		private object _lock;

		// Token: 0x0400347F RID: 13439
		private int _disposed;

		// Token: 0x04003480 RID: 13440
		private int _totalCount;

		// Token: 0x04003481 RID: 13441
		private int _publicCount;

		// Token: 0x04003482 RID: 13442
		private ManualResetEvent _insertBlock;

		// Token: 0x04003483 RID: 13443
		private bool _useInsertBlock;

		// Token: 0x04003484 RID: 13444
		private int _insertBlockCalls;

		// Token: 0x04003485 RID: 13445
		private int _iSubCache;

		// Token: 0x04003486 RID: 13446
		private CacheMultiple _cacheMultiple;
	}
}
