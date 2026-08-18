using System;
using System.Text;
using System.Web.UI;

namespace Telerik.Web.UI.ButtonRendering.Lightweight
{
	// Token: 0x020000E6 RID: 230
	public class ImageButtonRenderer : StandardButtonRenderer
	{
		// Token: 0x0600099F RID: 2463 RVA: 0x00022EB0 File Offset: 0x000210B0
		public ImageButtonRenderer(ButtonRenderingOptions options, IconRenderingOptions iconOptions, ImageRenderingOptions imgOptions) : base(options)
		{
			this.imageOptions = imgOptions;
			this.iconRenderer = new IconRenderer(options, iconOptions);
			this.imageRenderer = new ImageRenderer(options, imgOptions);
			this.hasToggleStates = (options.ToggleType == ButtonToggleType.CustomToggle && options.ToggleStatesCount != 0);
			this.isImageButtonOnly = (options.HasImage && !options.HasBackgroundImage && !this.hasToggleStates);
		}

		// Token: 0x17000359 RID: 857
		// (get) Token: 0x060009A0 RID: 2464 RVA: 0x00022F28 File Offset: 0x00021128
		public override string CssClassFormatString
		{
			get
			{
				StringBuilder stringBuilder = new StringBuilder();
				bool flag = this.options.HasImage && !this.hasToggleStates;
				stringBuilder.Append(flag ? this.imageRenderer.GetButtonCssClasses(this) : base.CssClassFormatString);
				base.AddCssClass(this.iconRenderer.HasPrimaryIconWithPosition ? "rbPrimary" : null, stringBuilder);
				base.AddCssClass(this.iconRenderer.HasSecondaryIconWithPosition ? "rbSecondary" : null, stringBuilder);
				return stringBuilder.ToString();
			}
		}

		// Token: 0x060009A1 RID: 2465 RVA: 0x00022FB1 File Offset: 0x000211B1
		protected override void RenderButtonChildNodes(HtmlTextWriter writer)
		{
			if (!this.isImageButtonOnly)
			{
				this.iconRenderer.RenderPrimaryIcon(writer);
				base.RenderTextHolder(writer);
				this.iconRenderer.RenderSecondaryIcon(writer);
			}
		}

		// Token: 0x060009A2 RID: 2466 RVA: 0x00022FDA File Offset: 0x000211DA
		public override void AddAttributesToRender(HtmlTextWriter writer)
		{
			base.AddAttributesToRender(writer);
			this.imageRenderer.RenderBackgroundImage(writer);
		}

		// Token: 0x04000248 RID: 584
		private readonly ImageRenderingOptions imageOptions;

		// Token: 0x04000249 RID: 585
		private readonly IconRenderer iconRenderer;

		// Token: 0x0400024A RID: 586
		private readonly ImageRenderer imageRenderer;

		// Token: 0x0400024B RID: 587
		private readonly bool isImageButtonOnly;

		// Token: 0x0400024C RID: 588
		private readonly bool hasToggleStates;
	}
}
