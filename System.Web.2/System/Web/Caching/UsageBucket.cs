using System;

namespace System.Web.Caching
{
	// Token: 0x020008A5 RID: 2213
	internal sealed class UsageBucket
	{
		// Token: 0x06006773 RID: 26483 RVA: 0x0016E8B6 File Offset: 0x0016CAB6
		internal UsageBucket(CacheUsage cacheUsage, byte bucket)
		{
			this._cacheUsage = cacheUsage;
			this._bucket = bucket;
			this.InitZeroPages();
		}

		// Token: 0x06006774 RID: 26484 RVA: 0x0016E8D2 File Offset: 0x0016CAD2
		private void InitZeroPages()
		{
			this._pages = null;
			this._minEntriesInUse = -1;
			this._freePageList._head = -1;
			this._freePageList._tail = -1;
			this._freeEntryList._head = -1;
			this._freeEntryList._tail = -1;
		}

		// Token: 0x06006775 RID: 26485 RVA: 0x0016E914 File Offset: 0x0016CB14
		private void AddToListHead(int pageIndex, ref UsagePageList list)
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

		// Token: 0x06006776 RID: 26486 RVA: 0x0016E97C File Offset: 0x0016CB7C
		private void AddToListTail(int pageIndex, ref UsagePageList list)
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

		// Token: 0x06006777 RID: 26487 RVA: 0x0016E9E4 File Offset: 0x0016CBE4
		private int RemoveFromListHead(ref UsagePageList list)
		{
			int head = list._head;
			this.RemoveFromList(head, ref list);
			return head;
		}

		// Token: 0x06006778 RID: 26488 RVA: 0x0016EA04 File Offset: 0x0016CC04
		private void RemoveFromList(int pageIndex, ref UsagePageList list)
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

		// Token: 0x06006779 RID: 26489 RVA: 0x0016EAF3 File Offset: 0x0016CCF3
		private void MoveToListHead(int pageIndex, ref UsagePageList list)
		{
			if (list._head == pageIndex)
			{
				return;
			}
			this.RemoveFromList(pageIndex, ref list);
			this.AddToListHead(pageIndex, ref list);
		}

		// Token: 0x0600677A RID: 26490 RVA: 0x0016EB0F File Offset: 0x0016CD0F
		private void MoveToListTail(int pageIndex, ref UsagePageList list)
		{
			if (list._tail == pageIndex)
			{
				return;
			}
			this.RemoveFromList(pageIndex, ref list);
			this.AddToListTail(pageIndex, ref list);
		}

		// Token: 0x0600677B RID: 26491 RVA: 0x0016EB2C File Offset: 0x0016CD2C
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

		// Token: 0x0600677C RID: 26492 RVA: 0x0016EB84 File Offset: 0x0016CD84
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

		// Token: 0x0600677D RID: 26493 RVA: 0x0016EBE0 File Offset: 0x0016CDE0
		private UsageEntryRef GetFreeUsageEntry()
		{
			int head = this._freeEntryList._head;
			UsageEntry[] entries = this._pages[head]._entries;
			int ref1Index = entries[0]._ref1._next.Ref1Index;
			entries[0]._ref1._next = entries[ref1Index]._ref1._next;
			UsageEntry[] array = entries;
			int num = 0;
			array[num]._cFree = array[num]._cFree - 1;
			if (entries[0]._cFree == 0)
			{
				this.RemoveFromList(head, ref this._freeEntryList);
			}
			return new UsageEntryRef(head, ref1Index);
		}

