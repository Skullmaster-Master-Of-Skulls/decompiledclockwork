using System;
using System.Text;
using NLog.Internal;

namespace NLog.LayoutRenderers
{
	// Token: 0x020000E2 RID: 226
	[LayoutRenderer("newline")]
	public class NewLineLayoutRenderer : LayoutRenderer
	{
		// Token: 0x06000689 RID: 1673 RVA: 0x0000EB67 File Offset: 0x0000CD67
		protected override void Append(StringBuilder builder, LogEventInfo logEvent)
		{
			builder.Append(EnvironmentHelper.NewLine);
		}
	}
}
