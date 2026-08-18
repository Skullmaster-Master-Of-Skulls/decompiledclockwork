using System;
using System.Configuration.Internal;
using System.IO;
using System.Security.Permissions;
using System.Xml;

namespace System.Configuration
{
	// Token: 0x020006F2 RID: 1778
	[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
	public sealed class ConfigXmlDocument : XmlDocument, IConfigErrorInfo
	{
		// Token: 0x17000CBC RID: 3260
		// (get) Token: 0x060036F1 RID: 14065 RVA: 0x000EA00C File Offset: 0x000E900C
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

		// Token: 0x17000CBD RID: 3261
		// (get) Token: 0x060036F2 RID: 14066 RVA: 0x000EA041 File Offset: 0x000E9041
		public int LineNumber
		{
			get
			{
				return ((IConfigErrorInfo)this).LineNumber;
			}
		}

		// Token: 0x17000CBE RID: 3262
		// (get) Token: 0x060036F3 RID: 14067 RVA: 0x000EA049 File Offset: 0x000E9049
		public string Filename
		{
			get
			{
				return ConfigurationException.SafeFilename(this._filename);
			}
		}

		// Token: 0x17000CBF RID: 3263
		// (get) Token: 0x060036F4 RID: 14068 RVA: 0x000EA056 File Offset: 0x000E9056
		string IConfigErrorInfo.Filename
		{
			get
			{
				return this._filename;
			}
		}

		// Token: 0x060036F5 RID: 14069 RVA: 0x000EA060 File Offset: 0x000E9060
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

		// Token: 0x060036F6 RID: 14070 RVA: 0x000EA0C8 File Offset: 0x000E90C8
		public void LoadSingleElement(string filename, XmlTextReader sourceReader)
		{
			this._filename = filename;
			this._lineOffset = sourceReader.LineNumber;
			string s = sourceReader.ReadOuterXml();
			try
			{
				this._reader = new XmlTextReader(new StringReader(s), sourceReader.NameTable);
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

		// Token: 0x060036F7 RID: 14071 RVA: 0x000EA140 File Offset: 0x000E9140
		public override XmlAttribute CreateAttribute(string prefix, string localName, string namespaceUri)
		{
			return new ConfigXmlAttribute(this._filename, this.LineNumber, prefix, localName, namespaceUri, this);
		}

		// Token: 0x060036F8 RID: 14072 RVA: 0x000EA157 File Offset: 0x000E9157
		public override XmlElement CreateElement(string prefix, string localName, string namespaceUri)
		{
			return new ConfigXmlElement(this._filename, this.LineNumber, prefix, localName, namespaceUri, this);
		}

		// Token: 0x060036F9 RID: 14073 RVA: 0x000EA16E File Offset: 0x000E916E
		public override XmlText CreateTextNode(string text)
		{
			return new ConfigXmlText(this._filename, this.LineNumber, text, this);
		}

		// Token: 0x060036FA RID: 14074 RVA: 0x000EA183 File Offset: 0x000E9183
		public override XmlCDataSection CreateCDataSection(string data)
		{
			return new ConfigXmlCDataSection(this._filename, this.LineNumber, data, this);
		}

		// Token: 0x060036FB RID: 14075 RVA: 0x000EA198 File Offset: 0x000E9198
		public override XmlComment CreateComment(string data)
		{
			return new ConfigXmlComment(this._filename, this.LineNumber, data, this);
		}

		// Token: 0x060036FC RID: 14076 RVA: 0x000EA1AD File Offset: 0x000E91AD
		public override XmlSignificantWhitespace CreateSignificantWhitespace(string data)
		{
			return new ConfigXmlSignificantWhitespace(this._filename, this.LineNumber, data, this);
		}

		// Token: 0x060036FD RID: 14077 RVA: 0x000EA1C2 File Offset: 0x000E91C2
		public override XmlWhitespace CreateWhitespace(string data)
		{
			return new ConfigXmlWhitespace(this._filename, this.LineNumber, data, this);
		}

		// Token: 0x040031B6 RID: 12726
		private XmlTextReader _reader;

		// Token: 0x040031B7 RID: 12727
		private int _lineOffset;

		// Token: 0x040031B8 RID: 12728
		private string _filename;
	}
}
