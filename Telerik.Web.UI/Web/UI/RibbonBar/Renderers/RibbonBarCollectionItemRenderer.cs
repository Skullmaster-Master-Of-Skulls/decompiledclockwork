using System;
using System.Web.UI;
using Telerik.Web.UI.Renderers;

namespace Telerik.Web.UI.RibbonBar.Renderers
{
	// Token: 0x0200078E RID: 1934
	internal class RibbonBarCollectionItemRenderer : RendererBase
	{
		// Token: 0x060043FB RID: 17403 RVA: 0x000D4FDB File Offset: 0x000D31DB
		public RibbonBarCollectionItemRenderer(RibbonBarCollectionItemBase owner)
		{
			this.Owner = owner;
		}

		// Token: 0x1700161B RID: 5659
		// (get) Token: 0x060043FC RID: 17404 RVA: 0x000D4FEA File Offset: 0x000D31EA
		// (set) Token: 0x060043FD RID: 17405 RVA: 0x000D4FF2 File Offset: 0x000D31F2
		protected RibbonBarCollectionItemBase Owner { get; set; }

		// Token: 0x060043FE RID: 17406 RVA: 0x000D4FFB File Offset: 0x000D31FB
		public virtual void RenderControl(HtmlTextWriter writer)
		{
			throw new NotImplementedException();
		}
	}
}
