using System;

namespace Spire.Xls
{
	// Token: 0x0200009C RID: 156
	[Flags]
	public enum WorksheetCopyType
	{
		// Token: 0x040006AB RID: 1707
		None = 0,
		// Token: 0x040006AC RID: 1708
		ClearBefore = 1,
		// Token: 0x040006AD RID: 1709
		CopyNames = 2,
		// Token: 0x040006AE RID: 1710
		CopyCells = 4,
		// Token: 0x040006AF RID: 1711
		CopyRowHeight = 8,
		// Token: 0x040006B0 RID: 1712
		CopyColumnHeight = 16,
		// Token: 0x040006B1 RID: 1713
		CopyOptions = 32,
		// Token: 0x040006B2 RID: 1714
		CopyMerges = 64,
		// Token: 0x040006B3 RID: 1715
		CopyShapes = 128,
		// Token: 0x040006B4 RID: 1716
		CopyConditionlFormats = 256,
		// Token: 0x040006B5 RID: 1717
		CopyAutoFilters = 512,
		// Token: 0x040006B6 RID: 1718
		CopyDataValidations = 1024,
		// Token: 0x040006B7 RID: 1719
		CopyPageSetup = 2048,
		// Token: 0x040006B8 RID: 1720
		CopyTables = 2560,
		// Token: 0x040006B9 RID: 1721
		CopyPivotTables = 4096,
		// Token: 0x040006BA RID: 1722
		CopyPalette = 8192,
		// Token: 0x040006BB RID: 1723
		CopyAll = 16383,
		// Token: 0x040006BC RID: 1724
		CopyWithoutNames = 8189
	}
}
