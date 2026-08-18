using System;

namespace System.Windows.Forms
{
	// Token: 0x02000129 RID: 297
	public enum AutoCompleteSource
	{
		// Token: 0x04000615 RID: 1557
		FileSystem = 1,
		// Token: 0x04000616 RID: 1558
		HistoryList,
		// Token: 0x04000617 RID: 1559
		RecentlyUsedList = 4,
		// Token: 0x04000618 RID: 1560
		AllUrl = 6,
		// Token: 0x04000619 RID: 1561
		AllSystemSources,
		// Token: 0x0400061A RID: 1562
		FileSystemDirectories = 32,
		// Token: 0x0400061B RID: 1563
		CustomSource = 64,
		// Token: 0x0400061C RID: 1564
		None = 128,
		// Token: 0x0400061D RID: 1565
		ListItems = 256
	}
}
