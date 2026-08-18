using System;
using System.Text;
using System.Web.UI;

namespace Telerik.Web.UI.ButtonRendering.Lightweight
{
	// Token: 0x020000AB RID: 171
	public class ToggleRadioButtonRenderer : StandardButtonRenderer
	{
		// Token: 0x06000699 RID: 1689 RVA: 0x0001ABDB File Offset: 0x00018DDB
		public ToggleRadioButtonRenderer(ButtonRenderingOptions options) : base(options)
		{
		}

		// Token: 0x0600069A RID: 1690 RVA: 0x0001ABE4 File Offset: 0x00018DE4
		protected override void RenderButtonChildNodes(HtmlTextWriter writer)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("rbIcon p-icon");
			stringBuilder.Append(" ");
			stringBuilder.Append((this.options.Checked == true) ? "p-i-radio-checked rbToggleRadioChecked" : "p-i-radio rbToggleRadio");
			base.RenderSpan(writer, stringBuilder.ToString(), null);
			base.RenderTextHolder(writer);
		}

		// Token: 0x0600069B RID: 1691 RVA: 0x0001AC58 File Offset: 0x00018E58
		internal override void AddCustomCssClass(StringBuilder classes)
		{
			base.AddCssClass("rbRadioButton", classes);
		}
	}
}
