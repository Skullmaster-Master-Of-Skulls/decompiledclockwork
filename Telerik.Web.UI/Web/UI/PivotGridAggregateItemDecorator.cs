using System;
using System.Collections.Generic;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x02000DC4 RID: 3524
	public class PivotGridAggregateItemDecorator : PivotGridItemDecorator
	{
		// Token: 0x06008379 RID: 33657 RVA: 0x001DF6A6 File Offset: 0x001DD8A6
		public PivotGridAggregateItemDecorator(PivotGridItem item) : base(item)
		{
		}

		// Token: 0x0600837A RID: 33658 RVA: 0x001DF6B0 File Offset: 0x001DD8B0
		public override void DecorateItem(RadPivotGrid owner)
		{
			this.SetItemStyle(owner);
			PivotGridAggregateZone aggregateZone = ((PivotGridAggregateItem)base.Item).AggregateZone;
			aggregateZone.CssClass = "rpgDataZone";
			List<Control> allControls = ChildControlHelper.GetAllControls(new List<Control>(), typeof(PivotGridFieldRenderingControl), aggregateZone);
			foreach (Control control in allControls)
			{
				PivotGridFieldRenderingControl pivotGridFieldRenderingControl = control as PivotGridFieldRenderingControl;
				if (pivotGridFieldRenderingControl != null)
				{
					pivotGridFieldRenderingControl.PrepareFieldRenderingControlStyle();
				}
			}
			PivotGridColumnZone columnZone = ((PivotGridAggregateItem)base.Item).ColumnZone;
			columnZone.CssClass = "rpgColumnsZone";
			allControls = ChildControlHelper.GetAllControls(new List<Control>(), typeof(PivotGridFieldRenderingControl), columnZone);
			foreach (Control control2 in allControls)
			{
				PivotGridFieldRenderingControl pivotGridFieldRenderingControl2 = control2 as PivotGridFieldRenderingControl;
				if (pivotGridFieldRenderingControl2 != null)
				{
					pivotGridFieldRenderingControl2.PrepareFieldRenderingControlStyle();
				}
			}
		}
	}
}
