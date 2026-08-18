using System;
using NLog.Config;

namespace NLog.Layouts
{
	// Token: 0x02000111 RID: 273
	[Layout("LayoutWithHeaderAndFooter")]
	[ThreadAgnostic]
	public class LayoutWithHeaderAndFooter : Layout
	{
		// Token: 0x17000153 RID: 339
		// (get) Token: 0x06000791 RID: 1937 RVA: 0x000108F6 File Offset: 0x0000EAF6
		// (set) Token: 0x06000792 RID: 1938 RVA: 0x000108FE File Offset: 0x0000EAFE
		public Layout Layout { get; set; }

		// Token: 0x17000154 RID: 340
		// (get) Token: 0x06000793 RID: 1939 RVA: 0x00010907 File Offset: 0x0000EB07
		// (set) Token: 0x06000794 RID: 1940 RVA: 0x0001090F File Offset: 0x0000EB0F
		public Layout Header { get; set; }

		// Token: 0x17000155 RID: 341
		// (get) Token: 0x06000795 RID: 1941 RVA: 0x00010918 File Offset: 0x0000EB18
		// (set) Token: 0x06000796 RID: 1942 RVA: 0x00010920 File Offset: 0x0000EB20
		public Layout Footer { get; set; }

		// Token: 0x06000797 RID: 1943 RVA: 0x00010929 File Offset: 0x0000EB29
		protected override string GetFormattedMessage(LogEventInfo logEvent)
		{
			return this.Layout.Render(logEvent);
		}
	}
}
