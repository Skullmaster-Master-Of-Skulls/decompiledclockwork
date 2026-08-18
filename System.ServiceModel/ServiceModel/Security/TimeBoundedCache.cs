using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime;
using System.Threading;

namespace System.ServiceModel.Security
{
	// Token: 0x02000327 RID: 807
	internal class TimeBoundedCache
	{
		// Token: 0x06001C9B RID: 7323 RVA: 0x0006A8F0 File Offset: 0x00068AF0
		protected TimeBoundedCache(int lowWaterMark, int maxCacheItems, IEqualityComparer keyComparer, PurgingMode purgingMode, TimeSpan purgeInterval, bool doRemoveNotification)
		{
			this.entries = new Hashtable(keyComparer);
			this.cacheLock = new ReaderWriterLock();
			this.lowWaterMark = lowWaterMark;
			this.maxCacheItems = maxCacheItems;
			this.purgingMode = purgingMode;
			this.purgeInterval = purgeInterval;
			this.doRemoveNotification = doRemoveNotification;
			this.nextPurgeTimeUtc = DateTime.UtcNow.Add(this.purgeInterval);
		}

		// Token: 0x17000721 RID: 1825
		// (get) Token: 0x06001C9C RID: 7324 RVA: 0x0006A959 File Offset: 0x00068B59
		public int Count
		{
			get
			{
				return this.entries.Count;
			}
		}

		// Token: 0x17000722 RID: 1826
		// (get) Token: 0x06001C9D RID: 7325 RVA: 0x0006A966 File Offset: 0x00068B66
		private static Action<object> PurgeCallback
		{
			get
			{
				if (TimeBoundedCache.purgeCallback == null)
				{
					TimeBoundedCache.purgeCallback = new Action<object>(TimeBoundedCache.PurgeCallbackStatic);
				}
				return TimeBoundedCache.purgeCallback;
			}
		}

		// Token: 0x17000723 RID: 1827
		// (get) Token: 0x06001C9E RID: 7326 RVA: 0x0006A985 File Offset: 0x00068B85
		protected int Capacity
		{
			get
			{
				return this.maxCacheItems;
			}
		}

		// Token: 0x17000724 RID: 1828
		// (get) Token: 0x06001C9F RID: 7327 RVA: 0x0006A98D File Offset: 0x00068B8D
		protected Hashtable Entries
		{
			get
			{
				return this.entries;
			}
		}

		// Token: 0x17000725 RID: 1829
		// (get) Token: 0x06001CA0 RID: 7328 RVA: 0x0006A995 File Offset: 0x00068B95
		protected ReaderWriterLock CacheLock
		{
			get
			{
				return this.cacheLock;
			}
		}

		// Token: 0x06001CA1 RID: 7329 RVA: 0x0006A99D File Offset: 0x00068B9D
		protected bool TryAddItem(object key, object item, DateTime expirationTime, bool replaceExistingEntry)
		{
			return this.TryAddItem(key, new TimeBoundedCache.ExpirableItem(item, expirationTime), replaceExistingEntry);
		}

		// Token: 0x06001CA2 RID: 7330 RVA: 0x0006A9AF File Offset: 0x00068BAF
		private void CancelTimerIfNeeded()
		{
			if (this.Count == 0 && this.purgingTimer != null)
			{
				this.purgingTimer.Cancel();
				this.purgingTimer = null;
			}
		}

		// Token: 0x06001CA3 RID: 7331 RVA: 0x0006A9D4 File Offset: 0x00068BD4
		private void StartTimerIfNeeded()
		{
			if (this.purgingMode != PurgingMode.TimerBasedPurge)
			{
				return;
			}
			if (this.purgingTimer == null)
			{
				this.purgingTimer = new IOThreadTimer(TimeBoundedCache.PurgeCallback, this, false);
				this.purgingTimer.Set(this.purgeInterval);
			}
		}

