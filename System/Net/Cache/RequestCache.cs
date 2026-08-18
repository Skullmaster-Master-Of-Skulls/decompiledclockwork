using System;
using System.Collections.Specialized;
using System.IO;

namespace System.Net.Cache
{
	// Token: 0x02000564 RID: 1380
	internal abstract class RequestCache
	{
		// Token: 0x06002A40 RID: 10816 RVA: 0x000B279F File Offset: 0x000B179F
		protected RequestCache(bool isPrivateCache, bool canWrite)
		{
			this._IsPrivateCache = isPrivateCache;
			this._CanWrite = canWrite;
		}

		// Token: 0x170008B7 RID: 2231
		// (get) Token: 0x06002A41 RID: 10817 RVA: 0x000B27B5 File Offset: 0x000B17B5
		internal bool IsPrivateCache
		{
			get
			{
				return this._IsPrivateCache;
			}
		}

		// Token: 0x170008B8 RID: 2232
		// (get) Token: 0x06002A42 RID: 10818 RVA: 0x000B27BD File Offset: 0x000B17BD
		internal bool CanWrite
		{
			get
			{
				return this._CanWrite;
			}
		}

		// Token: 0x06002A43 RID: 10819
		internal abstract Stream Retrieve(string key, out RequestCacheEntry cacheEntry);

		// Token: 0x06002A44 RID: 10820
		internal abstract Stream Store(string key, long contentLength, DateTime expiresUtc, DateTime lastModifiedUtc, TimeSpan maxStale, StringCollection entryMetadata, StringCollection systemMetadata);

		// Token: 0x06002A45 RID: 10821
		internal abstract void Remove(string key);

		// Token: 0x06002A46 RID: 10822
		internal abstract void Update(string key, DateTime expiresUtc, DateTime lastModifiedUtc, DateTime lastSynchronizedUtc, TimeSpan maxStale, StringCollection entryMetadata, StringCollection systemMetadata);

		// Token: 0x06002A47 RID: 10823
		internal abstract bool TryRetrieve(string key, out RequestCacheEntry cacheEntry, out Stream readStream);

		// Token: 0x06002A48 RID: 10824
		internal abstract bool TryStore(string key, long contentLength, DateTime expiresUtc, DateTime lastModifiedUtc, TimeSpan maxStale, StringCollection entryMetadata, StringCollection systemMetadata, out Stream writeStream);

		// Token: 0x06002A49 RID: 10825
		internal abstract bool TryRemove(string key);

		// Token: 0x06002A4A RID: 10826
		internal abstract bool TryUpdate(string key, DateTime expiresUtc, DateTime lastModifiedUtc, DateTime lastSynchronizedUtc, TimeSpan maxStale, StringCollection entryMetadata, StringCollection systemMetadata);

		// Token: 0x06002A4B RID: 10827
		internal abstract void UnlockEntry(Stream retrieveStream);

		// Token: 0x040028EA RID: 10474
		internal static readonly char[] LineSplits = new char[]
		{
			'\r',
			'\n'
		};

		// Token: 0x040028EB RID: 10475
		private bool _IsPrivateCache;

		// Token: 0x040028EC RID: 10476
		private bool _CanWrite;
	}
}
