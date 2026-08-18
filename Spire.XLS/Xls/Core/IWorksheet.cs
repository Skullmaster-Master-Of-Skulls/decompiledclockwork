using System;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using Spire.Xls.Calculation;
using Spire.Xls.Collections;
using Spire.Xls.Core.Spreadsheet;

namespace Spire.Xls.Core
{
	// Token: 0x020001E4 RID: 484
	public interface IWorksheet : ITabSheet, IFormulaEngine
	{
		// Token: 0x17000A2E RID: 2606
		// (get) Token: 0x06001B1A RID: 6938
		IAutoFilters AutoFilters { get; }

		// Token: 0x17000A2F RID: 2607
		// (get) Token: 0x06001B1B RID: 6939
		IXLSRange[] Cells { get; }

		// Token: 0x17000A30 RID: 2608
		// (get) Token: 0x06001B1C RID: 6940
		// (set) Token: 0x06001B1D RID: 6941
		bool DisplayPageBreaks { get; set; }

		// Token: 0x17000A31 RID: 2609
		// (get) Token: 0x06001B1E RID: 6942
		int Index { get; }

		// Token: 0x17000A32 RID: 2610
		// (get) Token: 0x06001B1F RID: 6943
		IXLSRange[] MergedCells { get; }

		// Token: 0x17000A33 RID: 2611
		// (get) Token: 0x06001B20 RID: 6944
		INameRanges Names { get; }

		// Token: 0x17000A34 RID: 2612
		// (get) Token: 0x06001B21 RID: 6945
		string CodeName { get; }

		// Token: 0x17000A35 RID: 2613
		// (get) Token: 0x06001B22 RID: 6946
		IPageSetup PageSetup { get; }

		// Token: 0x17000A36 RID: 2614
		// (get) Token: 0x06001B23 RID: 6947
		IXLSRange AllocatedRange { get; }

		// Token: 0x17000A37 RID: 2615
		// (get) Token: 0x06001B24 RID: 6948
		IXLSRange[] Rows { get; }

		// Token: 0x17000A38 RID: 2616
		// (get) Token: 0x06001B25 RID: 6949
		IXLSRange[] Columns { get; }

		// Token: 0x17000A39 RID: 2617
		// (get) Token: 0x06001B26 RID: 6950
		// (set) Token: 0x06001B27 RID: 6951
		double DefaultRowHeight { get; set; }

		// Token: 0x17000A3A RID: 2618
		// (get) Token: 0x06001B28 RID: 6952
		// (set) Token: 0x06001B29 RID: 6953
		double DefaultColumnWidth { get; set; }

		// Token: 0x17000A3B RID: 2619
		// (get) Token: 0x06001B2A RID: 6954
		ExcelSheetType Type { get; }

		// Token: 0x17000A3C RID: 2620
		// (get) Token: 0x06001B2B RID: 6955
		XlsRange Range { get; }

		// Token: 0x17000A3D RID: 2621
		// (get) Token: 0x06001B2C RID: 6956
		// (set) Token: 0x06001B2D RID: 6957
		int Zoom { get; set; }

		// Token: 0x17000A3E RID: 2622
		// (get) Token: 0x06001B2E RID: 6958
		// (set) Token: 0x06001B2F RID: 6959
		int VerticalSplit { get; set; }

		// Token: 0x17000A3F RID: 2623
		// (get) Token: 0x06001B30 RID: 6960
		// (set) Token: 0x06001B31 RID: 6961
		int HorizontalSplit { get; set; }

		// Token: 0x17000A40 RID: 2624
		// (get) Token: 0x06001B32 RID: 6962
		// (set) Token: 0x06001B33 RID: 6963
		int FirstVisibleRow { get; set; }

		// Token: 0x17000A41 RID: 2625
		// (get) Token: 0x06001B34 RID: 6964
		// (set) Token: 0x06001B35 RID: 6965
		int FirstVisibleColumn { get; set; }

		// Token: 0x17000A42 RID: 2626
		// (get) Token: 0x06001B36 RID: 6966
		// (set) Token: 0x06001B37 RID: 6967
		int ActivePane { get; set; }

		// Token: 0x17000A43 RID: 2627
		// (get) Token: 0x06001B38 RID: 6968
		// (set) Token: 0x06001B39 RID: 6969
		bool IsDisplayZeros { get; set; }

		// Token: 0x17000A44 RID: 2628
		// (get) Token: 0x06001B3A RID: 6970
		// (set) Token: 0x06001B3B RID: 6971
		bool GridLinesVisible { get; set; }

