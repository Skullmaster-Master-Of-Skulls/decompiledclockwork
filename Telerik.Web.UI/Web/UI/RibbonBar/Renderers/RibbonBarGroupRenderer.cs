using System;
using System.Web.UI;
using Telerik.Web.UI.Renderers;

namespace Telerik.Web.UI.RibbonBar.Renderers
{
	// Token: 0x0200078C RID: 1932
	internal class RibbonBarGroupRenderer : RendererBase
	{
		// Token: 0x060043F2 RID: 17394 RVA: 0x000D4DAC File Offset: 0x000D2FAC
		public RibbonBarGroupRenderer(RibbonBarGroup owner)
		{
			this.Owner = owner;
		}

		// Token: 0x1700161A RID: 5658
		// (get) Token: 0x060043F3 RID: 17395 RVA: 0x000D4DBB File Offset: 0x000D2FBB
		// (set) Token: 0x060043F4 RID: 17396 RVA: 0x000D4DC3 File Offset: 0x000D2FC3
		protected RibbonBarGroup Owner { get; set; }

		// Token: 0x060043F5 RID: 17397 RVA: 0x000D4DCC File Offset: 0x000D2FCC
		public virtual void RenderBeginTag(HtmlTextWriter writer)
		{
			throw new NotImplementedException();
		}
	}
}
