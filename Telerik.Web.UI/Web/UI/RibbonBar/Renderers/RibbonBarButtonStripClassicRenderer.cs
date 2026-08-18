using System;
using System.Web.UI;

namespace Telerik.Web.UI.RibbonBar.Renderers
{
	// Token: 0x020007A5 RID: 1957
	internal class RibbonBarButtonStripClassicRenderer : RibbonBarItemRenderBase
	{
		// Token: 0x0600447E RID: 17534 RVA: 0x000D7AA9 File Offset: 0x000D5CA9
		public RibbonBarButtonStripClassicRenderer(RibbonBarItem owner) : base(owner)
		{
		}

		// Token: 0x1700162B RID: 5675
		// (get) Token: 0x0600447F RID: 17535 RVA: 0x000D7AB2 File Offset: 0x000D5CB2
		public override HtmlTextWriterTag TagKey
		{
			get
			{
				return HtmlTextWriterTag.Span;
			}
		}

		// Token: 0x06004480 RID: 17536 RVA: 0x000D7AB8 File Offset: 0x000D5CB8
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

		// Token: 0x06004481 RID: 17537 RVA: 0x000D7B20 File Offset: 0x000D5D20
		protected virtual string GetCssClass()
		{
			return RibbonBarStyles.Combine(new string[]
			{
				"rrbButtonOut",
				"rrbButtonStrip"
			});
		}
	}
}
