using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x020001FD RID: 509
	internal class RadDataFormTableCell : TableCell
	{
		// Token: 0x060011B4 RID: 4532 RVA: 0x00040534 File Offset: 0x0003E734
		protected override ControlCollection CreateControlCollection()
		{
			return new ControlCollection(this);
		}

		// Token: 0x060011B5 RID: 4533 RVA: 0x0004053C File Offset: 0x0003E73C
		protected override void Render(HtmlTextWriter writer)
		{
			this.RenderChildren(writer);
		}
	}
}
