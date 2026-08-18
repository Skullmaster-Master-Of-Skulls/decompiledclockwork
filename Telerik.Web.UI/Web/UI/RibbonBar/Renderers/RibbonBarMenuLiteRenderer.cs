using System;
using System.Web.UI;

namespace Telerik.Web.UI.RibbonBar.Renderers
{
	// Token: 0x020007B4 RID: 1972
	internal class RibbonBarMenuLiteRenderer : RibbonBarMenuBaseItemLiteRenderBase
	{
		// Token: 0x060044D1 RID: 17617 RVA: 0x000D9508 File Offset: 0x000D7708
		public RibbonBarMenuLiteRenderer(RibbonBarItem owner) : base(owner)
		{
		}

		// Token: 0x060044D2 RID: 17618 RVA: 0x000D9511 File Offset: 0x000D7711
		protected override void RenderImage(HtmlTextWriter writer)
		{
			if (!this.ImageUrlToRender.Contains("WebResource.axd"))
			{
				base.RenderImage(writer);
			}
		}

		// Token: 0x060044D3 RID: 17619 RVA: 0x000D952C File Offset: 0x000D772C
		protected override void RenderDropDownContents(HtmlTextWriter writer)
		{
			foreach (RibbonBarMenuItem ribbonBarMenuItem in ((RibbonBarMenu)base.Owner).GetVisibleItems())
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Class, "rrbItem");
				writer.RenderBeginTag(HtmlTextWriterTag.Li);
				ribbonBarMenuItem.RenderControl(writer);
				writer.RenderEndTag();
			}
		}
	}
}
