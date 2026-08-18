using System;
using Spire.Xls.Core.Spreadsheet.PivotTables;

namespace Spire.Xls.Core
{
	// Token: 0x02000229 RID: 553
	public interface IPivotTable
	{
		// Token: 0x17000C17 RID: 3095
		// (get) Token: 0x06002196 RID: 8598
		// (set) Token: 0x06002197 RID: 8599
		string Name { get; set; }

		// Token: 0x17000C18 RID: 3096
		// (get) Token: 0x06002198 RID: 8600
		PivotTableFields PivotFields { get; }

		// Token: 0x17000C19 RID: 3097
		// (get) Token: 0x06002199 RID: 8601
		PivotDataFields DataFields { get; }

		// Token: 0x17000C1A RID: 3098
		// (get) Token: 0x0600219A RID: 8602
		// (set) Token: 0x0600219B RID: 8603
		bool IsRowGrand { get; set; }

		// Token: 0x17000C1B RID: 3099
		// (get) Token: 0x0600219C RID: 8604
		// (set) Token: 0x0600219D RID: 8605
		bool IsColumnGrand { get; set; }

		// Token: 0x17000C1C RID: 3100
		// (get) Token: 0x0600219E RID: 8606
		// (set) Token: 0x0600219F RID: 8607
		bool ShowDrillIndicators { get; set; }

		// Token: 0x17000C1D RID: 3101
		// (get) Token: 0x060021A0 RID: 8608
		// (set) Token: 0x060021A1 RID: 8609
		bool DisplayFieldCaptions { get; set; }

		// Token: 0x17000C1E RID: 3102
		// (get) Token: 0x060021A2 RID: 8610
		// (set) Token: 0x060021A3 RID: 8611
		bool RepeatItemsOnEachPrintedPage { get; set; }

		// Token: 0x17000C1F RID: 3103
		// (get) Token: 0x060021A4 RID: 8612
		// (set) Token: 0x060021A5 RID: 8613
		PivotBuiltInStyles? BuiltInStyle { get; set; }

		// Token: 0x17000C20 RID: 3104
		// (get) Token: 0x060021A6 RID: 8614
		// (set) Token: 0x060021A7 RID: 8615
		bool ShowRowGrand { get; set; }

		// Token: 0x17000C21 RID: 3105
		// (get) Token: 0x060021A8 RID: 8616
		// (set) Token: 0x060021A9 RID: 8617
		bool ShowColumnGrand { get; set; }

		// Token: 0x17000C22 RID: 3106
		// (get) Token: 0x060021AA RID: 8618
		int CacheIndex { get; }

		// Token: 0x17000C23 RID: 3107
		// (get) Token: 0x060021AB RID: 8619
		// (set) Token: 0x060021AC RID: 8620
		CellRange Location { get; set; }

		// Token: 0x17000C24 RID: 3108
		// (get) Token: 0x060021AD RID: 8621
		IPivotTableOptions Options { get; }

		// Token: 0x17000C25 RID: 3109
		// (get) Token: 0x060021AE RID: 8622
		int RowsPerPage { get; }

		// Token: 0x17000C26 RID: 3110
		// (get) Token: 0x060021AF RID: 8623
		int ColumnsPerPage { get; }

		// Token: 0x17000C27 RID: 3111
		// (get) Token: 0x060021B0 RID: 8624
		IPivotCalculatedFields CalculatedFields { get; }

		// Token: 0x17000C28 RID: 3112
		// (get) Token: 0x060021B1 RID: 8625
		IPivotFields PageFields { get; }

		// Token: 0x17000C29 RID: 3113
		// (get) Token: 0x060021B2 RID: 8626
		IPivotFields RowFields { get; }

		// Token: 0x17000C2A RID: 3114
		// (get) Token: 0x060021B3 RID: 8627
		IPivotFields ColumnFields { get; }

		// Token: 0x17000C2B RID: 3115
		// (get) Token: 0x060021B4 RID: 8628
		// (set) Token: 0x060021B5 RID: 8629
		bool ShowDataFieldInRow { get; set; }

		// Token: 0x060021B6 RID: 8630
		void Clear();
	}
}
