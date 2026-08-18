using System;

namespace Telerik.Web.UI.TabStrip.Rendering
{
	// Token: 0x020008E9 RID: 2281
	internal static class RendererFactory
	{
		// Token: 0x06005642 RID: 22082 RVA: 0x00108048 File Offset: 0x00106248
		public static IRenderer CreateTabStripRenderer(RadTabStrip tabStrip)
		{
			if (tabStrip.ResolvedRenderMode == RenderMode.Lightweight)
			{
				return new TabStripLiteRenderer(tabStrip);
			}
			return new TabStripClassicRenderer(tabStrip);
		}

		// Token: 0x06005643 RID: 22083 RVA: 0x00108060 File Offset: 0x00106260
		public static IRenderer CreateTabRenderer(RadTab tab)
		{
			if (tab.IsSeparator)
			{
				return new SeparatorRenderer(tab);
			}
			if (tab.TabStrip.ResolvedRenderMode == RenderMode.Lightweight)
			{
				return new TabLiteRenderer(tab);
			}
			return new TabClassicRenderer(tab);
		}
	}
}
