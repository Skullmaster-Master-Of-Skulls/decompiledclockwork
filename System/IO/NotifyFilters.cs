using System;

namespace System.IO
{
	// Token: 0x02000726 RID: 1830
	[Flags]
	public enum NotifyFilters
	{
		// Token: 0x040031FD RID: 12797
		FileName = 1,
		// Token: 0x040031FE RID: 12798
		DirectoryName = 2,
		// Token: 0x040031FF RID: 12799
		Attributes = 4,
		// Token: 0x04003200 RID: 12800
		Size = 8,
		// Token: 0x04003201 RID: 12801
		LastWrite = 16,
		// Token: 0x04003202 RID: 12802
		LastAccess = 32,
		// Token: 0x04003203 RID: 12803
		CreationTime = 64,
		// Token: 0x04003204 RID: 12804
		Security = 256
	}
}
