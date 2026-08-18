using System;
using System.Collections.Generic;
using System.Threading;

namespace System.IdentityModel
{
	// Token: 0x02000026 RID: 38
	internal class BoundedCache<T> where T : class
	{
		// Token: 0x06000115 RID: 277 RVA: 0x000057CA File Offset: 0x000039CA
		public BoundedCache(int capacity, TimeSpan purgeInterval) : this(capacity, purgeInterval, StringComparer.Ordinal)
		{
		}

		// Token: 0x06000116 RID: 278 RVA: 0x000057DC File Offset: 0x000039DC
		public BoundedCache(int capacity, TimeSpan purgeInterval, IEqualityComparer<string> keyComparer)
		{
			if (capacity <= 0)
			{
				throw DiagnosticUtility.ThrowHelperArgumentOutOfRange("capacity", capacity, SR.GetString("ID0002"));
			}
			if (purgeInterval <= TimeSpan.Zero)
			{
				throw DiagnosticUtility.ThrowHelperArgumentOutOfRange("purgeInterval", purgeInterval, SR.GetString("ID0016"));
			}
			if (keyComparer == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("keyComparer");
			}
			this._capacity = capacity;
			this._purgeInterval = purgeInterval;
			this._items = new Dictionary<string, BoundedCache<T>.ExpirableItem<T>>(keyComparer);
			this._readWriteLock = new ReaderWriterLock();
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x06000117 RID: 279 RVA: 0x00005879 File Offset: 0x00003A79
		protected ReaderWriterLock CacheLock
		{
			get
			{
				return this._readWriteLock;
			}
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x06000118 RID: 280 RVA: 0x00005881 File Offset: 0x00003A81
		// (set) Token: 0x06000119 RID: 281 RVA: 0x00005889 File Offset: 0x00003A89
		public virtual int Capacity
		{
			get
			{
				return this._capacity;
			}
			set
			{
				if (value <= 0)
				{
					throw DiagnosticUtility.ThrowHelperArgumentOutOfRange("value", value, SR.GetString("ID0002"));
				}
				this._capacity = value;
			}
		}

		// Token: 0x0600011A RID: 282 RVA: 0x000058B4 File Offset: 0x00003AB4
		public virtual void Clear()
		{
			this._readWriteLock.AcquireWriterLock(TimeSpan.FromMilliseconds(-1.0));
			try
			{
				this._items.Clear();
			}
			finally
			{
				this._readWriteLock.ReleaseWriterLock();
			}
		}

		// Token: 0x0600011B RID: 283 RVA: 0x00005904 File Offset: 0x00003B04
		private void EnforceQuota()
		{
			if (this._capacity == 2147483647)
			{
				return;
			}
			if (this._items.Count >= this._capacity)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new LimitExceededException(SR.GetString("ID0021", new object[]
				{
					this._capacity
				})));
			}
		}

