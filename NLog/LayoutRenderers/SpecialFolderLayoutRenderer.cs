using System;
using System.IO;
using System.Text;
using NLog.Config;

namespace NLog.LayoutRenderers
{
	// Token: 0x020000EF RID: 239
	[LayoutRenderer("specialfolder")]
	[AppDomainFixedOutput]
	public class SpecialFolderLayoutRenderer : LayoutRenderer
	{
		// Token: 0x17000124 RID: 292
		// (get) Token: 0x060006D8 RID: 1752 RVA: 0x0000F5F0 File Offset: 0x0000D7F0
		// (set) Token: 0x060006D9 RID: 1753 RVA: 0x0000F5F8 File Offset: 0x0000D7F8
		[DefaultParameter]
		public Environment.SpecialFolder Folder { get; set; }

		// Token: 0x17000125 RID: 293
		// (get) Token: 0x060006DA RID: 1754 RVA: 0x0000F601 File Offset: 0x0000D801
		// (set) Token: 0x060006DB RID: 1755 RVA: 0x0000F609 File Offset: 0x0000D809
		public string File { get; set; }

		// Token: 0x17000126 RID: 294
		// (get) Token: 0x060006DC RID: 1756 RVA: 0x0000F612 File Offset: 0x0000D812
		// (set) Token: 0x060006DD RID: 1757 RVA: 0x0000F61A File Offset: 0x0000D81A
		public string Dir { get; set; }

		// Token: 0x060006DE RID: 1758 RVA: 0x0000F624 File Offset: 0x0000D824
		protected override void Append(StringBuilder builder, LogEventInfo logEvent)
		{
			string text = Environment.GetFolderPath(this.Folder);
			if (this.Dir != null)
			{
				text = Path.Combine(text, this.Dir);
			}
			if (this.File != null)
			{
				text = Path.Combine(text, this.File);
			}
			builder.Append(text);
		}
	}
}
