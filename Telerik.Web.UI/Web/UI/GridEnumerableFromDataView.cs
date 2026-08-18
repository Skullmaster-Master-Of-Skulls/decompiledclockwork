using System;
using System.Collections;
using System.Collections.Specialized;
using System.Data;
using System.Diagnostics.CodeAnalysis;

namespace Telerik.Web.UI
{
	// Token: 0x020010F5 RID: 4341
	[SuppressMessage("Microsoft.Design", "CA1001:TypesThatOwnDisposableFieldsShouldBeDisposable")]
	internal class GridEnumerableFromDataView : GridEnumerableBase
	{
		// Token: 0x0600B1D4 RID: 45524 RVA: 0x00269B54 File Offset: 0x00267D54
		public GridEnumerableFromDataView(GridTableView owner, IEnumerable enumerable, bool CaseSensitive, bool autoGenerateColumns, GridColumnCollection presentColumns, string[] additionalField, bool retrieveAllFields, bool enableSplitHeaderText)
		{
			GridDataTableFromEnumerable gridDataTableFromEnumerable = new GridDataTableFromEnumerable(owner, enumerable, autoGenerateColumns, true, presentColumns, additionalField, retrieveAllFields, enableSplitHeaderText);
			DataTable dataTable = gridDataTableFromEnumerable.DataTable;
			dataTable.CaseSensitive = CaseSensitive;
			if (owner.OwnerGrid.EnableLinqExpressions)
			{
				this._dataView = dataTable.AsDataView();
			}
			else
			{
				this._dataView = dataTable.DefaultView;
			}
			this.generatedColumns = gridDataTableFromEnumerable.Columns;
			this._objectForInsertion = gridDataTableFromEnumerable.GetNewInsertionObject();
			this.gridTableView = owner;
		}

		// Token: 0x0600B1D5 RID: 45525 RVA: 0x00269BF8 File Offset: 0x00267DF8
		public GridEnumerableFromDataView(GridTableView owner, DataView dataView, bool autoGenerateColumns, GridColumnCollection presentColumns, string[] additionalField, bool retrieveAllFields, bool enableSplitHeaderText)
		{
			GridDataTableFromEnumerable gridDataTableFromEnumerable = new GridDataTableFromEnumerable(owner, dataView, autoGenerateColumns, true, presentColumns, additionalField, retrieveAllFields, enableSplitHeaderText);
			DataTable dataTable = gridDataTableFromEnumerable.DataTable;
			dataTable.CaseSensitive = dataView.Table.CaseSensitive;
			if (owner.OwnerGrid.EnableLinqExpressions)
			{
				this._dataView = dataTable.AsDataView();
			}
			else
			{
				this._dataView = dataTable.DefaultView;
			}
			this.generatedColumns = gridDataTableFromEnumerable.Columns;
			this._objectForInsertion = gridDataTableFromEnumerable.GetNewInsertionObject();
			this.gridTableView = owner;
		}

		// Token: 0x0600B1D6 RID: 45526 RVA: 0x00269CA3 File Offset: 0x00267EA3
		public override GridInsertionObject GetObjectForInsertion(IDictionary values)
		{
			if (values != null)
			{
				this._objectForInsertion.SetupValues(values);
			}
			return this._objectForInsertion;
		}

		// Token: 0x0600B1D7 RID: 45527 RVA: 0x00269CBA File Offset: 0x00267EBA
		public override ArrayList GetColumns()
		{
			return this.generatedColumns;
		}

		// Token: 0x17003993 RID: 14739
		// (get) Token: 0x0600B1D8 RID: 45528 RVA: 0x00269CC2 File Offset: 0x00267EC2
		public override bool IsReady
		{
			get
			{
				return this._isReady;
			}
		}

