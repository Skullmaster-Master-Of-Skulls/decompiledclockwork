using System;
using System.Text;
using System.Threading;

namespace NLog.LayoutRenderers
{
	// Token: 0x020000F4 RID: 244
	[LayoutRenderer("threadname")]
	public class ThreadNameLayoutRenderer : LayoutRenderer
	{
		// Token: 0x060006F4 RID: 1780 RVA: 0x0000F907 File Offset: 0x0000DB07
		protected override void Append(StringBuilder builder, LogEventInfo logEvent)
		{
			builder.Append(Thread.CurrentThread.Name);
		}
	}
}
