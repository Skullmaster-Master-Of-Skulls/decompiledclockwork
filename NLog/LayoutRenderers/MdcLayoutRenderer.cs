using System;
using System.Text;
using NLog.Config;
using NLog.Internal;

namespace NLog.LayoutRenderers
{
	// Token: 0x020000DE RID: 222
	[LayoutRenderer("mdc")]
	public class MdcLayoutRenderer : LayoutRenderer
	{
		// Token: 0x17000106 RID: 262
		// (get) Token: 0x06000673 RID: 1651 RVA: 0x0000E997 File Offset: 0x0000CB97
		// (set) Token: 0x06000674 RID: 1652 RVA: 0x0000E99F File Offset: 0x0000CB9F
		[DefaultParameter]
		[RequiredParameter]
		public string Item { get; set; }

		// Token: 0x06000675 RID: 1653 RVA: 0x0000E9A8 File Offset: 0x0000CBA8
		protected override void Append(StringBuilder builder, LogEventInfo logEvent)
		{
			object @object = MappedDiagnosticsContext.GetObject(this.Item);
			builder.Append(@object, logEvent, base.LoggingConfiguration);
		}
	}
}
