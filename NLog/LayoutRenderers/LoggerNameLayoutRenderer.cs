using System;
using System.ComponentModel;
using System.Text;
using NLog.Config;

namespace NLog.LayoutRenderers
{
	// Token: 0x020000DB RID: 219
	[LayoutRenderer("logger")]
	[ThreadAgnostic]
	public class LoggerNameLayoutRenderer : LayoutRenderer
	{
		// Token: 0x17000103 RID: 259
		// (get) Token: 0x06000664 RID: 1636 RVA: 0x0000E750 File Offset: 0x0000C950
		// (set) Token: 0x06000665 RID: 1637 RVA: 0x0000E758 File Offset: 0x0000C958
		[DefaultValue(false)]
		public bool ShortName { get; set; }

		// Token: 0x06000666 RID: 1638 RVA: 0x0000E764 File Offset: 0x0000C964
		protected override void Append(StringBuilder builder, LogEventInfo logEvent)
		{
			if (!this.ShortName)
			{
				builder.Append(logEvent.LoggerName);
				return;
			}
			int num = logEvent.LoggerName.LastIndexOf('.');
			if (num < 0)
			{
				builder.Append(logEvent.LoggerName);
				return;
			}
			builder.Append(logEvent.LoggerName.Substring(num + 1));
		}
	}
}
