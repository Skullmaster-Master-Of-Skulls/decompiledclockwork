using System;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x02000B36 RID: 2870
	public class DropDownEmbeddedTreeRenderer : IDropDownEmbeddedTreeRenderer
	{
		// Token: 0x06006C68 RID: 27752 RVA: 0x00192E7D File Offset: 0x0019107D
		public DropDownEmbeddedTreeRenderer(RadDropDownTree control)
		{
			this._control = control;
		}

		// Token: 0x06006C69 RID: 27753 RVA: 0x00192E8C File Offset: 0x0019108C
		public void RenderContents(HtmlTextWriter writer)
		{
			this._control._embeddedTreeAdapter.RenderEmbeddedTree(writer);
		}

		// Token: 0x04001D2D RID: 7469
		private RadDropDownTree _control;
	}
}
