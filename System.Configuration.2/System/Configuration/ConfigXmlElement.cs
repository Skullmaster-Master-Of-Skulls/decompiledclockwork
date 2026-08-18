using System;
using System.Configuration.Internal;
using System.Xml;

namespace System.Configuration
{
	// Token: 0x02000045 RID: 69
	internal sealed class ConfigXmlElement : XmlElement, IConfigErrorInfo
	{
		// Token: 0x060002FE RID: 766 RVA: 0x000124EC File Offset: 0x000106EC
		public ConfigXmlElement(string filename, int line, string prefix, string localName, string namespaceUri, XmlDocument doc) : base(prefix, localName, namespaceUri, doc)
		{
			this._line = line;
			this._filename = filename;
		}

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x060002FF RID: 767 RVA: 0x00012509 File Offset: 0x00010709
		int IConfigErrorInfo.LineNumber
		{
			get
			{
				return this._line;
			}
		}

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x06000300 RID: 768 RVA: 0x00012511 File Offset: 0x00010711
		string IConfigErrorInfo.Filename
		{
			get
			{
				return this._filename;
			}
		}

		// Token: 0x06000301 RID: 769 RVA: 0x0001251C File Offset: 0x0001071C
		public override XmlNode CloneNode(bool deep)
		{
			XmlNode xmlNode = base.CloneNode(deep);
			ConfigXmlElement configXmlElement = xmlNode as ConfigXmlElement;
			if (configXmlElement != null)
			{
				configXmlElement._line = this._line;
				configXmlElement._filename = this._filename;
			}
			return xmlNode;
		}

		// Token: 0x04000230 RID: 560
		private int _line;

		// Token: 0x04000231 RID: 561
		private string _filename;
	}
}
