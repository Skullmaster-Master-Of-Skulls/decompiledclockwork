using System;
using System.Web.UI;

namespace Telerik.Web.UI.RibbonBar.Renderers
{
	// Token: 0x020007BA RID: 1978
	internal class RibbonBarTemplateLiteRenderer : RibbonBarItemRenderBase
	{
		// Token: 0x060044F4 RID: 17652 RVA: 0x000D9FF6 File Offset: 0x000D81F6
		public RibbonBarTemplateLiteRenderer(RibbonBarItem owner) : base(owner)
		{
		}

		// Token: 0x060044F5 RID: 17653 RVA: 0x000DA000 File Offset: 0x000D8200
		public override void AddAttributesToRender(HtmlTextWriter writer)
		{
			string value = RibbonBarStyles.Combine(new string[]
			{
				this.GetCssClass(),
				base.Owner.CssClass
			});
			writer.AddAttribute(HtmlTextWriterAttribute.Class, value);
		}

		// Token: 0x060044F6 RID: 17654 RVA: 0x000DA03B File Offset: 0x000D823B
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
