using System;
using System.Configuration.Internal;
using System.Xml;

namespace System.Configuration
{
	// Token: 0x02000043 RID: 67
	internal sealed class ConfigXmlCDataSection : XmlCDataSection, IConfigErrorInfo
	{
		// Token: 0x060002F6 RID: 758 RVA: 0x00012424 File Offset: 0x00010624
		public ConfigXmlCDataSection(string filename, int line, string data, XmlDocument doc) : base(data, doc)
		{
			this._line = line;
			this._filename = filename;
		}

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x060002F7 RID: 759 RVA: 0x0001243D File Offset: 0x0001063D
		int IConfigErrorInfo.LineNumber
		{
			get
			{
				return this._line;
			}
		}

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x060002F8 RID: 760 RVA: 0x00012445 File Offset: 0x00010645
		string IConfigErrorInfo.Filename
		{
			get
			{
				return this._filename;
			}
		}

		// Token: 0x060002F9 RID: 761 RVA: 0x00012450 File Offset: 0x00010650
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

		// Token: 0x0400022C RID: 556
		private int _line;

		// Token: 0x0400022D RID: 557
		private string _filename;
	}
}
