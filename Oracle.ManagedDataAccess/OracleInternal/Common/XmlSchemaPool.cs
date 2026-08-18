using System;
using System.Collections.Concurrent;

namespace OracleInternal.Common
{
	// Token: 0x020000AE RID: 174
	internal class XmlSchemaPool
	{
		// Token: 0x060006F5 RID: 1781 RVA: 0x00040668 File Offset: 0x0003E868
		internal XmlSchemaPool(int maxCacheSize)
		{
			this.m_sync = new object();
			this.m_maxCacheSize = maxCacheSize;
			this.m_schemaIdCache = new ConcurrentDictionary<byte[], CachedSchemaWithUrl>();
			this.m_schemaUrlCache = new ConcurrentDictionary<string, CachedSchemaWithId>();
		}

		// Token: 0x060006F6 RID: 1782 RVA: 0x00040698 File Offset: 0x0003E898
		internal bool Contains(string url)
		{
			return this.m_schemaUrlCache.ContainsKey(url);
		}

		// Token: 0x060006F7 RID: 1783 RVA: 0x000406AC File Offset: 0x0003E8AC
		internal bool Contains(byte[] id)
		{
			return this.m_schemaIdCache.ContainsKey(id);
		}

		// Token: 0x170001B3 RID: 435
		internal CachedSchemaWithId this[string url]
		{
			get
			{
				CachedSchemaWithId result;
				try
				{
					if (url == null)
					{
						result = null;
					}
					else
					{
						url = url.Trim();
						if (url.Length == 0)
						{
							result = null;
						}
						else
						{
							CachedSchemaWithId cachedSchemaWithId = null;
							if (this.m_schemaUrlCache.TryGetValue(url, out cachedSchemaWithId))
							{
								result = cachedSchemaWithId;
							}
							else
							{
								result = null;
							}
						}
					}
				}
				catch
				{
					result = null;
				}
				return result;
			}
			set
			{
				if (url == null)
				{
					return;
				}
				url = url.Trim();
				if (url.Length == 0)
				{
					return;
				}
				if (this.m_schemaUrlCache.Count < this.m_maxCacheSize)
				{
					lock (this.m_sync)
					{
						if (this.m_schemaUrlCache.Count < this.m_maxCacheSize && !this.m_schemaUrlCache.TryAdd(url, value))
						{
							this.m_schemaUrlCache.TryUpdate(url, value, null);
						}
					}
				}
			}
		}

		// Token: 0x170001B4 RID: 436
		internal CachedSchemaWithUrl this[byte[] id]
		{
			get
			{
				CachedSchemaWithUrl result;
				try
				{
					if (id == null)
					{
						result = null;
					}
					else
					{
						CachedSchemaWithUrl cachedSchemaWithUrl = null;
						if (this.m_schemaIdCache.TryGetValue(id, out cachedSchemaWithUrl))
						{
							result = cachedSchemaWithUrl;
						}
						else
						{
							result = null;
						}
					}
				}
				catch
				{
					result = null;
				}
				return result;
			}
			set
			{
				if (id == null)
				{
					return;
				}
				if (this.m_schemaIdCache.Count < this.m_maxCacheSize)
				{
					lock (this.m_sync)
					{
						if (this.m_schemaIdCache.Count < this.m_maxCacheSize && !this.m_schemaIdCache.TryAdd(id, value))
						{
							this.m_schemaIdCache.TryUpdate(id, value, null);
						}
					}
				}
			}
		}

		// Token: 0x04000951 RID: 2385
		private object m_sync;

		// Token: 0x04000952 RID: 2386
		private ConcurrentDictionary<string, CachedSchemaWithId> m_schemaUrlCache;

		// Token: 0x04000953 RID: 2387
		private ConcurrentDictionary<byte[], CachedSchemaWithUrl> m_schemaIdCache;

		// Token: 0x04000954 RID: 2388
		private int m_maxCacheSize;
	}
}
