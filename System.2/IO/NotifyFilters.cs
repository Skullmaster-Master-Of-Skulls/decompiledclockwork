using System;

namespace System.IO
{
	// Token: 0x020003FA RID: 1018
	[Flags]
	public enum NotifyFilters
	{
		// Token: 0x040020B3 RID: 8371
		FileName = 1,
		// Token: 0x040020B4 RID: 8372
		DirectoryName = 2,
		// Token: 0x040020B5 RID: 8373
		Attributes = 4,
		// Token: 0x040020B6 RID: 8374
		Size = 8,
		// Token: 0x040020B7 RID: 8375
		LastWrite = 16,
		// Token: 0x040020B8 RID: 8376
		LastAccess = 32,
		// Token: 0x040020B9 RID: 8377
		CreationTime = 64,
		// Token: 0x040020BA RID: 8378
		Security = 256
	}
}
