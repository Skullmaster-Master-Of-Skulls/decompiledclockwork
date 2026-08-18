using System;
using System.Linq;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x02000DE4 RID: 3556
	public class PivotGridRowHeaderItem : PivotGridHeaderItem
	{
		// Token: 0x0600840A RID: 33802 RVA: 0x001E1A46 File Offset: 0x001DFC46
		public PivotGridRowHeaderItem(RadPivotGrid ownerPivotGrid, PivotGridItemType itemType, bool isDataBinding) : base(ownerPivotGrid, itemType, isDataBinding)
		{
		}

		// Token: 0x170029BA RID: 10682
		// (get) Token: 0x0600840B RID: 33803 RVA: 0x001E1A51 File Offset: 0x001DFC51
		protected override string ExpandCollapseButtonIDPrefix
		{
			get
			{
				return "RowExpandCollapseButton_";
			}
		}

		// Token: 0x170029BB RID: 10683
		// (get) Token: 0x0600840C RID: 33804 RVA: 0x001E1A58 File Offset: 0x001DFC58
		protected override string HeaderLabelIDPrefix
		{
			get
			{
				return "RowHeaderLabel_";
			}
		}

		// Token: 0x170029BC RID: 10684
		// (get) Token: 0x0600840D RID: 33805 RVA: 0x001E1A5F File Offset: 0x001DFC5F
		protected override string CommandArgPrefix
		{
			get
			{
				return "0_";
			}
		}

		// Token: 0x0600840E RID: 33806 RVA: 0x001E1A66 File Offset: 0x001DFC66
		protected override PivotGridCell CreateCell()
		{
			return new PivotGridRowHeaderCell(base.OwnerPivotGrid);
		}

		// Token: 0x0600840F RID: 33807 RVA: 0x001E1AB4 File Offset: 0x001DFCB4
		internal override void InitializeCell(PivotGridCell pivotGridCell, PivotGridModelCellBase modelBaseCell, PivotGridDataItem row, bool dataBinding)
		{
			if (pivotGridCell is PivotGridDataCell)
			{
				base.InitializeCell(pivotGridCell, modelBaseCell, row, dataBinding);
				return;
			}
			PivotGridRowHeaderCell pivotGridRowHeaderCell = pivotGridCell as PivotGridRowHeaderCell;
			PivotGridModelCell modelCell = modelBaseCell as PivotGridModelCell;
			if (!dataBinding)
			{
				PivotGridField pivotGridField = (from f in base.OwnerPivotGrid.Fields
				where f.UniqueName == modelCell.FieldName && !f.IsHidden
				select f).FirstOrDefault<PivotGridField>();
				if (pivotGridField != null)
				{
					pivotGridRowHeaderCell.Field = (modelCell.Field = pivotGridField);
				}
			}
			PivotGridRowField pivotGridRowField = modelCell.Field as PivotGridRowField;
			PivotGridAggregateField pivotGridAggregateField = modelCell.Field as PivotGridAggregateField;
			if (pivotGridRowField != null && pivotGridRowField.CellTemplate != null && !modelCell.IsTotalCell)
			{
				pivotGridRowHeaderCell.TemplateType = TemplateType.Row;
			}
			else if (pivotGridAggregateField != null && pivotGridAggregateField.HeaderCellTemplate != null)
			{
				pivotGridRowHeaderCell.TemplateType = TemplateType.RowAggregateHeader;
			}
			else if (pivotGridRowField != null && pivotGridRowField.TotalHeaderCellTemplate != null)
			{
				pivotGridRowHeaderCell.TemplateType = TemplateType.RowTotalHeader;
			}
			int value = base.OwnerPivotGrid.OuterTable.Rows.OfType<PivotGridRowHeaderItem>().Count<PivotGridRowHeaderItem>();
			if (modelCell.IsGrandTotalCell && row.FirstGrandTotalRowIndex == null)
			{
				row.FirstGrandTotalRowIndex = new int?(value);
			}
			base.InitializeExpandCollapseButton(modelCell, pivotGridRowHeaderCell);
			if (modelCell.ShouldCreateExpandCollapseButton && pivotGridRowHeaderCell.TemplateType == TemplateType.Row)
			{
				pivotGridRowField.CellTemplate.InstantiateIn(pivotGridRowHeaderCell);
				pivotGridRowHeaderCell.HasInstantiatedTemplate = true;
			}
			base.SetSpansOnCell(modelCell, pivotGridRowHeaderCell);
			base.CopyProperties(modelCell, pivotGridRowHeaderCell);
			row.CallOnCellCreated(pivotGridRowHeaderCell);
			if (!modelCell.ShouldCreateExpandCollapseButton)
			{
				if (pivotGridRowHeaderCell.TemplateType == TemplateType.RowTotalHeader && modelCell.IsTotalCell)
				{
					pivotGridRowField.TotalHeaderCellTemplate.InstantiateIn(pivotGridRowHeaderCell);
					pivotGridRowHeaderCell.HasInstantiatedTemplate = true;
				}
				else if (modelCell.IsGrandTotalCell)
				{
					int num = base.OwnerPivotGrid.RowHeaderModel.Rows.Count - row.FirstGrandTotalRowIndex.Value;
					IOrderedEnumerable<PivotGridAggregateField> source = from f in base.OwnerPivotGrid.Fields.OfType<PivotGridAggregateField>()
					where !f.IsHidden
					orderby f.ZoneIndex descending
					select f;
					if (source.Count<PivotGridAggregateField>() > 0)
					{
						pivotGridAggregateField = source.ElementAt(num % source.Count<PivotGridAggregateField>());
						if (pivotGridAggregateField != null && pivotGridAggregateField.RowGrandTotalHeaderCellTemplate != null)
						{
							pivotGridRowHeaderCell.TemplateType = TemplateType.RowGrandTotalHeader;
							pivotGridAggregateField.RowGrandTotalHeaderCellTemplate.InstantiateIn(pivotGridRowHeaderCell);
							pivotGridRowHeaderCell.HasInstantiatedTemplate = true;
						}
					}
				}
				else if (pivotGridRowHeaderCell.TemplateType == TemplateType.RowAggregateHeader)
				{
					pivotGridAggregateField.HeaderCellTemplate.InstantiateIn(pivotGridRowHeaderCell);
					pivotGridRowHeaderCell.HasInstantiatedTemplate = true;
				}
				else if (pivotGridRowHeaderCell.TemplateType == TemplateType.Row)
				{
					pivotGridRowField.CellTemplate.InstantiateIn(pivotGridRowHeaderCell);
					pivotGridRowHeaderCell.HasInstantiatedTemplate = true;
				}
			}
			base.InitializeExpandCollapseLabel(modelCell, pivotGridRowHeaderCell);
			if (dataBinding)
			{
				pivotGridRowHeaderCell.DataItem = modelCell.Name;
				pivotGridRowHeaderCell.Field = modelCell.Field;
				pivotGridRowHeaderCell.ParentIndexes = modelCell.RowIndexes;
			}
			pivotGridRowHeaderCell.DataBinding += base.OnCellDataBinding;
			if (base.OwnerPivotGrid.RowTableLayout != PivotGridLayout.Tabular)
			{
				for (int i = 0; i < modelCell.GroupLevel; i++)
				{
					this.Cells.AddAt(0, new TableCell
					{
						Text = "&nbsp;"
					});
				}
			}
		}

		// Token: 0x06008410 RID: 33808 RVA: 0x001E1E3C File Offset: 0x001E003C
		public PivotGridDataZone GetDataZone()
		{
			foreach (object obj in this.Cells)
			{
				PivotGridDataZone pivotGridDataZone = obj as PivotGridDataZone;
				if (pivotGridDataZone != null)
				{
					return pivotGridDataZone;
				}
			}
			return null;
		}
	}
}
