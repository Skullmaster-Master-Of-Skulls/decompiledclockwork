using System;
using System.Configuration.Internal;
using System.Xml;

namespace System.Configuration
{
	// Token: 0x020006F0 RID: 1776
	internal sealed class ConfigXmlCDataSection : XmlCDataSection, IConfigErrorInfo
	{
		// Token: 0x060036E9 RID: 14057 RVA: 0x000E9F44 File Offset: 0x000E8F44
		public ConfigXmlCDataSection(string filename, int line, string data, XmlDocument doc) : base(data, doc)
		{
			this._line = line;
			this._filename = filename;
		}

		// Token: 0x17000CB8 RID: 3256
		// (get) Token: 0x060036EA RID: 14058 RVA: 0x000E9F5D File Offset: 0x000E8F5D
		int IConfigErrorInfo.LineNumber
		{
			get
			{
				return this._line;
			}
		}

		// Token: 0x17000CB9 RID: 3257
		// (get) Token: 0x060036EB RID: 14059 RVA: 0x000E9F65 File Offset: 0x000E8F65
		string IConfigErrorInfo.Filename
		{
			get
			{
				return this._filename;
			}
		}

		// Token: 0x060036EC RID: 14060 RVA: 0x000E9F70 File Offset: 0x000E8F70
		public override XmlNode CloneNode(bool deep)
		{
			XmlNode xmlNode = base.CloneNode(deep);
			ConfigXmlCDataSection configXmlCDataSection = xmlNode as ConfigXmlCDataSection;
			if (configXmlCDataSection != null)
			{
				configXmlCDataSection._line = this._line;
				configXmlCDataSection._filename = this._filename;
			}
			return xmlNode;
		}

		// Token: 0x040031B2 RID: 12722
		private int _line;

		// Token: 0x040031B3 RID: 12723
		private string _filename;
	}
}
