using System;

namespace System.IdentityModel.Tokens
{
	// Token: 0x02000117 RID: 279
	internal class DefaultTokenReplayCache : TokenReplayCache
	{
		// Token: 0x0600079A RID: 1946 RVA: 0x0002035A File Offset: 0x0001E55A
		public DefaultTokenReplayCache() : this(DefaultTokenReplayCache.DefaultTokenReplayCacheCapacity, DefaultTokenReplayCache.DefaultTokenReplayCachePurgeInterval)
		{
		}

		// Token: 0x0600079B RID: 1947 RVA: 0x0002036C File Offset: 0x0001E56C
		public DefaultTokenReplayCache(int capacity, TimeSpan purgeInterval)
		{
			this._internalCache = new BoundedCache<SecurityToken>(capacity, purgeInterval, StringComparer.Ordinal);
		}

		// Token: 0x170001BD RID: 445
		// (get) Token: 0x0600079C RID: 1948 RVA: 0x00020386 File Offset: 0x0001E586
		// (set) Token: 0x0600079D RID: 1949 RVA: 0x00020393 File Offset: 0x0001E593
		public int Capacity
		{
			get
			{
				return this._internalCache.Capacity;
			}
			set
			{
				if (value <= 0)
				{
					throw DiagnosticUtility.ThrowHelperArgumentOutOfRange("value", value, SR.GetString("ID0002"));
				}
				this._internalCache.Capacity = value;
			}
		}

		// Token: 0x0600079E RID: 1950 RVA: 0x000203C0 File Offset: 0x0001E5C0
		public void Clear()
		{
			this._internalCache.Clear();
		}

		// Token: 0x0600079F RID: 1951 RVA: 0x000203CD File Offset: 0x0001E5CD
		public int IncreaseCapacity(int size)
		{
			if (size <= 0)
			{
				throw DiagnosticUtility.ThrowHelperArgumentOutOfRange("size", size, SR.GetString("ID0002"));
			}
			return this._internalCache.IncreaseCapacity(size);
		}

		// Token: 0x170001BE RID: 446
		// (get) Token: 0x060007A0 RID: 1952 RVA: 0x000203FA File Offset: 0x0001E5FA
		// (set) Token: 0x060007A1 RID: 1953 RVA: 0x00020407 File Offset: 0x0001E607
		public TimeSpan PurgeInterval
		{
			get
			{
				return this._internalCache.PurgeInterval;
			}
			set
			{
				if (value <= TimeSpan.Zero)
				{
					throw DiagnosticUtility.ThrowHelperArgumentOutOfRange("value", value, SR.GetString("ID0016"));
				}
				this._internalCache.PurgeInterval = value;
			}
		}

		// Token: 0x060007A2 RID: 1954 RVA: 0x0002043D File Offset: 0x0001E63D
		public override void AddOrUpdate(string key, SecurityToken securityToken, DateTime expirationTime)
		{
			if (DateTime.Equals(expirationTime, DateTime.MaxValue))
			{
				throw DiagnosticUtility.ThrowHelperInvalidOperation(SR.GetString("ID1072"));
			}
			this._internalCache.TryRemove(key);
			this._internalCache.TryAdd(key, securityToken, expirationTime);
		}

		// Token: 0x060007A3 RID: 1955 RVA: 0x00020478 File Offset: 0x0001E678
		public override bool Contains(string key)
		{
			return this._internalCache.TryFind(key);
		}

		// Token: 0x060007A4 RID: 1956 RVA: 0x00020488 File Offset: 0x0001E688
		public override SecurityToken Get(string key)
		{
			SecurityToken result;
			this._internalCache.TryGet(key, out result);
			return result;
		}

		// Token: 0x060007A5 RID: 1957 RVA: 0x000204A5 File Offset: 0x0001E6A5
		public override void Remove(string key)
		{
			this._internalCache.TryRemove(key);
		}

		// Token: 0x04000AD0 RID: 2768
		private static readonly int DefaultTokenReplayCacheCapacity = 500000;

		// Token: 0x04000AD1 RID: 2769
		private static readonly TimeSpan DefaultTokenReplayCachePurgeInterval = TimeSpan.FromMinutes(1.0);

		// Token: 0x04000AD2 RID: 2770
		private BoundedCache<SecurityToken> _internalCache;
	}
}