		// Token: 0x0600011C RID: 284 RVA: 0x00005960 File Offset: 0x00003B60
		public virtual int IncreaseCapacity(int size)
		{
			if (size <= 0)
			{
				throw DiagnosticUtility.ThrowHelperArgumentOutOfRange("size", size, SR.GetString("ID0002"));
			}
			this._readWriteLock.AcquireWriterLock(TimeSpan.FromMilliseconds(-1.0));
			int capacity;
			try
			{
				if (2147483647 - size <= this._capacity)
				{
					this._capacity = int.MaxValue;
				}
				else
				{
					this._capacity += size;
				}
				capacity = this._capacity;
			}
			finally
			{
				this._readWriteLock.ReleaseWriterLock();
			}
			return capacity;
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x0600011D RID: 285 RVA: 0x000059F8 File Offset: 0x00003BF8
		protected Dictionary<string, BoundedCache<T>.ExpirableItem<T>> Items
		{
			get
			{
				return this._items;
			}
		}

		// Token: 0x0600011E RID: 286 RVA: 0x00005A00 File Offset: 0x00003C00
		private void Purge()
		{
			DateTime utcNow = DateTime.UtcNow;
			if (utcNow < this._nextPurgeTime)
			{
				return;
			}
			this._nextPurgeTime = DateTimeUtil.Add(utcNow, this._purgeInterval);
			this._readWriteLock.AcquireWriterLock(TimeSpan.FromMilliseconds(-1.0));
			try
			{
				List<string> list = new List<string>();
				foreach (string text in this._items.Keys)
				{
					if (this._items[text].IsExpired())
					{
						list.Add(text);
					}
				}
				for (int i = 0; i < list.Count; i++)
				{
					this._items.Remove(list[i]);
				}
			}
			finally
			{
				this._readWriteLock.ReleaseWriterLock();
			}
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x0600011F RID: 287 RVA: 0x00005AF4 File Offset: 0x00003CF4
		// (set) Token: 0x06000120 RID: 288 RVA: 0x00005AFC File Offset: 0x00003CFC
		public TimeSpan PurgeInterval
		{
			get
			{
				return this._purgeInterval;
			}
			set
			{
				if (value <= TimeSpan.Zero)
				{
					throw DiagnosticUtility.ThrowHelperArgumentOutOfRange("value", value, SR.GetString("ID0016"));
				}
				this._purgeInterval = value;
			}
		}

		// Token: 0x06000121 RID: 289 RVA: 0x00005B30 File Offset: 0x00003D30
		public virtual bool TryAdd(string key, T item, DateTime expirationTime)
		{
			this.Purge();
			this._readWriteLock.AcquireWriterLock(TimeSpan.FromMilliseconds(-1.0));
			this.EnforceQuota();
			bool result;
			try
			{
				if (this._items.ContainsKey(key))
				{
					result = false;
				}
				else
				{
					this._items[key] = new BoundedCache<T>.ExpirableItem<T>(item, expirationTime);
					result = true;
				}
			}
			finally
			{
				this._readWriteLock.ReleaseWriterLock();
			}
			return result;
		}

		// Token: 0x06000122 RID: 290 RVA: 0x00005BA8 File Offset: 0x00003DA8
		public virtual bool TryFind(string key)
		{
			this.Purge();
			this._readWriteLock.AcquireReaderLock(TimeSpan.FromMilliseconds(-1.0));
			bool result;
			try
			{
				if (this._items.ContainsKey(key) && !this._items[key].IsExpired())
				{
					result = true;
				}
				else
				{
					result = false;
				}
			}
			finally
			{
				this._readWriteLock.ReleaseReaderLock();
			}
			return result;
		}

		// Token: 0x06000123 RID: 291 RVA: 0x00005C1C File Offset: 0x00003E1C
		public virtual bool TryGet(string key, out T item)
		{
			this.Purge();
			item = default(T);
			this._readWriteLock.AcquireReaderLock(TimeSpan.FromMilliseconds(-1.0));
			bool result;
			try
			{
				if (this._items.ContainsKey(key) && !this._items[key].IsExpired())
				{
					item = this._items[key].Item;
					result = true;
				}
				else
				{
					result = false;
				}
			}
			finally
			{
				this._readWriteLock.ReleaseReaderLock();
			}
			return result;
		}

		// Token: 0x06000124 RID: 292 RVA: 0x00005CAC File Offset: 0x00003EAC
		public virtual bool TryRemove(string key)
		{
			this.Purge();
			this._readWriteLock.AcquireWriterLock(TimeSpan.FromMilliseconds(-1.0));
			bool result;
			try
			{
				if (!this._items.ContainsKey(key))
				{
					result = false;
				}
				else
				{
					this._items.Remove(key);
					result = true;
				}
			}
			finally
			{
				this._readWriteLock.ReleaseWriterLock();
			}
			return result;
		}

		// Token: 0x040000E0 RID: 224
		private Dictionary<string, BoundedCache<T>.ExpirableItem<T>> _items;

		// Token: 0x040000E1 RID: 225
		private int _capacity;

		// Token: 0x040000E2 RID: 226
		private TimeSpan _purgeInterval;

		// Token: 0x040000E3 RID: 227
		private ReaderWriterLock _readWriteLock;

		// Token: 0x040000E4 RID: 228
		private DateTime _nextPurgeTime = DateTime.UtcNow;

		// Token: 0x02000228 RID: 552
		protected class ExpirableItem<ET>
		{
			// Token: 0x060011E5 RID: 4581 RVA: 0x0004E54F File Offset: 0x0004C74F
			public ExpirableItem(ET item, DateTime expirationTime)
			{
				this._item = item;
				if (expirationTime.Kind != DateTimeKind.Utc)
				{
					this._expirationTime = DateTimeUtil.ToUniversalTime(expirationTime);
					return;
				}
				this._expirationTime = expirationTime;
			}

			// Token: 0x060011E6 RID: 4582 RVA: 0x0004E57C File Offset: 0x0004C77C
			public bool IsExpired()
			{
				return this._expirationTime <= DateTime.UtcNow;
			}

			// Token: 0x170004FB RID: 1275
			// (get) Token: 0x060011E7 RID: 4583 RVA: 0x0004E58E File Offset: 0x0004C78E
			public ET Item
			{
				get
				{
					return this._item;
				}
			}

			// Token: 0x04000F05 RID: 3845
			private DateTime _expirationTime;

			// Token: 0x04000F06 RID: 3846
			private ET _item;
		}

		// Token: 0x02000229 RID: 553
		[Flags]
		internal enum CachingMode
		{
			// Token: 0x04000F08 RID: 3848
			Time = 0,
			// Token: 0x04000F09 RID: 3849
			MRU = 1,
			// Token: 0x04000F0A RID: 3850
			FIFO = 2
		}
	}
}
