using System;
using System.Collections.Specialized;
using System.IO;

namespace System.Net.Cache
{
	// Token: 0x0200030D RID: 781
	internal abstract class RequestCache
	{
		// Token: 0x06001BE7 RID: 7143 RVA: 0x000856D1 File Offset: 0x000838D1
		protected RequestCache(bool isPrivateCache, bool canWrite)
		{
			this._IsPrivateCache = isPrivateCache;
			this._CanWrite = canWrite;
		}

		// Token: 0x170006D1 RID: 1745
		// (get) Token: 0x06001BE8 RID: 7144 RVA: 0x000856E7 File Offset: 0x000838E7
		internal bool IsPrivateCache
		{
			get
			{
				return this._IsPrivateCache;
			}
		}

		// Token: 0x170006D2 RID: 1746
		// (get) Token: 0x06001BE9 RID: 7145 RVA: 0x000856EF File Offset: 0x000838EF
		internal bool CanWrite
		{
			get
			{
				return this._CanWrite;
			}
		}

		// Token: 0x06001BEA RID: 7146
		internal abstract Stream Retrieve(string key, out RequestCacheEntry cacheEntry);

		// Token: 0x06001BEB RID: 7147
		internal abstract Stream Store(string key, long contentLength, DateTime expiresUtc, DateTime lastModifiedUtc, TimeSpan maxStale, StringCollection entryMetadata, StringCollection systemMetadata);

		// Token: 0x06001BEC RID: 7148
		internal abstract void Remove(string key);

		// Token: 0x06001BED RID: 7149
		internal abstract void Update(string key, DateTime expiresUtc, DateTime lastModifiedUtc, DateTime lastSynchronizedUtc, TimeSpan maxStale, StringCollection entryMetadata, StringCollection systemMetadata);

		// Token: 0x06001BEE RID: 7150
		internal abstract bool TryRetrieve(string key, out RequestCacheEntry cacheEntry, out Stream readStream);

		// Token: 0x06001BEF RID: 7151
		internal abstract bool TryStore(string key, long contentLength, DateTime expiresUtc, DateTime lastModifiedUtc, TimeSpan maxStale, StringCollection entryMetadata, StringCollection systemMetadata, out Stream writeStream);

		// Token: 0x06001BF0 RID: 7152
		internal abstract bool TryRemove(string key);

		// Token: 0x06001BF1 RID: 7153
		internal abstract bool TryUpdate(string key, DateTime expiresUtc, DateTime lastModifiedUtc, DateTime lastSynchronizedUtc, TimeSpan maxStale, StringCollection entryMetadata, StringCollection systemMetadata);

		// Token: 0x06001BF2 RID: 7154
		internal abstract void UnlockEntry(Stream retrieveStream);

		// Token: 0x04001B40 RID: 6976
		internal static readonly char[] LineSplits = new char[]
		{
			'\r',
			'\n'
		};

		// Token: 0x04001B41 RID: 6977
		private bool _IsPrivateCache;

		// Token: 0x04001B42 RID: 6978
		private bool _CanWrite;
	}
}
