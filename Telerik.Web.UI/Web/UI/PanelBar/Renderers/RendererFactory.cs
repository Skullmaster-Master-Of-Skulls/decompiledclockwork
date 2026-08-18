using System;

namespace Telerik.Web.UI.PanelBar.Renderers
{
	// Token: 0x02000651 RID: 1617
	internal static class RendererFactory
	{
		// Token: 0x06003B68 RID: 15208 RVA: 0x000C1978 File Offset: 0x000BFB78
		public static IRenderer CreateItemRenderer(RadPanelItem item)
		{
			RenderMode resolvedRenderMode = item.PanelBar.ResolvedRenderMode;
			if (resolvedRenderMode == RenderMode.Lightweight)
			{
				return new PanelItemLiteRenderer(item);
			}
			return new PanelItemClassicRenderer(item);
		}
	}
}
