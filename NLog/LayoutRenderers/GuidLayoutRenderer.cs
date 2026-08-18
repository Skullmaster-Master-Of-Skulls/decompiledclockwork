using System;
using System.ComponentModel;
using System.Text;

namespace NLog.LayoutRenderers
{
	// Token: 0x020000D4 RID: 212
	[LayoutRenderer("guid")]
	public class GuidLayoutRenderer : LayoutRenderer
	{
		// Token: 0x06000632 RID: 1586 RVA: 0x0000DDFF File Offset: 0x0000BFFF
		public GuidLayoutRenderer()
		{
			this.Format = "N";
		}

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x06000633 RID: 1587 RVA: 0x0000DE12 File Offset: 0x0000C012
		// (set) Token: 0x06000634 RID: 1588 RVA: 0x0000DE1A File Offset: 0x0000C01A
		[DefaultValue("N")]
		public string Format { get; set; }

		// Token: 0x06000635 RID: 1589 RVA: 0x0000DE24 File Offset: 0x0000C024
		protected override void Append(StringBuilder builder, LogEventInfo logEvent)
		{
			builder.Append(Guid.NewGuid().ToString(this.Format));
		}
	}
}
