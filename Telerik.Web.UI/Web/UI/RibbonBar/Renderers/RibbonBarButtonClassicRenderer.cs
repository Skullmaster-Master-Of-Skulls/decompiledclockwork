using System;
using System.Web.UI;

namespace Telerik.Web.UI.RibbonBar.Renderers
{
	// Token: 0x0200079F RID: 1951
	internal class RibbonBarButtonClassicRenderer : RibbonBarClickableItemClassicRenderBase
	{
		// Token: 0x06004466 RID: 17510 RVA: 0x000D722C File Offset: 0x000D542C
		public RibbonBarButtonClassicRenderer(RibbonBarItem owner) : base(owner)
		{
		}

		// Token: 0x1700162A RID: 5674
		// (get) Token: 0x06004467 RID: 17511 RVA: 0x000D7238 File Offset: 0x000D5438
		public override string CssClassFormatString
		{
			get
			{
				string text = (((RibbonBarButton)base.Owner).ImageRenderingMode == RibbonBarImageRenderingMode.Dual) ? "rrbDualImage" : string.Empty;
				string text2 = ((RibbonBarButton)base.Owner).ShouldRenderButtonStripClasses ? "rrbButtonStripPart" : "rrbButtonOut";
				string text3 = ((RibbonBarButton)base.Owner).ShouldRenderButtonStripClasses ? string.Empty : base.SizeCssClass;
				return RibbonBarStyles.Combine(new string[]
				{
					text2,
					text3,
					text
				});
			}
		}

		// Token: 0x06004468 RID: 17512 RVA: 0x000D72C4 File Offset: 0x000D54C4
		protected override void RenderImage(HtmlTextWriter writer)
		{
			bool flag = base.Owner.ParentWebControl is RibbonBarSplitButton;
			if (!flag || (this.ImageUrlToRender == base.Owner.ResolveUrl(((RibbonBarButton)base.Owner).ImageUrl) && ((RibbonBarButton)base.Owner).Enabled) || (this.ImageUrlToRender == base.Owner.ResolveUrl(((RibbonBarButton)base.Owner).DisabledImageUrl) && !((RibbonBarButton)base.Owner).Enabled))
			{
				base.RenderImage(writer);
			}
		}
	}
}
