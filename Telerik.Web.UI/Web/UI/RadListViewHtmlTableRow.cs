using System;
using System.Web.UI;
using System.Web.UI.HtmlControls;

namespace Telerik.Web.UI
{
	// Token: 0x020019BE RID: 6590
	internal class RadListViewHtmlTableRow : HtmlTableRow
	{
		// Token: 0x0600FE9E RID: 65182 RVA: 0x00392A41 File Offset: 0x00390C41
		protected override ControlCollection CreateControlCollection()
		{
			return new ControlCollection(this);
		}

		// Token: 0x0600FE9F RID: 65183 RVA: 0x00392A49 File Offset: 0x00390C49
		protected override void Render(HtmlTextWriter writer)
		{
			this.RenderChildren(writer);
		}
	}
}
