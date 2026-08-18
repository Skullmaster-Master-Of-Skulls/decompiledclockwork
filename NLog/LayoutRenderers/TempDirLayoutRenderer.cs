using System;
using System.IO;
using System.Text;
using NLog.Config;

namespace NLog.LayoutRenderers
{
	// Token: 0x020000F2 RID: 242
	[LayoutRenderer("tempdir")]
	[AppDomainFixedOutput]
	public class TempDirLayoutRenderer : LayoutRenderer
	{
		// Token: 0x1700012C RID: 300
		// (get) Token: 0x060006EB RID: 1771 RVA: 0x0000F84A File Offset: 0x0000DA4A
		// (set) Token: 0x060006EC RID: 1772 RVA: 0x0000F852 File Offset: 0x0000DA52
		public string File { get; set; }

		// Token: 0x1700012D RID: 301
		// (get) Token: 0x060006ED RID: 1773 RVA: 0x0000F85B File Offset: 0x0000DA5B
		// (set) Token: 0x060006EE RID: 1774 RVA: 0x0000F863 File Offset: 0x0000DA63
		public string Dir { get; set; }

		// Token: 0x060006EF RID: 1775 RVA: 0x0000F86C File Offset: 0x0000DA6C
		protected override void Append(StringBuilder builder, LogEventInfo logEvent)
		{
			string text = TempDirLayoutRenderer.tempDir;
			if (this.File != null)
			{
				builder.Append(Path.Combine(text, this.File));
				return;
			}
			if (this.Dir != null)
			{
				builder.Append(Path.Combine(text, this.Dir));
				return;
			}
			builder.Append(text);
		}

		// Token: 0x040001FD RID: 509
		private static string tempDir = Path.GetTempPath();
	}
}
