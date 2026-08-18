using System;
using System.Collections.Generic;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x02000DC7 RID: 3527
	public class PivotGridFilterItemDecorator : PivotGridItemDecorator
	{
		// Token: 0x0600837E RID: 33662 RVA: 0x001DF958 File Offset: 0x001DDB58
		public PivotGridFilterItemDecorator(PivotGridItem item) : base(item)
		{
		}

		// Token: 0x0600837F RID: 33663 RVA: 0x001DF964 File Offset: 0x001DDB64
		public override void DecorateItem(RadPivotGrid owner)
		{
			this.SetItemStyle(owner);
			PivotGridFilterZone filterZone = ((PivotGridFilterItem)base.Item).FilterZone;
			filterZone.CssClass = "rpgFilterZone";
			List<Control> allControls = ChildControlHelper.GetAllControls(new List<Control>(), typeof(PivotGridFieldRenderingControl), filterZone);
			foreach (Control control in allControls)
			{
				PivotGridFieldRenderingControl pivotGridFieldRenderingControl = control as PivotGridFieldRenderingControl;
				if (pivotGridFieldRenderingControl != null)
				{
					pivotGridFieldRenderingControl.PrepareFieldRenderingControlStyle();
				}
			}
		}
	}
}
