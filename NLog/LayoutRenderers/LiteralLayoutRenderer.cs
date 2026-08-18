using System;
using System.Text;
using NLog.Config;

namespace NLog.LayoutRenderers
{
	// Token: 0x020000D9 RID: 217
	[ThreadAgnostic]
	[AppDomainFixedOutput]
	[LayoutRenderer("literal")]
	public class LiteralLayoutRenderer : LayoutRenderer
	{
		// Token: 0x06000647 RID: 1607 RVA: 0x0000DFD6 File Offset: 0x0000C1D6
		public LiteralLayoutRenderer()
		{
		}

		// Token: 0x06000648 RID: 1608 RVA: 0x0000DFDE File Offset: 0x0000C1DE
		public LiteralLayoutRenderer(string text)
		{
			this.Text = text;
		}

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x06000649 RID: 1609 RVA: 0x0000DFED File Offset: 0x0000C1ED
		// (set) Token: 0x0600064A RID: 1610 RVA: 0x0000DFF5 File Offset: 0x0000C1F5
		public string Text { get; set; }

		// Token: 0x0600064B RID: 1611 RVA: 0x0000DFFE File Offset: 0x0000C1FE
		protected override void Append(StringBuilder builder, LogEventInfo logEvent)
		{
			builder.Append(this.Text);
		}
	}
}
