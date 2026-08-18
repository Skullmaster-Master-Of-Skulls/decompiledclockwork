using System;
using System.Text;
using NLog.Config;

namespace NLog.LayoutRenderers
{
	// Token: 0x020000D8 RID: 216
	[ThreadAgnostic]
	[LayoutRenderer("level")]
	public class LevelLayoutRenderer : LayoutRenderer
	{
		// Token: 0x06000645 RID: 1605 RVA: 0x0000DFBA File Offset: 0x0000C1BA
		protected override void Append(StringBuilder builder, LogEventInfo logEvent)
		{
			builder.Append(logEvent.Level.ToString());
		}
	}
}
