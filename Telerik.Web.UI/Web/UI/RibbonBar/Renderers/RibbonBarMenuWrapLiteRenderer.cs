using System;
using System.Web.UI;

namespace Telerik.Web.UI.RibbonBar.Renderers
{
	// Token: 0x020007B5 RID: 1973
	internal class RibbonBarMenuWrapLiteRenderer : RibbonBarMenuLiteRenderer
	{
		// Token: 0x060044D4 RID: 17620 RVA: 0x000D95A0 File Offset: 0x000D77A0
		public RibbonBarMenuWrapLiteRenderer(RibbonBarItem owner) : base(owner)
		{
		}

		// Token: 0x060044D5 RID: 17621 RVA: 0x000D95A9 File Offset: 0x000D77A9
		protected override void RenderImage(HtmlTextWriter writer)
		{
			if (!this.ImageUrlToRender.Contains("WebResource.axd"))
			{
				base.RenderImage(writer);
			}
		}
	}
}
