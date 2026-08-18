using System;
using System.Text;
using System.Web.UI;

namespace Telerik.Web.UI.ButtonRendering.Lightweight
{
	// Token: 0x020000AA RID: 170
	public class ToggleCheckBoxRenderer : StandardButtonRenderer
	{
		// Token: 0x06000695 RID: 1685 RVA: 0x0001AAED File Offset: 0x00018CED
		public ToggleCheckBoxRenderer(ButtonRenderingOptions options) : base(options)
		{
		}

		// Token: 0x06000696 RID: 1686 RVA: 0x0001AAF8 File Offset: 0x00018CF8
		protected override void RenderButtonChildNodes(HtmlTextWriter writer)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("rbIcon p-icon");
			stringBuilder.Append(" ");
			stringBuilder.Append((this.options.Checked == true) ? "p-i-checkbox-checked rbToggleCheckboxChecked" : "p-i-checkbox rbToggleCheckbox");
			base.RenderSpan(writer, stringBuilder.ToString(), null);
			base.RenderTextHolder(writer);
		}

		// Token: 0x06000697 RID: 1687 RVA: 0x0001AB6C File Offset: 0x00018D6C
		public override void AddAttributesToRender(HtmlTextWriter writer)
		{
			string value = string.IsNullOrEmpty(this.options.Value) ? this.options.Text : this.options.Value;
			writer.AddAttribute(HtmlTextWriterAttribute.Type, "button");
			writer.AddAttribute(HtmlTextWriterAttribute.Name, this.options.UniqueID);
			writer.AddAttribute(HtmlTextWriterAttribute.Value, value);
		}

		// Token: 0x06000698 RID: 1688 RVA: 0x0001ABCD File Offset: 0x00018DCD
		internal override void AddCustomCssClass(StringBuilder classes)
		{
			base.AddCssClass("rbCheckBox", classes);
		}
	}
}
