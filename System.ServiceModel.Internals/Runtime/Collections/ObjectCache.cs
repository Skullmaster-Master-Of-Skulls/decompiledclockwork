using System;
using System.Collections.Generic;

namespace System.Runtime.Collections
{
	// Token: 0x02000050 RID: 80
	internal class ObjectCache<TKey, TValue> where TValue : class
	{
		// Token: 0x0600031A RID: 794 RVA: 0x00010AA8 File Offset: 0x0000ECA8
		public ObjectCache(ObjectCacheSettings settings) : this(settings, null)
		{
		}

		// Token: 0x0600031B RID: 795 RVA: 0x00010AB4 File Offset: 0x0000ECB4
		public ObjectCache(ObjectCacheSettings settings, IEqualityComparer<TKey> comparer)
		{
			this.settings = settings.Clone();
			this.cacheItems = new Dictionary<TKey, ObjectCache<TKey, TValue>.Item>(comparer);
			this.idleTimeoutEnabled = (settings.IdleTimeout != TimeSpan.MaxValue);
			this.leaseTimeoutEnabled = (settings.LeaseTimeout != TimeSpan.MaxValue);
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x0600031C RID: 796 RVA: 0x00005E5F File Offset: 0x0000405F
		private object ThisLock
		{
			get
			{
				return this;
			}
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x0600031D RID: 797 RVA: 0x00010B0B File Offset: 0x0000ED0B
		// (set) Token: 0x0600031E RID: 798 RVA: 0x00010B13 File Offset: 0x0000ED13
		public Action<TValue> DisposeItemCallback { get; set; }

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x0600031F RID: 799 RVA: 0x00010B1C File Offset: 0x0000ED1C
		public int Count
		{
			get
			{
				return this.cacheItems.Count;
			}
		}

		// Token: 0x06000320 RID: 800 RVA: 0x00010B2C File Offset: 0x0000ED2C
		public ObjectCacheItem<TValue> Add(TKey key, TValue value)
		{
			object thisLock = this.ThisLock;
			ObjectCacheItem<TValue> result;
			lock (thisLock)
			{
				if (this.Count >= this.settings.CacheLimit || this.cacheItems.ContainsKey(key))
				{
					result = new ObjectCache<TKey, TValue>.Item(key, value, this.DisposeItemCallback);
				}
				else
				{
					result = this.InternalAdd(key, value);
				}
			}
			return result;
		}

		// Token: 0x06000321 RID: 801 RVA: 0x00010BA4 File Offset: 0x0000EDA4
		public ObjectCacheItem<TValue> Take(TKey key)
		{
			return this.Take(key, null);
		}

		// Token: 0x06000322 RID: 802 RVA: 0x00010BB0 File Offset: 0x0000EDB0
		public ObjectCacheItem<TValue> Take(TKey key, Func<TValue> initializerDelegate)
		{
			ObjectCache<TKey, TValue>.Item item = null;
			object thisLock = this.ThisLock;
			lock (thisLock)
			{
				if (this.cacheItems.TryGetValue(key, out item))
				{
					item.InternalAddReference();
				}
				else
				{
					if (initializerDelegate == null)
					{
						return null;
					}
					TValue value = initializerDelegate();
					if (this.Count >= this.settings.CacheLimit)
					{
						return new ObjectCache<TKey, TValue>.Item(key, value, this.DisposeItemCallback);
					}
					item = this.InternalAdd(key, value);
				}
			}
			return item;
		}

		// Token: 0x06000323 RID: 803 RVA: 0x00010C44 File Offset: 0x0000EE44
		private ObjectCache<TKey, TValue>.Item InternalAdd(TKey key, TValue value)
		{
			ObjectCache<TKey, TValue>.Item item = new ObjectCache<TKey, TValue>.Item(key, value, this);
			if (this.leaseTimeoutEnabled)
			{
				item.CreationTime = DateTime.UtcNow;
			}
			this.cacheItems.Add(key, item);
			this.StartTimerIfNecessary();
			return item;
		}

		// Token: 0x06000324 RID: 804 RVA: 0x00010C84 File Offset: 0x0000EE84
		private bool Return(TKey key, ObjectCache<TKey, TValue>.Item cacheItem)
		{
			bool result = false;
			if (this.disposed)
			{
				result = true;
			}
			else
			{
				cacheItem.InternalReleaseReference();
				DateTime utcNow = DateTime.UtcNow;
				if (this.idleTimeoutEnabled)
				{
					cacheItem.LastUsage = utcNow;
				}
				if (this.ShouldPurgeItem(cacheItem, utcNow))
				{
					bool flag = this.cacheItems.Remove(key);
					cacheItem.LockedDispose();
					result = true;
				}
			}
			return result;
		}

		// Token: 0x06000325 RID: 805 RVA: 0x00010CDC File Offset: 0x0000EEDC
		private void StartTimerIfNecessary()
		{
			if (this.idleTimeoutEnabled && this.Count > 1)
			{
				if (this.idleTimer == null)
				{
					if (ObjectCache<TKey, TValue>.onIdle == null)
					{
						ObjectCache<TKey, TValue>.onIdle = new Action<object>(ObjectCache<TKey, TValue>.OnIdle);
					}
					this.idleTimer = new IOThreadTimer(ObjectCache<TKey, TValue>.onIdle, this, false);
				}
				this.idleTimer.Set(this.settings.IdleTimeout);
			}
		}

		// Token: 0x06000326 RID: 806 RVA: 0x00010D44 File Offset: 0x0000EF44
		private static void OnIdle(object state)
		{
			ObjectCache<TKey, TValue> objectCache = (ObjectCache<TKey, TValue>)state;
			objectCache.PurgeCache(true);
		}

		// Token: 0x06000327 RID: 807 RVA: 0x00010D5F File Offset: 0x0000EF5F
		private static void Add<T>(ref List<T> list, T item)
		{
			if (list == null)
			{
				list = new List<T>();
			}
			list.Add(item);
		}

		// Token: 0x06000328 RID: 808 RVA: 0x00010D74 File Offset: 0x0000EF74
		private bool ShouldPurgeItem(ObjectCache<TKey, TValue>.Item cacheItem, DateTime now)
		{
			return cacheItem.ReferenceCount <= 0 && ((this.idleTimeoutEnabled && now >= cacheItem.LastUsage + this.settings.IdleTimeout) || (this.leaseTimeoutEnabled && now - cacheItem.CreationTime >= this.settings.LeaseTimeout));
		}

		// Token: 0x06000329 RID: 809 RVA: 0x00010DE0 File Offset: 0x0000EFE0
		private void GatherExpiredItems(ref List<KeyValuePair<TKey, ObjectCache<TKey, TValue>.Item>> expiredItems, bool calledFromTimer)
		{
			if (this.Count == 0)
			{
				return;
			}
			if (!this.leaseTimeoutEnabled && !this.idleTimeoutEnabled)
			{
				return;
			}
			DateTime utcNow = DateTime.UtcNow;
			bool flag = false;
			object thisLock = this.ThisLock;
			lock (thisLock)
			{
				foreach (KeyValuePair<TKey, ObjectCache<TKey, TValue>.Item> item in this.cacheItems)
				{
					if (this.ShouldPurgeItem(item.Value, utcNow))
					{
						item.Value.LockedDispose();
						ObjectCache<TKey, TValue>.Add<KeyValuePair<TKey, ObjectCache<TKey, TValue>.Item>>(ref expiredItems, item);
					}
				}
				if (expiredItems != null)
				{
					for (int i = 0; i < expiredItems.Count; i++)
					{
						this.cacheItems.Remove(expiredItems[i].Key);
					}
				}
				flag = (calledFromTimer && this.Count > 0);
			}
			if (flag)
			{
				this.idleTimer.Set(this.settings.IdleTimeout);
			}
		}

		// Token: 0x0600032A RID: 810 RVA: 0x00010F00 File Offset: 0x0000F100
		private void PurgeCache(bool calledFromTimer)
		{
			List<KeyValuePair<TKey, ObjectCache<TKey, TValue>.Item>> list = null;
			object thisLock = this.ThisLock;
			lock (thisLock)
			{
				this.GatherExpiredItems(ref list, calledFromTimer);
			}
			if (list != null)
			{
				for (int i = 0; i < list.Count; i++)
				{
					list[i].Value.LocalDispose();
				}
			}
		}

		// Token: 0x0600032B RID: 811 RVA: 0x00010F70 File Offset: 0x0000F170
		public void Dispose()
		{
			object thisLock = this.ThisLock;
			lock (thisLock)
			{
				foreach (ObjectCache<TKey, TValue>.Item item in this.cacheItems.Values)
				{
					if (item != null)
					{
						item.Dispose();
					}
				}
				this.cacheItems.Clear();
				this.settings.CacheLimit = 0;
				this.disposed = true;
				if (this.idleTimer != null)
				{
					this.idleTimer.Cancel();
					this.idleTimer = null;
				}
			}
		}

		// Token: 0x040001AF RID: 431
		private const int timerThreshold = 1;

		// Token: 0x040001B0 RID: 432
		private ObjectCacheSettings settings;

		// Token: 0x040001B1 RID: 433
		private Dictionary<TKey, ObjectCache<TKey, TValue>.Item> cacheItems;

		// Token: 0x040001B2 RID: 434
		private bool idleTimeoutEnabled;

		// Token: 0x040001B3 RID: 435
		private bool leaseTimeoutEnabled;

		// Token: 0x040001B4 RID: 436
		private IOThreadTimer idleTimer;

		// Token: 0x040001B5 RID: 437
		private static Action<object> onIdle;

		// Token: 0x040001B6 RID: 438
		private bool disposed;

		// Token: 0x02000096 RID: 150
		private class Item : ObjectCacheItem<TValue>
		{
			// Token: 0x06000454 RID: 1108 RVA: 0x00013D4B File Offset: 0x00011F4B
			public Item(TKey key, TValue value, Action<TValue> disposeItemCallback) : this(key, value)
			{
				this.disposeItemCallback = disposeItemCallback;
			}

			// Token: 0x06000455 RID: 1109 RVA: 0x00013D5C File Offset: 0x00011F5C
			public Item(TKey key, TValue value, ObjectCache<TKey, TValue> parent) : this(key, value)
			{
				this.parent = parent;
			}

			// Token: 0x06000456 RID: 1110 RVA: 0x00013D6D File Offset: 0x00011F6D
			private Item(TKey key, TValue value)
			{
				this.key = key;
				this.value = value;
				this.referenceCount = 1;
			}

			// Token: 0x170000C1 RID: 193
			// (get) Token: 0x06000457 RID: 1111 RVA: 0x00013D8A File Offset: 0x00011F8A
			public int ReferenceCount
			{
				get
				{
					return this.referenceCount;
				}
			}

			// Token: 0x170000C2 RID: 194
			// (get) Token: 0x06000458 RID: 1112 RVA: 0x00013D92 File Offset: 0x00011F92
			public override TValue Value
			{
				get
				{
					return this.value;
				}
			}

			// Token: 0x170000C3 RID: 195
			// (get) Token: 0x06000459 RID: 1113 RVA: 0x00013D9A File Offset: 0x00011F9A
			// (set) Token: 0x0600045A RID: 1114 RVA: 0x00013DA2 File Offset: 0x00011FA2
			public DateTime CreationTime { get; set; }

			// Token: 0x170000C4 RID: 196
			// (get) Token: 0x0600045B RID: 1115 RVA: 0x00013DAB File Offset: 0x00011FAB
			// (set) Token: 0x0600045C RID: 1116 RVA: 0x00013DB3 File Offset: 0x00011FB3
			public DateTime LastUsage { get; set; }

			// Token: 0x0600045D RID: 1117 RVA: 0x00013DBC File Offset: 0x00011FBC
			public override bool TryAddReference()
			{
				bool result;
				if (this.parent == null || this.referenceCount == -1)
				{
					result = false;
				}
				else
				{
					bool flag = false;
					object thisLock = this.parent.ThisLock;
					lock (thisLock)
					{
						if (this.referenceCount == -1)
						{
							result = false;
						}
						else if (this.referenceCount == 0 && this.parent.ShouldPurgeItem(this, DateTime.UtcNow))
						{
							this.LockedDispose();
							flag = true;
							result = false;
							this.parent.cacheItems.Remove(this.key);
						}
						else
						{
							this.referenceCount++;
							result = true;
						}
					}
					if (flag)
					{
						this.LocalDispose();
					}
				}
				return result;
			}

			// Token: 0x0600045E RID: 1118 RVA: 0x00013E7C File Offset: 0x0001207C
			public override void ReleaseReference()
			{
				bool flag;
				if (this.parent == null)
				{
					this.referenceCount = -1;
					flag = true;
				}
				else
				{
					object thisLock = this.parent.ThisLock;
					lock (thisLock)
					{
						if (this.referenceCount > 1)
						{
							this.InternalReleaseReference();
							flag = false;
						}
						else
						{
							flag = this.parent.Return(this.key, this);
						}
					}
				}
				if (flag)
				{
					this.LocalDispose();
				}
			}

			// Token: 0x0600045F RID: 1119 RVA: 0x00013F00 File Offset: 0x00012100
			internal void InternalAddReference()
			{
				this.referenceCount++;
			}

			// Token: 0x06000460 RID: 1120 RVA: 0x00013F10 File Offset: 0x00012110
			internal void InternalReleaseReference()
			{
				this.referenceCount--;
			}

			// Token: 0x06000461 RID: 1121 RVA: 0x00013F20 File Offset: 0x00012120
			public void LockedDispose()
			{
				this.referenceCount = -1;
			}

			// Token: 0x06000462 RID: 1122 RVA: 0x00013F2C File Offset: 0x0001212C
			public void Dispose()
			{
				if (this.Value != null)
				{
					Action<TValue> action = this.disposeItemCallback;
					if (this.parent != null)
					{
						action = this.parent.DisposeItemCallback;
					}
					if (action != null)
					{
						action(this.Value);
					}
					else if (this.Value is IDisposable)
					{
						((IDisposable)((object)this.Value)).Dispose();
					}
				}
				this.value = default(TValue);
				this.referenceCount = -1;
			}

			// Token: 0x06000463 RID: 1123 RVA: 0x00013FAC File Offset: 0x000121AC
			public void LocalDispose()
			{
				this.Dispose();
			}

			// Token: 0x040002BD RID: 701
			private readonly ObjectCache<TKey, TValue> parent;

			// Token: 0x040002BE RID: 702
			private readonly TKey key;

			// Token: 0x040002BF RID: 703
			private readonly Action<TValue> disposeItemCallback;

			// Token: 0x040002C0 RID: 704
			private TValue value;

			// Token: 0x040002C1 RID: 705
			private int referenceCount;
		}
	}
}
