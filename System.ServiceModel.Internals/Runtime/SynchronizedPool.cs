using System;
using System.Collections.Generic;
using System.Security;
using System.Security.Permissions;
using System.Threading;

namespace System.Runtime
{
	// Token: 0x0200002A RID: 42
	internal class SynchronizedPool<T> where T : class
	{
		// Token: 0x0600014C RID: 332 RVA: 0x00005E10 File Offset: 0x00004010
		public SynchronizedPool(int maxCount)
		{
			int num = maxCount;
			int num2 = 16 + SynchronizedPool<T>.SynchronizedPoolHelper.ProcessorCount;
			if (num > num2)
			{
				num = num2;
			}
			this.maxCount = maxCount;
			this.entries = new SynchronizedPool<T>.Entry[num];
			this.pending = new SynchronizedPool<T>.PendingEntry[4];
			this.globalPool = new SynchronizedPool<T>.GlobalPool(maxCount);
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x0600014D RID: 333 RVA: 0x00005E5F File Offset: 0x0000405F
		private object ThisLock
		{
			get
			{
				return this;
			}
		}

		// Token: 0x0600014E RID: 334 RVA: 0x00005E64 File Offset: 0x00004064
		public void Clear()
		{
			SynchronizedPool<T>.Entry[] array = this.entries;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].value = default(T);
			}
			this.globalPool.Clear();
		}

		// Token: 0x0600014F RID: 335 RVA: 0x00005EA4 File Offset: 0x000040A4
		private void HandlePromotionFailure(int thisThreadID)
		{
			int num = this.promotionFailures + 1;
			if (num >= 64)
			{
				object thisLock = this.ThisLock;
				lock (thisLock)
				{
					this.entries = new SynchronizedPool<T>.Entry[this.entries.Length];
					this.globalPool.MaxCount = this.maxCount;
				}
				this.PromoteThread(thisThreadID);
				return;
			}
			this.promotionFailures = num;
		}

