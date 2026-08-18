using System;
using System.Web.UI;

namespace Telerik.Web.UI.RibbonBar.Renderers
{
	// Token: 0x020007AC RID: 1964
	internal class RibbonBarButtonLiteRenderer : RibbonBarClickableItemLiteRenderBase
	{
		// Token: 0x060044AD RID: 17581 RVA: 0x000D8AD1 File Offset: 0x000D6CD1
		public RibbonBarButtonLiteRenderer(RibbonBarItem owner) : base(owner)
		{
		}

		// Token: 0x17001632 RID: 5682
		// (get) Token: 0x060044AE RID: 17582 RVA: 0x000D8ADA File Offset: 0x000D6CDA
		public override HtmlTextWriterTag TagKey
		{
			get
			{
				return HtmlTextWriterTag.Span;
			}
		}

		// Token: 0x060044AF RID: 17583 RVA: 0x000D8ADE File Offset: 0x000D6CDE
		public override void RenderBeginTagContext(HtmlTextWriter writer)
		{
		}

		// Token: 0x17001633 RID: 5683
		// (get) Token: 0x060044B0 RID: 17584 RVA: 0x000D8AE0 File Offset: 0x000D6CE0
		public override string CssClassFormatString
		{
			get
			{
				string text = (((RibbonBarButton)base.Owner).ImageRenderingMode == RibbonBarImageRenderingMode.Dual) ? "rrbDualImage" : string.Empty;
				string text2 = ((RibbonBarButton)base.Owner).ShouldRenderButtonStripClasses ? "rrbButton" : "rrbButton";
				string text3 = ((RibbonBarButton)base.Owner).ShouldRenderButtonStripClasses ? "rrbSmallButton" : base.SizeCssClass;
				return RibbonBarStyles.Combine(new string[]
				{
					text2,
					text3,
					text
				});
			}
		}

		// Token: 0x060044B1 RID: 17585 RVA: 0x000D8B6C File Offset: 0x000D6D6C
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
