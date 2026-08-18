using System;
using System.Web.UI;
using System.Web.UI.HtmlControls;

namespace Telerik.Web.UI
{
	// Token: 0x020001FB RID: 507
	internal class RadDataFormHtmlTableCell : HtmlTableCell
	{
		// Token: 0x060011AE RID: 4526 RVA: 0x00040502 File Offset: 0x0003E702
		protected override ControlCollection CreateControlCollection()
		{
			return new ControlCollection(this);
		}

		// Token: 0x060011AF RID: 4527 RVA: 0x0004050A File Offset: 0x0003E70A
		protected override void Render(HtmlTextWriter writer)
		{
			this.RenderChildren(writer);
		}
	}
}
