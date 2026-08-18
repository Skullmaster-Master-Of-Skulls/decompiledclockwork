using System;
using System.Web.UI;

namespace Telerik.Web.UI.RibbonBar.Renderers
{
	// Token: 0x0200078A RID: 1930
	internal class RibbonBarApplicationSplitMenuItemClassicRenderer : RibbonBarApplicationMenuItemRenderBase
	{
		// Token: 0x060043E9 RID: 17385 RVA: 0x000D49C4 File Offset: 0x000D2BC4
		public RibbonBarApplicationSplitMenuItemClassicRenderer(RibbonBarApplicationSplitMenuItem owner) : base(owner)
		{
		}

		// Token: 0x060043EA RID: 17386 RVA: 0x000D49CD File Offset: 0x000D2BCD
		public override void RenderContents(HtmlTextWriter writer)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "rrbMIInner");
			writer.RenderBeginTag(HtmlTextWriterTag.Span);
			this.RenderInnerContents(writer);
			writer.RenderEndTag();
			this.RenderDropDown(writer);
		}

		// Token: 0x060043EB RID: 17387 RVA: 0x000D49F8 File Offset: 0x000D2BF8
		protected override string GetItemCssClassToRender()
		{
			return RibbonBarStyles.Combine(new string[]
			{
				"rrbMenuItem",
				"rrbSplitMenuItem"
			});
		}

		// Token: 0x060043EC RID: 17388 RVA: 0x000D4A24 File Offset: 0x000D2C24
		protected void RenderDropDown(HtmlTextWriter writer)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Class, RibbonBarStyles.Combine(new string[]
			{
				"rrbPopup",
				"rrbMenu"
			}));
			writer.RenderBeginTag(HtmlTextWriterTag.Div);
			if (!string.IsNullOrEmpty(((RibbonBarApplicationSplitMenuItem)base.Owner).Header))
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Class, "rrbHeader");
				writer.RenderBeginTag(HtmlTextWriterTag.Strong);
				writer.Write(((RibbonBarApplicationSplitMenuItem)base.Owner).Header);
				writer.RenderEndTag();
			}
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "rrbMenuGroup");
			writer.RenderBeginTag(HtmlTextWriterTag.Ul);
			foreach (RibbonBarApplicationMenuItemBase ribbonBarApplicationMenuItemBase in ((RibbonBarApplicationSplitMenuItem)base.Owner).Items)
			{
				ribbonBarApplicationMenuItemBase.RibbonBar = base.Owner.RibbonBar;
				ribbonBarApplicationMenuItemBase.RenderControl(writer);
			}
			writer.RenderEndTag();
			writer.RenderEndTag();
		}

		// Token: 0x060043ED RID: 17389 RVA: 0x000D4B28 File Offset: 0x000D2D28
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
			if (!string.IsNullOrEmpty(((RibbonBarApplicationSplitMenuItem)base.Owner).ExpandAccessKey))
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Accesskey, ((RibbonBarApplicationSplitMenuItem)base.Owner).ExpandAccessKey);
			}
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "rrbMIToggle");
			writer.RenderBeginTag(HtmlTextWriterTag.Span);
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "rrbIcon");
			writer.RenderBeginTag(HtmlTextWriterTag.Span);
			writer.Write("<!-- &nbsp; -->");
			writer.RenderEndTag();
			if (!string.IsNullOrEmpty(((RibbonBarApplicationSplitMenuItem)base.Owner).ExpandAccessKey))
			{
				base.RenderKeyboardBox(writer, ((RibbonBarApplicationSplitMenuItem)base.Owner).ExpandAccessKey);
			}
			writer.RenderEndTag();
			if (!string.IsNullOrEmpty(base.Owner.AccessKey))
			{
				base.RenderKeyboardBox(writer, base.Owner.AccessKey);
			}
		}
	}
}