		// Token: 0x17000A45 RID: 2629
		// (get) Token: 0x06001B3C RID: 6972
		// (set) Token: 0x06001B3D RID: 6973
		ExcelColors GridLineColor { get; set; }

		// Token: 0x17000A46 RID: 2630
		// (get) Token: 0x06001B3E RID: 6974
		// (set) Token: 0x06001B3F RID: 6975
		bool RowColumnHeadersVisible { get; set; }

		// Token: 0x17000A47 RID: 2631
		// (get) Token: 0x06001B40 RID: 6976
		IVPageBreaks VPageBreaks { get; }

		// Token: 0x17000A48 RID: 2632
		// (get) Token: 0x06001B41 RID: 6977
		IHPageBreaks HPageBreaks { get; }

		// Token: 0x17000A49 RID: 2633
		// (get) Token: 0x06001B42 RID: 6978
		// (set) Token: 0x06001B43 RID: 6979
		bool IsStringsPreserved { get; set; }

		// Token: 0x17000A4A RID: 2634
		// (get) Token: 0x06001B44 RID: 6980
		IComments Comments { get; }

		// Token: 0x17000A4B RID: 2635
		IXLSRange this[int row, int column]
		{
			get;
		}

		// Token: 0x17000A4C RID: 2636
		IXLSRange this[int row, int column, int lastRow, int lastColumn]
		{
			get;
		}

		// Token: 0x17000A4D RID: 2637
		IXLSRange this[string name]
		{
			get;
		}

		// Token: 0x17000A4E RID: 2638
		// (get) Token: 0x06001B48 RID: 6984
		IHyperLinks HyperLinks { get; }

		// Token: 0x17000A4F RID: 2639
		// (get) Token: 0x06001B49 RID: 6985
		// (set) Token: 0x06001B4A RID: 6986
		bool UseRangesCache { get; set; }

		// Token: 0x17000A50 RID: 2640
		// (get) Token: 0x06001B4B RID: 6987
		// (set) Token: 0x06001B4C RID: 6988
		int TopVisibleRow { get; set; }

		// Token: 0x17000A51 RID: 2641
		// (get) Token: 0x06001B4D RID: 6989
		// (set) Token: 0x06001B4E RID: 6990
		int LeftVisibleColumn { get; set; }

		// Token: 0x17000A52 RID: 2642
		// (get) Token: 0x06001B4F RID: 6991
		// (set) Token: 0x06001B50 RID: 6992
		bool AllocatedRangeIncludesFormatting { get; set; }

		// Token: 0x17000A53 RID: 2643
		// (get) Token: 0x06001B51 RID: 6993
		PivotTablesCollection PivotTables { get; }

		// Token: 0x17000A54 RID: 2644
		// (get) Token: 0x06001B52 RID: 6994
		IListObjects ListObjects { get; }

		// Token: 0x17000A55 RID: 2645
		// (get) Token: 0x06001B53 RID: 6995
		IOleObjects OleObjects { get; }

		// Token: 0x17000A56 RID: 2646
		// (get) Token: 0x06001B54 RID: 6996
		bool HasOleObjects { get; }

		// Token: 0x17000A57 RID: 2647
		// (get) Token: 0x06001B55 RID: 6997
		// (set) Token: 0x06001B56 RID: 6998
		FormulaEngine FormulaEngine { get; set; }

		// Token: 0x06001B57 RID: 6999
		void CopyToClipboard();

		// Token: 0x06001B58 RID: 7000
		void Clear();

		// Token: 0x06001B59 RID: 7001
		void ClearData();

		// Token: 0x06001B5A RID: 7002
		bool CheckExistence(int iRow, int iColumn);

		// Token: 0x06001B5B RID: 7003
		void CreateNamedRanges(string namedRange, string referRange, bool vertical);

		// Token: 0x06001B5C RID: 7004
		bool IsColumnVisible(int columnIndex);

		// Token: 0x06001B5D RID: 7005
		bool IsRowVisible(int rowIndex);

		// Token: 0x06001B5E RID: 7006
		void DeleteRow(int index);

		// Token: 0x06001B5F RID: 7007
		void DeleteColumn(int index);

		// Token: 0x06001B60 RID: 7008
		int InsertArray(object[] arrObject, int firstRow, int firstColumn, bool isVertical);

		// Token: 0x06001B61 RID: 7009
		int InsertArray(string[] arrString, int firstRow, int firstColumn, bool isVertical);