		// Token: 0x06001CA4 RID: 7332 RVA: 0x0006AA0C File Offset: 0x00068C0C
		protected bool TryAddItem(object key, TimeBoundedCache.IExpirableItem item, bool replaceExistingEntry)
		{
			bool flag = false;
			bool result;
			try
			{
				try
				{
				}
				finally
				{
					this.cacheLock.AcquireWriterLock(-1);
					flag = true;
				}
				this.PurgeIfNeeded();
				this.EnforceQuota();
				TimeBoundedCache.IExpirableItem expirableItem = this.entries[key] as TimeBoundedCache.IExpirableItem;
				if (expirableItem == null || this.IsExpired(expirableItem))
				{
					this.entries[key] = item;
				}
				else
				{
					if (!replaceExistingEntry)
					{
						return false;
					}
					this.entries[key] = item;
				}
				if (expirableItem != null && this.doRemoveNotification)
				{
					this.OnRemove(this.ExtractItem(expirableItem));
				}
				this.StartTimerIfNeeded();
				result = true;
			}
			finally
			{
				if (flag)
				{
					this.cacheLock.ReleaseWriterLock();
				}
			}
			return result;
		}

		// Token: 0x06001CA5 RID: 7333 RVA: 0x0006AAC8 File Offset: 0x00068CC8
		protected bool TryReplaceItem(object key, object item, DateTime expirationTime)
		{
			bool flag = false;
			bool result;
			try
			{
				try
				{
				}
				finally
				{
					this.cacheLock.AcquireWriterLock(-1);
					flag = true;
				}
				this.PurgeIfNeeded();
				this.EnforceQuota();
				TimeBoundedCache.IExpirableItem expirableItem = this.entries[key] as TimeBoundedCache.IExpirableItem;
				if (expirableItem == null || this.IsExpired(expirableItem))
				{
					result = false;
				}
				else
				{
					this.entries[key] = new TimeBoundedCache.ExpirableItem(item, expirationTime);
					if (expirableItem != null && this.doRemoveNotification)
					{
						this.OnRemove(this.ExtractItem(expirableItem));
					}
					this.StartTimerIfNeeded();
					result = true;
				}
			}
			finally
			{
				if (flag)
				{
					this.cacheLock.ReleaseWriterLock();
				}
			}
			return result;
		}

		// Token: 0x06001CA6 RID: 7334 RVA: 0x0006AB78 File Offset: 0x00068D78
		protected void ClearItems()
		{
			bool flag = false;
			try
			{
				try
				{
				}
				finally
				{
					this.cacheLock.AcquireWriterLock(-1);
					flag = true;
				}
				int count = this.entries.Count;
				if (this.doRemoveNotification)
				{
					foreach (object obj in this.entries.Values)
					{
						TimeBoundedCache.IExpirableItem val = (TimeBoundedCache.IExpirableItem)obj;
						this.OnRemove(this.ExtractItem(val));
					}
				}
				this.entries.Clear();
				this.CancelTimerIfNeeded();
			}
			finally
			{
				if (flag)
				{
					this.cacheLock.ReleaseWriterLock();
				}
			}
		}

		// Token: 0x06001CA7 RID: 7335 RVA: 0x0006AC40 File Offset: 0x00068E40
		protected object GetItem(object key)
		{
			bool flag = false;
			object result;
			try
			{
				try
				{
				}
				finally
				{
					this.cacheLock.AcquireReaderLock(-1);
					flag = true;
				}
				TimeBoundedCache.IExpirableItem expirableItem = this.entries[key] as TimeBoundedCache.IExpirableItem;
				if (expirableItem == null)
				{
					result = null;
				}
				else if (this.IsExpired(expirableItem))
				{
					result = null;
				}
				else
				{
					result = this.ExtractItem(expirableItem);
				}
			}
			finally
			{
				if (flag)
				{
					this.cacheLock.ReleaseReaderLock();
				}
			}
			return result;
		}

		// Token: 0x06001CA8 RID: 7336 RVA: 0x0006ACBC File Offset: 0x00068EBC
		protected virtual ArrayList OnQuotaReached(Hashtable cacheTable)
		{
			this.ThrowQuotaReachedException();
			return null;
		}

		// Token: 0x06001CA9 RID: 7337 RVA: 0x0006ACC5 File Offset: 0x00068EC5
		protected virtual void OnRemove(object item)
		{
		}

