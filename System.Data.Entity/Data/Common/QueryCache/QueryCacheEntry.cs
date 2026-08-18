using System;

namespace System.Data.Common.QueryCache
{
	// Token: 0x020003E0 RID: 992
	internal class QueryCacheEntry
	{
		// Token: 0x06003545 RID: 13637 RVA: 0x000CF649 File Offset: 0x000CD849
		internal QueryCacheEntry(QueryCacheKey queryCacheKey, object target)
		{
			this._queryCacheKey = queryCacheKey;
			this._target = target;
		}

		// Token: 0x06003546 RID: 13638 RVA: 0x000CF65F File Offset: 0x000CD85F
		internal virtual object GetTarget()
		{
			return this._target;
		}

		// Token: 0x17000A37 RID: 2615
		// (get) Token: 0x06003547 RID: 13639 RVA: 0x000CF667 File Offset: 0x000CD867
		internal QueryCacheKey QueryCacheKey
		{
			get
			{
				return this._queryCacheKey;
			}
		}

		// Token: 0x04001796 RID: 6038
		private readonly QueryCacheKey _queryCacheKey;

		// Token: 0x04001797 RID: 6039
		protected readonly object _target;
	}
}