		// Token: 0x06001B62 RID: 7010
		int InsertArray(int[] arrInt, int firstRow, int firstColumn, bool isVertical);

		// Token: 0x06001B63 RID: 7011
		int InsertArray(double[] arrDouble, int firstRow, int firstColumn, bool isVertical);

		// Token: 0x06001B64 RID: 7012
		int InsertArray(DateTime[] arrDateTime, int firstRow, int firstColumn, bool isVertical);

		// Token: 0x06001B65 RID: 7013
		int InsertArray(object[,] arrObject, int firstRow, int firstColumn);

		// Token: 0x06001B66 RID: 7014
		int InsertDataColumn(DataColumn dataColumn, bool isFieldNameShown, int firstRow, int firstColumn);

		// Token: 0x06001B67 RID: 7015
		int InsertDataTable(DataTable dataTable, bool isFieldNameShown, int firstRow, int firstColumn);

		// Token: 0x06001B68 RID: 7016
		int InsertDataTable(DataTable dataTable, bool isFieldNameShown, int firstRow, int firstColumn, bool preserveTypes);

		// Token: 0x06001B69 RID: 7017
		int InsertDataTable(DataTable dataTable, bool isFieldNameShown, int firstRow, int firstColumn, int maxRows, int maxColumns);

		// Token: 0x06001B6A RID: 7018
		int InsertDataTable(DataTable dataTable, bool isFieldNameShown, int firstRow, int firstColumn, int maxRows, int maxColumns, bool preserveTypes);

		// Token: 0x06001B6B RID: 7019
		int InsertDataView(DataView dataView, bool isFieldNameShown, int firstRow, int firstColumn);

		// Token: 0x06001B6C RID: 7020
		int InsertDataView(DataView dataView, bool isFieldNameShown, int firstRow, int firstColumn, bool bPreserveTypes);

		// Token: 0x06001B6D RID: 7021
		int InsertDataView(DataView dataView, bool isFieldNameShown, int firstRow, int firstColumn, int maxRows, int maxColumns);

		// Token: 0x06001B6E RID: 7022
		int InsertDataView(DataView dataView, bool isFieldNameShown, int firstRow, int firstColumn, int maxRows, int maxColumns, bool bPreserveTypes);

		// Token: 0x06001B6F RID: 7023
		void RemovePanes();

		// Token: 0x06001B70 RID: 7024
		void Protect(string password);

		// Token: 0x06001B71 RID: 7025
		void Unprotect(string password);

		// Token: 0x06001B72 RID: 7026
		void AutoFitRow(int rowIndex);

		// Token: 0x06001B73 RID: 7027
		void AutoFitColumn(int columnIndex);

		// Token: 0x06001B74 RID: 7028
		void Replace(string oldValue, string newValue);

		// Token: 0x06001B75 RID: 7029
		void Replace(string oldValue, double newValue);

		// Token: 0x06001B76 RID: 7030
		void Replace(string oldValue, DateTime newValue);

		// Token: 0x06001B77 RID: 7031
		void Replace(string oldValue, string[] newValues, bool isVertical);

		// Token: 0x06001B78 RID: 7032
		void Replace(string oldValue, int[] newValues, bool isVertical);

		// Token: 0x06001B79 RID: 7033
		void Replace(string oldValue, double[] newValues, bool isVertical);

		// Token: 0x06001B7A RID: 7034
		void Replace(string oldValue, DataTable newValues, bool isFieldNamesShown);

		// Token: 0x06001B7B RID: 7035
		void Replace(string oldValue, DataColumn newValues, bool isFieldNamesShown);

		// Token: 0x06001B7C RID: 7036
		void Remove();

		// Token: 0x06001B7D RID: 7037
		void MoveWorksheet(int iNewIndex);

		// Token: 0x06001B7E RID: 7038
		int ColumnWidthToPixels(double widthInChars);

		// Token: 0x06001B7F RID: 7039
		double PixelsToColumnWidth(double pixels);

		// Token: 0x06001B80 RID: 7040
		void SetColumnWidthInPixels(int columnIndex, int value);

		// Token: 0x06001B81 RID: 7041
		void SetRowHeightPixels(int Row, double value);

		// Token: 0x06001B82 RID: 7042
		int GetColumnWidthPixels(int Column);

		// Token: 0x06001B83 RID: 7043
		int GetRowHeightPixels(int Row);

		// Token: 0x06001B84 RID: 7044
		void SaveToFile(string fileName, string separator);

