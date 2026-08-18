using System;

namespace Telerik.Web.UI.Notification.Renderers
{
	// Token: 0x0200062D RID: 1581
	public static class RendererFactory
	{
		// Token: 0x06003986 RID: 14726 RVA: 0x000BD048 File Offset: 0x000BB248
		public static BaseRenderer GetRenderer(RadNotification notification)
		{
			RenderMode resolvedRenderMode = notification.ResolvedRenderMode;
			if (resolvedRenderMode == RenderMode.Lightweight)
			{
				return new LiteRenderer(notification);
			}
			return new ClassicRenderer(notification);
		}
	}
}
