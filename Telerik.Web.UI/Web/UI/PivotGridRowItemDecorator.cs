using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x02000DCD RID: 3533
	public class PivotGridRowItemDecorator : PivotGridItemDecorator
	{
		// Token: 0x06008389 RID: 33673 RVA: 0x001DFC6C File Offset: 0x001DDE6C
		public PivotGridRowItemDecorator(PivotGridItem item) : base(item)
		{
		}

		// Token: 0x0600838A RID: 33674 RVA: 0x001DFC78 File Offset: 0x001DDE78
		public override void DecorateItem(RadPivotGrid owner)
		{
			this.SetItemStyle(owner);
			List<PivotGridRowZone> rowZones = ((PivotGridRowItem)base.Item).RowZones;
			foreach (PivotGridRowZone pivotGridRowZone in rowZones)
			{
				pivotGridRowZone.CssClass = "rpgRowsZone";
				if (owner.RowTableLayout == PivotGridLayout.Compact && pivotGridRowZone.Controls.Count > 0)
				{
					SpanPanel spanPanel = pivotGridRowZone.Controls[0] as SpanPanel;
					spanPanel.Attributes.Add("style", "white-space: nowrap;");
				}
				List<Control> allControls = ChildControlHelper.GetAllControls(new List<Control>(), typeof(PivotGridFieldRenderingControl), pivotGridRowZone);
				foreach (Control control in allControls)
				{
					PivotGridFieldRenderingControl pivotGridFieldRenderingControl = control as PivotGridFieldRenderingControl;
					if (pivotGridFieldRenderingControl != null)
					{
						pivotGridFieldRenderingControl.PrepareFieldRenderingControlStyle();
					}
				}
			}
			TableCell tableCell = base.Item.FindControl("DropFieldHereCell") as TableCell;
			if (tableCell != null)
			{
				tableCell.CssClass = "rpgRowsZone";
			}
		}
	}
}
