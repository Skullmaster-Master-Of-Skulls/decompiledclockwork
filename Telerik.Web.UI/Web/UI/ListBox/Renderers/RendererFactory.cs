using System;

namespace Telerik.Web.UI.ListBox.Renderers
{
	// Token: 0x0200057D RID: 1405
	internal static class RendererFactory
	{
		// Token: 0x060032DB RID: 13019 RVA: 0x000A88C4 File Offset: 0x000A6AC4
		public static ListBoxRenderBase CreateListBoxRenderer(RadListBox listBox)
		{
			RenderMode resolvedRenderMode = listBox.ResolvedRenderMode;
			if (resolvedRenderMode == RenderMode.Lightweight)
			{
				return new ListBoxLiteRenderer(listBox);
			}
			return new ListBoxClassicRenderer(listBox);
		}

		// Token: 0x060032DC RID: 13020 RVA: 0x000A88EC File Offset: 0x000A6AEC
		public static IRenderer CreateItemRenderer(RadListBoxItem item)
		{
			RenderMode resolvedRenderMode = item.ListBox.ResolvedRenderMode;
			if (resolvedRenderMode == RenderMode.Lightweight)
			{
				return new ListBoxItemLiteRenderer(item);
			}
			return new ListBoxItemClassicRenderer(item);
		}
	}
}
