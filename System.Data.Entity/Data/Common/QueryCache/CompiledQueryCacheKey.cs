using System;

namespace System.Data.Common.QueryCache
{
	// Token: 0x020003DB RID: 987
	internal sealed class CompiledQueryCacheKey : QueryCacheKey
	{
		// Token: 0x06003526 RID: 13606 RVA: 0x000CEF9D File Offset: 0x000CD19D
		internal CompiledQueryCacheKey(Guid cacheIdentity)
		{
			this._cacheIdentity = cacheIdentity;
		}

		// Token: 0x06003527 RID: 13607 RVA: 0x000CEFAC File Offset: 0x000CD1AC
		public override bool Equals(object compareTo)
		{
			return !(typeof(CompiledQueryCacheKey) != compareTo.GetType()) && ((CompiledQueryCacheKey)compareTo)._cacheIdentity.Equals(this._cacheIdentity);
		}

		// Token: 0x06003528 RID: 13608 RVA: 0x000CEFEC File Offset: 0x000CD1EC
		public override int GetHashCode()
		{
			return this._cacheIdentity.GetHashCode();
		}

		// Token: 0x06003529 RID: 13609 RVA: 0x000CF010 File Offset: 0x000CD210
		public override string ToString()
		{
			return this._cacheIdentity.ToString();
		}

		// Token: 0x0400177D RID: 6013
		private readonly Guid _cacheIdentity;
	}
}