		// Token: 0x06001CAA RID: 7338 RVA: 0x0006ACC8 File Offset: 0x00068EC8
		protected bool TryRemoveItem(object key)
		{
			bool flag = false;
			bool result;
			try
			{
				try
				{
				}
				finally
				{
					this.cacheLock.AcquireWriterLock(-1);
					flag = true;
				}
				this.PurgeIfNeeded();
				TimeBoundedCache.IExpirableItem expirableItem = this.entries[key] as TimeBoundedCache.IExpirableItem;
				bool flag2 = expirableItem != null && !this.IsExpired(expirableItem);
				if (expirableItem != null)
				{
					this.entries.Remove(key);
					if (this.doRemoveNotification)
					{
						this.OnRemove(this.ExtractItem(expirableItem));
					}
					this.CancelTimerIfNeeded();
				}
				result = flag2;
			}
			finally
			{
				if (flag)
				{
					this.cacheLock.ReleaseWriterLock();
				}
			}
			return result;
		}

		// Token: 0x06001CAB RID: 7339 RVA: 0x0006AD6C File Offset: 0x00068F6C
		private void EnforceQuota()
		{
			if (!this.cacheLock.IsWriterLockHeld)
			{
				DiagnosticUtility.FailFast("Cache write lock is not held.");
			}
			if (this.Count >= this.maxCacheItems)
			{
				ArrayList arrayList = this.OnQuotaReached(this.entries);
				if (arrayList != null)
				{
					for (int i = 0; i < arrayList.Count; i++)
					{
						this.entries.Remove(arrayList[i]);
					}
				}
				this.CancelTimerIfNeeded();
				if (this.Count >= this.maxCacheItems)
				{
					this.ThrowQuotaReachedException();
				}
			}
		}

		// Token: 0x06001CAC RID: 7340 RVA: 0x0006ADEC File Offset: 0x00068FEC
		protected object ExtractItem(TimeBoundedCache.IExpirableItem val)
		{
			TimeBoundedCache.ExpirableItem expirableItem = val as TimeBoundedCache.ExpirableItem;
			if (expirableItem != null)
			{
				return expirableItem.Item;
			}
			return val;
		}

		// Token: 0x06001CAD RID: 7341 RVA: 0x0006AE0B File Offset: 0x0006900B
		private bool IsExpired(TimeBoundedCache.IExpirableItem item)
		{
			return item.ExpirationTime <= DateTime.UtcNow;
		}

		// Token: 0x06001CAE RID: 7342 RVA: 0x0006AE1D File Offset: 0x0006901D
		private bool ShouldPurge()
		{
			return this.Count >= this.maxCacheItems || (this.purgingMode == PurgingMode.AccessBasedPurge && DateTime.UtcNow > this.nextPurgeTimeUtc && this.Count > this.lowWaterMark);
		}

		// Token: 0x06001CAF RID: 7343 RVA: 0x0006AE5B File Offset: 0x0006905B
		private void PurgeIfNeeded()
		{
			if (!this.cacheLock.IsWriterLockHeld)
			{
				DiagnosticUtility.FailFast("Cache write lock is not held.");
			}
			if (this.ShouldPurge())
			{
				this.PurgeStaleItems();
			}
		}

		// Token: 0x06001CB0 RID: 7344 RVA: 0x0006AE84 File Offset: 0x00069084
		private void PurgeStaleItems()
		{
			if (!this.cacheLock.IsWriterLockHeld)
			{
				DiagnosticUtility.FailFast("Cache write lock is not held.");
			}
			ArrayList arrayList = new ArrayList();
			foreach (object obj in this.entries.Keys)
			{
				TimeBoundedCache.IExpirableItem expirableItem = this.entries[obj] as TimeBoundedCache.IExpirableItem;
				if (this.IsExpired(expirableItem))
				{
					this.OnRemove(this.ExtractItem(expirableItem));
					arrayList.Add(obj);
				}
			}
			for (int i = 0; i < arrayList.Count; i++)
			{
				this.entries.Remove(arrayList[i]);
			}
			this.CancelTimerIfNeeded();
			this.nextPurgeTimeUtc = DateTime.UtcNow.Add(this.purgeInterval);
		}

		// Token: 0x06001CB1 RID: 7345 RVA: 0x0006AF70 File Offset: 0x00069170
		private void ThrowQuotaReachedException()
		{
			string @string = SR.GetString("CacheQuotaReached", new object[]
			{
				this.maxCacheItems
			});
			Exception innerException = new QuotaExceededException(@string);
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new CommunicationException(@string, innerException));
		}

