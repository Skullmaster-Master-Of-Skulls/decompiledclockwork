using System;
using System.Web.UI;

namespace Telerik.Web.UI.RibbonBar.Renderers
{
	// Token: 0x0200079B RID: 1947
	internal class RibbonBarMenuBaseItemClassicRenderBase : RibbonBarClickableItemClassicRenderBase
	{
		// Token: 0x06004455 RID: 17493 RVA: 0x000D6C79 File Offset: 0x000D4E79
		public RibbonBarMenuBaseItemClassicRenderBase(RibbonBarItem owner) : base(owner)
		{
		}

		// Token: 0x17001627 RID: 5671
		// (get) Token: 0x06004456 RID: 17494 RVA: 0x000D6C82 File Offset: 0x000D4E82
		internal virtual RibbonBarClickableItem CurrentOwner
		{
			get
			{
				return (RibbonBarMenuBaseItem)base.Owner;
			}
		}

		// Token: 0x06004457 RID: 17495 RVA: 0x000D6C90 File Offset: 0x000D4E90
		public override void RenderDropDown(HtmlTextWriter writer)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "rrbMenuGroupOut");
			writer.RenderBeginTag(HtmlTextWriterTag.Div);
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "rrbMenuGroupMid");
			writer.RenderBeginTag(HtmlTextWriterTag.Div);
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "rrbMenuGroupIn");
			writer.RenderBeginTag(HtmlTextWriterTag.Div);
			this.RenderDropDownContents(writer);
			writer.RenderEndTag();
			writer.RenderEndTag();
			writer.RenderEndTag();
		}

		// Token: 0x06004458 RID: 17496 RVA: 0x000D6CF8 File Offset: 0x000D4EF8
		protected override void RenderTextStructure(HtmlTextWriter writer)
		{
			if (!((RibbonBarMenuBaseItem)base.Owner).ShouldRenderTextStructure)
			{
				return;
			}
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "rrbButtonText");
			writer.RenderBeginTag(HtmlTextWriterTag.Span);
			if (this.CurrentOwner.ShouldRenderTextContent)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Class, "rrbTextContent");
				writer.RenderBeginTag(HtmlTextWriterTag.Span);
				writer.Write(this.TextToRender);
				writer.RenderEndTag();
			}
			writer.RenderEndTag();
			this.RenderArrow(writer);
		}

		// Token: 0x06004459 RID: 17497 RVA: 0x000D6D70 File Offset: 0x000D4F70
		protected void RenderArrow(HtmlTextWriter writer)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "rrbButtonArrow");
			writer.RenderBeginTag(HtmlTextWriterTag.Span);
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "rrbIcon");
			writer.RenderBeginTag(HtmlTextWriterTag.Span);
			writer.Write("&nbsp;");
			writer.RenderEndTag();
			writer.RenderEndTag();
		}

		// Token: 0x0600445A RID: 17498 RVA: 0x000D6DBE File Offset: 0x000D4FBE
		protected virtual void RenderDropDownContents(HtmlTextWriter writer)
		{
			throw new NotImplementedException();
		}
	}
}
