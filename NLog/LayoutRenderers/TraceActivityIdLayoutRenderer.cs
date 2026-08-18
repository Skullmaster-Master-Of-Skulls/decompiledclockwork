using System;
using System.Diagnostics;
using System.Globalization;
using System.Text;

namespace NLog.LayoutRenderers
{
	// Token: 0x020000F7 RID: 247
	[LayoutRenderer("activityid")]
	public class TraceActivityIdLayoutRenderer : LayoutRenderer
	{
		// Token: 0x060006FE RID: 1790 RVA: 0x0000FA9C File Offset: 0x0000DC9C
		protected override void Append(StringBuilder builder, LogEventInfo logEvent)
		{
			builder.Append(Guid.Empty.Equals(Trace.CorrelationManager.ActivityId) ? string.Empty : Trace.CorrelationManager.ActivityId.ToString("D", CultureInfo.InvariantCulture));
		}
	}
}
