using System;

namespace System.Web.Caching
{
	// Token: 0x0200089E RID: 2206
	internal sealed class ExpiresBucket
	{
		// Token: 0x06006743 RID: 26435 RVA: 0x0016D5BC File Offset: 0x0016B7BC
		internal ExpiresBucket(CacheExpires cacheExpires, byte bucket, DateTime utcNow)
		{
			this._cacheExpires = cacheExpires;
			this._bucket = bucket;
			this._counts = new int[4];
			this.ResetCounts(utcNow);
			this.InitZeroPages();
		}

		// Token: 0x06006744 RID: 26436 RVA: 0x0016D5EB File Offset: 0x0016B7EB
		private void InitZeroPages()
		{
			this._pages = null;
			this._minEntriesInUse = -1;
			this._freePageList._head = -1;
			this._freePageList._tail = -1;
			this._freeEntryList._head = -1;
			this._freeEntryList._tail = -1;
		}

		// Token: 0x06006745 RID: 26437 RVA: 0x0016D62C File Offset: 0x0016B82C
		private void ResetCounts(DateTime utcNow)
		{
			this._utcLastCountReset = utcNow;
			this._utcMinExpires = DateTime.MaxValue;
			for (int i = 0; i < this._counts.Length; i++)
			{
				this._counts[i] = 0;
			}
		}

		// Token: 0x06006746 RID: 26438 RVA: 0x0016D668 File Offset: 0x0016B868
		private int GetCountIndex(DateTime utcExpires)
		{
			return Math.Max(0, (int)((utcExpires - this._utcLastCountReset).Ticks / ExpiresBucket.COUNT_INTERVAL.Ticks));
		}

		// Token: 0x06006747 RID: 26439 RVA: 0x0016D6A0 File Offset: 0x0016B8A0
		private void AddCount(DateTime utcExpires)
		{
			int countIndex = this.GetCountIndex(utcExpires);
			for (int i = this._counts.Length - 1; i >= countIndex; i--)
			{
				this._counts[i]++;
			}
			if (utcExpires < this._utcMinExpires)
			{
				this._utcMinExpires = utcExpires;
			}
		}

		// Token: 0x06006748 RID: 26440 RVA: 0x0016D6F0 File Offset: 0x0016B8F0
		private void RemoveCount(DateTime utcExpires)
		{
			int countIndex = this.GetCountIndex(utcExpires);
			for (int i = this._counts.Length - 1; i >= countIndex; i--)
			{
				this._counts[i]--;
			}
		}

		// Token: 0x06006749 RID: 26441 RVA: 0x0016D72C File Offset: 0x0016B92C
		private int GetExpiresCount(DateTime utcExpires)
		{
			if (utcExpires < this._utcMinExpires)
			{
				return 0;
			}
			int countIndex = this.GetCountIndex(utcExpires);
			if (countIndex >= this._counts.Length)
			{
				return this._cEntriesInUse;
			}
			return this._counts[countIndex];
		}

		// Token: 0x0600674A RID: 26442 RVA: 0x0016D76C File Offset: 0x0016B96C
		private void AddToListHead(int pageIndex, ref ExpiresPageList list)
		{
			this._pages[pageIndex]._pagePrev = -1;
			this._pages[pageIndex]._pageNext = list._head;
			if (list._head != -1)
			{
				this._pages[list._head]._pagePrev = pageIndex;
			}
			else
			{
				list._tail = pageIndex;
			}
			list._head = pageIndex;
		}

		// Token: 0x0600674B RID: 26443 RVA: 0x0016D7D4 File Offset: 0x0016B9D4
		private void AddToListTail(int pageIndex, ref ExpiresPageList list)
		{
			this._pages[pageIndex]._pageNext = -1;
			this._pages[pageIndex]._pagePrev = list._tail;
			if (list._tail != -1)
			{
				this._pages[list._tail]._pageNext = pageIndex;
			}
			else
			{
				list._head = pageIndex;
			}
			list._tail = pageIndex;
		}

		// Token: 0x0600674C RID: 26444 RVA: 0x0016D83C File Offset: 0x0016BA3C
		private int RemoveFromListHead(ref ExpiresPageList list)
		{
			int head = list._head;
			this.RemoveFromList(head, ref list);
			return head;
		}

