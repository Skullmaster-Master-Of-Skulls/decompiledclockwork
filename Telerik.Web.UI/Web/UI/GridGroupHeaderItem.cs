using System;
using System.Collections;
using System.Collections.Specialized;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x02001143 RID: 4419
	public class GridGroupHeaderItem : GridItem
	{
		// Token: 0x17003A33 RID: 14899
		// (get) Token: 0x0600B40F RID: 46095 RVA: 0x00275DBE File Offset: 0x00273FBE
		// (set) Token: 0x0600B410 RID: 46096 RVA: 0x00275DD9 File Offset: 0x00273FD9
		public IDictionary AggregatesValues
		{
			get
			{
				if (this.aggregatesValues == null)
				{
					this.aggregatesValues = new ListDictionary();
				}
				return this.aggregatesValues;
			}
			set
			{
				this.aggregatesValues = (ListDictionary)value;
			}
		}

		// Token: 0x0600B411 RID: 46097 RVA: 0x00275DE7 File Offset: 0x00273FE7
		public GridGroupHeaderItem(GridTableView ownerTableView, int itemIndex, int dataSetIndex) : base(ownerTableView, itemIndex, dataSetIndex, GridItemType.GroupHeader)
		{
		}

		// Token: 0x0600B412 RID: 46098 RVA: 0x00275DF4 File Offset: 0x00273FF4
		protected override TableCell CreateCellObject()
		{
			return new GridTableCell(base.OwnerTableView.OwnerGrid.ResolvedRenderMode != RenderMode.Lightweight);
		}

		// Token: 0x0600B413 RID: 46099 RVA: 0x00275E14 File Offset: 0x00274014
		public override void Initialize(GridColumn[] columns)
		{
			TableCellCollection cells = this.Cells;
			int num = 0;
			if (this.AggregatesValues.Count != columns.Length)
			{
				foreach (GridColumn gridColumn in columns)
				{
					string text = string.Empty;
					GridBoundColumn gridBoundColumn = gridColumn as GridBoundColumn;
					if (gridBoundColumn != null)
					{
						text = gridBoundColumn.DataField;
					}
					GridTemplateColumn gridTemplateColumn = gridColumn as GridTemplateColumn;
					if (gridTemplateColumn != null)
					{
						text = gridTemplateColumn.DataField;
					}
					GridCalculatedColumn gridCalculatedColumn = gridColumn as GridCalculatedColumn;
					if (gridCalculatedColumn != null)
					{
						text = string.Format("{0}Result", gridCalculatedColumn.UniqueName);
					}
					if (text != string.Empty && !this.AggregatesValues.Contains(text))
					{
						this.AggregatesValues.Add(text, null);
					}
				}
			}
			TableCell tableCell;
			foreach (GridColumn gridColumn2 in columns)
			{
				tableCell = this.CreateCellObject();
				cells.Add(tableCell);
				gridColumn2.InitializeCell(tableCell, num, this);
				GridGroupSplitterColumn gridGroupSplitterColumn = gridColumn2 as GridGroupSplitterColumn;
				if (gridGroupSplitterColumn != null && gridGroupSplitterColumn.CorrespondingExpression.Index == base.GroupLevel)
				{
					break;
				}
				num++;
			}
			this.calculatedColumnIndex = num + 1;
			tableCell = this.CreateCellObject();
			if (!base.OwnerTableView.OwnerGrid.ClientSettings.Scrolling.AllowScroll || !base.OwnerTableView.OwnerGrid.ClientSettings.Scrolling.UseStaticHeaders)
			{
				tableCell.ColumnSpan = base.CalcColSpan(columns, this.calculatedColumnIndex, -1);
			}
			if (base.OwnerTableView.OwnerGrid.ResolvedRenderMode == RenderMode.Lightweight)
			{
				tableCell.ColumnSpan = base.CalcColSpan(columns, this.calculatedColumnIndex, -1);
			}
			cells.Add(tableCell);
			ITemplate groupHeaderTemplate = base.OwnerTableView.GroupHeaderTemplate;
			if (groupHeaderTemplate != null)
			{
				groupHeaderTemplate.InstantiateIn(tableCell);
			}
			this._dataCell = tableCell;
			if (base.OwnerTableView.OwnerGrid.ClientSettings.Scrolling.AllowScroll && base.OwnerTableView.OwnerGrid.ClientSettings.Scrolling.UseStaticHeaders && base.OwnerTableView.OwnerGrid.ResolvedRenderMode != RenderMode.Lightweight)
			{
				int num2 = base.CalcColSpan(columns, this.calculatedColumnIndex, -1) - 1;
				for (int k = 0; k < num2; k++)
				{
					TableCell tableCell2 = this.CreateCellObject();
					if (base.OwnerTableView.OwnerGrid.EmptySkin())
					{
						tableCell2.Style["border"] = "0";
					}
					cells.Add(tableCell2);
				}
			}
		}

		// Token: 0x17003A34 RID: 14900
		// (get) Token: 0x0600B414 RID: 46100 RVA: 0x0027608C File Offset: 0x0027428C
		public TableCell DataCell
		{
			get
			{
				return this._dataCell;
			}
		}

		// Token: 0x17003A35 RID: 14901
		// (get) Token: 0x0600B415 RID: 46101 RVA: 0x00276094 File Offset: 0x00274294
		public override bool HasChildItems
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17003A36 RID: 14902
		// (get) Token: 0x0600B416 RID: 46102 RVA: 0x00276097 File Offset: 0x00274297
		// (set) Token: 0x0600B417 RID: 46103 RVA: 0x0027609F File Offset: 0x0027429F
		internal GridGroup OriginalGroup { get; set; }

		// Token: 0x0600B418 RID: 46104 RVA: 0x002760A8 File Offset: 0x002742A8
		public override void PrepareItemStyle()
		{
			if (base.OwnerTableView.OwnerGrid.ClientSettings.Scrolling.AllowScroll && base.OwnerTableView.OwnerGrid.ClientSettings.Scrolling.UseStaticHeaders)
			{
				int num = base.CalcColSpan(base.OwnerTableView.RenderColumns, this.calculatedColumnIndex, -1) + this.calculatedColumnIndex;
				int num2 = this.Cells.Count - num;
				if (num2 > 0)
				{
					for (int i = 0; i < num2; i++)
					{
						this.Cells[this.Cells.Count - i - 1].Style.Add(HtmlTextWriterStyle.Display, "none");
					}
				}
			}
			else
			{
				this.DataCell.ColumnSpan = base.CalcColSpan(base.OwnerTableView.RenderColumns, this.calculatedColumnIndex, -1);
			}
			base.PrepareItemStyle();
		}

		// Token: 0x0600B419 RID: 46105 RVA: 0x00276184 File Offset: 0x00274384
		public override void SetupItem(bool dataBind, object dataItem, GridColumn[] columns, ControlCollection rows)
		{
			GridItemEventArgs e = new GridItemEventArgs(this, new GridItemCreated());
			this.DataItem = dataItem;
			rows.Add(this);
			this.Initialize(columns);
			base.OwnerTableView.OwnerGrid.CallOnItemCreated(e);
			if (!dataBind)
			{
				return;
			}
			if (base.OwnerTableView.GroupHeaderTemplate == null)
			{
				this.DataBind();
			}
			if (base.GroupLevel >= 0)
			{
				GridGroupByExpression gridGroupByExpression = base.OwnerTableView.GroupByExpressions[base.GroupLevel];
				DataRowView dataRowView = dataItem as DataRowView;
				if (dataRowView == null)
				{
					return;
				}
				string text = string.Empty;
				int num = 0;
				foreach (object obj in gridGroupByExpression.SelectFields)
				{
					GridGroupByField gridGroupByField = (GridGroupByField)obj;
					try
					{
						if (base.OwnerTableView.OwnerGrid.EnableLinqExpressions && base.OwnerTableView.EnableLinqGrouping && gridGroupByField.Aggregate != GridAggregateFunction.None && base.OwnerTableView._shouldUseLinqGrouping && base.OwnerTableView.LinqGroupingHelper != null)
						{
							base.OwnerTableView.LinqGroupingHelper.GroupHeaderItem = this;
							base.OwnerTableView.LinqGroupingHelper.CalculateAggregatesWhenLinqGrouping(rows, gridGroupByExpression, dataRowView, gridGroupByField);
						}
						object obj2 = dataRowView[gridGroupByField.FieldAlias];
						if (!string.IsNullOrEmpty(base.OwnerTableView.TimeZoneID) && obj2.GetType() == typeof(DateTime))
						{
							obj2 = base.OwnerTableView.TimeZoneProvider.UtcToLocal((DateTime)obj2);
						}
						text += string.Format(gridGroupByField.GetFormatString(), obj2);
						if (num != gridGroupByExpression.SelectFields.Count - 1)
						{
							text += base.OwnerTableView.OwnerGrid.GroupingSettings.GroupByFieldsSeparator;
						}
						if (base.OwnerTableView.GroupHeaderTemplate != null)
						{
							this.AggregatesValues[gridGroupByField.FieldAlias] = dataRowView[gridGroupByField.FieldAlias];
							if (dataRowView.DataView.Table.Columns.Contains(gridGroupByField.FieldName))
							{
								if (!this.AggregatesValues.Contains(gridGroupByField.FieldName))
								{
									this.AggregatesValues.Add(gridGroupByField.FieldName, dataRowView[gridGroupByField.FieldName]);
								}
								else
								{
									this.AggregatesValues[gridGroupByField.FieldName] = dataRowView[gridGroupByField.FieldName];
								}
							}
						}
					}
					catch (Exception)
					{
						text = gridGroupByField.GetFormatString();
					}
					num++;
				}
				object obj3 = dataRowView["SplitGroup"];
				if (obj3 != DBNull.Value)
				{
					GridSplitGroup gridSplitGroup = (GridSplitGroup)obj3;
					string text2 = "";
					if (gridSplitGroup.Mode == GridGroupSplitMode.Continued)
					{
						text2 = text2 + base.OwnerTableView.OwnerGrid.GroupingSettings.GroupContinuedFormatString + base.OwnerTableView.OwnerGrid.GroupingSettings.GroupSplitDisplayFormat;
					}
					else if (gridSplitGroup.Mode == GridGroupSplitMode.Continues)
					{
						text2 = text2 + base.OwnerTableView.OwnerGrid.GroupingSettings.GroupSplitDisplayFormat + base.OwnerTableView.OwnerGrid.GroupingSettings.GroupContinuesFormatString;
					}
					else
					{
						text2 = text2 + base.OwnerTableView.OwnerGrid.GroupingSettings.GroupContinuedFormatString + base.OwnerTableView.OwnerGrid.GroupingSettings.GroupSplitDisplayFormat + base.OwnerTableView.OwnerGrid.GroupingSettings.GroupContinuesFormatString;
					}
					text += string.Format(base.OwnerTableView.OwnerGrid.GroupingSettings.GroupSplitFormat, string.Format(text2, gridSplitGroup.ActualItemCount, gridSplitGroup.GroupItemsCount));
				}
				if (base.OwnerTableView.GroupHeaderTemplate == null)
				{
					this.DataCell.Text = text;
				}
			}
			if (base.OwnerTableView.GroupHeaderTemplate != null)
			{
				this.DataBind();
			}
			this.CellsDataBound(columns);
			e = new GridItemEventArgs(this, new GridItemDataBound());
			base.OwnerTableView.OwnerGrid.CallOnItemDataBound(e);
		}

		// Token: 0x17003A37 RID: 14903
		// (get) Token: 0x0600B41A RID: 46106 RVA: 0x002765C0 File Offset: 0x002747C0
		public override bool CanExpand
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600B41B RID: 46107 RVA: 0x002765C3 File Offset: 0x002747C3
		protected override bool GetExpandedDefaultValue()
		{
			return base.OwnerTableView.GroupsDefaultExpanded;
		}

		// Token: 0x0600B41C RID: 46108 RVA: 0x002765D0 File Offset: 0x002747D0
		public GridItem[] GetChildItems()
		{
			ArrayList arrayList = new ArrayList();
			GridTable gridTable = base.OwnerTableView.GetGridTable();
			foreach (object obj in gridTable.Rows)
			{
				GridItem gridItem = (GridItem)obj;
				if (gridItem != this && (gridItem.GroupIndexInternal == base.GroupIndexInternal || gridItem.GroupIndexInternal.StartsWith(base.GroupIndexInternal + "_")) && gridItem.GroupIndexInternal.LastIndexOf("_") == base.GroupIndexInternal.Length)
				{
					arrayList.Add(gridItem);
				}
			}
			GridItem[] array = new GridItem[arrayList.Count];
			arrayList.CopyTo(array, 0);
			return array;
		}

		// Token: 0x0600B41D RID: 46109 RVA: 0x002766AC File Offset: 0x002748AC
		[SuppressMessage("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
		public override void SetVisibleChildren(bool value)
		{
			GridTable gridTable = base.OwnerTableView.GetGridTable();
			string text = string.Empty;
			foreach (object obj in gridTable.Rows)
			{
				GridItem gridItem = (GridItem)obj;
				if (gridItem == this)
				{
					GridGroupHeaderItem gridGroupHeaderItem = gridItem as GridGroupHeaderItem;
					if (gridGroupHeaderItem != null && !value && !base.OwnerTableView.OwnerGrid.GroupingSettings.RetainGroupFootersVisibility && base.OwnerTableView.ShowGroupFooter && !gridGroupHeaderItem.expandedInternal && gridGroupHeaderItem.GroupIndexInternal.LastIndexOf("_") <= base.GroupIndexInternal.Length)
					{
						this.HideFooterByGroupHeaderItem(gridGroupHeaderItem);
					}
					if (gridGroupHeaderItem != null && value && gridGroupHeaderItem.expandedInternal && gridGroupHeaderItem.GroupIndexInternal.LastIndexOf("_") <= base.GroupIndexInternal.Length && base.OwnerTableView.GroupHeaderFooterIndexMap.ContainsKey(gridGroupHeaderItem.GroupIndexInternal))
					{
						GridGroupFooterItem gridGroupFooterItem = base.OwnerTableView.GroupHeaderFooterIndexMap[gridGroupHeaderItem.GroupIndexInternal];
						gridGroupFooterItem.SetVisibility(true);
					}
				}
				else if (gridItem.GroupIndexInternal == base.GroupIndexInternal || (gridItem.GroupIndexInternal.StartsWith(base.GroupIndexInternal + "_") && gridItem.GroupIndexInternal.LastIndexOf("_") == base.GroupIndexInternal.Length))
				{
					if (base.OwnerTableView.GroupLoadMode == GridGroupLoadMode.Client)
					{
						GridGroupHeaderItem gridGroupHeaderItem2 = gridItem as GridGroupHeaderItem;
						if (value)
						{
							gridItem.SetVisibility(true);
							if (gridGroupHeaderItem2 != null)
							{
								gridGroupHeaderItem2.SetChildrenVisibility(true);
							}
						}
						else
						{
							if (gridGroupHeaderItem2 != null && base.OwnerTableView.ShowGroupFooter)
							{
								this.HideFooterByGroupHeaderItem(gridGroupHeaderItem2);
							}
							if (gridItem.GetType() != typeof(GridGroupFooterItem))
							{
								gridItem.SetVisibility(false);
							}
							if (gridGroupHeaderItem2 != null)
							{
								gridGroupHeaderItem2.SetChildrenVisibility(value);
							}
							GridDataItem gridDataItem = gridItem as GridDataItem;
							if (base.OwnerTableView.HierarchyLoadMode == GridChildLoadMode.Client && gridDataItem != null && gridDataItem.ChildItem != null)
							{
								gridDataItem.ChildItem.Style["display"] = "none";
							}
						}
					}
					else if (base.OwnerTableView.OwnerGrid.GroupingSettings.RetainGroupFootersVisibility && gridItem is GridGroupFooterItem)
					{
						text = GridGroupHeaderItem.CalculateCurrentGroupIndex(text, gridItem);
						gridItem.Visible = this.GetHeaderItemByGroupIndex(text).Visible;
					}
					else
					{
						if (gridItem is GridGroupFooterItem)
						{
							text = GridGroupHeaderItem.CalculateCurrentGroupIndex(text, gridItem);
							GridGroupHeaderItem headerItemByGroupIndex = this.GetHeaderItemByGroupIndex(text);
							if (text == base.GroupIndex)
							{
								gridItem.Visible = value;
							}
							else
							{
								bool expandedGroupInternal = base.OwnerTableView.expandedGroupInternal;
								string expandedGroupIndexInternal = base.OwnerTableView.expandedGroupIndexInternal;
								bool flag = headerItemByGroupIndex.expandedInternal;
								if (flag)
								{
									flag = headerItemByGroupIndex.Visible;
									if (expandedGroupInternal)
									{
										flag = (expandedGroupIndexInternal == text || (headerItemByGroupIndex.Expanded && flag));
									}
								}
								gridItem.Visible = flag;
							}
						}
						else
						{
							gridItem.Visible = value;
						}
						gridItem.RestoreChildrenVisible();
					}
				}
			}
		}

		// Token: 0x0600B41E RID: 46110 RVA: 0x002769F8 File Offset: 0x00274BF8
		private void HideFooterByGroupHeaderItem(GridGroupHeaderItem gridGroupHeaderItem)
		{
			if (base.OwnerTableView.GroupHeaderFooterIndexMap.ContainsKey(gridGroupHeaderItem.GroupIndexInternal))
			{
				GridGroupFooterItem gridGroupFooterItem = base.OwnerTableView.GroupHeaderFooterIndexMap[gridGroupHeaderItem.GroupIndexInternal];
				gridGroupFooterItem.Style["display"] = "none";
			}
		}

		// Token: 0x0600B41F RID: 46111 RVA: 0x00276A4C File Offset: 0x00274C4C
		private static string CalculateCurrentGroupIndex(string currentGroupIndex, GridItem gridItem)
		{
			if (string.IsNullOrEmpty(currentGroupIndex))
			{
				int num = gridItem.GroupIndexInternal.LastIndexOf('_');
				if (num >= 0)
				{
					currentGroupIndex = gridItem.GroupIndexInternal.Remove(num);
				}
				else
				{
					currentGroupIndex = gridItem.GroupIndexInternal;
				}
			}
			else
			{
				int num2 = currentGroupIndex.LastIndexOf('_');
				if (num2 >= 0)
				{
					currentGroupIndex = currentGroupIndex.Remove(currentGroupIndex.LastIndexOf('_'));
				}
			}
			return currentGroupIndex;
		}

		// Token: 0x0600B420 RID: 46112 RVA: 0x00276AAC File Offset: 0x00274CAC
		protected void SetChildrenVisibility(bool visible)
		{
			GridTable gridTable = base.OwnerTableView.GetGridTable();
			int i = this.RowIndex;
			bool flag = visible;
			bool flag2 = visible;
			while (i < gridTable.Rows.Count - base.GroupLevel)
			{
				GridItem gridItem = gridTable.Rows[i++] as GridItem;
				GridGroupHeaderItem gridGroupHeaderItem = gridItem as GridGroupHeaderItem;
				if (gridGroupHeaderItem != null && gridItem.GroupLevel < base.GroupLevel)
				{
					return;
				}
				if (gridItem is GridNestedViewItem && base.OwnerTableView.GroupLoadMode == GridGroupLoadMode.Client && base.OwnerTableView.HierarchyLoadMode == GridChildLoadMode.ServerOnDemand)
				{
					gridItem.SetVisibility(true);
				}
				else if (!(gridItem is GridGroupFooterItem) || !flag || !base.OwnerTableView.OwnerGrid.GroupingSettings.RetainGroupFootersVisibility)
				{
					gridItem.SetVisibility(visible);
				}
				if (gridGroupHeaderItem != null)
				{
					if (base.OwnerTableView.GroupHeaderFooterIndexMap.ContainsKey(gridGroupHeaderItem.GroupIndexInternal))
					{
						GridGroupFooterItem gridGroupFooterItem = base.OwnerTableView.GroupHeaderFooterIndexMap[gridGroupHeaderItem.GroupIndexInternal];
						gridGroupFooterItem.SetVisibility(visible && base.OwnerTableView.OwnerGrid.GroupingSettings.RetainGroupFootersVisibility);
					}
					flag2 = (flag2 && gridGroupHeaderItem.Expanded);
					visible = flag2;
				}
			}
		}

		// Token: 0x0600B421 RID: 46113 RVA: 0x00276BE4 File Offset: 0x00274DE4
		protected GridGroupHeaderItem GetHeaderItemByGroupIndex(string GroupIndex)
		{
			GridItem[] items = base.OwnerTableView.GetItems(new GridItemType[]
			{
				GridItemType.GroupHeader
			});
			foreach (GridGroupHeaderItem gridGroupHeaderItem in items)
			{
				if (gridGroupHeaderItem.GroupIndexInternal == GroupIndex)
				{
					return gridGroupHeaderItem;
				}
			}
			return null;
		}

		// Token: 0x04002F60 RID: 12128
		private TableCell _dataCell;

		// Token: 0x04002F61 RID: 12129
		private int calculatedColumnIndex;

		// Token: 0x04002F62 RID: 12130
		private ListDictionary aggregatesValues;
	}
}