		// Token: 0x0600677E RID: 26494 RVA: 0x0016EC78 File Offset: 0x0016CE78
		private void AddUsageEntryToFreeList(UsageEntryRef entryRef)
		{
			UsageEntry[] entries = this._pages[entryRef.PageIndex]._entries;
			int ref1Index = entryRef.Ref1Index;
			entries[ref1Index]._utcDate = DateTime.MinValue;
			entries[ref1Index]._ref1._prev = UsageEntryRef.INVALID;
			entries[ref1Index]._ref2._next = UsageEntryRef.INVALID;
			entries[ref1Index]._ref2._prev = UsageEntryRef.INVALID;
			entries[ref1Index]._ref1._next = entries[0]._ref1._next;
			entries[0]._ref1._next = entryRef;
			this._cEntriesInUse--;
			int pageIndex = entryRef.PageIndex;
			UsageEntry[] array = entries;
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

		// Token: 0x0600677F RID: 26495 RVA: 0x0016ED88 File Offset: 0x0016CF88
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
				UsagePage[] array = new UsagePage[num2];
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
			UsageEntry[] array2 = new UsageEntry[128];
			array2[0]._cFree = 127;
			for (int k = 0; k < array2.Length - 1; k++)
			{
				array2[k]._ref1._next = new UsageEntryRef(num3, k + 1);
			}
			array2[array2.Length - 1]._ref1._next = UsageEntryRef.INVALID;
			this._pages[num3]._entries = array2;
			this._cPagesInUse++;
			this.UpdateMinEntries();
		}

