using System;

namespace System.Data.Entity.Core.Common.QueryCache
{
	// Token: 0x020002F7 RID: 759
	internal sealed class CompiledQueryCacheKey : QueryCacheKey
	{
		// Token: 0x06001AC4 RID: 6852 RVA: 0x000857B5 File Offset: 0x000839B5
		internal CompiledQueryCacheKey(Guid cacheIdentity)
		{
			this._cacheIdentity = cacheIdentity;
		}

		// Token: 0x06001AC5 RID: 6853 RVA: 0x000857C4 File Offset: 0x000839C4
		public override bool Equals(object compareTo)
		{
			return !(typeof(CompiledQueryCacheKey) != compareTo.GetType()) && ((CompiledQueryCacheKey)compareTo)._cacheIdentity.Equals(this._cacheIdentity);
		}

		// Token: 0x06001AC6 RID: 6854 RVA: 0x00085804 File Offset: 0x00083A04
		public override int GetHashCode()
		{
			return this._cacheIdentity.GetHashCode();
		}

		// Token: 0x06001AC7 RID: 6855 RVA: 0x00085828 File Offset: 0x00083A28
		public override string ToString()
		{
			return this._cacheIdentity.ToString();
		}

		// Token: 0x04000948 RID: 2376
		private readonly Guid _cacheIdentity;
	}
}
