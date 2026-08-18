using System;
using System.Web.UI;
using Telerik.Web.UI.RibbonBar.Renderers;

namespace Telerik.Web.UI
{
	// Token: 0x020007CD RID: 1997
	public abstract class RibbonBarMenuBaseItem : RibbonBarClickableItem
	{
		// Token: 0x1700167B RID: 5755
		// (get) Token: 0x060045AA RID: 17834 RVA: 0x000DB71A File Offset: 0x000D991A
		internal override bool ShouldRenderTextStructure
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700167C RID: 5756
		// (get) Token: 0x060045AB RID: 17835 RVA: 0x000DB71D File Offset: 0x000D991D
		internal override bool ShouldRenderTextContent
		{
			get
			{
				return this.Size != RibbonBarItemSize.Small;
			}
		}

		// Token: 0x060045AC RID: 17836 RVA: 0x000DB72B File Offset: 0x000D992B
		protected override void Render(HtmlTextWriter writer)
		{
			base.Render(writer);
			((RibbonBarItemRenderBase)base.Renderer).RenderDropDown(writer);
		}
	}
}
