using System;
using System.Globalization;
using System.Text;
using System.Threading;

namespace NLog.LayoutRenderers
{
	// Token: 0x020000F3 RID: 243
	[LayoutRenderer("threadid")]
	public class ThreadIdLayoutRenderer : LayoutRenderer
	{
		// Token: 0x060006F2 RID: 1778 RVA: 0x0000F8D4 File Offset: 0x0000DAD4
		protected override void Append(StringBuilder builder, LogEventInfo logEvent)
		{
			builder.Append(Thread.CurrentThread.ManagedThreadId.ToString(CultureInfo.InvariantCulture));
		}
	}
}
