using System;

namespace System.Web
{
	// Token: 0x02000109 RID: 265
	[Flags]
	internal enum VirtualPathOptions
	{
		// Token: 0x04000641 RID: 1601
		AllowNull = 1,
		// Token: 0x04000642 RID: 1602
		EnsureTrailingSlash = 2,
		// Token: 0x04000643 RID: 1603
		AllowAbsolutePath = 4,
		// Token: 0x04000644 RID: 1604
		AllowAppRelativePath = 8,
		// Token: 0x04000645 RID: 1605
		AllowRelativePath = 16,
		// Token: 0x04000646 RID: 1606
		FailIfMalformed = 32,
		// Token: 0x04000647 RID: 1607
		AllowAllPath = 28
	}
}
