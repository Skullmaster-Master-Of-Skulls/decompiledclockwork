using System;
using System.Web.UI;

namespace Telerik.Web.UI.RibbonBar.Renderers
{
	// Token: 0x020007AA RID: 1962
	internal class RibbonBarApplicationSplitMenuItemLiteRenderer : RibbonBarApplicationMenuItemRenderBase
	{
		// Token: 0x0600449D RID: 17565 RVA: 0x000D839A File Offset: 0x000D659A
		public RibbonBarApplicationSplitMenuItemLiteRenderer(RibbonBarApplicationSplitMenuItem owner) : base(owner)
		{
		}

		// Token: 0x0600449E RID: 17566 RVA: 0x000D83A4 File Offset: 0x000D65A4
		protected override string GetItemCssClassToRender()
		{
			return RibbonBarStyles.Combine(new string[]
			{
				"rrbItem",
				"rrbSplitMenuItem"
			});
		}

		// Token: 0x0600449F RID: 17567 RVA: 0x000D83CE File Offset: 0x000D65CE
		public override void RenderContents(HtmlTextWriter writer)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "rrbInner");
			writer.RenderBeginTag(HtmlTextWriterTag.Span);
			this.RenderInnerContents(writer);
			writer.RenderEndTag();
			this.RenderDropDown(writer);
		}

		// Token: 0x060044A0 RID: 17568 RVA: 0x000D83FC File Offset: 0x000D65FC
		private void RenderDropDown(HtmlTextWriter writer)
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
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "rrbGroup");
			writer.RenderBeginTag(HtmlTextWriterTag.Ul);
			foreach (RibbonBarApplicationMenuItemBase ribbonBarApplicationMenuItemBase in ((RibbonBarApplicationSplitMenuItem)base.Owner).Items)
			{
				ribbonBarApplicationMenuItemBase.RibbonBar = base.Owner.RibbonBar;
				ribbonBarApplicationMenuItemBase.RenderControl(writer);
			}
			writer.RenderEndTag();
			writer.RenderEndTag();
		}

		// Token: 0x060044A1 RID: 17569 RVA: 0x000D8500 File Offset: 0x000D6700
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
			if (!string.IsNullOrEmpty(((RibbonBarApplicationSplitMenuItem)base.Owner).ExpandAccessKey))
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Accesskey, ((RibbonBarApplicationSplitMenuItem)base.Owner).ExpandAccessKey);
			}
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "rrbToggle");
			writer.RenderBeginTag(HtmlTextWriterTag.Span);
			writer.AddAttribute(HtmlTextWriterAttribute.Class, RibbonBarStyles.Combine(new string[]
			{
				"radIcon",
				"radIconRight"
			}));
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
