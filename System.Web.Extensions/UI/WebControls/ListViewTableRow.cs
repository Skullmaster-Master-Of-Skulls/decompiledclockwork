using System;
using System.Web.UI.HtmlControls;

namespace System.Web.UI.WebControls
{
	// Token: 0x020000BA RID: 186
	internal class ListViewTableRow : HtmlTableRow
	{
		// Token: 0x060008FA RID: 2298 RVA: 0x0002271F File Offset: 0x0002091F
		protected override ControlCollection CreateControlCollection()
		{
			return new ControlCollection(this);
		}

		// Token: 0x060008FB RID: 2299 RVA: 0x00022727 File Offset: 0x00020927
		protected internal override void Render(HtmlTextWriter writer)
		{
			this.RenderChildren(writer);
		}
	}
}
