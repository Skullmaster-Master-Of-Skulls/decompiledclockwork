using System;
using System.Web.UI;

namespace Telerik.Web.UI.RibbonBar.Renderers
{
	// Token: 0x020007B7 RID: 1975
	internal class RibbonBarButtonStripLiteRenderer : RibbonBarItemRenderBase
	{
		// Token: 0x060044DC RID: 17628 RVA: 0x000D999C File Offset: 0x000D7B9C
		public RibbonBarButtonStripLiteRenderer(RibbonBarItem owner) : base(owner)
		{
		}

		// Token: 0x17001639 RID: 5689
		// (get) Token: 0x060044DD RID: 17629 RVA: 0x000D99A5 File Offset: 0x000D7BA5
		public override HtmlTextWriterTag TagKey
		{
			get
			{
				return HtmlTextWriterTag.Div;
			}
		}

		// Token: 0x060044DE RID: 17630 RVA: 0x000D99AC File Offset: 0x000D7BAC
		public override void AddAttributesToRender(HtmlTextWriter writer)
		{
			string cssClass = base.Owner.CssClass;
			base.Owner.CssClass = RibbonBarStyles.Combine(new string[]
			{
				this.GetCssClass(),
				base.Owner.CssClass
			});
			base.Owner.BaseAddAttributesToRender(writer);
			if (!string.IsNullOrEmpty(cssClass))
			{
				base.Owner.CssClass = cssClass;
			}
		}

		// Token: 0x060044DF RID: 17631 RVA: 0x000D9A14 File Offset: 0x000D7C14
		protected virtual string GetCssClass()
		{
			return "rrbButtonGroup";
		}
	}
}
