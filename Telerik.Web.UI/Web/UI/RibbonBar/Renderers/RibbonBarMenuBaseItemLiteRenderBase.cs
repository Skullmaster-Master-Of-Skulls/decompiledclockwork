using System;
using System.Web.UI;

namespace Telerik.Web.UI.RibbonBar.Renderers
{
	// Token: 0x020007B3 RID: 1971
	internal class RibbonBarMenuBaseItemLiteRenderBase : RibbonBarClickableItemLiteRenderBase
	{
		// Token: 0x060044CB RID: 17611 RVA: 0x000D93ED File Offset: 0x000D75ED
		public RibbonBarMenuBaseItemLiteRenderBase(RibbonBarItem owner) : base(owner)
		{
		}

		// Token: 0x17001636 RID: 5686
		// (get) Token: 0x060044CC RID: 17612 RVA: 0x000D93F6 File Offset: 0x000D75F6
		internal virtual RibbonBarClickableItem CurrentOwner
		{
			get
			{
				return (RibbonBarMenuBaseItem)base.Owner;
			}
		}

		// Token: 0x060044CD RID: 17613 RVA: 0x000D9403 File Offset: 0x000D7603
		public override void RenderDropDown(HtmlTextWriter writer)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "rrbMenu");
			writer.RenderBeginTag(HtmlTextWriterTag.Div);
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "rrbUL");
			writer.RenderBeginTag(HtmlTextWriterTag.Ul);
			this.RenderDropDownContents(writer);
			writer.RenderEndTag();
			writer.RenderEndTag();
		}

		// Token: 0x060044CE RID: 17614 RVA: 0x000D9444 File Offset: 0x000D7644
		protected override void RenderTextStructure(HtmlTextWriter writer)
		{
			if (!((RibbonBarMenuBaseItem)base.Owner).ShouldRenderTextStructure)
			{
				return;
			}
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "rrbText");
			writer.RenderBeginTag(HtmlTextWriterTag.Span);
			if (this.CurrentOwner.ShouldRenderTextContent)
			{
				writer.Write(this.TextToRender);
			}
			writer.RenderEndTag();
			this.RenderArrow(writer);
		}

		// Token: 0x060044CF RID: 17615 RVA: 0x000D94A0 File Offset: 0x000D76A0
		protected void RenderArrow(HtmlTextWriter writer)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "rrbArrow");
			writer.RenderBeginTag(HtmlTextWriterTag.Span);
			writer.AddAttribute(HtmlTextWriterAttribute.Class, RibbonBarStyles.Combine(new string[]
			{
				"radIcon",
				"radIconDown"
			}));
			writer.RenderBeginTag(HtmlTextWriterTag.Span);
			writer.Write("&nbsp;");
			writer.RenderEndTag();
			writer.RenderEndTag();
		}

		// Token: 0x060044D0 RID: 17616 RVA: 0x000D9506 File Offset: 0x000D7706
		protected virtual void RenderDropDownContents(HtmlTextWriter writer)
		{
		}
	}
}
