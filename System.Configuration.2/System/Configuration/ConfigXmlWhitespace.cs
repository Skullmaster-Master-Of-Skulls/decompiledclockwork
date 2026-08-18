using System;
using System.Configuration.Internal;
using System.Xml;

namespace System.Configuration
{
	// Token: 0x02000049 RID: 73
	internal sealed class ConfigXmlWhitespace : XmlWhitespace, IConfigErrorInfo
	{
		// Token: 0x06000310 RID: 784 RVA: 0x000126B8 File Offset: 0x000108B8
		public ConfigXmlWhitespace(string filename, int line, string comment, XmlDocument doc) : base(comment, doc)
		{
			this._line = line;
			this._filename = filename;
		}

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x06000311 RID: 785 RVA: 0x000126D1 File Offset: 0x000108D1
		int IConfigErrorInfo.LineNumber
		{
			get
			{
				return this._line;
			}
		}

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x06000312 RID: 786 RVA: 0x000126D9 File Offset: 0x000108D9
		string IConfigErrorInfo.Filename
		{
			get
			{
				return this._filename;
			}
		}

		// Token: 0x06000313 RID: 787 RVA: 0x000126E4 File Offset: 0x000108E4
		public override XmlNode CloneNode(bool deep)
		{
			XmlNode xmlNode = base.CloneNode(deep);
			ConfigXmlWhitespace configXmlWhitespace = xmlNode as ConfigXmlWhitespace;
			if (configXmlWhitespace != null)
			{
				configXmlWhitespace._line = this._line;
				configXmlWhitespace._filename = this._filename;
			}
			return xmlNode;
		}

		// Token: 0x0400023A RID: 570
		private int _line;

		// Token: 0x0400023B RID: 571
		private string _filename;
	}
}
