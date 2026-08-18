using System;
using System.Linq;

namespace Telerik.Web.UI
{
	// Token: 0x02000DD7 RID: 3543
	public class PivotGridAggregateItem : PivotGridItem
	{
		// Token: 0x17002991 RID: 10641
		// (get) Token: 0x060083A3 RID: 33699 RVA: 0x001DFF20 File Offset: 0x001DE120
		// (set) Token: 0x060083A4 RID: 33700 RVA: 0x001DFF28 File Offset: 0x001DE128
		public PivotGridAggregateZone AggregateZone { get; protected set; }

		// Token: 0x17002992 RID: 10642
		// (get) Token: 0x060083A5 RID: 33701 RVA: 0x001DFF31 File Offset: 0x001DE131
		// (set) Token: 0x060083A6 RID: 33702 RVA: 0x001DFF39 File Offset: 0x001DE139
		public PivotGridColumnZone ColumnZone { get; protected set; }

		// Token: 0x060083A7 RID: 33703 RVA: 0x001DFF7C File Offset: 0x001DE17C
		internal override void Initialize()
		{
			this.AggregateZone = (PivotGridAggregateZone)this.CreateCellObject();
			this.AggregateZone.ColumnSpan = (from f in base.OwnerPivotGrid.Fields
			where f is PivotGridRowField && !f.IsHidden
			select f).Sum((PivotGridField f) => (f as PivotGridRowField).ColumnSpan);
			if (base.OwnerPivotGrid.AggregatesPosition == PivotGridAxis.Rows)
			{
				if ((from field in base.OwnerPivotGrid.Fields
				where field is PivotGridAggregateField && !field.IsHidden
				select field).Count<PivotGridField>() > 1)
				{
					this.AggregateZone.ColumnSpan++;
				}
			}
			this.zoneType = PivotGridZoneType.Column;
			this.ColumnZone = (PivotGridColumnZone)this.CreateCellObject();
			this.Cells.Add(this.AggregateZone);
			this.AggregateZone.Initialize();
			this.Cells.Add(this.ColumnZone);
			if (base.OwnerPivotGrid.ClientSettings.Scrolling.AllowVerticalScroll)
			{
				this.ColumnZone.ColumnSpan = 2;
			}
			this.ColumnZone.Initialize();
			this.CallOnItemCreated();
			base.OwnerPivotGrid.Items.Add(this);
			if (this.IsDataBinding)
			{
				this.DataBind();
				this.CallOnItemDataBound();
			}
		}

		// Token: 0x060083A8 RID: 33704 RVA: 0x001E00E9 File Offset: 0x001DE2E9
		protected override PivotGridTableCell CreateCellObject()
		{
			if (this.zoneType == PivotGridZoneType.Aggregate)
			{
				return new PivotGridAggregateZone(base.OwnerPivotGrid);
			}
			return new PivotGridColumnZone(base.OwnerPivotGrid);
		}

		// Token: 0x060083A9 RID: 33705 RVA: 0x001E010B File Offset: 0x001DE30B
		public PivotGridAggregateItem(RadPivotGrid ownerPivotGrid, PivotGridItemType itemType, bool isDataBinding) : base(ownerPivotGrid, itemType, isDataBinding)
		{
		}

		// Token: 0x04002476 RID: 9334
		private PivotGridZoneType zoneType = PivotGridZoneType.Aggregate;
	}
}
