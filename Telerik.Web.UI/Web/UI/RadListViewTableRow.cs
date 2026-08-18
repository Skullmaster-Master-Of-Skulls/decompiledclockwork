using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x020019C0 RID: 6592
	internal class RadListViewTableRow : TableRow
	{
		// Token: 0x0600FEA4 RID: 65188 RVA: 0x00392A73 File Offset: 0x00390C73
		protected override ControlCollection CreateControlCollection()
		{
			return new ControlCollection(this);
		}

		// Token: 0x0600FEA5 RID: 65189 RVA: 0x00392A7B File Offset: 0x00390C7B
		protected override void Render(HtmlTextWriter writer)
		{
			this.RenderChildren(writer);
		}
	}
}
