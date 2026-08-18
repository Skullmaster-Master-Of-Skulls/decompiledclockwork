using System;
using System.Web.UI;

namespace Telerik.Web.UI.RibbonBar.Renderers
{
	// Token: 0x020007A8 RID: 1960
	internal class RibbonBarApplicationMenuItemLiteRenderer : RibbonBarApplicationMenuItemRenderBase
	{
		// Token: 0x0600448D RID: 17549 RVA: 0x000D7E79 File Offset: 0x000D6079
		public RibbonBarApplicationMenuItemLiteRenderer(RibbonBarApplicationMenuItem owner) : base(owner)
		{
		}

		// Token: 0x0600448E RID: 17550 RVA: 0x000D7E82 File Offset: 0x000D6082
		protected override string GetItemCssClassToRender()
		{
			return "rrbItem";
		}

		// Token: 0x0600448F RID: 17551 RVA: 0x000D7E89 File Offset: 0x000D6089
		public override void RenderContents(HtmlTextWriter writer)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "rrbInner");
			writer.RenderBeginTag(HtmlTextWriterTag.Span);
			this.RenderInnerContents(writer);
			writer.RenderEndTag();
		}

		// Token: 0x06004490 RID: 17552 RVA: 0x000D7EB0 File Offset: 0x000D60B0
		protected override void RenderInnerContents(HtmlTextWriter writer)
		{
			if (!string.IsNullOrEmpty(base.Owner.ImageUrl))
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Class, "rrbImage");
				writer.AddAttribute(HtmlTextWriterAttribute.Src, base.Owner.ResolveUrl(base.Owner.ImageUrl));
				writer.AddAttribute(HtmlTextWriterAttribute.Alt, "#");
				writer.RenderBeginTag(HtmlTextWriterTag.Img);
				writer.RenderEndTag();
			}
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "rrbText");
			writer.RenderBeginTag(HtmlTextWriterTag.Span);
			writer.Write(base.Owner.Text);
			writer.RenderEndTag();
			if (!string.IsNullOrEmpty(((RibbonBarApplicationMenuItem)base.Owner).Description))
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Class, "rrbDescription");
				writer.RenderBeginTag(HtmlTextWriterTag.Span);
				writer.Write(((RibbonBarApplicationMenuItem)base.Owner).Description);
				writer.RenderEndTag();
			}
			if (!string.IsNullOrEmpty(base.Owner.AccessKey))
			{
				base.RenderKeyboardBox(writer, base.Owner.AccessKey);
			}
		}
	}
}
