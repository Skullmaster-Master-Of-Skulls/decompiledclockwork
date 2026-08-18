using System;

namespace Telerik.Web.UI.ComboBox
{
	// Token: 0x02000A18 RID: 2584
	internal static class RendererFactory
	{
		// Token: 0x060061EE RID: 25070 RVA: 0x00171D58 File Offset: 0x0016FF58
		public static ComboRendererBase CreateRenderer(RadComboBox combo)
		{
			RenderMode resolvedRenderMode = combo.ResolvedRenderMode;
			if (resolvedRenderMode == RenderMode.Classic)
			{
				return new ClassicRenderer(combo);
			}
			if (resolvedRenderMode == RenderMode.Lightweight)
			{
				return new LiteRenderer(combo);
			}
			return new NativeRenderer(combo);
		}
	}
}
