using System;
using System.Text;
using System.Web;
using System.Web.UI;

namespace Telerik.Web.UI.ButtonRendering.Lightweight
{
	// Token: 0x02000017 RID: 23
	public class SwitchRenderer : StandardButtonRenderer
	{
		// Token: 0x0600012B RID: 299 RVA: 0x00003EFE File Offset: 0x000020FE
		public SwitchRenderer(ButtonRenderingOptions options, SwitchToggleStatesSettings switchToggleStates) : base(options)
		{
			this.switchToggleStates = switchToggleStates;
		}

		// Token: 0x0600012C RID: 300 RVA: 0x00003F10 File Offset: 0x00002110
		protected override void RenderButtonChildNodes(HtmlTextWriter writer)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("rbIcon p-icon");
			stringBuilder.Append(" ");
			stringBuilder.Append((this.options.Checked == true) ? "p-i-checkbox-checked rbToggleCheckboxChecked" : "p-i-checkbox rbToggleCheckbox");
			this.RenderSpan(writer, "rbText", this.options.Text, false);
		}

		// Token: 0x0600012D RID: 301 RVA: 0x00003F88 File Offset: 0x00002188
		public override void AddAttributesToRender(HtmlTextWriter writer)
		{
			string value = string.IsNullOrEmpty(this.options.Value) ? this.options.Text : this.options.Value;
			writer.AddAttribute(HtmlTextWriterAttribute.Type, "button");
			writer.AddAttribute(HtmlTextWriterAttribute.Name, this.options.UniqueID);
			writer.AddAttribute(HtmlTextWriterAttribute.Value, value);
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x0600012E RID: 302 RVA: 0x00003FEC File Offset: 0x000021EC
		public override string CssClassFormatString
		{
			get
			{
				StringBuilder stringBuilder = new StringBuilder(base.CssClassFormatString);
				base.AddSkinCssClass(stringBuilder);
				this.AddCustomCssClass(stringBuilder);
				return stringBuilder.ToString();
			}
		}

		// Token: 0x0600012F RID: 303 RVA: 0x0000401C File Offset: 0x0000221C
		internal override void AddCustomCssClass(StringBuilder classes)
		{
			string str = "k-switch-" + ((this.options.Checked == true) ? "on" : "off");
			base.AddCssClass("k-switch k-widget " + str, classes);
		}

		// Token: 0x06000130 RID: 304 RVA: 0x00004074 File Offset: 0x00002274
		internal void RenderSpan(HtmlTextWriter writer, string cssClasses, string content, bool isDesignMode)
		{
			if (isDesignMode)
			{
				return;
			}
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "k-switch-container");
			writer.RenderBeginTag(HtmlTextWriterTag.Span);
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "k-switch-label-on");
			writer.RenderBeginTag(HtmlTextWriterTag.Span);
			if (!string.IsNullOrEmpty(this.switchToggleStates.ToggleStateOn.Text))
			{
				writer.Write(HttpUtility.HtmlEncode(this.switchToggleStates.ToggleStateOn.Text));
			}
			writer.RenderEndTag();
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "k-switch-label-off");
			writer.RenderBeginTag(HtmlTextWriterTag.Span);
			if (!string.IsNullOrEmpty(this.switchToggleStates.ToggleStateOff.Text))
			{
				writer.Write(HttpUtility.HtmlEncode(this.switchToggleStates.ToggleStateOff.Text));
			}
			writer.RenderEndTag();
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "k-switch-handle");
			writer.RenderBeginTag(HtmlTextWriterTag.Span);
			writer.RenderEndTag();
			writer.RenderEndTag();
		}

		// Token: 0x04000014 RID: 20
		private readonly SwitchToggleStatesSettings switchToggleStates;
	}
}
