using System;
using System.Threading;
using System.Web.Util;

namespace System.Web.Caching
{
	// Token: 0x0200089F RID: 2207
	internal sealed class CacheExpires
	{
		// Token: 0x0600675C RID: 26460 RVA: 0x0016E488 File Offset: 0x0016C688
		internal CacheExpires(CacheSingle cacheSingle)
		{
			DateTime utcNow = DateTime.UtcNow;
			this._cacheSingle = cacheSingle;
			this._buckets = new ExpiresBucket[30];
			byte b = 0;
			while ((int)b < this._buckets.Length)
			{
				this._buckets[(int)b] = new ExpiresBucket(this, b, utcNow);
				b += 1;
			}
		}

		// Token: 0x0600675D RID: 26461 RVA: 0x0016E4DC File Offset: 0x0016C6DC
		private int UtcCalcExpiresBucket(DateTime utcDate)
		{
			long num = utcDate.Ticks % CacheExpires._tsPerCycle.Ticks;
			return (int)((num / CacheExpires._tsPerBucket.Ticks + 1L) % 30L);
		}

		// Token: 0x0600675E RID: 26462 RVA: 0x0016E518 File Offset: 0x0016C718
		private int FlushExpiredItems(bool checkDelta, bool useInsertBlock)
		{
			int num = 0;
			if (Interlocked.Exchange(ref this._inFlush, 1) == 0)
			{
				try
				{
					if (this._timerHandleRef == null)
					{
						return 0;
					}
					DateTime utcNow = DateTime.UtcNow;
					if (!checkDelta || utcNow - this._utcLastFlush >= CacheExpires.MIN_FLUSH_INTERVAL || utcNow < this._utcLastFlush)
					{
						this._utcLastFlush = utcNow;
						foreach (ExpiresBucket expiresBucket in this._buckets)
						{
							num += expiresBucket.FlushExpiredItems(utcNow, useInsertBlock);
						}
					}
				}
				finally
				{
					Interlocked.Exchange(ref this._inFlush, 0);
				}
				return num;
			}
			return num;
		}

		// Token: 0x0600675F RID: 26463 RVA: 0x0016E5C8 File Offset: 0x0016C7C8
		internal int FlushExpiredItems(bool useInsertBlock)
		{
			return this.FlushExpiredItems(true, useInsertBlock);
		}

		// Token: 0x06006760 RID: 26464 RVA: 0x0016E5D2 File Offset: 0x0016C7D2
		private void TimerCallback(object state)
		{
			this.FlushExpiredItems(false, false);
		}

		// Token: 0x06006761 RID: 26465 RVA: 0x0016E5E0 File Offset: 0x0016C7E0
		internal void EnableExpirationTimer(bool enable)
		{
			if (enable)
			{
				if (this._timerHandleRef == null)
				{
					DateTime utcNow = DateTime.UtcNow;
					TimeSpan timeSpan = CacheExpires._tsPerBucket - new TimeSpan(utcNow.Ticks % CacheExpires._tsPerBucket.Ticks);
					Timer t = new Timer(new TimerCallback(this.TimerCallback), null, timeSpan.Ticks / 10000L, CacheExpires._tsPerBucket.Ticks / 10000L);
					this._timerHandleRef = new DisposableGCHandleRef<Timer>(t);
					return;
				}
			}
			else
			{
				DisposableGCHandleRef<Timer> timerHandleRef = this._timerHandleRef;
				if (timerHandleRef != null && Interlocked.CompareExchange<DisposableGCHandleRef<Timer>>(ref this._timerHandleRef, null, timerHandleRef) == timerHandleRef)
				{
					timerHandleRef.Dispose();
					while (this._inFlush != 0)
					{
						Thread.Sleep(100);
					}
				}
			}
		}

		// Token: 0x17001CD3 RID: 7379
		// (get) Token: 0x06006762 RID: 26466 RVA: 0x0016E69E File Offset: 0x0016C89E
		internal CacheSingle CacheSingle
		{
			get
			{
				return this._cacheSingle;
			}
		}

		// Token: 0x06006763 RID: 26467 RVA: 0x0016E6A8 File Offset: 0x0016C8A8
		internal void Add(CacheEntry cacheEntry)
		{
			DateTime utcNow = DateTime.UtcNow;
			if (utcNow > cacheEntry.UtcExpires)
			{
				cacheEntry.UtcExpires = utcNow;
			}
			int num = this.UtcCalcExpiresBucket(cacheEntry.UtcExpires);
			this._buckets[num].AddCacheEntry(cacheEntry);
		}

		// Token: 0x06006764 RID: 26468 RVA: 0x0016E6EC File Offset: 0x0016C8EC
		internal void Remove(CacheEntry cacheEntry)
		{
			byte expiresBucket = cacheEntry.ExpiresBucket;
			if (expiresBucket != 255)
			{
				this._buckets[(int)expiresBucket].RemoveCacheEntry(cacheEntry);
			}
		}

		// Token: 0x06006765 RID: 26469 RVA: 0x0016E718 File Offset: 0x0016C918
		internal void UtcUpdate(CacheEntry cacheEntry, DateTime utcNewExpires)
		{
			int expiresBucket = (int)cacheEntry.ExpiresBucket;
			int num = this.UtcCalcExpiresBucket(utcNewExpires);
			if (expiresBucket != num)
			{
				if (expiresBucket != 255)
				{
					this._buckets[expiresBucket].RemoveCacheEntry(cacheEntry);
					cacheEntry.UtcExpires = utcNewExpires;
					this._buckets[num].AddCacheEntry(cacheEntry);
					return;
				}
			}
			else if (expiresBucket != 255)
			{
				this._buckets[expiresBucket].UtcUpdateCacheEntry(cacheEntry, utcNewExpires);
			}
		}

		// Token: 0x0400357E RID: 13694
		internal static readonly TimeSpan MIN_UPDATE_DELTA = new TimeSpan(0, 0, 1);

		// Token: 0x0400357F RID: 13695
		internal static readonly TimeSpan MIN_FLUSH_INTERVAL = new TimeSpan(0, 0, 1);

		// Token: 0x04003580 RID: 13696
		internal static readonly TimeSpan _tsPerBucket = new TimeSpan(0, 0, 20);

		// Token: 0x04003581 RID: 13697
		private const int NUMBUCKETS = 30;

		// Token: 0x04003582 RID: 13698
		private static readonly TimeSpan _tsPerCycle = new TimeSpan(30L * CacheExpires._tsPerBucket.Ticks);

		// Token: 0x04003583 RID: 13699
		private readonly CacheSingle _cacheSingle;

		// Token: 0x04003584 RID: 13700
		private readonly ExpiresBucket[] _buckets;

		// Token: 0x04003585 RID: 13701
		private DisposableGCHandleRef<Timer> _timerHandleRef;

		// Token: 0x04003586 RID: 13702
		private DateTime _utcLastFlush;

		// Token: 0x04003587 RID: 13703
		private int _inFlush;
	}
}
