using System;
using System.Configuration.Internal;
using System.Xml;

namespace System.Configuration
{
	// Token: 0x02000048 RID: 72
	internal sealed class ConfigXmlText : XmlText, IConfigErrorInfo
	{
		// Token: 0x0600030C RID: 780 RVA: 0x00012654 File Offset: 0x00010854
		public ConfigXmlText(string filename, int line, string strData, XmlDocument doc) : base(strData, doc)
		{
			this._line = line;
			this._filename = filename;
		}

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x0600030D RID: 781 RVA: 0x0001266D File Offset: 0x0001086D
		int IConfigErrorInfo.LineNumber
		{
			get
			{
				return this._line;
			}
		}

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x0600030E RID: 782 RVA: 0x00012675 File Offset: 0x00010875
		string IConfigErrorInfo.Filename
		{
			get
			{
				return this._filename;
			}
		}

		// Token: 0x0600030F RID: 783 RVA: 0x00012680 File Offset: 0x00010880
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

		// Token: 0x04000238 RID: 568
		private int _line;

		// Token: 0x04000239 RID: 569
		private string _filename;
	}
}
