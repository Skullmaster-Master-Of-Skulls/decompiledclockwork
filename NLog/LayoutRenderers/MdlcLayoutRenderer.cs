using System;
using System.Text;
using NLog.Config;
using NLog.Internal;

namespace NLog.LayoutRenderers
{
	// Token: 0x020000DF RID: 223
	[LayoutRenderer("mdlc")]
	public class MdlcLayoutRenderer : LayoutRenderer
	{
		// Token: 0x17000107 RID: 263
		// (get) Token: 0x06000677 RID: 1655 RVA: 0x0000E9D7 File Offset: 0x0000CBD7
		// (set) Token: 0x06000678 RID: 1656 RVA: 0x0000E9DF File Offset: 0x0000CBDF
		[DefaultParameter]
		[RequiredParameter]
		public string Item { get; set; }

		// Token: 0x06000679 RID: 1657 RVA: 0x0000E9E8 File Offset: 0x0000CBE8
		protected override void Append(StringBuilder builder, LogEventInfo logEvent)
		{
			object @object = MappedDiagnosticsLogicalContext.GetObject(this.Item);
			builder.Append(@object, logEvent, base.LoggingConfiguration);
		}
	}
}
