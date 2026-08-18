using System;
using NLog.Config;
using NLog.Layouts;

namespace NLog.LayoutRenderers.Wrappers
{
	// Token: 0x0200010A RID: 266
	[ThreadAgnostic]
	[LayoutRenderer("whenEmpty")]
	[AmbientProperty("WhenEmpty")]
	public sealed class WhenEmptyLayoutRendererWrapper : WrapperLayoutRendererBase
	{
		// Token: 0x1700014A RID: 330
		// (get) Token: 0x06000766 RID: 1894 RVA: 0x000104FE File Offset: 0x0000E6FE
		// (set) Token: 0x06000767 RID: 1895 RVA: 0x00010506 File Offset: 0x0000E706
		[RequiredParameter]
		public Layout WhenEmpty { get; set; }

		// Token: 0x06000768 RID: 1896 RVA: 0x0001050F File Offset: 0x0000E70F
		protected override string Transform(string text)
		{
			return text;
		}

		// Token: 0x06000769 RID: 1897 RVA: 0x00010514 File Offset: 0x0000E714
		protected override string RenderInner(LogEventInfo logEvent)
		{
			string text = base.RenderInner(logEvent);
			if (!string.IsNullOrEmpty(text))
			{
				return text;
			}
			return this.WhenEmpty.Render(logEvent);
		}
	}
}
