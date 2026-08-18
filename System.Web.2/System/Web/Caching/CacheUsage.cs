using System;
using System.Threading;

namespace System.Web.Caching
{
	// Token: 0x020008A6 RID: 2214
	internal class CacheUsage
	{
		// Token: 0x06006786 RID: 26502 RVA: 0x0017015C File Offset: 0x0016E35C
		internal CacheUsage(CacheSingle cacheSingle)
		{
			this._cacheSingle = cacheSingle;
			this._buckets = new UsageBucket[5];
			byte b = 0;
			while ((int)b < this._buckets.Length)
			{
				this._buckets[(int)b] = new UsageBucket(this, b);
				b += 1;
			}
		}

		// Token: 0x17001CDA RID: 7386
		// (get) Token: 0x06006787 RID: 26503 RVA: 0x001701A5 File Offset: 0x0016E3A5
		internal CacheSingle CacheSingle
		{
			get
			{
				return this._cacheSingle;
			}
		}

		// Token: 0x06006788 RID: 26504 RVA: 0x001701B0 File Offset: 0x0016E3B0
		internal void Add(CacheEntry cacheEntry)
		{
			byte usageBucket = cacheEntry.UsageBucket;
			this._buckets[(int)usageBucket].AddCacheEntry(cacheEntry);
		}

		// Token: 0x06006789 RID: 26505 RVA: 0x001701D4 File Offset: 0x0016E3D4
		internal void Remove(CacheEntry cacheEntry)
		{
			byte usageBucket = cacheEntry.UsageBucket;
			if (usageBucket != 255)
			{
				this._buckets[(int)usageBucket].RemoveCacheEntry(cacheEntry);
			}
		}

		// Token: 0x0600678A RID: 26506 RVA: 0x00170200 File Offset: 0x0016E400
		internal void Update(CacheEntry cacheEntry)
		{
			byte usageBucket = cacheEntry.UsageBucket;
			if (usageBucket != 255)
			{
				this._buckets[(int)usageBucket].UpdateCacheEntry(cacheEntry);
			}
		}

		// Token: 0x0600678B RID: 26507 RVA: 0x0017022C File Offset: 0x0016E42C
		internal int FlushUnderUsedItems(int toFlush, ref int publicEntriesFlushed, ref int ocEntriesFlushed)
		{
			int num = 0;
			if (Interlocked.Exchange(ref this._inFlush, 1) == 0)
			{
				try
				{
					foreach (UsageBucket usageBucket in this._buckets)
					{
						int num2 = usageBucket.FlushUnderUsedItems(toFlush - num, false, ref publicEntriesFlushed, ref ocEntriesFlushed);
						num += num2;
						if (num >= toFlush)
						{
							break;
						}
					}
					if (num < toFlush)
					{
						foreach (UsageBucket usageBucket2 in this._buckets)
						{
							int num3 = usageBucket2.FlushUnderUsedItems(toFlush - num, true, ref publicEntriesFlushed, ref ocEntriesFlushed);
							num += num3;
							if (num >= toFlush)
							{
								break;
							}
						}
					}
				}
				finally
				{
					Interlocked.Exchange(ref this._inFlush, 0);
				}
			}
			return num;
		}

		// Token: 0x040035AB RID: 13739
		internal static readonly TimeSpan NEWADD_INTERVAL = new TimeSpan(0, 0, 10);

		// Token: 0x040035AC RID: 13740
		internal static readonly TimeSpan CORRELATED_REQUEST_TIMEOUT = new TimeSpan(0, 0, 1);

		// Token: 0x040035AD RID: 13741
		internal static readonly TimeSpan MIN_LIFETIME_FOR_USAGE = CacheUsage.NEWADD_INTERVAL;

		// Token: 0x040035AE RID: 13742
		private const byte NUMBUCKETS = 5;

		// Token: 0x040035AF RID: 13743
		private const int MAX_REMOVE = 1024;

		// Token: 0x040035B0 RID: 13744
		private readonly CacheSingle _cacheSingle;

		// Token: 0x040035B1 RID: 13745
		internal readonly UsageBucket[] _buckets;

		// Token: 0x040035B2 RID: 13746
		private int _inFlush;
	}
}
