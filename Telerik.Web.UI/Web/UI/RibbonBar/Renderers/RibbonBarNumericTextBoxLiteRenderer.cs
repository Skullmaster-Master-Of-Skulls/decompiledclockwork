using System;
using System.Web.UI;

namespace Telerik.Web.UI.RibbonBar.Renderers
{
	// Token: 0x020007B9 RID: 1977
	internal class RibbonBarNumericTextBoxLiteRenderer : RibbonBarItemRenderBase
	{
		// Token: 0x060044EB RID: 17643 RVA: 0x000D9CFB File Offset: 0x000D7EFB
		public RibbonBarNumericTextBoxLiteRenderer(RibbonBarItem owner) : base(owner)
		{
		}

		// Token: 0x060044EC RID: 17644 RVA: 0x000D9D04 File Offset: 0x000D7F04
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

		// Token: 0x060044ED RID: 17645 RVA: 0x000D9DA8 File Offset: 0x000D7FA8
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

		// Token: 0x060044EE RID: 17646 RVA: 0x000D9DF7 File Offset: 0x000D7FF7
		protected void RenderKeyboardBox(HtmlTextWriter writer)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "rrbKeyBox");
			writer.RenderBeginTag(HtmlTextWriterTag.Span);
			writer.Write(base.Owner.AccessKey);
			writer.RenderEndTag();
		}

		// Token: 0x060044EF RID: 17647 RVA: 0x000D9E28 File Offset: 0x000D8028
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

		// Token: 0x060044F0 RID: 17648 RVA: 0x000D9F0D File Offset: 0x000D810D
		protected void RenderButtons(HtmlTextWriter writer)
		{
			this.RenderUpButton(writer);
			this.RenderDownButton(writer);
		}

		// Token: 0x060044F1 RID: 17649 RVA: 0x000D9F1D File Offset: 0x000D811D
		protected void RenderUpButton(HtmlTextWriter writer)
		{
			this.RenderButton(writer, "rrbButtonUp", "Increase", "radIconUp");
		}

		// Token: 0x060044F2 RID: 17650 RVA: 0x000D9F35 File Offset: 0x000D8135
		protected void RenderDownButton(HtmlTextWriter writer)
		{
			this.RenderButton(writer, "rrbButtonDown", "Decrease", "radIconDown");
		}

		// Token: 0x060044F3 RID: 17651 RVA: 0x000D9F50 File Offset: 0x000D8150
		protected void RenderButton(HtmlTextWriter writer, string buttonClassName, string buttonText, string buttonImageClassName)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Class, RibbonBarStyles.Combine(new string[]
			{
				"rrbButton",
				buttonClassName
			}));
			writer.AddAttribute(HtmlTextWriterAttribute.Type, "button");
			writer.RenderBeginTag(HtmlTextWriterTag.Button);
			writer.AddAttribute(HtmlTextWriterAttribute.Class, RibbonBarStyles.Combine(new string[]
			{
				"radIcon",
				buttonImageClassName
			}));
			writer.RenderBeginTag(HtmlTextWriterTag.Span);
			writer.Write("<!-- &nbsp; -->");
			writer.RenderEndTag();
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "rrbText");
			writer.RenderBeginTag(HtmlTextWriterTag.Span);
			writer.Write(buttonText);
			writer.RenderEndTag();
			writer.RenderEndTag();
		}
	}
}
