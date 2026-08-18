using System;
using NLog.LayoutRenderers;

namespace NLog.Layouts
{
	// Token: 0x02000119 RID: 281
	[Layout("Log4JXmlEventLayout")]
	public class Log4JXmlEventLayout : Layout
	{
		// Token: 0x060007C9 RID: 1993 RVA: 0x0001179E File Offset: 0x0000F99E
		public Log4JXmlEventLayout()
		{
			this.Renderer = new Log4JXmlEventLayoutRenderer();
		}

		// Token: 0x17000162 RID: 354
		// (get) Token: 0x060007CA RID: 1994 RVA: 0x000117B1 File Offset: 0x0000F9B1
		// (set) Token: 0x060007CB RID: 1995 RVA: 0x000117B9 File Offset: 0x0000F9B9
		public Log4JXmlEventLayoutRenderer Renderer { get; private set; }

		// Token: 0x060007CC RID: 1996 RVA: 0x000117C4 File Offset: 0x0000F9C4
		protected override string GetFormattedMessage(LogEventInfo logEvent)
		{
			string result;
			if (logEvent.TryGetCachedLayoutValue(this, out result))
			{
				return result;
			}
			return logEvent.AddCachedLayoutValue(this, this.Renderer.Render(logEvent));
		}
	}
}
