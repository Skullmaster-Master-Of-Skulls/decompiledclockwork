using System;
using System.Linq;

namespace Telerik.Web.UI
{
	// Token: 0x02000DD8 RID: 3544
	public class PivotGridColumnHeaderItem : PivotGridHeaderItem
	{
		// Token: 0x060083AD RID: 33709 RVA: 0x001E011D File Offset: 0x001DE31D
		public PivotGridColumnHeaderItem(RadPivotGrid ownerPivotGrid, PivotGridItemType itemType, bool isDataBinding) : base(ownerPivotGrid, itemType, isDataBinding)
		{
		}

		// Token: 0x17002993 RID: 10643
		// (get) Token: 0x060083AE RID: 33710 RVA: 0x001E0128 File Offset: 0x001DE328
		protected override string ExpandCollapseButtonIDPrefix
		{
			get
			{
				return "ColumnExpandCollapseButton_";
			}
		}

		// Token: 0x17002994 RID: 10644
		// (get) Token: 0x060083AF RID: 33711 RVA: 0x001E012F File Offset: 0x001DE32F
		protected override string HeaderLabelIDPrefix
		{
			get
			{
				return "ColumnHeaderLabel_";
			}
		}

		// Token: 0x17002995 RID: 10645
		// (get) Token: 0x060083B0 RID: 33712 RVA: 0x001E0136 File Offset: 0x001DE336
		protected override string CommandArgPrefix
		{
			get
			{
				return "1_";
			}
		}

		// Token: 0x060083B1 RID: 33713 RVA: 0x001E013D File Offset: 0x001DE33D
		protected override PivotGridCell CreateCell()
		{
			return new PivotGridColumnHeaderCell(base.OwnerPivotGrid);
		}

