using System;
using System.Web.UI;
using System.Web.UI.HtmlControls;

namespace Telerik.Web.UI
{
	// Token: 0x020019BF RID: 6591
	internal class RadListViewHtmlTableCell : HtmlTableCell
	{
		// Token: 0x0600FEA1 RID: 65185 RVA: 0x00392A5A File Offset: 0x00390C5A
		protected override ControlCollection CreateControlCollection()
		{
			return new ControlCollection(this);
		}

		// Token: 0x0600FEA2 RID: 65186 RVA: 0x00392A62 File Offset: 0x00390C62
		protected override void Render(HtmlTextWriter writer)
		{
			this.RenderChildren(writer);
		}
	}
}