		// Token: 0x0600674D RID: 26445 RVA: 0x0016D85C File Offset: 0x0016BA5C
		private void RemoveFromList(int pageIndex, ref ExpiresPageList list)
		{
			if (this._pages[pageIndex]._pagePrev != -1)
			{
				this._pages[this._pages[pageIndex]._pagePrev]._pageNext = this._pages[pageIndex]._pageNext;
			}
			else
			{
				list._head = this._pages[pageIndex]._pageNext;
			}
			if (this._pages[pageIndex]._pageNext != -1)
			{
				this._pages[this._pages[pageIndex]._pageNext]._pagePrev = this._pages[pageIndex]._pagePrev;
			}
			else
			{
				list._tail = this._pages[pageIndex]._pagePrev;
			}
			this._pages[pageIndex]._pagePrev = -1;
			this._pages[pageIndex]._pageNext = -1;
		}

		// Token: 0x0600674E RID: 26446 RVA: 0x0016D94B File Offset: 0x0016BB4B
		private void MoveToListHead(int pageIndex, ref ExpiresPageList list)
		{
			if (list._head == pageIndex)
			{
				return;
			}
			this.RemoveFromList(pageIndex, ref list);
			this.AddToListHead(pageIndex, ref list);
		}

		// Token: 0x0600674F RID: 26447 RVA: 0x0016D967 File Offset: 0x0016BB67
		private void MoveToListTail(int pageIndex, ref ExpiresPageList list)
		{
			if (list._tail == pageIndex)
			{
				return;
			}
			this.RemoveFromList(pageIndex, ref list);
			this.AddToListTail(pageIndex, ref list);
		}

		// Token: 0x06006750 RID: 26448 RVA: 0x0016D984 File Offset: 0x0016BB84
		private void UpdateMinEntries()
		{
			if (this._cPagesInUse <= 1)
			{
				this._minEntriesInUse = -1;
				return;
			}
			int num = this._cPagesInUse * 127;
			this._minEntriesInUse = (int)((double)num * 0.5);
			if (this._minEntriesInUse - 1 > (this._cPagesInUse - 1) * 127)
			{
				this._minEntriesInUse = -1;
			}
		}

		// Token: 0x06006751 RID: 26449 RVA: 0x0016D9DC File Offset: 0x0016BBDC
		private void RemovePage(int pageIndex)
		{
			this.RemoveFromList(pageIndex, ref this._freeEntryList);
			this.AddToListHead(pageIndex, ref this._freePageList);
			this._pages[pageIndex]._entries = null;
			this._cPagesInUse--;
			if (this._cPagesInUse == 0)
			{
				this.InitZeroPages();
				return;
			}
			this.UpdateMinEntries();
		}

		// Token: 0x06006752 RID: 26450 RVA: 0x0016DA38 File Offset: 0x0016BC38
		private ExpiresEntryRef GetFreeExpiresEntry()
		{
			int head = this._freeEntryList._head;
			ExpiresEntry[] entries = this._pages[head]._entries;
			int index = entries[0]._next.Index;
			entries[0]._next = entries[index]._next;
			ExpiresEntry[] array = entries;
			int num = 0;
			array[num]._cFree = array[num]._cFree - 1;
			if (entries[0]._cFree == 0)
			{
				this.RemoveFromList(head, ref this._freeEntryList);
			}
			return new ExpiresEntryRef(head, index);
		}

		// Token: 0x06006753 RID: 26451 RVA: 0x0016DAC0 File Offset: 0x0016BCC0
		private void AddExpiresEntryToFreeList(ExpiresEntryRef entryRef)
		{
			ExpiresEntry[] entries = this._pages[entryRef.PageIndex]._entries;
			int index = entryRef.Index;
			entries[index]._cFree = 0;
			entries[index]._next = entries[0]._next;
			entries[0]._next = entryRef;
			this._cEntriesInUse--;
			int pageIndex = entryRef.PageIndex;
			ExpiresEntry[] array = entries;
			int num = 0;
			array[num]._cFree = array[num]._cFree + 1;
			if (entries[0]._cFree == 1)
			{
				this.AddToListHead(pageIndex, ref this._freeEntryList);
				return;
			}
			if (entries[0]._cFree == 127)
			{
				this.RemovePage(pageIndex);
			}
		}

