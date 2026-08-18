using System;

namespace System.Data.Entity.Internal
{
	// Token: 0x020001A1 RID: 417
	internal enum DatabaseExistenceState
	{
		// Token: 0x040003CA RID: 970
		Unknown,
		// Token: 0x040003CB RID: 971
		DoesNotExist,
		// Token: 0x040003CC RID: 972
		ExistsConsideredEmpty,
		// Token: 0x040003CD RID: 973
		Exists
	}
}
