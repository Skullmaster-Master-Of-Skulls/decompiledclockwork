using System;
using System.IO;
using System.Text;
using NLog.Common;
using NLog.Config;
using NLog.Internal;
using NLog.Layouts;

namespace NLog.LayoutRenderers
{
	// Token: 0x020000D0 RID: 208
	[LayoutRenderer("file-contents")]
	public class FileContentsLayoutRenderer : LayoutRenderer
	{
		// Token: 0x06000623 RID: 1571 RVA: 0x0000DBD6 File Offset: 0x0000BDD6
		public FileContentsLayoutRenderer()
		{
			this.Encoding = Encoding.Default;
			this.lastFileName = string.Empty;
		}

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x06000624 RID: 1572 RVA: 0x0000DBF4 File Offset: 0x0000BDF4
		// (set) Token: 0x06000625 RID: 1573 RVA: 0x0000DBFC File Offset: 0x0000BDFC
		[DefaultParameter]
		public Layout FileName { get; set; }

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x06000626 RID: 1574 RVA: 0x0000DC05 File Offset: 0x0000BE05
		// (set) Token: 0x06000627 RID: 1575 RVA: 0x0000DC0D File Offset: 0x0000BE0D
		public Encoding Encoding { get; set; }

		// Token: 0x06000628 RID: 1576 RVA: 0x0000DC18 File Offset: 0x0000BE18
		protected override void Append(StringBuilder builder, LogEventInfo logEvent)
		{
			lock (this)
			{
				string text = this.FileName.Render(logEvent);
				if (text != this.lastFileName)
				{
					this.currentFileContents = this.ReadFileContents(text);
					this.lastFileName = text;
				}
			}
			builder.Append(this.currentFileContents);
		}

		// Token: 0x06000629 RID: 1577 RVA: 0x0000DC8C File Offset: 0x0000BE8C
		private string ReadFileContents(string fileName)
		{
			string result;
			try
			{
				using (StreamReader streamReader = new StreamReader(fileName, this.Encoding))
				{
					result = streamReader.ReadToEnd();
				}
			}
			catch (Exception ex)
			{
				InternalLogger.Error(ex, "Cannot read file contents of '{0}'.", new object[]
				{
					fileName
				});
				if (ex.MustBeRethrown())
				{
					throw;
				}
				result = string.Empty;
			}
			return result;
		}

		// Token: 0x0400017F RID: 383
		private string lastFileName;

		// Token: 0x04000180 RID: 384
		private string currentFileContents;
	}
}