		// Token: 0x06001B85 RID: 7045
		void SaveToStream(Stream stream, string separator);

		// Token: 0x06001B86 RID: 7046
		void SetDefaultColumnStyle(int iColumnIndex, IStyle defaultStyle);

		// Token: 0x06001B87 RID: 7047
		void SetDefaultColumnStyle(int iStartColumnIndex, int iEndColumnIndex, IStyle defaultStyle);

		// Token: 0x06001B88 RID: 7048
		void SetDefaultRowStyle(int rowIndex, IStyle defaultStyle);

		// Token: 0x06001B89 RID: 7049
		void SetDefaultRowStyle(int iStartRowIndex, int iEndRowIndex, IStyle defaultStyle);

		// Token: 0x06001B8A RID: 7050
		IStyle GetDefaultColumnStyle(int iColumnIndex);

		// Token: 0x06001B8B RID: 7051
		IStyle GetDefaultRowStyle(int rowIndex);

		// Token: 0x06001B8C RID: 7052
		void SetValue(int iRow, int iColumn, string value);

		// Token: 0x06001B8D RID: 7053
		void SetNumber(int iRow, int iColumn, double value);

		// Token: 0x06001B8E RID: 7054
		void SetBoolean(int iRow, int iColumn, bool value);

		// Token: 0x06001B8F RID: 7055
		void SetText(int iRow, int iColumn, string value);

		// Token: 0x06001B90 RID: 7056
		void SetFormula(int iRow, int iColumn, string value);

		// Token: 0x06001B91 RID: 7057
		void SetError(int iRow, int iColumn, string value);

		// Token: 0x06001B92 RID: 7058
		void SetBlank(int iRow, int iColumn);

		// Token: 0x06001B93 RID: 7059
		void SetFormulaNumberValue(int iRow, int iColumn, double value);

		// Token: 0x06001B94 RID: 7060
		void SetFormulaErrorValue(int iRow, int iColumn, string value);

		// Token: 0x06001B95 RID: 7061
		void SetFormulaBoolValue(int iRow, int iColumn, bool value);

		// Token: 0x06001B96 RID: 7062
		void SetFormulaStringValue(int iRow, int iColumn, string value);

		// Token: 0x06001B97 RID: 7063
		string GetText(int row, int column);

		// Token: 0x06001B98 RID: 7064
		double GetNumber(int row, int column);

		// Token: 0x06001B99 RID: 7065
		string GetFormula(int row, int column, bool bR1C1);

		// Token: 0x06001B9A RID: 7066
		string GetError(int row, int column);

		// Token: 0x06001B9B RID: 7067
		bool GetBoolean(int row, int column);

		// Token: 0x06001B9C RID: 7068
		bool GetFormulaBoolValue(int row, int column);

		// Token: 0x06001B9D RID: 7069
		string GetFormulaErrorValue(int row, int column);

		// Token: 0x06001B9E RID: 7070
		double GetFormulaNumberValue(int row, int column);

		// Token: 0x06001B9F RID: 7071
		string GetFormulaStringValue(int row, int column);

		// Token: 0x06001BA0 RID: 7072
		Image SaveToImage(int firstRow, int firstColumn, int lastRow, int lastColumn);

		// Token: 0x06001BA1 RID: 7073
		Image SaveToImage(Stream stream, int firstRow, int firstColumn, int lastRow, int lastColumn, ImageType imageType);

		// Token: 0x06001BA2 RID: 7074
		void SaveToHtml(string filename);

		// Token: 0x06001BA3 RID: 7075
		void SaveToHtml(Stream stream);

		// Token: 0x06001BA4 RID: 7076
		void SaveToHtml(string filename, HTMLOptions saveOptions);

		// Token: 0x06001BA5 RID: 7077
		void SaveToHtml(Stream stream, HTMLOptions saveOptions);

		// Token: 0x06001BA6 RID: 7078
		Image SaveToImage(Stream outputStream, int firstRow, int firstColumn, int lastRow, int lastColumn, EmfType emfType);

		// Token: 0x06001BA7 RID: 7079
		Image SaveToImage(Stream outputStream, int firstRow, int firstColumn, int lastRow, int lastColumn, ImageType imageType, EmfType emfType);

		// Token: 0x14000019 RID: 25
		// (add) Token: 0x06001BA8 RID: 7080
		// (remove) Token: 0x06001BA9 RID: 7081
		event XlsRange.CellValueChangedEventHandler CellValueChanged;
	}
}
