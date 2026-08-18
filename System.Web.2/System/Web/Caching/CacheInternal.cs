using System;
using System.Collections;
using System.Web.Configuration;
using System.Web.Util;

namespace System.Web.Caching
{
	// Token: 0x02000870 RID: 2160
	internal abstract class CacheInternal : IDisposable
	{
		// Token: 0x17001C82 RID: 7298
		// (get) Token: 0x060065BC RID: 26044
		internal abstract int PublicCount { get; }

		// Token: 0x17001C83 RID: 7299
		// (get) Token: 0x060065BD RID: 26045
		internal abstract long TotalCount { get; }

		// Token: 0x060065BE RID: 26046
		internal abstract IDictionaryEnumerator CreateEnumerator(bool getPrivateItems = false, CacheGetOptions options = CacheGetOptions.None);

		// Token: 0x060065BF RID: 26047
		internal abstract CacheEntry UpdateCache(CacheKey cacheKey, CacheEntry newEntry, bool replace, CacheItemRemovedReason removedReason, out object valueOld);

		// Token: 0x060065C0 RID: 26048
		internal abstract long TrimIfNecessary(int percent);

		// Token: 0x060065C1 RID: 26049
		internal abstract void EnableExpirationTimer(bool enable);

		// Token: 0x060065C2 RID: 26050 RVA: 0x001668E4 File Offset: 0x00164AE4
		internal static CacheInternal Create()
		{
			CacheCommon cacheCommon = new CacheCommon();
			uint num = (uint)SystemInfo.GetNumProcessCPUs();
			int num2 = 1;
			for (num -= 1U; num > 0U; num >>= 1)
			{
				num2 <<= 1;
			}
			CacheInternal cacheInternal;
			if (num2 == 1)
			{
				cacheInternal = new CacheSingle(cacheCommon, null, 0);
			}
			else
			{
				cacheInternal = new CacheMultiple(cacheCommon, num2);
			}
			cacheCommon.SetCacheInternal(cacheInternal);
			cacheCommon.ResetFromConfigSettings();
			return cacheInternal;
		}

		// Token: 0x060065C3 RID: 26051 RVA: 0x00166938 File Offset: 0x00164B38
		protected CacheInternal(CacheCommon cacheCommon)
		{
			this._cacheCommon = cacheCommon;
		}

		// Token: 0x060065C4 RID: 26052 RVA: 0x00166947 File Offset: 0x00164B47
		protected virtual void Dispose(bool disposing)
		{
			this._disposed = 1;
			this._cacheCommon.Dispose(disposing);
		}

		// Token: 0x060065C5 RID: 26053 RVA: 0x0016695C File Offset: 0x00164B5C
		public void Dispose()
		{
			if (this._refCount <= 0)
			{
				this.Dispose(true);
			}
		}

		// Token: 0x17001C84 RID: 7300
		// (get) Token: 0x060065C6 RID: 26054 RVA: 0x0016696E File Offset: 0x00164B6E
		internal bool IsDisposed
		{
			get
			{
				return this._disposed == 1;
			}
		}

		// Token: 0x060065C7 RID: 26055 RVA: 0x00166979 File Offset: 0x00164B79
		internal virtual void ReadCacheInternalConfig(CacheSection cacheSection)
		{
			this._cacheCommon.ReadCacheInternalConfig(cacheSection);
		}

		// Token: 0x060065C8 RID: 26056 RVA: 0x00166987 File Offset: 0x00164B87
		internal virtual long TrimCache(int percent)
		{
			return this._cacheCommon.CacheManagerThread(percent);
		}

		// Token: 0x17001C85 RID: 7301
		// (get) Token: 0x060065C9 RID: 26057 RVA: 0x00166995 File Offset: 0x00164B95
		internal long ApproximateSize
		{
			get
			{
				return this._cacheCommon._srefMultiple.ApproximateSize;
			}
		}

		// Token: 0x17001C86 RID: 7302
		// (get) Token: 0x060065CA RID: 26058 RVA: 0x001669A7 File Offset: 0x00164BA7
		internal bool EnableExpiration
		{
			get
			{
				return this._cacheCommon._enableExpiration;
			}
		}

		// Token: 0x17001C87 RID: 7303
		internal object this[string key]
		{
			get
			{
				return this.Get(key);
			}
		}

		// Token: 0x060065CC RID: 26060 RVA: 0x001669BD File Offset: 0x00164BBD
		internal object Get(string key)
		{
			return this.DoGet(false, key, CacheGetOptions.None);
		}

		// Token: 0x060065CD RID: 26061 RVA: 0x001669C8 File Offset: 0x00164BC8
		internal object Get(string key, CacheGetOptions getOptions)
		{
			return this.DoGet(false, key, getOptions);
		}

		// Token: 0x060065CE RID: 26062 RVA: 0x001669D4 File Offset: 0x00164BD4
		internal object DoGet(bool isPublic, string key, CacheGetOptions getOptions)
		{
			CacheKey cacheKey = new CacheKey(key, isPublic);
			object obj;
			CacheEntry cacheEntry = this.UpdateCache(cacheKey, null, false, CacheItemRemovedReason.Removed, out obj);
			if (cacheEntry == null)
			{
				return null;
			}
			if ((getOptions & CacheGetOptions.ReturnCacheEntry) != CacheGetOptions.None)
			{
				return cacheEntry;
			}
			return cacheEntry.Value;
		}

