using System;
using System.Text;
using System.Web;
using System.Web.UI;

namespace Telerik.Web.UI.ButtonRendering.Lightweight
{
	// Token: 0x020000E4 RID: 228
	public class CheckBoxRenderer : StandardButtonRenderer
	{
		// Token: 0x06000989 RID: 2441 RVA: 0x0002285E File Offset: 0x00020A5E
		public CheckBoxRenderer(ButtonRenderingOptions options) : base(options)
		{
		}

		// Token: 0x0600098A RID: 2442 RVA: 0x00022868 File Offset: 0x00020A68
		protected override void RenderButtonChildNodes(HtmlTextWriter writer)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("rbIcon p-icon");
			stringBuilder.Append(" ");
			stringBuilder.Append((this.options.Checked == true) ? "p-i-checkbox-checked rbToggleCheckboxChecked" : "p-i-checkbox rbToggleCheckbox");
			this.RenderSpan(writer, stringBuilder.ToString(), "", this.options.InDesignMode);
			this.RenderSpan(writer, "rbText", this.options.Text, false);
		}

		// Token: 0x0600098B RID: 2443 RVA: 0x000228FC File Offset: 0x00020AFC
		public override void AddAttributesToRender(HtmlTextWriter writer)
		{
			string value = string.IsNullOrEmpty(this.options.Value) ? this.options.Text : this.options.Value;
			writer.AddAttribute(HtmlTextWriterAttribute.Type, "button");
			writer.AddAttribute(HtmlTextWriterAttribute.Name, this.options.UniqueID);
			writer.AddAttribute(HtmlTextWriterAttribute.Value, value);
		}

		// Token: 0x0600098C RID: 2444 RVA: 0x0002295D File Offset: 0x00020B5D
		internal override void AddCustomCssClass(StringBuilder classes)
		{
			base.AddCssClass("rbCheckBox", classes);
		}

		// Token: 0x0600098D RID: 2445 RVA: 0x0002296C File Offset: 0x00020B6C
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
