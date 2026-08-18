using System;
using System.Data.Entity.Internal.ConfigFile;

namespace System.Data.Entity.Internal
{
	// Token: 0x020001A2 RID: 418
	internal class QueryCacheConfig
	{
		// Token: 0x06000E2A RID: 3626 RVA: 0x0003E899 File Offset: 0x0003CA99
		public QueryCacheConfig(EntityFrameworkSection entityFrameworkSection)
		{
			this._entityFrameworkSection = entityFrameworkSection;
		}

		// Token: 0x06000E2B RID: 3627 RVA: 0x0003E8A8 File Offset: 0x0003CAA8
		public int GetQueryCacheSize()
		{
			int size = this._entityFrameworkSection.QueryCache.Size;
			if (size == 0)
			{
				return 1000;
			}
			return size;
		}

		// Token: 0x06000E2C RID: 3628 RVA: 0x0003E8D0 File Offset: 0x0003CAD0
		public int GetCleaningIntervalInSeconds()
		{
			int cleaningIntervalInSeconds = this._entityFrameworkSection.QueryCache.CleaningIntervalInSeconds;
			if (cleaningIntervalInSeconds == 0)
			{
				return 60;
			}
			return cleaningIntervalInSeconds;
		}

		// Token: 0x040003CE RID: 974
		private const int DefaultSize = 1000;

		// Token: 0x040003CF RID: 975
		private const int DefaultCleaningIntervalInSeconds = 60;

		// Token: 0x040003D0 RID: 976
		private readonly EntityFrameworkSection _entityFrameworkSection;
	}
}