		// Token: 0x060065CF RID: 26063 RVA: 0x00166A08 File Offset: 0x00164C08
		internal object DoInsert(bool isPublic, string key, object value, CacheDependency dependencies, DateTime utcAbsoluteExpiration, TimeSpan slidingExpiration, CacheItemPriority priority, CacheItemRemovedCallback onRemoveCallback, bool replace)
		{
			object result;
			try
			{
				CacheEntry cacheEntry = new CacheEntry(key, value, dependencies, onRemoveCallback, utcAbsoluteExpiration, slidingExpiration, priority, isPublic, this);
				object obj;
				cacheEntry = this.UpdateCache(cacheEntry, cacheEntry, replace, CacheItemRemovedReason.Removed, out obj);
				if (cacheEntry != null)
				{
					result = cacheEntry.Value;
				}
				else
				{
					result = null;
				}
			}
			finally
			{
				if (dependencies != null)
				{
					((IDisposable)dependencies).Dispose();
				}
			}
			return result;
		}

		// Token: 0x060065D0 RID: 26064 RVA: 0x00166A68 File Offset: 0x00164C68
		internal object Remove(string key)
		{
			CacheKey cacheKey = new CacheKey(key, false);
			return this.DoRemove(cacheKey, CacheItemRemovedReason.Removed);
		}

		// Token: 0x060065D1 RID: 26065 RVA: 0x00166A85 File Offset: 0x00164C85
		internal object Remove(CacheKey cacheKey, CacheItemRemovedReason reason)
		{
			return this.DoRemove(cacheKey, reason);
		}

		// Token: 0x060065D2 RID: 26066 RVA: 0x00166A90 File Offset: 0x00164C90
		internal object DoRemove(CacheKey cacheKey, CacheItemRemovedReason reason)
		{
			object result;
			this.UpdateCache(cacheKey, null, true, reason, out result);
			return result;
		}

		// Token: 0x04003458 RID: 13400
		internal const string PrefixFIRST = "A";

		// Token: 0x04003459 RID: 13401
		internal const string PrefixResourceProvider = "A";

		// Token: 0x0400345A RID: 13402
		internal const string PrefixMapPathVPPFile = "Bf";

		// Token: 0x0400345B RID: 13403
		internal const string PrefixMapPathVPPDir = "Bd";

		// Token: 0x0400345C RID: 13404
		internal const string PrefixOutputCache = "a";

		// Token: 0x0400345D RID: 13405
		internal const string PrefixSqlCacheDependency = "b";

		// Token: 0x0400345E RID: 13406
		internal const string PrefixMemoryBuildResult = "c";

		// Token: 0x0400345F RID: 13407
		internal const string PrefixPathData = "d";

		// Token: 0x04003460 RID: 13408
		internal const string PrefixHttpCapabilities = "e";

		// Token: 0x04003461 RID: 13409
		internal const string PrefixMapPath = "f";

		// Token: 0x04003462 RID: 13410
		internal const string PrefixHttpSys = "g";

		// Token: 0x04003463 RID: 13411
		internal const string PrefixFileSecurity = "h";

		// Token: 0x04003464 RID: 13412
		internal const string PrefixInProcSessionState = "j";

		// Token: 0x04003465 RID: 13413
		internal const string PrefixStateApplication = "k";

		// Token: 0x04003466 RID: 13414
		internal const string PrefixPartialCachingControl = "l";

		// Token: 0x04003467 RID: 13415
		internal const string UNUSED = "m";

		// Token: 0x04003468 RID: 13416
		internal const string PrefixAdRotator = "n";

		// Token: 0x04003469 RID: 13417
		internal const string PrefixWebServiceDataSource = "o";

		// Token: 0x0400346A RID: 13418
		internal const string PrefixLoadXPath = "p";

		// Token: 0x0400346B RID: 13419
		internal const string PrefixLoadXml = "q";

		// Token: 0x0400346C RID: 13420
		internal const string PrefixLoadTransform = "r";

		// Token: 0x0400346D RID: 13421
		internal const string PrefixAspCompatThreading = "s";

		// Token: 0x0400346E RID: 13422
		internal const string PrefixDataSourceControl = "u";

		// Token: 0x0400346F RID: 13423
		internal const string PrefixValidationSentinel = "w";

		// Token: 0x04003470 RID: 13424
		internal const string PrefixWebEventResource = "x";

		// Token: 0x04003471 RID: 13425
		internal const string PrefixAssemblyPath = "y";

		// Token: 0x04003472 RID: 13426
		internal const string PrefixBrowserCapsHash = "z";

		// Token: 0x04003473 RID: 13427
		internal const string PrefixLAST = "z";

		// Token: 0x04003474 RID: 13428
		protected CacheCommon _cacheCommon;

		// Token: 0x04003475 RID: 13429
		internal int _refCount;

		// Token: 0x04003476 RID: 13430
		private int _disposed;
	}
}
