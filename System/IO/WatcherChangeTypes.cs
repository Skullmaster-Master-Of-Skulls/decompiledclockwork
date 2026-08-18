using System;

namespace System.IO
{
	// Token: 0x02000734 RID: 1844
	[Flags]
	public enum WatcherChangeTypes
	{
		// Token: 0x0400323D RID: 12861
		Created = 1,
		// Token: 0x0400323E RID: 12862
		Deleted = 2,
		// Token: 0x0400323F RID: 12863
		Changed = 4,
		// Token: 0x04003240 RID: 12864
		Renamed = 8,
		// Token: 0x04003241 RID: 12865
		All = 15
	}
}
