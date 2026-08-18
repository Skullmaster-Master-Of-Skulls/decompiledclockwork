using System;
using System.Text;
using System.Web.UI;

namespace Telerik.Web.UI.ButtonRendering.Lightweight
{
	// Token: 0x020000E8 RID: 232
	public class ToggleButtonRenderer : StandardButtonRenderer
	{
		// Token: 0x060009A6 RID: 2470 RVA: 0x000230B9 File Offset: 0x000212B9
		public ToggleButtonRenderer(ButtonRenderingOptions options, IconRenderingOptions iconOptions) : base(options)
		{
			this.iconRenderer = new IconRenderer(options, iconOptions);
		}

		// Token: 0x060009A7 RID: 2471 RVA: 0x000230CF File Offset: 0x000212CF
		protected override void RenderButtonChildNodes(HtmlTextWriter writer)
		{
			this.iconRenderer.RenderPrimaryIcon(writer);
			base.RenderTextHolder(writer);
			this.iconRenderer.RenderSecondaryIcon(writer);
		}

		// Token: 0x060009A8 RID: 2472 RVA: 0x000230F0 File Offset: 0x000212F0
		internal override void AddCustomCssClass(StringBuilder classes)
		{
			base.AddCssClass("rbToggleButton", classes);
			base.AddCssClass(this.iconRenderer.HasPrimaryIconWithPosition ? "rbPrimary" : null, classes);
			base.AddCssClass(this.iconRenderer.HasSecondaryIconWithPosition ? "rbSecondary" : null, classes);
		}

		// Token: 0x0400024F RID: 591
		private readonly IconRenderer iconRenderer;
	}
}
