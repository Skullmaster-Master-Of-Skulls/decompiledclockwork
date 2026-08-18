using System;
using System.Collections;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace Telerik.Web.UI
{
	// Token: 0x020010CA RID: 4298
	internal class GridDataSetHelper
	{
		// Token: 0x0600AF62 RID: 44898 RVA: 0x0025F76B File Offset: 0x0025D96B
		public GridDataSetHelper(GridGroupByExpressionCollection expressions, GridSortExpressionCollection sortExpressions)
		{
			this.groupByExpressions = expressions;
			this.sortExpressions = sortExpressions;
		}

		// Token: 0x170038A2 RID: 14498
		// (get) Token: 0x0600AF63 RID: 44899 RVA: 0x0025F781 File Offset: 0x0025D981
		public ArrayList GroupByFieldInfoList
		{
			get
			{
				return this.groupGroupByFields;
			}
		}

		// Token: 0x170038A3 RID: 14499
		// (get) Token: 0x0600AF64 RID: 44900 RVA: 0x0025F789 File Offset: 0x0025D989
		public ArrayList FieldInfoList
		{
			get
			{
				return this.groupSelectFields;
			}
		}

		// Token: 0x0600AF65 RID: 44901 RVA: 0x0025F794 File Offset: 0x0025D994
		internal static DataTable CloneTableStructure(DataTable sourceTable)
		{
			DataTable dataTable = new DataTable();
			dataTable.Locale = (CultureInfo)sourceTable.Locale.Clone();
			dataTable.CaseSensitive = sourceTable.CaseSensitive;
			foreach (object obj in sourceTable.Columns)
			{
				DataColumn dataColumn = (DataColumn)obj;
				if (!string.IsNullOrEmpty(dataColumn.Expression))
				{
					dataTable.Columns.Add(dataColumn.ColumnName, dataColumn.DataType, dataColumn.Expression);
				}
				else
				{
					dataTable.Columns.Add(dataColumn.ColumnName, dataColumn.DataType);
				}
			}
			return dataTable;
		}

		// Token: 0x0600AF66 RID: 44902 RVA: 0x0025F854 File Offset: 0x0025DA54
		internal static DataTable CloneTableStructure(DataTable res, DataTable sourceTable)
		{
			res.Locale = (CultureInfo)sourceTable.Locale.Clone();
			res.CaseSensitive = sourceTable.CaseSensitive;
			foreach (object obj in sourceTable.Columns)
			{
				DataColumn dataColumn = (DataColumn)obj;
				if (!string.IsNullOrEmpty(dataColumn.Expression))
				{
					res.Columns.Add(dataColumn.ColumnName, dataColumn.DataType, dataColumn.Expression);
				}
				else
				{
					res.Columns.Add(dataColumn.ColumnName, dataColumn.DataType);
				}
			}
			return res;
		}

		// Token: 0x0600AF67 RID: 44903 RVA: 0x0025F910 File Offset: 0x0025DB10
		private DataTable CreateGroupByTable(DataTable SourceTable, GridTableView view)
		{
			DataTable dataTable = new DataTable();
			dataTable.Locale = (CultureInfo)SourceTable.Locale.Clone();
			dataTable.CaseSensitive = SourceTable.CaseSensitive;
			foreach (object obj in this.groupSelectFields)
			{
				GridGroupByField gridGroupByField = (GridGroupByField)obj;
				DataColumn dataColumn = SourceTable.Columns[gridGroupByField.FieldName];
				if (dataColumn == null)
				{
					throw new GridGroupByException("Field " + gridGroupByField.FieldName + " not found in the source table. Please check the expression syntax.");
				}
				if (gridGroupByField.Aggregate == GridAggregateFunction.None)
				{
					dataTable.Columns.Add(gridGroupByField.FieldAlias, dataColumn.DataType, dataColumn.Expression);
				}
				else if (gridGroupByField.Aggregate == GridAggregateFunction.Count)
				{
					dataTable.Columns.Add(gridGroupByField.FieldAlias, typeof(int));
				}
				else if (gridGroupByField.Aggregate == GridAggregateFunction.Sum || gridGroupByField.Aggregate == GridAggregateFunction.Avg)
				{
					dataTable.Columns.Add(gridGroupByField.FieldAlias, typeof(decimal));
				}
				else
				{
					dataTable.Columns.Add(gridGroupByField.FieldAlias, dataColumn.DataType);
				}
			}
			foreach (GridColumn gridColumn in view.RenderColumns)
			{
				GridBoundColumn gridBoundColumn = gridColumn as GridBoundColumn;
				if (gridBoundColumn != null && gridBoundColumn.Aggregate != GridAggregateFunction.None && !dataTable.Columns.Contains(gridBoundColumn.DataField))
				{
					dataTable.Columns.Add(gridBoundColumn.DataField, gridBoundColumn.DataType);
				}
				GridTemplateColumn gridTemplateColumn = gridColumn as GridTemplateColumn;
				if (gridTemplateColumn != null && gridTemplateColumn.Aggregate != GridAggregateFunction.None && !dataTable.Columns.Contains(gridTemplateColumn.DataField))
				{
					dataTable.Columns.Add(gridTemplateColumn.DataField, gridTemplateColumn.DataType);
				}
				GridCalculatedColumn gridCalculatedColumn = gridColumn as GridCalculatedColumn;
				if (gridCalculatedColumn != null && gridCalculatedColumn.Aggregate != GridAggregateFunction.None && !dataTable.Columns.Contains(string.Format("{0}Result", gridColumn.UniqueName)))
				{
					dataTable.Columns.Add(string.Format("{0}Result", gridColumn.UniqueName), gridCalculatedColumn.DataType);
				}
			}
			dataTable.Columns.Add(new GridDataColumn("SplitGroup", typeof(object)));
			return dataTable;
		}

		// Token: 0x0600AF68 RID: 44904 RVA: 0x0025FB80 File Offset: 0x0025DD80
		private bool IsSameRow(DataRow row1, DataRow row2, bool caseSensitive)
		{
			bool flag = true;
			if (row2 != null)
			{
				foreach (object obj in this.groupGroupByFields)
				{
					GridGroupByField gridGroupByField = (GridGroupByField)obj;
					flag = GridDataSetHelper.ColumnsEqual(row2[gridGroupByField.FieldName], row1[gridGroupByField.FieldName], caseSensitive);
					if (!flag)
					{
						break;
					}
				}
			}
			return flag;
		}

		// Token: 0x0600AF69 RID: 44905 RVA: 0x0025FBFC File Offset: 0x0025DDFC
		private void CalculateAverageAggregates(DataRow sourceRow, DataRow destRow, int rowCountInGroup, GridTableView view)
		{
			if (destRow == null)
			{
				return;
			}
			bool flag = false;
			foreach (object obj in this.groupSelectFields)
			{
				GridGroupByField gridGroupByField = (GridGroupByField)obj;
				GridAggregateFunction aggregate = gridGroupByField.Aggregate;
				if (aggregate == GridAggregateFunction.Avg)
				{
					if (rowCountInGroup > 0)
					{
						if (gridGroupByField.FieldAlias != gridGroupByField.FieldName)
						{
							destRow[gridGroupByField.FieldAlias] = this.Avg(sourceRow[gridGroupByField.FieldAlias], rowCountInGroup);
						}
						else
						{
							destRow[gridGroupByField.FieldAlias] = this.Avg(sourceRow[gridGroupByField.FieldName], rowCountInGroup);
						}
					}
					flag = true;
				}
			}
			if (flag)
			{
				foreach (GridColumn gridColumn in view.RenderColumns)
				{
					GridBoundColumn gridBoundColumn = gridColumn as GridBoundColumn;
					if (gridBoundColumn != null && gridBoundColumn.Aggregate != GridAggregateFunction.None)
					{
						string dataField = gridBoundColumn.DataField;
						destRow[dataField] = sourceRow[dataField];
					}
					GridCalculatedColumn gridCalculatedColumn = gridColumn as GridCalculatedColumn;
					if (gridCalculatedColumn != null && gridCalculatedColumn.Aggregate != GridAggregateFunction.None)
					{
						string text = string.Format("{0}Result", gridColumn.UniqueName);
						if (sourceRow.Table.Columns.Contains(text))
						{
							destRow[text] = sourceRow[text];
						}
					}
				}
			}
		}

		// Token: 0x0600AF6A RID: 44906 RVA: 0x0025FD64 File Offset: 0x0025DF64
		private void CalculateAggregates(DataRow SourceRow, DataRow DestRow, int rowCountInGroup, GridTableView view, ref bool changeRowCount)
		{
			foreach (object obj in this.groupSelectFields)
			{
				GridGroupByField gridGroupByField = (GridGroupByField)obj;
				switch (gridGroupByField.Aggregate)
				{
				case GridAggregateFunction.None:
				case GridAggregateFunction.Last:
					DestRow[gridGroupByField.FieldAlias] = SourceRow[gridGroupByField.FieldName];
					break;
				case GridAggregateFunction.Sum:
				case GridAggregateFunction.Avg:
					DestRow[gridGroupByField.FieldAlias] = this.Add(DestRow[gridGroupByField.FieldAlias], SourceRow[gridGroupByField.FieldName]);
					break;
				case GridAggregateFunction.Min:
					if (rowCountInGroup == 1)
					{
						DestRow[gridGroupByField.FieldAlias] = SourceRow[gridGroupByField.FieldName];
					}
					else
					{
						DestRow[gridGroupByField.FieldAlias] = this.Min(DestRow[gridGroupByField.FieldAlias], SourceRow[gridGroupByField.FieldName]);
					}
					break;
				case GridAggregateFunction.Max:
					DestRow[gridGroupByField.FieldAlias] = this.Max(DestRow[gridGroupByField.FieldAlias], SourceRow[gridGroupByField.FieldName]);
					break;
				case GridAggregateFunction.First:
					if (rowCountInGroup == 1)
					{
						changeRowCount = false;
						DestRow[gridGroupByField.FieldAlias] = SourceRow[gridGroupByField.FieldName];
					}
					break;
				case GridAggregateFunction.Count:
					DestRow[gridGroupByField.FieldAlias] = rowCountInGroup;
					break;
				}
			}
			foreach (GridColumn gridColumn in view.RenderColumns)
			{
				GridBoundColumn gridBoundColumn = gridColumn as GridBoundColumn;
				if (gridBoundColumn != null && gridBoundColumn.Aggregate != GridAggregateFunction.None)
				{
					string dataField = gridBoundColumn.DataField;
					if (!this.IsFieldAlias(dataField))
					{
						DestRow[dataField] = SourceRow[dataField];
					}
				}
				GridCalculatedColumn gridCalculatedColumn = gridColumn as GridCalculatedColumn;
				if (gridCalculatedColumn != null && gridCalculatedColumn.Aggregate != GridAggregateFunction.None)
				{
					string text = string.Format("{0}Result", gridColumn.UniqueName);
					if (SourceRow.Table.Columns.Contains(text))
					{
						DestRow[text] = SourceRow[text];
					}
				}
			}
		}

		// Token: 0x0600AF6B RID: 44907 RVA: 0x0025FFA4 File Offset: 0x0025E1A4
		private bool IsFieldAlias(string dataField)
		{
			bool result = false;
			foreach (object obj in this.groupSelectFields)
			{
				GridGroupByField gridGroupByField = (GridGroupByField)obj;
				if (gridGroupByField.FieldAlias == dataField)
				{
					result = true;
					break;
				}
			}
			return result;
		}

		// Token: 0x0600AF6C RID: 44908 RVA: 0x0026000C File Offset: 0x0025E20C
		private void InsertGroupByInto(GridTableView gridTableView, DataTable DestTable, DataTable SourceTable, string RowFilter, int FirstIndexInPage, int LastIndexInPage, bool ApplyPaging, bool IsCustomPaging)
		{
			string groupingSortString = this.sortExpressions.GetGroupingSortString();
			this._resultSourceTable = GridDataSetHelper.CloneTableStructure(SourceTable);
			DataRow[] array = SourceTable.Select(RowFilter, groupingSortString);
			if (IsCustomPaging && (gridTableView.BoundUsingDataSourceID || gridTableView.OwnerGrid.isBoundUsingNeedDataSource))
			{
				ApplyPaging = true;
			}
			int num;
			int num2;
			if (ApplyPaging)
			{
				num = FirstIndexInPage;
				num2 = LastIndexInPage;
				if (IsCustomPaging && gridTableView.OwnerGrid.isBoundUsingNeedDataSource && FirstIndexInPage == 0)
				{
					num = FirstIndexInPage;
					num2 = Math.Min(LastIndexInPage, gridTableView.PageSize);
				}
			}
			else
			{
				num = 0;
				num2 = Math.Min(array.Length - 1, gridTableView.PageSize);
			}
			if (array.Length - 1 < num2)
			{
				num2 = array.Length - 1;
			}
			if (num > array.Length - 1)
			{
				return;
			}
			DataRow dataRow = array[num];
			int num3 = 1;
			DataRow dataRow2 = DestTable.NewRow();
			bool flag = true;
			int num4 = num3;
			if (ApplyPaging || IsCustomPaging)
			{
				int i = num - 1;
				while (i >= 0)
				{
					DataRow dataRow3 = array[i];
					if (!this.IsSameRow(dataRow3, dataRow, SourceTable.CaseSensitive))
					{
						if (i != num - 1)
						{
							dataRow2["SplitGroup"] = new GridSplitGroup
							{
								Mode = GridGroupSplitMode.Continued
							};
							break;
						}
						break;
					}
					else
					{
						this.CalculateAggregates(dataRow3, dataRow2, num3, gridTableView, ref flag);
						dataRow = dataRow3;
						if (flag)
						{
							num3++;
						}
						num4++;
						if (i == 0)
						{
							dataRow2["SplitGroup"] = new GridSplitGroup
							{
								Mode = GridGroupSplitMode.Continued
							};
						}
						i--;
					}
				}
				if (num4 != num3)
				{
					num3 = num4;
				}
			}
			if (num == num2)
			{
				object obj = dataRow2["SplitGroup"];
				if (obj != DBNull.Value)
				{
					GridSplitGroup gridSplitGroup = (GridSplitGroup)obj;
					gridSplitGroup.GroupItemsCount = num3;
					gridSplitGroup.ActualItemCount = 1;
				}
			}
			dataRow = array[num];
			if (ApplyPaging || IsCustomPaging)
			{
				this._resultSourceTable.LoadDataRow(dataRow.ItemArray, true);
			}
			this.CalculateAggregates(dataRow, dataRow2, num3, gridTableView, ref flag);
			int num5 = 0;
			int num6 = 0;
			int j;
			for (j = num + 1; j <= num2; j++)
			{
				num5++;
				num3++;
				DataRow dataRow4 = array[j];
				if (!this.IsSameRow(dataRow4, dataRow, SourceTable.CaseSensitive))
				{
					object obj2 = dataRow2["SplitGroup"];
					if (obj2 != DBNull.Value)
					{
						GridSplitGroup gridSplitGroup2 = (GridSplitGroup)obj2;
						gridSplitGroup2.GroupItemsCount = num3 - 1;
						gridSplitGroup2.ActualItemCount = num5;
					}
					this.CalculateAverageAggregates(dataRow2, dataRow2, num3 - 1, gridTableView);
					DestTable.Rows.Add(dataRow2);
					dataRow2 = DestTable.NewRow();
					num3 = 1;
					num5 = 0;
				}
				else if (j == array.Length - 1)
				{
					object obj3 = dataRow2["SplitGroup"];
					GridSplitGroup gridSplitGroup3;
					if (obj3 == DBNull.Value)
					{
						gridSplitGroup3 = new GridSplitGroup();
						if (num5 + 1 > num3)
						{
							dataRow2["SplitGroup"] = gridSplitGroup3;
						}
					}
					else
					{
						gridSplitGroup3 = (GridSplitGroup)obj3;
					}
					gridSplitGroup3.ActualItemCount = num5 + 1;
					gridSplitGroup3.Mode = GridGroupSplitMode.Continued;
					gridSplitGroup3.GroupItemsCount = num3;
				}
				this.CalculateAggregates(dataRow4, dataRow2, num3, gridTableView, ref flag);
				if (j == num2)
				{
					num6 = num3;
				}
				dataRow = dataRow4;
				if (ApplyPaging || IsCustomPaging)
				{
					this._resultSourceTable.LoadDataRow(dataRow4.ItemArray, true);
				}
			}
			num3++;
			bool flag2 = true;
			if (!ApplyPaging)
			{
				if (!IsCustomPaging)
				{
					goto IL_494;
				}
			}
			while (j < array.Length)
			{
				DataRow dataRow5 = array[j];
				if (!this.IsSameRow(dataRow5, dataRow, SourceTable.CaseSensitive))
				{
					object obj4 = dataRow2["SplitGroup"];
					if (obj4 != DBNull.Value)
					{
						GridSplitGroup gridSplitGroup4 = (GridSplitGroup)obj4;
						gridSplitGroup4.GroupItemsCount = num3 - 1;
						gridSplitGroup4.ActualItemCount = num5 + 1;
						break;
					}
					break;
				}
				else
				{
					if (j == array.Length - 1)
					{
						object obj5 = dataRow2["SplitGroup"];
						GridSplitGroup gridSplitGroup5;
						if (obj5 == DBNull.Value)
						{
							gridSplitGroup5 = new GridSplitGroup();
							dataRow2["SplitGroup"] = gridSplitGroup5;
							gridSplitGroup5.Mode = GridGroupSplitMode.Continues;
						}
						else
						{
							gridSplitGroup5 = (GridSplitGroup)obj5;
							if (gridSplitGroup5.Mode == GridGroupSplitMode.Continued)
							{
								gridSplitGroup5.Mode = GridGroupSplitMode.Both;
							}
						}
						gridSplitGroup5.ActualItemCount = num5 + 1;
						gridSplitGroup5.GroupItemsCount = num3;
					}
					else
					{
						if (flag2)
						{
							object obj6 = dataRow2["SplitGroup"];
							if (obj6 == DBNull.Value)
							{
								dataRow2["SplitGroup"] = new GridSplitGroup
								{
									ActualItemCount = num5 + 1,
									Mode = GridGroupSplitMode.Continues
								};
							}
							else
							{
								GridSplitGroup gridSplitGroup6 = (GridSplitGroup)obj6;
								gridSplitGroup6.ActualItemCount = num5 + 1;
								gridSplitGroup6.Mode = GridGroupSplitMode.Both;
								dataRow2["SplitGroup"] = gridSplitGroup6;
							}
						}
						flag2 = false;
					}
					this.CalculateAggregates(dataRow5, dataRow2, num3, gridTableView, ref flag);
					dataRow = dataRow5;
					num3++;
					j++;
				}
			}
			IL_494:
			if (num6 < num3)
			{
				this.CalculateAverageAggregates(dataRow2, dataRow2, num3 - 1, gridTableView);
			}
			else
			{
				this.CalculateAverageAggregates(dataRow2, dataRow2, num3 - 1, gridTableView);
			}
			DestTable.Rows.Add(dataRow2);
		}

		// Token: 0x0600AF6D RID: 44909 RVA: 0x002604E0 File Offset: 0x0025E6E0
		[SuppressMessage("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
		private static bool ColumnsEqual(object a, object b, bool caseSensitive)
		{
			if (a is DBNull && b is DBNull)
			{
				return true;
			}
			if (a is DBNull || b is DBNull)
			{
				return false;
			}
			if (!a.GetType().IsInstanceOfType(b) && !b.GetType().IsInstanceOfType(a))
			{
				return a.Equals(b);
			}
			if (caseSensitive)
			{
				return Comparer.Default.Compare(a, b) == 0;
			}
			return CaseInsensitiveComparer.Default.Compare(a, b) == 0;
		}

		// Token: 0x0600AF6E RID: 44910 RVA: 0x00260557 File Offset: 0x0025E757
		private object Min(object a, object b)
		{
			if (a is DBNull || b is DBNull)
			{
				return DBNull.Value;
			}
			if (((IComparable)a).CompareTo(b) == -1)
			{
				return a;
			}
			return b;
		}

		// Token: 0x0600AF6F RID: 44911 RVA: 0x00260581 File Offset: 0x0025E781
		private object Max(object a, object b)
		{
			if (a is DBNull)
			{
				return b;
			}
			if (b is DBNull)
			{
				return a;
			}
			if (((IComparable)a).CompareTo(b) == 1)
			{
				return a;
			}
			return b;
		}

		// Token: 0x0600AF70 RID: 44912 RVA: 0x002605AC File Offset: 0x0025E7AC
		private object Avg(object sum, int rowCount)
		{
			if (sum != null && !(sum is DBNull))
			{
				try
				{
					return decimal.Parse(sum.ToString()) / decimal.Parse(rowCount.ToString());
				}
				catch
				{
				}
			}
			return DBNull.Value;
		}

		// Token: 0x0600AF71 RID: 44913 RVA: 0x00260604 File Offset: 0x0025E804
		private object Avg(object a, object b, int rowCount)
		{
			object obj = this.Add(a, b);
			if (obj != null && !(obj is DBNull))
			{
				try
				{
					return decimal.Parse(obj.ToString()) / decimal.Parse(rowCount.ToString());
				}
				catch
				{
				}
			}
			return DBNull.Value;
		}

		// Token: 0x0600AF72 RID: 44914 RVA: 0x00260664 File Offset: 0x0025E864
		private object Add(object a, object b)
		{
			if (a is DBNull)
			{
				return b;
			}
			if (b is DBNull)
			{
				return a;
			}
			return decimal.Parse(a.ToString()) + decimal.Parse(b.ToString());
		}

		// Token: 0x0600AF73 RID: 44915 RVA: 0x0026069C File Offset: 0x0025E89C
		public void CalcGroupByTables(GridTableView gridTableView, DataTable SourceTable, string RowFilter, int FirstIndex, int LastIndex, bool applyPaging, bool isCustomPaging)
		{
			this.GetGroupingFields();
			this.AddSelectFieldsForGroupByFields();
			DataTable dataTable = this.CreateGroupByTable(SourceTable, gridTableView);
			this.InsertGroupByInto(gridTableView, dataTable, SourceTable, RowFilter, FirstIndex, LastIndex, applyPaging, isCustomPaging);
			this._resultGroupTable = dataTable;
		}

		// Token: 0x0600AF74 RID: 44916 RVA: 0x002606D8 File Offset: 0x0025E8D8
		private void AddSelectFieldsForGroupByFields()
		{
			foreach (object obj in this.groupGroupByFields)
			{
				GridGroupByField gridGroupByField = (GridGroupByField)obj;
				GridGroupByField gridGroupByField2 = null;
				foreach (object obj2 in this.groupSelectFields)
				{
					GridGroupByField gridGroupByField3 = (GridGroupByField)obj2;
					if (gridGroupByField3.FieldName == gridGroupByField.FieldName && gridGroupByField3.Aggregate == GridAggregateFunction.None)
					{
						gridGroupByField2 = gridGroupByField3;
						break;
					}
				}
				if (gridGroupByField2 == null)
				{
					this.groupSelectFields.Add(gridGroupByField);
				}
				else
				{
					gridGroupByField.FieldAlias = gridGroupByField2.FieldAlias;
				}
			}
		}

		// Token: 0x0600AF75 RID: 44917 RVA: 0x002607BC File Offset: 0x0025E9BC
		private void GetGroupingFields()
		{
			this.groupGroupByFields = new ArrayList();
			this.groupSelectFields = new ArrayList();
			int num = 0;
			foreach (GridGroupByExpression gridGroupByExpression in this.groupByExpressions)
			{
				if (num == this.groupByExpressions.Count - 1)
				{
					this.groupGroupByFields.AddRange(gridGroupByExpression.GroupByFields);
					this.groupSelectFields.AddRange(gridGroupByExpression.SelectFields);
				}
				else
				{
					this.groupGroupByFields.AddRange(gridGroupByExpression.GroupByFields);
				}
				num++;
			}
		}

		// Token: 0x170038A4 RID: 14500
		// (get) Token: 0x0600AF76 RID: 44918 RVA: 0x0026086C File Offset: 0x0025EA6C
		public DataTable ResultGroupTable
		{
			get
			{
				return this._resultGroupTable;
			}
		}

		// Token: 0x170038A5 RID: 14501
		// (get) Token: 0x0600AF77 RID: 44919 RVA: 0x00260874 File Offset: 0x0025EA74
		public DataTable ResultSourceTable
		{
			get
			{
				return this._resultSourceTable;
			}
		}

		// Token: 0x04002E3A RID: 11834
		private GridGroupByExpressionCollection groupByExpressions;

		// Token: 0x04002E3B RID: 11835
		private GridSortExpressionCollection sortExpressions;

		// Token: 0x04002E3C RID: 11836
		private ArrayList groupGroupByFields;

		// Token: 0x04002E3D RID: 11837
		private ArrayList groupSelectFields;

		// Token: 0x04002E3E RID: 11838
		private DataTable _resultGroupTable;

		// Token: 0x04002E3F RID: 11839
		private DataTable _resultSourceTable;
	}
}
