using System;
using System.Globalization;
using System.IO;
using System.Text;
using System.Web.Hosting;
using System.Web.UI;
using System.Web.Util;

namespace System.Web
{
	// Token: 0x0200005E RID: 94
	internal abstract class FormatterWithFileInfo : ErrorFormatter
	{
		// Token: 0x06000646 RID: 1606 RVA: 0x000099F8 File Offset: 0x00007BF8
		internal static string GetSourceFileLines(string fileName, Encoding encoding, string sourceCode, int lineNumber)
		{
			if (fileName != null && !HttpRuntime.HasFilePermission(fileName))
			{
				return SR.GetString("WithFile_No_Relevant_Line");
			}
			StringBuilder stringBuilder = new StringBuilder();
			if (lineNumber <= 0)
			{
				return SR.GetString("WithFile_No_Relevant_Line");
			}
			TextReader textReader = null;
			string virtualPathFromHttpLinePragma = ErrorFormatter.GetVirtualPathFromHttpLinePragma(fileName);
			if (virtualPathFromHttpLinePragma != null)
			{
				Stream stream = VirtualPathProvider.OpenFile(virtualPathFromHttpLinePragma);
				if (stream != null)
				{
					textReader = Util.ReaderFromStream(stream, System.Web.VirtualPath.Create(virtualPathFromHttpLinePragma));
				}
			}
			try
			{
				if (textReader == null && fileName != null)
				{
					textReader = new StreamReader(fileName, encoding, true, 4096);
				}
			}
			catch
			{
			}
			if (textReader == null)
			{
				if (sourceCode == null)
				{
					return SR.GetString("WithFile_No_Relevant_Line");
				}
				textReader = new StringReader(sourceCode);
			}
			try
			{
				bool flag = false;
				if (ErrorFormatter.IsTextRightToLeft)
				{
					stringBuilder.Append("<div dir=\"ltr\">");
				}
				int num = 1;
				for (;;)
				{
					string text = textReader.ReadLine();
					if (text == null)
					{
						break;
					}
					if (num == lineNumber)
					{
						stringBuilder.Append("<font color=red>");
					}
					if (num >= lineNumber - 2 && num <= lineNumber + 2)
					{
						flag = true;
						string text2 = num.ToString("G", CultureInfo.CurrentCulture);
						stringBuilder.Append(SR.GetString("WithFile_Line_Num", new object[]
						{
							text2
						}));
						if (text2.Length < 3)
						{
							stringBuilder.Append(' ', 3 - text2.Length);
						}
						stringBuilder.Append(HttpUtility.HtmlEncode(text));
						if (num != lineNumber + 2)
						{
							stringBuilder.Append("\r\n");
						}
					}
					if (num == lineNumber)
					{
						stringBuilder.Append("</font>");
					}
					if (num > lineNumber + 2)
					{
						break;
					}
					num++;
				}
				if (ErrorFormatter.IsTextRightToLeft)
				{
					stringBuilder.Append("</div>");
				}
				if (!flag)
				{
					return SR.GetString("WithFile_No_Relevant_Line");
				}
			}
			finally
			{
				textReader.Close();
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000647 RID: 1607 RVA: 0x00009BCC File Offset: 0x00007DCC
		private string GetSourceFileLines()
		{
			return FormatterWithFileInfo.GetSourceFileLines(this._physicalPath, this.SourceFileEncoding, this._sourceCode, this._line);
		}

		// Token: 0x06000648 RID: 1608 RVA: 0x00009BEC File Offset: 0x00007DEC
		internal FormatterWithFileInfo(string virtualPath, string physicalPath, string sourceCode, int line)
		{
			this._virtualPath = virtualPath;
			this._physicalPath = physicalPath;
			if (sourceCode == null && this._physicalPath == null && this._virtualPath != null)
			{
				if (UrlPath.IsValidVirtualPathWithoutProtocol(this._virtualPath))
				{
					this._physicalPath = HostingEnvironment.MapPath(this._virtualPath);
				}
				else
				{
					this._physicalPath = this._virtualPath;
				}
			}
			this._sourceCode = sourceCode;
			this._line = line;
		}

		// Token: 0x170002D3 RID: 723
		// (get) Token: 0x06000649 RID: 1609 RVA: 0x00009C5B File Offset: 0x00007E5B
		protected virtual Encoding SourceFileEncoding
		{
			get
			{
				return Encoding.Default;
			}
		}

		// Token: 0x170002D4 RID: 724
		// (get) Token: 0x0600064A RID: 1610 RVA: 0x00009C62 File Offset: 0x00007E62
		protected override string ColoredSquareContent
		{
			get
			{
				return this.GetSourceFileLines();
			}
		}

		// Token: 0x170002D5 RID: 725
		// (get) Token: 0x0600064B RID: 1611 RVA: 0x000097B7 File Offset: 0x000079B7
		protected override bool ShowSourceFileInfo
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170002D6 RID: 726
		// (get) Token: 0x0600064C RID: 1612 RVA: 0x00009C6A File Offset: 0x00007E6A
		protected override string PhysicalPath
		{
			get
			{
				return this._physicalPath;
			}
		}

		// Token: 0x170002D7 RID: 727
		// (get) Token: 0x0600064D RID: 1613 RVA: 0x00009C72 File Offset: 0x00007E72
		protected override string VirtualPath
		{
			get
			{
				return this._virtualPath;
			}
		}

		// Token: 0x170002D8 RID: 728
		// (get) Token: 0x0600064E RID: 1614 RVA: 0x00009C7A File Offset: 0x00007E7A
		protected override int SourceFileLineNumber
		{
			get
			{
				return this._line;
			}
		}

		// Token: 0x0400017E RID: 382
		protected string _virtualPath;

		// Token: 0x0400017F RID: 383
		protected string _physicalPath;

		// Token: 0x04000180 RID: 384
		protected string _sourceCode;

		// Token: 0x04000181 RID: 385
		protected int _line;

		// Token: 0x04000182 RID: 386
		private const int errorRange = 2;
	}
}
