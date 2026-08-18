using System;
using System.Web.UI;

namespace Telerik.Web.UI.RibbonBar.Renderers
{
	// Token: 0x02000792 RID: 1938
	internal class RibbonBarGalleryCategoryLiteRenderer : RibbonBarCollectionItemRenderer
	{
		// Token: 0x0600440C RID: 17420 RVA: 0x000D54F7 File Offset: 0x000D36F7
		public RibbonBarGalleryCategoryLiteRenderer(RibbonBarCollectionItemBase owner) : base(owner)
		{
		}

		// Token: 0x0600440D RID: 17421 RVA: 0x000D5500 File Offset: 0x000D3700
		public override void RenderControl(HtmlTextWriter writer)
		{
			this.RenderTitle(writer);
			this.RenderItems(writer);
		}

		// Token: 0x0600440E RID: 17422 RVA: 0x000D5510 File Offset: 0x000D3710
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

		// Token: 0x0600440F RID: 17423 RVA: 0x000D5568 File Offset: 0x000D3768
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