		// Token: 0x06006754 RID: 26452 RVA: 0x0016DB7C File Offset: 0x0016BD7C
		private void Expand()
		{
			if (this._freePageList._head == -1)
			{
				int num;
				if (this._pages == null)
				{
					num = 0;
				}
				else
				{
					num = this._pages.Length;
				}
				int num2 = num * 2;
				num2 = Math.Max(num + 10, num2);
				num2 = Math.Min(num2, num + 340);
				ExpiresPage[] array = new ExpiresPage[num2];
				for (int i = 0; i < num; i++)
				{
					array[i] = this._pages[i];
				}
				for (int j = num; j < array.Length; j++)
				{
					array[j]._pagePrev = j - 1;
					array[j]._pageNext = j + 1;
				}
				array[num]._pagePrev = -1;
				array[array.Length - 1]._pageNext = -1;
				this._freePageList._head = num;
				this._freePageList._tail = array.Length - 1;
				this._pages = array;
			}
			int num3 = this.RemoveFromListHead(ref this._freePageList);
			this.AddToListHead(num3, ref this._freeEntryList);
			ExpiresEntry[] array2 = new ExpiresEntry[128];
			array2[0]._cFree = 127;
			for (int k = 0; k < array2.Length - 1; k++)
			{
				array2[k]._next = new ExpiresEntryRef(num3, k + 1);
			}
			array2[array2.Length - 1]._next = ExpiresEntryRef.INVALID;
			this._pages[num3]._entries = array2;
			this._cPagesInUse++;
			this.UpdateMinEntries();
		}

		// Token: 0x06006755 RID: 26453 RVA: 0x0016DD10 File Offset: 0x0016BF10
		private void Reduce()
		{
			if (this._cEntriesInUse >= this._minEntriesInUse || this._blockReduce)
			{
				return;
			}
			int num = 63;
			int tail = this._freeEntryList._tail;
			int num2 = this._freeEntryList._head;
			for (;;)
			{
				int pageNext = this._pages[num2]._pageNext;
				if (this._pages[num2]._entries[0]._cFree > num)
				{
					this.MoveToListTail(num2, ref this._freeEntryList);
				}
				else
				{
					this.MoveToListHead(num2, ref this._freeEntryList);
				}
				if (num2 == tail)
				{
					break;
				}
				num2 = pageNext;
			}
			while (this._freeEntryList._tail != -1)
			{
				ExpiresEntry[] entries = this._pages[this._freeEntryList._tail]._entries;
				int num3 = this._cPagesInUse * 127 - entries[0]._cFree - this._cEntriesInUse;
				if (num3 < 127 - entries[0]._cFree)
				{
					break;
				}
				for (int i = 1; i < entries.Length; i++)
				{
					if (entries[i]._cacheEntry != null)
					{
						ExpiresEntryRef freeExpiresEntry = this.GetFreeExpiresEntry();
						CacheEntry cacheEntry = entries[i]._cacheEntry;
						cacheEntry.ExpiresEntryRef = freeExpiresEntry;
						ExpiresEntry[] entries2 = this._pages[freeExpiresEntry.PageIndex]._entries;
						entries2[freeExpiresEntry.Index] = entries[i];
						ExpiresEntry[] array = entries;
						int num4 = 0;
						array[num4]._cFree = array[num4]._cFree + 1;
					}
				}
				this.RemovePage(this._freeEntryList._tail);
			}
		}

