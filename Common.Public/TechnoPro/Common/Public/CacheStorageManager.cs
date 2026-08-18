using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using TechnoPro.Common.Public.Entities.Caching;
using TechnoPro.Common.Unity.IoC;

namespace TechnoPro.Common.Public
{
	// Token: 0x020000BA RID: 186
	public class CacheStorageManager : ICacheStorageManager
	{
		// Token: 0x170001BD RID: 445
		// (get) Token: 0x060004BC RID: 1212 RVA: 0x0000D730 File Offset: 0x0000B930
		public object[] Keys
		{
			get
			{
				return this._items.Keys.ToArray<object>();
			}
		}

		// Token: 0x170001BE RID: 446
		// (get) Token: 0x060004BD RID: 1213 RVA: 0x0000D754 File Offset: 0x0000B954
		// (set) Token: 0x060004BE RID: 1214 RVA: 0x0000D7A0 File Offset: 0x0000B9A0
		public TimeSpan CacheRefreshFrequency
		{
			get
			{
				object cacheRefreshFrequencyLock = this._cacheRefreshFrequencyLock;
				TimeSpan cacheRefreshFrequency;
				lock (cacheRefreshFrequencyLock)
				{
					cacheRefreshFrequency = this._cacheRefreshFrequency;
				}
				return cacheRefreshFrequency;
			}
			set
			{
				object cacheRefreshFrequencyLock = this._cacheRefreshFrequencyLock;
				lock (cacheRefreshFrequencyLock)
				{
					this._cacheRefreshFrequency = value;
				}
				int num = (int)this.CacheRefreshFrequency.TotalMilliseconds;
				this._timer.Change(num, num);
			}
		}

		// Token: 0x170001BF RID: 447
		// (get) Token: 0x060004BF RID: 1215 RVA: 0x0000D804 File Offset: 0x0000BA04
		[Obsolete("Use ObjectFactory.Resolve<ICacheStorageManager>() instead")]
		public static ICacheStorageManager Current { get; } = new CacheStorageManager();

		// Token: 0x060004C0 RID: 1216 RVA: 0x0000D80C File Offset: 0x0000BA0C
		public static ICacheStorageManager GetCacheManager(string InstanceName)
		{
			bool flag = string.IsNullOrEmpty(InstanceName);
			ICacheStorageManager result;
			if (flag)
			{
				result = ObjectFactory.Resolve<ICacheStorageManager>();
			}
			else
			{
				bool flag2 = CacheStorageManager._storageManagers == null;
				if (flag2)
				{
					CacheStorageManager._storageManagers = new Dictionary<string, ICacheStorageManager>();
				}
				bool flag3 = !CacheStorageManager._storageManagers.ContainsKey(InstanceName);
				if (flag3)
				{
					CacheStorageManager._storageManagers.Add(InstanceName, new CacheStorageManager());
				}
				result = CacheStorageManager._storageManagers[InstanceName];
			}
			return result;
		}

		// Token: 0x060004C1 RID: 1217 RVA: 0x0000D874 File Offset: 0x0000BA74
		public CacheStorageManager()
		{
			int num = (int)this.CacheRefreshFrequency.TotalMilliseconds;
			this._timer = new Timer(new TimerCallback(this.CacherefreshTimerCallback), null, num, num);
		}

		// Token: 0x170001C0 RID: 448
		// (get) Token: 0x060004C2 RID: 1218 RVA: 0x0000D8E4 File Offset: 0x0000BAE4
		public int CountItems
		{
			get
			{
				this._itemsLock.EnterReadLock();
				int count;
				try
				{
					count = this._items.Count;
				}
				finally
				{
					this._itemsLock.ExitReadLock();
				}
				return count;
			}
		}

