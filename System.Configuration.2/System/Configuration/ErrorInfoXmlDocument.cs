using System;
using System.Configuration.Internal;
using System.Xml;

namespace System.Configuration
{
	// Token: 0x02000058 RID: 88
	internal sealed class ErrorInfoXmlDocument : XmlDocument, IConfigErrorInfo
	{
		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x06000375 RID: 885 RVA: 0x000133AE File Offset: 0x000115AE
		int IConfigErrorInfo.LineNumber
		{
			get
			{
				if (this._reader == null)
				{
					return 0;
				}
				if (this._lineOffset > 0)
				{
					return this._reader.LineNumber + this._lineOffset - 1;
				}
				return this._reader.LineNumber;
			}
		}

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x06000376 RID: 886 RVA: 0x000133E3 File Offset: 0x000115E3
		internal int LineNumber
		{
			get
			{
				return ((IConfigErrorInfo)this).LineNumber;
			}
		}

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x06000377 RID: 887 RVA: 0x000133EB File Offset: 0x000115EB
		string IConfigErrorInfo.Filename
		{
			get
			{
				return this._filename;
			}
		}

		// Token: 0x06000378 RID: 888 RVA: 0x000133F4 File Offset: 0x000115F4
		public override void Load(string filename)
		{
			this._filename = filename;
			try
			{
				this._reader = new XmlTextReader(filename);
				this._reader.XmlResolver = null;
				base.Load(this._reader);
			}
			finally
			{
				if (this._reader != null)
				{
					this._reader.Close();
					this._reader = null;
				}
			}
		}

		// Token: 0x06000379 RID: 889 RVA: 0x0001345C File Offset: 0x0001165C
		private void LoadFromConfigXmlReader(ConfigXmlReader reader)
		{
			this._filename = ((IConfigErrorInfo)reader).Filename;
			this._lineOffset = ((IConfigErrorInfo)reader).LineNumber + 1;
			try
			{
				this._reader = reader;
				base.Load(this._reader);
			}
			finally
			{
				if (this._reader != null)
				{
					this._reader.Close();
					this._reader = null;
				}
			}
		}

		// Token: 0x0600037A RID: 890 RVA: 0x000134C8 File Offset: 0x000116C8
		internal static XmlNode CreateSectionXmlNode(ConfigXmlReader reader)
		{
			ErrorInfoXmlDocument errorInfoXmlDocument = new ErrorInfoXmlDocument();
			errorInfoXmlDocument.LoadFromConfigXmlReader(reader);
			return errorInfoXmlDocument.DocumentElement;
		}

		// Token: 0x0600037B RID: 891 RVA: 0x000134EA File Offset: 0x000116EA
		public override XmlAttribute CreateAttribute(string prefix, string localName, string namespaceUri)
		{
			return new ConfigXmlAttribute(this._filename, this.LineNumber, prefix, localName, namespaceUri, this);
		}

		// Token: 0x0600037C RID: 892 RVA: 0x00013501 File Offset: 0x00011701
		public override XmlElement CreateElement(string prefix, string localName, string namespaceUri)
		{
			return new ConfigXmlElement(this._filename, this.LineNumber, prefix, localName, namespaceUri, this);
		}

		// Token: 0x0600037D RID: 893 RVA: 0x00013518 File Offset: 0x00011718
		public override XmlText CreateTextNode(string text)
		{
			return new ConfigXmlText(this._filename, this.LineNumber, text, this);
		}

		// Token: 0x0600037E RID: 894 RVA: 0x0001352D File Offset: 0x0001172D
		public override XmlCDataSection CreateCDataSection(string data)
		{
			return new ConfigXmlCDataSection(this._filename, this.LineNumber, data, this);
		}

		// Token: 0x0600037F RID: 895 RVA: 0x00013542 File Offset: 0x00011742
		public override XmlComment CreateComment(string data)
		{
			return new ConfigXmlComment(this._filename, this.LineNumber, data, this);
		}

		// Token: 0x06000380 RID: 896 RVA: 0x00013557 File Offset: 0x00011757
		public override XmlSignificantWhitespace CreateSignificantWhitespace(string data)
		{
			return new ConfigXmlSignificantWhitespace(this._filename, this.LineNumber, data, this);
		}

		// Token: 0x06000381 RID: 897 RVA: 0x0001356C File Offset: 0x0001176C
		public override XmlWhitespace CreateWhitespace(string data)
		{
			return new ConfigXmlWhitespace(this._filename, this.LineNumber, data, this);
		}

		// Token: 0x0400025D RID: 605
		private XmlTextReader _reader;

		// Token: 0x0400025E RID: 606
		private int _lineOffset;

		// Token: 0x0400025F RID: 607
		private string _filename;
	}
}
