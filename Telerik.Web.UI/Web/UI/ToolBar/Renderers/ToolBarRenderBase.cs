using System;
using Telerik.Web.UI.Renderers;

namespace Telerik.Web.UI.ToolBar.Renderers
{
	// Token: 0x02000953 RID: 2387
	internal class ToolBarRenderBase : RendererBase
	{
		// Token: 0x06005B14 RID: 23316 RVA: 0x00115280 File Offset: 0x00113480
		public ToolBarRenderBase(RadToolBar owner)
		{
			this.Owner = owner;
		}

		// Token: 0x17001E11 RID: 7697
		// (get) Token: 0x06005B15 RID: 23317 RVA: 0x0011528F File Offset: 0x0011348F
		// (set) Token: 0x06005B16 RID: 23318 RVA: 0x00115297 File Offset: 0x00113497
		protected RadToolBar Owner { get; set; }
	}
}
