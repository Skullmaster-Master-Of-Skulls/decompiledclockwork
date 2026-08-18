using System;

namespace Telerik.Web.UI
{
	// Token: 0x0200076E RID: 1902
	public class PivotGridNoRecordsItem : PivotGridItem
	{
		// Token: 0x06004328 RID: 17192 RVA: 0x000D1D5A File Offset: 0x000CFF5A
		public PivotGridNoRecordsItem(RadPivotGrid ownerPivotGrid, PivotGridItemType itemType, bool isDataBinding) : base(ownerPivotGrid, itemType, isDataBinding)
		{
		}

		// Token: 0x06004329 RID: 17193 RVA: 0x000D1D68 File Offset: 0x000CFF68
		internal override void Initialize()
		{
			base.OwnerPivotGrid.Items.Add(this);
			PivotGridTableCell pivotGridTableCell = new PivotGridTableCell();
			this.Cells.Add(pivotGridTableCell);
			int count = base.OwnerPivotGrid.Fields.GetFieldsByType("PivotGridAggregateField").Count;
			if (base.OwnerPivotGrid.DataModel.Rows.Count > 0 && base.OwnerPivotGrid.DataModel.Rows[0].Cells.Count > 1)
			{
				this.Cells[0].ColumnSpan = base.OwnerPivotGrid.DataModel.Rows[0].Cells.Count;
			}
			else if (count > 0)
			{
				this.Cells[0].ColumnSpan = count;
			}
			base.OwnerPivotGrid.GetNoRecordsTemplate().InstantiateIn(pivotGridTableCell);
		}
	}
}