		// Token: 0x060083B2 RID: 33714 RVA: 0x001E018C File Offset: 0x001DE38C
		internal override void InitializeCell(PivotGridCell pivotGridCell, PivotGridModelCellBase modelBaseCell, PivotGridDataItem row, bool dataBinding)
		{
			if (pivotGridCell is PivotGridDataCell)
			{
				base.InitializeCell(pivotGridCell, modelBaseCell, row, dataBinding);
				return;
			}
			PivotGridColumnHeaderCell pivotGridColumnHeaderCell = pivotGridCell as PivotGridColumnHeaderCell;
			PivotGridModelCell modelCell = modelBaseCell as PivotGridModelCell;
			if (string.IsNullOrEmpty(pivotGridColumnHeaderCell.ID) && base.OwnerPivotGrid.columnHeaderItemsCreatedCount + modelCell.RowSpan == base.OwnerPivotGrid.ColumnHeadersModel.Rows.Count)
			{
				pivotGridColumnHeaderCell.ID = base.OwnerPivotGrid.columnHeaderItemsCreatedCount.ToString() + (this.Cells.Count - 1).ToString();
				base.OwnerPivotGrid.resizableHeaderCells.Add("col" + pivotGridColumnHeaderCell.ID, pivotGridColumnHeaderCell);
			}
			if (!dataBinding)
			{
				PivotGridField pivotGridField = (from f in base.OwnerPivotGrid.Fields
				where f.UniqueName == modelCell.FieldName && !f.IsHidden
				select f).FirstOrDefault<PivotGridField>();
				if (pivotGridField != null)
				{
					pivotGridColumnHeaderCell.Field = (modelCell.Field = pivotGridField);
				}
			}
			PivotGridColumnField pivotGridColumnField = modelCell.Field as PivotGridColumnField;
			PivotGridAggregateField pivotGridAggregateField = modelCell.Field as PivotGridAggregateField;
			if (pivotGridColumnField != null && pivotGridColumnField.CellTemplate != null && !modelCell.IsTotalCell)
			{
				pivotGridColumnHeaderCell.TemplateType = TemplateType.Column;
			}
			else if (pivotGridAggregateField != null && pivotGridAggregateField.HeaderCellTemplate != null)
			{
				pivotGridColumnHeaderCell.TemplateType = TemplateType.ColumnAggregateHeader;
			}
			else if (pivotGridColumnField != null && pivotGridColumnField.TotalHeaderCellTemplate != null)
			{
				pivotGridColumnHeaderCell.TemplateType = TemplateType.ColumnTotalHeader;
			}
			if (modelCell.IsGrandTotalCell && row.FirstGrandTotalCellIndex == null)
			{
				row.FirstGrandTotalCellIndex = new int?(row.Cells.Count);
			}
			base.InitializeExpandCollapseButton(modelCell, pivotGridColumnHeaderCell);
			if (modelCell.ShouldCreateExpandCollapseButton && pivotGridColumnHeaderCell.TemplateType == TemplateType.Column)
			{
				pivotGridColumnField.CellTemplate.InstantiateIn(pivotGridColumnHeaderCell);
				pivotGridColumnHeaderCell.HasInstantiatedTemplate = true;
			}
			base.SetSpansOnCell(modelCell, pivotGridColumnHeaderCell);
			base.CopyProperties(modelCell, pivotGridColumnHeaderCell);
			if (!string.IsNullOrEmpty(pivotGridColumnHeaderCell.ID))
			{
				base.OwnerPivotGrid.resizeableHeaderCellsList.Add(pivotGridColumnHeaderCell.Slot, pivotGridColumnHeaderCell);
			}
			row.CallOnCellCreated(pivotGridColumnHeaderCell);
			if (!modelCell.ShouldCreateExpandCollapseButton)
			{
				if (pivotGridColumnHeaderCell.TemplateType == TemplateType.ColumnAggregateHeader && !modelCell.IsGrandTotalCell)
				{
					pivotGridAggregateField.HeaderCellTemplate.InstantiateIn(pivotGridColumnHeaderCell);
					pivotGridColumnHeaderCell.HasInstantiatedTemplate = true;
				}
				else if (modelCell.IsGrandTotalCell)
				{
					int index = row.Cells.Count - row.FirstGrandTotalCellIndex.Value;
					IOrderedEnumerable<PivotGridAggregateField> source = from f in base.OwnerPivotGrid.Fields.OfType<PivotGridAggregateField>()
					where !f.IsHidden
					orderby f.ZoneIndex
					select f;
					if (source.Count<PivotGridAggregateField>() > 0)
					{
						pivotGridAggregateField = source.ElementAt(index);
						if (pivotGridAggregateField != null && pivotGridAggregateField.ColumnGrandTotalHeaderCellTemplate != null)
						{
							pivotGridColumnHeaderCell.TemplateType = TemplateType.ColumnGrandTotalHeader;
							pivotGridAggregateField.ColumnGrandTotalHeaderCellTemplate.InstantiateIn(pivotGridColumnHeaderCell);
							pivotGridColumnHeaderCell.HasInstantiatedTemplate = true;
						}
					}
				}
				else if (pivotGridColumnHeaderCell.TemplateType == TemplateType.ColumnTotalHeader && modelCell.IsTotalCell)
				{
					pivotGridColumnField.TotalHeaderCellTemplate.InstantiateIn(pivotGridColumnHeaderCell);
					pivotGridColumnHeaderCell.HasInstantiatedTemplate = true;
				}
				else if (pivotGridColumnHeaderCell.TemplateType == TemplateType.Column)
				{
					pivotGridColumnField.CellTemplate.InstantiateIn(pivotGridColumnHeaderCell);
					pivotGridColumnHeaderCell.HasInstantiatedTemplate = true;
				}
			}
			base.InitializeExpandCollapseLabel(modelCell, pivotGridColumnHeaderCell);
			if (dataBinding)
			{
				pivotGridColumnHeaderCell.DataItem = modelCell.Name;
				pivotGridColumnHeaderCell.Field = modelCell.Field;
				pivotGridColumnHeaderCell.ParentIndexes = modelCell.ColumnIndexes;
			}
			pivotGridColumnHeaderCell.DataBinding += base.OnCellDataBinding;
		}

		// Token: 0x060083B3 RID: 33715 RVA: 0x001E0568 File Offset: 0x001DE768
		public PivotGridColumnHeaderZone GetColumnHeaderZone()
		{
			foreach (object obj in this.Cells)
			{
				PivotGridColumnHeaderZone pivotGridColumnHeaderZone = obj as PivotGridColumnHeaderZone;
				if (pivotGridColumnHeaderZone != null)
				{
					return pivotGridColumnHeaderZone;
				}
			}
			return null;
		}
	}
}