		// Token: 0x06006780 RID: 26496 RVA: 0x0016EF28 File Offset: 0x0016D128
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
				UsageEntry[] entries = this._pages[this._freeEntryList._tail]._entries;
				int num3 = this._cPagesInUse * 127 - entries[0]._cFree - this._cEntriesInUse;
				if (num3 < 127 - entries[0]._cFree)
				{
					break;
				}
				for (int i = 1; i < entries.Length; i++)
				{
					if (entries[i]._cacheEntry != null)
					{
						UsageEntryRef freeUsageEntry = this.GetFreeUsageEntry();
						UsageEntryRef usageEntryRef = new UsageEntryRef(freeUsageEntry.PageIndex, -freeUsageEntry.Ref1Index);
						UsageEntryRef r = new UsageEntryRef(this._freeEntryList._tail, i);
						UsageEntryRef r2 = new UsageEntryRef(r.PageIndex, -r.Ref1Index);
						CacheEntry cacheEntry = entries[i]._cacheEntry;
						cacheEntry.UsageEntryRef = freeUsageEntry;
						UsageEntry[] entries2 = this._pages[freeUsageEntry.PageIndex]._entries;
						entries2[freeUsageEntry.Ref1Index] = entries[i];
						UsageEntry[] array = entries;
						int num4 = 0;
						array[num4]._cFree = array[num4]._cFree + 1;
						UsageEntryRef r3 = entries2[freeUsageEntry.Ref1Index]._ref1._prev;
						UsageEntryRef r4 = entries2[freeUsageEntry.Ref1Index]._ref1._next;
						if (r4 == r2)
						{
							r4 = usageEntryRef;
						}
						if (r3.IsRef1)
						{
							this._pages[r3.PageIndex]._entries[r3.Ref1Index]._ref1._next = freeUsageEntry;
						}
						else if (r3.IsRef2)
						{
							this._pages[r3.PageIndex]._entries[r3.Ref2Index]._ref2._next = freeUsageEntry;
						}
						else
						{
							this._lastRefHead = freeUsageEntry;
						}
						if (r4.IsRef1)
						{
							this._pages[r4.PageIndex]._entries[r4.Ref1Index]._ref1._prev = freeUsageEntry;
						}
						else if (r4.IsRef2)
						{
							this._pages[r4.PageIndex]._entries[r4.Ref2Index]._ref2._prev = freeUsageEntry;
						}
						else
						{
							this._lastRefTail = freeUsageEntry;
						}
						r3 = entries2[freeUsageEntry.Ref1Index]._ref2._prev;
						if (r3 == r)
						{
							r3 = freeUsageEntry;
						}
						r4 = entries2[freeUsageEntry.Ref1Index]._ref2._next;
						if (r3.IsRef1)
						{
							this._pages[r3.PageIndex]._entries[r3.Ref1Index]._ref1._next = usageEntryRef;
						}
						else if (r3.IsRef2)
						{
							this._pages[r3.PageIndex]._entries[r3.Ref2Index]._ref2._next = usageEntryRef;
						}
						else
						{
							this._lastRefHead = usageEntryRef;
						}
						if (r4.IsRef1)
						{
							this._pages[r4.PageIndex]._entries[r4.Ref1Index]._ref1._prev = usageEntryRef;
						}
						else if (r4.IsRef2)
						{
							this._pages[r4.PageIndex]._entries[r4.Ref2Index]._ref2._prev = usageEntryRef;
						}
						else
						{
							this._lastRefTail = usageEntryRef;
						}
						if (this._addRef2Head == r2)
						{
							this._addRef2Head = usageEntryRef;
						}
					}
				}
				this.RemovePage(this._freeEntryList._tail);
			}
		}

		// Token: 0x06006781 RID: 26497 RVA: 0x0016F394 File Offset: 0x0016D594
		internal void AddCacheEntry(CacheEntry cacheEntry)
		{
			lock (this)
			{
				if (this._freeEntryList._head == -1)
				{
					this.Expand();
				}
				UsageEntryRef freeUsageEntry = this.GetFreeUsageEntry();
				UsageEntryRef usageEntryRef = new UsageEntryRef(freeUsageEntry.PageIndex, -freeUsageEntry.Ref1Index);
				cacheEntry.UsageEntryRef = freeUsageEntry;
				UsageEntry[] entries = this._pages[freeUsageEntry.PageIndex]._entries;
				int ref1Index = freeUsageEntry.Ref1Index;
				entries[ref1Index]._cacheEntry = cacheEntry;
				entries[ref1Index]._utcDate = DateTime.UtcNow;
				entries[ref1Index]._ref1._prev = UsageEntryRef.INVALID;
				entries[ref1Index]._ref2._next = this._addRef2Head;
				if (this._lastRefHead.IsInvalid)
				{
					entries[ref1Index]._ref1._next = usageEntryRef;
					entries[ref1Index]._ref2._prev = freeUsageEntry;
					this._lastRefTail = usageEntryRef;
				}
				else
				{
					entries[ref1Index]._ref1._next = this._lastRefHead;
					if (this._lastRefHead.IsRef1)
					{
						this._pages[this._lastRefHead.PageIndex]._entries[this._lastRefHead.Ref1Index]._ref1._prev = freeUsageEntry;
					}
					else if (this._lastRefHead.IsRef2)
					{
						this._pages[this._lastRefHead.PageIndex]._entries[this._lastRefHead.Ref2Index]._ref2._prev = freeUsageEntry;
					}
					else
					{
						this._lastRefTail = freeUsageEntry;
					}
					UsageEntryRef prev;
					UsageEntryRef usageEntryRef2;
					if (this._addRef2Head.IsInvalid)
					{
						prev = this._lastRefTail;
						usageEntryRef2 = UsageEntryRef.INVALID;
					}
					else
					{
						prev = this._pages[this._addRef2Head.PageIndex]._entries[this._addRef2Head.Ref2Index]._ref2._prev;
						usageEntryRef2 = this._addRef2Head;
					}
					entries[ref1Index]._ref2._prev = prev;
					if (prev.IsRef1)
					{
						this._pages[prev.PageIndex]._entries[prev.Ref1Index]._ref1._next = usageEntryRef;
					}
					else if (prev.IsRef2)
					{
						this._pages[prev.PageIndex]._entries[prev.Ref2Index]._ref2._next = usageEntryRef;
					}
					else
					{
						this._lastRefHead = usageEntryRef;
					}
					if (usageEntryRef2.IsRef1)
					{
						this._pages[usageEntryRef2.PageIndex]._entries[usageEntryRef2.Ref1Index]._ref1._prev = usageEntryRef;
					}
					else if (usageEntryRef2.IsRef2)
					{
						this._pages[usageEntryRef2.PageIndex]._entries[usageEntryRef2.Ref2Index]._ref2._prev = usageEntryRef;
					}
					else
					{
						this._lastRefTail = usageEntryRef;
					}
				}
				this._lastRefHead = freeUsageEntry;
				this._addRef2Head = usageEntryRef;
				this._cEntriesInUse++;
			}
		}

		// Token: 0x06006782 RID: 26498 RVA: 0x0016F6F0 File Offset: 0x0016D8F0
		private void RemoveEntryFromLastRefList(UsageEntryRef entryRef)
		{
			UsageEntry[] entries = this._pages[entryRef.PageIndex]._entries;
			int ref1Index = entryRef.Ref1Index;
			UsageEntryRef prev = entries[ref1Index]._ref1._prev;
			UsageEntryRef next = entries[ref1Index]._ref1._next;
			if (prev.IsRef1)
			{
				this._pages[prev.PageIndex]._entries[prev.Ref1Index]._ref1._next = next;
			}
			else if (prev.IsRef2)
			{
				this._pages[prev.PageIndex]._entries[prev.Ref2Index]._ref2._next = next;
			}
			else
			{
				this._lastRefHead = next;
			}
			if (next.IsRef1)
			{
				this._pages[next.PageIndex]._entries[next.Ref1Index]._ref1._prev = prev;
			}
			else if (next.IsRef2)
			{
				this._pages[next.PageIndex]._entries[next.Ref2Index]._ref2._prev = prev;
			}
			else
			{
				this._lastRefTail = prev;
			}
			prev = entries[ref1Index]._ref2._prev;
			next = entries[ref1Index]._ref2._next;
			UsageEntryRef r = new UsageEntryRef(entryRef.PageIndex, -entryRef.Ref1Index);
			if (prev.IsRef1)
			{
				this._pages[prev.PageIndex]._entries[prev.Ref1Index]._ref1._next = next;
			}
			else if (prev.IsRef2)
			{
				this._pages[prev.PageIndex]._entries[prev.Ref2Index]._ref2._next = next;
			}
			else
			{
				this._lastRefHead = next;
			}
			if (next.IsRef1)
			{
				this._pages[next.PageIndex]._entries[next.Ref1Index]._ref1._prev = prev;
			}
			else if (next.IsRef2)
			{
				this._pages[next.PageIndex]._entries[next.Ref2Index]._ref2._prev = prev;
			}
			else
			{
				this._lastRefTail = prev;
			}
			if (this._addRef2Head == r)
			{
				this._addRef2Head = next;
			}
		}

		// Token: 0x06006783 RID: 26499 RVA: 0x0016F978 File Offset: 0x0016DB78
		internal void RemoveCacheEntry(CacheEntry cacheEntry)
		{
			lock (this)
			{
				UsageEntryRef usageEntryRef = cacheEntry.UsageEntryRef;
				if (!usageEntryRef.IsInvalid)
				{
					UsageEntry[] entries = this._pages[usageEntryRef.PageIndex]._entries;
					int ref1Index = usageEntryRef.Ref1Index;
					cacheEntry.UsageEntryRef = UsageEntryRef.INVALID;
					entries[ref1Index]._cacheEntry = null;
					this.RemoveEntryFromLastRefList(usageEntryRef);
					this.AddUsageEntryToFreeList(usageEntryRef);
					this.Reduce();
				}
			}
		}

		// Token: 0x06006784 RID: 26500 RVA: 0x0016FA10 File Offset: 0x0016DC10
		internal void UpdateCacheEntry(CacheEntry cacheEntry)
		{
			lock (this)
			{
				UsageEntryRef usageEntryRef = cacheEntry.UsageEntryRef;
				if (!usageEntryRef.IsInvalid)
				{
					UsageEntry[] entries = this._pages[usageEntryRef.PageIndex]._entries;
					int ref1Index = usageEntryRef.Ref1Index;
					UsageEntryRef usageEntryRef2 = new UsageEntryRef(usageEntryRef.PageIndex, -usageEntryRef.Ref1Index);
					UsageEntryRef prev = entries[ref1Index]._ref2._prev;
					UsageEntryRef next = entries[ref1Index]._ref2._next;
					if (prev.IsRef1)
					{
						this._pages[prev.PageIndex]._entries[prev.Ref1Index]._ref1._next = next;
					}
					else if (prev.IsRef2)
					{
						this._pages[prev.PageIndex]._entries[prev.Ref2Index]._ref2._next = next;
					}
					else
					{
						this._lastRefHead = next;
					}
					if (next.IsRef1)
					{
						this._pages[next.PageIndex]._entries[next.Ref1Index]._ref1._prev = prev;
					}
					else if (next.IsRef2)
					{
						this._pages[next.PageIndex]._entries[next.Ref2Index]._ref2._prev = prev;
					}
					else
					{
						this._lastRefTail = prev;
					}
					if (this._addRef2Head == usageEntryRef2)
					{
						this._addRef2Head = next;
					}
					entries[ref1Index]._ref2 = entries[ref1Index]._ref1;
					prev = entries[ref1Index]._ref2._prev;
					next = entries[ref1Index]._ref2._next;
					if (prev.IsRef1)
					{
						this._pages[prev.PageIndex]._entries[prev.Ref1Index]._ref1._next = usageEntryRef2;
					}
					else if (prev.IsRef2)
					{
						this._pages[prev.PageIndex]._entries[prev.Ref2Index]._ref2._next = usageEntryRef2;
					}
					else
					{
						this._lastRefHead = usageEntryRef2;
					}
					if (next.IsRef1)
					{
						this._pages[next.PageIndex]._entries[next.Ref1Index]._ref1._prev = usageEntryRef2;
					}
					else if (next.IsRef2)
					{
						this._pages[next.PageIndex]._entries[next.Ref2Index]._ref2._prev = usageEntryRef2;
					}
					else
					{
						this._lastRefTail = usageEntryRef2;
					}
					entries[ref1Index]._ref1._prev = UsageEntryRef.INVALID;
					entries[ref1Index]._ref1._next = this._lastRefHead;
					if (this._lastRefHead.IsRef1)
					{
						this._pages[this._lastRefHead.PageIndex]._entries[this._lastRefHead.Ref1Index]._ref1._prev = usageEntryRef;
					}
					else if (this._lastRefHead.IsRef2)
					{
						this._pages[this._lastRefHead.PageIndex]._entries[this._lastRefHead.Ref2Index]._ref2._prev = usageEntryRef;
					}
					else
					{
						this._lastRefTail = usageEntryRef;
					}
					this._lastRefHead = usageEntryRef;
				}
			}
		}

		// Token: 0x06006785 RID: 26501 RVA: 0x0016FDD8 File Offset: 0x0016DFD8
		internal int FlushUnderUsedItems(int maxFlush, bool force, ref int publicEntriesFlushed, ref int ocEntriesFlushed)
		{
			if (this._cEntriesInUse == 0)
			{
				return 0;
			}
			UsageEntryRef usageEntryRef = UsageEntryRef.INVALID;
			int num = 0;
			try
			{
				this._cacheUsage.CacheSingle.BlockInsertIfNeeded();
				lock (this)
				{
					if (this._cEntriesInUse == 0)
					{
						return 0;
					}
					DateTime utcNow = DateTime.UtcNow;
					UsageEntryRef usageEntryRef2 = this._lastRefTail;
					while (this._cEntriesInFlush < maxFlush && !usageEntryRef2.IsInvalid)
					{
						UsageEntryRef prev = this._pages[usageEntryRef2.PageIndex]._entries[usageEntryRef2.Ref2Index]._ref2._prev;
						while (prev.IsRef1)
						{
							prev = this._pages[prev.PageIndex]._entries[prev.Ref1Index]._ref1._prev;
						}
						UsageEntry[] entries = this._pages[usageEntryRef2.PageIndex]._entries;
						int num2 = usageEntryRef2.Ref2Index;
						if (force)
						{
							goto IL_111;
						}
						DateTime utcDate = entries[num2]._utcDate;
						if (!(utcNow - utcDate <= CacheUsage.NEWADD_INTERVAL) || !(utcNow >= utcDate))
						{
							goto IL_111;
						}
						IL_197:
						usageEntryRef2 = prev;
						continue;
						IL_111:
						UsageEntryRef usageEntryRef3 = new UsageEntryRef(usageEntryRef2.PageIndex, usageEntryRef2.Ref2Index);
						CacheEntry cacheEntry = entries[num2]._cacheEntry;
						cacheEntry.UsageEntryRef = UsageEntryRef.INVALID;
						if (cacheEntry.IsPublic)
						{
							publicEntriesFlushed++;
						}
						else if (cacheEntry.IsOutputCache)
						{
							ocEntriesFlushed++;
						}
						this.RemoveEntryFromLastRefList(usageEntryRef3);
						entries[num2]._ref1._next = usageEntryRef;
						usageEntryRef = usageEntryRef3;
						num++;
						this._cEntriesInFlush++;
						goto IL_197;
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
				this._cacheUsage.CacheSingle.UnblockInsert();
			}
			CacheSingle cacheSingle = this._cacheUsage.CacheSingle;
			UsageEntryRef entryRef = usageEntryRef;
			while (!entryRef.IsInvalid)
			{
				UsageEntry[] entries = this._pages[entryRef.PageIndex]._entries;
				int num2 = entryRef.Ref1Index;
				UsageEntryRef next = entries[num2]._ref1._next;
				CacheEntry cacheEntry = entries[num2]._cacheEntry;
				entries[num2]._cacheEntry = null;
				cacheSingle.Remove(cacheEntry, CacheItemRemovedReason.Underused);
				entryRef = next;
			}
			try
			{
				this._cacheUsage.CacheSingle.BlockInsertIfNeeded();
				lock (this)
				{
					entryRef = usageEntryRef;
					while (!entryRef.IsInvalid)
					{
						UsageEntry[] entries = this._pages[entryRef.PageIndex]._entries;
						int num2 = entryRef.Ref1Index;
						UsageEntryRef next = entries[num2]._ref1._next;
						this._cEntriesInFlush--;
						this.AddUsageEntryToFreeList(entryRef);
						entryRef = next;
					}
					this._blockReduce = false;
					this.Reduce();
				}
			}
			finally
			{
				this._cacheUsage.CacheSingle.UnblockInsert();
			}
			return num;
		}

		// Token: 0x04003599 RID: 13721
		internal const int NUM_ENTRIES = 127;

		// Token: 0x0400359A RID: 13722
		private const int LENGTH_ENTRIES = 128;

		// Token: 0x0400359B RID: 13723
		private const int MIN_PAGES_INCREMENT = 10;

		// Token: 0x0400359C RID: 13724
		private const int MAX_PAGES_INCREMENT = 340;

		// Token: 0x0400359D RID: 13725
		private const double MIN_LOAD_FACTOR = 0.5;

		// Token: 0x0400359E RID: 13726
		private CacheUsage _cacheUsage;

		// Token: 0x0400359F RID: 13727
		private byte _bucket;

		// Token: 0x040035A0 RID: 13728
		private UsagePage[] _pages;

		// Token: 0x040035A1 RID: 13729
		private int _cEntriesInUse;

		// Token: 0x040035A2 RID: 13730
		private int _cPagesInUse;

		// Token: 0x040035A3 RID: 13731
		private int _cEntriesInFlush;

		// Token: 0x040035A4 RID: 13732
		private int _minEntriesInUse;

		// Token: 0x040035A5 RID: 13733
		private UsagePageList _freePageList;

		// Token: 0x040035A6 RID: 13734
		private UsagePageList _freeEntryList;

		// Token: 0x040035A7 RID: 13735
		private UsageEntryRef _lastRefHead;

		// Token: 0x040035A8 RID: 13736
		private UsageEntryRef _lastRefTail;

		// Token: 0x040035A9 RID: 13737
		private UsageEntryRef _addRef2Head;

		// Token: 0x040035AA RID: 13738
		private bool _blockReduce;
	}
}
