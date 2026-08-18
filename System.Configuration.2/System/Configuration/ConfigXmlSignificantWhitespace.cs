using System;
using System.Configuration.Internal;
using System.Xml;

namespace System.Configuration
{
	// Token: 0x02000047 RID: 71
	internal sealed class ConfigXmlSignificantWhitespace : XmlSignificantWhitespace, IConfigErrorInfo
	{
		// Token: 0x06000308 RID: 776 RVA: 0x000125F1 File Offset: 0x000107F1
		public ConfigXmlSignificantWhitespace(string filename, int line, string strData, XmlDocument doc) : base(strData, doc)
		{
			this._line = line;
			this._filename = filename;
		}

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x06000309 RID: 777 RVA: 0x0001260A File Offset: 0x0001080A
		int IConfigErrorInfo.LineNumber
		{
			get
			{
				return this._line;
			}
		}

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x0600030A RID: 778 RVA: 0x00012612 File Offset: 0x00010812
		string IConfigErrorInfo.Filename
		{
			get
			{
				return this._filename;
			}
		}

		// Token: 0x0600030B RID: 779 RVA: 0x0001261C File Offset: 0x0001081C
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

		// Token: 0x04000236 RID: 566
		private int _line;

		// Token: 0x04000237 RID: 567
		private string _filename;
	}
}
