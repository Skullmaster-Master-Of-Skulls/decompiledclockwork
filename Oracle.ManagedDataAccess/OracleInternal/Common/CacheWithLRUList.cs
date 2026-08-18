using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace OracleInternal.Common
{
	// Token: 0x020000A5 RID: 165
	internal class CacheWithLRUList<keyType, valType>
	{
		// Token: 0x060006D4 RID: 1748 RVA: 0x0003F824 File Offset: 0x0003DA24
		internal CacheWithLRUList(int maxCacheSize)
		{
			this.m_maxCacheSize = maxCacheSize;
			this.m_cache = new ConcurrentDictionary<keyType, CachedLRUItem<valType, LinkedListNode<keyType>>>();
			this.m_LRUList = new LinkedList<keyType>();
		}

		// Token: 0x170001B0 RID: 432
		// (get) Token: 0x060006D5 RID: 1749 RVA: 0x0003F854 File Offset: 0x0003DA54
		internal int Count
		{
			get
			{
				if (this.m_cache != null)
				{
					return this.m_cache.Count;
				}
				return 0;
			}
		}

		// Token: 0x060006D6 RID: 1750 RVA: 0x0003F86C File Offset: 0x0003DA6C
		internal virtual valType RemoveLRU(int count = 1)
		{
			CachedLRUItem<valType, LinkedListNode<keyType>> cachedLRUItem = null;
			lock (this.m_LRUsync)
			{
				while (this.m_LRUList.Count > 0 && count > 0)
				{
					if (this.m_cache.TryRemove(this.m_LRUList.First.Value, out cachedLRUItem))
					{
						this.m_LRUList.Remove(this.m_LRUList.First);
					}
					count--;
				}
			}
			if (cachedLRUItem != null)
			{
				return cachedLRUItem.m_value;
			}
			return default(valType);
		}

		// Token: 0x060006D7 RID: 1751 RVA: 0x0003F90C File Offset: 0x0003DB0C
		internal virtual valType Put(keyType key, valType value, bool updateLRU = false)
		{
			valType valType = default(valType);
			LinkedListNode<keyType> linkedListNode = new LinkedListNode<keyType>(key);
			CachedLRUItem<valType, LinkedListNode<keyType>> value2 = new CachedLRUItem<valType, LinkedListNode<keyType>>(value, linkedListNode);
			bool flag = this.m_cache.TryAdd(key, value2);
			if (!updateLRU)
			{
				return value;
			}
			if (flag)
			{
				if (this.m_cache.Count > this.m_maxCacheSize)
				{
					valType = this.RemoveLRU(1);
				}
				lock (this.m_LRUsync)
				{
					this.m_LRUList.AddLast(linkedListNode);
				}
			}
			if (valType == null)
			{
				return default(valType);
			}
			return valType;
		}

		// Token: 0x060006D8 RID: 1752 RVA: 0x0003F9B4 File Offset: 0x0003DBB4
		internal virtual valType Get(keyType key)
		{
			CachedLRUItem<valType, LinkedListNode<keyType>> cachedLRUItem = null;
			this.m_cache.TryGetValue(key, out cachedLRUItem);
			if (cachedLRUItem != null)
			{
				return cachedLRUItem.m_value;
			}
			return default(valType);
		}

		// Token: 0x060006D9 RID: 1753 RVA: 0x0003F9E8 File Offset: 0x0003DBE8
		internal virtual bool ContainsKey(keyType key)
		{
			return this.m_cache.ContainsKey(key);
		}

		// Token: 0x060006DA RID: 1754 RVA: 0x0003F9F8 File Offset: 0x0003DBF8
		internal void Purge()
		{
			if (this.m_LRUList.Count <= 0)
			{
				lock (this.m_LRUsync)
				{
					this.m_cache.Clear();
				}
				return;
			}
			CachedLRUItem<valType, LinkedListNode<keyType>> cachedLRUItem = null;
			lock (this.m_LRUsync)
			{
				while (this.m_LRUList.Count > 0)
				{
					if (this.m_cache.TryRemove(this.m_LRUList.First.Value, out cachedLRUItem))
					{
						this.m_LRUList.Remove(this.m_LRUList.First);
					}
				}
			}
		}

		// Token: 0x04000934 RID: 2356
		private object m_LRUsync = new object();

		// Token: 0x04000935 RID: 2357
		private ConcurrentDictionary<keyType, CachedLRUItem<valType, LinkedListNode<keyType>>> m_cache;

		// Token: 0x04000936 RID: 2358
		private LinkedList<keyType> m_LRUList;

		// Token: 0x04000937 RID: 2359
		internal int m_maxCacheSize;
	}
}
