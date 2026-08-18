using System;
using System.Text;
using System.Text.RegularExpressions;

namespace Telerik.Web.UI.Upload
{
	// Token: 0x02001B78 RID: 7032
	internal class FileHeaderInfo : FieldHeaderInfo
	{
		// Token: 0x06011090 RID: 69776 RVA: 0x003C29AE File Offset: 0x003C0BAE
		public FileHeaderInfo(byte[] headerContent, Encoding encoding) : base(headerContent, encoding)
		{
		}

		// Token: 0x06011091 RID: 69777 RVA: 0x003C29B8 File Offset: 0x003C0BB8
		public static bool IsFileHeaderInfo(byte[] headerContent, Encoding encoding)
		{
			string input = encoding.GetString(headerContent).ToLower();
			return FileHeaderInfo._fileNameExtractor.IsMatch(input);
		}

		// Token: 0x17005328 RID: 21288
		// (get) Token: 0x06011092 RID: 69778 RVA: 0x003C29DD File Offset: 0x003C0BDD
		public virtual string FileName
		{
			get
			{
				if (this._fileName == null)
				{
					this._fileName = FileHeaderInfo._fileNameExtractor.Match(base.ContentAsString).Groups[2].Value;
				}
				return this._fileName;
			}
		}

		// Token: 0x17005329 RID: 21289
		// (get) Token: 0x06011093 RID: 69779 RVA: 0x003C2A14 File Offset: 0x003C0C14
		public virtual string ContentType
		{
			get
			{
				if (this._contentType == null)
				{
					Regex regex = new Regex("\\bContent-Type: ?(\"?)([^;\\r\\n]*)\\1", RegexOptions.IgnoreCase | RegexOptions.Compiled);
					this._contentType = regex.Match(base.ContentAsString).Groups[2].Value;
				}
				return this._contentType;
			}
		}

		// Token: 0x04004C35 RID: 19509
		private static Regex _fileNameExtractor = new Regex("\\bfilename=(\"?)([^;\\r\\n]*)\\1", RegexOptions.IgnoreCase | RegexOptions.Compiled);

		// Token: 0x04004C36 RID: 19510
		private string _fileName;

		// Token: 0x04004C37 RID: 19511
		private string _contentType;
	}
}
