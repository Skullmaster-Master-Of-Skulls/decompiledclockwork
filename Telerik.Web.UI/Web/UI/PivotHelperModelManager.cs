using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Telerik.Web.UI.PivotGrid.Core;
using Telerik.Web.UI.PivotGrid.Core.Layouts;
using Telerik.Web.UI.PivotGrid.Core.Olap;
using Telerik.Web.UI.PivotGrid.Core.ViewModels;
using Telerik.Web.UI.PivotGrid.Xmla;

namespace Telerik.Web.UI
{
	// Token: 0x02000E15 RID: 3605
	internal class PivotHelperModelManager
	{
		// Token: 0x06008700 RID: 34560 RVA: 0x001E9320 File Offset: 0x001E7520
		public PivotHelperModelManager(RadPivotGrid ownerGrid)
		{
			this.ownerPivotGrid = ownerGrid;
			this.rowGroups = new List<GroupNode>();
			this.columnGroups = new List<GroupNode>();
		}

		// Token: 0x06008701 RID: 34561 RVA: 0x001E93A0 File Offset: 0x001E75A0
		[SuppressMessage("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
		public void BuildRowPivotModel()
		{
			int totalItemCount = this.ownerPivotGrid.TotalItemCount;
			int num = this.ownerPivotGrid.PageSize * this.ownerPivotGrid.CurrentPageIndex;
			if (num >= totalItemCount)
			{
				int num2 = totalItemCount % this.ownerPivotGrid.PageSize;
				num = totalItemCount - num2;
				if (num < 0)
				{
					num = 0;
				}
			}
			int num3 = num + this.ownerPivotGrid.PageSize;
			BaseLayout rowLayout = this.ownerPivotGrid.RowLayout;
			PivotGridRowHeadersModel rowHeaderModel = this.ownerPivotGrid.RowHeaderModel;
			PivotViewModel pivotModel = this.ownerPivotGrid.PivotModel;
			PivotGridFieldsCollection fields = this.ownerPivotGrid.Fields;
			if (!this.ownerPivotGrid.AllowPaging && this.ownerPivotGrid.IsDataBinding)
			{
				num = 0;
				num3 = this.ownerPivotGrid.TotalItemCount;
			}
			rowHeaderModel.Clear();
			if (this.ownerPivotGrid.RowLayoutLevelsCount == 0)
			{
				if ((this.ownerPivotGrid.TotalsSettings.GrandTotalsVisibility == PivotGridGrandTotalsVisibility.RowsAndColumns || this.ownerPivotGrid.TotalsSettings.GrandTotalsVisibility == PivotGridGrandTotalsVisibility.RowsOnly) && this.ownerPivotGrid.TotalsSettings.RowGrandTotalsPosition != TotalsPosition.None)
				{
					this.AddGrandTotal(rowHeaderModel, this.rowGroups);
				}
				return;
			}
			PivotGridModelCell[] array = new PivotGridModelCell[this.ownerPivotGrid.RowLayoutLevelsCount];
			int num4 = num3 - num;
			if (this.ownerPivotGrid.TotalsSettings.RowGrandTotalsPosition == TotalsPosition.First && num == 0)
			{
				num4++;
			}
			List<IList<ItemInfo>> list = rowLayout.GetLines(num, true).Take(num4).ToList<IList<ItemInfo>>();
			int[] array2 = new int[this.ownerPivotGrid.RowLayoutLevelsCount];
			List<PivotGridField> source;
			if (pivotModel.DataProvider.AggregatesPosition == PivotAxis.Rows)
			{
				source = (from f in fields
				where f is PivotGridRowField || f is PivotGridAggregateField
				orderby f.ZoneIndex
				select f).ToList<PivotGridField>();
			}
			else
			{
				source = (from f in fields
				where f is PivotGridRowField
				orderby f.ZoneIndex
				select f).ToList<PivotGridField>();
			}
			List<PivotGridField> list2 = (from f in source
			where !f.IsHidden
			orderby f.ZoneIndex
			select f into fa
			orderby fa.GetType().Name descending
			select fa).ToList<PivotGridField>();
			for (int i = 0; i < list.Count; i++)
			{
				List<ItemInfo> list3;
				if (i == 0)
				{
					list3 = list[i].ToList<ItemInfo>();
				}
				else
				{
					list3 = (from g in list[i]
					where g.IsDisplayed
					select g).ToList<ItemInfo>();
				}
				PivotGridModelRow pivotGridModelRow = new PivotGridModelRow();
				rowHeaderModel.Rows.Add(pivotGridModelRow);
				int count = list3.Count;
				for (int j = 0; j < count; j++)
				{
					ItemInfo itemInfo = list3[j];
					if (itemInfo.Level > this.ownerPivotGrid.RowLayoutLevelsCount)
					{
						this.ownerPivotGrid.RowLayoutLevelsCount = itemInfo.Level;
					}
					if (itemInfo.ItemType == GroupType.GrandTotal)
					{
						rowHeaderModel.Rows.Remove(pivotGridModelRow);
					}
					else
					{
						PivotGridModelCell pivotGridModelCell = new PivotGridModelCell();
						if (list2.Count > 0 && (itemInfo.Item as IGroup).Type != GroupType.GrandTotal)
						{
							pivotGridModelCell.Field = this.GetGroupDescription(itemInfo.Item as IGroup, PivotGridAxis.Rows, list2);
							pivotGridModelCell.FieldName = pivotGridModelCell.Field.UniqueName;
						}
						pivotGridModelCell.FieldName = pivotGridModelCell.Field.UniqueName;
						pivotGridModelCell.RowIndexes = (itemInfo.Item as IGroup).GetGroupIndex();
						pivotGridModelCell.HasChildren = itemInfo.IsCollapsible;
						pivotGridModelCell.IsTotalCell = (itemInfo.ItemType == GroupType.Subtotal);
						pivotGridModelCell.IsGrandTotalCell = (itemInfo.ItemType == GroupType.GrandTotal);
						pivotModel.GetRowGroupDescription(itemInfo.Item as IGroup);
						bool flag = this.ownerPivotGrid.RowTableLayout == PivotGridLayout.Tabular;
						pivotGridModelCell.IsCollapsed = itemInfo.IsCollapsed;
						pivotGridModelCell.ShouldCreateExpandCollapseButton = itemInfo.IsCollapsible;
						if (j == count - 1)
						{
							this.rowGroups.Add(new GroupNode
							{
								Group = (itemInfo.Item as IGroup),
								isCollapsed = itemInfo.IsCollapsed
							});
						}
						pivotGridModelRow.Cells.Add(pivotGridModelCell);
						pivotGridModelCell.Name = (itemInfo.Item as IGroup).Name;
						pivotGridModelCell.GroupLevel = (itemInfo.Item as IGroup).Level;
						if (itemInfo.ItemType == GroupType.Subtotal && pivotGridModelCell.GroupLevel > 0 && (array[pivotGridModelCell.GroupLevel - 1] == null || !array[pivotGridModelCell.GroupLevel - 1].IsCollapsed))
						{
							pivotGridModelCell.GroupLevel--;
						}
						pivotGridModelCell.Slot = itemInfo.LayoutInfo.Line;
						int rowLayoutLevelsCount = this.ownerPivotGrid.RowLayoutLevelsCount;
						if (j < count - 1)
						{
							pivotGridModelCell.ColSpan = 1;
						}
						else
						{
							pivotGridModelCell.ColSpan = rowLayoutLevelsCount - pivotGridModelCell.GroupLevel;
						}
						if (flag)
						{
							for (int k = pivotGridModelCell.GroupLevel; k < this.ownerPivotGrid.RowLayoutLevelsCount - 1; k++)
							{
								if (array[k] != null)
								{
									array[k].RowSpan = rowHeaderModel.Rows.Count - array2[k];
									array[k] = null;
								}
								array2[k] = rowHeaderModel.Rows.Count;
							}
							array[pivotGridModelCell.GroupLevel] = pivotGridModelCell;
						}
					}
				}
			}
			for (int l = 0; l < this.ownerPivotGrid.RowLayoutLevelsCount - 1; l++)
			{
				if (array[l] != null)
				{
					array[l].RowSpan = 1 + rowHeaderModel.Rows.Count - array2[l];
				}
			}
			if ((this.ownerPivotGrid.TotalsSettings.GrandTotalsVisibility == PivotGridGrandTotalsVisibility.RowsAndColumns || this.ownerPivotGrid.TotalsSettings.GrandTotalsVisibility == PivotGridGrandTotalsVisibility.RowsOnly) && this.ownerPivotGrid.TotalsSettings.RowGrandTotalsPosition != TotalsPosition.None)
			{
				this.AddGrandTotal(rowHeaderModel, this.rowGroups);
			}
		}

		// Token: 0x06008702 RID: 34562 RVA: 0x001E9A6C File Offset: 0x001E7C6C
		public void BuildColumnsPivotModel()
		{
			PivotGridRowHeadersModel columnHeadersModel = this.ownerPivotGrid.ColumnHeadersModel;
			BaseLayout columnLayout = this.ownerPivotGrid.ColumnLayout;
			int visibleLineCount = columnLayout.VisibleLineCount;
			int num = this.CreateModelRows();
			List<IList<ItemInfo>> list = columnLayout.GetLines(0, true).ToList<IList<ItemInfo>>();
			for (int i = 0; i < visibleLineCount; i++)
			{
				List<ItemInfo> list2 = (from g in list[i]
				where g.IsDisplayed
				select g).ToList<ItemInfo>();
				for (int j = 0; j < list2.Count; j++)
				{
					ItemInfo itemInfo = list2[j];
					if ((this.ownerPivotGrid.TotalsSettings.GrandTotalsVisibility != PivotGridGrandTotalsVisibility.RowsOnly && this.ownerPivotGrid.TotalsSettings.GrandTotalsVisibility != PivotGridGrandTotalsVisibility.None) || itemInfo.ItemType != GroupType.GrandTotal)
					{
						PivotGridModelCell pivotGridModelCell = new PivotGridModelCell();
						pivotGridModelCell.ColumnIndexes = (itemInfo.Item as IGroup).GetGroupIndex();
						List<PivotGridField> list3;
						if (this.ownerPivotGrid.PivotModel.DataProvider.AggregatesPosition == PivotAxis.Columns)
						{
							list3 = (from f in this.ownerPivotGrid.Fields
							where (f is PivotGridColumnField || f is PivotGridAggregateField) && !f.IsHidden
							orderby f.ZoneIndex
							orderby f.GetType().ToString() descending
							select f).ToList<PivotGridField>();
						}
						else
						{
							list3 = (from f in this.ownerPivotGrid.Fields
							where f is PivotGridColumnField && !f.IsHidden
							orderby f.ZoneIndex
							select f).ToList<PivotGridField>();
						}
						if (list3.Count > 0 && (itemInfo.Item as IGroup).Type != GroupType.GrandTotal)
						{
							pivotGridModelCell.Field = this.GetGroupDescription(itemInfo.Item as IGroup, PivotGridAxis.Columns, list3);
							pivotGridModelCell.FieldName = pivotGridModelCell.Field.UniqueName;
						}
						pivotGridModelCell.HasChildren = itemInfo.IsCollapsible;
						pivotGridModelCell.IsTotalCell = (itemInfo.ItemType == GroupType.Subtotal);
						pivotGridModelCell.IsGrandTotalCell = (itemInfo.ItemType == GroupType.GrandTotal);
						pivotGridModelCell.Name = (itemInfo.Item as IGroup).Name;
						pivotGridModelCell.Slot = itemInfo.LayoutInfo.Line;
						pivotGridModelCell.GroupLevel = itemInfo.LayoutInfo.Level;
						int num2 = itemInfo.Level;
						int rowSpan = 1;
						if (itemInfo.ItemType == GroupType.Subtotal && !columnLayout.IsCollapsed((itemInfo.Item as IGroup).Parent))
						{
							num2--;
							rowSpan = num - itemInfo.Level + 1;
						}
						else if (itemInfo.ItemType == GroupType.Subtotal)
						{
							num2 = num - 1;
						}
						if (itemInfo.ItemType == GroupType.GrandTotal)
						{
							rowSpan = num;
						}
						if (num2 < columnHeadersModel.Rows.Count)
						{
							columnHeadersModel.Rows[num2].Cells.Add(pivotGridModelCell);
						}
						bool isCollapsed = itemInfo.IsCollapsed;
						if ((num2 < num - 1 || isCollapsed) && itemInfo.ItemType != GroupType.Subtotal && itemInfo.ItemType != GroupType.GrandTotal)
						{
							pivotGridModelCell.IsCollapsed = isCollapsed;
							pivotGridModelCell.ShouldCreateExpandCollapseButton = itemInfo.IsCollapsible;
						}
						if (j == list2.Count - 1)
						{
							this.columnGroups.Add(new GroupNode
							{
								Group = (itemInfo.Item as IGroup),
								isCollapsed = isCollapsed
							});
						}
						int num3 = (from gr in (itemInfo.Item as IGroup).Groups
						where gr.Type == GroupType.Subtotal || gr.Type == GroupType.GrandTotal
						select gr).Count<IGroup>();
						if (isCollapsed && num3 > 1)
						{
							pivotGridModelCell.ColSpan = num3;
						}
						else
						{
							pivotGridModelCell.ColSpan = this.GetColSpan(itemInfo.Item as IGroup, 0) - num3;
						}
						if (this.ownerPivotGrid.TotalsSettings.ColumnsSubTotalsPosition == TotalsPosition.None && isCollapsed)
						{
							pivotGridModelCell.ColSpan = 1;
						}
						if (!isCollapsed)
						{
							pivotGridModelCell.RowSpan = rowSpan;
						}
						else
						{
							pivotGridModelCell.RowSpan = num - itemInfo.Level;
						}
						if (isCollapsed && num3 > 1)
						{
							pivotGridModelCell.RowSpan += -1;
						}
					}
				}
			}
		}

		// Token: 0x06008703 RID: 34563 RVA: 0x001E9F28 File Offset: 0x001E8128
		private PivotGridField GetGroupDescription(IGroup group, PivotGridAxis axis, IEnumerable<PivotGridField> fields)
		{
			IGroupDescription groupDescription = null;
			for (;;)
			{
				switch (axis)
				{
				case PivotGridAxis.Rows:
					groupDescription = this.ownerPivotGrid.PivotModel.GetRowGroupDescription(group);
					break;
				case PivotGridAxis.Columns:
					groupDescription = this.ownerPivotGrid.PivotModel.GetColumnGroupDescription(group);
					break;
				}
				if ((groupDescription != null && (group.Type == GroupType.BottomLevel || group.Type == GroupType.Subheading || groupDescription.DisplayName == "Values")) || group.Parent == null)
				{
					break;
				}
				group = group.Parent;
			}
			PropertyGroupDescription propertyGroupDescription = groupDescription as PropertyGroupDescription;
			int index = group.Level;
			if ((propertyGroupDescription != null && propertyGroupDescription.PropertyName == "Values") || groupDescription.DisplayName == "Values")
			{
				index = ((GroupNode)group).AggregateIndex;
				if (fields.Any((PivotGridField f) => f is PivotGridAggregateField) && (group.Type == GroupType.Subheading || group.Type == GroupType.BottomLevel || (group.Type == GroupType.Subtotal && axis == PivotGridAxis.Columns)))
				{
					return fields.FirstOrDefault((PivotGridField f) => f is PivotGridAggregateField && f.DescriptorIndex == index);
				}
			}
			IPivotResults results = this.ownerPivotGrid.PivotModel.DataProvider.Results;
			IReadOnlyList<GroupDescription> readOnlyList = (axis == PivotGridAxis.Rows) ? results.RowGroupDescriptions : results.ColumnGroupDescriptions;
			for (int i = 0; i < readOnlyList.Count; i++)
			{
				if (readOnlyList[i] == groupDescription)
				{
					index = i;
					break;
				}
			}
			for (int j = 0; j < readOnlyList.Count; j++)
			{
				XmlaGroupDescription xmlaGroupDescription = readOnlyList[j] as XmlaGroupDescription;
				if (xmlaGroupDescription != null)
				{
					index -= xmlaGroupDescription.Levels.Count;
				}
			}
			index = Math.Max(index, 0);
			return fields.FirstOrDefault((PivotGridField f) => !(f is PivotGridAggregateField) && f.DescriptorIndex == index);
		}

		// Token: 0x06008704 RID: 34564 RVA: 0x001EA11B File Offset: 0x001E831B
		private IGroupDescription GetAggregateDescription()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06008705 RID: 34565 RVA: 0x001EA124 File Offset: 0x001E8324
		private int CreateModelRows()
		{
			PivotGridRowHeadersModel columnHeadersModel = this.ownerPivotGrid.ColumnHeadersModel;
			BaseLayout columnLayout = this.ownerPivotGrid.ColumnLayout;
			columnHeadersModel.Clear();
			int num = Math.Max(this.ownerPivotGrid.PivotModel.ColumnLevels, 1);
			for (int i = 0; i < num; i++)
			{
				PivotGridModelRow item = new PivotGridModelRow();
				columnHeadersModel.Rows.Add(item);
				if (columnLayout.VisibleLineCount - 1 == this.ownerPivotGrid.CollapsedColumnIndexes.Count)
				{
					break;
				}
			}
			if (columnLayout.VisibleLineCount == 0 && (this.ownerPivotGrid.TotalsSettings.GrandTotalsVisibility == PivotGridGrandTotalsVisibility.RowsAndColumns || this.ownerPivotGrid.TotalsSettings.GrandTotalsVisibility == PivotGridGrandTotalsVisibility.ColumnsOnly) && this.ownerPivotGrid.TotalsSettings.ColumnGrandTotalsPosition != TotalsPosition.None)
			{
				PivotGridModelCell pivotGridModelCell = new PivotGridModelCell();
				columnHeadersModel.Rows[0].Cells.Add(pivotGridModelCell);
				pivotGridModelCell.Name = this.ownerPivotGrid.Localization.GrandTotalText;
				pivotGridModelCell.IsGrandTotalCell = true;
			}
			return num;
		}

		// Token: 0x06008706 RID: 34566 RVA: 0x001EA224 File Offset: 0x001E8424
		public void BuildDataPivotModel()
		{
			this.ownerPivotGrid.DataModel.Clear();
			int num = 0;
			foreach (GroupNode rowGroup in this.rowGroups)
			{
				PivotGridModelDataRow pivotGridModelDataRow = new PivotGridModelDataRow();
				this.ownerPivotGrid.DataModel.Rows.Add(pivotGridModelDataRow);
				pivotGridModelDataRow.DisplayIndex = num;
				num++;
				foreach (GroupNode columnGroup in this.columnGroups)
				{
					PivotGridModelDataCell item = this.CreatePivotGridModelDataCell(rowGroup, columnGroup);
					pivotGridModelDataRow.Cells.Add(item);
				}
			}
		}

		// Token: 0x06008707 RID: 34567 RVA: 0x001EA300 File Offset: 0x001E8500
		private PivotGridModelDataCell CreatePivotGridModelDataCell(GroupNode rowGroup, GroupNode columnGroup)
		{
			CellAggregateValue cellAggregateValue = null;
			PivotGridModelDataCell pivotGridModelDataCell = new PivotGridModelDataCell();
			pivotGridModelDataCell.ColumnIndexes = columnGroup.Group.GetGroupIndex();
			if (rowGroup.Group != null)
			{
				pivotGridModelDataCell.RowIndexes = rowGroup.Group.GetGroupIndex();
				pivotGridModelDataCell.CellType = this.MapGroupTypesToDataCellType(rowGroup.Group.Type, columnGroup.Group.Type);
				cellAggregateValue = this.ownerPivotGrid.PivotModel.GetAggregateValue(rowGroup.Group, columnGroup.Group, rowGroup.isCollapsed, columnGroup.isCollapsed);
				object name = string.Empty;
				if (cellAggregateValue != null)
				{
					name = cellAggregateValue.Value;
					pivotGridModelDataCell.FormattedValue = cellAggregateValue.FormattedValue;
				}
				pivotGridModelDataCell.Name = name;
			}
			else
			{
				pivotGridModelDataCell.CellType = PivotGridDataCellType.RowGrandTotalDataCell;
				pivotGridModelDataCell.Name = string.Empty;
			}
			PivotGridField fieldFromGroupNode;
			if (this.ownerPivotGrid.AggregatesPosition == PivotGridAxis.Rows && rowGroup != null)
			{
				fieldFromGroupNode = this.GetFieldFromGroupNode(rowGroup.Group as GroupNode);
			}
			else
			{
				fieldFromGroupNode = this.GetFieldFromGroupNode(columnGroup.Group as GroupNode);
			}
			if (fieldFromGroupNode != null)
			{
				pivotGridModelDataCell.Field = fieldFromGroupNode;
				pivotGridModelDataCell.FieldName = pivotGridModelDataCell.Field.UniqueName;
				PivotGridAggregateField pivotGridAggregateField = fieldFromGroupNode as PivotGridAggregateField;
				pivotGridModelDataCell.DisplayValueAsKpi = pivotGridAggregateField.GroupDescription.DisplayValueAsKpi;
				if (pivotGridModelDataCell.DisplayValueAsKpi)
				{
					pivotGridModelDataCell.KpiType = this.GetKpiTypeFromAggregateField(pivotGridAggregateField);
					if (cellAggregateValue != null && cellAggregateValue.Value != null)
					{
						pivotGridModelDataCell.KpiIndicator = this.GetKpiValue(cellAggregateValue.Value);
					}
				}
			}
			return pivotGridModelDataCell;
		}

		// Token: 0x06008708 RID: 34568 RVA: 0x001EA464 File Offset: 0x001E8664
		private PivotGridDataCellType MapGroupTypesToDataCellType(GroupType rowGroupType, GroupType columnGroupType)
		{
			PivotGridDataCellType result = PivotGridDataCellType.DataCell;
			if (rowGroupType == GroupType.Subtotal && columnGroupType == GroupType.Subtotal)
			{
				result = PivotGridDataCellType.RowAndColumnTotal;
			}
			else if (rowGroupType == GroupType.GrandTotal && columnGroupType == GroupType.GrandTotal)
			{
				result = PivotGridDataCellType.RowAndColumnGrandTotal;
			}
			else if (rowGroupType == GroupType.GrandTotal && columnGroupType == GroupType.Subtotal)
			{
				result = PivotGridDataCellType.RowGrandTotalColumnTotal;
			}
			else if (rowGroupType == GroupType.Subtotal && columnGroupType == GroupType.GrandTotal)
			{
				result = PivotGridDataCellType.ColumnGrandTotalRowTotal;
			}
			else if (rowGroupType == GroupType.GrandTotal)
			{
				result = PivotGridDataCellType.RowGrandTotalDataCell;
			}
			else if (columnGroupType == GroupType.GrandTotal)
			{
				result = PivotGridDataCellType.ColumnGrandTotalDataCell;
			}
			else if (rowGroupType == GroupType.Subtotal)
			{
				result = PivotGridDataCellType.RowTotalDataCell;
			}
			else if (columnGroupType == GroupType.Subtotal)
			{
				result = PivotGridDataCellType.ColumnTotalDataCell;
			}
			return result;
		}

		// Token: 0x06008709 RID: 34569 RVA: 0x001EA4E0 File Offset: 0x001E86E0
		private PivotGridField GetFieldFromGroupNode(GroupNode groupNode)
		{
			PivotGridField result = null;
			if (groupNode != null)
			{
				int aggregateIndex = groupNode.AggregateIndex;
				List<PivotGridField> list = (from f in this.ownerPivotGrid.Fields
				where f is PivotGridAggregateField && !f.IsHidden
				orderby f.ZoneIndex
				select f).ToList<PivotGridField>();
				if (list.Count > 0)
				{
					if (aggregateIndex > list.Count || aggregateIndex < 0)
					{
						result = list[list.Count - 1];
					}
					else
					{
						result = list[aggregateIndex];
					}
				}
			}
			return result;
		}

		// Token: 0x0600870A RID: 34570 RVA: 0x001EA584 File Offset: 0x001E8784
		private PivotGridKpiType GetKpiTypeFromAggregateField(PivotGridAggregateField aggregateField)
		{
			PivotGridKpiType result = PivotGridKpiType.Value;
			string uniqueName = aggregateField.GroupDescription.GetUniqueName();
			if (uniqueName.LastIndexOf("Trend") == uniqueName.Length - 6)
			{
				result = PivotGridKpiType.Trend;
			}
			else if (uniqueName.LastIndexOf("Status") == uniqueName.Length - 7)
			{
				result = PivotGridKpiType.Status;
			}
			else if (uniqueName.LastIndexOf("Goal") == uniqueName.Length - 5)
			{
				result = PivotGridKpiType.Status;
			}
			else if (uniqueName.LastIndexOf("Value") == uniqueName.Length - 6)
			{
				result = PivotGridKpiType.Status;
			}
			return result;
		}

		// Token: 0x0600870B RID: 34571 RVA: 0x001EA604 File Offset: 0x001E8804
		private PivotGridKpiValue GetKpiValue(object value)
		{
			PivotGridKpiValue result = PivotGridKpiValue.NA;
			int num;
			if (int.TryParse(value.ToString(), out num))
			{
				if (num == -1)
				{
					result = PivotGridKpiValue.Down;
				}
				else if (num == 1)
				{
					result = PivotGridKpiValue.Up;
				}
				else if (num == 0)
				{
					result = PivotGridKpiValue.NoChange;
				}
			}
			return result;
		}

		// Token: 0x0600870C RID: 34572 RVA: 0x001EA638 File Offset: 0x001E8838
		public int GetColumnsGroupCount()
		{
			return this.columnGroups.Count;
		}

		// Token: 0x0600870D RID: 34573 RVA: 0x001EA648 File Offset: 0x001E8848
		private int GetColSpan(IGroup group, int sum)
		{
			sum = 0;
			if (!this.ownerPivotGrid.ColumnLayout.IsCollapsed(group))
			{
				if (group.HasGroups)
				{
					using (IEnumerator<IGroup> enumerator = group.Groups.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							IGroup group2 = enumerator.Current;
							sum += this.GetColSpan(group2, sum);
						}
						return sum;
					}
					return 1;
				}
				return 1;
			}
			if (group.Level < this.ownerPivotGrid.AggregatesLevel)
			{
				return this.ownerPivotGrid.PivotModel.AggregateDescriptionCount;
			}
			return 1;
		}

		// Token: 0x0600870E RID: 34574 RVA: 0x001EA6F0 File Offset: 0x001E88F0
		private void AddGrandTotal(PivotGridRowHeadersModel rowHeadersModel, List<GroupNode> rowGroups)
		{
			int num = (this.ownerPivotGrid.AggregatesPosition == PivotGridAxis.Rows) ? this.ownerPivotGrid.PivotModel.AggregateDescriptionCount : 1;
			if (this.ownerPivotGrid.AggregatesPosition == PivotGridAxis.Rows)
			{
				if ((from f in this.ownerPivotGrid.Fields.GetFieldsByType(typeof(PivotGridRowField).Name)
				where !f.IsHidden
				select f).Count<PivotGridField>() == 0 && this.ownerPivotGrid.PivotModel.AggregateDescriptionCount > 1)
				{
					num = 0;
				}
			}
			for (int i = num - 1; i >= 0; i--)
			{
				PivotGridModelRow pivotGridModelRow = this.CreateGrandTotalRow(rowHeadersModel);
				PivotGridModelCell item = this.CreateGrandTotalCell(rowGroups, i);
				pivotGridModelRow.Cells.Add(item);
			}
		}

		// Token: 0x0600870F RID: 34575 RVA: 0x001EA7B4 File Offset: 0x001E89B4
		private PivotGridModelRow CreateGrandTotalRow(PivotGridRowHeadersModel rowHeadersModel)
		{
			PivotGridModelRow pivotGridModelRow = new PivotGridModelRow();
			if (this.ownerPivotGrid.TotalsSettings.RowGrandTotalsPosition == TotalsPosition.First)
			{
				rowHeadersModel.Rows.Insert(0, pivotGridModelRow);
			}
			else
			{
				rowHeadersModel.Rows.Add(pivotGridModelRow);
			}
			return pivotGridModelRow;
		}

		// Token: 0x06008710 RID: 34576 RVA: 0x001EA7F8 File Offset: 0x001E89F8
		private PivotGridModelCell CreateGrandTotalCell(List<GroupNode> rowGroups, int index)
		{
			PivotViewModel pivotModel = this.ownerPivotGrid.PivotModel;
			PivotGridModelCell pivotGridModelCell = new PivotGridModelCell();
			IGroup group = null;
			if (this.ownerPivotGrid.TotalsSettings.RowGrandTotalsPosition == TotalsPosition.First)
			{
				group = pivotModel.RowGroups.ElementAtOrDefault(index);
			}
			else
			{
				group = pivotModel.RowGroups.ElementAtOrDefault(pivotModel.RowGroups.Count - 1 - index);
			}
			if (group != null)
			{
				pivotGridModelCell.Name = group.Name;
			}
			else
			{
				pivotGridModelCell.Name = this.ownerPivotGrid.Localization.GrandTotalText;
			}
			int num = 0;
			foreach (object obj in pivotModel.DataProvider.Settings.RowGroupDescriptions)
			{
				OlapGroupDescription olapGroupDescription = obj as OlapGroupDescription;
				if (olapGroupDescription != null && olapGroupDescription.Levels.Count > 1)
				{
					num += olapGroupDescription.Levels.Count;
				}
				else
				{
					num++;
				}
			}
			num = ((num > 1) ? num : 1);
			if (this.ownerPivotGrid.AggregatesPosition == PivotGridAxis.Rows && this.ownerPivotGrid.PivotModel.AggregateDescriptionCount > 1)
			{
				num++;
			}
			pivotGridModelCell.ColSpan = num;
			pivotGridModelCell.IsGrandTotalCell = true;
			if (this.ownerPivotGrid.TotalsSettings.RowGrandTotalsPosition == TotalsPosition.First)
			{
				rowGroups.Insert(0, new GroupNode
				{
					Group = group,
					isCollapsed = false
				});
			}
			else
			{
				rowGroups.Add(new GroupNode
				{
					Group = group,
					isCollapsed = false
				});
			}
			return pivotGridModelCell;
		}

		// Token: 0x0400254F RID: 9551
		private RadPivotGrid ownerPivotGrid;

		// Token: 0x04002550 RID: 9552
		private List<GroupNode> rowGroups;

		// Token: 0x04002551 RID: 9553
		internal List<GroupNode> columnGroups;
	}
}
