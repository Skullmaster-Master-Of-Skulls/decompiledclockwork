using System;
using System.Text;
using System.Web;
using System.Web.UI;
using Telerik.Web.UI.Renderers;

namespace Telerik.Web.UI.ButtonRendering.Lightweight
{
	// Token: 0x02000016 RID: 22
	public class StandardButtonRenderer : RendererBase
	{
		// Token: 0x0600011F RID: 287 RVA: 0x00003BC8 File Offset: 0x00001DC8
		public StandardButtonRenderer(ButtonRenderingOptions renderOptions)
		{
			this.options = renderOptions;
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x06000120 RID: 288 RVA: 0x00003BD7 File Offset: 0x00001DD7
		public override HtmlTextWriterTag TagKey
		{
			get
			{
				return HtmlTextWriterTag.Button;
			}
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x06000121 RID: 289 RVA: 0x00003BDC File Offset: 0x00001DDC
		public override string CssClassFormatString
		{
			get
			{
				StringBuilder stringBuilder = new StringBuilder();
				this.AddCommonCssClasses(stringBuilder);
				if (!this.options.EnableBrowserButtonStyle)
				{
					this.AddSkinCssClass(stringBuilder);
					this.AddCustomCssClass(stringBuilder);
					this.AddCssClass("rbRounded", stringBuilder);
					if (this.options.ButtonType == RadButtonType.LinkButton && !this.options.IsTemplateInitialized)
					{
						this.AddCssClass("rbLink", stringBuilder);
					}
					if (string.IsNullOrEmpty(this.options.Text))
					{
						this.AddCssClass("rbIconOnly", stringBuilder);
					}
					else
					{
						this.AddCssClass("rbIconButton", stringBuilder);
					}
				}
				return stringBuilder.ToString();
			}
		}

		// Token: 0x06000122 RID: 290 RVA: 0x00003C78 File Offset: 0x00001E78
		internal void AddCommonCssClasses(StringBuilder classes)
		{
			this.AddCssClass("RadButton", classes);
			if (this.options.ReadOnly)
			{
				this.AddCssClass(this.options.ReadOnlyCssClass, classes);
			}
			bool flag = !this.options.IsButtonEnabled || !this.options.OriginalEnabled;
			if (flag)
			{
				this.AddCssClass("rbDisabled", classes);
				this.AddCssClass(this.options.DisabledButtonCssClass, classes);
			}
			if (this.options.Checked == true && !flag && !this.options.IsTemplateInitialized && this.options.ButtonType != RadButtonType.ToggleButton && (this.options.ToggleType == ButtonToggleType.CheckBox || this.options.ToggleType == ButtonToggleType.Radio))
			{
				this.AddCssClass("rbToggled", classes);
			}
			if (this.options.Primary)
			{
				this.AddCssClass("rbPrimaryButton", classes);
			}
		}

		// Token: 0x06000123 RID: 291 RVA: 0x00003D70 File Offset: 0x00001F70
		internal void AddSkinCssClass(StringBuilder classes)
		{
			this.AddCssClass("RadButton_" + this.options.Skin, classes);
		}

		// Token: 0x06000124 RID: 292 RVA: 0x00003D8E File Offset: 0x00001F8E
		internal virtual void AddCustomCssClass(StringBuilder classes)
		{
			this.AddCssClass("rbButton", classes);
		}

		// Token: 0x06000125 RID: 293 RVA: 0x00003D9C File Offset: 0x00001F9C
		internal void AddCssClass(string className, StringBuilder classes)
		{
			if (!string.IsNullOrEmpty(className))
			{
				if (classes.Length != 0)
				{
					classes.Append(" ");
				}
				classes.Append(className);
			}
		}

		// Token: 0x06000126 RID: 294 RVA: 0x00003DC4 File Offset: 0x00001FC4
		public override void AddAttributesToRender(HtmlTextWriter writer)
		{
			string value = this.options.IsClientSubmit ? "button" : "submit";
			string value2 = string.IsNullOrEmpty(this.options.Value) ? this.options.Text : this.options.Value;
			writer.AddAttribute(HtmlTextWriterAttribute.Type, value);
			writer.AddAttribute(HtmlTextWriterAttribute.Name, this.options.UniqueID);
			writer.AddAttribute(HtmlTextWriterAttribute.Value, value2);
		}

		// Token: 0x06000127 RID: 295 RVA: 0x00003E3C File Offset: 0x0000203C
		public override void RenderContents(HtmlTextWriter writer)
		{
			base.RenderContents(writer);
			if (this.options.InDesignMode)
			{
				writer.Write(this.options.DesignTimeStyleSheet);
				writer.AddStyleAttribute(HtmlTextWriterStyle.Position, "relative");
				writer.RenderBeginTag(HtmlTextWriterTag.Div);
			}
			this.RenderButtonChildNodes(writer);
			if (this.options.InDesignMode)
			{
				writer.RenderEndTag();
			}
		}

		// Token: 0x06000128 RID: 296 RVA: 0x00003E9D File Offset: 0x0000209D
		protected virtual void RenderButtonChildNodes(HtmlTextWriter writer)
		{
			this.RenderTextHolder(writer);
		}

		// Token: 0x06000129 RID: 297 RVA: 0x00003EA6 File Offset: 0x000020A6
		internal void RenderTextHolder(HtmlTextWriter writer)
		{
			if (!string.IsNullOrEmpty(this.options.Text))
			{
				this.RenderSpan(writer, "rbText", this.options.Text);
			}
		}

		// Token: 0x0600012A RID: 298 RVA: 0x00003ED1 File Offset: 0x000020D1
		internal void RenderSpan(HtmlTextWriter writer, string cssClasses, string content)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Class, cssClasses);
			writer.RenderBeginTag(HtmlTextWriterTag.Span);
			if (!string.IsNullOrEmpty(content))
			{
				writer.Write(HttpUtility.HtmlEncode(content));
			}
			writer.RenderEndTag();
		}

		// Token: 0x04000013 RID: 19
		protected readonly ButtonRenderingOptions options;
	}
}