		// Token: 0x170001C1 RID: 449
		public object this[object key]
		{
			get
			{
				this._itemsLock.EnterUpgradeableReadLock();
				object result;
				try
				{
					bool flag = key is ServerCacheItem;
					object key2;
					if (flag)
					{
						ServerCacheItem serverCacheItem = (ServerCacheItem)key;
						key2 = serverCacheItem.ServerCacheItemType.ToString() + "_" + serverCacheItem.SubItemId.ToString();
					}
					else
					{
						key2 = key;
					}
					CacheItem cacheItem;
					bool flag2 = this._items.TryGetValue(key2, out cacheItem) && cacheItem != null;
					if (flag2)
					{
						bool flag3 = cacheItem.SlidingExpirationTime.TotalMilliseconds > 0.0;
						if (flag3)
						{
							this._itemsLock.EnterWriteLock();
							try
							{
								cacheItem.LastAccessTime = DateTime.Now;
							}
							finally
							{
								this._itemsLock.ExitWriteLock();
							}
						}
						result = cacheItem.ItemValue;
					}
					else
					{
						result = null;
					}
				}
				finally
				{
					this._itemsLock.ExitUpgradeableReadLock();
				}
				return result;
			}
			set
			{
				this._itemsLock.EnterWriteLock();
				try
				{
					bool flag = key is ServerCacheItem;
					object key2;
					if (flag)
					{
						ServerCacheItem serverCacheItem = (ServerCacheItem)key;
						key2 = serverCacheItem.ServerCacheItemType.ToString() + "_" + serverCacheItem.SubItemId.ToString();
					}
					else
					{
						key2 = key;
					}
					this._items[key2] = new CacheItem(value);
				}
				finally
				{
					this._itemsLock.ExitWriteLock();
				}
			}
		}

		// Token: 0x060004C5 RID: 1221 RVA: 0x0000DAD4 File Offset: 0x0000BCD4
		public void Insert(object key, object value)
		{
			this._itemsLock.EnterWriteLock();
			try
			{
				this._items[key] = new CacheItem(value);
			}
			finally
			{
				this._itemsLock.ExitWriteLock();
			}
		}

		// Token: 0x060004C6 RID: 1222 RVA: 0x0000DB24 File Offset: 0x0000BD24
		public void Insert(object key, object value, DateTime expirationDate)
		{
			this._itemsLock.EnterWriteLock();
			try
			{
				this._items[key] = new CacheItem(value)
				{
					ExpirationDate = expirationDate
				};
			}
			finally
			{
				this._itemsLock.ExitWriteLock();
			}
		}

		// Token: 0x060004C7 RID: 1223 RVA: 0x0000DB7C File Offset: 0x0000BD7C
		public void Insert(object key, object value, TimeSpan expirationTime)
		{
			this._itemsLock.EnterWriteLock();
			try
			{
				this._items[key] = new CacheItem(value, expirationTime);
			}
			finally
			{
				this._itemsLock.ExitWriteLock();
			}
		}

		// Token: 0x060004C8 RID: 1224 RVA: 0x0000DBD0 File Offset: 0x0000BDD0
		public void Insert(object key, object value, TimeSpan expirationTime, bool slidingExpiration)
		{
			this._itemsLock.EnterWriteLock();
			try
			{
				this._items[key] = new CacheItem(value, expirationTime, slidingExpiration);
			}
			finally
			{
				this._itemsLock.ExitWriteLock();
			}
		}

		// Token: 0x060004C9 RID: 1225 RVA: 0x0000DC24 File Offset: 0x0000BE24
		public void Insert(object key, object value, DateTime expirationDate, TimeSpan slidingExpirationTime)
		{
			this._itemsLock.EnterWriteLock();
			try
			{
				this._items[key] = new CacheItem(value)
				{
					ExpirationDate = expirationDate,
					SlidingExpirationTime = slidingExpirationTime
				};
			}
			finally
			{
				this._itemsLock.ExitWriteLock();
			}
		}

		// Token: 0x060004CA RID: 1226 RVA: 0x0000DC88 File Offset: 0x0000BE88
		public void Remove(object key)
		{
			this._itemsLock.EnterWriteLock();
			try
			{
				this._items.Remove(key);
			}
			finally
			{
				this._itemsLock.ExitWriteLock();
			}
		}

