using System;

namespace System.Data.Entity.Infrastructure
{
	// Token: 0x020002A3 RID: 675
	public interface IDbModelCacheKey
	{
		// Token: 0x060017EF RID: 6127
		bool Equals(object other);

		// Token: 0x060017F0 RID: 6128
		int GetHashCode();
	}
}
