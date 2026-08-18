using System;
using System.Web.UI;

namespace Telerik.Web.UI.RibbonBar.Renderers
{
	// Token: 0x0200078F RID: 1935
	internal class RibbonBarGalleryCategoryClassicRenderer : RibbonBarCollectionItemRenderer
	{
		// Token: 0x060043FF RID: 17407 RVA: 0x000D5002 File Offset: 0x000D3202
		public RibbonBarGalleryCategoryClassicRenderer(RibbonBarCollectionItemBase owner) : base(owner)
		{
		}

		// Token: 0x06004400 RID: 17408 RVA: 0x000D500B File Offset: 0x000D320B
		public override void RenderControl(HtmlTextWriter writer)
		{
			this.RenderTitle(writer);
			this.RenderItems(writer);
		}

		// Token: 0x06004401 RID: 17409 RVA: 0x000D501C File Offset: 0x000D321C
		private void RenderTitle(HtmlTextWriter writer)
		{
			if (!string.IsNullOrEmpty(((RibbonBarGalleryCategory)base.Owner).Title))
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Class, "rrbCategoryTitle");
				writer.RenderBeginTag(HtmlTextWriterTag.Strong);
				writer.Write(((RibbonBarGalleryCategory)base.Owner).Title);
				writer.RenderEndTag();
			}
		}

		// Token: 0x06004402 RID: 17410 RVA: 0x000D5074 File Offset: 0x000D3274
		private void RenderItems(HtmlTextWriter writer)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "rrbCategory");
			writer.RenderBeginTag(HtmlTextWriterTag.Ul);
			foreach (RibbonBarGalleryItem ribbonBarGalleryItem in ((RibbonBarGalleryCategory)base.Owner).Items)
			{
				ribbonBarGalleryItem.RenderControl(writer);
			}
			writer.RenderEndTag();
		}
	}
}
