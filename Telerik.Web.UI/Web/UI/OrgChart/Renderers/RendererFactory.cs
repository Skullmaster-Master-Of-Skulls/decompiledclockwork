using System;

namespace Telerik.Web.UI.OrgChart.Renderers
{
	// Token: 0x02000636 RID: 1590
	internal static class RendererFactory
	{
		// Token: 0x060039F9 RID: 14841 RVA: 0x000BE0D4 File Offset: 0x000BC2D4
		public static OrgChartNodeRendererBase CreateOrgChartNodeRenderer(RadOrgChart orgChart)
		{
			OrgChartNodeRendererBase result;
			if (orgChart.ResolvedRenderMode == RenderMode.Lightweight)
			{
				result = new OrgChartNodeRendererLite();
			}
			else
			{
				result = new OrgChartNodeRenderer();
			}
			return result;
		}

		// Token: 0x060039FA RID: 14842 RVA: 0x000BE0FC File Offset: 0x000BC2FC
		public static OrgChartGroupItemRendererBase CreateOrgChartGroupItemRenderer(RadOrgChart orgChart)
		{
			OrgChartGroupItemRendererBase result;
			if (orgChart.ResolvedRenderMode == RenderMode.Lightweight)
			{
				result = new OrgChartGroupItemRendererLite();
			}
			else
			{
				result = new OrgChartGroupItemRenderer();
			}
			return result;
		}

		// Token: 0x060039FB RID: 14843 RVA: 0x000BE124 File Offset: 0x000BC324
		public static OrgChartGroupItemCollectionRendererBase CreateOrgChartGroupItemCollectionRenderer(RadOrgChart orgChart)
		{
			OrgChartGroupItemCollectionRendererBase result;
			if (orgChart.ResolvedRenderMode == RenderMode.Lightweight)
			{
				result = new OrgChartGroupItemCollectionRendererLite();
			}
			else
			{
				result = new OrgChartGroupItemCollectionRendererBase();
			}
			return result;
		}
	}
}
