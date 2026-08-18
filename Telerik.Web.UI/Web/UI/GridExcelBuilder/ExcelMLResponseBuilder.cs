using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Text;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI.GridExcelBuilder
{
	// Token: 0x02001B25 RID: 6949
	internal class ExcelMLResponseBuilder
	{
		// Token: 0x06010CCF RID: 68815 RVA: 0x003BA5EA File Offset: 0x003B87EA
		public ExcelMLResponseBuilder(WorkBook book, GridTableView tableView, GridEnumerableBase gridEnumerableBase)
		{
			this._book = book;
			this._tableView = tableView;
			this._gridEnumerableBase = gridEnumerableBase;
		}

		// Token: 0x06010CD0 RID: 68816 RVA: 0x003BA60E File Offset: 0x003B880E
		protected virtual void OnRowCreated(GridExportExcelMLRowCreatedArgs args)
		{
			if (this._tableView != null)
			{
				this._tableView.OwnerGrid.CallOnExcelMLExportRowCreated(args);
			}
		}

		// Token: 0x06010CD1 RID: 68817 RVA: 0x003BA629 File Offset: 0x003B8829
		protected virtual void OnStylesCreated(GridExportExcelMLStyleCreatedArgs args)
		{
			if (this._tableView != null)
			{
				this._tableView.OwnerGrid.CallOnExcelMLExportStylesCreated(args);
			}
		}

		// Token: 0x06010CD2 RID: 68818 RVA: 0x003BA644 File Offset: 0x003B8844
		internal void AppendStyles()
		{
			GridTableItemStyle gridTableItemStyle = this._tableView.OwnerGrid.HeaderStyle;
			HorizontalAlign defaultCellAlignment = this._tableView.OwnerGrid.ExportSettings.Excel.DefaultCellAlignment;
			if (gridTableItemStyle != null || defaultCellAlignment != HorizontalAlign.NotSet)
			{
				StyleElement value = this.ConvertGridStyle(gridTableItemStyle, ExcelMLResponseBuilder.Constants.HeaderStyleName);
				this._book.Styles.Add(value);
			}
			gridTableItemStyle = this._tableView.OwnerGrid.ItemStyle;
			if (gridTableItemStyle != null || defaultCellAlignment != HorizontalAlign.NotSet)
			{
				StyleElement value2 = this.ConvertGridStyle(gridTableItemStyle, ExcelMLResponseBuilder.Constants.ItemStyleName);
				this._book.Styles.Add(value2);
				StyleElement styleElement = this.ConvertGridStyle(gridTableItemStyle, ExcelMLResponseBuilder.Constants.DateItemStyleName);
				styleElement.NumberFormat.FormatType = NumberFormatType.GeneralDate;
				this._book.Styles.Add(styleElement);
			}
			gridTableItemStyle = this._tableView.OwnerGrid.AlternatingItemStyle;
			if (gridTableItemStyle != null || defaultCellAlignment != HorizontalAlign.NotSet)
			{
				StyleElement value3 = this.ConvertGridStyle(gridTableItemStyle, ExcelMLResponseBuilder.Constants.AlternatingItemStyleName);
				this._book.Styles.Add(value3);
				StyleElement styleElement2 = this.ConvertGridStyle(gridTableItemStyle, ExcelMLResponseBuilder.Constants.AlternatingDateItemStyleName);
				styleElement2.NumberFormat.FormatType = NumberFormatType.GeneralDate;
				this._book.Styles.Add(styleElement2);
			}
			this.OnStylesCreated(new GridExportExcelMLStyleCreatedArgs(this._book.Styles));
		}

		// Token: 0x06010CD3 RID: 68819 RVA: 0x003BA784 File Offset: 0x003B8984
		internal void AppendColumns(TableElement tableElement, DataTable dataTable, GridTableView tableView)
		{
			foreach (object obj in dataTable.Columns)
			{
				DataColumn dataColumn = (DataColumn)obj;
				if (dataColumn.ColumnName != "OriginalDataItem" && this.IsColumnVisible(dataColumn.ColumnName, tableView))
				{
					ColumnElement columnElement = new ColumnElement();
					columnElement.Attributes.Add("ss:Width", "100");
					tableElement.Columns.Add(columnElement);
				}
			}
		}

		// Token: 0x06010CD4 RID: 68820 RVA: 0x003BA820 File Offset: 0x003B8A20
		private DataRow ExtractOriginalItemRow(DataRow dataRow)
		{
			if (dataRow.Table.Columns.Contains("OriginalDataItem"))
			{
				DataRowView dataRowView = dataRow["OriginalDataItem"] as DataRowView;
				if (dataRowView != null)
				{
					return dataRowView.Row;
				}
			}
			return dataRow;
		}

		// Token: 0x06010CD5 RID: 68821 RVA: 0x003BA860 File Offset: 0x003B8A60
		private static int CompareKeys(KeyValuePair<int, DataColumn> a, KeyValuePair<int, DataColumn> b)
		{
			return a.Key.CompareTo(b.Key);
		}

		// Token: 0x06010CD6 RID: 68822 RVA: 0x003BA884 File Offset: 0x003B8A84
		private static List<DataColumn> ReorderColumns(DataColumnCollection originalColumns, GridTableView tableView)
		{
			List<KeyValuePair<int, DataColumn>> list = new List<KeyValuePair<int, DataColumn>>();
			foreach (object obj in originalColumns)
			{
				DataColumn dataColumn = (DataColumn)obj;
				GridColumn columnBy = ExcelMLResponseBuilder.GetColumnBy(dataColumn.ColumnName, tableView);
				if (columnBy != null)
				{
					list.Add(new KeyValuePair<int, DataColumn>(columnBy.OrderIndex, dataColumn));
				}
			}
			list.Sort(new Comparison<KeyValuePair<int, DataColumn>>(ExcelMLResponseBuilder.CompareKeys));
			List<DataColumn> list2 = new List<DataColumn>(list.Count);
			foreach (KeyValuePair<int, DataColumn> keyValuePair in list)
			{
				list2.Add(keyValuePair.Value);
			}
			return list2;
		}

		// Token: 0x06010CD7 RID: 68823 RVA: 0x003BA964 File Offset: 0x003B8B64
		[SuppressMessage("Microsoft.Usage", "CA2233:OperationsShouldNotOverflow")]
		internal void AppendRows(TableElement tableElement, DataRow[] dataRows, int indentCount, GridTableView tableView)
		{
			int num = 0;
			bool flag = false;
			List<DataColumn> list = null;
			foreach (DataRow dataRow in dataRows)
			{
				DataRow dataRow2 = this.ExtractOriginalItemRow(dataRow);
				if (!flag)
				{
					list = ExcelMLResponseBuilder.ReorderColumns(dataRow2.Table.Columns, tableView);
					flag = true;
				}
				RowElement rowElement = new RowElement();
				tableElement.Rows.Add(rowElement);
				int num2 = 0;
				for (int j = 0; j < list.Count; j++)
				{
					if (list[j].ColumnName != "OriginalDataItem" && this.IsColumnVisible(list[j].ColumnName, tableView))
					{
						CellElement cellElement = new CellElement();
						rowElement.Cells.Add(cellElement);
						object obj = dataRow2[list[j].ColumnName];
						if (obj is Guid)
						{
							obj = obj.ToString();
						}
						GridColumn columnBy = ExcelMLResponseBuilder.GetColumnBy(list[j].ColumnName, tableView);
						if (columnBy != null)
						{
							cellElement.ColumnName = columnBy.UniqueName;
							GridHyperLinkColumn gridHyperLinkColumn = columnBy as GridHyperLinkColumn;
							if (gridHyperLinkColumn != null)
							{
								string text = (!string.IsNullOrEmpty(gridHyperLinkColumn.DataTextField)) ? gridHyperLinkColumn.DataTextField : gridHyperLinkColumn.NavigateUrl;
								if (string.IsNullOrEmpty(text))
								{
									obj = gridHyperLinkColumn.Text;
								}
								else
								{
									string format = string.IsNullOrEmpty(gridHyperLinkColumn.DataTextFormatString) ? "{0}" : gridHyperLinkColumn.DataTextFormatString;
									obj = string.Format(format, dataRow2[text]);
								}
								if (gridHyperLinkColumn.DataNavigateUrlFields.Length == 0 && !string.IsNullOrEmpty(gridHyperLinkColumn.NavigateUrl))
								{
									cellElement.Attributes.Add("ss:HRef", gridHyperLinkColumn.NavigateUrl);
								}
								else if (gridHyperLinkColumn.DataNavigateUrlFields.Length > 0)
								{
									cellElement.Attributes.Add("ss:HRef", this.BuildNavigateUrlString(gridHyperLinkColumn, dataRow2));
								}
							}
						}
						else
						{
							cellElement.ColumnName = list[j].ColumnName;
						}
						cellElement.Data.DataItem = obj;
						cellElement.Attributes.Add("ss:Index", Convert.ToString(num2 + indentCount + 1));
						if (num % 2 == 0)
						{
							if (cellElement.Data.DataType == DataType.DateTime)
							{
								cellElement.StyleValue = ExcelMLResponseBuilder.Constants.AlternatingDateItemStyleName;
							}
							else
							{
								cellElement.StyleValue = ExcelMLResponseBuilder.Constants.AlternatingItemStyleName;
							}
						}
						else if (cellElement.Data.DataType == DataType.DateTime)
						{
							cellElement.StyleValue = ExcelMLResponseBuilder.Constants.DateItemStyleName;
						}
						else
						{
							cellElement.StyleValue = ExcelMLResponseBuilder.Constants.ItemStyleName;
						}
						num2++;
					}
				}
				num++;
				this.OnRowCreated(new GridExportExcelMLRowCreatedArgs(rowElement, GridExportExcelMLRowType.DataRow, this._currentWorksheet));
				this.AppendRowsChilds(tableElement, dataRow2, indentCount + 1, tableView, num);
			}
		}

		// Token: 0x06010CD8 RID: 68824 RVA: 0x003BAC24 File Offset: 0x003B8E24
		private string BuildNavigateUrlString(GridHyperLinkColumn linkColumn, DataRow innerRow)
		{
			List<object> list = new List<object>();
			foreach (string columnName in linkColumn.DataNavigateUrlFields)
			{
				list.Add(innerRow[columnName]);
			}
			return string.Format(linkColumn.DataNavigateUrlFormatString, list.ToArray());
		}

		// Token: 0x06010CD9 RID: 68825 RVA: 0x003BAC70 File Offset: 0x003B8E70
		protected StyleElement ConvertGridStyle(GridTableItemStyle gridTableStyle, string styleId)
		{
			HorizontalAlign defaultCellAlignment = this._tableView.OwnerGrid.ExportSettings.Excel.DefaultCellAlignment;
			StyleElement styleElement = new StyleElement(styleId);
			styleElement.AlignmentElement.HorizontalAlignment = Utils.ConvertHorizontalAlign((defaultCellAlignment != HorizontalAlign.NotSet) ? defaultCellAlignment : gridTableStyle.HorizontalAlign);
			if (gridTableStyle != null)
			{
				styleElement.InteriorStyle.Color = gridTableStyle.BackColor;
				if (gridTableStyle.BackColor != Color.Empty)
				{
					styleElement.InteriorStyle.Pattern = InteriorPatternType.Solid;
				}
				styleElement.FontStyle.Bold = gridTableStyle.Font.Bold;
				styleElement.FontStyle.Color = gridTableStyle.ForeColor;
				styleElement.FontStyle.FontName = gridTableStyle.Font.Name;
				styleElement.FontStyle.Italic = gridTableStyle.Font.Italic;
			}
			return styleElement;
		}

		// Token: 0x06010CDA RID: 68826 RVA: 0x003BAD44 File Offset: 0x003B8F44
		private string GetGroupByFieldAlias(string ColumnName, GridTableView tableView)
		{
			foreach (GridGroupByExpression gridGroupByExpression in tableView.GroupByExpressions)
			{
				foreach (object obj in gridGroupByExpression.SelectFields)
				{
					GridGroupByField gridGroupByField = (GridGroupByField)obj;
					if (gridGroupByField.FieldName.Equals(ColumnName, StringComparison.OrdinalIgnoreCase))
					{
						return (!string.IsNullOrEmpty(gridGroupByField.HeaderText)) ? gridGroupByField.HeaderText : gridGroupByField.FieldName;
					}
				}
			}
			return string.Empty;
		}

		// Token: 0x06010CDB RID: 68827 RVA: 0x003BAE10 File Offset: 0x003B9010
		private bool IsParentGroupExpColumn(string ColumnName, DataRow dataRow)
		{
			bool result = false;
			foreach (object obj in dataRow.Table.ParentRelations)
			{
				DataRelation dataRelation = (DataRelation)obj;
				foreach (DataColumn dataColumn in dataRelation.ParentColumns)
				{
					if (dataColumn.ColumnName.Equals(ColumnName, StringComparison.OrdinalIgnoreCase))
					{
						return true;
					}
				}
			}
			return result;
		}

		// Token: 0x06010CDC RID: 68828 RVA: 0x003BAEA8 File Offset: 0x003B90A8
		[SuppressMessage("Microsoft.Usage", "CA2233:OperationsShouldNotOverflow")]
		internal void AppendGroupingRow(TableElement tableElement, DataColumnCollection dataColumns, DataRow[] dataRows, int indentCount, GridTableView tableView)
		{
			int num = 0;
			foreach (DataRow dataRow in dataRows)
			{
				RowElement rowElement = new RowElement();
				tableElement.Rows.Add(rowElement);
				StringBuilder stringBuilder = new StringBuilder();
				int num2 = 0;
				for (int j = 0; j < dataColumns.Count; j++)
				{
					if (dataColumns[j].ColumnName != "OriginalDataItem" && dataColumns[j].ColumnName != "SplitGroup" && !string.IsNullOrEmpty(dataColumns[j].ColumnName) && !this.IsParentGroupExpColumn(dataColumns[j].ColumnName, dataRow))
					{
						string groupByFieldAlias = this.GetGroupByFieldAlias(dataColumns[j].ColumnName, tableView);
						dataRow[dataColumns[j].ColumnName].ToString();
						if (!string.IsNullOrEmpty(groupByFieldAlias))
						{
							stringBuilder.AppendFormat(" {0}: {1} ", groupByFieldAlias, dataRow[dataColumns[j].ColumnName]);
						}
						num2++;
					}
				}
				CellElement cellElement = new CellElement();
				rowElement.Cells.Add(cellElement);
				cellElement.Data.DataItem = stringBuilder.ToString();
				cellElement.Attributes.Add("ss:Index", Convert.ToString(indentCount + 1));
				cellElement.StyleValue = ExcelMLResponseBuilder.Constants.HeaderStyleName;
				num++;
				this.OnRowCreated(new GridExportExcelMLRowCreatedArgs(rowElement, GridExportExcelMLRowType.GroupByHeaderRow, this._currentWorksheet));
				this.AppendRowsChilds(tableElement, dataRow, indentCount + 1, tableView, num);
			}
		}

		// Token: 0x06010CDD RID: 68829 RVA: 0x003BB048 File Offset: 0x003B9248
		private void AppendRowsChilds(TableElement tableElement, DataRow dataRow, int indentCount, GridTableView tableView, int rowCount)
		{
			foreach (object obj in dataRow.Table.ChildRelations)
			{
				DataRelation dataRelation = (DataRelation)obj;
				if (!dataRelation.RelationName.Contains("GroupedTable"))
				{
					this.BuildDetailTable(tableElement, indentCount, tableView, rowCount);
				}
				else
				{
					DataRow[] array = this.SelectAllChildNodes(dataRow, dataRelation);
					if (array.Length > 0)
					{
						if (this._showHeader)
						{
							this.AppendHeaderRow(tableElement, array[0].Table, array.Length, indentCount, tableView);
						}
						if (array[0].Table.ChildRelations.Count == 0 && !this._headerWithGroupingInserted && !this.IsMainHeaderRowAdded)
						{
							this.AppendHeaderRowAt(tableElement, array[0].Table, array.Length, indentCount, 0, tableView);
							this._headerWithGroupingInserted = true;
						}
						if (array[0].Table.ChildRelations.Count > 0)
						{
							this.AppendGroupingRow(tableElement, array[0].Table.Columns, array, indentCount, tableView);
						}
						else
						{
							this.AppendRows(tableElement, array, indentCount, tableView);
						}
					}
				}
			}
			if (dataRow.Table.ChildRelations.Count == 0)
			{
				this.BuildDetailTable(tableElement, indentCount, tableView, rowCount);
			}
		}

		// Token: 0x06010CDE RID: 68830 RVA: 0x003BB190 File Offset: 0x003B9390
		private DataRow[] SelectAllChildNodes(DataRow dataRow, DataRelation relation)
		{
			int num = relation.ChildColumns.Length;
			string empty = string.Empty;
			List<string> list = new List<string>();
			string text = string.Empty;
			for (int i = 0; i < num; i++)
			{
				object obj = dataRow[relation.ParentColumns[i]];
				text = relation.ChildColumns[i].ColumnName;
				if (obj == DBNull.Value)
				{
					list.Add(text + " is null");
				}
				else
				{
					list.Add(string.Format("{0} = '{1}'", text, obj.ToString().Replace("'", "''")));
				}
			}
			return relation.ChildTable.Select(string.Join(" AND ", list.ToArray()));
		}

		// Token: 0x06010CDF RID: 68831 RVA: 0x003BB248 File Offset: 0x003B9448
		[SuppressMessage("Microsoft.Usage", "CA2233:OperationsShouldNotOverflow")]
		private void BuildDetailTable(TableElement tableElement, int indentCount, GridTableView tableView, int rowCount)
		{
			GridItem[] items = tableView.GetItems(new GridItemType[]
			{
				GridItemType.NestedView
			});
			if (items.Length > rowCount - 1 && items[rowCount - 1] != null)
			{
				GridNestedViewItem gridNestedViewItem = (GridNestedViewItem)items[rowCount - 1];
				foreach (GridTableView gridTableView in gridNestedViewItem.NestedTableViews)
				{
					GridEnumerableBase resolvedDataSource = gridTableView._resolvedDataSource;
					if (resolvedDataSource != null && resolvedDataSource.GroupingDataSet != null)
					{
						DataTable dataTable = resolvedDataSource.GroupingDataSet.Tables[0];
						if (dataTable != null)
						{
							DataRow[] array = new DataRow[dataTable.Rows.Count];
							dataTable.Rows.CopyTo(array, 0);
							if (dataTable.ChildRelations.Count > 0)
							{
								DataTable childTable = dataTable.ChildRelations[dataTable.ChildRelations.Count - 1].ChildTable;
								this.AppendGroupingRow(tableElement, dataTable.Columns, array, indentCount, gridTableView);
							}
							else
							{
								this.AppendHeaderRow(tableElement, dataTable, dataTable.Rows.Count, indentCount, gridTableView);
								this.AppendRows(tableElement, array, indentCount, gridTableView);
							}
						}
					}
				}
			}
		}

		// Token: 0x06010CE0 RID: 68832 RVA: 0x003BB370 File Offset: 0x003B9570
		internal void BuildExcelTable(int worksheetIndex)
		{
			if (worksheetIndex > this._book.Worksheets.Count || worksheetIndex < 0)
			{
				throw new ArgumentOutOfRangeException("worksheetIndex must be inside Workbook.Worksheets collection boundaries.");
			}
			if (this._gridEnumerableBase.GroupingDataSet == null)
			{
				throw new Exception("GridEnumerableBase.GroupingDataSet cannot be null");
			}
			WorksheetElement worksheetElement = this._book.Worksheets[worksheetIndex];
			if (worksheetElement != null)
			{
				this._currentWorksheet = worksheetElement;
				DataTable dataTable = this._gridEnumerableBase.GroupingDataSet.Tables[0];
				if (dataTable != null)
				{
					TableElement tableElement = new TableElement();
					worksheetElement.Table = tableElement;
					DataRow[] array = new DataRow[dataTable.Rows.Count];
					dataTable.Rows.CopyTo(array, 0);
					if (dataTable.ChildRelations.Count > 0)
					{
						this._showHeader = false;
						DataTable childTable = dataTable.ChildRelations[dataTable.ChildRelations.Count - 1].ChildTable;
						this.AppendGroupingRow(tableElement, dataTable.Columns, array, 0, this._tableView);
					}
					else
					{
						this._showHeader = true;
						this.AppendHeaderRow(tableElement, dataTable, dataTable.Rows.Count, 0, this._tableView);
						this.AppendRows(tableElement, array, 0, this._tableView);
					}
				}
			}
			this._tableView.OwnerGrid.CallOnExcelMLWorkBookCreated(new GridExcelMLWorkBookCreatedEventArgs(this._book));
		}

		// Token: 0x06010CE1 RID: 68833 RVA: 0x003BB4B3 File Offset: 0x003B96B3
		internal void BuildExcelTable()
		{
			this.BuildExcelTable(0);
		}

		// Token: 0x06010CE2 RID: 68834 RVA: 0x003BB4BC File Offset: 0x003B96BC
		internal void AppendHeaderRow(TableElement tableElement, DataTable dataTable, int rowSpan, int indentCount, GridTableView tableView)
		{
			this.AppendHeaderRowAt(tableElement, dataTable, rowSpan, indentCount, tableElement.Rows.Count, tableView);
		}

		// Token: 0x06010CE3 RID: 68835 RVA: 0x003BB4D8 File Offset: 0x003B96D8
		internal virtual bool IsColumnVisible(string dataColumnName, GridTableView tableView)
		{
			foreach (GridColumn gridColumn in tableView.RenderColumns)
			{
				if (gridColumn.IsBoundToFieldName(dataColumnName))
				{
					return gridColumn.Visible;
				}
			}
			return false;
		}

		// Token: 0x06010CE4 RID: 68836 RVA: 0x003BB514 File Offset: 0x003B9714
		internal static GridColumn GetColumnBy(string dataColumnName, GridTableView tableView)
		{
			foreach (GridColumn gridColumn in tableView.RenderColumns)
			{
				if (gridColumn.IsBoundToFieldName(dataColumnName))
				{
					return gridColumn;
				}
			}
			return null;
		}

		// Token: 0x06010CE5 RID: 68837 RVA: 0x003BB54C File Offset: 0x003B974C
		[SuppressMessage("Microsoft.Usage", "CA2233:OperationsShouldNotOverflow")]
		internal void AppendHeaderRowAt(TableElement tableElement, DataTable dataTable, int rowSpan, int indentCount, int rowIndex, GridTableView tableView)
		{
			DataTable dataTable2 = dataTable;
			if (dataTable.Rows.Count > 0)
			{
				DataRow dataRow = this.ExtractOriginalItemRow(dataTable.Rows[0]);
				dataTable2 = dataRow.Table;
			}
			if (dataTable2 != null)
			{
				RowElement rowElement = new RowElement();
				tableElement.Rows.Insert(rowIndex, rowElement);
				int num = this.CreateHeaderRowCells(rowElement, tableView, indentCount, dataTable2);
				int val = indentCount + 1;
				int num2 = indentCount + num;
				if (num2 > 0 && !this.IsMainHeaderRowAdded)
				{
					this._book.Worksheets[0].AutoFilter.Range = string.Format("R1C{0}:R1C{1}", Math.Min(val, num2), Math.Max(val, num2));
				}
				this.OnRowCreated(new GridExportExcelMLRowCreatedArgs(rowElement, GridExportExcelMLRowType.HeaderRow, this._currentWorksheet));
				this.AppendColumns(tableElement, dataTable2, tableView);
			}
		}

		// Token: 0x06010CE6 RID: 68838 RVA: 0x003BB624 File Offset: 0x003B9824
		private int CreateHeaderRowCells(RowElement headerRow, GridTableView tableView, int indentCount, DataTable dataTable)
		{
			int num = 0;
			List<DataColumn> list = ExcelMLResponseBuilder.ReorderColumns(dataTable.Columns, tableView);
			foreach (DataColumn dataColumn in list)
			{
				if (dataColumn.ColumnName != "OriginalDataItem" && this.IsColumnVisible(dataColumn.ColumnName, tableView))
				{
					CellElement cellElement = new CellElement();
					headerRow.Cells.Add(cellElement);
					cellElement.Attributes.Add("ss:Index", Convert.ToString(indentCount + num + 1));
					GridColumn columnBy = ExcelMLResponseBuilder.GetColumnBy(dataColumn.ColumnName, tableView);
					if (columnBy != null)
					{
						cellElement.Data.DataItem = columnBy.HeaderText;
						cellElement.ColumnName = columnBy.UniqueName;
					}
					else
					{
						cellElement.Data.DataItem = dataColumn.ColumnName;
						cellElement.ColumnName = dataColumn.ColumnName;
					}
					cellElement.StyleValue = ExcelMLResponseBuilder.Constants.HeaderStyleName;
					num++;
				}
			}
			return num;
		}

		// Token: 0x170051D9 RID: 20953
		// (get) Token: 0x06010CE7 RID: 68839 RVA: 0x003BB738 File Offset: 0x003B9938
		private bool IsMainHeaderRowAdded
		{
			get
			{
				return !string.IsNullOrEmpty(this._book.Worksheets[0].AutoFilter.Range);
			}
		}

		// Token: 0x04004B2C RID: 19244
		private WorkBook _book;

		// Token: 0x04004B2D RID: 19245
		private WorksheetElement _currentWorksheet;

		// Token: 0x04004B2E RID: 19246
		private GridEnumerableBase _gridEnumerableBase;

		// Token: 0x04004B2F RID: 19247
		private bool _headerWithGroupingInserted;

		// Token: 0x04004B30 RID: 19248
		private bool _showHeader = true;

		// Token: 0x04004B31 RID: 19249
		private GridTableView _tableView;

		// Token: 0x02001B26 RID: 6950
		private class Constants
		{
			// Token: 0x04004B32 RID: 19250
			public static readonly string AlternatingDateItemStyleName = "alternatingDateItemStyle";

			// Token: 0x04004B33 RID: 19251
			public static readonly string AlternatingItemStyleName = "alternatingItemStyle";

			// Token: 0x04004B34 RID: 19252
			public static readonly string DateItemStyleName = "dateItemStyle";

			// Token: 0x04004B35 RID: 19253
			public static readonly string HeaderStyleName = "headerStyle";

			// Token: 0x04004B36 RID: 19254
			public static readonly string ItemStyleName = "itemStyle";
		}
	}
}
