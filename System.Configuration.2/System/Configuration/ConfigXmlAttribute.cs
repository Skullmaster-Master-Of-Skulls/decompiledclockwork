using System;
using System.Configuration.Internal;
using System.Xml;

namespace System.Configuration
{
	// Token: 0x02000042 RID: 66
	internal sealed class ConfigXmlAttribute : XmlAttribute, IConfigErrorInfo
	{
		// Token: 0x060002F2 RID: 754 RVA: 0x000123BC File Offset: 0x000105BC
		public ConfigXmlAttribute(string filename, int line, string prefix, string localName, string namespaceUri, XmlDocument doc) : base(prefix, localName, namespaceUri, doc)
		{
			this._line = line;
			this._filename = filename;
		}

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x060002F3 RID: 755 RVA: 0x000123D9 File Offset: 0x000105D9
		int IConfigErrorInfo.LineNumber
		{
			get
			{
				return this._line;
			}
		}

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x060002F4 RID: 756 RVA: 0x000123E1 File Offset: 0x000105E1
		string IConfigErrorInfo.Filename
		{
			get
			{
				return this._filename;
			}
		}

		// Token: 0x060002F5 RID: 757 RVA: 0x000123EC File Offset: 0x000105EC
		public override XmlNode CloneNode(bool deep)
		{
			XmlNode xmlNode = base.CloneNode(deep);
			ConfigXmlAttribute configXmlAttribute = xmlNode as ConfigXmlAttribute;
			if (configXmlAttribute != null)
			{
				configXmlAttribute._line = this._line;
				configXmlAttribute._filename = this._filename;
			}
			return xmlNode;
		}

		// Token: 0x0400022A RID: 554
		private int _line;

		// Token: 0x0400022B RID: 555
		private string _filename;
	}
}
