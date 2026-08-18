using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI.RibbonBar.Renderers
{
	// Token: 0x02000793 RID: 1939
	internal class RibbonBarGalleryItemLiteRenderer : RibbonBarCollectionItemRenderer
	{
		// Token: 0x06004410 RID: 17424 RVA: 0x000D55E0 File Offset: 0x000D37E0
		public RibbonBarGalleryItemLiteRenderer(RibbonBarCollectionItemBase owner) : base(owner)
		{
		}

		// Token: 0x06004411 RID: 17425 RVA: 0x000D55EC File Offset: 0x000D37EC
		public override void RenderControl(HtmlTextWriter writer)
		{
			string arg = ((RibbonBarGalleryItem)base.Owner).Selected ? "rrbSelected" : "";
			string value = string.Format("{0} {1} {2}", "rrbItem", arg, base.Owner.CssClass).Trim();
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
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "rrbLink");
			writer.RenderBeginTag(HtmlTextWriterTag.Div);
			this.RenderImage(writer);
			this.RenderText(writer);
			writer.RenderEndTag();
			writer.RenderEndTag();
		}

		// Token: 0x06004412 RID: 17426 RVA: 0x000D571C File Offset: 0x000D391C
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
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "rrbImage");
			writer.AddAttribute(HtmlTextWriterAttribute.Alt, "image");
			writer.AddAttribute(HtmlTextWriterAttribute.Src, base.Owner.ResolveUrl(text));
			writer.RenderBeginTag(HtmlTextWriterTag.Img);
			writer.RenderEndTag();
		}

		// Token: 0x06004413 RID: 17427 RVA: 0x000D57A0 File Offset: 0x000D39A0
		private void RenderText(HtmlTextWriter writer)
		{
			if (!string.IsNullOrEmpty(((RibbonBarGalleryItem)base.Owner).Text))
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Class, "rrbText");
				writer.RenderBeginTag(HtmlTextWriterTag.Span);
				writer.Write(((RibbonBarGalleryItem)base.Owner).Text);
				writer.RenderEndTag();
			}
		}
	}
}
