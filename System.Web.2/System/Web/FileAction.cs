using System;

namespace System.Web
{
	// Token: 0x0200006C RID: 108
	internal enum FileAction
	{
		// Token: 0x040001F1 RID: 497
		Dispose = -2,
		// Token: 0x040001F2 RID: 498
		Error,
		// Token: 0x040001F3 RID: 499
		Overwhelming,
		// Token: 0x040001F4 RID: 500
		Added,
		// Token: 0x040001F5 RID: 501
		Removed,
		// Token: 0x040001F6 RID: 502
		Modified,
		// Token: 0x040001F7 RID: 503
		RenamedOldName,
		// Token: 0x040001F8 RID: 504
		RenamedNewName
	}
}
