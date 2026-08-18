using System;

namespace System.IO
{
	// Token: 0x020000A1 RID: 161
	internal enum LogRetentionOption
	{
		// Token: 0x04000513 RID: 1299
		SingleFileUnboundedSize = 2,
		// Token: 0x04000514 RID: 1300
		SingleFileBoundedSize = 4,
		// Token: 0x04000515 RID: 1301
		UnlimitedSequentialFiles = 0,
		// Token: 0x04000516 RID: 1302
		LimitedSequentialFiles = 3,
		// Token: 0x04000517 RID: 1303
		LimitedCircularFiles = 1
	}
}
