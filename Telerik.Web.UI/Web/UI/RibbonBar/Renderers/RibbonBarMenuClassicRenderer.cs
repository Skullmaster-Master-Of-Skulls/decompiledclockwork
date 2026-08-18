using System;
using System.Web.UI;

namespace Telerik.Web.UI.RibbonBar.Renderers
{
	// Token: 0x0200079D RID: 1949
	internal class RibbonBarMenuClassicRenderer : RibbonBarMenuBaseItemClassicRenderBase
	{
		// Token: 0x06004461 RID: 17505 RVA: 0x000D718C File Offset: 0x000D538C
		public RibbonBarMenuClassicRenderer(RibbonBarItem owner) : base(owner)
		{
		}

		// Token: 0x06004462 RID: 17506 RVA: 0x000D7195 File Offset: 0x000D5395
		protected override void RenderImage(HtmlTextWriter writer)
		{
			if (!this.ImageUrlToRender.Contains("WebResource.axd"))
			{
				base.RenderImage(writer);
			}
		}

		// Token: 0x06004463 RID: 17507 RVA: 0x000D71B0 File Offset: 0x000D53B0
		protected override void RenderDropDownContents(HtmlTextWriter writer)
		{
			foreach (RibbonBarMenuItem ribbonBarMenuItem in ((RibbonBarMenu)base.Owner).GetVisibleItems())
			{
				ribbonBarMenuItem.RenderControl(writer);
			}
		}
	}
}