		// Token: 0x17003994 RID: 14740
		// (get) Token: 0x0600B1D9 RID: 45529 RVA: 0x00269CCA File Offset: 0x00267ECA
		public override bool SupportsPaging
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17003995 RID: 14741
		// (get) Token: 0x0600B1DA RID: 45530 RVA: 0x00269CCD File Offset: 0x00267ECD
		public override bool SupportsSorting
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17003996 RID: 14742
		// (get) Token: 0x0600B1DB RID: 45531 RVA: 0x00269CD0 File Offset: 0x00267ED0
		public override bool SupportsGrouping
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17003997 RID: 14743
		// (get) Token: 0x0600B1DC RID: 45532 RVA: 0x00269CD3 File Offset: 0x00267ED3
		public override bool SupportsFiltering
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600B1DD RID: 45533 RVA: 0x00269CD6 File Offset: 0x00267ED6
		public override void SetGroupByExpressions(GridGroupByExpressionCollection expressions)
		{
			this._groupByExpressions = expressions;
		}

		// Token: 0x17003998 RID: 14744
		// (get) Token: 0x0600B1DE RID: 45534 RVA: 0x00269CE0 File Offset: 0x00267EE0
		public override int Count
		{
			get
			{
				this.TransformEnumerable();
				int num = 0;
				if (base.GroupingEnabled)
				{
					num = ((this.GroupByExpressions != null) ? this.GroupByExpressions.Count : 0);
				}
				DataTable dataTable = this.dsGroups.Tables["GroupedTable" + num];
				if (dataTable != null)
				{
					return dataTable.Rows.Count;
				}
				return 0;
			}
		}

		// Token: 0x17003999 RID: 14745
		// (get) Token: 0x0600B1DF RID: 45535 RVA: 0x00269D45 File Offset: 0x00267F45
		public override int DataSourceCount
		{
			get
			{
				if (this._dsCount == -1)
				{
					throw new GridException("DataSourceCount is not ready at this moment");
				}
				return this._dsCount;
			}
		}

		// Token: 0x1700399A RID: 14746
		// (get) Token: 0x0600B1E0 RID: 45536 RVA: 0x00269D61 File Offset: 0x00267F61
		public GridGroupByExpressionCollection GroupByExpressions
		{
			get
			{
				return this._groupByExpressions;
			}
		}

		// Token: 0x1700399B RID: 14747
		// (get) Token: 0x0600B1E1 RID: 45537 RVA: 0x00269D69 File Offset: 0x00267F69
		public GridSortExpressionCollection SortExpressions
		{
			get
			{
				if (this._sortExpressions == null)
				{
					this._sortExpressions = new GridSortExpressionCollection();
				}
				return this._sortExpressions;
			}
		}

		// Token: 0x0600B1E2 RID: 45538 RVA: 0x00269D84 File Offset: 0x00267F84
		public override void SetSortExpressions(GridSortExpressionCollection expressions)
		{
			this._sortExpressions = expressions;
		}

		// Token: 0x0600B1E3 RID: 45539 RVA: 0x00269D8D File Offset: 0x00267F8D
		public override void AddHierarchyFilterExpression(string expression)
		{
			if (!string.IsNullOrEmpty(expression))
			{
				this._hierarchyFilterExpressions.Add(expression);
			}
		}

		// Token: 0x0600B1E4 RID: 45540 RVA: 0x00269DA4 File Offset: 0x00267FA4
		public override void AddFilterExpression(string expression)
		{
			if (!string.IsNullOrEmpty(expression))
			{
				this._filterExpressions.Add(expression);
			}
		}

		// Token: 0x1700399C RID: 14748
		// (get) Token: 0x0600B1E5 RID: 45541 RVA: 0x00269DBB File Offset: 0x00267FBB
		public override DataSet GroupingDataSet
		{
			get
			{
				return this.dsGroups;
			}
		}

