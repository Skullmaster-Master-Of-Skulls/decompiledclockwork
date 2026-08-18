using System;
using System.Globalization;
using System.Text;
using NLog.Config;
using NLog.Internal;

namespace NLog.LayoutRenderers
{
	// Token: 0x020000E5 RID: 229
	[LayoutRenderer("processid")]
	[AppDomainFixedOutput]
	[ThreadAgnostic]
	public class ProcessIdLayoutRenderer : LayoutRenderer
	{
		// Token: 0x060006A0 RID: 1696 RVA: 0x0000ED5C File Offset: 0x0000CF5C
		protected override void Append(StringBuilder builder, LogEventInfo logEvent)
		{
			builder.Append(ThreadIDHelper.Instance.CurrentProcessID.ToString(CultureInfo.InvariantCulture));
		}
	}
}
