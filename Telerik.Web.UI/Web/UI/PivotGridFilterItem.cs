using System;
using System.Linq;
using Telerik.Web.UI.PivotGrid.Core.ViewModels;

namespace Telerik.Web.UI
{
	// Token: 0x02000DD9 RID: 3545
	public class PivotGridFilterItem : PivotGridItem
	{
		// Token: 0x17002996 RID: 10646
		// (get) Token: 0x060083B6 RID: 33718 RVA: 0x001E05CC File Offset: 0x001DE7CC
		// (set) Token: 0x060083B7 RID: 33719 RVA: 0x001E05D4 File Offset: 0x001DE7D4
		public PivotGridFilterZone FilterZone { get; protected set; }

		// Token: 0x060083B8 RID: 33720 RVA: 0x001E05DD File Offset: 0x001DE7DD
		public PivotGridFilterItem(RadPivotGrid ownerPivotGrid, PivotGridItemType itemType, bool isDataBinding) : base(ownerPivotGrid, itemType, isDataBinding)
		{
		}

		// Token: 0x060083B9 RID: 33721 RVA: 0x001E0620 File Offset: 0x001DE820
		internal override void Initialize()
		{
			this.FilterZone = (PivotGridFilterZone)this.CreateCellObject();
			PivotViewModel pivotModel = base.OwnerPivotGrid.PivotModel;
			int num = (from f in base.OwnerPivotGrid.Fields
			where f is PivotGridRowField && !f.IsHidden
			select f).Sum((PivotGridField f) => (f as PivotGridRowField).ColumnSpan);
			int num2 = 1 + num;
			if (base.OwnerPivotGrid.ClientSettings.Scrolling.AllowVerticalScroll)
			{
				num2++;
			}
			if (base.OwnerPivotGrid.AggregatesPosition == PivotGridAxis.Rows)
			{
				if ((from field in base.OwnerPivotGrid.Fields
				where field is PivotGridAggregateField && !field.IsHidden
				select field).Count<PivotGridField>() > 1)
				{
					num2++;
				}
			}
			this.FilterZone.ColumnSpan = Math.Max(2, num2);
			this.Cells.Add(this.FilterZone);
			this.FilterZone.Initialize();
			this.CallOnItemCreated();
			base.OwnerPivotGrid.Items.Add(this);
		}

		// Token: 0x060083BA RID: 33722 RVA: 0x001E0745 File Offset: 0x001DE945
		protected override PivotGridTableCell CreateCellObject()
		{
			return new PivotGridFilterZone(base.OwnerPivotGrid);
		}
	}
}
