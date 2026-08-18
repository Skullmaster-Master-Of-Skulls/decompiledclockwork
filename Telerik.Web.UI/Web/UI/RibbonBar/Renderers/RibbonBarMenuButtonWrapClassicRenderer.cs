using System;
using System.Web.UI;

namespace Telerik.Web.UI.RibbonBar.Renderers
{
	// Token: 0x020007A0 RID: 1952
	internal class RibbonBarMenuButtonWrapClassicRenderer : RibbonBarButtonClassicRenderer
	{
		// Token: 0x06004469 RID: 17513 RVA: 0x000D7366 File Offset: 0x000D5566
		public RibbonBarMenuButtonWrapClassicRenderer(RibbonBarItem owner) : base(owner)
		{
		}

		// Token: 0x0600446A RID: 17514 RVA: 0x000D7370 File Offset: 0x000D5570
		public override void RenderBeginTagContext(HtmlTextWriter writer)
		{
			string value = "#";
			if (!string.IsNullOrEmpty(((MenuButtonWrap)base.Owner).NavigateUrl) && ((MenuButtonWrap)base.Owner).NavigateUrl != "#")
			{
				value = base.Owner.ResolveUrl(((MenuButtonWrap)base.Owner).NavigateUrl);
			}
			writer.AddAttribute(HtmlTextWriterAttribute.Href, value);
		}

		// Token: 0x0600446B RID: 17515 RVA: 0x000D73DB File Offset: 0x000D55DB
		protected override void RenderImage(HtmlTextWriter writer)
		{
			if (!this.ImageUrlToRender.Contains("WebResource.axd"))
			{
				base.RenderImage(writer);
			}
		}
	}
}
