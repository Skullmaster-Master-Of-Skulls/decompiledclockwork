using System;
using System.Data.Entity.Core.Mapping;

namespace System.Data.Entity.Infrastructure.MappingViews
{
	// Token: 0x02000192 RID: 402
	public abstract class DbMappingViewCacheFactory
	{
		// Token: 0x06000D8B RID: 3467
		public abstract DbMappingViewCache Create(string conceptualModelContainerName, string storeModelContainerName);

		// Token: 0x06000D8C RID: 3468 RVA: 0x0003D0A3 File Offset: 0x0003B2A3
		internal DbMappingViewCache Create(EntityContainerMapping mapping)
		{
			return this.Create(mapping.EdmEntityContainer.Name, mapping.StorageEntityContainer.Name);
		}
	}
}
