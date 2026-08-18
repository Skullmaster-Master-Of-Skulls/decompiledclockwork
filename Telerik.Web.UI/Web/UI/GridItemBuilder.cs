using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x02001114 RID: 4372
	[SuppressMessage("Microsoft.Design", "CA1001:TypesThatOwnDisposableFieldsShouldBeDisposable")]
	internal class GridItemBuilder
	{
		// Token: 0x0600B2FC RID: 45820 RVA: 0x0026E817 File Offset: 0x0026CA17
		public GridItemBuilder(GridTableView ownerTableView, IEnumerator enumerator, GridColumn[] columns, ControlCollection controls)
		{
			this._ownerTableView = ownerTableView;
			this._enumerator = enumerator;
			this._controls = controls;
			this._columns = columns;
		}

		// Token: 0x0600B2FD RID: 45821 RVA: 0x0026E850 File Offset: 0x0026CA50
		internal void CreateItems(GridGroupingContext group)
		{
			GridEnumerableBase resolvedDataSource = this._ownerTableView.ResolvedDataSource;
			bool flag = true;
			while (!this.DoneCreatingItems())
			{
				if (this._ownerTableView.UseDataSource)
				{
					this.dataItem = this._enumerator.Current;
					this.dataItem = this.ReadOriginalDataItem(this.dataItem);
				}
				group.currentItemGroupIndex = this.concatGroupIndex(group.parentGroupIndex, group.itemIndexInGroup);
				bool flag2 = false;
				bool flag3 = false;
				this.HandleGrouping(group, resolvedDataSource, ref flag2, ref flag3);
				if (flag && !flag2 && !flag3)
				{
					int num = this._ownerTableView.itemIndexCounter;
					if (this._ownerTableView.ResolvedDataSource.Paging.IsPagingEnabled)
					{
						num = this._ownerTableView.ResolvedDataSource.Paging.FirstIndexInPage + this._ownerTableView.itemIndexCounter;
					}
					if (this._ownerTableView.UseDataSource)
					{
						this._ownerTableView.PopulateDataKeys(this.dataItem);
					}
					string itemIndexHierarchical;
					bool flag4;
					this._lastCreatedItem = this.InitializeItem(num, out itemIndexHierarchical, out flag4);
					this.SetItemGroupLevel(group.groupLevel);
					if (this._ownerTableView.UseDataSource)
					{
						this._lastCreatedItem.SetItemIndexHierarchical(itemIndexHierarchical);
					}
					this._lastCreatedItem.GroupIndexInternal = group.currentItemGroupIndex;
					if (this._ownerTableView.GroupLoadMode == GridGroupLoadMode.Server)
					{
						this._lastCreatedItem.Visible = this._ownerTableView.currentGroupExpanded;
					}
					else if (!this._ownerTableView.currentGroupExpanded)
					{
						this._lastCreatedItem.Style["display"] = "none";
						GridDataItem gridDataItem = this._lastCreatedItem as GridDataItem;
						if (gridDataItem != null && this._ownerTableView.DetailItemTemplate != null)
						{
							(gridDataItem.DetailTemplateItemDataCell.Parent as GridDetailTemplateItem).Style["display"] = "none";
						}
					}
					if (this._ownerTableView.EditMode == GridEditMode.EditForms || this._ownerTableView.EditMode == GridEditMode.PopUp)
					{
						GridEditFormItem gridEditFormItem = new GridEditFormItem(this._ownerTableView, this._ownerTableView.itemIndexCounter, num, this._lastCreatedItem as GridDataItem, flag4);
						gridEditFormItem.SetupItem(this._ownerTableView.UseDataSource, this.dataItem, this._columns, this._controls);
						if (flag4)
						{
							this._ownerTableView.ExtractValuesFromItem(gridEditFormItem.SavedOldValues, gridEditFormItem);
						}
					}
					else if (flag4)
					{
						this._ownerTableView.ExtractValuesFromItem((this._lastCreatedItem as GridEditableItem).SavedOldValues, this._lastCreatedItem as GridEditableItem);
					}
					this._ownerTableView.itemIndexCounter++;
					this._ownerTableView.itemGroupIndexCounter++;
					this._ownerTableView.allItemsCount++;
					flag = (!this._ownerTableView.HasDetailTables && this._ownerTableView.nestedViewTemplate == null);
					this.advanceEnumerator = flag;
					this._ownerTableView.itemsArray.Add(this._lastCreatedItem);
				}
				if (!flag && !flag2 && !flag3)
				{
					GridDataItem parentItem = this._lastCreatedItem as GridDataItem;
					GridNestedViewItem detailItem = this.CreateDetailItem(parentItem);
					this.ExpandDetailItem(parentItem, detailItem);
					this.BindDetailTables(parentItem, detailItem);
					this._ownerTableView.allItemsCount++;
					flag = true;
					this.advanceEnumerator = flag;
				}
			}
		}

		// Token: 0x0600B2FE RID: 45822 RVA: 0x0026EB94 File Offset: 0x0026CD94
		internal string concatGroupIndex(string parentIndex, int groupIndex)
		{
			string text = groupIndex.ToString();
			if (!string.IsNullOrEmpty(parentIndex))
			{
				text = parentIndex + "_" + text;
			}
			return text;
		}

		// Token: 0x0600B2FF RID: 45823 RVA: 0x0026EBC0 File Offset: 0x0026CDC0
		internal void HandleGrouping(GridGroupingContext group, GridEnumerableBase enumerable, ref bool isGroup, ref bool isGroupFooter)
		{
			this.InitializeGrouping(group, enumerable, ref isGroup, ref isGroupFooter);
			if (isGroup)
			{
				this.CreateGroupHeaderItem(group);
			}
			this.CreateGroupSubItems(group, enumerable);
			if (this._lastCreatedItem is GridGroupHeaderItem || this._lastCreatedItem is GridDataItem || this._lastCreatedItem == null)
			{
				group.itemIndexInGroup++;
			}
			if (isGroupFooter && this._ownerTableView.ShowGroupFooter)
			{
				if (this._ownerTableView.UseDataSource)
				{
					this.SaveGroupFooterState(group);
				}
				this.CreateGroupFooterItem(group);
				group.itemIndexInGroup++;
			}
		}

		// Token: 0x0600B300 RID: 45824 RVA: 0x0026EC58 File Offset: 0x0026CE58
		internal void InitializeGrouping(GridGroupingContext group, GridEnumerableBase enumerable, ref bool isGroup, ref bool isGroupFooter)
		{
			if (this._ownerTableView.UseDataSource)
			{
				isGroup = (group.groupLevel < enumerable.GroupingDataSet.Tables.Count - 1);
				isGroupFooter = isGroup;
				this.SaveGroupingState(group, isGroup);
				return;
			}
			this.LoadGroupingState(group, ref isGroup, ref isGroupFooter);
		}

		// Token: 0x0600B301 RID: 45825 RVA: 0x0026ECA8 File Offset: 0x0026CEA8
		internal void CreateGroupHeaderItem(GridGroupingContext group)
		{
			this._lastCreatedItem = new GridGroupHeaderItem(this._ownerTableView, -1, -1);
			this._lastCreatedItem.GroupLevel = group.groupLevel;
			this._lastCreatedItem.GroupIndexInternal = group.currentItemGroupIndex;
			this._lastCreatedItem.SetupItem(this._ownerTableView.UseDataSource, this.dataItem, this._columns, this._controls);
			this.GroupHeaderItems.Push(this._lastCreatedItem as GridGroupHeaderItem);
			if (this._ownerTableView.GroupLoadMode == GridGroupLoadMode.Server)
			{
				this._lastCreatedItem.Visible = group.parentGroupExpanded;
			}
			else if (!group.parentGroupExpanded)
			{
				this._lastCreatedItem.Style["display"] = "none";
			}
			this._ownerTableView.currentGroupExpanded = (this._lastCreatedItem.Expanded && group.parentGroupExpanded);
			this._ownerTableView.itemGroupIndexCounter++;
			this._ownerTableView.allItemsCount++;
			this.advanceEnumerator = true;
		}

		// Token: 0x0600B302 RID: 45826 RVA: 0x0026EDB8 File Offset: 0x0026CFB8
		internal void CreateGroupFooterItem(GridGroupingContext group)
		{
			GridGroupFooterItem gridGroupFooterItem = new GridGroupFooterItem(this._ownerTableView, -1, -1);
			this._lastCreatedItem = gridGroupFooterItem;
			gridGroupFooterItem.GroupHeaderItem = this.GroupHeaderItems.Pop();
			this._lastCreatedItem.GroupLevel = group.groupLevel;
			this._lastCreatedItem.GroupIndexInternal = group.currentItemGroupIndex;
			this._lastCreatedItem.SetupItem(this._ownerTableView.UseDataSource, this.dataItem, this._columns, this._controls);
			bool flag = this._ownerTableView.currentGroupExpanded || (this._ownerTableView.OwnerGrid.GroupingSettings.RetainGroupFootersVisibility && group.parentGroupExpanded);
			if (this._ownerTableView.GroupLoadMode == GridGroupLoadMode.Server)
			{
				this._lastCreatedItem.Visible = flag;
			}
			else if (!flag)
			{
				this._lastCreatedItem.Style["display"] = "none";
			}
			this._ownerTableView.itemGroupIndexCounter++;
			this._ownerTableView.allItemsCount++;
			this.advanceEnumerator = true;
		}

		// Token: 0x0600B303 RID: 45827 RVA: 0x0026EECC File Offset: 0x0026D0CC
		internal void CreateGroupSubItems(GridGroupingContext group, GridEnumerableBase enumerable)
		{
			if (this._ownerTableView.UseDataSource && this._lastCreatedItem is GridGroupHeaderItem)
			{
				this.dataItem = this._enumerator.Current;
				DataTableCollection tables = enumerable.GroupingDataSet.Tables;
				DataRowView drv = this.dataItem as DataRowView;
				string name = tables["GroupedTable" + group.groupLevel.ToString()].TableName + tables["GroupedTable" + (group.groupLevel + 1).ToString()].TableName;
				DataRelation relation = enumerable.GroupingDataSet.Relations[name];
				DataView dataView = GridItemBuilder.CreateChildView(enumerable.GroupingDataSet, drv, relation);
				IEnumerator enumerator = dataView.GetEnumerator();
				if (this._ownerTableView.ShowGroupFooter && this._ownerTableView.HasAggregates())
				{
					this.GetNestedView(enumerable, tables, group, dataView, enumerator);
				}
				GridGroupingContext gridGroupingContext = new GridGroupingContext();
				gridGroupingContext.groupLevel = group.groupLevel + 1;
				gridGroupingContext.parentGroupIndex = group.currentItemGroupIndex;
				gridGroupingContext.parentGroupExpanded = this._lastCreatedItem.Expanded;
				GridItemBuilder gridItemBuilder = new GridItemBuilder(this._ownerTableView, enumerator, this._columns, this._controls);
				gridItemBuilder.CreateItems(gridGroupingContext);
			}
		}

		// Token: 0x0600B304 RID: 45828 RVA: 0x0026F018 File Offset: 0x0026D218
		private void GetNestedView(GridEnumerableBase enumerable, DataTableCollection tables, GridGroupingContext group, DataView childView, IEnumerator tableRowEnumeratror)
		{
			if (tables.Count > group.groupLevel + 1 && tables["GroupedTable" + (group.groupLevel + 1).ToString()] != null && tables["GroupedTable" + (group.groupLevel + 2).ToString()] != null)
			{
				string name = tables["GroupedTable" + (group.groupLevel + 1).ToString()].TableName + tables["GroupedTable" + (group.groupLevel + 2).ToString()].TableName;
				DataRelation dataRelation = enumerable.GroupingDataSet.Relations[name];
				if (dataRelation != null)
				{
					DataTable dataTable = null;
					int currentGroupLevel = group.groupLevel + 1;
					DataView dataView = this.ComputeNestedAggregatesData(tables, enumerable, currentGroupLevel, tableRowEnumeratror);
					if (dataView != null)
					{
						dataTable = GridDataSetHelper.CloneTableStructure(dataView.Table);
						dataTable.Merge(dataView.Table);
					}
					if (dataTable != null)
					{
						DataTable dataTable2 = GridDataSetHelper.CloneTableStructure(childView.Table);
						dataTable2.Merge(dataTable);
						string key = string.Format("GroupedResult{0}", group.groupLevel);
						enumerable.GroupingDataSet.ExtendedProperties[key] = dataTable2;
					}
					tableRowEnumeratror.Reset();
					return;
				}
			}
			else
			{
				string key2 = string.Format("GroupedResult{0}", group.groupLevel);
				enumerable.GroupingDataSet.ExtendedProperties[key2] = childView.Table;
			}
		}

		// Token: 0x0600B305 RID: 45829 RVA: 0x0026F1A4 File Offset: 0x0026D3A4
		private DataView ComputeNestedAggregatesData(DataTableCollection tables, GridEnumerableBase enumerable, int currentGroupLevel, IEnumerator enumeratror)
		{
			DataView dataView = null;
			DataTable dataTable = null;
			if (tables.Count > currentGroupLevel && tables["GroupedTable" + currentGroupLevel.ToString()] != null && tables["GroupedTable" + (currentGroupLevel + 1).ToString()] != null)
			{
				string name = tables["GroupedTable" + currentGroupLevel.ToString()].TableName + tables["GroupedTable" + (currentGroupLevel + 1).ToString()].TableName;
				DataRelation dataRelation = enumerable.GroupingDataSet.Relations[name];
				if (dataRelation != null)
				{
					while (enumeratror.MoveNext())
					{
						object obj = enumeratror.Current;
						dataView = GridItemBuilder.CreateChildView(enumerable.GroupingDataSet, (DataRowView)obj, dataRelation);
						if (dataView != null)
						{
							DataView dataView2 = this.ComputeNestedAggregatesData(tables, enumerable, currentGroupLevel + 1, dataView.GetEnumerator());
							if (dataView2 != null)
							{
								dataView = dataView2;
							}
							if (dataTable == null)
							{
								dataTable = GridDataSetHelper.CloneTableStructure(dataView.Table);
							}
							dataTable.Merge(dataView.Table);
						}
					}
				}
			}
			if (dataView != null)
			{
				dataView.Table.Clear();
				dataView.Table.Merge(dataTable);
			}
			return dataView;
		}

		// Token: 0x0600B306 RID: 45830 RVA: 0x0026F2D0 File Offset: 0x0026D4D0
		internal static DataView CreateChildView(DataSet originalDataSet, DataRowView drv, DataRelation relation)
		{
			DataRow parent = drv.Row;
			GridItemBuilder.GroupingHelperDataRow groupingHelperDataRow = drv.Row as GridItemBuilder.GroupingHelperDataRow;
			if (groupingHelperDataRow != null)
			{
				parent = groupingHelperDataRow.OriginalRow;
			}
			DataRow[] childRows = GridItemBuilder.GetChildRows(parent, relation);
			DataTable childTable = relation.ChildTable;
			DataTable dataTable = GridDataSetHelper.CloneTableStructure(new GridItemBuilder.GroupingHelperDataTable(), childTable);
			foreach (DataRow dataRow in childRows)
			{
				dataTable.LoadDataRow(dataRow.ItemArray, false);
				GridItemBuilder.GroupingHelperDataRow groupingHelperDataRow2 = (GridItemBuilder.GroupingHelperDataRow)dataTable.Rows[dataTable.Rows.Count - 1];
				groupingHelperDataRow2.OriginalRow = dataRow;
			}
			return new DataView(dataTable);
		}

		// Token: 0x0600B307 RID: 45831 RVA: 0x0026F374 File Offset: 0x0026D574
		private static DataRow[] GetChildRows(DataRow parent, DataRelation relation)
		{
			object obj = parent[relation.ParentColumns[0]];
			if (obj == null || obj == DBNull.Value)
			{
				return GridItemBuilder.GetChildRowsForNullValue(parent, relation);
			}
			return parent.GetChildRows(relation);
		}

		// Token: 0x0600B308 RID: 45832 RVA: 0x0026F3AC File Offset: 0x0026D5AC
		private static DataRow[] GetChildRowsForNullValue(DataRow parent, DataRelation relation)
		{
			DataTable childTable = relation.ChildTable;
			ArrayList arrayList = new ArrayList();
			foreach (object obj in childTable.Rows)
			{
				DataRow dataRow = (DataRow)obj;
				int num = Math.Min(relation.ParentColumns.Length, relation.ChildColumns.Length);
				bool flag = num > 0;
				for (int i = 0; i < num; i++)
				{
					object obj2 = parent[relation.ParentColumns[i]];
					object obj3 = dataRow[relation.ChildColumns[i]];
					if (obj2 != obj3 && (obj2 == null || !obj2.Equals(obj3)))
					{
						flag = false;
					}
				}
				if (flag)
				{
					arrayList.Add(dataRow);
				}
			}
			return (DataRow[])arrayList.ToArray(typeof(DataRow));
		}

		// Token: 0x0600B309 RID: 45833 RVA: 0x0026F49C File Offset: 0x0026D69C
		internal void LoadGroupingState(GridGroupingContext group, ref bool isGroup, ref bool isGroupFooter)
		{
			isGroupFooter = (this._ownerTableView.CreateItemsState[this._ownerTableView.allItemsCount] != null);
			isGroup = (this._ownerTableView.CreateItemsState[this._ownerTableView.allItemsCount] != null);
			if (isGroup)
			{
				Triplet triplet = (Triplet)this._ownerTableView.CreateItemsState[this._ownerTableView.allItemsCount];
				if ((bool)triplet.Third)
				{
					group.groupLevel = (int)triplet.First;
					group.parentGroupIndex = (string)triplet.Second;
					group.currentItemGroupIndex = group.parentGroupIndex;
					group.itemIndexInGroup = -1;
				}
				else
				{
					isGroup = false;
				}
			}
			if (isGroupFooter)
			{
				Triplet triplet2 = (Triplet)this._ownerTableView.CreateItemsState[this._ownerTableView.allItemsCount];
				if (!(bool)triplet2.Third)
				{
					if ((int)triplet2.First > group.groupLevel)
					{
						isGroupFooter = false;
						return;
					}
				}
				else
				{
					isGroupFooter = false;
				}
			}
		}

		// Token: 0x0600B30A RID: 45834 RVA: 0x0026F5BC File Offset: 0x0026D7BC
		internal void SaveGroupingState(GridGroupingContext group, bool isGroup)
		{
			if (isGroup)
			{
				this._ownerTableView.CreateItemsState[this._ownerTableView.allItemsCount] = new Triplet(group.groupLevel, group.currentItemGroupIndex, true);
			}
		}

		// Token: 0x0600B30B RID: 45835 RVA: 0x0026F608 File Offset: 0x0026D808
		internal void SaveGroupFooterState(GridGroupingContext group)
		{
			this._ownerTableView.CreateItemsState[this._ownerTableView.allItemsCount] = new Triplet(group.groupLevel, group.currentItemGroupIndex, false);
		}

		// Token: 0x0600B30C RID: 45836 RVA: 0x0026F648 File Offset: 0x0026D848
		internal GridItem InitializeItem(int dataSourceIndex, out string nextItemHierarchicalIndex, out bool itemIsInEditMode)
		{
			nextItemHierarchicalIndex = this._ownerTableView.GetIndexHierarchical(this._ownerTableView.itemIndexCounter);
			itemIsInEditMode = this._ownerTableView.OwnerGrid.EditIndexes.Contains(nextItemHierarchicalIndex);
			this._lastCreatedItem = this.CreateItem(dataSourceIndex, itemIsInEditMode);
			this._lastCreatedItem.SetTempIndexHierarchical(nextItemHierarchicalIndex);
			this._lastCreatedItem.SetupItem(this._ownerTableView.UseDataSource, this.dataItem, this._columns, this._controls);
			return this._lastCreatedItem;
		}

		// Token: 0x0600B30D RID: 45837 RVA: 0x0026F6D0 File Offset: 0x0026D8D0
		internal GridItem CreateItem(int dataSourceIndex, bool itemIsInEditMode)
		{
			if (itemIsInEditMode)
			{
				this._lastCreatedItem = this.CreateEditItem(dataSourceIndex);
			}
			else
			{
				this._lastCreatedItem = this.CreateAlternatingItem(dataSourceIndex);
			}
			return this._lastCreatedItem;
		}

		// Token: 0x0600B30E RID: 45838 RVA: 0x0026F6F8 File Offset: 0x0026D8F8
		internal void BindDetailTables(GridDataItem parentItem, GridNestedViewItem detailItem)
		{
			int num = 0;
			foreach (GridTableView gridTableView in this._ownerTableView.DetailTables)
			{
				GridTableView masterTableView = this._ownerTableView._ownerGrid.MasterTableView;
				if (!masterTableView.detailTablesSelectMethods.ContainsKey(gridTableView.HierarchyIndex))
				{
					masterTableView.detailTablesSelectMethods.Add(gridTableView.HierarchyIndex, gridTableView.SelectMethod);
				}
				GridTableView cloned = this.CreateDetailTable(parentItem, detailItem, num, gridTableView);
				this.BindDetailTable(parentItem, cloned);
				num++;
			}
		}

		// Token: 0x0600B30F RID: 45839 RVA: 0x0026F7A8 File Offset: 0x0026D9A8
		internal void BindDetailTable(GridDataItem parentItem, GridTableView cloned)
		{
			if (this._ownerTableView.UseDataSource)
			{
				if (parentItem.Expanded)
				{
					if (this._ownerTableView.IsUsingModelBinding)
					{
						cloned.SelectMethod = this._ownerTableView.detailTablesSelectMethods[cloned.HierarchyIndex];
					}
					this._ownerTableView.BindClone(cloned);
					return;
				}
				if (this._ownerTableView.OwnerGrid.SelectedIndexes.ContainsChildIndex(parentItem.ItemIndexHierarchical) || this._ownerTableView.OwnerGrid.EditIndexes.ContainsChildIndex(parentItem.ItemIndexHierarchical))
				{
					parentItem.Expanded = true;
					return;
				}
				if (this._ownerTableView.HierarchyLoadMode == GridChildLoadMode.ServerBind || this._ownerTableView.HierarchyLoadMode == GridChildLoadMode.Client)
				{
					if (this._ownerTableView.IsUsingModelBinding)
					{
						cloned.SelectMethod = this._ownerTableView.detailTablesSelectMethods[cloned.HierarchyIndex];
					}
					this._ownerTableView.BindClone(cloned);
				}
			}
		}

		// Token: 0x0600B310 RID: 45840 RVA: 0x0026F898 File Offset: 0x0026DA98
		internal GridTableView CreateDetailTable(GridDataItem parentItem, GridNestedViewItem detailItem, int detailCounter, GridTableView detailTable)
		{
			GridTableView gridTableView = detailTable.Clone();
			gridTableView.childIndex = detailCounter.ToString();
			gridTableView.ID = "Detail" + this._ownerTableView.itemIndexCounter + detailCounter;
			gridTableView.SetParentItem(parentItem);
			if (parentItem.OwnerTableView.nestedViewTemplate != null)
			{
				gridTableView.Visible = false;
			}
			detailItem.NestedViewCell.Controls.Add(gridTableView);
			return gridTableView;
		}

		// Token: 0x0600B311 RID: 45841 RVA: 0x0026F910 File Offset: 0x0026DB10
		internal void ExpandDetailItem(GridDataItem parentItem, GridNestedViewItem detailItem)
		{
			detailItem.Visible = ((this._ownerTableView.HierarchyLoadMode == GridChildLoadMode.Client || parentItem.Expanded) && this._ownerTableView.currentGroupExpanded);
			if (this._ownerTableView.HierarchyLoadMode == GridChildLoadMode.Client)
			{
				bool flag = false;
				if (parentItem.Expanded)
				{
					GridTable gridTable = parentItem.Parent as GridTable;
					if (gridTable != null)
					{
						int num = parentItem.RowIndex - 1;
						if (num >= 0)
						{
							GridGroupHeaderItem gridGroupHeaderItem = gridTable.Rows[num] as GridGroupHeaderItem;
							if (gridGroupHeaderItem != null && !gridGroupHeaderItem.Expanded)
							{
								flag = true;
							}
						}
					}
				}
				if (!parentItem.Expanded || flag)
				{
					detailItem.Style["display"] = "none";
				}
			}
		}

		// Token: 0x0600B312 RID: 45842 RVA: 0x0026F9BC File Offset: 0x0026DBBC
		internal GridNestedViewItem CreateDetailItem(GridDataItem parentItem)
		{
			GridNestedViewItem gridNestedViewItem = new GridNestedViewItem(this._ownerTableView, -1, -1);
			gridNestedViewItem.ParentItem = parentItem;
			gridNestedViewItem.SetupItem(this._ownerTableView.UseDataSource, null, this._columns, this._controls);
			this._lastCreatedItem = gridNestedViewItem;
			parentItem.SetChildItem(gridNestedViewItem);
			return gridNestedViewItem;
		}

		// Token: 0x0600B313 RID: 45843 RVA: 0x0026FA0B File Offset: 0x0026DC0B
		[SuppressMessage("Microsoft.Usage", "CA2233:OperationsShouldNotOverflow")]
		internal void SetItemGroupLevel(int groupLevel)
		{
			if (this._ownerTableView.UseDataSource)
			{
				this._lastCreatedItem.GroupLevel = groupLevel - 1;
				return;
			}
			this._lastCreatedItem.GroupLevel = groupLevel;
		}

		// Token: 0x0600B314 RID: 45844 RVA: 0x0026FA38 File Offset: 0x0026DC38
		internal GridItem CreateAlternatingItem(int dataSourceIndex)
		{
			if (this._ownerTableView.itemIndexCounter % 2 != 0)
			{
				this._lastCreatedItem = new GridDataItem(this._ownerTableView, this._ownerTableView.itemIndexCounter, dataSourceIndex, GridItemType.AlternatingItem);
			}
			else
			{
				this._lastCreatedItem = new GridDataItem(this._ownerTableView, this._ownerTableView.itemIndexCounter, dataSourceIndex);
			}
			if (this._ownerTableView._ownerGrid._isClientBindingDummyDataGenerated && !this._ownerTableView._ownerGrid.ClientSettings.DataBinding.ShowEmptyRowsOnLoad)
			{
				this._lastCreatedItem.Style["display"] = "none";
			}
			return this._lastCreatedItem;
		}

		// Token: 0x0600B315 RID: 45845 RVA: 0x0026FAE0 File Offset: 0x0026DCE0
		internal GridItem CreateEditItem(int dataSourceIndex)
		{
			if (this._ownerTableView.EditMode == GridEditMode.EditForms || this._ownerTableView.EditMode == GridEditMode.PopUp)
			{
				this._lastCreatedItem = new GridDataItem(this._ownerTableView, this._ownerTableView.itemIndexCounter, dataSourceIndex);
				this._lastCreatedItem.SetItemDecorator(new GridEditItemDecorator(this._lastCreatedItem));
			}
			else
			{
				this._lastCreatedItem = new GridDataItem(this._ownerTableView, this._ownerTableView.itemIndexCounter, dataSourceIndex, GridItemType.EditItem);
			}
			return this._lastCreatedItem;
		}

		// Token: 0x0600B316 RID: 45846 RVA: 0x0026FB64 File Offset: 0x0026DD64
		internal object ReadOriginalDataItem(object dataItem)
		{
			DataRowView dataRowView = dataItem as DataRowView;
			if (dataItem != null && dataRowView != null && dataRowView.Row.Table.Columns.Contains("OriginalDataItem"))
			{
				object obj = (this._enumerator.Current as DataRowView)["OriginalDataItem"];
				if (obj != null && obj != DBNull.Value)
				{
					dataItem = obj;
				}
			}
			return dataItem;
		}

		// Token: 0x0600B317 RID: 45847 RVA: 0x0026FBC4 File Offset: 0x0026DDC4
		internal bool DoneCreatingItems()
		{
			if (this.advanceEnumerator)
			{
				this.finished = !this._enumerator.MoveNext();
			}
			else
			{
				this.finished = false;
			}
			return this.finished;
		}

		// Token: 0x04002F29 RID: 12073
		private GridTableView _ownerTableView;

		// Token: 0x04002F2A RID: 12074
		private GridColumn[] _columns;

		// Token: 0x04002F2B RID: 12075
		private ControlCollection _controls;

		// Token: 0x04002F2C RID: 12076
		private IEnumerator _enumerator;

		// Token: 0x04002F2D RID: 12077
		private bool finished;

		// Token: 0x04002F2E RID: 12078
		private object dataItem;

		// Token: 0x04002F2F RID: 12079
		private bool advanceEnumerator = true;

		// Token: 0x04002F30 RID: 12080
		private GridItem _lastCreatedItem;

		// Token: 0x04002F31 RID: 12081
		private Stack<GridGroupHeaderItem> GroupHeaderItems = new Stack<GridGroupHeaderItem>();

		// Token: 0x02001115 RID: 4373
		private class GroupingHelperDataRow : DataRow
		{
			// Token: 0x0600B318 RID: 45848 RVA: 0x0026FBF1 File Offset: 0x0026DDF1
			public GroupingHelperDataRow(DataRowBuilder builder) : base(builder)
			{
			}

			// Token: 0x170039F0 RID: 14832
			// (get) Token: 0x0600B319 RID: 45849 RVA: 0x0026FBFA File Offset: 0x0026DDFA
			// (set) Token: 0x0600B31A RID: 45850 RVA: 0x0026FC02 File Offset: 0x0026DE02
			public DataRow OriginalRow
			{
				get
				{
					return this.originalRow;
				}
				set
				{
					this.originalRow = value;
				}
			}

			// Token: 0x04002F32 RID: 12082
			private DataRow originalRow;
		}

		// Token: 0x02001116 RID: 4374
		private class GroupingHelperDataTable : DataTable
		{
			// Token: 0x0600B31B RID: 45851 RVA: 0x0026FC0B File Offset: 0x0026DE0B
			protected override Type GetRowType()
			{
				return typeof(GridItemBuilder.GroupingHelperDataRow);
			}

			// Token: 0x0600B31C RID: 45852 RVA: 0x0026FC17 File Offset: 0x0026DE17
			protected override DataRow NewRowFromBuilder(DataRowBuilder builder)
			{
				return new GridItemBuilder.GroupingHelperDataRow(builder);
			}
		}
	}
}
