using System;
using System.Text;
using System.Web.UI;

namespace Telerik.Web.UI.ButtonRendering.Lightweight
{
	// Token: 0x020000EB RID: 235
	public class TemplateButtonRenderer : StandardButtonRenderer
	{
		// Token: 0x060009B4 RID: 2484 RVA: 0x000232A5 File Offset: 0x000214A5
		public TemplateButtonRenderer(Action<HtmlTextWriter> templateRenderAction, ButtonRenderingOptions options, ImageRenderingOptions imgOptions) : this(templateRenderAction, options)
		{
			this.imageOptions = imgOptions;
			this.imageRenderer = new ImageRenderer(options, imgOptions);
		}

		// Token: 0x060009B5 RID: 2485 RVA: 0x000232C3 File Offset: 0x000214C3
		public TemplateButtonRenderer(Action<HtmlTextWriter> templateRenderAction, ButtonRenderingOptions options) : base(options)
		{
			this.renderAction = templateRenderAction;
		}

		// Token: 0x060009B6 RID: 2486 RVA: 0x000232D3 File Offset: 0x000214D3
		protected override void RenderButtonChildNodes(HtmlTextWriter writer)
		{
			this.renderAction(writer);
		}

		// Token: 0x1700035C RID: 860
		// (get) Token: 0x060009B7 RID: 2487 RVA: 0x000232E4 File Offset: 0x000214E4
		public override string CssClassFormatString
		{
			get
			{
				if (this.imageOptions != null && this.options.HasImage)
				{
					StringBuilder stringBuilder = new StringBuilder();
					stringBuilder.Append(this.imageRenderer.GetButtonCssClasses(this));
					return stringBuilder.ToString();
				}
				return base.CssClassFormatString;
			}
		}

		// Token: 0x060009B8 RID: 2488 RVA: 0x0002332C File Offset: 0x0002152C
		public override void AddAttributesToRender(HtmlTextWriter writer)
		{
			base.AddAttributesToRender(writer);
			if (this.imageOptions != null)
			{
				this.imageRenderer.RenderBackgroundImage(writer);
			}
		}

		// Token: 0x0400026F RID: 623
		private readonly Action<HtmlTextWriter> renderAction;

		// Token: 0x04000270 RID: 624
		private readonly ImageRenderingOptions imageOptions;

		// Token: 0x04000271 RID: 625
		private readonly ImageRenderer imageRenderer;
	}
}
