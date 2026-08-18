using System;

namespace Telerik.Web.UI.TreeView.Renderers
{
	// Token: 0x02000980 RID: 2432
	internal static class RendererFactory
	{
		// Token: 0x06005C64 RID: 23652 RVA: 0x0011A0C0 File Offset: 0x001182C0
		public static TreeViewRenderBase CreateTreeViewRenderer(RadTreeView treeView)
		{
			RenderMode resolvedRenderMode = treeView.ResolvedRenderMode;
			if (resolvedRenderMode == RenderMode.Lightweight)
			{
				return new TreeViewLiteRenderer(treeView);
			}
			return new TreeViewClassicRenderer(treeView);
		}

		// Token: 0x06005C65 RID: 23653 RVA: 0x0011A0E8 File Offset: 0x001182E8
		public static IRenderer CreateNodeRenderer(RadTreeNode node)
		{
			RenderMode resolvedRenderMode = node.TreeView.ResolvedRenderMode;
			if (resolvedRenderMode == RenderMode.Lightweight)
			{
				return new TreeNodeLiteRenderer(node);
			}
			return new TreeNodeClassicRenderer(node);
		}
	}
}
