using System;
using System.Text;
using NLog.Config;
using NLog.Internal;

namespace NLog.LayoutRenderers
{
	// Token: 0x020000E0 RID: 224
	[LayoutRenderer("message")]
	[ThreadAgnostic]
	public class MessageLayoutRenderer : LayoutRenderer
	{
		// Token: 0x0600067B RID: 1659 RVA: 0x0000EA17 File Offset: 0x0000CC17
		public MessageLayoutRenderer()
		{
			this.ExceptionSeparator = EnvironmentHelper.NewLine;
		}

		// Token: 0x17000108 RID: 264
		// (get) Token: 0x0600067C RID: 1660 RVA: 0x0000EA2A File Offset: 0x0000CC2A
		// (set) Token: 0x0600067D RID: 1661 RVA: 0x0000EA32 File Offset: 0x0000CC32
		public bool WithException { get; set; }

		// Token: 0x17000109 RID: 265
		// (get) Token: 0x0600067E RID: 1662 RVA: 0x0000EA3B File Offset: 0x0000CC3B
		// (set) Token: 0x0600067F RID: 1663 RVA: 0x0000EA43 File Offset: 0x0000CC43
		public string ExceptionSeparator { get; set; }

		// Token: 0x06000680 RID: 1664 RVA: 0x0000EA4C File Offset: 0x0000CC4C
		protected override void Append(StringBuilder builder, LogEventInfo logEvent)
		{
			builder.Append(logEvent.FormattedMessage);
			if (this.WithException && logEvent.Exception != null)
			{
				builder.Append(this.ExceptionSeparator);
				builder.Append(logEvent.Exception.ToString());
			}
		}
	}
}
