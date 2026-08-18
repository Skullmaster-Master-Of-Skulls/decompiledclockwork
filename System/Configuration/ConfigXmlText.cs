using System;
using System.Configuration.Internal;
using System.Xml;

namespace System.Configuration
{
	// Token: 0x020006F5 RID: 1781
	internal sealed class ConfigXmlText : XmlText, IConfigErrorInfo
	{
		// Token: 0x06003707 RID: 14087 RVA: 0x000EA2A8 File Offset: 0x000E92A8
		public ConfigXmlText(string filename, int line, string strData, XmlDocument doc) : base(strData, doc)
		{
			this._line = line;
			this._filename = filename;
		}

		// Token: 0x17000CC4 RID: 3268
		// (get) Token: 0x06003708 RID: 14088 RVA: 0x000EA2C1 File Offset: 0x000E92C1
		int IConfigErrorInfo.LineNumber
		{
			get
			{
				return this._line;
			}
		}

		// Token: 0x17000CC5 RID: 3269
		// (get) Token: 0x06003709 RID: 14089 RVA: 0x000EA2C9 File Offset: 0x000E92C9
		string IConfigErrorInfo.Filename
		{
			get
			{
				return this._filename;
			}
		}

		// Token: 0x0600370A RID: 14090 RVA: 0x000EA2D4 File Offset: 0x000E92D4
		public override XmlNode CloneNode(bool deep)
		{
			XmlNode xmlNode = base.CloneNode(deep);
			ConfigXmlText configXmlText = xmlNode as ConfigXmlText;
			if (configXmlText != null)
			{
				configXmlText._line = this._line;
				configXmlText._filename = this._filename;
			}
			return xmlNode;
		}

		// Token: 0x040031BD RID: 12733
		private int _line;

		// Token: 0x040031BE RID: 12734
		private string _filename;
	}
}
