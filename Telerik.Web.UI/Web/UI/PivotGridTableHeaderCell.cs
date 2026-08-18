using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x02000C2D RID: 3117
	public class PivotGridTableHeaderCell : TableHeaderCell
	{
		// Token: 0x0600764A RID: 30282 RVA: 0x001B7694 File Offset: 0x001B5894
		private void AddScopeAttribute(HtmlTextWriter writer)
		{
			writer.AddAttribute("scope", "col");
		}

		// Token: 0x0600764B RID: 30283 RVA: 0x001B76A8 File Offset: 0x001B58A8
		private void AddStyleAttributes(HtmlTextWriter writer)
		{
			if (base.ControlStyle is TableItemStyle && (base.ControlStyle as TableItemStyle).HorizontalAlign != HorizontalAlign.NotSet)
			{
				base.Style["text-align"] = (base.ControlStyle as TableItemStyle).HorizontalAlign.ToString().ToLower();
				(base.ControlStyle as TableItemStyle).HorizontalAlign = HorizontalAlign.NotSet;
			}
		}

		// Token: 0x0600764C RID: 30284 RVA: 0x001B7714 File Offset: 0x001B5914
		protected override void AddAttributesToRender(HtmlTextWriter writer)
		{
			this.AddScopeAttribute(writer);
			this.AddStyleAttributes(writer);
			base.AddAttributesToRender(writer);
		}
	}
}
