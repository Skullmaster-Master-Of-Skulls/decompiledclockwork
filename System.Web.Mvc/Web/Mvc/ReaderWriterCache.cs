using System;
using System.Collections.Generic;
using System.Threading;

namespace System.Web.Mvc
{
	// Token: 0x02000178 RID: 376
	internal abstract class ReaderWriterCache<TKey, TValue>
	{
		// Token: 0x06000A11 RID: 2577 RVA: 0x0001BD6B File Offset: 0x00019F6B
		protected ReaderWriterCache() : this(null)
		{
		}

		// Token: 0x06000A12 RID: 2578 RVA: 0x0001BD74 File Offset: 0x00019F74
		protected ReaderWriterCache(IEqualityComparer<TKey> comparer)
		{
			this._cache = new Dictionary<TKey, TValue>(comparer);
		}

		// Token: 0x1700024D RID: 589
		// (get) Token: 0x06000A13 RID: 2579 RVA: 0x0001BD93 File Offset: 0x00019F93
		protected Dictionary<TKey, TValue> Cache
		{
			get
			{
				return this._cache;
			}
		}

		// Token: 0x06000A14 RID: 2580 RVA: 0x0001BDA3 File Offset: 0x00019FA3
		protected TValue FetchOrCreateItem(TKey key, Func<TValue> creator)
		{
			return this.FetchOrCreateItem<Func<TValue>>(key, (Func<TValue> innerCreator) => innerCreator(), creator);
		}

		// Token: 0x06000A15 RID: 2581 RVA: 0x0001BDCC File Offset: 0x00019FCC
		protected TValue FetchOrCreateItem<TArgument>(TKey key, Func<TArgument, TValue> creator, TArgument state)
		{
			this._readerWriterLock.EnterReadLock();
			try
			{
				TValue result;
				if (this._cache.TryGetValue(key, out result))
				{
					return result;
				}
			}
			finally
			{
				this._readerWriterLock.ExitReadLock();
			}
			TValue tvalue = creator(state);
			this._readerWriterLock.EnterWriteLock();
			TValue result2;
			try
			{
				TValue tvalue2;
				if (this._cache.TryGetValue(key, out tvalue2))
				{
					result2 = tvalue2;
				}
				else
				{
					this._cache[key] = tvalue;
					result2 = tvalue;
				}
			}
			finally
			{
				this._readerWriterLock.ExitWriteLock();
			}
			return result2;
		}

		// Token: 0x040002B2 RID: 690
		private readonly Dictionary<TKey, TValue> _cache;

		// Token: 0x040002B3 RID: 691
		private readonly ReaderWriterLockSlim _readerWriterLock = new ReaderWriterLockSlim();
	}
}
