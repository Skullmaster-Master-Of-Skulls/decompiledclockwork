using System;
using System.Configuration.Internal;
using System.Xml;

namespace System.Configuration
{
	// Token: 0x020006EF RID: 1775
	internal sealed class ConfigXmlAttribute : XmlAttribute, IConfigErrorInfo
	{
		// Token: 0x060036E5 RID: 14053 RVA: 0x000E9EDC File Offset: 0x000E8EDC
		public ConfigXmlAttribute(string filename, int line, string prefix, string localName, string namespaceUri, XmlDocument doc) : base(prefix, localName, namespaceUri, doc)
		{
			this._line = line;
			this._filename = filename;
		}

		// Token: 0x17000CB6 RID: 3254
		// (get) Token: 0x060036E6 RID: 14054 RVA: 0x000E9EF9 File Offset: 0x000E8EF9
		int IConfigErrorInfo.LineNumber
		{
			get
			{
				return this._line;
			}
		}

		// Token: 0x17000CB7 RID: 3255
		// (get) Token: 0x060036E7 RID: 14055 RVA: 0x000E9F01 File Offset: 0x000E8F01
		string IConfigErrorInfo.Filename
		{
			get
			{
				return this._filename;
			}
		}

		// Token: 0x060036E8 RID: 14056 RVA: 0x000E9F0C File Offset: 0x000E8F0C
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

		// Token: 0x040031B0 RID: 12720
		private int _line;

		// Token: 0x040031B1 RID: 12721
		private string _filename;
	}
}
