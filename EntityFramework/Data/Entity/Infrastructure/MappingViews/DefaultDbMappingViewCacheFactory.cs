using System;

namespace System.Data.Entity.Infrastructure.MappingViews
{
	// Token: 0x02000194 RID: 404
	internal class DefaultDbMappingViewCacheFactory : DbMappingViewCacheFactory
	{
		// Token: 0x06000D92 RID: 3474 RVA: 0x0003D20C File Offset: 0x0003B40C
		public DefaultDbMappingViewCacheFactory(Type cacheType)
		{
			this._cacheType = cacheType;
		}

		// Token: 0x06000D93 RID: 3475 RVA: 0x0003D21B File Offset: 0x0003B41B
		public override DbMappingViewCache Create(string conceptualModelContainerName, string storeModelContainerName)
		{
			return (DbMappingViewCache)Activator.CreateInstance(this._cacheType);
		}

		// Token: 0x06000D94 RID: 3476 RVA: 0x0003D22D File Offset: 0x0003B42D
		public override int GetHashCode()
		{
			return this._cacheType.GetHashCode() * 397 ^ typeof(DefaultDbMappingViewCacheFactory).GetHashCode();
		}

		// Token: 0x06000D95 RID: 3477 RVA: 0x0003D250 File Offset: 0x0003B450
		public override bool Equals(object obj)
		{
			DefaultDbMappingViewCacheFactory defaultDbMappingViewCacheFactory = obj as DefaultDbMappingViewCacheFactory;
			return defaultDbMappingViewCacheFactory != null && object.ReferenceEquals(defaultDbMappingViewCacheFactory._cacheType, this._cacheType);
		}

		// Token: 0x040003B1 RID: 945
		private readonly Type _cacheType;
	}
}
