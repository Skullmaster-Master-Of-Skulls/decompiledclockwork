using System;
using System.Collections.Generic;

namespace System.Runtime
{
	// Token: 0x02000024 RID: 36
	internal class MruCache<TKey, TValue> where TKey : class where TValue : class
	{
		// Token: 0x0600011D RID: 285 RVA: 0x00005925 File Offset: 0x00003B25
		public MruCache(int watermark) : this(watermark * 4 / 5, watermark)
		{
		}

		// Token: 0x0600011E RID: 286 RVA: 0x00005933 File Offset: 0x00003B33
		public MruCache(int lowWatermark, int highWatermark) : this(lowWatermark, highWatermark, null)
		{
		}

		// Token: 0x0600011F RID: 287 RVA: 0x0000593E File Offset: 0x00003B3E
		public MruCache(int lowWatermark, int highWatermark, IEqualityComparer<TKey> comparer)
		{
			this.lowWatermark = lowWatermark;
			this.highWatermark = highWatermark;
			this.mruList = new LinkedList<TKey>();
			if (comparer == null)
			{
				this.items = new Dictionary<TKey, MruCache<TKey, TValue>.CacheEntry>();
				return;
			}
			this.items = new Dictionary<TKey, MruCache<TKey, TValue>.CacheEntry>(comparer);
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x06000120 RID: 288 RVA: 0x0000597A File Offset: 0x00003B7A
		public int Count
		{
			get
			{
				return this.items.Count;
			}
		}

		// Token: 0x06000121 RID: 289 RVA: 0x00005988 File Offset: 0x00003B88
		public void Add(TKey key, TValue value)
		{
			bool flag = false;
			try
			{
				if (this.items.Count == this.highWatermark)
				{
					int num = this.highWatermark - this.lowWatermark;
					for (int i = 0; i < num; i++)
					{
						TKey value2 = this.mruList.Last.Value;
						this.mruList.RemoveLast();
						TValue value3 = this.items[value2].value;
						this.items.Remove(value2);
						this.OnSingleItemRemoved(value3);
						this.OnItemAgedOutOfCache(value3);
					}
				}
				MruCache<TKey, TValue>.CacheEntry value4;
				value4.node = this.mruList.AddFirst(key);
				value4.value = value;
				this.items.Add(key, value4);
				this.mruEntry = value4;
				flag = true;
			}
			finally
			{
				if (!flag)
				{
					this.Clear();
				}
			}
		}

		// Token: 0x06000122 RID: 290 RVA: 0x00005A60 File Offset: 0x00003C60
		public void Clear()
		{
			this.mruList.Clear();
			this.items.Clear();
			this.mruEntry.value = default(TValue);
			this.mruEntry.node = null;
		}

		// Token: 0x06000123 RID: 291 RVA: 0x00005A98 File Offset: 0x00003C98
		public bool Remove(TKey key)
		{
			MruCache<TKey, TValue>.CacheEntry cacheEntry;
			if (this.items.TryGetValue(key, out cacheEntry))
			{
				this.items.Remove(key);
				this.OnSingleItemRemoved(cacheEntry.value);
				this.mruList.Remove(cacheEntry.node);
				if (this.mruEntry.node == cacheEntry.node)
				{
					this.mruEntry.value = default(TValue);
					this.mruEntry.node = null;
				}
				return true;
			}
			return false;
		}

		// Token: 0x06000124 RID: 292 RVA: 0x000033BD File Offset: 0x000015BD
		protected virtual void OnSingleItemRemoved(TValue item)
		{
		}

		// Token: 0x06000125 RID: 293 RVA: 0x000033BD File Offset: 0x000015BD
		protected virtual void OnItemAgedOutOfCache(TValue item)
		{
		}

		// Token: 0x06000126 RID: 294 RVA: 0x00005B14 File Offset: 0x00003D14
		public bool TryGetValue(TKey key, out TValue value)
		{
			if (this.mruEntry.node != null && key != null && key.Equals(this.mruEntry.node.Value))
			{
				value = this.mruEntry.value;
				return true;
			}
			MruCache<TKey, TValue>.CacheEntry cacheEntry;
			bool flag = this.items.TryGetValue(key, out cacheEntry);
			value = cacheEntry.value;
			if (flag && this.mruList.Count > 1 && this.mruList.First != cacheEntry.node)
			{
				this.mruList.Remove(cacheEntry.node);
				this.mruList.AddFirst(cacheEntry.node);
				this.mruEntry = cacheEntry;
			}
			return flag;
		}

		// Token: 0x04000090 RID: 144
		private LinkedList<TKey> mruList;

		// Token: 0x04000091 RID: 145
		private Dictionary<TKey, MruCache<TKey, TValue>.CacheEntry> items;

		// Token: 0x04000092 RID: 146
		private int lowWatermark;

		// Token: 0x04000093 RID: 147
		private int highWatermark;

		// Token: 0x04000094 RID: 148
		private MruCache<TKey, TValue>.CacheEntry mruEntry;

		// Token: 0x0200007E RID: 126
		private struct CacheEntry
		{
			// Token: 0x04000273 RID: 627
			internal TValue value;

			// Token: 0x04000274 RID: 628
			internal LinkedListNode<TKey> node;
		}
	}
}
