using System;
using System.Collections;
using System.Threading;
using System.Web.Util;

namespace System.Web.Caching
{
	// Token: 0x02000873 RID: 2163
	internal class CacheMultiple : CacheInternal
	{
		// Token: 0x060065E9 RID: 26089 RVA: 0x0016754C File Offset: 0x0016574C
		internal CacheMultiple(CacheCommon cacheCommon, int numSingleCaches) : base(cacheCommon)
		{
			this._cacheIndexMask = numSingleCaches - 1;
			this._cachesRefs = new DisposableGCHandleRef<CacheSingle>[numSingleCaches];
			for (int i = 0; i < numSingleCaches; i++)
			{
				this._cachesRefs[i] = new DisposableGCHandleRef<CacheSingle>(new CacheSingle(cacheCommon, this, i));
			}
		}

		// Token: 0x060065EA RID: 26090 RVA: 0x00167598 File Offset: 0x00165798
		protected override void Dispose(bool disposing)
		{
			if (disposing && Interlocked.Exchange(ref this._disposed, 1) == 0)
			{
				foreach (DisposableGCHandleRef<CacheSingle> disposableGCHandleRef in this._cachesRefs)
				{
					disposableGCHandleRef.Target.Dispose();
				}
			}
			base.Dispose(disposing);
		}

		// Token: 0x17001C8A RID: 7306
		// (get) Token: 0x060065EB RID: 26091 RVA: 0x001675E4 File Offset: 0x001657E4
		internal override int PublicCount
		{
			get
			{
				int num = 0;
				foreach (DisposableGCHandleRef<CacheSingle> disposableGCHandleRef in this._cachesRefs)
				{
					num += disposableGCHandleRef.Target.PublicCount;
				}
				return num;
			}
		}

		// Token: 0x17001C8B RID: 7307
		// (get) Token: 0x060065EC RID: 26092 RVA: 0x0016761C File Offset: 0x0016581C
		internal override long TotalCount
		{
			get
			{
				long num = 0L;
				foreach (DisposableGCHandleRef<CacheSingle> disposableGCHandleRef in this._cachesRefs)
				{
					num += disposableGCHandleRef.Target.TotalCount;
				}
				return num;
			}
		}

		// Token: 0x060065ED RID: 26093 RVA: 0x00167654 File Offset: 0x00165854
		internal override IDictionaryEnumerator CreateEnumerator(bool getPrivateItems = false, CacheGetOptions options = CacheGetOptions.None)
		{
			IDictionaryEnumerator[] array = new IDictionaryEnumerator[this._cachesRefs.Length];
			int i = 0;
			int num = this._cachesRefs.Length;
			while (i < num)
			{
				array[i] = this._cachesRefs[i].Target.CreateEnumerator(getPrivateItems, options);
				i++;
			}
			return new AggregateEnumerator(array);
		}

		// Token: 0x060065EE RID: 26094 RVA: 0x001676A4 File Offset: 0x001658A4
		internal CacheSingle GetCacheSingle(int hashCode)
		{
			if (hashCode < 0)
			{
				hashCode = ((hashCode == int.MinValue) ? 0 : (-hashCode));
			}
			int num = hashCode & this._cacheIndexMask;
			return this._cachesRefs[num].Target;
		}

		// Token: 0x060065EF RID: 26095 RVA: 0x001676DC File Offset: 0x001658DC
		internal override CacheEntry UpdateCache(CacheKey cacheKey, CacheEntry newEntry, bool replace, CacheItemRemovedReason removedReason, out object valueOld)
		{
			int hashCode = cacheKey.Key.GetHashCode();
			CacheSingle cacheSingle = this.GetCacheSingle(hashCode);
			return cacheSingle.UpdateCache(cacheKey, newEntry, replace, removedReason, out valueOld);
		}

		// Token: 0x060065F0 RID: 26096 RVA: 0x0016770C File Offset: 0x0016590C
		internal override long TrimIfNecessary(int percent)
		{
			long num = 0L;
			foreach (DisposableGCHandleRef<CacheSingle> disposableGCHandleRef in this._cachesRefs)
			{
				num += disposableGCHandleRef.Target.TrimIfNecessary(percent);
			}
			return num;
		}

		// Token: 0x060065F1 RID: 26097 RVA: 0x00167748 File Offset: 0x00165948
		internal override void EnableExpirationTimer(bool enable)
		{
			foreach (DisposableGCHandleRef<CacheSingle> disposableGCHandleRef in this._cachesRefs)
			{
				disposableGCHandleRef.Target.EnableExpirationTimer(enable);
			}
		}

		// Token: 0x04003487 RID: 13447
		private int _disposed;

		// Token: 0x04003488 RID: 13448
		private DisposableGCHandleRef<CacheSingle>[] _cachesRefs;

		// Token: 0x04003489 RID: 13449
		private int _cacheIndexMask;
	}
}
