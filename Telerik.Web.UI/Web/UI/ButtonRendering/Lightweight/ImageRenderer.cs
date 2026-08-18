using System;
using System.Text;
using System.Web.UI;

namespace Telerik.Web.UI.ButtonRendering.Lightweight
{
	// Token: 0x020000E7 RID: 231
	internal class ImageRenderer
	{
		// Token: 0x060009A3 RID: 2467 RVA: 0x00022FEF File Offset: 0x000211EF
		public ImageRenderer(ButtonRenderingOptions btnOptions, ImageRenderingOptions imgOptions)
		{
			this.buttonOptions = btnOptions;
			this.imageOptions = imgOptions;
		}

		// Token: 0x060009A4 RID: 2468 RVA: 0x00023008 File Offset: 0x00021208
		public string GetButtonCssClasses(StandardButtonRenderer renderer)
		{
			StringBuilder stringBuilder = new StringBuilder();
			renderer.AddCommonCssClasses(stringBuilder);
			renderer.AddCustomCssClass(stringBuilder);
			renderer.AddCssClass("rbImageButton", stringBuilder);
			return stringBuilder.ToString();
		}

		// Token: 0x060009A5 RID: 2469 RVA: 0x0002303C File Offset: 0x0002123C
		public void RenderBackgroundImage(HtmlTextWriter writer)
		{
			string text = (!this.buttonOptions.OriginalEnabled && !string.IsNullOrEmpty(this.imageOptions.DisabledImageUrl)) ? this.imageOptions.DisabledImageUrl : this.imageOptions.ImageUrl;
			if (!string.IsNullOrEmpty(text))
			{
				writer.AddStyleAttribute(HtmlTextWriterStyle.BackgroundImage, string.Format("'{0}'", text));
				if (this.imageOptions.Sizing == ImageSizing.Stretch)
				{
					writer.AddStyleAttribute("background-size", "100% 100%");
				}
			}
		}

		// Token: 0x0400024D RID: 589
		private readonly ImageRenderingOptions imageOptions;

		// Token: 0x0400024E RID: 590
		private readonly ButtonRenderingOptions buttonOptions;
	}
}