		// Token: 0x06006756 RID: 26454 RVA: 0x0016DEA8 File Offset: 0x0016C0A8
		internal void AddCacheEntry(CacheEntry cacheEntry)
		{
			lock (this)
			{
				if ((cacheEntry.State & (CacheEntry.EntryState)3) != CacheEntry.EntryState.NotInCache)
				{
					ExpiresEntryRef expiresEntryRef = cacheEntry.ExpiresEntryRef;
					if (cacheEntry.ExpiresBucket == 255 && expiresEntryRef.IsInvalid)
					{
						if (this._freeEntryList._head == -1)
						{
							this.Expand();
						}
						ExpiresEntryRef freeExpiresEntry = this.GetFreeExpiresEntry();
						cacheEntry.ExpiresBucket = this._bucket;
						cacheEntry.ExpiresEntryRef = freeExpiresEntry;
						ExpiresEntry[] entries = this._pages[freeExpiresEntry.PageIndex]._entries;
						int index = freeExpiresEntry.Index;
						entries[index]._cacheEntry = cacheEntry;
						entries[index]._utcExpires = cacheEntry.UtcExpires;
						this.AddCount(cacheEntry.UtcExpires);
						this._cEntriesInUse++;
						if ((cacheEntry.State & (CacheEntry.EntryState)3) == CacheEntry.EntryState.NotInCache)
						{
							this.RemoveCacheEntryNoLock(cacheEntry);
						}
					}
				}
			}
		}

		// Token: 0x06006757 RID: 26455 RVA: 0x0016DFAC File Offset: 0x0016C1AC
		private void RemoveCacheEntryNoLock(CacheEntry cacheEntry)
		{
			ExpiresEntryRef expiresEntryRef = cacheEntry.ExpiresEntryRef;
			if (cacheEntry.ExpiresBucket != this._bucket || expiresEntryRef.IsInvalid)
			{
				return;
			}
			ExpiresEntry[] entries = this._pages[expiresEntryRef.PageIndex]._entries;
			int index = expiresEntryRef.Index;
			this.RemoveCount(entries[index]._utcExpires);
			cacheEntry.ExpiresBucket = byte.MaxValue;
			cacheEntry.ExpiresEntryRef = ExpiresEntryRef.INVALID;
			entries[index]._cacheEntry = null;
			this.AddExpiresEntryToFreeList(expiresEntryRef);
			if (this._cEntriesInUse == 0)
			{
				this.ResetCounts(DateTime.UtcNow);
			}
			this.Reduce();
		}

		// Token: 0x06006758 RID: 26456 RVA: 0x0016E050 File Offset: 0x0016C250
		internal void RemoveCacheEntry(CacheEntry cacheEntry)
		{
			lock (this)
			{
				this.RemoveCacheEntryNoLock(cacheEntry);
			}
		}

		// Token: 0x06006759 RID: 26457 RVA: 0x0016E08C File Offset: 0x0016C28C
		internal void UtcUpdateCacheEntry(CacheEntry cacheEntry, DateTime utcExpires)
		{
			lock (this)
			{
				ExpiresEntryRef expiresEntryRef = cacheEntry.ExpiresEntryRef;
				if (cacheEntry.ExpiresBucket == this._bucket && !expiresEntryRef.IsInvalid)
				{
					ExpiresEntry[] entries = this._pages[expiresEntryRef.PageIndex]._entries;
					int index = expiresEntryRef.Index;
					this.RemoveCount(entries[index]._utcExpires);
					this.AddCount(utcExpires);
					entries[index]._utcExpires = utcExpires;
					cacheEntry.UtcExpires = utcExpires;
				}
			}
		}

