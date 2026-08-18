using System;
using System.Web.UI;

namespace Telerik.Web.UI.RibbonBar.Renderers
{
	// Token: 0x02000798 RID: 1944
	internal class RibbonBarTemplateClassicRenderer : RibbonBarItemRenderBase
	{
		// Token: 0x0600443C RID: 17468 RVA: 0x000D6486 File Offset: 0x000D4686
		public RibbonBarTemplateClassicRenderer(RibbonBarItem owner) : base(owner)
		{
		}

		// Token: 0x0600443D RID: 17469 RVA: 0x000D6490 File Offset: 0x000D4690
		public override void AddAttributesToRender(HtmlTextWriter writer)
		{
			string value = RibbonBarStyles.Combine(new string[]
			{
				this.GetCssClass(),
				base.Owner.CssClass
			});
			writer.AddAttribute(HtmlTextWriterAttribute.Class, value);
		}

		// Token: 0x0600443E RID: 17470 RVA: 0x000D64CB File Offset: 0x000D46CB
		protected virtual string GetCssClass()
		{
			if (((RibbonBarTemplateItem)base.Owner).Size == RibbonBarItemSize.Large)
			{
				return "rrbTemplateItemLarge";
			}
			return "rrbTemplateItem";
		}
	}
}
