using System;

namespace System.Windows.Forms
{
	// Token: 0x020001B8 RID: 440
	[Flags]
	public enum DataGridViewCellStyleScopes
	{
		// Token: 0x04000D03 RID: 3331
		None = 0,
		// Token: 0x04000D04 RID: 3332
		Cell = 1,
		// Token: 0x04000D05 RID: 3333
		Column = 2,
		// Token: 0x04000D06 RID: 3334
		Row = 4,
		// Token: 0x04000D07 RID: 3335
		DataGridView = 8,
		// Token: 0x04000D08 RID: 3336
		ColumnHeaders = 16,
		// Token: 0x04000D09 RID: 3337
		RowHeaders = 32,
		// Token: 0x04000D0A RID: 3338
		Rows = 64,
		// Token: 0x04000D0B RID: 3339
		AlternatingRows = 128
	}
}
