using System;
using System.Text;
using NLog.Config;
using NLog.Internal;

namespace NLog.LayoutRenderers
{
	// Token: 0x020000D3 RID: 211
	[LayoutRenderer("gdc")]
	public class GdcLayoutRenderer : LayoutRenderer
	{
		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x0600062E RID: 1582 RVA: 0x0000DDBE File Offset: 0x0000BFBE
		// (set) Token: 0x0600062F RID: 1583 RVA: 0x0000DDC6 File Offset: 0x0000BFC6
		[DefaultParameter]
		[RequiredParameter]
		public string Item { get; set; }

		// Token: 0x06000630 RID: 1584 RVA: 0x0000DDD0 File Offset: 0x0000BFD0
		protected override void Append(StringBuilder builder, LogEventInfo logEvent)
		{
			object @object = GlobalDiagnosticsContext.GetObject(this.Item);
			builder.Append(@object, logEvent, base.LoggingConfiguration);
		}
	}
}
