using System;
using System.Collections;
using System.Collections.Specialized;
using System.Threading;
using System.Web.Configuration;

namespace System.Web.Caching
{
	// Token: 0x0200086D RID: 2157
	internal sealed class AspNetCache : CacheStoreProvider
	{
		// Token: 0x0600659F RID: 26015 RVA: 0x001661A0 File Offset: 0x001643A0
		public AspNetCache()
		{
			this._cacheInternal = CacheInternal.Create();
			Interlocked.Exchange(ref this._cacheInternal._refCount, 1);
		}

		// Token: 0x060065A0 RID: 26016 RVA: 0x001661CC File Offset: 0x001643CC
		internal AspNetCache(bool isPublic)
		{
			this._isPublic = isPublic;
			this._cacheInternal = CacheInternal.Create();
			Interlocked.Exchange(ref this._cacheInternal._refCount, 1);
		}

		// Token: 0x060065A1 RID: 26017 RVA: 0x001661FF File Offset: 0x001643FF
		internal AspNetCache(AspNetCache cache, bool isPublic)
		{
			this._isPublic = isPublic;
			this._cacheInternal = cache._cacheInternal;
			Interlocked.Increment(ref this._cacheInternal._refCount);
		}

		// Token: 0x17001C80 RID: 7296
		// (get) Token: 0x060065A2 RID: 26018 RVA: 0x00166232 File Offset: 0x00164432
		public override long ItemCount
		{
			get
			{
				if (this._isPublic)
				{
					return (long)this._cacheInternal.PublicCount;
				}
				return this._cacheInternal.TotalCount - (long)this._cacheInternal.PublicCount;
			}
		}

		// Token: 0x17001C81 RID: 7297
		// (get) Token: 0x060065A3 RID: 26019 RVA: 0x00166261 File Offset: 0x00164461
		public override long SizeInBytes
		{
			get
			{
				return this._cacheInternal.ApproximateSize;
			}
		}

		// Token: 0x060065A4 RID: 26020 RVA: 0x00166270 File Offset: 0x00164470
		public override void Initialize(string name, NameValueCollection config)
		{
			bool isPublic = this._isPublic;
			if (bool.TryParse(config["isPublic"], out isPublic))
			{
				this._isPublic = isPublic;
			}
			CacheSection cache = RuntimeConfig.GetAppConfig().Cache;
			this._cacheInternal.ReadCacheInternalConfig(cache);
		}

		// Token: 0x060065A5 RID: 26021 RVA: 0x001662B8 File Offset: 0x001644B8
		public override object Add(string key, object item, CacheInsertOptions options)
		{
			CacheInsertOptions cacheInsertOptions = options ?? AspNetCache.DefaultInsertOptions;
			return this._cacheInternal.DoInsert(this._isPublic, key, item, cacheInsertOptions.Dependencies, cacheInsertOptions.AbsoluteExpiration, cacheInsertOptions.SlidingExpiration, cacheInsertOptions.Priority, cacheInsertOptions.OnRemovedCallback, false);
		}

		// Token: 0x060065A6 RID: 26022 RVA: 0x00166302 File Offset: 0x00164502
		public override object Get(string key)
		{
			return this._cacheInternal.DoGet(this._isPublic, key, CacheGetOptions.None);
		}

		// Token: 0x060065A7 RID: 26023 RVA: 0x00166318 File Offset: 0x00164518
		public override void Insert(string key, object item, CacheInsertOptions options)
		{
			CacheInsertOptions cacheInsertOptions = options ?? AspNetCache.DefaultInsertOptions;
			this._cacheInternal.DoInsert(this._isPublic, key, item, cacheInsertOptions.Dependencies, cacheInsertOptions.AbsoluteExpiration, cacheInsertOptions.SlidingExpiration, cacheInsertOptions.Priority, cacheInsertOptions.OnRemovedCallback, true);
		}

		// Token: 0x060065A8 RID: 26024 RVA: 0x00166363 File Offset: 0x00164563
		public override object Remove(string key)
		{
			return this.Remove(key, CacheItemRemovedReason.Removed);
		}

		// Token: 0x060065A9 RID: 26025 RVA: 0x00166370 File Offset: 0x00164570
		public override object Remove(string key, CacheItemRemovedReason reason)
		{
			CacheKey cacheKey = new CacheKey(key, this._isPublic);
			return this._cacheInternal.Remove(cacheKey, reason);
		}

		// Token: 0x060065AA RID: 26026 RVA: 0x00166397 File Offset: 0x00164597
		public override long Trim(int percent)
		{
			return this._cacheInternal.TrimCache(percent);
		}

		// Token: 0x060065AB RID: 26027 RVA: 0x001663A8 File Offset: 0x001645A8
		public override bool AddDependent(string key, CacheDependency dependency, out DateTime utcLastUpdated)
		{
			CacheEntry cacheEntry = (CacheEntry)this._cacheInternal.DoGet(this._isPublic, key, CacheGetOptions.ReturnCacheEntry);
			if (cacheEntry != null)
			{
				utcLastUpdated = cacheEntry.UtcCreated;
				cacheEntry.AddDependent(dependency);
				if (cacheEntry.State == CacheEntry.EntryState.AddedToCache)
				{
					return true;
				}
			}
			utcLastUpdated = DateTime.MinValue;
			return false;
		}

		// Token: 0x060065AC RID: 26028 RVA: 0x001663FC File Offset: 0x001645FC
		public override void RemoveDependent(string key, CacheDependency dependency)
		{
			CacheEntry cacheEntry = (CacheEntry)this._cacheInternal.DoGet(this._isPublic, key, CacheGetOptions.ReturnCacheEntry);
			if (cacheEntry != null)
			{
				cacheEntry.RemoveDependent(dependency);
			}
		}

		// Token: 0x060065AD RID: 26029 RVA: 0x0016642C File Offset: 0x0016462C
		public override IDictionaryEnumerator GetEnumerator()
		{
			return this._cacheInternal.CreateEnumerator(!this._isPublic, CacheGetOptions.None);
		}

		// Token: 0x060065AE RID: 26030 RVA: 0x00166444 File Offset: 0x00164644
		public override bool Equals(object obj)
		{
			AspNetCache aspNetCache = obj as AspNetCache;
			if (aspNetCache != null)
			{
				return this._cacheInternal == aspNetCache._cacheInternal;
			}
			return base.Equals(obj);
		}

		// Token: 0x060065AF RID: 26031 RVA: 0x00166471 File Offset: 0x00164671
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x060065B0 RID: 26032 RVA: 0x0016647C File Offset: 0x0016467C
		public override void Dispose()
		{
			if (!this._isDisposed)
			{
				lock (this)
				{
					if (!this._isDisposed)
					{
						this._isDisposed = true;
						Interlocked.Decrement(ref this._cacheInternal._refCount);
						this._cacheInternal.Dispose();
					}
				}
			}
		}

		// Token: 0x04003444 RID: 13380
		private static CacheInsertOptions DefaultInsertOptions = new CacheInsertOptions();

		// Token: 0x04003445 RID: 13381
		internal CacheInternal _cacheInternal;

		// Token: 0x04003446 RID: 13382
		private bool _isPublic = true;

		// Token: 0x04003447 RID: 13383
		private bool _isDisposed;
	}
}