		// Token: 0x0600B1E6 RID: 45542 RVA: 0x00269DC4 File Offset: 0x00267FC4
		private void AddRelation(ArrayList fieldInfoList, ArrayList groupByFieldInfoList, DataTable parentTable, DataTable childTable, bool mainTable)
		{
			ArrayList arrayList = new ArrayList();
			ArrayList arrayList2 = new ArrayList();
			foreach (object obj in groupByFieldInfoList)
			{
				GridGroupByField gridGroupByField = (GridGroupByField)obj;
				GridGroupByField gridGroupByField2 = null;
				foreach (object obj2 in fieldInfoList)
				{
					GridGroupByField gridGroupByField3 = (GridGroupByField)obj2;
					if (gridGroupByField3.FieldName == gridGroupByField.FieldName && gridGroupByField3.Aggregate == GridAggregateFunction.None)
					{
						gridGroupByField2 = gridGroupByField3;
						break;
					}
				}
				arrayList.Add(parentTable.Columns[gridGroupByField2.FieldAlias]);
				DataColumn value;
				if (mainTable)
				{
					value = childTable.Columns[gridGroupByField.FieldName];
				}
				else
				{
					value = childTable.Columns[gridGroupByField.FieldAlias];
				}
				arrayList2.Add(value);
			}
			DataColumn[] array = new DataColumn[arrayList.Count];
			arrayList.CopyTo(array);
			DataColumn[] array2 = new DataColumn[arrayList2.Count];
			arrayList2.CopyTo(array2);
			try
			{
				this.GroupingDataSet.Relations.Add(parentTable.TableName + childTable.TableName, array, array2);
			}
			catch (InvalidConstraintException)
			{
				throw new GridGroupByException("An error occured adding a relation to DataRelationCollection. Please, make sure you have configured the expressions properly - both GroupByFields and SelectFields are required!");
			}
		}

