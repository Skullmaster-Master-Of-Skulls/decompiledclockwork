using System;
using Telerik.Web.UI.Renderers;

namespace Telerik.Web.UI.RibbonBar.Renderers
{
	// Token: 0x02000795 RID: 1941
	internal class RibbonBarRenderBase : RendererBase
	{
		// Token: 0x0600441D RID: 17437 RVA: 0x000D59E4 File Offset: 0x000D3BE4
		public RibbonBarRenderBase(RadRibbonBar owner)
		{
			this.Owner = owner;
		}

		// Token: 0x1700161E RID: 5662
		// (get) Token: 0x0600441E RID: 17438 RVA: 0x000D59F3 File Offset: 0x000D3BF3
		// (set) Token: 0x0600441F RID: 17439 RVA: 0x000D59FB File Offset: 0x000D3BFB
		protected RadRibbonBar Owner { get; set; }
	}
}
