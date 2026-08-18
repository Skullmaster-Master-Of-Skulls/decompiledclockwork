using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x020019C1 RID: 6593
	internal class RadListViewTableCell : TableCell
	{
		// Token: 0x0600FEA7 RID: 65191 RVA: 0x00392A8C File Offset: 0x00390C8C
		protected override ControlCollection CreateControlCollection()
		{
			return new ControlCollection(this);
		}

		// Token: 0x0600FEA8 RID: 65192 RVA: 0x00392A94 File Offset: 0x00390C94
		protected override void Render(HtmlTextWriter writer)
		{
			this.RenderChildren(writer);
		}
	}
}
