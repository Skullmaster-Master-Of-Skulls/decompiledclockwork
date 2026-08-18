using System;

namespace System.Net
{
	// Token: 0x02000504 RID: 1284
	[Flags]
	internal enum SocketConstructorFlags
	{
		// Token: 0x04002743 RID: 10051
		WSA_FLAG_OVERLAPPED = 1,
		// Token: 0x04002744 RID: 10052
		WSA_FLAG_MULTIPOINT_C_ROOT = 2,
		// Token: 0x04002745 RID: 10053
		WSA_FLAG_MULTIPOINT_C_LEAF = 4,
		// Token: 0x04002746 RID: 10054
		WSA_FLAG_MULTIPOINT_D_ROOT = 8,
		// Token: 0x04002747 RID: 10055
		WSA_FLAG_MULTIPOINT_D_LEAF = 16
	}
}
