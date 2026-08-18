using System;
using System.Web.UI;

namespace Telerik.Web.UI.RibbonBar.Renderers
{
	// Token: 0x02000799 RID: 1945
	internal class RibbonBarGalleryClassicRenderer : RibbonBarItemRenderBase
	{
		// Token: 0x0600443F RID: 17471 RVA: 0x000D64EB File Offset: 0x000D46EB
		public RibbonBarGalleryClassicRenderer(RibbonBarItem owner) : base(owner)
		{
		}

		// Token: 0x06004440 RID: 17472 RVA: 0x000D64F4 File Offset: 0x000D46F4
		public override void AddAttributesToRender(HtmlTextWriter writer)
		{
			string cssClass = base.Owner.CssClass;
			string text = base.Owner.Enabled ? string.Empty : "rrbDisabled";
			base.Owner.CssClass = RibbonBarStyles.Combine(new string[]
			{
				"rrbGallery",
				this.GetTextPositionClassName(),
				base.Owner.CssClass,
				text
			});
			base.Owner.BaseAddAttributesToRender(writer);
			if (!string.IsNullOrEmpty(base.Owner.AccessKey))
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Accesskey, base.Owner.AccessKey);
			}
			base.Owner.CssClass = cssClass;
		}

		// Token: 0x06004441 RID: 17473 RVA: 0x000D65A0 File Offset: 0x000D47A0
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

		// Token: 0x06004442 RID: 17474 RVA: 0x000D65EF File Offset: 0x000D47EF
		protected void RenderKeyboardBox(HtmlTextWriter writer)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "rrbKeyBox");
			writer.RenderBeginTag(HtmlTextWriterTag.Span);
			writer.Write(base.Owner.AccessKey);
			writer.RenderEndTag();
		}

		// Token: 0x06004443 RID: 17475 RVA: 0x000D6620 File Offset: 0x000D4820
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

		// Token: 0x06004444 RID: 17476 RVA: 0x000D6664 File Offset: 0x000D4864
		protected void RenderCategories(HtmlTextWriter writer)
		{
			foreach (RibbonBarGalleryCategory ribbonBarGalleryCategory in ((RibbonBarGallery)base.Owner).Categories)
			{
				ribbonBarGalleryCategory.RenderControl(writer);
			}
		}

		// Token: 0x06004445 RID: 17477 RVA: 0x000D66C4 File Offset: 0x000D48C4
		protected void RenderActionButtons(HtmlTextWriter writer)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "rrbGalleryActions");
			writer.RenderBeginTag(HtmlTextWriterTag.Div);
			this.RenderActionUpButton(writer);
			this.RenderActionDownButton(writer);
			this.RenderActionExpandButton(writer);
			writer.RenderEndTag();
		}

		// Token: 0x06004446 RID: 17478 RVA: 0x000D66F6 File Offset: 0x000D48F6
		protected void RenderActionUpButton(HtmlTextWriter writer)
		{
			this.RenderActionButton(writer, "rrbGalleryActionUp", "Page Up");
		}

		// Token: 0x06004447 RID: 17479 RVA: 0x000D6709 File Offset: 0x000D4909
		protected void RenderActionDownButton(HtmlTextWriter writer)
		{
			this.RenderActionButton(writer, "rrbGalleryActionDown", "Page Down");
		}

		// Token: 0x06004448 RID: 17480 RVA: 0x000D671C File Offset: 0x000D491C
		protected void RenderActionExpandButton(HtmlTextWriter writer)
		{
			this.RenderActionButton(writer, "rrbGalleryActionExpand", "Expand Gallery");
		}

		// Token: 0x06004449 RID: 17481 RVA: 0x000D6730 File Offset: 0x000D4930
		protected void RenderActionButton(HtmlTextWriter writer, string className, string title)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Class, RibbonBarStyles.Combine(new string[]
			{
				"rrbGalleryAction",
				className
			}));
			writer.AddAttribute(HtmlTextWriterAttribute.Title, title);
			writer.RenderBeginTag(HtmlTextWriterTag.Span);
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "rrbIcon");
			writer.RenderBeginTag(HtmlTextWriterTag.Span);
			writer.Write("<!-- &nbsp; -->");
			writer.RenderEndTag();
			writer.RenderEndTag();
		}
	}
}
