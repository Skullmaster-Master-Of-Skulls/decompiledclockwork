using System;

namespace System.Data.Entity.Core.Common.QueryCache
{
	// Token: 0x020002F4 RID: 756
	internal class QueryCacheEntry
	{
		// Token: 0x06001AB1 RID: 6833 RVA: 0x000855F8 File Offset: 0x000837F8
		internal QueryCacheEntry(QueryCacheKey queryCacheKey, object target)
		{
			this._queryCacheKey = queryCacheKey;
			this._target = target;
		}

		// Token: 0x06001AB2 RID: 6834 RVA: 0x0008560E File Offset: 0x0008380E
		internal virtual object GetTarget()
		{
			return this._target;
		}

		// Token: 0x170002F7 RID: 759
		// (get) Token: 0x06001AB3 RID: 6835 RVA: 0x00085616 File Offset: 0x00083816
		internal QueryCacheKey QueryCacheKey
		{
			get
			{
				return this._queryCacheKey;
			}
		}

		// Token: 0x04000940 RID: 2368
		private readonly QueryCacheKey _queryCacheKey;

		// Token: 0x04000941 RID: 2369
		protected readonly object _target;
	}
}
