using System;
using System.Web.UI;
using System.Web.UI.HtmlControls;

namespace Telerik.Web.UI
{
	// Token: 0x020001FA RID: 506
	internal class RadDataFormHtmlTableRow : HtmlTableRow
	{
		// Token: 0x060011AB RID: 4523 RVA: 0x000404E9 File Offset: 0x0003E6E9
		protected override ControlCollection CreateControlCollection()
		{
			return new ControlCollection(this);
		}

		// Token: 0x060011AC RID: 4524 RVA: 0x000404F1 File Offset: 0x0003E6F1
		protected override void Render(HtmlTextWriter writer)
		{
			this.RenderChildren(writer);
		}
	}
}
