using System;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.Internal
{
	// Token: 0x020002A5 RID: 677
	internal sealed class DefaultModelCacheKeyFactory
	{
		// Token: 0x060017F5 RID: 6133 RVA: 0x00079048 File Offset: 0x00077248
		public IDbModelCacheKey Create(DbContext context)
		{
			Check.NotNull<DbContext>(context, "context");
			string customKey = null;
			IDbModelCacheKeyProvider dbModelCacheKeyProvider = context as IDbModelCacheKeyProvider;
			if (dbModelCacheKeyProvider != null)
			{
				customKey = dbModelCacheKeyProvider.CacheKey;
			}
			return new DefaultModelCacheKey(context.GetType(), context.InternalContext.ProviderName, context.InternalContext.ProviderFactory.GetType(), customKey);
		}
	}
}
