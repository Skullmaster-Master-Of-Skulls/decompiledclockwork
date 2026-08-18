using System;

namespace Spire.Xls
{
	// Token: 0x020000D2 RID: 210
	[Flags]
	public enum SheetProtectionType
	{
		// Token: 0x04000868 RID: 2152
		None = 0,
		// Token: 0x04000869 RID: 2153
		Objects = 1,
		// Token: 0x0400086A RID: 2154
		Scenarios = 2,
		// Token: 0x0400086B RID: 2155
		FormattingCells = 4,
		// Token: 0x0400086C RID: 2156
		FormattingColumns = 8,
		// Token: 0x0400086D RID: 2157
		FormattingRows = 16,
		// Token: 0x0400086E RID: 2158
		InsertingColumns = 32,
		// Token: 0x0400086F RID: 2159
		InsertingRows = 64,
		// Token: 0x04000870 RID: 2160
		InsertingHyperlinks = 128,
		// Token: 0x04000871 RID: 2161
		DeletingColumns = 256,
		// Token: 0x04000872 RID: 2162
		DeletingRows = 512,
		// Token: 0x04000873 RID: 2163
		LockedCells = 1024,
		// Token: 0x04000874 RID: 2164
		Sorting = 2048,
		// Token: 0x04000875 RID: 2165
		Filtering = 4096,
		// Token: 0x04000876 RID: 2166
		UsingPivotTables = 8192,
		// Token: 0x04000877 RID: 2167
		UnLockedCells = 16384,
		// Token: 0x04000878 RID: 2168
		Content = 32768,
		// Token: 0x04000879 RID: 2169
		All = 65535
	}
}
