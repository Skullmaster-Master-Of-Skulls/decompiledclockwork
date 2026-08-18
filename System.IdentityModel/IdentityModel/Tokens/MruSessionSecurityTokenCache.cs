using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IdentityModel.Diagnostics;
using System.Xml;

namespace System.IdentityModel.Tokens
{
	// Token: 0x0200012B RID: 299
	internal class MruSessionSecurityTokenCache : SessionSecurityTokenCache
	{
		// Token: 0x06000858 RID: 2136 RVA: 0x00022C55 File Offset: 0x00020E55
		public MruSessionSecurityTokenCache() : this(20000)
		{
		}

		// Token: 0x06000859 RID: 2137 RVA: 0x00022C62 File Offset: 0x00020E62
		public MruSessionSecurityTokenCache(int maximumSize) : this(maximumSize, null)
		{
		}

		// Token: 0x0600085A RID: 2138 RVA: 0x00022C6C File Offset: 0x00020E6C
		public MruSessionSecurityTokenCache(int maximumSize, IEqualityComparer<SessionSecurityTokenCacheKey> comparer) : this(maximumSize / 5 * 4, maximumSize, comparer)
		{
		}

		// Token: 0x0600085B RID: 2139 RVA: 0x00022C7B File Offset: 0x00020E7B
		public MruSessionSecurityTokenCache(int sizeAfterPurge, int maximumSize) : this(sizeAfterPurge, maximumSize, null)
		{
		}

