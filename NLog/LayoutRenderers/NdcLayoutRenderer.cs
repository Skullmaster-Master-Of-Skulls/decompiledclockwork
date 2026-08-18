using System;
using System.Text;

namespace NLog.LayoutRenderers
{
	// Token: 0x020000E1 RID: 225
	[LayoutRenderer("ndc")]
	public class NdcLayoutRenderer : LayoutRenderer
	{
		// Token: 0x06000681 RID: 1665 RVA: 0x0000EA8A File Offset: 0x0000CC8A
		public NdcLayoutRenderer()
		{
			this.Separator = " ";
			this.BottomFrames = -1;
			this.TopFrames = -1;
		}

		// Token: 0x1700010A RID: 266
		// (get) Token: 0x06000682 RID: 1666 RVA: 0x0000EAAB File Offset: 0x0000CCAB
		// (set) Token: 0x06000683 RID: 1667 RVA: 0x0000EAB3 File Offset: 0x0000CCB3
		public int TopFrames { get; set; }

		// Token: 0x1700010B RID: 267
		// (get) Token: 0x06000684 RID: 1668 RVA: 0x0000EABC File Offset: 0x0000CCBC
		// (set) Token: 0x06000685 RID: 1669 RVA: 0x0000EAC4 File Offset: 0x0000CCC4
		public int BottomFrames { get; set; }

		// Token: 0x1700010C RID: 268
		// (get) Token: 0x06000686 RID: 1670 RVA: 0x0000EACD File Offset: 0x0000CCCD
		// (set) Token: 0x06000687 RID: 1671 RVA: 0x0000EAD5 File Offset: 0x0000CCD5
		public string Separator { get; set; }

		// Token: 0x06000688 RID: 1672 RVA: 0x0000EAE0 File Offset: 0x0000CCE0
		protected override void Append(StringBuilder builder, LogEventInfo logEvent)
		{
			string[] allMessages = NestedDiagnosticsContext.GetAllMessages(logEvent.FormatProvider);
			int num = 0;
			int num2 = allMessages.Length;
			if (this.TopFrames != -1)
			{
				num2 = Math.Min(this.TopFrames, allMessages.Length);
			}
			else if (this.BottomFrames != -1)
			{
				num = allMessages.Length - Math.Min(this.BottomFrames, allMessages.Length);
			}
			string value = string.Empty;
			for (int i = num2 - 1; i >= num; i--)
			{
				builder.Append(value);
				builder.Append(allMessages[i]);
				value = this.Separator;
			}
		}
	}
}
