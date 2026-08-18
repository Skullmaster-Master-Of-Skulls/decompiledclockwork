using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI.RibbonBar.Renderers
{
	// Token: 0x02000790 RID: 1936
	internal class RibbonBarGalleryItemClassicRenderer : RibbonBarCollectionItemRenderer
	{
		// Token: 0x06004403 RID: 17411 RVA: 0x000D50EC File Offset: 0x000D32EC
		public RibbonBarGalleryItemClassicRenderer(RibbonBarCollectionItemBase owner) : base(owner)
		{
		}

		// Token: 0x06004404 RID: 17412 RVA: 0x000D50F8 File Offset: 0x000D32F8
		public override void RenderControl(HtmlTextWriter writer)
		{
			string text = ((RibbonBarGalleryItem)base.Owner).Selected ? "rrbGalleryItemSelected" : "";
			string value = RibbonBarStyles.Combine(new string[]
			{
				"rrbGalleryItem",
				text,
				base.Owner.CssClass
			});
			writer.AddAttribute(HtmlTextWriterAttribute.Class, value);
			if (!string.IsNullOrEmpty(((RibbonBarGalleryItem)base.Owner).ToolTip))
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Title, ((RibbonBarGalleryItem)base.Owner).ToolTip);
			}
			RibbonBarGallery ribbonBarGallery = ((RibbonBarGalleryItem)base.Owner).ParentWebControl as RibbonBarGallery;
			if (ribbonBarGallery.ItemWidth != Unit.Empty)
			{
				writer.AddStyleAttribute(HtmlTextWriterStyle.Width, ribbonBarGallery.ItemWidth.ToString());
			}
			if (ribbonBarGallery.ItemHeight != Unit.Empty)
			{
				writer.AddStyleAttribute(HtmlTextWriterStyle.Height, ribbonBarGallery.ItemHeight.ToString());
			}
			writer.RenderBeginTag(HtmlTextWriterTag.Li);
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "rrbGalleryItemInner");
			writer.RenderBeginTag(HtmlTextWriterTag.Div);
			this.RenderImage(writer);
			this.RenderText(writer);
			writer.RenderEndTag();
			writer.RenderEndTag();
		}

		// Token: 0x06004405 RID: 17413 RVA: 0x000D5230 File Offset: 0x000D3430
		private void RenderImage(HtmlTextWriter writer)
		{
			string text = ((RibbonBarGalleryItem)base.Owner).ImageUrl;
			if (string.IsNullOrEmpty(text))
			{
				if (!string.IsNullOrEmpty(((RibbonBarGalleryItem)base.Owner).Text))
				{
					return;
				}
				text = "";
			}
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "rrbGalleryItemImage");
			writer.AddAttribute(HtmlTextWriterAttribute.Alt, "image");
			writer.AddAttribute(HtmlTextWriterAttribute.Src, base.Owner.ResolveUrl(text));
			writer.RenderBeginTag(HtmlTextWriterTag.Img);
			writer.RenderEndTag();
		}

		// Token: 0x06004406 RID: 17414 RVA: 0x000D52B4 File Offset: 0x000D34B4
		private void RenderText(HtmlTextWriter writer)
		{
			if (!string.IsNullOrEmpty(((RibbonBarGalleryItem)base.Owner).Text))
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Class, "rrbGalleryItemText");
				writer.RenderBeginTag(HtmlTextWriterTag.Span);
				writer.Write(((RibbonBarGalleryItem)base.Owner).Text);
				writer.RenderEndTag();
			}
		}
	}
}
