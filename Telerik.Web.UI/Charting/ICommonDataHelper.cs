using System;

namespace Telerik.Charting
{
	// Token: 0x020016EB RID: 5867
	internal interface ICommonDataHelper
	{
		// Token: 0x1700458A RID: 17802
		// (get) Token: 0x0600E3BF RID: 58303
		int RowsCount { get; }

		// Token: 0x1700458B RID: 17803
		// (get) Token: 0x0600E3C0 RID: 58304
		int ColumnsCount { get; }

		// Token: 0x0600E3C1 RID: 58305
		int GetColumnIndex(string columnName);

		// Token: 0x0600E3C2 RID: 58306
		string GetColumnName(int columnIndex);

		// Token: 0x1700458C RID: 17804
		// (get) Token: 0x0600E3C3 RID: 58307
		bool ColumnNameSupported { get; }

		// Token: 0x0600E3C4 RID: 58308
		double GetDoubleValue(int rowIndex, int columnIndex);

		// Token: 0x0600E3C5 RID: 58309
		object GetObjectValue(int rowIndex, int columnIndex);

		// Token: 0x0600E3C6 RID: 58310
		string GetStringValue(int rowIndex, int columnIndex);

		// Token: 0x0600E3C7 RID: 58311
		object[] GetFilteredColumn(int columnIndex);

		// Token: 0x0600E3C8 RID: 58312
		object[] GetSortedAndFilteredColumn(int columnIndex);

		// Token: 0x0600E3C9 RID: 58313
		bool IsColumnNumeric(int columnIndex);

		// Token: 0x0600E3CA RID: 58314
		bool IsColumnString(int columnIndex);

		// Token: 0x0600E3CB RID: 58315
		bool IsItemNumeric(int rowIndex, int columnIndex);

		// Token: 0x0600E3CC RID: 58316
		int GetGroupsColumnIndex();

		// Token: 0x0600E3CD RID: 58317
		int GetLabelsColumnIndex(int groupColumn);

		// Token: 0x0600E3CE RID: 58318
		int GetValuesXColumnIndex();

		// Token: 0x0600E3CF RID: 58319
		int GetValuesYColumnIndex();

		// Token: 0x0600E3D0 RID: 58320
		int[] GetValuesYColumns();

		// Token: 0x0600E3D1 RID: 58321
		int[] GetGanttValuesColumns();
	}
}
