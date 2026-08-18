using System;
using System.Text;
using System.Web;
using System.Web.UI;

namespace Telerik.Web.UI.ButtonRendering.Lightweight
{
	// Token: 0x020000EC RID: 236
	public class RadioButtonRenderer : StandardButtonRenderer
	{
		// Token: 0x060009B9 RID: 2489 RVA: 0x00023349 File Offset: 0x00021549
		public RadioButtonRenderer(ButtonRenderingOptions options) : base(options)
		{
		}

		// Token: 0x060009BA RID: 2490 RVA: 0x00023354 File Offset: 0x00021554
		protected override void RenderButtonChildNodes(HtmlTextWriter writer)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("rbIcon p-icon");
			stringBuilder.Append(" ");
			stringBuilder.Append((this.options.Checked == true) ? "p-i-radio-checked rbToggleRadioChecked" : "p-i-radio rbToggleRadio");
			this.RenderSpan(writer, stringBuilder.ToString(), "", this.options.InDesignMode);
			this.RenderSpan(writer, "rbText", this.options.Text, false);
		}

		// Token: 0x060009BB RID: 2491 RVA: 0x000233E8 File Offset: 0x000215E8
		internal override void AddCustomCssClass(StringBuilder classes)
		{
			base.AddCssClass("rbRadioButton", classes);
		}

		// Token: 0x060009BC RID: 2492 RVA: 0x000233F8 File Offset: 0x000215F8
		internal void RenderSpan(HtmlTextWriter writer, string cssClasses, string content, bool isDesignMode)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Class, cssClasses);
			writer.RenderBeginTag(HtmlTextWriterTag.Span);
			if (!string.IsNullOrEmpty(content))
			{
				writer.Write(HttpUtility.HtmlEncode(content));
			}
			if (isDesignMode)
			{
				base.RenderSpan(writer, "rbDesignModeIcon", "");
			}
			writer.RenderEndTag();
		}
	}
}
