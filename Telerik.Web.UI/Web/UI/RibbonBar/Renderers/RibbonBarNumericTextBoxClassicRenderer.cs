using System;
using System.Web.UI;

namespace Telerik.Web.UI.RibbonBar.Renderers
{
	// Token: 0x020007A6 RID: 1958
	internal class RibbonBarNumericTextBoxClassicRenderer : RibbonBarItemRenderBase
	{
		// Token: 0x06004482 RID: 17538 RVA: 0x000D7B4C File Offset: 0x000D5D4C
		public RibbonBarNumericTextBoxClassicRenderer(RibbonBarItem owner) : base(owner)
		{
		}

		// Token: 0x06004483 RID: 17539 RVA: 0x000D7B58 File Offset: 0x000D5D58
		public override void AddAttributesToRender(HtmlTextWriter writer)
		{
			string cssClass = base.Owner.CssClass;
			string text = base.Owner.Enabled ? string.Empty : "rrbDisabled";
			base.Owner.CssClass = RibbonBarStyles.Combine(new string[]
			{
				"rrbNumericTextBox",
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

		// Token: 0x06004484 RID: 17540 RVA: 0x000D7BFC File Offset: 0x000D5DFC
		public override void RenderContents(HtmlTextWriter writer)
		{
			if (!string.IsNullOrEmpty(base.Owner.AccessKey))
			{
				this.RenderKeyboardBox(writer);
			}
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "rrbNTBInner");
			writer.RenderBeginTag(HtmlTextWriterTag.Span);
			this.RenderInput(writer);
			this.RenderButtons(writer);
			writer.RenderEndTag();
		}

		// Token: 0x06004485 RID: 17541 RVA: 0x000D7C4B File Offset: 0x000D5E4B
		protected void RenderKeyboardBox(HtmlTextWriter writer)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "rrbKeyBox");
			writer.RenderBeginTag(HtmlTextWriterTag.Span);
			writer.Write(base.Owner.AccessKey);
			writer.RenderEndTag();
		}

		// Token: 0x06004486 RID: 17542 RVA: 0x000D7C7C File Offset: 0x000D5E7C
		protected virtual void RenderInput(HtmlTextWriter writer)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "rrbNTBInput radPreventDecorate");
			writer.AddAttribute(HtmlTextWriterAttribute.Type, "text");
			if (((RibbonBarNumericTextBox)base.Owner).Value != null)
			{
				string value = ((RibbonBarNumericTextBox)base.Owner).Prefix + ((RibbonBarNumericTextBox)base.Owner).Value + ((RibbonBarNumericTextBox)base.Owner).Suffix;
				writer.AddAttribute(HtmlTextWriterAttribute.Value, value);
			}
			if (!base.Owner.Enabled)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.ReadOnly, "readonly");
			}
			writer.AddAttribute(HtmlTextWriterAttribute.Name, base.Owner.RibbonBar.ClientID + ":" + base.Owner.RibbonBar.GetItemHierarchicalIndex(base.Owner));
			writer.RenderBeginTag(HtmlTextWriterTag.Input);
			writer.RenderEndTag();
		}

		// Token: 0x06004487 RID: 17543 RVA: 0x000D7D61 File Offset: 0x000D5F61
		protected void RenderButtons(HtmlTextWriter writer)
		{
			this.RenderUpButton(writer);
			this.RenderDownButton(writer);
		}

		// Token: 0x06004488 RID: 17544 RVA: 0x000D7D71 File Offset: 0x000D5F71
		protected void RenderUpButton(HtmlTextWriter writer)
		{
			this.RenderButton(writer, "rrbActionButtonUp", "Increase", "rrbIconUp");
		}

		// Token: 0x06004489 RID: 17545 RVA: 0x000D7D89 File Offset: 0x000D5F89
		protected void RenderDownButton(HtmlTextWriter writer)
		{
			this.RenderButton(writer, "rrbActionButtonDown", "Decrease", "rrbIconDown");
		}

		// Token: 0x0600448A RID: 17546 RVA: 0x000D7DA4 File Offset: 0x000D5FA4
		protected void RenderButton(HtmlTextWriter writer, string buttonClassName, string buttonText, string buttonImageClassName)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "rrbActionButton " + buttonClassName);
			writer.AddAttribute(HtmlTextWriterAttribute.Type, "button");
			writer.RenderBeginTag(HtmlTextWriterTag.Button);
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "rrbIcon " + buttonImageClassName);
			writer.RenderBeginTag(HtmlTextWriterTag.Span);
			writer.Write("<!-- &nbsp; -->");
			writer.RenderEndTag();
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "rrbButtonText");
			writer.RenderBeginTag(HtmlTextWriterTag.Span);
			writer.Write(buttonText);
			writer.RenderEndTag();
			writer.RenderEndTag();
		}
	}
}
