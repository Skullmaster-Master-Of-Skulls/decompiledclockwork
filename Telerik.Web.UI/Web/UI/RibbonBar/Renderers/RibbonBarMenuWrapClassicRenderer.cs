using System;
using System.Web.UI;

namespace Telerik.Web.UI.RibbonBar.Renderers
{
	// Token: 0x0200079E RID: 1950
	internal class RibbonBarMenuWrapClassicRenderer : RibbonBarMenuClassicRenderer
	{
		// Token: 0x06004464 RID: 17508 RVA: 0x000D7208 File Offset: 0x000D5408
		public RibbonBarMenuWrapClassicRenderer(RibbonBarItem owner) : base(owner)
		{
		}

		// Token: 0x06004465 RID: 17509 RVA: 0x000D7211 File Offset: 0x000D5411
		protected override void RenderImage(HtmlTextWriter writer)
		{
			if (!this.ImageUrlToRender.Contains("WebResource.axd"))
			{
				base.RenderImage(writer);
			}
		}
	}
}
