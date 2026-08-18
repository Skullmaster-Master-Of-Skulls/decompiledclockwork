using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x020001FC RID: 508
	internal class RadDataFormTableRow : TableRow
	{
		// Token: 0x060011B1 RID: 4529 RVA: 0x0004051B File Offset: 0x0003E71B
		protected override ControlCollection CreateControlCollection()
		{
			return new ControlCollection(this);
		}

		// Token: 0x060011B2 RID: 4530 RVA: 0x00040523 File Offset: 0x0003E723
		protected override void Render(HtmlTextWriter writer)
		{
			this.RenderChildren(writer);
		}
	}
}
