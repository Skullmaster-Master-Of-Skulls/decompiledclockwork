using System;
using System.IO;
using System.Reflection;
using System.Text;
using NLog.Config;

namespace NLog.LayoutRenderers
{
	// Token: 0x020000E3 RID: 227
	[ThreadAgnostic]
	[LayoutRenderer("nlogdir")]
	[AppDomainFixedOutput]
	public class NLogDirLayoutRenderer : LayoutRenderer
	{
		// Token: 0x0600068B RID: 1675 RVA: 0x0000EB80 File Offset: 0x0000CD80
		static NLogDirLayoutRenderer()
		{
			Assembly assembly = typeof(LogManager).Assembly;
			string path = (!string.IsNullOrEmpty(assembly.Location)) ? assembly.Location : new Uri(assembly.CodeBase).LocalPath;
			NLogDirLayoutRenderer.NLogDir = Path.GetDirectoryName(path);
		}

		// Token: 0x1700010D RID: 269
		// (get) Token: 0x0600068C RID: 1676 RVA: 0x0000EBCE File Offset: 0x0000CDCE
		// (set) Token: 0x0600068D RID: 1677 RVA: 0x0000EBD6 File Offset: 0x0000CDD6
		public string File { get; set; }

		// Token: 0x1700010E RID: 270
		// (get) Token: 0x0600068E RID: 1678 RVA: 0x0000EBDF File Offset: 0x0000CDDF
		// (set) Token: 0x0600068F RID: 1679 RVA: 0x0000EBE7 File Offset: 0x0000CDE7
		public string Dir { get; set; }

		// Token: 0x1700010F RID: 271
		// (get) Token: 0x06000690 RID: 1680 RVA: 0x0000EBF0 File Offset: 0x0000CDF0
		// (set) Token: 0x06000691 RID: 1681 RVA: 0x0000EBF7 File Offset: 0x0000CDF7
		private static string NLogDir { get; set; }

		// Token: 0x06000692 RID: 1682 RVA: 0x0000EC00 File Offset: 0x0000CE00
		protected override void Append(StringBuilder builder, LogEventInfo logEvent)
		{
			string nlogDir = NLogDirLayoutRenderer.NLogDir;
			if (this.File != null)
			{
				builder.Append(Path.Combine(nlogDir, this.File));
				return;
			}
			if (this.Dir != null)
			{
				builder.Append(Path.Combine(nlogDir, this.Dir));
				return;
			}
			builder.Append(nlogDir);
		}
	}
}
