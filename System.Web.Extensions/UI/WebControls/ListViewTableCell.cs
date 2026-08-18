using System;
using System.Web.UI.HtmlControls;

namespace System.Web.UI.WebControls
{
	// Token: 0x020000B9 RID: 185
	internal class ListViewTableCell : HtmlTableCell
	{
		// Token: 0x060008F7 RID: 2295 RVA: 0x0002271F File Offset: 0x0002091F
		protected override ControlCollection CreateControlCollection()
		{
			return new ControlCollection(this);
		}

		// Token: 0x060008F8 RID: 2296 RVA: 0x00022727 File Offset: 0x00020927
		protected internal override void Render(HtmlTextWriter writer)
		{
			this.RenderChildren(writer);
		}
	}
}