		// Token: 0x0600085C RID: 2140 RVA: 0x00022C88 File Offset: 0x00020E88
		public MruSessionSecurityTokenCache(int sizeAfterPurge, int maximumSize, IEqualityComparer<SessionSecurityTokenCacheKey> comparer)
		{
			if (sizeAfterPurge < 0)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentException(SR.GetString("ID0008"), "sizeAfterPurge"));
			}
			if (sizeAfterPurge >= maximumSize)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentException(SR.GetString("ID0009"), "sizeAfterPurge"));
			}
			this.items = new Dictionary<SessionSecurityTokenCacheKey, MruSessionSecurityTokenCache.CacheEntry>(maximumSize, comparer);
			this.maximumSize = maximumSize;
			this.mruList = new LinkedList<SessionSecurityTokenCacheKey>();
			this.sizeAfterPurge = sizeAfterPurge;
			this.mruEntry = new MruSessionSecurityTokenCache.CacheEntry();
		}

		// Token: 0x170001F1 RID: 497
		// (get) Token: 0x0600085D RID: 2141 RVA: 0x00022D3D File Offset: 0x00020F3D
		public int MaximumSize
		{
			get
			{
				return this.maximumSize;
			}
		}

		// Token: 0x0600085E RID: 2142 RVA: 0x00022D48 File Offset: 0x00020F48
		public override void Remove(SessionSecurityTokenCacheKey key)
		{
			if (key == null)
			{
				return;
			}
			object obj = this.syncRoot;
			lock (obj)
			{
				MruSessionSecurityTokenCache.CacheEntry cacheEntry;
				if (this.items.TryGetValue(key, out cacheEntry))
				{
					this.items.Remove(key);
					this.mruList.Remove(cacheEntry.Node);
					if (this.mruEntry.Node == cacheEntry.Node)
					{
						this.mruEntry.Value = null;
						this.mruEntry.Node = null;
					}
				}
			}
		}

		// Token: 0x0600085F RID: 2143 RVA: 0x00022DE8 File Offset: 0x00020FE8
		public override void AddOrUpdate(SessionSecurityTokenCacheKey key, SessionSecurityToken value, DateTime expirationTime)
		{
			if (key == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("key");
			}
			object obj = this.syncRoot;
			lock (obj)
			{
				this.Purge();
				this.Remove(key);
				MruSessionSecurityTokenCache.CacheEntry cacheEntry = new MruSessionSecurityTokenCache.CacheEntry();
				cacheEntry.Node = this.mruList.AddFirst(key);
				cacheEntry.Value = value;
				this.items.Add(key, cacheEntry);
				this.mruEntry = cacheEntry;
			}
		}

		// Token: 0x06000860 RID: 2144 RVA: 0x00022E7C File Offset: 0x0002107C
		public override SessionSecurityToken Get(SessionSecurityTokenCacheKey key)
		{
			if (key == null)
			{
				return null;
			}
			SessionSecurityToken result = null;
			object obj = this.syncRoot;
			lock (obj)
			{
				if (this.mruEntry.Node != null && key != null && key.Equals(this.mruEntry.Node.Value))
				{
					return this.mruEntry.Value;
				}
				MruSessionSecurityTokenCache.CacheEntry cacheEntry;
				bool flag2 = this.items.TryGetValue(key, out cacheEntry);
				if (flag2)
				{
					result = cacheEntry.Value;
					if (this.mruList.Count > 1 && this.mruList.First != cacheEntry.Node)
					{
						this.mruList.Remove(cacheEntry.Node);
						this.mruList.AddFirst(cacheEntry.Node);
						this.mruEntry = cacheEntry;
					}
				}
			}
			return result;
		}

		// Token: 0x06000861 RID: 2145 RVA: 0x00022F6C File Offset: 0x0002116C
		public override void RemoveAll(string endpointId, UniqueId contextId)
		{
			if (null == contextId || string.IsNullOrEmpty(endpointId))
			{
				return;
			}
			Dictionary<SessionSecurityTokenCacheKey, MruSessionSecurityTokenCache.CacheEntry> dictionary = new Dictionary<SessionSecurityTokenCacheKey, MruSessionSecurityTokenCache.CacheEntry>();
			SessionSecurityTokenCacheKey sessionSecurityTokenCacheKey = new SessionSecurityTokenCacheKey(endpointId, contextId, null);
			sessionSecurityTokenCacheKey.IgnoreKeyGeneration = true;
			object obj = this.syncRoot;
			lock (obj)
			{
				foreach (SessionSecurityTokenCacheKey sessionSecurityTokenCacheKey2 in this.items.Keys)
				{
					if (sessionSecurityTokenCacheKey2.Equals(sessionSecurityTokenCacheKey))
					{
						dictionary.Add(sessionSecurityTokenCacheKey2, this.items[sessionSecurityTokenCacheKey2]);
					}
				}
				foreach (SessionSecurityTokenCacheKey key in dictionary.Keys)
				{
					this.items.Remove(key);
					MruSessionSecurityTokenCache.CacheEntry cacheEntry = dictionary[key];
					this.mruList.Remove(cacheEntry.Node);
					if (this.mruEntry.Node == cacheEntry.Node)
					{
						this.mruEntry.Value = null;
						this.mruEntry.Node = null;
					}
				}
			}
		}

		// Token: 0x06000862 RID: 2146 RVA: 0x000230C0 File Offset: 0x000212C0
		public override void RemoveAll(string endpointId)
		{
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotImplementedException(SR.GetString("ID4294")));
		}

		// Token: 0x06000863 RID: 2147 RVA: 0x000230DC File Offset: 0x000212DC
		public override IEnumerable<SessionSecurityToken> GetAll(string endpointId, UniqueId contextId)
		{
			Collection<SessionSecurityToken> collection = new Collection<SessionSecurityToken>();
			if (null == contextId || string.IsNullOrEmpty(endpointId))
			{
				return collection;
			}
			SessionSecurityTokenCacheKey sessionSecurityTokenCacheKey = new SessionSecurityTokenCacheKey(endpointId, contextId, null);
			sessionSecurityTokenCacheKey.IgnoreKeyGeneration = true;
			object obj = this.syncRoot;
			lock (obj)
			{
				foreach (SessionSecurityTokenCacheKey sessionSecurityTokenCacheKey2 in this.items.Keys)
				{
					if (sessionSecurityTokenCacheKey2.Equals(sessionSecurityTokenCacheKey))
					{
						MruSessionSecurityTokenCache.CacheEntry cacheEntry = this.items[sessionSecurityTokenCacheKey2];
						if (this.mruList.Count > 1 && this.mruList.First != cacheEntry.Node)
						{
							this.mruList.Remove(cacheEntry.Node);
							this.mruList.AddFirst(cacheEntry.Node);
							this.mruEntry = cacheEntry;
						}
						collection.Add(cacheEntry.Value);
					}
				}
			}
			return collection;
		}

		// Token: 0x06000864 RID: 2148 RVA: 0x000231F0 File Offset: 0x000213F0
		private void Purge()
		{
			if (this.items.Count >= this.maximumSize)
			{
				int num = this.maximumSize - this.sizeAfterPurge;
				for (int i = 0; i < num; i++)
				{
					SessionSecurityTokenCacheKey value = this.mruList.Last.Value;
					this.mruList.RemoveLast();
					this.items.Remove(value);
				}
				if (DiagnosticUtility.ShouldTrace(TraceEventType.Information))
				{
					TraceUtility.TraceString(TraceEventType.Information, SR.GetString("ID8003", new object[]
					{
						this.maximumSize,
						this.sizeAfterPurge
					}), new object[0]);
				}
			}
		}

		// Token: 0x04000B0E RID: 2830
		public const int DefaultTokenCacheSize = 20000;

		// Token: 0x04000B0F RID: 2831
		public static readonly TimeSpan DefaultPurgeInterval = TimeSpan.FromMinutes(15.0);

		// Token: 0x04000B10 RID: 2832
		private DateTime nextPurgeTime = DateTime.UtcNow + MruSessionSecurityTokenCache.DefaultPurgeInterval;

		// Token: 0x04000B11 RID: 2833
		private Dictionary<SessionSecurityTokenCacheKey, MruSessionSecurityTokenCache.CacheEntry> items;

		// Token: 0x04000B12 RID: 2834
		private int maximumSize;

		// Token: 0x04000B13 RID: 2835
		private MruSessionSecurityTokenCache.CacheEntry mruEntry;

		// Token: 0x04000B14 RID: 2836
		private LinkedList<SessionSecurityTokenCacheKey> mruList;

		// Token: 0x04000B15 RID: 2837
		private int sizeAfterPurge;

		// Token: 0x04000B16 RID: 2838
		private object syncRoot = new object();

		// Token: 0x04000B17 RID: 2839
		private object purgeLock = new object();

		// Token: 0x0200025E RID: 606
		public class CacheEntry
		{
			// Token: 0x17000527 RID: 1319
			// (get) Token: 0x06001260 RID: 4704 RVA: 0x0005020C File Offset: 0x0004E40C
			// (set) Token: 0x06001261 RID: 4705 RVA: 0x00050214 File Offset: 0x0004E414
			public SessionSecurityToken Value { get; set; }

			// Token: 0x17000528 RID: 1320
			// (get) Token: 0x06001262 RID: 4706 RVA: 0x0005021D File Offset: 0x0004E41D
			// (set) Token: 0x06001263 RID: 4707 RVA: 0x00050225 File Offset: 0x0004E425
			public LinkedListNode<SessionSecurityTokenCacheKey> Node { get; set; }
		}
	}
}
