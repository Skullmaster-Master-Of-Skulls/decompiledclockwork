using System;
using System.Web.UI;

namespace Telerik.Web.UI.RibbonBar.Renderers
{
	// Token: 0x0200078B RID: 1931
	internal class RibbonBarApplicationMenuItemClassicRenderer : RibbonBarApplicationMenuItemRenderBase
	{
		// Token: 0x060043EE RID: 17390 RVA: 0x000D4C79 File Offset: 0x000D2E79
		public RibbonBarApplicationMenuItemClassicRenderer(RibbonBarApplicationMenuItem owner) : base(owner)
		{
		}

		// Token: 0x060043EF RID: 17391 RVA: 0x000D4C82 File Offset: 0x000D2E82
		public override void RenderContents(HtmlTextWriter writer)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "rrbMIInner");
			writer.RenderBeginTag(HtmlTextWriterTag.Span);
			this.RenderInnerContents(writer);
			writer.RenderEndTag();
		}

		// Token: 0x060043F0 RID: 17392 RVA: 0x000D4CA6 File Offset: 0x000D2EA6
		protected override string GetItemCssClassToRender()
		{
			return "rrbMenuItem";
		}

		// Token: 0x060043F1 RID: 17393 RVA: 0x000D4CB0 File Offset: 0x000D2EB0
		protected override void RenderInnerContents(HtmlTextWriter writer)
		{
			if (!string.IsNullOrEmpty(base.Owner.ImageUrl))
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Class, "rrbMIImage");
				writer.AddAttribute(HtmlTextWriterAttribute.Src, base.Owner.ResolveUrl(base.Owner.ImageUrl));
				writer.AddAttribute(HtmlTextWriterAttribute.Alt, "#");
				writer.RenderBeginTag(HtmlTextWriterTag.Img);
				writer.RenderEndTag();
			}
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "rrbMIText");
			writer.RenderBeginTag(HtmlTextWriterTag.Span);
			writer.Write(base.Owner.Text);
			writer.RenderEndTag();
			if (!string.IsNullOrEmpty(((RibbonBarApplicationMenuItem)base.Owner).Description))
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Class, "rrbMIDesc");
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
