using System;

namespace System.Net
{
	// Token: 0x020001DB RID: 475
	[Flags]
	internal enum SocketConstructorFlags
	{
		// Token: 0x04001502 RID: 5378
		WSA_FLAG_OVERLAPPED = 1,
		// Token: 0x04001503 RID: 5379
		WSA_FLAG_MULTIPOINT_C_ROOT = 2,
		// Token: 0x04001504 RID: 5380
		WSA_FLAG_MULTIPOINT_C_LEAF = 4,
		// Token: 0x04001505 RID: 5381
		WSA_FLAG_MULTIPOINT_D_ROOT = 8,
		// Token: 0x04001506 RID: 5382
		WSA_FLAG_MULTIPOINT_D_LEAF = 16
	}
}
