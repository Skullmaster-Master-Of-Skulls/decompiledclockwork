using System;

namespace Telerik.Web.UI
{
	// Token: 0x02000DA3 RID: 3491
	[Flags]
	public enum PivotGridDataCellType
	{
		// Token: 0x040023E7 RID: 9191
		DataCell = 0,
		// Token: 0x040023E8 RID: 9192
		RowTotalDataCell = 1,
		// Token: 0x040023E9 RID: 9193
		ColumnTotalDataCell = 2,
		// Token: 0x040023EA RID: 9194
		RowGrandTotalDataCell = 3,
		// Token: 0x040023EB RID: 9195
		ColumnGrandTotalDataCell = 4,
		// Token: 0x040023EC RID: 9196
		RowAndColumnTotal = 5,
		// Token: 0x040023ED RID: 9197
		RowAndColumnGrandTotal = 6,
		// Token: 0x040023EE RID: 9198
		RowGrandTotalColumnTotal = 7,
		// Token: 0x040023EF RID: 9199
		ColumnGrandTotalRowTotal = 8
	}
}
