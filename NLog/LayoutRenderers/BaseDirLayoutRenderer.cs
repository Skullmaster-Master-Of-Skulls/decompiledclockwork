using System;
using System.IO;
using System.Text;
using NLog.Config;
using NLog.Internal.Fakeables;

namespace NLog.LayoutRenderers
{
	// Token: 0x020000C7 RID: 199
	[LayoutRenderer("basedir")]
	[AppDomainFixedOutput]
	public class BaseDirLayoutRenderer : LayoutRenderer
	{
		// Token: 0x060005CB RID: 1483 RVA: 0x0000D066 File Offset: 0x0000B266
		public BaseDirLayoutRenderer() : this(AppDomainWrapper.CurrentDomain)
		{
		}

		// Token: 0x060005CC RID: 1484 RVA: 0x0000D073 File Offset: 0x0000B273
		public BaseDirLayoutRenderer(IAppDomain appDomain)
		{
			this.baseDir = appDomain.BaseDirectory;
		}

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x060005CD RID: 1485 RVA: 0x0000D087 File Offset: 0x0000B287
		// (set) Token: 0x060005CE RID: 1486 RVA: 0x0000D08F File Offset: 0x0000B28F
		public string File { get; set; }

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x060005CF RID: 1487 RVA: 0x0000D098 File Offset: 0x0000B298
		// (set) Token: 0x060005D0 RID: 1488 RVA: 0x0000D0A0 File Offset: 0x0000B2A0
		public string Dir { get; set; }

		// Token: 0x060005D1 RID: 1489 RVA: 0x0000D0AC File Offset: 0x0000B2AC
		protected override void Append(StringBuilder builder, LogEventInfo logEvent)
		{
			string text = this.baseDir;
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

		// Token: 0x0400015F RID: 351
		private string baseDir;
	}
}
