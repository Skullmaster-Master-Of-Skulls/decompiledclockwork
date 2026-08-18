using System;
using System.Configuration.Internal;
using System.Xml;

namespace System.Configuration
{
	// Token: 0x020006F4 RID: 1780
	internal sealed class ConfigXmlSignificantWhitespace : XmlSignificantWhitespace, IConfigErrorInfo
	{
		// Token: 0x06003703 RID: 14083 RVA: 0x000EA244 File Offset: 0x000E9244
		public ConfigXmlSignificantWhitespace(string filename, int line, string strData, XmlDocument doc) : base(strData, doc)
		{
			this._line = line;
			this._filename = filename;
		}

		// Token: 0x17000CC2 RID: 3266
		// (get) Token: 0x06003704 RID: 14084 RVA: 0x000EA25D File Offset: 0x000E925D
		int IConfigErrorInfo.LineNumber
		{
			get
			{
				return this._line;
			}
		}

		// Token: 0x17000CC3 RID: 3267
		// (get) Token: 0x06003705 RID: 14085 RVA: 0x000EA265 File Offset: 0x000E9265
		string IConfigErrorInfo.Filename
		{
			get
			{
				return this._filename;
			}
		}

		// Token: 0x06003706 RID: 14086 RVA: 0x000EA270 File Offset: 0x000E9270
		public override XmlNode CloneNode(bool deep)
		{
			XmlNode xmlNode = base.CloneNode(deep);
			ConfigXmlSignificantWhitespace configXmlSignificantWhitespace = xmlNode as ConfigXmlSignificantWhitespace;
			if (configXmlSignificantWhitespace != null)
			{
				configXmlSignificantWhitespace._line = this._line;
				configXmlSignificantWhitespace._filename = this._filename;
			}
			return xmlNode;
		}

		// Token: 0x040031BB RID: 12731
		private int _line;

		// Token: 0x040031BC RID: 12732
		private string _filename;
	}
}