		// Token: 0x0600B1E7 RID: 45543 RVA: 0x00269F50 File Offset: 0x00268150
		private void PerformTransformation()
		{
			this.dsGroups.Clear();
			DataTable dataTable = this._dataView.Table;
			bool flag = this.gridTableView.CurrentDataSource != null && this.gridTableView.CurrentDataSource.GetType().GetInterface("IDataReader") != null;
			if (this._dataView.Count == 0)
			{
				this._dsCount = 0;
				this._isReady = true;
				dataTable.TableName = "GroupedTable0";
				this.GroupingDataSet.Tables.Add(dataTable);
				return;
			}
			string text = "";
			text = this.GetFullFilterExpression();
			if (!string.IsNullOrEmpty(this._dataView.RowFilter))
			{
				text = this._dataView.RowFilter;
			}
			else
			{
				try
				{
					string fullFilterExpression = this.GetFullFilterExpression();
					if (this._dataView.RowFilter != fullFilterExpression)
					{
						this._dataView.RowFilter = fullFilterExpression;
					}
				}
				catch (Exception)
				{
					string hierarchyFilterExpression = this.GetHierarchyFilterExpression();
					if (this._dataView.RowFilter != hierarchyFilterExpression)
					{
						this._dataView.RowFilter = hierarchyFilterExpression;
					}
					text = hierarchyFilterExpression;
				}
			}
			this._dsCount = this._dataView.Count;
			string.IsNullOrEmpty(this._dataView.RowFilter);
			if (this._dataView.Table.ExtendedProperties["rowsCount"] != null)
			{
				this._dsCount = (int)this._dataView.Table.ExtendedProperties["rowsCount"];
			}
			if (this.gridTableView.OwnerGrid.EnableLinqExpressions && this.gridTableView.EnableLinqGrouping && this.gridTableView.GroupByExpressions.Count > 0 && string.IsNullOrEmpty(this.gridTableView.OwnerGrid.ClientDataSourceID) && this.gridTableView._shouldUseLinqGrouping)
			{
				this._dsCount = this.gridTableView.itemsCountWhenGrouping;
			}
			if ((this.Paging.IsPagingEnabled && this.Paging.CurrentPageIndex < 0) || this.Paging.CurrentPageIndex >= this.Paging.PageCount)
			{
				if (this.CurrentResetPageIndexAction == GridResetPageIndexAction.SetPageIndexToFirst)
				{
					this.Paging.setCurrentPageIndex(0);
				}
				else
				{
					if (this.CurrentResetPageIndexAction != GridResetPageIndexAction.SetPageIndexToLast)
					{
						throw new InvalidOperationException(string.Format("Page number: {0} is greater than the total number of pages.", this.Paging.CurrentPageIndex + 1));
					}
					this.Paging.setCurrentPageIndex(this.Paging.PageCount - 1);
				}
			}
			if (!base.GroupingEnabled || this.GroupByExpressions == null || this.GroupByExpressions.Count <= 0 || !string.IsNullOrEmpty(this.gridTableView.OwnerGrid.ClientDataSourceID))
			{
				string text2 = this.SortExpressions.GetSortString();
				if (string.IsNullOrEmpty(text2))
				{
					text2 = this._dataView.Sort;
				}
				dataTable = GridDataSetHelper.CloneTableStructure(this._dataView.Table);
				DataView dataView = new DataView(this._dataView.Table);
				if (!base.IsDesignMode)
				{
					if (dataView.RowFilter != text)
					{
						dataView.RowFilter = text;
					}
					if (!this.gridTableView.OwnerGrid.EnableLinqExpressions || flag || this.gridTableView.IsBoundToForwardOnly)
					{
						dataView.Sort = text2;
					}
				}
				int num = this.Paging.FirstIndexInPage;
				int count = dataView.Count;
				int num2 = Math.Min(this.Paging.LastIndexInPage, count);
				bool flag2 = !this.gridTableView.BoundUsingDataSourceID || !this.gridTableView.OverrideDataSourceControlSorting || this.gridTableView.SortExpressions.Count == 0 || !this.gridTableView.CanRetrieveAllData || this.gridTableView.OwnerGrid.EnableLinqExpressions;
				if ((this.Paging.IsCustomPagingEnabled || (this._dataView.Table.ExtendedProperties["rowsCount"] != null && this.gridTableView.AllowPaging)) && flag2)
				{
					num = 0;
					num2 = Math.Min(this.Paging.PageSize, dataView.Count) - 1;
				}
				if (this.gridTableView.OwnerGrid.ClientSettings.Virtualization.EnableVirtualization && this.gridTableView.OwnerGrid.ClientSettings.Virtualization.StartIndex > 0 && this.gridTableView.OwnerGrid.ClientSettings.Virtualization.StartIndex < dataView.Count)
				{
					num = this.gridTableView.OwnerGrid.ClientSettings.Virtualization.StartIndex;
					num2 = this.gridTableView.OwnerGrid.ClientSettings.Virtualization.StartIndex + this.gridTableView.OwnerGrid.ClientSettings.Virtualization.ItemsPerView;
				}
				int num3 = num;
				while (num3 <= num2 && num3 < dataView.Count)
				{
					dataTable.LoadDataRow(dataView[num3].Row.ItemArray, true);
					num3++;
				}
				dataTable.TableName = "GroupedTable0";
				this.GroupingDataSet.Tables.Add(dataTable);
				this._isReady = true;
				return;
			}
			this.GroupingDataSet.CaseSensitive = dataTable.CaseSensitive;
			if (dataTable.Columns.Count == 0)
			{
				return;
			}
			GridSortExpressionCollection gridSortExpressionCollection = new GridSortExpressionCollection();
			gridSortExpressionCollection.AllowMultiColumnSorting = true;
			foreach (GridGroupByExpression gridGroupByExpression in this.GroupByExpressions)
			{
				foreach (object obj in gridGroupByExpression.GroupByFields)
				{
					GridGroupByField gridGroupByField = (GridGroupByField)obj;
					gridSortExpressionCollection.Add(new GridSortExpression
					{
						FieldName = gridGroupByField.FieldName,
						SortOrder = gridGroupByField.SortOrder
					});
				}
			}
			foreach (object obj2 in this.SortExpressions)
			{
				GridSortExpression gridSortExpression = (GridSortExpression)obj2;
				if (!gridSortExpressionCollection.ContainsExpression(gridSortExpression.FieldName))
				{
					gridSortExpressionCollection.Add(gridSortExpression);
				}
			}
			GridGroupByExpressionCollection gridGroupByExpressionCollection = new GridGroupByExpressionCollection();
			ArrayList arrayList = null;
			bool applyPaging = true;
			if (this.Paging.IsCustomPagingEnabled)
			{
				applyPaging = false;
			}
			foreach (GridGroupByExpression gridGroupByExpression2 in this.GroupByExpressions)
			{
				GridGroupByExpression gridGroupByExpression3 = gridGroupByExpression2.Clone();
				if (arrayList != null)
				{
					foreach (object obj3 in arrayList)
					{
						GridGroupByField value = (GridGroupByField)obj3;
						gridGroupByExpression3.GroupByFields.Add(value);
					}
				}
				GridDataSetHelper gridDataSetHelper = new GridDataSetHelper(new GridGroupByExpressionCollection
				{
					gridGroupByExpression3
				}, gridSortExpressionCollection);
				int firstIndex = Math.Min(this.Paging.FirstIndexInPage, this._dsCount);
				if (this.Paging.IsCustomPagingEnabled && this.Paging.FirstIndexInPage >= this._dsCount)
				{
					firstIndex = 0;
				}
				gridDataSetHelper.CalcGroupByTables(this.gridTableView, this._dataView.Table, text, firstIndex, this.Paging.LastIndexInPage, applyPaging, this.Paging.IsCustomPagingEnabled);
				DataTable resultGroupTable = gridDataSetHelper.ResultGroupTable;
				resultGroupTable.TableName = "GroupedTable" + gridGroupByExpression2.Index.ToString();
				this.dsGroups.Tables.Add(resultGroupTable);
				if (gridGroupByExpression2.Index > 0)
				{
					this.AddRelation(gridDataSetHelper.FieldInfoList, arrayList, this.dsGroups.Tables["GroupedTable" + (gridGroupByExpression2.Index - 1).ToString()], resultGroupTable, false);
				}
				else
				{
					dataTable = gridDataSetHelper.ResultSourceTable;
					dataTable.TableName = "GroupedTable" + this.GroupByExpressions.Count.ToString();
					this.GroupingDataSet.Tables.Add(dataTable);
				}
				if (gridGroupByExpression2.Index == this.GroupByExpressions.Count - 1)
				{
					this.AddRelation(gridDataSetHelper.FieldInfoList, gridDataSetHelper.GroupByFieldInfoList, resultGroupTable, dataTable, true);
				}
				arrayList = gridDataSetHelper.GroupByFieldInfoList;
			}
			this._isReady = true;
		}

