using System;

namespace System.IO
{
	// Token: 0x02000407 RID: 1031
	[Flags]
	public enum WatcherChangeTypes
	{
		// Token: 0x040020F2 RID: 8434
		Created = 1,
		// Token: 0x040020F3 RID: 8435
		Deleted = 2,
		// Token: 0x040020F4 RID: 8436
		Changed = 4,
		// Token: 0x040020F5 RID: 8437
		Renamed = 8,
		// Token: 0x040020F6 RID: 8438
		All = 15
	}
}
