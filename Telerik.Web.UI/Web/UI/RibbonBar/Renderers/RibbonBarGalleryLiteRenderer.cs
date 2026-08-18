using System;
using System.Web.UI;

namespace Telerik.Web.UI.RibbonBar.Renderers
{
	// Token: 0x020007B8 RID: 1976
	internal class RibbonBarGalleryLiteRenderer : RibbonBarItemRenderBase
	{
		// Token: 0x060044E0 RID: 17632 RVA: 0x000D9A28 File Offset: 0x000D7C28
		public RibbonBarGalleryLiteRenderer(RibbonBarItem owner) : base(owner)
		{
		}

		// Token: 0x060044E1 RID: 17633 RVA: 0x000D9A34 File Offset: 0x000D7C34
		public override void AddAttributesToRender(HtmlTextWriter writer)
		{
			string cssClass = base.Owner.CssClass;
			string text = base.Owner.Enabled ? string.Empty : "rrbDisabled";
			base.Owner.CssClass = string.Format("{0} {1} {2} {3}", new object[]
			{
				"rrbGallery",
				this.GetTextPositionClassName(),
				base.Owner.CssClass,
				text
			}).Trim();
			base.Owner.BaseAddAttributesToRender(writer);
			if (!string.IsNullOrEmpty(base.Owner.AccessKey))
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Accesskey, base.Owner.AccessKey);
			}
			base.Owner.CssClass = cssClass;
		}

		// Token: 0x060044E2 RID: 17634 RVA: 0x000D9AE8 File Offset: 0x000D7CE8
		public override void RenderContents(HtmlTextWriter writer)
		{
			if (!string.IsNullOrEmpty(base.Owner.AccessKey))
			{
				this.RenderKeyboardBox(writer);
			}
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "rrbGalleryScrollWrap");
			writer.RenderBeginTag(HtmlTextWriterTag.Div);
			this.RenderCategories(writer);
			writer.RenderEndTag();
			this.RenderActionButtons(writer);
		}

		// Token: 0x060044E3 RID: 17635 RVA: 0x000D9B37 File Offset: 0x000D7D37
		protected void RenderKeyboardBox(HtmlTextWriter writer)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "rrbKeyBox");
			writer.RenderBeginTag(HtmlTextWriterTag.Span);
			writer.Write(base.Owner.AccessKey);
			writer.RenderEndTag();
		}

		// Token: 0x060044E4 RID: 17636 RVA: 0x000D9B68 File Offset: 0x000D7D68
		protected string GetTextPositionClassName()
		{
			switch (((RibbonBarGallery)base.Owner).ItemTextPosition)
			{
			case RibbonBarGalleryItemTextPosition.Inline:
				return "rrbGalleryTextPositionInline";
			case RibbonBarGalleryItemTextPosition.None:
				return "rrbGalleryTextPositionNone";
			default:
				return "rrbGalleryTextPositionBottom";
			}
		}

		// Token: 0x060044E5 RID: 17637 RVA: 0x000D9BAC File Offset: 0x000D7DAC
		protected void RenderCategories(HtmlTextWriter writer)
		{
			foreach (RibbonBarGalleryCategory ribbonBarGalleryCategory in ((RibbonBarGallery)base.Owner).Categories)
			{
				ribbonBarGalleryCategory.RenderControl(writer);
			}
		}

		// Token: 0x060044E6 RID: 17638 RVA: 0x000D9C0C File Offset: 0x000D7E0C
		protected void RenderActionButtons(HtmlTextWriter writer)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "rrbGalleryActions");
			writer.RenderBeginTag(HtmlTextWriterTag.Div);
			this.RenderActionUpButton(writer);
			this.RenderActionDownButton(writer);
			this.RenderActionExpandButton(writer);
			writer.RenderEndTag();
		}

		// Token: 0x060044E7 RID: 17639 RVA: 0x000D9C3E File Offset: 0x000D7E3E
		protected void RenderActionUpButton(HtmlTextWriter writer)
		{
			this.RenderActionButton(writer, "radIconUp", "Page Up");
		}

		// Token: 0x060044E8 RID: 17640 RVA: 0x000D9C51 File Offset: 0x000D7E51
		protected void RenderActionDownButton(HtmlTextWriter writer)
		{
			this.RenderActionButton(writer, "radIconDown", "Page Down");
		}

		// Token: 0x060044E9 RID: 17641 RVA: 0x000D9C64 File Offset: 0x000D7E64
		protected void RenderActionExpandButton(HtmlTextWriter writer)
		{
			this.RenderActionButton(writer, "radIconExpand", "Expand Gallery");
		}

		// Token: 0x060044EA RID: 17642 RVA: 0x000D9C78 File Offset: 0x000D7E78
		protected void RenderActionButton(HtmlTextWriter writer, string className, string title)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Class, RibbonBarStyles.Combine(new string[]
			{
				"rrbGalleryAction",
				"rrbButton"
			}));
			writer.AddAttribute(HtmlTextWriterAttribute.Title, title);
			writer.RenderBeginTag(HtmlTextWriterTag.Span);
			writer.AddAttribute(HtmlTextWriterAttribute.Class, RibbonBarStyles.Combine(new string[]
			{
				"radIcon",
				className
			}));
			writer.RenderBeginTag(HtmlTextWriterTag.Span);
			writer.Write("<!-- &nbsp; -->");
			writer.RenderEndTag();
			writer.RenderEndTag();
		}
	}
}