		// Token: 0x0600B1E8 RID: 45544 RVA: 0x0026A868 File Offset: 0x00268A68
		public override void TransformEnumerable()
		{
			if (this.enumerableTransformed)
			{
				return;
			}
			this.PerformTransformation();
			this.enumerableTransformed = true;
		}

		// Token: 0x0600B1E9 RID: 45545 RVA: 0x0026A880 File Offset: 0x00268A80
		private string GetHierarchyFilterExpression()
		{
			string text = "";
			int num = 0;
			foreach (string str in this._hierarchyFilterExpressions)
			{
				if (num > 0)
				{
					text += " AND ";
				}
				text = text + "(" + str + ")";
				num++;
			}
			if (!base.IsDesignMode)
			{
				return text;
			}
			return string.Empty;
		}

		// Token: 0x0600B1EA RID: 45546 RVA: 0x0026A910 File Offset: 0x00268B10
		private string GetFullFilterExpression()
		{
			string text = "";
			int num = 0;
			foreach (string str in this._hierarchyFilterExpressions)
			{
				if (num > 0)
				{
					text += " AND ";
				}
				text = text + "(" + str + ")";
				num++;
			}
			foreach (string str2 in this._filterExpressions)
			{
				if (num > 0)
				{
					text += " AND ";
				}
				text = text + "(" + str2 + ")";
				num++;
			}
			if (!base.IsDesignMode)
			{
				return text;
			}
			return string.Empty;
		}

		// Token: 0x04002EA6 RID: 11942
		internal DataView _dataView;

		// Token: 0x04002EA7 RID: 11943
		private GridSortExpressionCollection _sortExpressions;

		// Token: 0x04002EA8 RID: 11944
		private GridGroupByExpressionCollection _groupByExpressions;

		// Token: 0x04002EA9 RID: 11945
		private DataSet dsGroups = new DataSet();

		// Token: 0x04002EAA RID: 11946
		private ArrayList generatedColumns;

		// Token: 0x04002EAB RID: 11947
		private bool enumerableTransformed;

		// Token: 0x04002EAC RID: 11948
		private StringCollection _filterExpressions = new StringCollection();

		// Token: 0x04002EAD RID: 11949
		private StringCollection _hierarchyFilterExpressions = new StringCollection();

		// Token: 0x04002EAE RID: 11950
		private int _dsCount = -1;

		// Token: 0x04002EAF RID: 11951
		private bool _isReady;

		// Token: 0x04002EB0 RID: 11952
		private GridInsertionObject _objectForInsertion;

		// Token: 0x04002EB1 RID: 11953
		internal GridTableView gridTableView;
	}
}
