using System;

namespace Telerik.Web.UI.Menu.Renderers
{
	// Token: 0x020005E1 RID: 1505
	internal static class RendererFactory
	{
		// Token: 0x060036AB RID: 13995 RVA: 0x000B5448 File Offset: 0x000B3648
		public static IRenderer CreateMenuRenderer(RadMenu menu)
		{
			IRenderer result;
			if (menu.ResolvedRenderMode == RenderMode.Mobile)
			{
				result = new MenuMobileRenderer(menu);
			}
			else
			{
				result = new MenuRenderer(menu);
			}
			return result;
		}

		// Token: 0x060036AC RID: 13996 RVA: 0x000B546F File Offset: 0x000B366F
		public static IRenderer CreateContextMenuRenderer(RadContextMenu menu)
		{
			return new ContextMenuRenderer(menu);
		}

		// Token: 0x060036AD RID: 13997 RVA: 0x000B5478 File Offset: 0x000B3678
		public static IRenderer CreateItemRenderer(RadMenuItem item)
		{
			switch (item.Menu.ResolvedRenderMode)
			{
			case RenderMode.Lightweight:
				return new MenuItemLiteRenderer(item);
			case RenderMode.Mobile:
				return new MenuItemMobileRenderer(item);
			}
			return new MenuItemClassicRenderer(item);
		}
	}
}