		// Token: 0x06001CB2 RID: 7346 RVA: 0x0006AFB4 File Offset: 0x000691B4
		private static void PurgeCallbackStatic(object state)
		{
			TimeBoundedCache timeBoundedCache = (TimeBoundedCache)state;
			bool flag = false;
			try
			{
				try
				{
				}
				finally
				{
					timeBoundedCache.cacheLock.AcquireWriterLock(-1);
					flag = true;
				}
				if (timeBoundedCache.purgingTimer != null)
				{
					timeBoundedCache.PurgeStaleItems();
					if (timeBoundedCache.Count > 0 && timeBoundedCache.purgingTimer != null)
					{
						timeBoundedCache.purgingTimer.Set(timeBoundedCache.purgeInterval);
					}
				}
			}
			finally
			{
				if (flag)
				{
					timeBoundedCache.cacheLock.ReleaseWriterLock();
				}
			}
		}

		// Token: 0x04001DCE RID: 7630
		private static Action<object> purgeCallback;

		// Token: 0x04001DCF RID: 7631
		private ReaderWriterLock cacheLock;

		// Token: 0x04001DD0 RID: 7632
		private Hashtable entries;

		// Token: 0x04001DD1 RID: 7633
		private int lowWaterMark;

		// Token: 0x04001DD2 RID: 7634
		private int maxCacheItems;

		// Token: 0x04001DD3 RID: 7635
		private DateTime nextPurgeTimeUtc;

		// Token: 0x04001DD4 RID: 7636
		private TimeSpan purgeInterval;

		// Token: 0x04001DD5 RID: 7637
		private PurgingMode purgingMode;

		// Token: 0x04001DD6 RID: 7638
		private IOThreadTimer purgingTimer;

		// Token: 0x04001DD7 RID: 7639
		private bool doRemoveNotification;

		// Token: 0x02000B76 RID: 2934
		internal interface IExpirableItem
		{
			// Token: 0x17001A92 RID: 6802
			// (get) Token: 0x060072A9 RID: 29353
			DateTime ExpirationTime { get; }
		}

		// Token: 0x02000B77 RID: 2935
		internal class ExpirableItemComparer : IComparer<TimeBoundedCache.IExpirableItem>
		{
			// Token: 0x17001A93 RID: 6803
			// (get) Token: 0x060072AA RID: 29354 RVA: 0x001AC20B File Offset: 0x001AA40B
			public static TimeBoundedCache.ExpirableItemComparer Default
			{
				get
				{
					if (TimeBoundedCache.ExpirableItemComparer.instance == null)
					{
						TimeBoundedCache.ExpirableItemComparer.instance = new TimeBoundedCache.ExpirableItemComparer();
					}
					return TimeBoundedCache.ExpirableItemComparer.instance;
				}
			}

			// Token: 0x060072AB RID: 29355 RVA: 0x001AC223 File Offset: 0x001AA423
			public int Compare(TimeBoundedCache.IExpirableItem item1, TimeBoundedCache.IExpirableItem item2)
			{
				if (item1 == item2)
				{
					return 0;
				}
				if (item1.ExpirationTime < item2.ExpirationTime)
				{
					return 1;
				}
				if (item1.ExpirationTime > item2.ExpirationTime)
				{
					return -1;
				}
				return 0;
			}

			// Token: 0x040040E7 RID: 16615
			private static TimeBoundedCache.ExpirableItemComparer instance;
		}

		// Token: 0x02000B78 RID: 2936
		internal sealed class ExpirableItem : TimeBoundedCache.IExpirableItem
		{
			// Token: 0x060072AD RID: 29357 RVA: 0x001AC25E File Offset: 0x001AA45E
			public ExpirableItem(object item, DateTime expirationTime)
			{
				this.item = item;
				this.expirationTime = expirationTime;
			}

			// Token: 0x17001A94 RID: 6804
			// (get) Token: 0x060072AE RID: 29358 RVA: 0x001AC274 File Offset: 0x001AA474
			public DateTime ExpirationTime
			{
				get
				{
					return this.expirationTime;
				}
			}

			// Token: 0x17001A95 RID: 6805
			// (get) Token: 0x060072AF RID: 29359 RVA: 0x001AC27C File Offset: 0x001AA47C
			public object Item
			{
				get
				{
					return this.item;
				}
			}

			// Token: 0x040040E8 RID: 16616
			private DateTime expirationTime;

			// Token: 0x040040E9 RID: 16617
			private object item;
		}
	}
}
