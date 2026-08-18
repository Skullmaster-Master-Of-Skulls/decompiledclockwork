using System;
using System.Data.Entity.Core.Metadata.Edm;

namespace System.Data.Entity.Infrastructure.MappingViews
{
	// Token: 0x02000191 RID: 401
	public abstract class DbMappingViewCache
	{
		// Token: 0x17000121 RID: 289
		// (get) Token: 0x06000D88 RID: 3464
		public abstract string MappingHashValue { get; }

		// Token: 0x06000D89 RID: 3465
		public abstract DbMappingView GetView(EntitySetBase extent);
	}
}
