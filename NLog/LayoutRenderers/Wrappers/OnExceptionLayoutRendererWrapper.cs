using System;
using NLog.Config;

namespace NLog.LayoutRenderers.Wrappers
{
	// Token: 0x02000100 RID: 256
	[LayoutRenderer("onexception")]
	[ThreadAgnostic]
	public sealed class OnExceptionLayoutRendererWrapper : WrapperLayoutRendererBase
	{
		// Token: 0x0600072E RID: 1838 RVA: 0x0000FFF7 File Offset: 0x0000E1F7
		protected override string Transform(string text)
		{
			return text;
		}

		// Token: 0x0600072F RID: 1839 RVA: 0x0000FFFA File Offset: 0x0000E1FA
		protected override string RenderInner(LogEventInfo logEvent)
		{
			if (logEvent.Exception != null)
			{
				return base.RenderInner(logEvent);
			}
			return string.Empty;
		}
	}
}