		// Token: 0x060004CB RID: 1227 RVA: 0x0000DCD4 File Offset: 0x0000BED4
		public void Remove(Predicate<object> pKey)
		{
			this._itemsLock.EnterUpgradeableReadLock();
			try
			{
				foreach (object obj in this.Keys)
				{
					bool flag = pKey(obj);
					if (flag)
					{
						this.Remove(obj);
					}
				}
			}
			finally
			{
				this._itemsLock.ExitUpgradeableReadLock();
			}
		}

		// Token: 0x060004CC RID: 1228 RVA: 0x0000DD40 File Offset: 0x0000BF40
		public void RemoveAllSubItems(eServerCacheItemType key)
		{
			this._itemsLock.EnterWriteLock();
			try
			{
				IEnumerable<bool> enumerable = from f in this._items
				select f.Key is ServerCacheItem && ((ServerCacheItem)f.Key).ServerCacheItemType == key;
				foreach (bool flag in enumerable)
				{
					this._items.Remove(flag);
				}
			}
			finally
			{
				this._itemsLock.ExitWriteLock();
			}
		}

		// Token: 0x060004CD RID: 1229 RVA: 0x0000DDE8 File Offset: 0x0000BFE8
		public void ClearCache()
		{
			this._itemsLock.EnterWriteLock();
			try
			{
				this._items.Clear();
			}
			finally
			{
				this._itemsLock.ExitWriteLock();
			}
		}

		// Token: 0x060004CE RID: 1230 RVA: 0x0000DE34 File Offset: 0x0000C034
		private void CacherefreshTimerCallback(object state)
		{
			this._itemsLock.EnterUpgradeableReadLock();
			try
			{
				Dictionary<object, CacheItem> dictionary = new Dictionary<object, CacheItem>();
				DateTime now = DateTime.Now;
				foreach (KeyValuePair<object, CacheItem> keyValuePair in this._items)
				{
					CacheItem value = keyValuePair.Value;
					bool flag = value.ExpirationDate < now;
					if (flag)
					{
						dictionary.Add(keyValuePair.Key, value);
					}
					else
					{
						bool flag2 = value.SlidingExpirationTime.TotalMilliseconds > 0.0;
						if (flag2)
						{
							bool flag3 = now.Subtract(value.LastAccessTime).TotalMilliseconds > value.SlidingExpirationTime.TotalMilliseconds;
							if (flag3)
							{
								dictionary.Add(keyValuePair.Key, value);
							}
						}
					}
				}
				bool flag4 = dictionary.Count > 0;
				if (flag4)
				{
					this._itemsLock.EnterWriteLock();
					try
					{
						foreach (KeyValuePair<object, CacheItem> keyValuePair2 in dictionary)
						{
							bool flag5 = this._items.ContainsKey(keyValuePair2.Key);
							if (flag5)
							{
								this._items.Remove(keyValuePair2.Key);
							}
						}
					}
					finally
					{
						this._itemsLock.ExitWriteLock();
					}
				}
			}
			finally
			{
				this._itemsLock.ExitUpgradeableReadLock();
			}
		}

		// Token: 0x040001F3 RID: 499
		private readonly ReaderWriterLockSlim _itemsLock = new ReaderWriterLockSlim();

		// Token: 0x040001F4 RID: 500
		private readonly IDictionary<object, CacheItem> _items = new Dictionary<object, CacheItem>();

		// Token: 0x040001F5 RID: 501
		private readonly object _cacheRefreshFrequencyLock = new object();

		// Token: 0x040001F6 RID: 502
		private TimeSpan _cacheRefreshFrequency = new TimeSpan(0, 0, 10);

		// Token: 0x040001F7 RID: 503
		private readonly Timer _timer;

		// Token: 0x040001F9 RID: 505
		private static Dictionary<string, ICacheStorageManager> _storageManagers;
	}
}