		// Token: 0x0600675A RID: 26458 RVA: 0x0016E134 File Offset: 0x0016C334
		internal int FlushExpiredItems(DateTime utcNow, bool useInsertBlock)
		{
			if (this._cEntriesInUse == 0 || this.GetExpiresCount(utcNow) == 0)
			{
				return 0;
			}
			ExpiresEntryRef expiresEntryRef = ExpiresEntryRef.INVALID;
			int num = 0;
			try
			{
				if (useInsertBlock)
				{
					this._cacheExpires.CacheSingle.BlockInsertIfNeeded();
				}
				lock (this)
				{
					if (this._cEntriesInUse == 0 || this.GetExpiresCount(utcNow) == 0)
					{
						return 0;
					}
					this.ResetCounts(utcNow);
					int num2 = this._cPagesInUse;
					for (int i = 0; i < this._pages.Length; i++)
					{
						ExpiresEntry[] entries = this._pages[i]._entries;
						if (entries != null)
						{
							int num3 = 127 - entries[0]._cFree;
							for (int j = 1; j < entries.Length; j++)
							{
								CacheEntry cacheEntry = entries[j]._cacheEntry;
								if (cacheEntry != null)
								{
									if (entries[j]._utcExpires > utcNow)
									{
										this.AddCount(entries[j]._utcExpires);
									}
									else
									{
										cacheEntry.ExpiresBucket = byte.MaxValue;
										cacheEntry.ExpiresEntryRef = ExpiresEntryRef.INVALID;
										entries[j]._cFree = 1;
										entries[j]._next = expiresEntryRef;
										expiresEntryRef = new ExpiresEntryRef(i, j);
										num++;
										this._cEntriesInFlush++;
									}
									num3--;
									if (num3 == 0)
									{
										break;
									}
								}
							}
							num2--;
							if (num2 == 0)
							{
								break;
							}
						}
					}
					if (num == 0)
					{
						return 0;
					}
					this._blockReduce = true;
				}
			}
			finally
			{
				if (useInsertBlock)
				{
					this._cacheExpires.CacheSingle.UnblockInsert();
				}
			}
			CacheSingle cacheSingle = this._cacheExpires.CacheSingle;
			ExpiresEntryRef entryRef = expiresEntryRef;
			while (!entryRef.IsInvalid)
			{
				ExpiresEntry[] entries = this._pages[entryRef.PageIndex]._entries;
				int index = entryRef.Index;
				ExpiresEntryRef next = entries[index]._next;
				CacheEntry cacheEntry = entries[index]._cacheEntry;
				entries[index]._cacheEntry = null;
				cacheSingle.Remove(cacheEntry, CacheItemRemovedReason.Expired);
				entryRef = next;
			}
			try
			{
				if (useInsertBlock)
				{
					this._cacheExpires.CacheSingle.BlockInsertIfNeeded();
				}
				lock (this)
				{
					entryRef = expiresEntryRef;
					while (!entryRef.IsInvalid)
					{
						ExpiresEntry[] entries = this._pages[entryRef.PageIndex]._entries;
						int index = entryRef.Index;
						ExpiresEntryRef next = entries[index]._next;
						this._cEntriesInFlush--;
						this.AddExpiresEntryToFreeList(entryRef);
						entryRef = next;
					}
					this._blockReduce = false;
					this.Reduce();
				}
			}
			finally
			{
				if (useInsertBlock)
				{
					this._cacheExpires.CacheSingle.UnblockInsert();
				}
			}
			return num;
		}

		// Token: 0x0400356A RID: 13674
		internal const int NUM_ENTRIES = 127;

		// Token: 0x0400356B RID: 13675
		private const int LENGTH_ENTRIES = 128;

		// Token: 0x0400356C RID: 13676
		private const int MIN_PAGES_INCREMENT = 10;

		// Token: 0x0400356D RID: 13677
		private const int MAX_PAGES_INCREMENT = 340;

		// Token: 0x0400356E RID: 13678
		private const double MIN_LOAD_FACTOR = 0.5;

		// Token: 0x0400356F RID: 13679
		private const int COUNTS_LENGTH = 4;

		// Token: 0x04003570 RID: 13680
		private static readonly TimeSpan COUNT_INTERVAL = new TimeSpan(CacheExpires._tsPerBucket.Ticks / 4L);

		// Token: 0x04003571 RID: 13681
		private readonly CacheExpires _cacheExpires;

		// Token: 0x04003572 RID: 13682
		private readonly byte _bucket;

		// Token: 0x04003573 RID: 13683
		private ExpiresPage[] _pages;

		// Token: 0x04003574 RID: 13684
		private int _cEntriesInUse;

		// Token: 0x04003575 RID: 13685
		private int _cPagesInUse;

		// Token: 0x04003576 RID: 13686
		private int _cEntriesInFlush;

		// Token: 0x04003577 RID: 13687
		private int _minEntriesInUse;

		// Token: 0x04003578 RID: 13688
		private ExpiresPageList _freePageList;

		// Token: 0x04003579 RID: 13689
		private ExpiresPageList _freeEntryList;

		// Token: 0x0400357A RID: 13690
		private bool _blockReduce;

		// Token: 0x0400357B RID: 13691
		private DateTime _utcMinExpires;

		// Token: 0x0400357C RID: 13692
		private int[] _counts;

		// Token: 0x0400357D RID: 13693
		private DateTime _utcLastCountReset;
	}
}