		// Token: 0x06000150 RID: 336 RVA: 0x00005F20 File Offset: 0x00004120
		private bool PromoteThread(int thisThreadID)
		{
			object thisLock = this.ThisLock;
			lock (thisLock)
			{
				for (int i = 0; i < this.entries.Length; i++)
				{
					int threadID = this.entries[i].threadID;
					if (threadID == thisThreadID)
					{
						return true;
					}
					if (threadID == 0)
					{
						this.globalPool.DecrementMaxCount();
						this.entries[i].threadID = thisThreadID;
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06000151 RID: 337 RVA: 0x00005FB4 File Offset: 0x000041B4
		private void RecordReturnToGlobalPool(int thisThreadID)
		{
			SynchronizedPool<T>.PendingEntry[] array = this.pending;
			int i = 0;
			while (i < array.Length)
			{
				int threadID = array[i].threadID;
				if (threadID == thisThreadID)
				{
					int num = array[i].returnCount + 1;
					if (num < 64)
					{
						array[i].returnCount = num;
						return;
					}
					array[i].returnCount = 0;
					if (!this.PromoteThread(thisThreadID))
					{
						this.HandlePromotionFailure(thisThreadID);
						return;
					}
					break;
				}
				else
				{
					if (threadID == 0)
					{
						break;
					}
					i++;
				}
			}
		}

		// Token: 0x06000152 RID: 338 RVA: 0x0000602C File Offset: 0x0000422C
		private void RecordTakeFromGlobalPool(int thisThreadID)
		{
			SynchronizedPool<T>.PendingEntry[] array = this.pending;
			for (int i = 0; i < array.Length; i++)
			{
				int threadID = array[i].threadID;
				if (threadID == thisThreadID)
				{
					return;
				}
				if (threadID == 0)
				{
					SynchronizedPool<T>.PendingEntry[] obj = array;
					lock (obj)
					{
						if (array[i].threadID == 0)
						{
							array[i].threadID = thisThreadID;
							return;
						}
					}
				}
			}
			if (array.Length >= 128)
			{
				this.pending = new SynchronizedPool<T>.PendingEntry[array.Length];
				return;
			}
			SynchronizedPool<T>.PendingEntry[] destinationArray = new SynchronizedPool<T>.PendingEntry[array.Length * 2];
			Array.Copy(array, destinationArray, array.Length);
			this.pending = destinationArray;
		}

		// Token: 0x06000153 RID: 339 RVA: 0x000060E4 File Offset: 0x000042E4
		public bool Return(T value)
		{
			int managedThreadId = Thread.CurrentThread.ManagedThreadId;
			return managedThreadId != 0 && (this.ReturnToPerThreadPool(managedThreadId, value) || this.ReturnToGlobalPool(managedThreadId, value));
		}

		// Token: 0x06000154 RID: 340 RVA: 0x00006118 File Offset: 0x00004318
		private bool ReturnToPerThreadPool(int thisThreadID, T value)
		{
			SynchronizedPool<T>.Entry[] array = this.entries;
			int i = 0;
			while (i < array.Length)
			{
				int threadID = array[i].threadID;
				if (threadID == thisThreadID)
				{
					if (array[i].value == null)
					{
						array[i].value = value;
						return true;
					}
					return false;
				}
				else
				{
					if (threadID == 0)
					{
						break;
					}
					i++;
				}
			}
			return false;
		}

		// Token: 0x06000155 RID: 341 RVA: 0x00006173 File Offset: 0x00004373
		private bool ReturnToGlobalPool(int thisThreadID, T value)
		{
			this.RecordReturnToGlobalPool(thisThreadID);
			return this.globalPool.Return(value);
		}

		// Token: 0x06000156 RID: 342 RVA: 0x00006188 File Offset: 0x00004388
		public T Take()
		{
			int managedThreadId = Thread.CurrentThread.ManagedThreadId;
			if (managedThreadId == 0)
			{
				return default(T);
			}
			T t = this.TakeFromPerThreadPool(managedThreadId);
			if (t != null)
			{
				return t;
			}
			return this.TakeFromGlobalPool(managedThreadId);
		}

		// Token: 0x06000157 RID: 343 RVA: 0x000061C8 File Offset: 0x000043C8
		private T TakeFromPerThreadPool(int thisThreadID)
		{
			SynchronizedPool<T>.Entry[] array = this.entries;
			int i = 0;
			while (i < array.Length)
			{
				int threadID = array[i].threadID;
				if (threadID == thisThreadID)
				{
					T value = array[i].value;
					if (value != null)
					{
						array[i].value = default(T);
						return value;
					}
					return default(T);
				}
				else
				{
					if (threadID == 0)
					{
						break;
					}
					i++;
				}
			}
			return default(T);
		}

		// Token: 0x06000158 RID: 344 RVA: 0x0000623C File Offset: 0x0000443C
		private T TakeFromGlobalPool(int thisThreadID)
		{
			this.RecordTakeFromGlobalPool(thisThreadID);
			return this.globalPool.Take();
		}

		// Token: 0x0400009A RID: 154
		private const int maxPendingEntries = 128;

		// Token: 0x0400009B RID: 155
		private const int maxPromotionFailures = 64;

		// Token: 0x0400009C RID: 156
		private const int maxReturnsBeforePromotion = 64;

		// Token: 0x0400009D RID: 157
		private const int maxThreadItemsPerProcessor = 16;

		// Token: 0x0400009E RID: 158
		private SynchronizedPool<T>.Entry[] entries;

		// Token: 0x0400009F RID: 159
		private SynchronizedPool<T>.GlobalPool globalPool;

		// Token: 0x040000A0 RID: 160
		private int maxCount;

		// Token: 0x040000A1 RID: 161
		private SynchronizedPool<T>.PendingEntry[] pending;

		// Token: 0x040000A2 RID: 162
		private int promotionFailures;

		// Token: 0x02000080 RID: 128
		private struct Entry
		{
			// Token: 0x04000279 RID: 633
			public int threadID;

			// Token: 0x0400027A RID: 634
			public T value;
		}

		// Token: 0x02000081 RID: 129
		private struct PendingEntry
		{
			// Token: 0x0400027B RID: 635
			public int returnCount;

			// Token: 0x0400027C RID: 636
			public int threadID;
		}

		// Token: 0x02000082 RID: 130
		private static class SynchronizedPoolHelper
		{
			// Token: 0x06000404 RID: 1028 RVA: 0x000130B8 File Offset: 0x000112B8
			[SecuritySafeCritical]
			[EnvironmentPermission(SecurityAction.Assert, Read = "NUMBER_OF_PROCESSORS")]
			private static int GetProcessorCount()
			{
				return Environment.ProcessorCount;
			}

			// Token: 0x0400027D RID: 637
			public static readonly int ProcessorCount = SynchronizedPool<T>.SynchronizedPoolHelper.GetProcessorCount();
		}

		// Token: 0x02000083 RID: 131
		private class GlobalPool
		{
			// Token: 0x06000406 RID: 1030 RVA: 0x000130CB File Offset: 0x000112CB
			public GlobalPool(int maxCount)
			{
				this.items = new Stack<T>();
				this.maxCount = maxCount;
			}

			// Token: 0x170000B1 RID: 177
			// (get) Token: 0x06000407 RID: 1031 RVA: 0x000130E5 File Offset: 0x000112E5
			// (set) Token: 0x06000408 RID: 1032 RVA: 0x000130F0 File Offset: 0x000112F0
			public int MaxCount
			{
				get
				{
					return this.maxCount;
				}
				set
				{
					object thisLock = this.ThisLock;
					lock (thisLock)
					{
						while (this.items.Count > value)
						{
							this.items.Pop();
						}
						this.maxCount = value;
					}
				}
			}

			// Token: 0x170000B2 RID: 178
			// (get) Token: 0x06000409 RID: 1033 RVA: 0x00005E5F File Offset: 0x0000405F
			private object ThisLock
			{
				get
				{
					return this;
				}
			}

			// Token: 0x0600040A RID: 1034 RVA: 0x00013150 File Offset: 0x00011350
			public void DecrementMaxCount()
			{
				object thisLock = this.ThisLock;
				lock (thisLock)
				{
					if (this.items.Count == this.maxCount)
					{
						this.items.Pop();
					}
					this.maxCount--;
				}
			}

			// Token: 0x0600040B RID: 1035 RVA: 0x000131B8 File Offset: 0x000113B8
			public T Take()
			{
				if (this.items.Count > 0)
				{
					object thisLock = this.ThisLock;
					lock (thisLock)
					{
						if (this.items.Count > 0)
						{
							return this.items.Pop();
						}
					}
				}
				return default(T);
			}

			// Token: 0x0600040C RID: 1036 RVA: 0x00013228 File Offset: 0x00011428
			public bool Return(T value)
			{
				if (this.items.Count < this.MaxCount)
				{
					object thisLock = this.ThisLock;
					lock (thisLock)
					{
						if (this.items.Count < this.MaxCount)
						{
							this.items.Push(value);
							return true;
						}
					}
					return false;
				}
				return false;
			}

			// Token: 0x0600040D RID: 1037 RVA: 0x0001329C File Offset: 0x0001149C
			public void Clear()
			{
				object thisLock = this.ThisLock;
				lock (thisLock)
				{
					this.items.Clear();
				}
			}

			// Token: 0x0400027E RID: 638
			private Stack<T> items;

			// Token: 0x0400027F RID: 639
			private int maxCount;
		}
	}
}
