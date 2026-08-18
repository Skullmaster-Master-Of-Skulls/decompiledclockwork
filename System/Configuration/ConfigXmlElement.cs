using System;
using System.Configuration.Internal;
using System.Xml;

namespace System.Configuration
{
	// Token: 0x020006F3 RID: 1779
	internal sealed class ConfigXmlElement : XmlElement, IConfigErrorInfo
	{
		// Token: 0x060036FF RID: 14079 RVA: 0x000EA1DF File Offset: 0x000E91DF
		public ConfigXmlElement(string filename, int line, string prefix, string localName, string namespaceUri, XmlDocument doc) : base(prefix, localName, namespaceUri, doc)
		{
			this._line = line;
			this._filename = filename;
		}

		// Token: 0x17000CC0 RID: 3264
		// (get) Token: 0x06003700 RID: 14080 RVA: 0x000EA1FC File Offset: 0x000E91FC
		int IConfigErrorInfo.LineNumber
		{
			get
			{
				return this._line;
			}
		}

		// Token: 0x17000CC1 RID: 3265
		// (get) Token: 0x06003701 RID: 14081 RVA: 0x000EA204 File Offset: 0x000E9204
		string IConfigErrorInfo.Filename
		{
			get
			{
				return this._filename;
			}
		}

		// Token: 0x06003702 RID: 14082 RVA: 0x000EA20C File Offset: 0x000E920C
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

		// Token: 0x040031B9 RID: 12729
		private int _line;

		// Token: 0x040031BA RID: 12730
		private string _filename;
	}
}
