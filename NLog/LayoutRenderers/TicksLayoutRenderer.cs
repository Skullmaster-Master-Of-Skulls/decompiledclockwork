using System;
using System.Globalization;
using System.Text;
using NLog.Config;

namespace NLog.LayoutRenderers
{
	// Token: 0x020000F5 RID: 245
	[ThreadAgnostic]
	[LayoutRenderer("ticks")]
	public class TicksLayoutRenderer : LayoutRenderer
	{
		// Token: 0x060006F6 RID: 1782 RVA: 0x0000F924 File Offset: 0x0000DB24
		protected override void Append(StringBuilder builder, LogEventInfo logEvent)
		{
			builder.Append(logEvent.TimeStamp.Ticks.ToString(CultureInfo.InvariantCulture));
		}
	}
}
